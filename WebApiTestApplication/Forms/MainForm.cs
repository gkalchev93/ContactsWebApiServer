using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApiCommunication;
using WebApiCommunication.DTO;
using WebApiTestApplication.VisualElements;

namespace WebApiTestApplication.Forms
{
    public partial class MainForm : Form
    {
        Client httpClient;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            httpClient = new Client(@"http://localhost:65366/");
            cbSearchField.SelectedIndex = 0;
            RefreshContacts();
        }

        private void menuAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (ContactViewForm dlg = new ContactViewForm(null))
                {
                    var res = dlg.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        httpClient.CreateContact(dlg.ContactItem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Failed to create Contact");
            }
            finally
            {
                RefreshContacts();
            }
        }

        Contact[] contacts;
        private void RefreshContacts(bool all = true)
        {
            try
            {
                if(all)
                {
                    var getTask = httpClient.GetContacts();
                    getTask.Wait();
                    contacts = getTask.Result;
                }
                
                
                panelView.Controls.Clear();
                if(contacts != null)
                {
                    contacts = contacts.OrderBy(x => x.Name).ToArray();
                    foreach (var c in contacts)
                    {
                        panelView.Controls.Add(new ContactView(c));
                    }
                }
                
                panelView.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Failed to refresh the contacts");
            }
        }

        private void menuEdit_Click(object sender, EventArgs e)
        {
            Contact selectedItem = null;
            try
            {
                selectedItem = GetSelectedItem();

                if (selectedItem == null)
                {
                    MessageBox.Show("Please select item.", "Warning");
                }
                else
                {
                    using (ContactViewForm dlg = new ContactViewForm(selectedItem))
                    {
                        var res = dlg.ShowDialog();
                        if (res == DialogResult.OK)
                        {
                            httpClient.UpdateContact(dlg.ContactItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Failed to update Contact");
            }
            finally
            {
                RefreshContacts();
            }
        }

        private Contact GetSelectedItem()
        {
            Contact retObj = null;

            foreach (ContactView item in panelView.Controls)
            {
                if (item.IsSelected)
                {
                    retObj = item.ContactObj;
                    break;
                }
            }

            return retObj;
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshContacts();
        }

        private void menuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = GetSelectedItem();
                if(selectedItem != null)
                {
                    httpClient.DeleteContact(selectedItem.Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to delete Contact");
            }
            finally
            {
                RefreshContacts();
            }
        }

        bool searchModeEnabled = false;
        private void menuSearch_Click(object sender, EventArgs e)
        {
            if (searchModeEnabled)
            {
                this.Size = new Size(419, 489);
                searchModeEnabled = false;
            }
            else
            {
                this.Size = new Size(629, 489);
                searchModeEnabled = true;
            }
        }

        private void tbSearchValue_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tbSearchValue.Text))
                {
                    RefreshContacts();
                }
                else
                {
                    switch (cbSearchField.SelectedIndex)
                    {
                        case 0:
                            GetContactsById();
                            break;
                        case 1:
                            GetContactsByName();
                            break;
                        case 2:
                            GetContactsByEgn();
                            break;
                        case 3:
                            GetContactsByAddress();
                            break;
                        case 4:
                            GetContactsByTelephone();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
                RefreshContacts(true);
            }
        }

        private async void GetContactsByName()
        {
            contacts = await httpClient.GetContactsByName(tbSearchValue.Text, !checkMatch.Checked);
            RefreshContacts(false);
        }

        private async void GetContactsByEgn()
        {
            contacts = await httpClient.GetContactsByEgn(tbSearchValue.Text, !checkMatch.Checked);
            RefreshContacts(false);
        }

        private async void GetContactsByAddress()
        {
            contacts = await httpClient.GetContactsByAddress(tbSearchValue.Text, !checkMatch.Checked);
            RefreshContacts(false);
        }

        private async void GetContactsByTelephone()
        {
            contacts = await httpClient.GetContactsByTelephone(tbSearchValue.Text, !checkMatch.Checked);
            RefreshContacts(false);
        }

        private async void GetContactsById()
        {
            if(Int32.TryParse(tbSearchValue.Text, out int id))
            {
                contacts = await httpClient.GetContactsById(id, !checkMatch.Checked);
                RefreshContacts(false);
            }
        }
    }
}
