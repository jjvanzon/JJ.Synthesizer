using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms.Forms;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    public partial class MainForm : SimpleFileProcessForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_OnRunProcess(object sender, EventArgs e)
        {
            if (radioButtonMigrateSineVolumes.Checked)
            {
                string message = "Are you REALLY sure you want to run the process: MIGRATE SINE VOLUMES???";
                if (MessageBox.Show(message, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataMigrationExecutor.MigrateSineVolumes(x => ShowProgress(x));
                }
            }
            else if (radioButtonAddSampleOperatorFrequencyInlets.Checked)
            {
                string message = "Are you REALLY sure you want to run the process: ADD SAMPLE OPERATOR FREQUENCY INLETS???";
                if (MessageBox.Show(message, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataMigrationExecutor.AddSampleOperatorFrequencyInlets(x => ShowProgress(x));
                }
            }
            else
            {
                MessageBox.Show("Please select a radio button.");
            }
        }
    }
}
