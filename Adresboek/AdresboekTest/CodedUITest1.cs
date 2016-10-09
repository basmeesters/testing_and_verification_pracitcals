using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace AdresboekTest
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITest1
    {
        ApplicationUnderTest test;
        public CodedUITest1()
        {
        }
        [TestInitialize()]
        public void MyTestInitialize()
        {
            string basApp = "C:\\Users\\Bas\\Desktop\\Softwaretesting\\edu.3725154.SoftwareTesting\\Adresboek\\Adresboek\\bin\\Debug\\Adresboek.exe";
            string jarnoApp = "C:\\Users\\Jarno\\Documents\\Visual Studio 2010\\Projects\\stv_adresboek\\Adresboek\\Adresboek\\bin\\Debug\\Adresboek.exe";
            try {
                test = ApplicationUnderTest.Launch(basApp);
            }
            catch(Exception e) {
                try {
                    test = ApplicationUnderTest.Launch(jarnoApp);
                } catch(Exception e2) {
                    throw new Exception("Can't start the executable");
                }
            }
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {

            this.UIMap.RecordedMethod3();

            //this.UIMap.RecordedMethod1();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
            // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        }

        [TestMethod]
        public void TestMethod2()
        {

            this.UIMap.RecordedMethod2();

        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            test.Close();
        }
        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //    // For more information on generated code, see http://go.microsoft.com/fwlink/?LinkId=179463
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
