using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApiCommunication.DTO;

namespace WebApiTestApplication.VisualElements
{
    public class ContactView : RichTextBox
    {
        public Contact ContactObj { get; private set; }
        public bool IsSelected { get; private set; }

         public ContactView(Contact obj)
         {
             ContactObj = obj;

             this.SelectionProtected = true;
             this.ReadOnly = true;
             this.Size = new Size(360, 70);
             this.BorderStyle = BorderStyle.None;
             this.MouseEnter += LabelHover;
             this.MouseLeave += LabelLeave;
             this.MouseDoubleClick += DoubleClick;
             this.Click += MouseClick;
             this.SelectionChanged += DisableSelection;

             CreateContactLabel();
         }
        
         void CreateContactLabel()
         {
            this.BackColor = Color.LightGray;
            this.Text = $"  Id: {ContactObj.Id}\n  Name: {ContactObj.Name}\n  EGN:{ContactObj.Egn}\n  Address:{ContactObj.Address}\n  Telephone:{ContactObj.Telephone}";
         }


         Color oldColor = DefaultBackColor;
         private void LabelLeave(object sender, EventArgs e)
         {
             Cursor = Cursors.Arrow;
             ((RichTextBox)sender).BackColor = oldColor;
         }

         private void LabelHover(object sender, EventArgs e)
         {
             Cursor = Cursors.Hand;
             oldColor = ((RichTextBox)sender).BackColor;
             ((RichTextBox)sender).BackColor = Color.White;
         }

         private new void DoubleClick(object sender, EventArgs e)
         {
             throw new NotImplementedException();
         }

         private new void MouseClick(object sender, EventArgs e)
         {
             IsSelected = !IsSelected;
             if (IsSelected)
             {
                 foreach (ContactView c in this.Parent.Controls)
                 {
                     c.BorderStyle = BorderStyle.None;
                     c.IsSelected = false;
                 }

                 this.BorderStyle = BorderStyle.Fixed3D;
                 this.IsSelected = true;
             }
             else
             {
                 this.BorderStyle = BorderStyle.None;
             }
         }

         private void DisableSelection(object sender, EventArgs e)
         {
             // Move the cursor to the end
             if (this.SelectionStart != this.TextLength)
             {
                 this.SelectionStart = this.TextLength;
             }
         }
    }
}

