/*********************************************************************************************************************
Class: CharSheet
Description: The main page of the program, holds a players character stats and information, designed to make rolling and stat tracking easy for players
Status: not complete
TO DO: add death saving throw section, add roll function calls to EVERYTHING, prolly some other stuff cant think atm
**********************************************************************************************************************/

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
    public partial class CharSheet : Form
    {   
        //data members
        private int refValueAgilityMod;     //variables that hold the plus modifier of the corresponding ability score for reference
        private int refValueStaminaMod;
        private int refValueStrengthMod;
        private int refValueCharismaMod;
        private int refValueIntelligenceMod;
        private int refValueSpiritMod;
        private int refValueProfeciantMod;

        //constructors
        public CharSheet()
        {
            InitializeComponent();
            refValueProfeciantMod = 1; //$$temp remove once profeciant value is setup
        }

        //class methods
        private void MasterUpdate() //called on page load and anytime a change to the ability scores happens, used to update anything on the page that needs it
        {
            UpdateSkills();
            UpdateSavingThrows();
            initiativeTxt.Text = "+" + Convert.ToString(refValueAgilityMod);
        }

        private int Roll(int lowBound, int highBound)   //randoms a number between lowBound (inclusive) and highBound (inclusive)
        {
            Random rand = new Random();
            int rValue = rand.Next(lowBound, highBound + 1);    //+1 to make highbound inclusive
            return rValue;
        }

        private void UpdateSkills()    //gets information into all skills that need it
        {
            skillAcrobatics.UpdateCheckBoxAndLabel(refValueAgilityMod, refValueProfeciantMod);
            skillSlightOfHand.UpdateCheckBoxAndLabel(refValueAgilityMod, refValueProfeciantMod);
            skillStealth.UpdateCheckBoxAndLabel(refValueAgilityMod, refValueProfeciantMod);

            skillAnimalHandling.UpdateCheckBoxAndLabel(refValueSpiritMod, refValueProfeciantMod);
            skillInsight.UpdateCheckBoxAndLabel(refValueSpiritMod, refValueProfeciantMod);
            skillMedicine.UpdateCheckBoxAndLabel(refValueSpiritMod, refValueProfeciantMod);
            skillPerception.UpdateCheckBoxAndLabel(refValueSpiritMod, refValueProfeciantMod);
            skillSurvival.UpdateCheckBoxAndLabel(refValueSpiritMod, refValueProfeciantMod);

            skillHistory.UpdateCheckBoxAndLabel(refValueIntelligenceMod, refValueProfeciantMod);
            skillInvestigation.UpdateCheckBoxAndLabel(refValueIntelligenceMod, refValueProfeciantMod);
            skillNature.UpdateCheckBoxAndLabel(refValueIntelligenceMod, refValueProfeciantMod);
            skillReligion.UpdateCheckBoxAndLabel(refValueIntelligenceMod, refValueProfeciantMod);
            skillArcana.UpdateCheckBoxAndLabel(refValueIntelligenceMod, refValueProfeciantMod);

            skillAthletics.UpdateCheckBoxAndLabel(refValueStrengthMod, refValueProfeciantMod);

            skillDeception.UpdateCheckBoxAndLabel(refValueCharismaMod, refValueProfeciantMod);
            skillIntimidation.UpdateCheckBoxAndLabel(refValueCharismaMod, refValueProfeciantMod);
            skillPerformance.UpdateCheckBoxAndLabel(refValueCharismaMod, refValueProfeciantMod);
            skillPersuasion.UpdateCheckBoxAndLabel(refValueCharismaMod, refValueProfeciantMod);
        }

        private void UpdateSavingThrows()   //gets information into all savingThrows that need it
        {
            savingThrowAgility.UpdateCheckBoxAndLabel(refValueAgilityMod, refValueProfeciantMod);
            savingThrowStrength.UpdateCheckBoxAndLabel(refValueStrengthMod, refValueProfeciantMod);
            savingThrowSpirit.UpdateCheckBoxAndLabel(refValueSpiritMod, refValueProfeciantMod);
            savingThrowIntelligence.UpdateCheckBoxAndLabel(refValueIntelligenceMod, refValueProfeciantMod);
            savingThrowCharisma.UpdateCheckBoxAndLabel(refValueCharismaMod, refValueProfeciantMod);
            savingThrowStamina.UpdateCheckBoxAndLabel(refValueStaminaMod, refValueProfeciantMod);
        }

        //event methods
        private void staminaLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }

        private void strengthLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }

        private void agilityLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }

        private void intelligenceLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }

        private void spiritLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }

        private void charismaLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }
        private void initiativeLbl_Click(object sender, EventArgs e)
        {
            //$$add roll function call
        }

        private void staminaValue_TextChanged(object sender, EventArgs e)
        {
            int mod;
            int.TryParse(staminaValue.Text, out mod);

            refValueStaminaMod = ((mod / 2) - 5);
            staminaMod.Text = "+" + refValueStaminaMod;
            MasterUpdate();
        }

        private void strengthValue_TextChanged(object sender, EventArgs e)
        {
            int mod;
            int.TryParse(strengthValue.Text, out mod);

            refValueStrengthMod = ((mod / 2) - 5);
            strengthMod.Text = "+" + refValueStrengthMod;
            MasterUpdate();
        }

        private void agilityValue_TextChanged(object sender, EventArgs e)
        {
            int mod;
            int.TryParse(agilityValue.Text, out mod);

            refValueAgilityMod = ((mod / 2) - 5);
            agilityMod.Text = "+" + refValueAgilityMod;
            MasterUpdate();
        }

        private void intelligenceValue_TextChanged(object sender, EventArgs e)
        {
            int mod;
            int.TryParse(intelligenceValue.Text, out mod);

            refValueIntelligenceMod = ((mod / 2) - 5);
            intelligenceMod.Text = "+" + refValueIntelligenceMod;
            MasterUpdate();
        }

        private void spiritValue_TextChanged(object sender, EventArgs e)
        {
            int mod;
            int.TryParse(spiritValue.Text, out mod);

            refValueSpiritMod = ((mod / 2) - 5);
            spiritMod.Text = "+" + refValueSpiritMod;
            MasterUpdate();
        }

        private void charismaValue_TextChanged(object sender, EventArgs e)
        {
            int mod;
            int.TryParse(charismaValue.Text, out mod);

            refValueCharismaMod = ((mod / 2) - 5);
            charismaMod.Text = "+" + refValueCharismaMod;
            MasterUpdate();
        }
    }
}
