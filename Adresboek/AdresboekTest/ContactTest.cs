using Adresboek;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdresboekTest
{
   
    [TestClass()]
    public class ContactTest
    {

        // test the class Contact
        // are the email and name variables correctly setted

        [TestMethod()]
        public void ContactConstructorTest()
        {
            string[] names = makeContacts();
            string[] emails = makeEmails();
            Contact[] contacts = new Contact[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                string n = names[i]; 
                string e = emails[i]; 
                contacts[i] = new Contact(n, e);
            }
            for (int j = 0; j < names.Length; j++)
            {
                Assert.AreEqual(names[j], contacts[j].name);
                Assert.AreEqual(emails[j], contacts[j].email);
            }
            
        }


        // helpers functions

        public String[] makeContacts()
        {
            string[] contacts = { "Jan", "Piet Pieterse", "Henk", "Gerda", "Pietje Puk Pukerse" };
            return contacts;
        }

        public String[] makeEmails()
        {
            string[] emails = {"jan@jansen.nl", "piet@pieterse.nl", "henk@henkse.nl", "gerda@geerstse.nl", "pukie.nl"};
            return emails;
        }
    }
}
