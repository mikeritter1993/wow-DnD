/*******************************************************************************************************************************
Class: InputMessageBox
Description: Simple custom control for a pop up user input box
*******************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnD
{
    public partial class InputMessageBox : Form
    {
        //data members
        private String title;       //window title
        private String messageText; //the non-editable message to print in the window to the user
        private String toReturn;    //the text that the user enters

        
        //constuctors
        public InputMessageBox(String textTitle, String directions)
        {
            InitializeComponent();
            this.AcceptButton = okBtn;
            Title = textTitle;
            MessageText = directions;
        }

        public InputMessageBox()
        {
            InitializeComponent();
        }

        //class methods
        public String ShowForm()
        {
            this.ShowDialog();
            return toReturn;
        }

        //event methods
        private void okBtn_Click(object sender, EventArgs e)
        {
            ToReturn = enteredTextTxt.Text;
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            ToReturn = null;
            this.Close();
        }

        //getters and setters
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                Text = value;
            }
        }

        public string MessageText
        {
            get
            {
                return messageText;
            }

            set
            {
                messageText = value;
                messageBoxTxt.Text = value;
            }
        }

        public string ToReturn
        {
            get
            {
                return toReturn;
            }

            set
            {
                toReturn = value;
            }
        }
    }
}
