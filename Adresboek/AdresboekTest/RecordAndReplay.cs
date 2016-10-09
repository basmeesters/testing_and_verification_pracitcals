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
    /// The Record and Replay functionality is in this class. It uses the automatic generated code 
    /// from doing interactions yourself. It uses very short interactions which only test one element at
    /// a time. The methods are serpated in what they test. So, for example TestName tests all different
    /// code paths of inserting and adding a contact name.
    /// </summary>
    [CodedUITest]
    public class RecordAndReplay
    {
        ApplicationUnderTest test;
        public RecordAndReplay()
        {
            test = new ApplicationUnderTest();
        }

        [TestInitialize()]
        public void MyTestInitialize()
        {
            // Strings used to launch the application needed for the test
            string basApp = "C:\\Users\\Bas\\Desktop\\Softwaretesting\\edu.3725154.SoftwareTesting\\Adresboek\\Adresboek\\bin\\Debug\\Adresboek.exe";
            string jarnoApp = "C:\\Users\\Jarno\\Documents\\Visual Studio 2010\\Projects\\stv_adresboek\\Adresboek\\Adresboek\\bin\\Debug\\Adresboek.exe";
            try
            {
                test = ApplicationUnderTest.Launch(basApp);
            }
            catch
            {
                try
                {
                    test = ApplicationUnderTest.Launch(jarnoApp);
                }
                catch
                {
                    string last = "C:\\Users\\Bas\\Desktop\\Softwaretesting\\Adresboek\\Adresboek\\bin\\Debug\\Adresboek.exe";
                    test = ApplicationUnderTest.Launch(last);
                }
            }
        }


        [TestMethod]
        public void TestName()
        {
            // Test Name methods
            this.UIMap.ViewAdressbook1();
            this.UIMap.AddEmpty();
            this.UIMap.AddOnlyName();
            this.UIMap.AddValid();
            this.UIMap.AddOnlyEmail();
            this.UIMap.AddNonValidName();
            this.UIMap.DoubleDotsName();
            this.UIMap.SingleDotName();
            this.UIMap.CreateMultipleContacts();
            this.UIMap.DoubleContact();
        }

        [TestMethod]
        public void TestEmail()
        {
            // Test Email methods
            // Since not all email code can be covered in the application the 100% code coverage is achieved
            // with the help of the unit tests
            // Here are only a few of the paths covered
            this.UIMap.ViewAdressbook1();
            this.UIMap.AddNonValidEmail();
            this.UIMap.AddValidEmail();
            this.UIMap.AddEmptyDomain();
            this.UIMap.EmptyQuotes();
            this.UIMap.DotFirst();
            this.UIMap.DotLast();
            this.UIMap.SingleQuote();
            this.UIMap.EmptyDomain();

            this.UIMap.DoubleDotsInDomain();
            this.UIMap.SpecialChactersInDomain();
            this.UIMap.AddEmptyQuotes();
            this.UIMap.QuotesFirst();
        }

        [TestMethod]
        public void TestAdressbook()
        {

            // Test add / delete of contacts
            this.UIMap.ViewAdressbook1();
            this.UIMap.AddContact();
            this.UIMap.DeleteContact();


            // Test search
            this.UIMap.SearchClear();
            this.UIMap.SearchCopyClear();
            this.UIMap.SearchDeleteClear();
            this.UIMap.SearchEmpty();


        }

        [TestMethod]
        public void TestRemove()
        {
            this.UIMap.ViewAdressbook1();
            this.UIMap.RemoveAll();
        }

        [TestMethod]
        public void TestAddAdressbook()
        {
            // Test creating new adressbooks
            this.UIMap.GotoAddAdressbook();
            this.UIMap.CreateNonValidAdressbook();
            this.UIMap.GotoAddAdressbook();
            this.UIMap.CreateValidAdressbook();
            this.UIMap.GotoAddAdressbook();
            this.UIMap.CreateEmpty();
        }

        [TestMethod]
        public void TestSwitchingViews()
        {
            // Test switching views
            this.UIMap.ViewAdressbook1();
            this.UIMap.GotoAddAdressbook();
            this.UIMap.GotoViewAdressbooks();
        }

        //[TestMethod]
        //public void TestRemovingContactFromMultipleAddressbooks()
        //{
        //    // Delete contact
        //    this.UIMap.DeleteContactFromMultipleAddressbooks();
        //}




       
        #region Additional test attributes



        [TestCleanup()]
        public void MyTestCleanup()
        {
            test.Close();
        }

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
