using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class StyleHelper
    {
        public static Font DefaultFont { get; } = new Font("Verdana", 12);
        public static int DefaultSpacing { get; } = 4;

        // TODO: It uses all labels, not just the labels in column 0.

        /// <summary>
        /// Sets the first column of the TableLayoutPanel to accommodate the width of all its Labels' Texts.
        /// </summary>
        public static void SetPropertyLabelColumnSize(TableLayoutPanel tableLayoutPanel)
        {
            if (tableLayoutPanel == null) throw new NullException(() => tableLayoutPanel);

            const int LABEL_COLUMN_INDEX = 0;

            UserControl userControl = GetAncestorUserControl(tableLayoutPanel);
            IList<Label> labels = GetAllDescendantLabels(tableLayoutPanel);

            // Include only labels in column 0.
            labels = labels.Where(x => tableLayoutPanel.GetColumn(x) == LABEL_COLUMN_INDEX).ToArray(); 

            Graphics graphics = userControl.CreateGraphics();

            float labelColumnWidth = labels.Max(x => graphics.MeasureString(x.Text, x.Font).Width);

            // Use Font scaling.
            // UserControl.AutoScaleFactor.Width does not work,
            // not only because it is protected,
            // but because WinForms does not make AutoScaleFactor.Width > 1
            // when the font is changed in the form.
            float fontScalingCorrectionFactor = 0.9f; // Completely arbitrary experimentally obtained factor.
            float autoScaleFactor = userControl.Font.Size / userControl.ParentForm.Font.Size * fontScalingCorrectionFactor;
            labelColumnWidth *= autoScaleFactor;

            tableLayoutPanel.ColumnStyles[LABEL_COLUMN_INDEX].Width = labelColumnWidth;
        }

        private static UserControl GetAncestorUserControl(Control control)
        {
            if (control == null) throw new NullException(() => control);

            Control ancestor = control.Parent;
            while (ancestor != null)
            {
                var ancestorUserControl = ancestor as UserControl;
                if (ancestorUserControl != null)
                {
                    return ancestorUserControl;
                }

                ancestor = ancestor.Parent;
            }

            throw new Exception(String.Format("No ancestor UserControl found for Control '{0}'.", control.Name));
        }

        /// <summary>
        /// Gets all descendent labels of the UserControl.
        /// </summary>
        private static IList<Label> GetAllDescendantLabels(Control userControl)
        {
            IList<Label> labels = userControl.Controls.Cast<Control>()
                                             .SelectRecursive(x => x.Controls.Cast<Control>())
                                             .OfType<Label>()
                                             .ToArray();
            return labels;
        }
    }
}