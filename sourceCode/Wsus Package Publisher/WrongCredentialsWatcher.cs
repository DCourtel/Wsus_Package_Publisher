using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class WrongCredentialsWatcher
    {
        private bool _wrongCredentialsDetected = false;
        private object _wrongCredentialsLocker = new object();
        private bool _continueWithFailedCredentials = false;
        private object _continueWithFailedCredentialsLocker = new object();
        private bool _abortRequested = false;
        private object _abortRequestedLocker = new object();

        internal WrongCredentialsWatcher() { }
        
        internal bool IsWrongCredentials
        {
            get { return _wrongCredentialsDetected; }
            set
            {
                lock (_wrongCredentialsLocker)
                {
                    _wrongCredentialsDetected = value;
                }
            }
        }

        internal bool IsAbortRequested
        {
            get { return _abortRequested; }
            set
            {
                lock (_abortRequestedLocker)
                {
                    _abortRequested = value;
                }
            }
        }

        internal bool ContinueWithFailedCredentials
        {
            get { return _continueWithFailedCredentials; }
            set
            {
                lock (_continueWithFailedCredentialsLocker)
                {
                    _continueWithFailedCredentials = value;
                }
            }
        }


    }
}
