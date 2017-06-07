using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sql_and_interactive_window
{
    public partial class CharSheet : Form
    {
        private int refValueAgilityMod;
        private int refValueStaminaMod;
        private int refValueStrengthMod;
        private int refValueCharismaMod;
        private int refValueIntelligenceMod;
        private int refValueSpiritMod;
        private int refValueProfeciantMod;

        public CharSheet()
        {
            InitializeComponent();
            refValueProfeciantMod = 1; //$$temp remove once profeciant value is setup
        }

        private void MasterUpdate()
        {
            UpdateSkills();
            UpdateSavingThrows();
            initiativeTxt.Text = "+" + Convert.ToString(refValueAgilityMod);
        }

        private int Roll(int lowBound, int highBound)
        {
            Random rand = new Random();
            int rValue = rand.Next(lowBound, highBound + 1);
            return rValue;
        }

        private void staminaLbl_Click(object sender, EventArgs e)
        {
            int rolledValue = Roll(1, 20) + refValueStaminaMod;

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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CharSheet_Load(object sender, EventArgs e)
        {
            MasterUpdate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateSkills()
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

        private void UpdateSavingThrows()
        {
            savingThrowAgility.UpdateCheckBoxAndLabel(refValueAgilityMod, refValueProfeciantMod);
            savingThrowStrength.UpdateCheckBoxAndLabel(refValueStrengthMod, refValueProfeciantMod);
            savingThrowSpirit.UpdateCheckBoxAndLabel(refValueSpiritMod, refValueProfeciantMod);
            savingThrowIntelligence.UpdateCheckBoxAndLabel(refValueIntelligenceMod, refValueProfeciantMod);
            savingThrowCharisma.UpdateCheckBoxAndLabel(refValueCharismaMod, refValueProfeciantMod);
            savingThrowStamina.UpdateCheckBoxAndLabel(refValueStaminaMod, refValueProfeciantMod);
        }
        private void speedLbl_Click(object sender, EventArgs e)
        {

        }

        private void currentHpLbl_Click(object sender, EventArgs e)
        {

        }

        private void TempHpLbl_Click(object sender, EventArgs e)
        {

        }

        private void initiativeLbl_Click(object sender, EventArgs e)
        {

        }

        private void maxHpPan_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
