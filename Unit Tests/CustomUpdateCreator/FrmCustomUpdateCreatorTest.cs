using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomUpdateCreator;

namespace Unit_Tests_CustomUpdateCreator
{
    [TestClass]
    public class FrmCustomUpdateCreatorTest
    {
        private string baseFolder = @"C:\Users\AdminSRV\source\repos\Wsus_Package_Publisher\Unit Tests\CustomUpdateCreator";

        [TestMethod]
        public void RefersToUserProfileTest()
        {
            frmCustomUpdateCreator frm = new frmCustomUpdateCreator(baseFolder + @"\CustomUpdate With UserProfile reference.CustAct");

            Assert.IsTrue(frm.RefersToUserProfile);
            Assert.IsFalse(frm.RefersToHKeyCurrentUser);

            frm = new frmCustomUpdateCreator(baseFolder + @"\CustomUpdate Without UserProfile reference.CustAct");
            Assert.IsFalse(frm.RefersToUserProfile);
        }

        [TestMethod]
        public void RefersToHKCUTest()
        {
            frmCustomUpdateCreator frm = new frmCustomUpdateCreator(baseFolder + @"\CustomUpdate With HKCU reference.CustAct");

            Assert.IsTrue(frm.RefersToHKeyCurrentUser);
            Assert.IsFalse(frm.RefersToUserProfile);

            frm = new frmCustomUpdateCreator(baseFolder + @"\CustomUpdate Without HKCU reference.CustAct");
            Assert.IsFalse(frm.RefersToHKeyCurrentUser);
        }
    }
}
