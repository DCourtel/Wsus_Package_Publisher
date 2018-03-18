using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SUT = CustomUpdateEngine.RebootAction;

namespace Unit_Tests_CustomeUpdateEngine.Actions
{
    class RebootAction
    {
        [TestClass]
        public class Run_Should
        {
            [TestMethod]            
            public void RebootTheComputer_WhenCalled()
            {
                //// Arrange
                //SUT action = new SUT(Tools.GetXmlFragment("Reboot.CustAct"));
                //Process[] processes;
                //var finalResult = Tools.GetReturnCodeAction();

                //// Act
                //processes = Process.GetProcessesByName("Shutdown");
                //Assert.AreEqual(0, processes.Length);
                //action.Run(ref finalResult);
                //processes = Process.GetProcessesByName("Shutdown");

                //// Assert
                //Assert.AreEqual(1, processes.Length);
            }
        }
    }
}
