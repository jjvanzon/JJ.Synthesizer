using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Reflection;
using JJ.Framework.WinForms.Helpers;
using VectorGraphicsStyleHelper = JJ.Presentation.Synthesizer.VectorGraphics.Helpers.StyleHelper;

// ReSharper disable UnusedMember.Global

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal static class StyleHelper
    {
        private const int LABEL_COLUMN_INDEX = 0;

        public static Font DefaultFont { get; } = new Font("Verdana", 10);
        public static int DefaultSpacing { get; } = 4;
        public static int SplitterWidth { get; } = 6;
        public static int IconButtonSize { get; } = 24;
        public static int ButtonHeight { get; } = 32;

        public static Color AlmostBlack { get; } = GetColor();
        public static Color DarkGray { get; } = GetColor();
        public static Color MediumGray { get; } = GetColor();
        public static Color MediumLightGray { get; } = GetColor();
        public static Color LightGray { get; } = GetColor();
        public static Color LighterGray { get; } = GetColor();
        public static Color TransparentGray { get; } = GetColor();

        public static int TitleBarHeight { get; } = IconButtonSize + DefaultSpacing + DefaultSpacing;

        private static Color GetColor([CallerMemberName] string callerMemberName = null)
        {
            if (string.IsNullOrEmpty(callerMemberName)) throw new NullOrEmptyException(nameof(callerMemberName));
            PropertyInfo property = typeof(VectorGraphicsStyleHelper).GetPropertyOrException(callerMemberName);
            var colorInt = (int)property.GetValue();
            return Color.FromArgb(colorInt);
        }

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

            var labelColumnWidth = 1f;

            if (labels.Count > 0)
            {
                labelColumnWidth = labels.Max(x => graphics.MeasureString(x.Text, x.Font).Width);
            }

            // Use Font scaling.
            // UserControl.AutoScaleFactor.Width does not work,
            // not only because it is protected,
            // but because WinForms does not make AutoScaleFactor.Width > 1
            // when the font is changed in the form.
            if (userControl.ParentForm != null)
            {
                // HACK: Detection of LicenseUsageMode.Runtime earlier on 
                // does not seem to work return the correct value anymore.
                // That is why I ignore the null here, instead of throwing an exception.

                const float fontScalingCorrectionFactor = 0.9f; // Completely arbitrary experimentally obtained factor.
                float autoScaleFactor = userControl.Font.Size / userControl.ParentForm.Font.Size * fontScalingCorrectionFactor;
                labelColumnWidth *= autoScaleFactor;
            }

            tableLayoutPanel.ColumnStyles[LABEL_COLUMN_INDEX].Width = labelColumnWidth;
        }
    }
}