/*******************************************************************************************************************************
Class: CheckboxAndLabel
Description: Custom UserControl for windows forms porjects, uses the pre exsisting windows form tools "check box" and 2 "Labels"
             main functionality if check box is checked add profMod to label 1
*******************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DnD
{
    public partial class CheckboxAndLabel : UserControl
    {
        //Data Members
        private String skillName;   //the text value for nameLbl
        private String labelText;   //the text value for totalValueLbl
        private int abilityMod;     //the ability modifer score to add the roll of this skill, passed into from a CharSheet
        private int profMod;        //the proficiency modifer to add to the roll of this skill if the check box is checked, passed in from char sheet
        private bool boxChecked;    //tells if the check box is currently checked or not
        private CharSheet parentRef;//reference value of the Parent form that this control belongs to, used to call bubble up handler function in char sheet.

        //Constructors
        public CheckboxAndLabel()
        {
            InitializeComponent();
            boxChecked = false;
            abilityMod = 0;
            profMod = 0;
        }

        //Class Methods
        public void UpdateCheckBoxAndLabel(int updatedMod, int updatedProfMod)  //called from Char sheet, used to update ability mod and prof mod when new values are entered on char sheet
        {
            AbilityMod = updatedMod;
            profMod = updatedProfMod;
            LabelText = Convert.ToString(CalcTotal());
        }

        private int CalcTotal()     //called to figure out the total plus roll to add to this skill (if check box is checked add prof mod, otherwise only add abilityMod
        {
            int rValue = abilityMod;
            if (boxChecked)
            {
                rValue += profMod;
            }
            return rValue;
        }

        //Event Methods
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                boxChecked = true;
            }
            else
            {
                boxChecked = false;
            }
            LabelText = Convert.ToString(CalcTotal());
        }
        private void nameLbl_Click(object sender, EventArgs e)
        {
            ParentRef.CheckboxAndLabelHandeler(this, e);
        }


        //Member access methods
        public string LabelText
        {
            get
            {
                return totalValueLbl.Text;
            }

            set
            {
                totalValueLbl.Text = value;
            }
        }

        public int AbilityMod
        {
            get
            {
                return abilityMod;
            }

            set
            {
                abilityMod = value;
            }
        }

        public int ProfMod
        {
            get
            {
                return profMod;
            }

            set
            {
                profMod = value;
            }
        }

        public bool BoxChecked
        {
            get
            {
                return boxChecked;
            }

            set
            {
                boxChecked = value;
                checkBox1.Checked = value;
            }
        }

        public string SkillName
        {
            get
            {
                return nameLbl.Text;
            }

            set
            {
                nameLbl.Text = value;
            }
        }

        public CharSheet ParentRef
        {
            get
            {
                return parentRef;
            }

            set
            {
                parentRef = value;
            }
        }
    }
}