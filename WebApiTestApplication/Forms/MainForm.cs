﻿using System;
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

        private void RefreshContacts()
        {
            try
            {
                var getTask = httpClient.GetContacts();
                getTask.Wait();
                var contacts = getTask.Result;
                panelView.Controls.Clear();
                foreach (var c in contacts)
                {
                    panelView.Controls.Add(new ContactView(c));
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
                httpClient.DeleteContact(selectedItem.Id);
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
            if (cbContains.Checked)
            {
                switch (cbSearchField.SelectedIndex)
                {

                }

            }
            else
            {
                switch (cbSearchField.SelectedIndex)
                {

                }
            }
        }
    }
}