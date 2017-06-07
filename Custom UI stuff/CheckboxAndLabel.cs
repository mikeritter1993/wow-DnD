using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sql_and_interactive_window
{
    public partial class CheckboxAndLabel : UserControl
    {

        private String skillName;
        private String labelText;
        private int abilityMod;
        private int profMod;
        private bool boxChecked;

      

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

     

        public CheckboxAndLabel()
        {
            InitializeComponent();
            boxChecked = false;
            abilityMod = 0;
            profMod = 0;
        }

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

        private int CalcTotal()
        {
            int rValue = abilityMod;
            if (boxChecked)
            {
                rValue += profMod;
            }
            return rValue;
        }

        public void UpdateCheckBoxAndLabel(int updatedMod, int updatedProfMod)
        {
            AbilityMod = updatedMod;
            profMod = updatedProfMod;
            LabelText = Convert.ToString(CalcTotal());
        }

        private void nameLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }

        private void totalValueLbl_Click(object sender, EventArgs e)
        {

        }
    }
}