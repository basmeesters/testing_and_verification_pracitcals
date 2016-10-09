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

        public List<Label> addressbooksLabel = new List<Label>();
        public List<Button> addressbooksDeleteBtns = new List<Button>();

        // load (setup) this view
        private void loadViewSelectAddressbook()
        {
            int y = 40;

            // show all available addressbooks
            for (int i = 0; i < adressbooks.Count; i++)
            {
                Label l = new Label();
                l.Location = new Point(45, y);
                l.Text = adressbooks[i].name;
                addressbooksLabel.Add(l);
                this.Controls.Add(l);

                Button b = new Button();
                b.Location = new Point(170, y);
                b.Text = "View";
                b.Name = i.ToString();
                b.Click += viewAdressbooks;
                addressbooksDeleteBtns.Add(b);
                this.Controls.Add(b);

                y += 22;
            }

            // Check if texts of labels are the same as the names of the available addressbooks
            bool d = false;
            int z = 0;
            foreach (Label l in addressbooksLabel)
            {
                d = l.Text == adressbooks[z].name;
                z++;
                if (d == false)
                    break;
            }
            LogManager.Oracle(": Check if viewLables contain same text as the data", d);
        }

        // unload this view
        private void unloadViewSelectAddressbook()
        {
            foreach (Label l in addressbooksLabel)
                this.Controls.Remove(l);
            foreach (Button b in addressbooksDeleteBtns)
                this.Controls.Remove(b);

            addressbooksLabel.Clear();
            addressbooksDeleteBtns.Clear();

        }


        // when the view button is pressed, the corresponding addressbook will be shown
        private void viewAdressbooks(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            current = Convert.ToInt32(b.Name);
            changeViewTo("addressbook");
        }


    }
}
