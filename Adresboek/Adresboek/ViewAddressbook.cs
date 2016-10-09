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
        // GUI elements
        public Label adressbookname, contacts;
        public List<Label> contactnames, emailadresses;
        public List<Button> deletebuttons;
        public Button addContact, searchButton, searchHideButton;
        public TextBox search, contactname, emailadress;
        public ComboBox copyAllCombo;


        // load (setup) this view
        private void loadViewAddressbook()
        {
            LogManager.Write("loadViewAddressbook");
            changeToAddressbook(adressbooks[current]);
            LogManager.Write("loadViewAddressbook_callback1");
        }

        // unload this view
        private void unloadViewAddressbook()
        {
            LogManager.Write("unloadViewAddressbook");

            foreach (Label l in contactnames)
                l.Hide();
            foreach (Label l in emailadresses)
                l.Hide();
            foreach (Button b in deletebuttons)
                b.Hide();
            copyAllCombo.Hide();
            addContact.Hide();
            contacts.Hide();
            contactname.Hide();
            emailadress.Hide();
            contactnames.Clear();
            emailadresses.Clear();
            deletebuttons.Clear();
            adressbookname.Hide();
            searchButton.Hide();
            search.Hide();
        }


        // reload contacts list
        public void changeToAddressbook(Adressbook a)
        {
            LogManager.Write("j");

            // unload previous view
            unloadViewAddressbook();

            LogManager.Write("k");

            // when there is a search keyword filled in
            // refreshing the contacts list will only render the search results
            if (search.Text.Length > 0)
            {
                LogManager.Write("l");
                updateSearchbook();
                LogManager.Write("m");
                a = searchbook;
            }

            LogManager.Write("n");

            searchButton.Show();
            search.Show();
            adressbookname.Show();
            addContact.Show();
            contacts.Show();
            contactname.Show();
            emailadress.Show();

            adressbookname.Text = "Adressbook Name: " + a.name;

            // change the visible addressbook
            makeForm(a);

            LogManager.Write("o");

            this.Invalidate();
            this.Refresh();

            // Check if all contacts in a adressbook are displayed when switching views 
            // Oracle or code seems to have errors...
            bool b = false;
            for (int j = 0; j < contactnames.Count; j++)
            {
                b = adressbooks[current].Contacts[j].name == contactnames[j].Text;
                if (b == false)
                    break;
            }
            LogManager.Oracle(": All contacts from the current adressboek have labels", b);
        }


        // redraw list of contacts from the given addressbook
        private void makeForm(Adressbook a)
        {
            LogManager.Write("p");
            int y = 108;

            // show all contacts
            for (int i = 0; i < a.contacts.Count; i++)
            {
                LogManager.Write("q");

                Contact contact = a.contacts[i];

                contactnames.Add(new Label());
                contactnames[i].Location = new Point(45, y);
                contactnames[i].Text = a.contacts[i].name;
                this.Controls.Add(contactnames[i]);

                emailadresses.Add(new Label());
                emailadresses[i].Location = new Point(220, y);
                emailadresses[i].Text = a.contacts[i].email;
                emailadresses[i].Size = new Size(170, 15);
                this.Controls.Add(emailadresses[i]);

                deletebuttons.Add(new Button());
                deletebuttons[i].Location = new Point(400, y);
                deletebuttons[i].Name = "Delete Contact " + i;
                deletebuttons[i].Text = "Delete Contact";
                deletebuttons[i].Click += delegate(Object o, EventArgs e)
                {
                    delete_Click(o, e, contact);
                };
                this.Controls.Add(deletebuttons[i]);
                y += 22;
            }

            LogManager.Write("r");

            // button to remove all visible contacts
            Button removeAllBtn = new Button();
            removeAllBtn.Location = new Point(45, y + 22);
            removeAllBtn.Text = "Remove all";
            removeAllBtn.Click += removeAll_Click;
            this.Controls.Add(removeAllBtn);
            deletebuttons.Add(removeAllBtn);

            // button to copy all contacts to the selected addressbook
            Button copyAllBtn = new Button();
            copyAllBtn.Location = new Point(125, y + 22);
            copyAllBtn.Text = "Copy all to:";
            copyAllBtn.Click += copyAll_Click;
            this.Controls.Add(copyAllBtn);
            deletebuttons.Add(copyAllBtn);

            // select a addressbook to copy contact to
            copyAllCombo.Show();
            copyAllCombo.Items.Clear();
            copyAllCombo.Location = new Point(200, y + 23);
            foreach (Adressbook adb in adressbooks)
            {
                LogManager.Write("s");
                Adressbook currentadb = getCurrentAddressbook();
                LogManager.Write("t");
                if (adb != currentadb)
                {
                    LogManager.Write("u");
                    copyAllCombo.Items.Add(adb.name);
                }
                LogManager.Write("v");
            }

            LogManager.Write("w");
        }




        // user pressed "create contact" button
        private void addContact_Click(object sender, EventArgs e)
        {
            LogManager.Write("1");

            // creating new contact
            Contact contact = new Contact(contactname.Text, emailadress.Text);

            LogManager.Write("2");

            // check if the email and name values are correct
            if (contact.Validate())
            {
                LogManager.Write("3");

                if (!adressbooks[current].CheckNoDuplicates(contact))
                {
                    LogManager.Write("4");
                    adressbooks[current].Add(contact);
                } 
                else 
                {
                    LogManager.Write("5");
                    MessageBox.Show("Contact already exists");
                }

                LogManager.Write("6");

                changeToAddressbook(adressbooks[current]);

                LogManager.Write("7");
            }
            else
            {
                LogManager.Write("8");
                MessageBox.Show("Contact is not correct");
            }

            LogManager.Write("9");

            // Check if all contacts in the adressbook also are displayed on a label
            bool b = true;
            foreach (Contact c in adressbooks[current].contacts)
            {
                if (contactnames.Any(i => i.Text == c.name) == false)
                {
                    b = false; break;
                }
            }
            LogManager.Oracle(": All contacts in adressbooks[current] also in Labels", b);
        }

        // delete a contact
        public void delete_Click(object sender, EventArgs e, Contact c)
        {
            LogManager.Write("BEGIN", true);
           
            LogManager.Write("a", true);
            //delete contact
            adressbooks[current].Delete(c); 
            LogManager.Write("b", true);

            bool inAnyAddressbooks = false;
            foreach (Adressbook a in adressbooks)
            {
                LogManager.Write("f1", true);
                foreach (Contact con in a.contacts)
                {
                    LogManager.Write("g1", true);
                    if (Contact.EqualContatcs(con, c))
                    {
                        LogManager.Write("h", true);
                        //contact exists also in an other addressbook
                        inAnyAddressbooks = true; 
                    }
                    LogManager.Write("g2", true);
                }
                LogManager.Write("f2", true);
            }
            LogManager.Write("c", true);

            // delete contact from the standard addressbook if
            // the contact isn't in any of the addressbooks.
            if (!inAnyAddressbooks)
            {
                LogManager.Write("i", true);
                standard.contacts.Remove(c);
            }
            LogManager.Write("d", true);
        


            changeToAddressbook(adressbooks[current]);
            this.Invalidate();
            this.Refresh();

            LogManager.Write("END", true);

            // Test if a deleted contact in a adressbook is not displayed anymore
            bool b = true;
            foreach (Contact d in adressbooks[current].contacts)
            {
                if (contactnames.Any(i => i.Text == d.name) == false)
                {
                    b = false; break;
                }
            }
            LogManager.Oracle(": Deleted contact not in Label anymore", b);
        }

        // remove all visible contact button is pressed
        private void removeAll_Click(object sender, EventArgs e)
        {
            LogManager.Write("removeAllClicked");
            removeAll();
            LogManager.Write("removeAllClicked_callback1");
            changeToAddressbook(getCurrentAddressbook());
            LogManager.Write("removeAllClicked_callback2");
        }

        // copy all contacts button is pressed
        private void copyAll_Click(object sender, EventArgs e)
        {
            LogManager.Write("copyAllClicked");
            copyAll();
            LogManager.Write("copyAllClicked_callback1");
        }

        // the search button is pressed
        private void search_Click(object sender, EventArgs e)
        {
            LogManager.Write("searchClicked");

            if (search.Text.Length > 0)
            {
                LogManager.Write("search_show");

                // user have entered a search string
                searchHideButton.Show();
            }
            else
            {
                LogManager.Write("search_hide");

                // use searched with a empty string, stop search-mode
                searchHideButton.Hide();
            }

            LogManager.Write("search_callback1");

            updateSearchbook(); // update searchboox with contacts that match the search

            LogManager.Write("search_callback2");
            
            changeToAddressbook(searchbook); // redraw content

            LogManager.Write("search_callback3");

            // Check if all searched contacts are made visible
            // TODO: also check if other contacts are NOT visible
            bool b = false;
            foreach (Label l in contactnames)
            {
                b = searchbook.contacts.Any(i => i.name == l.Text);
                if (b == false)
                    break;
            }
            LogManager.Oracle(": Search contacts are all visible", b);
        }


        /////////////
        // IMPLEMENTATION
        ////////////

        // remove from the current addressbook all the contacts that are currently on screen
        private void removeAll()
        {
            LogManager.Write("removeAll");
            List<Contact> contacts = getCurrentVisibleAddressbook().Contacts;
            LogManager.Write("removeAll_callback1");
            getCurrentAddressbook().Delete(contacts);
            LogManager.Write("removeAll_callback2");

            // Check if contacts are also removed from the adressbooks list when removeAll is used
            bool b = true;
            foreach (Contact c in contacts)
            {
                b = adressbooks[current].contacts.Contains(c);
                if (b == false)
                    break;
            } 
            LogManager.Oracle(": All contacts removed from current adressbook truly removed", b);

            // Check if contacts which should be removed are not displayed anymore
            b = false;
            foreach (Label l in contactnames)
            {
                b = contacts.Any(i => i.name == l.Text);
                if (b == true)
                    break;
            }
            LogManager.Oracle(": All contacts removed from current adressbook not visible", !b);
        }

        // copy all contacts that are currently on screen
        // to the addressbook specified in the combobox
        private void copyAll()
        {
            LogManager.Write("copyAll");

            List<Contact> contacts = getCurrentVisibleAddressbook().Contacts;
            string adb_name = copyAllCombo.Text;
            if (addressbookExists(adb_name))
            {
                LogManager.Write("copyAll_exist");
                Adressbook ad = getAddressbook(adb_name);
                LogManager.Write("copyAll_callback1");
                foreach (Contact contact in contacts)
                {
                    LogManager.Write("copyAll_add");
                    ad.Add(contact);
                }

                // Check if copy contact to another adressbook works
                bool b = false;
                foreach (Contact contact in contacts)
                {
                    b = adressbooks[adressbooks.IndexOf(ad)].contacts.Contains(contact);
                    if (b == false)
                        break;
                }
                LogManager.Oracle(": Copy to adressbook", b);
            }
            
            LogManager.Write("copyAll_callback2");

            
            
        }

        // user stopped the search-mode by pressing the clear-button
        private void searchHide_Click(object sender, EventArgs e)
        {
            LogManager.Write("searchHideClicked");
            search.Text = "";
            updateSearchbook();
            LogManager.Write("searchHideClicked_callback1");
            changeToAddressbook(searchbook);
            LogManager.Write("searchHideClicked_callback2");
            searchHideButton.Hide();
        }

        // update searchbook with contacts that statisfy the search-string
        private void updateSearchbook()
        {
            LogManager.Write("updateSearchbook");
            searchbook.Contacts = adressbooks[current].Search(search.Text);
            LogManager.Write("updateSearchbook_callback1");
        }

        // get the addressbook from the visible contacts
        // when user performs a search, the addressbook will be the searchbook
        private Adressbook getCurrentVisibleAddressbook()
        {
            LogManager.Write("getCurrentVisibleAddressbook");

            Adressbook book;

            // when there is a search keyword filled in return the searchbook
            if (search.Text.Length > 0)
            {
                LogManager.Write("getCurrentVisibleAddressbook_searchbook");
                book = searchbook;
            }
            else
            {
                LogManager.Write("getCurrentVisibleAddressbook_current");
                // otherwise return the current addressbook
                book =  getCurrentAddressbook();
            }

            LogManager.Write("getCurrentVisibleAddressbook_callback1");

            return book;
        }

        // get the current addressbook that the user is viewing / editing
        private Adressbook getCurrentAddressbook()
        {
            LogManager.Write("getCurrentAddressbook");
            return adressbooks[current];
        }
    }
}
