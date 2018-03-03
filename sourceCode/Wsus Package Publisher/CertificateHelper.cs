using System;
using System.Collections.Generic;
using System.Text;
using CERTENROLLLib;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Wsus_Package_Publisher
{
    internal class CertificateHelper
    {
        internal enum CertificateStatus
        {
            Absent,
            Valid,
            NearExpiration,
            Expired,
            NotYetValid,
            Invalid,
            NotRequired
        }
        internal struct CertificateStatusResult
        {
            internal CertificateStatus Status { get; set; }
            internal DateTime ExpirationDate { get; set; }
        }

        internal static X509Certificate2 CreateSelfSignedCertificate(string subjectName)
        {
            // create DN for subject and issuer
            var dn = new CX500DistinguishedName();
            dn.Encode("CN=" + subjectName, X500NameFlags.XCN_CERT_NAME_STR_NONE);

            // create a new private key for the certificate
            CX509PrivateKey privateKey = new CX509PrivateKey();
            privateKey.ProviderName = "Microsoft Base Cryptographic Provider v1.0";
            privateKey.MachineContext = true;
            privateKey.Length = 2048;
            privateKey.KeySpec = X509KeySpec.XCN_AT_SIGNATURE; // use is not limited
            privateKey.ExportPolicy = X509PrivateKeyExportFlags.XCN_NCRYPT_ALLOW_PLAINTEXT_EXPORT_FLAG;
            privateKey.Create();

            // Use the stronger SHA512 hashing algorithm
            var hashobj = new CObjectId();
            hashobj.InitializeFromAlgorithmName(ObjectIdGroupId.XCN_CRYPT_HASH_ALG_OID_GROUP_ID,
                ObjectIdPublicKeyFlags.XCN_CRYPT_OID_INFO_PUBKEY_ANY,
                AlgorithmFlags.AlgorithmFlagsNone, "SHA512");

            // add extended key usage if you want - look at MSDN for a list of possible OIDs
            var oid = new CObjectId();
            oid.InitializeFromValue("1.3.6.1.5.5.7.3.3"); // Signature du code.
            var oidlist = new CObjectIds();
            oidlist.Add(oid);
            var eku = new CX509ExtensionEnhancedKeyUsage();
            eku.InitializeEncode(oidlist);

            // Create the self signing request
            var cert = new CX509CertificateRequestCertificate();
            cert.InitializeFromPrivateKey(X509CertificateEnrollmentContext.ContextMachine, privateKey, "");
            cert.Subject = dn;
            cert.Issuer = dn; // the issuer and the subject are the same
            DateTime today = DateTime.Now.ToUniversalTime();
            cert.NotBefore = today;
            // this cert is valid for 5 years
            cert.NotAfter = today.AddYears(5);
            cert.X509Extensions.Add((CX509Extension)eku); // add the EKU
            cert.HashAlgorithm = hashobj; // Specify the hashing algorithm
            cert.Encode(); // encode the certificate

            // Do the final enrollment process
            var enroll = new CX509Enrollment();
            enroll.InitializeFromRequest(cert); // load the certificate
            enroll.CertificateFriendlyName = subjectName; // Optional: add a friendly name
            string csr = enroll.CreateRequest(); // Output the request in base64
            // and install it back as the response
            enroll.InstallResponse(InstallResponseRestrictionFlags.AllowUntrustedCertificate,
                csr, EncodingType.XCN_CRYPT_STRING_BASE64, ""); // no password
            // output a base64 encoded PKCS#12 so we can import it back to the .Net security classes
            var base64encoded = enroll.CreatePFX("", // no password, this is for internal consumption
                PFXExportOptions.PFXExportChainWithRoot);

            // instantiate the target class with the PKCS#12 data (and the empty password)
            return new System.Security.Cryptography.X509Certificates.X509Certificate2(
                System.Convert.FromBase64String(base64encoded), "",
                // mark the private key as exportable (this is usually what you want to do)
                System.Security.Cryptography.X509Certificates.X509KeyStorageFlags.Exportable
            );
        }

        internal static CertificateStatusResult GetCertificateStatus(string certFilePath, bool ignoreCertificateErrors)
        {
            CertificateStatusResult result = new CertificateStatusResult();

            System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certFilePath);

            DateTime expirationDate = certificate.NotAfter;
            DateTime effectiveDate = certificate.NotBefore;
            Logger.Write("EffectiveDate : " + effectiveDate.ToString() + " / ExpirationDate : " + expirationDate.ToString());
            bool valid = certificate.Verify();
            result.ExpirationDate = expirationDate;
            System.IO.File.Delete(certFilePath);
            if (!valid)
            {
                if (DateTime.Compare(DateTime.Now, expirationDate) == 1)
                {
                    Logger.Write("Expired ! ");
                    if (ignoreCertificateErrors)
                    {
                        Logger.Write("Will ignore Code-Signing Certificate Errors !!!");
                        result.Status = CertificateStatus.Valid;
                        return result;
                    }
                    else
                    {
                        result.Status = CertificateStatus.Expired;
                        return result;
                    }
                }
                if (DateTime.Compare(DateTime.Now, effectiveDate) == -1)
                {
                    Logger.Write("Not Yet Valid ! ");
                    if (ignoreCertificateErrors)
                    {
                        Logger.Write("Will ignore Code-Signing Certificate Errors !!!");
                        result.Status = CertificateStatus.Valid;
                        return result;
                    }
                    else
                    {
                        result.Status = CertificateStatus.NotYetValid;
                        return result;
                    }
                }
                Logger.Write("Invalid !");
                if (ignoreCertificateErrors)
                {
                    Logger.Write("Will ignore Code-Signing Certificate Errors !!!");
                    result.Status = CertificateStatus.Valid;
                    return result;
                }
                else
                {
                    result.Status = CertificateStatus.Invalid;
                    return result;
                }
            }
            if (expirationDate.Subtract(DateTime.Now).Days < 30)
            {
                Logger.Write("Near Expiration");
                result.Status = CertificateStatus.NearExpiration;
                return result;
            }
            Logger.Write("Valid");
            result.Status = CertificateStatus.Valid;
            return result;
        }

        internal static int GetCertificateKeyLength(string certFilePath)
        {
            Logger.EnteringMethod(certFilePath);
            int result = 0;

            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certFilePath);
                result = certificate.PublicKey.Key.KeySize;
            }
            catch (Exception ex) { Logger.Write("**** " + ex.Message); }

            Logger.Write("Will return " + result);
            return result;
        }

        internal static void InstallCertificate(string certFile, System.Security.Cryptography.X509Certificates.StoreName store)
        {
            Logger.EnteringMethod();
            InstallCertificate(certFile, store, string.Empty);
        }

        internal static void InstallCertificate(string certFile, System.Security.Cryptography.X509Certificates.StoreName store, string password)
        {
            Logger.EnteringMethod(store.ToString());
            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificate;
                if(!string.IsNullOrEmpty(password))
                    certificate  = new System.Security.Cryptography.X509Certificates.X509Certificate2(certFile, password);
                else
                    certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certFile);
                System.Security.Cryptography.X509Certificates.X509Store certStore = new System.Security.Cryptography.X509Certificates.X509Store(store, System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine);

                certStore.Open(System.Security.Cryptography.X509Certificates.OpenFlags.ReadWrite);
                certStore.Add(certificate);
                certStore.Close();
                Logger.Write("Successfuly imported in " + store.ToString());
            }
            catch (Exception ex)
            { Logger.Write("**** " + ex.Message); }
        }

        internal static bool IsSelfSigned(string certFilePath, string certPassword)
        {
            Logger.EnteringMethod(certFilePath);

            try
            {
                System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(certFilePath, certPassword);
                bool result = certificate.SubjectName.Name.Equals(certificate.IssuerName.Name);
//#if(DEBUG)
                System.Windows.Forms.MessageBox.Show(certificate.SubjectName.Name.ToString() + "\r\n" + certificate.IssuerName.Name.ToString() + "\r\n" + result);
//#endif
                return result;
            }
            catch (Exception ex) { Logger.Write("**** " + ex.Message); }

            return false;
        }

        internal static bool IsValid(string filePath, string certPassword)
        {
            Logger.EnteringMethod();

            System.Security.Cryptography.X509Certificates.X509Certificate2 certificate = new System.Security.Cryptography.X509Certificates.X509Certificate2(filePath, certPassword);
            return certificate.Verify();
        }
    }
}
