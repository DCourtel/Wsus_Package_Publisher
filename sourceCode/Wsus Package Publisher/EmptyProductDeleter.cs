using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.UpdateServices.Administration;

namespace Wsus_Package_Publisher
{
    internal class EmptyProductDeleter
    {
        internal struct EmptyProductDeleterResult
        {
            internal IUpdateCategory DeletedProduct { get; set; }
            internal IUpdateCategory DeletedVendor { get; set; }
        }

        private IUpdateServer wsus;
        private FrmWaiting _waitingForm;
        private System.Threading.Thread _waitingThread;
        private System.Resources.ResourceManager resMan = new System.Resources.ResourceManager("Wsus_Package_Publisher.Resources.Resources", typeof(EmptyProductDeleter).Assembly);

        internal EmptyProductDeleter()
        {
            wsus = AdminProxy.GetUpdateServer();
        }

        internal EmptyProductDeleterResult DeleteProduct(Guid productID)
        {
            Logger.EnteringMethod(productID.ToString());
            IUpdate updateToDelete = null;
            IUpdateCategory productToDelete = wsus.GetUpdateCategory(productID);
            IUpdateCategory vendorToDelete = productToDelete.GetParentUpdateCategory();
            EmptyProductDeleterResult result = new EmptyProductDeleterResult();
            result.DeletedProduct = null;
            result.DeletedVendor = null;
            StartWaitingForm(resMan.GetString("DeletingProduct") + productToDelete.Title);
            updateToDelete = PublishUpdate(productToDelete, vendorToDelete);

            if (updateToDelete != null && MakeLocallyPublished(productToDelete, vendorToDelete))
            {
                DeclineUpdate(updateToDelete);
                CleanUpdateRevision();
                DeleteUpdate(updateToDelete);
                System.Threading.Thread.Sleep(500);
                try
                {
                    IUpdateCategory deletedCategory = wsus.GetUpdateCategory(productToDelete.Id);
                    Logger.Write("Failed to delete : " + productToDelete.Title);
                }
                catch (Exception)
                {
                    Logger.Write("Successfuly deleted : " + productToDelete.Title);
                    result.DeletedProduct = productToDelete;
                    if (vendorToDelete.GetSubcategories().Count == 0)
                    {
                        try
                        {
                            IUpdateCategory deletedCategory = wsus.GetUpdateCategory(vendorToDelete.Id);
                            Logger.Write("Failed to delete : " + vendorToDelete.Title);
                        }
                        catch (Exception)
                        {
                            Logger.Write("Succesfully deleted : " + vendorToDelete.Title);
                            result.DeletedVendor = vendorToDelete;
                            StopWaitingForm();
                            _waitingForm.TopLevel = false;
                            return result;
                        }
                    }
                }
            }
            else
            {
                StopWaitingForm();
                _waitingForm.TopLevel = false;
                return result;
            }

            StopWaitingForm();
            _waitingForm.TopLevel = false;
            return result;
        }

        private IUpdate PublishUpdate(IUpdateCategory productToDelete, IUpdateCategory vendorToDelete)
        {
            Logger.EnteringMethod("Product to Delete : " + productToDelete.Title + " and Vendor to delete : " + vendorToDelete.Title);
            try
            {
                SoftwareDistributionPackage sdp = new SoftwareDistributionPackage();
                string tmpFolderPath;

                sdp.PopulatePackageFromExe("ProductKiller.exe");

                sdp.Title = "Delete Me !";
                sdp.Description = "Delete Me !";
                sdp.VendorName = vendorToDelete.Title;
                sdp.ProductNames.Clear();
                sdp.ProductNames.Add(productToDelete.Title);
                sdp.PackageType = PackageType.Update;

                tmpFolderPath = GetTempFolder();

                if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId))
                    System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId);
                if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Xml\\"))
                    System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Xml\\");
                if (!System.IO.Directory.Exists(tmpFolderPath + sdp.PackageId + "\\Bin\\"))
                    System.IO.Directory.CreateDirectory(tmpFolderPath + sdp.PackageId + "\\Bin\\");

                System.IO.FileInfo updateFile = new System.IO.FileInfo("ProductKiller.exe");
                updateFile.CopyTo(tmpFolderPath + sdp.PackageId + "\\Bin\\" + updateFile.Name);
                sdp.Save(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");
                IPublisher publisher = wsus.GetPublisher(tmpFolderPath + sdp.PackageId + "\\Xml\\" + sdp.PackageId.ToString() + ".xml");

                publisher.PublishPackage(tmpFolderPath + sdp.PackageId + "\\Bin\\", null);
                System.Threading.Thread.Sleep(5000);
                UpdateCollection publishedUpdates = productToDelete.GetUpdates();
                if (publishedUpdates.Count == 1)
                {
                    Logger.Write("Successfuly publish ProductKiller");
                    return publishedUpdates[0];
                }
                else
                {
                    Logger.Write("Failed to publish ProductKiller");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                return null;
            }
        }

