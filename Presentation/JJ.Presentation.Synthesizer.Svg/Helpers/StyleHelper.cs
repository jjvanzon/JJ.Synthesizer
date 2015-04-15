using JJ.Framework.Presentation.Svg.Enums;
using JJ.Framework.Presentation.Svg.Helpers;
using JJ.Framework.Presentation.Svg.Models.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Svg.Helpers
{
    internal static class StyleHelper
    {
        public const float DEFAULT_WIDTH = 85; // 125;
        public const float DEFAULT_HEIGHT = 40; // 60;

        public static Font DefaultFont;
        public static TextStyle TextStyle;
        public static BackStyle BackStyle;
        public static BackStyle BackStyleSelected;
        public static BackStyle BackStyleInvisible;
        public static LineStyle LineStyle;
        public static LineStyle LineStyleDashed;
        public static LineStyle LineStyleSelected;
        public static LineStyle LineStyleInvisible;
        public static LineStyle LineStyleThin;
        public static PointStyle PointStyle;
        public static PointStyle PointStyleInvisible;

        public static BackStyle ToolTipBackStyle;
        public static LineStyle ToolTipLineStyle;
        public static TextStyle ToolTipTextStyle;

        static StyleHelper()
        {
            PointStyle = new PointStyle
            {
                Color = ColorHelper.GetColor(45, 45, 45),
                Width = 6
            };

            BackStyle = new BackStyle
            {
                Color = ColorHelper.GetColor(220, 220, 220)
            };

            BackStyleSelected = new BackStyle
            {
                Color = ColorHelper.GetColor(122, 189, 254)
            };

            LineStyle = new LineStyle
            {
                Width = 1,
                Color = ColorHelper.GetColor(45, 45, 45)
            };

            LineStyleThin = new LineStyle
            {
                Width = 1,
                Color = ColorHelper.GetColor(45, 45, 45)
            };

            LineStyleSelected = new LineStyle
            {
                Width = 1,
                Color = ColorHelper.GetColor(0, 0, 0)
            };

            LineStyleDashed = new LineStyle
            {
                Width = 3,
                Color = ColorHelper.GetColor(128, 45, 45, 45),
                DashStyleEnum = DashStyleEnum.Dotted
            };

            DefaultFont = new Font
            {
                Bold = true,
                Name = "Microsoft Sans Serif",
                Size = 11,
            };

            TextStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
                Font = DefaultFont,
                Color = ColorHelper.GetColor(20, 20, 20)
            };

            PointStyleInvisible = new PointStyle
            {
                Visible = false,
            };

            BackStyleInvisible = new BackStyle
            {
                Visible = false
            };

            LineStyleInvisible = new LineStyle
            {
                Visible = false
            };

            // Tool Tip

            ToolTipBackStyle = new BackStyle
            {
                Color = ColorHelper.GetColor(0xDDFEFEFE)
            };

            ToolTipTextStyle = new TextStyle
            {
                Color = ColorHelper.GetColor(0xFF575757),
                Font = new Font
                {
                    Name = "Microsoft Sans Serif",
                    Size = 9,
                },
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center
            };

            ToolTipLineStyle = new LineStyle
            {
                Color = ColorHelper.GetColor(0xFF575757)
            };
        }

        public static void MakeHiddenStylesVisible()
        {
            PointStyleInvisible.Visible = true;
            PointStyleInvisible.Color = ColorHelper.GetColor(128, 40, 128, 192);
            PointStyleInvisible.Width = 10;

            BackStyleInvisible.Visible = true;
            BackStyleInvisible.Color = ColorHelper.GetColor(64, 40, 128, 192);

            LineStyleInvisible.Visible = true;
            LineStyleInvisible.Color = ColorHelper.GetColor(128, 40, 128, 192);
            LineStyleInvisible.Width = 2;
            LineStyleInvisible.DashStyleEnum = DashStyleEnum.Dotted;
        }
    }
}
