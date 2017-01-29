using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Framework.Presentation.WinForms.Forms;
using JJ.Framework.Exceptions;

namespace JJ.OneOff.Synthesizer.DataMigration
{
    public partial class MainForm : SimpleFileProcessForm
    {
        private class MethodTuple
        {
            public MethodInfo Method { get; set; }
            public RadioButton RadioButton { get; set; }
        }

        private const int TOP_RADIO_BUTTON_Y = 123;
        private const int SPACING = 4;

        private readonly IList<MethodTuple> _methodTuples;

        public MainForm()
        {
            InitializeComponent();

            IList<MethodInfo> methods = typeof(DataMigrationExecutor).GetMethods(BindingFlags.Public | BindingFlags.Static);

            IList<MethodTuple> methodTuples = new List<MethodTuple>(methods.Count);

            int y = TOP_RADIO_BUTTON_Y;
            foreach (MethodInfo method in methods)
            {
                RadioButton radioButton = CreateRadioButton(method.Name);
                radioButton.Location = new Point(radioButton.Location.X, y);

                Controls.Add(radioButton);
                Controls.SetChildIndex(radioButton, 0);

                y += radioButton.Height;
                y += SPACING;

                methodTuples.Add(new MethodTuple
                {
                    Method = method,
                    RadioButton = radioButton
                });
            }

            if (methodTuples.Count == 0)
            {
                throw new ZeroException(() => methodTuples.Count);
            }

            methodTuples.Last().RadioButton.Checked = true;

            this.AutomaticallyAssignTabIndexes();

            _methodTuples = methodTuples;
        }

        private RadioButton CreateRadioButton(string methodName)
        {
            var radioButton = new RadioButton
            {
                AutoSize = true,
                Enabled = true,
                Font = new Font("Microsoft Sans Serif", 8F),
                Location = new Point(263, 123),
                Margin = new Padding(4),
                Name = "radioButton" + methodName,
                Size = new Size(191, 18),
                Text = methodName,
                UseVisualStyleBackColor = true,
            };

            return radioButton;
        }

        private void MainForm_OnRunProcess(object sender, EventArgs e)
        {
            MethodTuple methodTuple = _methodTuples.Where(x => x.RadioButton.Checked).SingleOrDefault();

            if (methodTuple != null)
            {
                // DIRTY: Assumption that all public methods of DataMigrationExecutor take a delegate of this kind.
                Action<string> showProgressDelegate = x => ShowProgress(x);
                methodTuple.Method.Invoke(null, new object[] { showProgressDelegate });
                return;
            }

            MessageBox.Show("Please select a radio button.");
        }
    }
}