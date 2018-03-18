using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using WindowsInstaller;
using System.Text.RegularExpressions;

namespace CustomUpdateEngine
{
    public class UninstallMsiProductByGuidAction : GenericAction
    {
        public struct MsiProduct
        {
            private string _name;
            private string _id;

            public MsiProduct(string name, string id)
            {
                _name = name;
                _id = id;
            }

            public string Name { get { return _name; } private set { _name = value; } }
            public string ID { get { return _id; } private set { _id = value; } }
        }

        public UninstallMsiProductByGuidAction(string xmlFragment)
        {
            Logger.Write("Get UninstallMsiProductByGuidAction from : " + xmlFragment);

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlFragment));

            if (!reader.ReadToFollowing("MsiProductCodes"))
                throw new ArgumentException("Unable to find token : MsiProductCodes");
            MsiProductCodes = reader.ReadString();
            if (!reader.ReadToFollowing("Exceptions"))
                throw new ArgumentException("Unable to find token : Exceptions");
            Exceptions = reader.ReadString();
            if (!reader.ReadToFollowing("DontUninstallIfNoException"))
                throw new ArgumentException("Unable to find token : DontUninstallIfNoException");
            DontUninstallIfNoException = Convert.ToBoolean(reader.ReadString());
            if (!reader.ReadToFollowing("KillProcess"))
                throw new ArgumentException("Unable to find token : KillProcess");
            KillProcess = Convert.ToBoolean(reader.ReadString());
            if (!reader.ReadToFollowing("KillAfter"))
                throw new ArgumentException("Unable to find token : KillAfter");
            KillAfter = reader.ReadElementContentAsInt();

