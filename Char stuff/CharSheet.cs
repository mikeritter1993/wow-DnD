/*********************************************************************************************************************
Class: CharSheet
Description: The most important part of the program, holds a players character stats and information, designed to make rolling and stat tracking easy for players
Status: not complete
TO DO: add weapon and spell functionality.
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
using Microsoft.VisualBasic;

namespace DnD
{
    public partial class CharSheet : Form
    {   
        enum Stats { Agility, Stamina, Strength, Charisma, Spirit, Intelligence };

        //data members
        private int refValueAgilityMod;     //variables that hold the plus modifier of the corresponding ability score for reference
        private int refValueStaminaMod;
        private int refValueStrengthMod;
        private int refValueCharismaMod;
        private int refValueIntelligenceMod;
        private int refValueSpiritMod;
        private int refValueProfeciantMod;
        private List<CheckboxAndLabel> skillsList;
        private List<CheckboxAndLabel> savingThrowsList;
        private List<String> charNameList;

        //constructors
        public CharSheet()
        {
            InitializeComponent();
            this.AcceptButton = messageBoxSendBtn;
            refValueProfeciantMod = 1; //$$temp remove once profeciant value is setup
            skillsList = new List<CheckboxAndLabel>();
            savingThrowsList = new List<CheckboxAndLabel>();
            charNameList = new List<String>();
            for (int i = 0; i < Program.User.CharSheets.Rows.Count; i++)
            {
                charNameList.Add((String)Program.User.CharSheets.Rows[i]["CharName"]);
            }
            Program.User.Client.MesgFunction = new MultiplayerConnectionHelper.Del(PrintMesgFromServer);
        }

        //class methods
        /***************************************************************************************************
        Parameters: String mesg - The message sent from the server.
        Description: Prints a mesg from the server to the charLog. This function may be called from different threads uses Invoke check to ensure thread safty.
        ****************************************************************************************************/
        private void PrintMesgFromServer(String mesg)
        {
            if (chatLogTxt.InvokeRequired)
            {
                this.Invoke(Program.User.Client.MesgFunction, mesg);
            }
            else
            {
                chatLogTxt.Text += mesg + "\n";
            }
        }

        /***************************************************************************************************
         Parameters: None
         Description: Sets everything on the sheet to 0
         ****************************************************************************************************/
        private void ZeroOutCharSheet()
        {
            staminaValue.Text = "0";
            strengthValue.Text = "0";
            agilityValue.Text = "0";
            intelligenceValue.Text = "0";
            spiritValue.Text = "0";
            charismaValue.Text = "0";

            foreach (CheckboxAndLabel item in skillsList)
            {
                item.BoxChecked = false;
            }
            foreach (CheckboxAndLabel item in savingThrowsList)
            {
                item.BoxChecked = false;
            }

            armorClassTxt.Text = "0";
            speedTxt.Text = "0";
            maxHpTxt.Text = "0";
            currentHpTxt.Text = "0";
            tempHpTxt.Text = "0";
            maxManaTxt.Text = "0";
            currentManaTxt.Text = "0";
            tempManaTxt.Text = "0";

        }

        /***************************************************************************************************
        Parameters: None
        Description: Takes all the data from the char sheet and puts them into properly formated CSV strings, 
                     then stores them in a data row and puts that row back into the main data table.
        ****************************************************************************************************/
        private void SaveCharData()
        {
            
            if (Program.User.CharSheets.Rows.Count > 0)
            {
                String[] dataStrings = new String[5];
                dataStrings[1] = staminaValue.Text + "," + strengthValue.Text + "," + agilityValue.Text + "," + intelligenceValue.Text + "," + spiritValue.Text + "," + charismaValue.Text;

                for (int i = 0; i < skillsList.Count; i++)
                {
                    if (skillsList[i].BoxChecked)
                    {
                        dataStrings[2] += "1";
                    }
                    else
                    {
                        dataStrings[2] += "0";
                    }
                    dataStrings[2] += ",";
                }
                dataStrings[2] = dataStrings[2].Remove(dataStrings[2].Length - 1, 1);

                for (int i = 0; i < savingThrowsList.Count; i++)
                {
                    if (savingThrowsList[i].BoxChecked)
                    {
                        dataStrings[3] += "1";
                    }
                    else
                    {
                        dataStrings[3] += "0";
                    }
                    dataStrings[3] += ",";
                }
                dataStrings[3] = dataStrings[3].Remove(dataStrings[3].Length - 1, 1);

                dataStrings[4] = armorClassTxt.Text + "," + speedTxt.Text + "," + maxHpTxt.Text + "," + currentHpTxt.Text + "," + tempHpTxt.Text + "," + maxManaTxt.Text + "," + currentManaTxt.Text + "," + tempManaTxt.Text;



                //charListCoBox.Items.Remove(Program.User.CharName);
                DataRow saveRow = Program.User.CharSheets.Rows[charListCoBox.SelectedIndex];
                //saveRow["CharName"] = charListCoBox.
                saveRow["Stats"] = dataStrings[1];
                saveRow["Skills"] = dataStrings[2];
                saveRow["SavingThrows"] = dataStrings[3];
                saveRow["Misc"] = dataStrings[4];
            }
        }

        /***************************************************************************************************
        Parameters: None
        Description: Gets the datarow of the corresponding selected character index and parses the CSV strings and puts their data into the correct place.
        ****************************************************************************************************/
        private void LoadCharData()
        {
            String selectCMD = "CharName = \'" + charListCoBox.SelectedValue + "\'";
            String[] dataStrings = new String[4];
            DataRow CharData = Program.User.CharSheets.Rows[charListCoBox.SelectedIndex];
            if (!CharData.IsNull("Stats"))
            {

                dataStrings[0] = (String)CharData["Stats"];
                dataStrings[1] = (String)CharData["Skills"];
                dataStrings[2] = (String)CharData["SavingThrows"];
                dataStrings[3] = (String)CharData["Misc"];
                List<int>[] dataInts = ParseCSV_Strings(dataStrings);

                staminaValue.Text = Convert.ToString(dataInts[0].ElementAt(0));
                strengthValue.Text = Convert.ToString(dataInts[0].ElementAt(1));
                agilityValue.Text = Convert.ToString(dataInts[0].ElementAt(2));
                intelligenceValue.Text = Convert.ToString(dataInts[0].ElementAt(3));
                spiritValue.Text = Convert.ToString(dataInts[0].ElementAt(4));
                charismaValue.Text = Convert.ToString(dataInts[0].ElementAt(5));

                for (int i = 0; i < dataInts[1].Count; i++)
                {
                    if (dataInts[1].ElementAt(i) == 1)
                    {
                        skillsList.ElementAt(i).BoxChecked = true;
                    }
                    else
                    {
                        skillsList.ElementAt(i).BoxChecked = false;
                    }
                }

                for (int i = 0; i < dataInts[2].Count; i++)
                {
                    if (dataInts[2].ElementAt(i) == 1)
                    {
                        savingThrowsList.ElementAt(i).BoxChecked = true;
                    }
                    else
                    {
                        savingThrowsList.ElementAt(i).BoxChecked = false;
                    }
                }

                armorClassTxt.Text = Convert.ToString(dataInts[3].ElementAt(0));
                speedTxt.Text = Convert.ToString(dataInts[3].ElementAt(1));
                maxHpTxt.Text = Convert.ToString(dataInts[3].ElementAt(2));
                currentHpTxt.Text = Convert.ToString(dataInts[3].ElementAt(3));
                tempHpTxt.Text = Convert.ToString(dataInts[3].ElementAt(4));
                maxManaTxt.Text = Convert.ToString(dataInts[3].ElementAt(5));
                currentManaTxt.Text = Convert.ToString(dataInts[3].ElementAt(6));
                tempManaTxt.Text = Convert.ToString(dataInts[3].ElementAt(7));
            }
        }

        /***************************************************************************************************
        Return: List of int arrays the contain the values of the parsed CSV strings 
        Parameters: data - Array of CSV strings to be parsed
        Description: Simple parse on CSV Strings using split function.
        ****************************************************************************************************/
        private List<int>[] ParseCSV_Strings(String[] data)
        {
            List<int>[] rValue = new List<int>[4];
            for (int i = 0; i < 4; i++)
            {
                rValue[i] = new List<int>();
            }
            String[] hold;
            int count = 0;
            foreach (String item in data)
            {
                hold = item.Split(',');
                foreach (String item2 in hold)
                {
                    if (item2 != "")
                    {
                        rValue[count].Add(int.Parse(item2));
                    }
                }
                count++;
            }
            return rValue;
        }

        /***************************************************************************************************
        Parameters: prefix - a message to be attached to the start of the roll message, diceRolls - list of ints of all the dice rolled numbers, totalRoll - total of all the dice rolled
        Description: Formats a roll message for sending to the server.
        ****************************************************************************************************/
        private void SendRollToServer(String prefix, List<int> diceRolls, int totalRoll)
        {
            String mesg = prefix + "( ";
            for (int i = 0; i < diceRolls.Count -1; i++)
            {
                mesg += diceRolls[i];
                if (diceRolls.Count-1 > 1 && i + 1 < diceRolls.Count-1)
                {
                    mesg += " + ";
                }
                else
                {
                    mesg += " ";
                }
            }
            mesg += ") + " + diceRolls[diceRolls.Count-1] + " = " + totalRoll;
            Program.User.Client.SendMessage(mesg);
        }

        /***************************************************************************************************
        Return: The random generated number.
        Parameters: lowBound - inclusive, highbound - inclusive
        Description: Generates a random number between the high and low bound parameters.
        ****************************************************************************************************/
        private int Roll(int lowBound, int highBound)
        {
            Random rand = new Random();
            int rValue = rand.Next(lowBound, highBound + 1);    //+1 to make highbound inclusive
            return rValue;
        }

        /***************************************************************************************************
        Return: List of ints that holds the dice values that were rolled
        Parameters: diceType - kind of dice(d20, d10...), numDice - how many dice of that type to troll, prof - to add proficiency bonus or not, total - total of all dice rolled, toAdd - which stat to add.
        Description: Rolls and totals all dice with stats and proficiency.
        ****************************************************************************************************/
        private List<int> CompleteRoll(int diceType, int numDice, bool prof, ref int total, Stats toAdd )
        {
            List<int> rolledDice = new List<int>();
            int bonus = 0;
            for (int i = 0; i < numDice; i++)
            {
                rolledDice.Add(Roll(1, diceType));
                total += rolledDice[i];
            }
           
            switch (toAdd)
            {
                case Stats.Agility:
                    bonus += refValueAgilityMod;
                    break;
                case Stats.Stamina:
                    bonus += refValueStaminaMod;
                    break;
                case Stats.Strength:
                    bonus += refValueStrengthMod;
                    break;
                case Stats.Charisma:
                    bonus += refValueCharismaMod;
                    break;
                case Stats.Spirit:
                    bonus += refValueSpiritMod;
                    break;
                case Stats.Intelligence:
                    bonus += refValueIntelligenceMod;
                    break;
                default:
                    break;
            }
            if (prof)
            {
                bonus += refValueProfeciantMod;
            }
            rolledDice.Add(bonus);
            total += bonus;
            return rolledDice;
        }

        /***************************************************************************************************
        Return: List of ints that holds the dice values that were rolled
        Parameters: diceType - kind of dice(d20, d10...), numDice - how many dice of that type to troll, prof - to add proficiency bonus or not, total - total of all dice rolled.
        Description: Rolls and totals all dice with stats and proficiency.
        ****************************************************************************************************/
        private List<int> CompleteRoll(int diceType, int numDice, bool prof, ref int total)
        {
            int bonus = 0;
            List<int> rolledDice = new List<int>();
            for (int i = 0; i < numDice; i++)
            {
                rolledDice.Add(Roll(1, diceType));
                total += rolledDice[i];
            }

            if (prof)
            {
                bonus += refValueProfeciantMod;
            }
            rolledDice.Add(bonus);
            total += bonus;
            return rolledDice;
        }

        /***************************************************************************************************
        Description: Called whenever critical stats are changed and need to be updated to all things that need them.
        ****************************************************************************************************/
        private void MasterUpdate()
        {
            UpdateSkills();
            UpdateSavingThrows();
            initiativeTxt.Text = "+" + Convert.ToString(refValueAgilityMod);
        }

        /***************************************************************************************************
        Description: Updates all skills with their appropriate stats
        ****************************************************************************************************/
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

        /***************************************************************************************************
        Description: Updates all saving throws with their appropriate stats
        ****************************************************************************************************/
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
        public void CheckboxAndLabelHandeler(object sender, EventArgs e)    //bubble up event handler for checkbox and label clicks
        {
            bool prof = false;
            int rollTotal = 0;
            List<int> diceRolls;

            if (sender.Equals(skillAcrobatics))     //Acrobatics
            {
                if (skillAcrobatics.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Agility);
                SendRollToServer("Acrobatics Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillAnimalHandling))    //Animal Handling
            {
                if (skillAnimalHandling.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Spirit);
                SendRollToServer("Animal Handling Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillArcana))        //Arcana
            {
                if (skillArcana.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Intelligence);
                SendRollToServer("Arcana Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillAthletics))     //Athletics
            {
                if (skillAthletics.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Strength);
                SendRollToServer("Athletics Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillDeception))     //Deception
            {
                if (skillDeception.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Charisma);
                SendRollToServer("Deception Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillHistory))       //History
            {
                if (skillHistory.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Intelligence);
                SendRollToServer("History Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillInsight))      //Insight
            {
                if (skillInsight.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Spirit);
                SendRollToServer("Insight Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillIntimidation))      //Intimidation
            {
                if (skillIntimidation.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Charisma);
                SendRollToServer("Intimidation Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillInvestigation))     //Investigation
            {
                if (skillInvestigation.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Intelligence);
                SendRollToServer("Investigation Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillMedicine))      //Medicine
            {
                if (skillMedicine.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Spirit);
                SendRollToServer("Medicine Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillNature))        //Nature
            {
                if (skillNature.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Intelligence);
                SendRollToServer("Nature Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillPerception))        //Perception
            {
                if (skillPerception.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Spirit);
                SendRollToServer("Perception Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillPerformance))       //Performance
            {
                if (skillPerformance.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Charisma);
                SendRollToServer("Performance Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillPersuasion))       //Persuasion
            {
                if (skillPersuasion.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Charisma);
                SendRollToServer("Persuasion Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillReligion))      //Religion
            {
                if (skillReligion.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Intelligence);
                SendRollToServer("Religion Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillSlightOfHand))      //Slight of Hand
            {
                if (skillSlightOfHand.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Agility);
                SendRollToServer("Slight Of Hand Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillStealth))       //Stealth
            {
                if (skillStealth.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Agility);
                SendRollToServer("Stealth Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(skillSurvival))      //Survival
            {
                if (skillSurvival.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Spirit);
                SendRollToServer("Survival Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(savingThrowStamina))      //saving throw stamina
            {
                if (savingThrowStamina.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Stamina);
                SendRollToServer("Stamina Saving Throw Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(savingThrowStrength))      //saving throw strength
            {
                if (savingThrowStrength.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Strength);
                SendRollToServer("Strength Saving Throw Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(savingThrowAgility))      //saving throw agility
            {
                if (savingThrowAgility.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Agility);
                SendRollToServer("Agility Saving Throw Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(savingThrowCharisma))      //saving throw charisma
            {
                if (savingThrowCharisma.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Charisma);
                SendRollToServer("Charisma Saving Throw Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(savingThrowIntelligence))      //saving throw intelligence
            {
                if (savingThrowIntelligence.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Intelligence);
                SendRollToServer("Intelligence Saving Throw Roll: ", diceRolls, rollTotal);
            }
            else if (sender.Equals(savingThrowSpirit))      //saving throw spirit
            {
                if (savingThrowSpirit.BoxChecked)
                {
                    prof = true;
                }
                diceRolls = CompleteRoll(20, 1, prof, ref rollTotal, Stats.Spirit);
                SendRollToServer("Spirit Saving Throw Roll: ", diceRolls, rollTotal);
            }
        }

        private void staminaLbl_Click(object sender, EventArgs e)       //rolls a stat check and sends it to the server.
        {
            List<int> diceRolls;
            int total = 0;
            diceRolls = CompleteRoll(20, 1, false, ref total, Stats.Stamina);
            SendRollToServer("Stamina Check: ", diceRolls, total);
        }
        private void strengthLbl_Click(object sender, EventArgs e)
        {
            List<int> diceRolls;
            int total = 0;
            diceRolls = CompleteRoll(20, 1, false, ref total, Stats.Strength);
            SendRollToServer("Strength Check: ", diceRolls, total);
        }
        private void agilityLbl_Click(object sender, EventArgs e)
        {
            List<int> diceRolls;
            int total = 0;
            diceRolls = CompleteRoll(20, 1, false, ref total, Stats.Agility);
            SendRollToServer("Agility Check: ", diceRolls, total);
        }
        private void intelligenceLbl_Click(object sender, EventArgs e)
        {
            List<int> diceRolls;
            int total = 0;
            diceRolls = CompleteRoll(20, 1, false, ref total, Stats.Intelligence);
            SendRollToServer("Intelligence Check: ", diceRolls, total);
        }
        private void spiritLbl_Click(object sender, EventArgs e)
        {
            List<int> diceRolls;
            int total = 0;
            diceRolls = CompleteRoll(20, 1, false, ref total, Stats.Spirit);
            SendRollToServer("Spirit Check: ", diceRolls, total);
        }
        private void charismaLbl_Click(object sender, EventArgs e)
        {
            List<int> diceRolls;
            int total = 0;
            diceRolls = CompleteRoll(20, 1, false, ref total, Stats.Charisma);
            SendRollToServer("Charisma Check: ", diceRolls, total);
        }
        private void initiativeLbl_Click(object sender, EventArgs e)
        {
            List<int> diceRolls;
            int total = 0;
            diceRolls = CompleteRoll(20, 1, false, ref total, Stats.Agility);
            SendRollToServer("Initiative Roll: ", diceRolls, total);
        }

        private void staminaValue_TextChanged(object sender, EventArgs e)   //stats were changed update everything that needs the value.
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

        private void CharSheet_FormClosing(object sender, FormClosingEventArgs e)   //Update all char rows in the main data table to the sql server.
        {
            if(Program.User.Client.Connected)
            {
                //Program.User.Client.SendMessage(Program.User.LoggedInUser["UserName"] + " Left the game");
            }
            SaveCharData();
            SqlHelper sql = new SqlHelper();

            for (int i = 0; i < Program.User.CharSheets.Rows.Count; i++)
            {
                sql.Command = "UPDATE [LoginData].[dbo].[CharSheets] SET [CharName] = \'" + Program.User.CharSheets.Rows[i]["CharName"] + "\', " +
                                                                        "[Stats] = \'" + Program.User.CharSheets.Rows[i]["Stats"] + "\', " +
                                                                        "[Skills] = \'" + Program.User.CharSheets.Rows[i]["Skills"] + "\', " +
                                                                        "[SavingThrows] = \'" + Program.User.CharSheets.Rows[i]["SavingThrows"] + "\', " +
                                                                        "[Misc] = \'" + Program.User.CharSheets.Rows[i]["Misc"] + "\' " +
                                                                     "WHERE [CharName] = \'" + charNameList[i] + "\'";
                sql.ExecuteNonQuery();
            }
        }
        private void CharSheet_Load(object sender, EventArgs e)
        {
            MasterUpdate();
      
            skillsList.Add(skillAcrobatics);        //fill checkboxandlabel lists with items/
            skillsList.Add(skillAnimalHandling);
            skillsList.Add(skillArcana);
            skillsList.Add(skillAthletics);
            skillsList.Add(skillDeception);
            skillsList.Add(skillHistory);
            skillsList.Add(skillInsight);
            skillsList.Add(skillIntimidation);
            skillsList.Add(skillInvestigation);
            skillsList.Add(skillMedicine);
            skillsList.Add(skillNature);
            skillsList.Add(skillPerception);
            skillsList.Add(skillPerformance);
            skillsList.Add(skillPersuasion);
            skillsList.Add(skillReligion);
            skillsList.Add(skillSlightOfHand);
            skillsList.Add(skillStealth);
            skillsList.Add(skillSurvival);
            savingThrowsList.Add(savingThrowStrength);
            savingThrowsList.Add(savingThrowAgility);
            savingThrowsList.Add(savingThrowStamina);
            savingThrowsList.Add(savingThrowIntelligence);
            savingThrowsList.Add(savingThrowSpirit);
            savingThrowsList.Add(savingThrowCharisma);

            foreach (CheckboxAndLabel item in skillsList)       //set parent reference for all checkboxandlabels
            {
                item.ParentRef = this;
            }
            foreach (CheckboxAndLabel item in savingThrowsList)
            {
                item.ParentRef = this;
            }

            charListCoBox.DropDownStyle = ComboBoxStyle.DropDownList;
            if (Program.User.CharSheets.Rows.Count > 0)         //if the account has preexisting characters load them into the dropdown list.
            {
                for (int i = 0; i < Program.User.CharSheets.Rows.Count; i++)    
                {
                    charListCoBox.Items.Add(Program.User.CharSheets.Rows[i]["CharName"]);
                    charNameList.Add((String)Program.User.CharSheets.Rows[i]["CharName"]);
                }
                charListCoBox.SelectedItem = charListCoBox.Items[0];
                charListCoBox.SelectedValue = charListCoBox.Items[0];
                LoadCharData();
            }
        }

        private void charListCoBox_SelectedIndexChanged(object sender, EventArgs e)     //drop down changed load new char data
        { 
            LoadCharData();
        }
        private void charListBox_DropDown(object sender, EventArgs e)       //drop down about to change save char data
        {
            SaveCharData();
        }

        private void newCharacterBtn_Click(object sender, EventArgs e)      //prompts user for the new char name if they dont hit cancel, add a data row to the char datatable and add the row to the sql server
        {
            if (charListCoBox.SelectedItem != null)
            {
                SaveCharData();
            }
            String newCharName;
            InputMessageBox mesgBox = new InputMessageBox("Enter Name", "Enter the new characters name.");
            newCharName = mesgBox.ShowForm();
            if (newCharName != null)
            {
                ZeroOutCharSheet();
                DataRow newChar = Program.User.CharSheets.NewRow();
                newChar["UserName"] = Program.User.LoggedInUser["UserName"];
                newChar["CharName"] = newCharName;
                Program.User.CharSheets.Rows.Add(newChar);
                charNameList.Add(newCharName);
                charListCoBox.SelectedIndex = charListCoBox.Items.Add(newCharName);
                SaveCharData();
                SqlHelper sql = new SqlHelper();
                sql.Command = "INSERT INTO [LoginData].[dbo].[CharSheets] (UserName, CharName, Stats, Skills, SavingThrows, Misc)" +
                                                                   " VALUES(\'" + newChar["UserName"] + "\'," +
                                                                            "\'" + newChar["CharName"] + "\'," +
                                                                            "\'" + newChar["Stats"] + "\'," +
                                                                            "\'" + newChar["Skills"] + "\'," +
                                                                            "\'" + newChar["SavingThrows"] + "\'," +
                                                                            "\'" + newChar["Misc"] + "\');";
                sql.ExecuteNonQuery();
                                                                                                     
            }
        }
        private void editCharacterBtn_Click(object sender, EventArgs e)     //simple name change prompt for user, edits the current selected character.
        {
            String newName;
            InputMessageBox mesgBox = new InputMessageBox("New Name", "Enter the new name for the character.");
            newName = mesgBox.ShowForm();
            if (newName != null)
            {
                SaveCharData();
                Program.User.CharSheets.Rows[charListCoBox.SelectedIndex]["CharName"] = newName;
                charListCoBox.Items[charListCoBox.SelectedIndex] = newName;
            }
        }
        private void deleteCharBtn_Click(object sender, EventArgs e)       //deletes the currently selected character, also removes its row from the sql server
        {
            DialogResult yesNo;
            yesNo = MessageBox.Show(this, "Are you sure you want to delete the currently selected character?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (yesNo == DialogResult.Yes)
            {
                SqlHelper sql = new SqlHelper();
                sql.Command = "DELETE FROM [LoginData].[dbo].[CharSheets] WHERE UserName = \'" + Program.User.LoggedInUser["UserName"] + "\' AND CharName = \'" + Program.User.CharSheets.Rows[charListCoBox.SelectedIndex]["CharName"] + "\';";
                sql.ExecuteNonQuery();
                charNameList.RemoveAt(charListCoBox.SelectedIndex);
                Program.User.CharSheets.Rows.RemoveAt(charListCoBox.SelectedIndex);
                charListCoBox.Items.RemoveAt(charListCoBox.SelectedIndex);
                ZeroOutCharSheet();
                if (charListCoBox.Items.Count > 0)
                {
                    charListCoBox.SelectedIndex = 0;
                }
            }
        }

        private void messageBoxSendBtn_Click(object sender, EventArgs e)    //send whatever is in the message box to the server
        {
            if (messageBoxTxt.Text != "")
            {
                Program.User.Client.SendMessage(messageBoxTxt.Text);
                messageBoxTxt.Text = "";
            }
        }
        private void chatLogTxt_TextChanged(object sender, EventArgs e) //auto scroll the chat log down when 1 page is full
        {
            chatLogTxt.SelectionStart = chatLogTxt.Text.Length;
            chatLogTxt.ScrollToCaret();
        }
    }
}
