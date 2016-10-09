using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Adresboek
{
    partial class Viewer2 : Form
    {
        public TextBox abn;
        public Button addAdressbook;

        // load (setup) this view
        private void loadViewAddAddressbook()
        {
            adressbookname.Show();
            abn.Show();
            addAdressbook.Show();

            // Check if a textbox is empty when switching to this view
            bool b = abn.Text == "";
            LogManager.Oracle(": Box is empty at start", b);

        }

        // unload this view
        private void unloadViewAddAddressbook()
        {
            adressbookname.Hide();
            abn.Hide();
            addAdressbook.Hide();
        }


        // creating a new addressbook when user pressed the "add" button
        private void addAdressbook_Click(object sender, EventArgs e)
        {
            Adressbook a = new Adressbook(abn.Text);
            if (a.Validate(abn.Text))
            {
                // name of addressbook is valid, create it!
                adressbooks.Add(a);
                current = adressbooks.IndexOf(a);
                changeViewTo("addressbook");
                bool b = (adressbookname.Text == "Adressbook Name: " + abn.Text);
                LogManager.Oracle(": Add adressbook the same name as Label", b);

            }
            else
            {
                MessageBox.Show("Adressbook name too long"); //no valid name
                abn.Text = "";
            }

        }

    }
}
