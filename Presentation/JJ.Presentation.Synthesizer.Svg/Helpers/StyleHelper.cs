using JJ.Framework.Presentation.Svg.Enums;
using JJ.Framework.Presentation.Svg.Helpers;
using JJ.Framework.Presentation.Svg.Models.Styling;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.Svg.Helpers
{
    internal static class StyleHelper
    {
        public const float DEFAULT_WIDTH = 85; // 125;
        public const float DEFAULT_HEIGHT = 40; // 60;

        public static Font DefaultFont { get; set; }
        public static TextStyle TextStyle { get; set; }
        public static BackStyle BackStyle { get; set; }
        public static BackStyle BackStyleSelected { get; set; }
        public static BackStyle BackStyleInvisible { get; set; }
        public static LineStyle BorderStyle { get; set; }
        public static LineStyle BorderStyleSelected { get; set; }
        public static LineStyle BorderStyleInvisible { get; set; }
        public static LineStyle LineStyleDashed { get; set; }
        public static LineStyle LineStyleThin { get; set; }
        public static PointStyle PointStyle { get; set; }
        public static PointStyle PointStyleInvisible { get; set; }

        public static BackStyle ToolTipBackStyle { get; set; }
        public static LineStyle ToolTipLineStyle { get; set; }
        public static TextStyle ToolTipTextStyle { get; set; }

        public static float Spacing { get; set; }
        public static float SpacingTimes2 { get; set; }

        static StyleHelper()
        {
            Spacing = 8;

            SpacingTimes2 = Spacing + Spacing;

            PointStyle = new PointStyle
            {
                Color = ColorHelper.GetColor(120, 120, 120),
                Width = 5
            };

            BackStyle = new BackStyle
            {
                Color = ColorHelper.GetColor(220, 220, 220)
            };

            BackStyleSelected = new BackStyle
            {
                Color = ColorHelper.GetColor(122, 189, 254)
            };

            BorderStyle = new LineStyle
            {
                Width = 1,
                Color = ColorHelper.GetColor(200, 200, 200)
            };

            BorderStyleSelected = new LineStyle
            {
                Width = 1,
                Color = ColorHelper.GetColor(200, 200, 200)
            };

            LineStyleThin = new LineStyle
            {
                Width = 1,
                Color = ColorHelper.GetColor(120, 120, 120)
            };

            LineStyleDashed = new LineStyle
            {
                Width = 2,
                Color = ColorHelper.GetColor(128, 45, 45, 45),
                DashStyleEnum = DashStyleEnum.Dotted
            };

            DefaultFont = new Font
            {
                Name = "Verdana",
                Size = 10.5f,
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
                Visible = false
            };

            BackStyleInvisible = new BackStyle
            {
                Visible = false
            };

            BorderStyleInvisible = new LineStyle
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

            BorderStyleInvisible.Visible = true;
            BorderStyleInvisible.Color = ColorHelper.GetColor(128, 40, 128, 192);
            BorderStyleInvisible.Width = 2;
            BorderStyleInvisible.DashStyleEnum = DashStyleEnum.Dotted;
        }
    }
}
