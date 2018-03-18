using CustomActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Unit_Tests_CustomAction
{
    /// <summary>
    ///Classe de test pour AddRegKeyActionTest, destinée à contenir tous
    ///les tests unitaires AddRegKeyActionTest
    ///</summary>
    [TestClass()]
    public class AllCustomActionsTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        // 
        //Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        //Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///Test que tous les éléments XML renvoyé par GetXmlAction() match une propriété de la classe.
        ///</summary>
        [TestMethod()]
        public void AllXmlElementsMatchaProperty()
        {
            GenericAction action = new GenericAction();

            Type[] types = action.GetType().Assembly.GetTypes();
            List<Type> allClass = new List<Type>();
            foreach (Type t in types)
            {
                if (t.IsClass && t.Namespace == "CustomActions" && t.Name != "GenericAction")
                {
                    allClass.Add(t);
                }
            }

            foreach (Type oneClass in allClass)
            {
                foreach (MethodInfo method in oneClass.GetMethods())
                {
                    if (method.Name == "GetXMLAction")
                    {
                        System.Reflection.Assembly assembly = oneClass.Assembly;
                        GenericAction tempAction = (GenericAction)assembly.CreateInstance(oneClass.FullName);

                        string xmlAction = (String)method.Invoke(tempAction, null);
                        Dictionary<string, string> properties = this.GetPropertiesFromXml(xmlAction);

                        foreach (KeyValuePair<string, string> pair in properties)
                        {
                            bool found = false;
                            foreach (PropertyInfo property in oneClass.GetProperties())
                            {
                                if (property.Name == pair.Key)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            Assert.IsTrue(found, "L'élément XML '" + pair.Key + "' n'a pas été matché avec une des propriétés de la classe " + oneClass.Name + "'");
                        }
                        break;
                    }
                }
            }
        }

        private Dictionary<string, string> GetPropertiesFromXml(string xmlAction)
        {
            Dictionary<string, string> properties = new Dictionary<string, string>();

            XmlReader reader = XmlReader.Create(new System.IO.StringReader(xmlAction));
            reader.ReadToFollowing("ElementType");
            string elementType = reader.ReadString();
            reader.ReadEndElement();

            while (reader.MoveToContent() == XmlNodeType.Element)
            {
                string propertyName = reader.Name;

                properties.Add(propertyName, reader.ReadElementContentAsString());
            }

            return properties;
        }

    }
}
