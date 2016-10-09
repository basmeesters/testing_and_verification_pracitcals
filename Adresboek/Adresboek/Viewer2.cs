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
        public List<Adressbook> adressbooks = new List<Adressbook>(); // hold addressbooks
        public Adressbook standard = new Adressbook("standard"); // all contacts
        public Adressbook searchbook = new Adressbook("searchbook"); // result of search

        public int current = 0; // id of the addressbook that is currently on screen
      
        public Viewer2()
        {
            this.Text = "Adressbook manager";
            this.BackColor = Color.White;
            this.ClientSize = new Size(600, 600);
            this.AutoScroll = true;

            initMenu();
            makeControls();
            Sample();

            Init();
            test();
        }

        // create a menubar on top of the application
        private void initMenu()
        {
            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem menu = new ToolStripMenuItem("Adressbooks");
            menu.DropDownItems.Add("Add Adressbook", null, this.addAddressbookform_Click);
            menu.DropDownItems.Add("View Adressbooks", null, this.viewAdressbooks_Click);
            menuStrip.Items.Add(menu);

            this.Controls.Add(menuStrip);
        }

        // creating all of the GUI controls
        private void makeControls()
        {
            adressbookname = new Label();
            adressbookname.Location = new Point(20, 30);
            adressbookname.Text = "Adressbook Name:";
            adressbookname.AutoSize = true;
            this.Controls.Add(adressbookname);

            search = new TextBox();
            search.Location = new Point(400, 30);
            this.Controls.Add(search);

            searchButton = new Button();
            searchButton.Location = new Point(500, 28);
            searchButton.Text = "Search";
            searchButton.Click += search_Click;
            this.Controls.Add(searchButton);
            
            searchHideButton = new Button();
            searchHideButton.Location = new Point(360, 28);
            searchHideButton.Text = "clear";
            searchHideButton.Width = 40;
            searchHideButton.Click += searchHide_Click;
            searchHideButton.Hide();
            this.Controls.Add(searchHideButton);

            abn = new TextBox();
            abn.Location = new Point(20, 55);
            this.Controls.Add(abn);

            addAdressbook = new Button();
            addAdressbook.Location = new Point(20, 80);
            addAdressbook.Text = "Create Adressbook";
            addAdressbook.Click += addAdressbook_Click;
            this.Controls.Add(addAdressbook);

            addContact = new Button();
            addContact.Location = new Point(220, 55);
            addContact.Text = "Add Contact";
            addContact.Click += addContact_Click;
            this.Controls.Add(addContact);

            contactname = new TextBox();
            contactname.Location = new Point(20, 55);
            this.Controls.Add(contactname);

            emailadress = new TextBox();
            emailadress.Location = new Point(120, 55);
            this.Controls.Add(emailadress);

            contacts = new Label();
            contacts.Location = new Point(30, 85);
            contacts.Text = "Contacts:";
            this.Controls.Add(contacts);

            contactnames = new List<Label>();
            emailadresses = new List<Label>();
            deletebuttons = new List<Button>();

            copyAllCombo = new ComboBox();
            this.Controls.Add(copyAllCombo);
        }

        // initialize state of controls Show/Hidden
        private void Init()
        {
            // hide all controls by unloading all views first
            unloadView("addressbook");
            unloadView("selectAddressbook");
            unloadView("addAddressbook");

            // start with the "select addressbook" view
            changeViewTo("selectAddressbook");
        }

       


        /////////
        // VIEWS 
        /////////


        // switch to view where the user can add a new addressbook
        private void addAddressbookform_Click(object sender, EventArgs e)
        {
            changeViewTo("addAddressbook");     
        }

        // switch to the view where the user can pick one of the addressbook to show
        private void viewAdressbooks_Click(object sender, EventArgs e)
        {
            changeViewTo("selectAddressbook");
        }



        // view that is currently on screen
        private string currentView = null;

        // change the view
        public void changeViewTo(string view)
        {
            if (currentView != null)
                unloadView(currentView);
            currentView = view;
            loadView(view);
        }

        // unload previous view
        private void unloadView(string view)
        {
            switch (view)
            {
                case "selectAddressbook":
                    unloadViewSelectAddressbook();
                    break;
                case "addAddressbook":
                    unloadViewAddAddressbook();
                    break;
                case "addressbook":
                    unloadViewAddressbook();
                    break;
            }
        }

        // load new view
        private void loadView(string view)
        {
            switch (view)
            {
                case "selectAddressbook":
                    loadViewSelectAddressbook();
                    break;
                case "addAddressbook":
                    loadViewAddAddressbook();
                    break;
                case "addressbook":
                    loadViewAddressbook();
                    break;
            }
        }



        //////////
        // HELPERS
        /////////

        // get the addressbook with the given name
        private Adressbook getAddressbook(string name)
        {
            return adressbooks.Find(a => a.name == name);
        }

        // check if an addressbook with the given name exists
        private bool addressbookExists(string name)
        {
            return adressbooks.Exists(a => a.name == name);
        }

        

       

        

       
        // fill addressbook with sample contacts
        private void Sample()
        {
            Adressbook a = new Adressbook("sample");
            a.Add(new Contact("Bla", "bla@nogwat.com"));
            a.Add(new Contact("Pa", "pa@nogwat.com"));
            a.Add(new Contact("Ma", "ma@nogwat.com"));
            adressbooks.Add(a);
            a = new Adressbook("sample2");
            a.Add(new Contact("Bas Meesters", "bas_meesters@hotmail.com"));
            a.Add(new Contact("Jarno Le Conte", "jarno.leconte@me.com"));
            adressbooks.Add(a);
            a = new Adressbook("sample3");
            a.Add(new Contact("Henk", "henk@hotramail.com"));
            a.Add(new Contact("Jan", "jan@hotmail.com"));
            a.Add(new Contact("Piet", "piet@pieterse.nl"));
            a.Add(new Contact("Henkie", "henkie@henkse.nl"));
            a.Add(new Contact("Janneke", "janneke@jansen.nl"));
            a.Add(new Contact("Pieter", "pieter@gmail.com"));
            a.Add(new Contact("Gerda", "gerda@gmail.com"));
            a.Add(new Contact("Bettie", "bettie@gmail.com"));
            a.Add(new Contact("Peter", "peter@gmail.com"));
            adressbooks.Add(a);

        }

        public void test()
        {

        }
    }
}
