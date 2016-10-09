using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adresboek
{
    public class Contact
    {
        public string name;
        public string email;
        public Contact(string n, string e)
        {
            name = n;
            email = e;
        }

        // check if the name and email of this user are valid
        public bool Validate()
        {
            return Validation.ValidateEmail(this.email) && Validation.ValidateName(this.name);
        }


        // check the equality of two contact (by checking name and email)
        static public bool EqualContatcs(Contact c1, Contact c2)
        {
            return c1.name == c2.name && c1.email == c2.email;
        }

    }
}
