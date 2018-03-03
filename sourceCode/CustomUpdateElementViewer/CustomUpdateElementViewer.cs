using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomUpdateElements;

namespace CustomUpdateElementViewer
{
    public partial class CustomUpdateElementViewer : UserControl
    {
        public CustomUpdateElementViewer()
        {
            InitializeComponent();
        }

        #region (Publics Methods - Méthodes publics)

        public void AddElement(GenericElement elementToAdd)
        {
            tlpElementViewer.Controls.Add(elementToAdd);
            elementToAdd.Dock = DockStyle.Fill;
            elementToAdd.ElementDoubleClick += new GenericElement.ElementDoubleClickEventHandler(Element_OnDoubleClick);
        }

        

        #endregion (Publics Methods - Méthodes publics)

        #region (Privates Methods - Méthodes privés)






        #endregion (Privates Methods - Méthodes privés)


        #region (Answers to events - Réponses aux évenements)

        private void Element_OnDoubleClick(GenericElement sender)
        {
            if (ElementDoubleClick != null)
                ElementDoubleClick(sender);
        }


        #endregion (Answers to events - Réponses aux évenements)

        #region (Public Event - Evenements Public)

        public delegate void ElementDoubleClickEventHandler(GenericElement sender);
        public event ElementDoubleClickEventHandler ElementDoubleClick;

        #endregion (Public Event - Evenements Public)
    }
}
