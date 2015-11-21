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
                DataMigrationExecutor.MigrateSineVolumes(x => ShowProgress(x));
            }
            else if (radioButtonAddSampleOperatorFrequencyInlets.Checked)
            {
                DataMigrationExecutor.AddSampleOperatorFrequencyInlets(x => ShowProgress(x));
            }
            else if (radioButtonMakePatchNamesUnique.Checked)
            {
                DataMigrationExecutor.MakePatchNamesUnique(x => ShowProgress(x));
            }
            else if (radioButtonMakeCurveNamesAndSampleNamesUnique.Checked)
            {
                DataMigrationExecutor.MakeCurveNamesAndSampleNamesUnique(x => ShowProgress(x));
            }
            else if (radioButtonPutEachPatchInAChildDocument.Checked)
            {
                DataMigrationExecutor.PutEachPatchInAChildDocument(x => ShowProgress(x));
            }
            else
            {
                MessageBox.Show("Please select a radio button.");
            }
        }
    }
}
