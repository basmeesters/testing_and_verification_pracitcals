using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adresboek;

namespace AdresboekTest
{
    [TestClass()]
    public class AdresboekTest
    {
        Adressbook adb;
        Contact c1;
        Contact c2;
        Contact c3;
        Contact c4;

        // initialize a addressbook with sample contacts
        [TestInitialize()]
        public void AddressbookInitialize()
        {
            adb = new Adressbook("testbook");
            c1 = new Contact("Jarno", "jarnoleconte@gmail.com");
            c2 = new Contact("Bas", "bas_meesters@hotmail.com");
            c3 = new Contact("C.A.R.Hoare", "C.A.R.Hoare@hotmail.com");
            c4 = new Contact("name4", "email4");
            adb.Add(c1);
            adb.Add(c2);
            adb.Add(c3);
            adb.Add(c4);
        }


        // testing adding a contact to the addressbook
        public Adressbook testbook = new Adressbook("test");
        [TestMethod()]
        public void AddTest()
        {
            string n = "test";
            Adressbook target = new Adressbook(n);
            Contact[] contacts = { new Contact("name1", "email1"), new Contact("name2", "email2"), new Contact("name3", "email3"), new Contact("name4", "email4") }; 
            foreach (Contact c in contacts)
                target.Add(c);
            for (int i = 0; i < contacts.Length; i++)
                Assert.AreEqual(contacts[i], target.contacts[i]);
        }

        // testing deleting a contact from the addressbook
        [TestMethod()]
        public void DeleteTest()
        {
            string n = "test"; 
            Adressbook target = new Adressbook(n);
            Contact[] contacts = { new Contact("name1", "email1"), new Contact("name2", "email2"), new Contact("name3", "email3"), new Contact("name4", "email4") };
            for (int i = 0; i < contacts.Length; i++)
                target.Add(contacts[i]);
            for (int i = 0; i < contacts.Length; i++)
            {
                target.Delete(contacts[i]);
                Assert.IsTrue(target.contacts.Count == contacts.Length - i -1);
            }
           
            // Check if adressbook crashes when an non existend contact is trying to be deleted
            Contact nonexistend = new Contact("name", "email");
            try
            {
                target.Delete(nonexistend);
                Assert.IsTrue(true);
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        // testing deleting multiple contact at once
        [TestMethod()]
        public void DeleteMultipleContactsTest()
        {
            List<Contact> contacts = new List<Contact> { c1, c2 };
            List<Contact> expected = new List<Contact> { c3, c4 };
            adb.Delete(contacts);
            CollectionAssert.AreEqual(expected, adb.Contacts);
        }

        // check if the name of the addressbook is in valid format
        [TestMethod()]
        public void ValidateTest()
        {
            string[] truetests = { "sample1", "sample2", "test", "this is correct" };
            string[] falsetests = { "a name which is too long", "another one which should fail" };
            foreach (string s in truetests)
                Assert.IsTrue(testbook.Validate(s));
            foreach (string s in falsetests)
                Assert.IsFalse(testbook.Validate(s));
        }

        // check if a given search string gives the correct search results
        [TestMethod()]
        public void SearchTest()
        {
            // empty search
            string search_value = "";
            List<Contact> expected = new List<Contact> { c1, c2, c3, c4 };
            List<Contact> actual = adb.Search(search_value);
            CollectionAssert.AreEqual(expected, actual);

            // name search
            search_value = "Jarn";
            expected = new List<Contact> { c1 };
            actual = adb.Search(search_value);
            CollectionAssert.AreEqual(expected, actual);

            // email search
            search_value = "meesters@";
            expected = new List<Contact> { c2 };
            actual = adb.Search(search_value);
            CollectionAssert.AreEqual(expected, actual);

            // search multiple contacts
            search_value = "@hotmail";
            expected = new List<Contact> { c2, c3 };
            actual = adb.Search(search_value);
            CollectionAssert.AreEqual(expected, actual);
        }



        
    }
}
