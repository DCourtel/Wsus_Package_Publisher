using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wsus_Package_Publisher
{
    internal class ApprovalObject
    {
        internal enum Approvals
        {
            Unchanged,
            ApproveForInstallation,
            ApproveForOptionalInstallation,
            ApproveForUninstallation,
            NotApproved
        }
        private Guid _groupId;
        private Approvals _approval = Approvals.Unchanged;
        private DateTime _deadLine = DateTime.MaxValue;
        private bool _hasDeadLine = false;

        internal ApprovalObject(Guid groupId, Approvals approval, DateTime deadLine)
        {
            GroupId = groupId;
            Approval = approval;
            DeadLine = deadLine;
        }

        internal ApprovalObject(Guid groupId, Approvals approval)
        {
            GroupId = groupId;
            Approval = approval;
        }
                
        #region (Properties - Propriétés)

        internal Guid GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        internal Approvals Approval
        {
            get { return _approval; }
            set { _approval = value; }
        }

        internal DateTime DeadLine
        {
            get { return _deadLine; }
            set 
            { 
                _deadLine = value;
                HasDeadLine = true;
            }
        }

        internal bool HasDeadLine
        {
            get { return _hasDeadLine; }
            set 
            {
                _hasDeadLine = value; 
                if(!value)
                    _deadLine = DateTime.MaxValue;
            }
        }

        #endregion

    }
}
