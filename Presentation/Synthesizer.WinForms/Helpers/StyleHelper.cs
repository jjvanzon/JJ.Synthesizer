using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms.Helpers;
using System.ComponentModel;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class StyleHelper
    {
        private const int LABEL_COLUMN_INDEX = 0;

        public static Font DefaultFont { get; } = new Font("Verdana", 12);
        public static int DefaultSpacing { get; } = 4;
        public static int IconButtonSize { get; } = 24;
        public static int DefaultMargin { get; } = 2;
        public static int ButtonHeight { get; } = 32;

        /// <summary>
        /// Sets the first column of the TableLayoutPanel to accommodate the width of all its Labels' Texts.
        /// </summary>
        public static void SetPropertyLabelColumnSize(TableLayoutPanel tableLayoutPanel)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
            {
                return;
            }

            if (tableLayoutPanel == null) throw new NullException(() => tableLayoutPanel);

            UserControl userControl = ControlHelper.GetAncestorUserControl(tableLayoutPanel);
            IList<Label> labels = ControlHelper.GetDescendantsOfType<Label>(tableLayoutPanel);

            // Include only labels in column 0.
            labels = labels.Where(x => tableLayoutPanel.GetColumn(x) == LABEL_COLUMN_INDEX).ToArray(); 

            Graphics graphics = userControl.CreateGraphics();

            float labelColumnWidth = 1f;

            if (labels.Count > 0)
            {
                labelColumnWidth = labels.Max(x => graphics.MeasureString(x.Text, x.Font).Width);
            }

            // Use Font scaling.
            // UserControl.AutoScaleFactor.Width does not work,
            // not only because it is protected,
            // but because WinForms does not make AutoScaleFactor.Width > 1
            // when the font is changed in the form.
            const float fontScalingCorrectionFactor = 0.9f; // Completely arbitrary experimentally obtained factor.
            if (userControl.ParentForm == null)
            {
                throw new NullException(() => userControl.ParentForm);
            }
            float autoScaleFactor = userControl.Font.Size / userControl.ParentForm.Font.Size * fontScalingCorrectionFactor;
            labelColumnWidth *= autoScaleFactor;

            tableLayoutPanel.ColumnStyles[LABEL_COLUMN_INDEX].Width = labelColumnWidth;
        }
    }
}