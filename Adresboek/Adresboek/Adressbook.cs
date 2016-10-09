using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adresboek
{
    public class Adressbook
    {
        // this addressbook holds a list with contacts
        public List<Contact> contacts = new List<Contact>();

        // name of the addressbook
        public string name;

 
        public Adressbook(string n)
        {
            name = n;
        }


        // check if the name of the addressbook is valid.
        public bool Validate(string n)
        {
            if (n.Length <= 20)
                return true;
            else
                return false;
        }

        // add a contact to this addressbook
        public void Add(Contact c)
        {
            contacts.Add(c);
        }

        // delete a contact from this addressbook
        public void Delete(Contact c)
        {
            if (contacts.Contains(c))
                contacts.Remove(c);
        }

        // delete multiple contacts from this addressbook
        public void Delete(List<Contact> cc)
        {
            contacts.RemoveAll(c => cc.Contains(c));
        }


        // search all contacts in this addressbook on the given string
        // return all contacts where the email or name contains the string 
        public List<Contact> Search(string s)
        {
            return contacts.FindAll(c => c.name.Contains(s) || c.email.Contains(s));
        }

        // getter/setter for property contacts
        public List<Contact> Contacts
        {
            get { return this.contacts; }
            set { this.contacts = value; }
        }

        public bool CheckNoDuplicates(Contact c)
        {
            if (this.contacts.Any(n => n.email == c.email && n.name == c.name))
                    return true;
                else
                    return false;
        }

     
    }
}