            Logger.Write("End of Initializing of UninstallMsiProductByGuidAction.");
        }

        public string MsiProductCodes { get; set; }
        public string Exceptions { get; set; }
        public bool DontUninstallIfNoException { get; set; }
        public bool KillProcess { get; set; }
        public int KillAfter { get; set; }


        public override void Run(ref ReturnCodeAction returnCode)
        {
            Logger.Write("Running UninstallMsiProductByGuidAction. MsiProductCodes= " + this.MsiProductCodes + " Exceptions= " + this.Exceptions);

            try
            {
                Logger.Write("Getting all installed product on this computer");
                List<MsiProduct> installedProducts = GetMsiProducts();
                Logger.Write("Found " + installedProducts.Count + " products installed.");

                if (!this.DontUninstallIfNoException || this.IsAtLeastOneExceptionIsInstalled(installedProducts))
                {
                    Logger.Write("Searching products to uninstall");
                    List<MsiProduct> productsToUninstall = this.GetProductsToUninstall(installedProducts);
                    Logger.Write(productsToUninstall.Count + " products to uninstall.");

                    foreach (MsiProduct product in productsToUninstall)
                    {
                        this.UninstallProduct(product);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("An error occurs while preparing uninstallation : " + ex.Message);
                throw;
            }

            Logger.Write("End of UninstallMsiProductByGuidAction.");
        }

        public static List<MsiProduct> GetMsiProducts()
        {
            List<MsiProduct> msiProducts = new List<MsiProduct>();

            try
            {
                Type type = Type.GetTypeFromProgID("WindowsInstaller.Installer");
                Installer installer = Activator.CreateInstance(type) as Installer;

                StringList products = installer.Products;
                foreach (string productGuid in products)
                {
                    try
                    {
                        msiProducts.Add(new MsiProduct(installer.ProductInfo[productGuid, "ProductName"], GetFormattedIdentifyingNumber(productGuid)));
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception ex)
            {
                Logger.Write("An error occurs while getting list of all MSI products installed on thhis computer : \r\n" + ex.Message);
            }

            return msiProducts;
        }

        private bool IsAtLeastOneExceptionIsInstalled(List<MsiProduct> installedProducts)
        {
            Logger.Write("Searching at least one exception in installed products");
            List<string> exceptions = this.SplitMsiProductCodes(Exceptions);

            foreach (MsiProduct installedProduct in installedProducts)
            {
                foreach (string exception in exceptions)
                {
                    if (PatternMatchMsiCode(installedProduct.ID, exception))
                    {
                        Logger.Write("At least one exception is installed : " + installedProduct.Name + " (" + installedProduct.ID + ") match exception : " + exception);
                        return true;
                    }
                }
            }
            Logger.Write("No installed products are matching exception");
            return false;
        }

        private static string GetFormattedIdentifyingNumber(string identifyingNumber)
        {
            return identifyingNumber.Substring(1, 36); // Remove leading '{' and trailing '}'
        }

        private List<MsiProduct> GetProductsToUninstall(List<MsiProduct> allInstalledProducts)
        {
            List<MsiProduct> productsToUninstall = new List<MsiProduct>();
            List<string> productToFind = this.SplitMsiProductCodes(MsiProductCodes);
            List<string> exceptions = this.SplitMsiProductCodes(Exceptions);

            foreach (MsiProduct installedProduct in allInstalledProducts)
            {
                foreach (string product in productToFind)
                {
                    if (PatternMatchMsiCode(installedProduct.ID, product))
                    {
                        bool uninstallIt = true;
                        foreach (string exception in exceptions)
                        {
                            if (PatternMatchMsiCode(installedProduct.ID, exception))
                            {
                                uninstallIt = false;
                                Logger.Write(installedProduct.ID + " match exception " + exception + " (it won't be uninstalled)");
                                break;
                            }
                        }
                        if (uninstallIt)
                        {
                            productsToUninstall.Add(installedProduct);
                            Logger.Write(installedProduct.ID + " is selected for uninstallation (" + installedProduct.Name + ")");
                            break;
                        }
                    }
                }
            }

            return productsToUninstall;
        }

        private List<string> SplitMsiProductCodes(string textToSplit)
        {
            List<string> msiProducts = new List<string>();

            string[] msiProductsArray = textToSplit.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string exception in msiProductsArray)
            {
                msiProducts.Add(exception);
            }

            return msiProducts;
        }

        private bool PatternMatchMsiCode(string msiCode, string pattern)
        {
            bool result = false;

            if (pattern.Contains("%") || pattern.Contains("_"))
            {
                Regex regExpr = new Regex("^" + GetRegExpPattern(pattern) + "$", RegexOptions.IgnoreCase);
                if (regExpr.IsMatch(msiCode))
                {
                    result = true;
                }
            }
            else
            {
                Logger.Write("pattern doesn't contains joker characters.");
                if (pattern.Length == 36)
                {
                    result = String.Compare(msiCode, pattern, true) == 0;
                }
                else
                    Logger.Write("MsiCode is not 36 characters length.");
            }

            if (result)
                Logger.Write(msiCode + " match pattern " + pattern);
            else
                Logger.Write(msiCode + " don't match pattern " + pattern);

            return result;
        }

        private string GetRegExpPattern(string pattern)
        {
            return pattern.Replace("%", @"[ABCDEF\d\-]*").Replace("_", @"[ABCDEF\d\-]");
        }

        private void UninstallProduct(MsiProduct productToUninstall)
        {
            Logger.Write("Starting uninstallation of : " + productToUninstall.Name);
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.Arguments = "/X{" + productToUninstall.ID + "} /qn /norestart /log \"C:\\Windows\\Temp\\Uninstall-" + productToUninstall.ID + ".log\"";
                proc.StartInfo.FileName = Tools.GetExpandedPath(@"%windir%\system32\msiexec.exe");

                proc.Start();

                try
                {
                    if (this.KillProcess)
                    {
                        if (!proc.WaitForExit(this.KillAfter * 60 * 1000))
                        {
                            proc.Kill();
                            Logger.Write("Process killed.");
                        }
                    }
                    else
                        proc.WaitForExit(int.MaxValue);

                    Logger.Write("Exiting process. With Exite code : " + proc.ExitCode.ToString());
                }
                catch (Exception)
                {
                    Logger.Write("The process is already stopped or doesn't have start.");
                }

                switch (proc.ExitCode)
                {
                    case 0:
                        Logger.Write("Successfully uninstalled " + productToUninstall.Name);
                        break;
                    case 3010:
                        Logger.Write("Successfully uninstalled " + productToUninstall.Name + " (A restart is required)");
                        break;
                    default:
                        Logger.Write("An error occurs while uninstalling " + productToUninstall.Name + " (MsiError : " + proc.ExitCode + ")");
                        break;
                }
                System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                Logger.Write("An error occurs while uninstalling " + productToUninstall.Name + "\r\n" + ex.Message);
            }
        }
    }
}
