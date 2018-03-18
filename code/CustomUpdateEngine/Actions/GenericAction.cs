using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomUpdateEngine
{
    public class GenericAction
    {
        #region (Methods)

        public virtual void Run(ref ReturnCodeAction returnCode) { }

        public void LogInitialization(string xmlFragment)
        {
            Logger.Write("Initializing " + this.GetType().ToString() + " with : " + xmlFragment);
        }

        public void LogCompletion()
        {
            Logger.Write("Successfully initialized " + this.GetType().ToString());
        }

        #endregion (Methods)
    }
}