        private bool MakeLocallyPublished(IUpdateCategory productToDelete, IUpdateCategory vendorToDelete)
        {
            Logger.EnteringMethod("Product to Delete : " + productToDelete.Title + " and Vendor to delete : " + vendorToDelete.Title);
            try
            {
                List<Guid> Ids = new List<Guid>();
                SqlHelper sqlHelper = SqlHelper.GetInstance();

                sqlHelper.ServerName = GetServerName(); ;
                sqlHelper.DataBaseName = "SUSDB";

                if (!sqlHelper.Connect(string.Empty, string.Empty))
                {
                    Logger.Write("Failed to connect to SQL");
                    return false;
                }
                else
                {
                    Logger.Write("Connected to SQL");
                    Logger.Write("ProductToDelete UpdateSource = " + productToDelete.UpdateSource.ToString());
                    if (productToDelete.UpdateSource == UpdateSource.MicrosoftUpdate)
                    {
                        Ids.Add(productToDelete.Id);
                        sqlHelper.HideUpdatesInConsole(Ids);
                    }
                    Ids.Clear();
                    if (vendorToDelete.GetSubcategories().Count == 1)
                    {
                        Logger.Write("vendorToDelete UpdateSource = " + vendorToDelete.UpdateSource.ToString());
                        if (vendorToDelete.UpdateSource == UpdateSource.MicrosoftUpdate)
                        {
                            Ids.Add(vendorToDelete.Id);
                            sqlHelper.HideUpdatesInConsole(Ids);
                        }
                    }
                    sqlHelper.Disconnect();
                    Logger.Write("End of SQL session");
                }
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
                return false;
            }
            Logger.Write("Successfuly made LocallyPublished");
            return true;
        }

        private void DeclineUpdate(IUpdate updateToDelete)
        {
            Logger.EnteringMethod(updateToDelete.Title);
            try
            {
                updateToDelete.Decline();
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
        }

        private void CleanUpdateRevision()
        {
            Logger.EnteringMethod();
            ICleanupManager cleanupWizard = wsus.GetCleanupManager();
            CleanupScope scope = new CleanupScope();
            scope.CompressUpdates = true;
            CleanupResults results = cleanupWizard.PerformCleanup(scope);
            Logger.Write(results.UpdatesCompressed + " update(s) compressed.");
        }

        private void DeleteUpdate(IUpdate updateToDelete)
        {
            Logger.EnteringMethod(updateToDelete.Title);
            try
            {
                Logger.Write(updateToDelete.UpdateSource.ToString());
                wsus.DeleteUpdate(updateToDelete.Id.UpdateId);
                Logger.Write("Successfuly delete " + updateToDelete.Title);
            }
            catch (Exception ex)
            {
                Logger.Write("**** " + ex.Message);
            }
        }

        private string GetTempFolder()
        {
            Logger.EnteringMethod();
            string tmpFolderPath = Environment.GetEnvironmentVariable("Temp") + "\\Wsus Package Publisher\\";
            if (!System.IO.Directory.Exists(tmpFolderPath))
                System.IO.Directory.CreateDirectory(tmpFolderPath);
            Logger.Write("Returning : " + tmpFolderPath);

            return tmpFolderPath;
        }

        private string GetServerName()
        {
            Logger.EnteringMethod();
            IDatabaseConfiguration dbConf = wsus.GetDatabaseConfiguration();
            Logger.Write("IsUsingWindowsInternalDatabase : " + dbConf.IsUsingWindowsInternalDatabase.ToString());
            Logger.Write("Major Version : " + wsus.Version.Major.ToString());
            if (dbConf.IsUsingWindowsInternalDatabase)
            {
                if (wsus.Version.Major == 3)
                    return @"\\.\pipe\MSSQL$MICROSOFT##SSEE\sql\query";
                else
                    return @"\\.\pipe\Microsoft##WID\tsql\query";
            }
            else
            {
                return dbConf.ServerName;
            }
        }

        private void StartWaitingForm(string description)
        {
            Logger.EnteringMethod();
            _waitingForm = new FrmWaiting();
            _waitingForm.Description = description;
            _waitingForm.GoOn = true;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.BelowNormal;
            _waitingThread = new System.Threading.Thread(new System.Threading.ThreadStart(_waitingForm.ShowForm));
            _waitingThread.Priority = System.Threading.ThreadPriority.AboveNormal;
            _waitingThread.Start();
            System.Threading.Thread.Sleep(200);
        }

        private void StopWaitingForm()
        {
            _waitingForm.GoOn = false;
            _waitingThread.Join(900);
            _waitingThread = null;
            System.Threading.Thread.CurrentThread.Priority = System.Threading.ThreadPriority.Normal;
        }

    }
}
