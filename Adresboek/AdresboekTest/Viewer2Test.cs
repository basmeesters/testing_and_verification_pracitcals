using Adresboek;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AdresboekTest
{
    
    [TestClass()]
    public class Viewer2Test
    {
        // remove from the active addressbook all the contacts that are currently on screen (search result)
        [TestMethod()]
        public void removeAllTest()
        {
            Viewer2_Accessor view = new Viewer2_Accessor();
            Assert.IsTrue(view.getCurrentAddressbook().Contacts.Count > 0);
            view.removeAll();
            Assert.IsTrue(view.getCurrentAddressbook().Contacts.Count == 0);
        }

        // copy the contacts that are currently on screen (search result) to an other addressbook specified in the combobox
        [TestMethod()]
        public void copyAll_ClickTest()
        {
            Viewer2_Accessor view = new Viewer2_Accessor();
            List<Contact> contacts = view.getCurrentAddressbook().Contacts;
            view.copyAllCombo.Text = "sample2";
            view.copyAll();
            foreach (Contact c in contacts)
                Assert.IsTrue(view.getAddressbook("sample2").Contacts.Contains(c));
        }


        // delete click unit test
        [TestMethod()]
        public void delete_click()
        {
            Viewer2_Accessor view = new Viewer2_Accessor();
            EventArgs e = new EventArgs();
            object o = new object();
            Contact c = new Contact("contactName", "contactName@gmail.com");
            view.delete_Click(o, e, c);
        }




        // PRIME PATH COVERAGE TESTING
        // of method: delete_Click()
        [TestMethod()]
        public void PrimePathTest()
        {
            Viewer2_Accessor view = new Viewer2_Accessor();

            // test all prime paths that are feasable
            path_4(view);
            path_7(view);
            path_9_12(view);
            path_10_11(view);
            path_10_11(view);
            path_13(view);
            path_15(view);
            path_16(view);
            path_17_18(view);
            path_19(view);
            path_20(view);
            path_21(view);
            path_22(view);
            path_23(view);
            path_24(view);

            // infeasible prime paths are: 1,2,3,5,6,8
            // where all can be toured with detours except primePath-8
        }


        // prime path 4: [b, f1, f2, c, i, d]
        private void path_4(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");

            removeContact(view, a1, c1);

            Assert.AreEqual(a1.Contacts.Count, 0);
        }

        // prime path 7: [b, f1, g1, g2, f2, c, i, d]
        private void path_7(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Contact c2 = addContact(a1, "c2");

            removeContact(view, a1, c2);

            Assert.AreEqual(a1.Contacts.Count, 1);
        }

        // prime path 9: [f1, f2, f1]
        // prime path 12: [f2, f1, f2]
        private void path_9_12(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Adressbook a2 = addAddressbook(view, "adb2");
            
            removeContact(view, a1, c1);

            Assert.AreEqual(a1.Contacts.Count, 0);
            Assert.AreEqual(a2.Contacts.Count, 0);
        }


        // prime path 10: [g1, g2, g1]
        // prime path 11: [g2, g1, g2]
        private void path_10_11(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Contact c2 = addContact(a1, "c2");
            Contact c3 = addContact(a1, "c3");

            removeContact(view, a1, c3);

            Assert.AreEqual(a1.Contacts.Count, 2);
        }

        // prime path 13: [g1, h, g2, g1]
        private void path_13(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Contact c2 = addContact(a1, "c2");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c1b = addContact(a2, "c1");

            removeContact(view, a2, c1b);

            Assert.AreEqual(a1.Contacts.Count, 2);
            Assert.AreEqual(a2.Contacts.Count, 0);
        }

        // prime path 15: [g2, g1, h, g2]
        private void path_15(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Contact c2 = addContact(a1, "c2");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c2b = addContact(a2, "c2");

            removeContact(view, a2, c2b);

            Assert.AreEqual(a1.Contacts.Count, 2);
            Assert.AreEqual(a2.Contacts.Count, 0);
        }

        // prime path 16: [f1, g1, g2, f2, f1]
        private void path_16(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c2 = addContact(a2, "c2");

            removeContact(view, a2, c2);

            Assert.AreEqual(a1.Contacts.Count, 1);
            Assert.AreEqual(a2.Contacts.Count, 0);
        }

        // prime path 17: [g1, g2, f2, f1, g1]
        // prime path 18: [g2, f2, f1, g1, g2]
        private void path_17_18(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c2 = addContact(a2, "c2");
            Contact c3 = addContact(a2, "c3");

            removeContact(view, a2, c3);

            Assert.AreEqual(a1.Contacts.Count, 1);
            Assert.AreEqual(a2.Contacts.Count, 1);
        }

        // prime path 19: [f2, f1, g1, g2, f2]
        private void path_19(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c1 = addContact(a2, "c1");
            Contact c2 = addContact(a2, "c2");

            removeContact(view, a2, c2);

            Assert.AreEqual(a1.Contacts.Count, 0);
            Assert.AreEqual(a2.Contacts.Count, 1);
        }

        // prime path 20: [f1, g1, h, g2, f2, f1]
        private void path_20(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c1b = addContact(a2, "c1");

            removeContact(view, a2, c1b);

            Assert.AreEqual(a1.Contacts.Count, 1);
            Assert.AreEqual(a2.Contacts.Count, 0);
        }

        // prime path 21: [g1, h, g2, f2, f1, g1]
        private void path_21(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c1b = addContact(a2, "c1");
            Contact c2 = addContact(a2, "c2");

            removeContact(view, a2, c1b);

            Assert.AreEqual(a1.Contacts.Count, 1);
            Assert.AreEqual(a2.Contacts.Count, 1);
        }

        // prime path 22: [h, g2, f2, f1, g1, h]
        private void path_22(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c1b = addContact(a2, "c1");
            Adressbook a3 = addAddressbook(view, "adb3");
            Contact c1c = addContact(a3, "c1");

            removeContact(view, a3, c1c);

            Assert.AreEqual(a1.Contacts.Count, 1);
            Assert.AreEqual(a2.Contacts.Count, 1);
            Assert.AreEqual(a3.Contacts.Count, 0);
        }

        // prime path 23: [g2, f2, f1, g1, h, g2]
        private void path_23(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Contact c2 = addContact(a1, "c2");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c2b = addContact(a2, "c2");
            

            removeContact(view, a1, c2);

            Assert.AreEqual(a1.Contacts.Count, 1);
            Assert.AreEqual(a2.Contacts.Count, 1);
        }

        // prime path 24: [f2, f1, g1, h, g2, f2]
        private void path_24(Viewer2_Accessor view)
        {
            clear(view);

            Adressbook a1 = addAddressbook(view, "adb1");
            Contact c1 = addContact(a1, "c1");
            Adressbook a2 = addAddressbook(view, "adb2");
            Contact c1b = addContact(a2, "c1");

            removeContact(view, a1, c1);

            Assert.AreEqual(a1.Contacts.Count, 0);
            Assert.AreEqual(a2.Contacts.Count, 1);
        }
 






        // helper functions for Prime Path Tests

        private Adressbook addAddressbook(Viewer2_Accessor view, string name)
        {
            EventArgs e = new EventArgs();
            object o = new object();
            view.abn.Text = name;
            view.addAdressbook_Click(o, e);
            return view.adressbooks[view.adressbooks.Count - 1];
        }

        private Contact addContact(Adressbook a, string name)
        {
            Contact c = new Contact(name, name + "@gmail.com");
            a.Add(c);
            return c;
        }

        private void removeContact(Viewer2_Accessor view, Adressbook a, Contact contact)
        {
            changeAddressbook(view, a);
            EventArgs e = new EventArgs();
            object o = new object();
            view.delete_Click(o, e, contact);
        }

        private void changeAddressbook(Viewer2_Accessor view, Adressbook a)
        {
            for (int i = 0; i < view.adressbooks.Count; i++)
                if (view.adressbooks[i] == a)
                    view.current = i;
            view.changeToAddressbook(a);
        }

        private void clear(Viewer2_Accessor view)
        {
            view.adressbooks.Clear();
        }
    }
}
