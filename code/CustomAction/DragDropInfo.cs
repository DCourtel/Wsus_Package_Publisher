using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomActions
{
    public class DragDropInfo
    {
        public GenericAction EmbededControl { get; private set; }

        public DragDropInfo(GenericAction control)
        {
            this.EmbededControl = control;
        }
    }
}
