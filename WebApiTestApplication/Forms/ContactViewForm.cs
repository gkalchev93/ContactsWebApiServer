using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApiCommunication.DTO;

namespace WebApiTestApplication.Forms
{
    public partial class ContactViewForm : Form
    {
        public Contact ContactItem { get; set; }

        public ContactViewForm(Contact item)
        {
            InitializeComponent();
            ContactItem = item;
            
        }

        private void ContactViewForm_Load(object sender, EventArgs e)
        {
            if (ContactItem != null)
            {
                tbName.Text = ContactItem.Name;
                tbEgn.Text = ContactItem.Egn;
                tbAddress.Text = ContactItem.Address;
                tbTelephone.Text = ContactItem.Telephone;
                btnOk.Text = "Save";
                this.Text += $" - ContactId = {ContactItem.Id}";
            }
            else
            {
                btnOk.Text = "Add";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbName.Text))
            {
                MessageBox.Show("Please fill the Name field.");
            }
            else if(string.IsNullOrEmpty(tbEgn.Text) || tbEgn.Text.Length != 10)
            {
                MessageBox.Show("Please fill the EGN field.");
            }
            else if(string.IsNullOrEmpty(tbAddress.Text))
            {
                MessageBox.Show("Please fill the Address field.");
            }
            else if(string.IsNullOrEmpty(tbTelephone.Text))
            {
                MessageBox.Show("Please fill the Telephone field.");
            }
            else
            {
                if(ContactItem != null)
                    ContactItem = new Contact(ContactItem.Id, tbName.Text, tbEgn.Text, tbAddress.Text, tbTelephone.Text);
                else
                    ContactItem = new Contact(tbName.Text, tbEgn.Text, tbAddress.Text, tbTelephone.Text);

                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
