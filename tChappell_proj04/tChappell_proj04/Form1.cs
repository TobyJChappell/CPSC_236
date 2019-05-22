using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace tChappell_proj04
{
    public partial class Form1 : Form
    {
        BindingList<Trainer> trainerList = new BindingList<Trainer>();

        public Form1()
        {
            InitializeComponent();

            // Set the GUI listbox to use the trainerList as a data source
            listRecords.DataSource = trainerList;
            listRecords.DisplayMember = "displayName";
            loadRecords();
        }

        /**
         * Loads records from a file called "Trainers.txt"
         */
        public void loadRecords()
        {
            if (!File.Exists(@"Trainers.txt"))
            {
                File.Create(@"Trainers.txt").Close();
                return;
            }
            using (StreamReader file = new StreamReader(@"Trainers.txt"))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');
                    trainerList.Add(new Trainer(parts[0], parts[1], parts[2]));
                }
            }
        }

        /**
         * Writes all records to a file called "Trainers.txt"
         */
        public void writeRecords()
        {
            using (StreamWriter file = new StreamWriter(@"Trainers.txt"))
            {
                foreach (Trainer trainer in trainerList)
                {
                    file.WriteLine(trainer.tname + "|" + trainer.rname + "|" + trainer.starter);
                }
            }
        }

        /**
         * Adds a record to the list
         */
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Visible == false)
            {
                buttonEdit.Visible = true;
                buttonUpdate.Visible = false;
            }
            if (!checkInputs())
                return;
            for (int x = 0; x < trainerList.Count; x++)
            {
                if (trainerList[x].tname == textboxTrainerName.Text)
                {
                    MessageBox.Show("Trainer names cannot be duplicated.");
                    return;
                }
            }
            trainerList.Add(new Trainer(textboxTrainerName.Text, textboxRivalName.Text, starterInput.SelectedItem.ToString()));
            listRecords.SelectedIndex = trainerList.Count - 1;
        }

        /**
         * Determines if an input is valid (not empty and does not have a duplicate title)
         */
        private bool checkInputs()
        {
            if (textboxTrainerName.Text == "" || textboxRivalName.Text == "" || starterInput.SelectedItem == null)
            {
                MessageBox.Show("Inputs must contain some value.");
                return false;
            }
            return true;
        }

        /**
         * Deletes a record from the list
         */
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Visible == false)
            {
                buttonEdit.Visible = true;
                buttonUpdate.Visible = false;
            }
            int idx = listRecords.SelectedIndex;
            if (idx > -1)
                trainerList.RemoveAt(idx);
        }

        /**
         * Allows the user to edit a record
         */
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int idx = listRecords.SelectedIndex;
            if(idx > -1)
            {
                buttonEdit.Visible = false;
                buttonUpdate.Visible = true;
            }
        }

        /**
         * Updates a record based on inputted information
         */ 
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            int idx = listRecords.SelectedIndex;
            if(idx > -1)
            {
                if (!checkInputs())
                    return;
                for (int x = 0; x < trainerList.Count; x++)
                {
                    if (trainerList[x].tname == textboxTrainerName.Text && x != listRecords.SelectedIndex)
                    {
                        MessageBox.Show("Trainer names cannot be duplicated.");
                        return;
                    }
                }
                trainerList[idx] = new Trainer(textboxTrainerName.Text, textboxRivalName.Text, starterInput.SelectedItem.ToString());
            }            
        }

        /**
         * Displays a records information in the appropriate text boxes
         */
        private void listRecords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(buttonEdit.Visible == false)
            {
                buttonEdit.Visible = true;
                buttonUpdate.Visible = false;
            }
            int idx = listRecords.SelectedIndex;
            if (idx > -1)
            {
                textboxTrainerName.Text = trainerList[idx].tname;
                textboxRivalName.Text = trainerList[idx].rname;
                starterInput.SelectedItem = trainerList[idx].starter;
            }
        }

        /**
         * Uploads records to "Trainers.txt" when form closes
         */
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            writeRecords();
        }
    }
}
