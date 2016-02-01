using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    internal static class StyleHelper
    {
        public const float DEFAULT_WIDTH = 85; // 125;
        public const float DEFAULT_HEIGHT = 40; // 60;
        public const float MINIMUM_OPERATOR_WIDTH = 40;

        public const int DRAG_DROP_LINE_ZINDEX = 100;

        public static Font DefaultFont { get; set; }
        public static TextStyle TextStyle { get; set; }
        public static BackStyle BackStyle { get; set; }
        public static BackStyle BackStyleSelected { get; set; }
        public static BackStyle BackStyleInvisible { get; set; }
        public static LineStyle BorderStyle { get; set; }
        public static LineStyle BorderStyleSelected { get; set; }
        public static LineStyle BorderStyleInvisible { get; set; }
        public static LineStyle LineStyleDashed { get; set; }
        public static LineStyle LineStyle { get; set; }
        public static LineStyle LineStyleTransparent { get; set; }
        public static LineStyle LineStyleThick { get; set; }
        public static PointStyle PointStyle { get; set; }
        public static PointStyle PointStyleThick { get; set; }
        public static PointStyle PointStyleThickSelected { get; internal set; }
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

            int almostBlack = ColorHelper.GetColor(20, 20, 20);
            int mediumGrey = ColorHelper.GetColor(120, 120, 120);
            int lightGrey = ColorHelper.GetColor(220, 220, 220);
            int lessLightGrey = ColorHelper.GetColor(200, 200, 200);
            int transparentGrey = ColorHelper.GetColor(128, 45, 45, 45);
            int blue = ColorHelper.GetColor(122, 189, 254);
            int darkerBlue = ColorHelper.SetBrightness(blue, 0.8);

            PointStyle = new PointStyle
            {
                Color = mediumGrey,
                Width = 5
            };

            PointStyleThick = new PointStyle
            {
                Color = mediumGrey,
                Width = 10
            };

            PointStyleThickSelected = new PointStyle
            {
                Color = darkerBlue,
                Width = 10
            };

            BackStyle = new BackStyle
            {
                Color = lightGrey
            };

            BackStyleSelected = new BackStyle
            {
                Color = blue
            };

            BorderStyle = new LineStyle
            {
                Width = 1,
                Color = lessLightGrey
            };

            BorderStyleSelected = new LineStyle
            {
                Width = 1,
                Color = lessLightGrey
            };

            LineStyle = new LineStyle
            {
                Width = 1,
                Color = mediumGrey
            };

            LineStyleTransparent = new LineStyle
            {
                Width = 1,
                Color = transparentGrey
            };

            LineStyleThick = new LineStyle
            {
                Width = 2,
                Color = mediumGrey
            };

            LineStyleDashed = new LineStyle
            {
                Width = 2,
                Color = transparentGrey,
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
                Color = almostBlack
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

        public static TextStyle CreateTextStyleSmallerTransparent()
        {
            var textStyle = new TextStyle
            {
                Color = ColorHelper.GetColor(128, 0, 0, 0),
                Font = new Font
                {
                    Name = DefaultFont.Name,
                    Size = DefaultFont.Size / 1.2f
                }
            };
            return textStyle;
        }

        public static void MakeHiddenStylesVisible()
        {
            PointStyleInvisible.Visible = true;
            PointStyleInvisible.Color = ColorHelper.GetColor(128, 40, 128, 192);
            PointStyleInvisible.Width = 5;

            BackStyleInvisible.Visible = true;
            BackStyleInvisible.Color = ColorHelper.GetColor(64, 40, 128, 192);

            BorderStyleInvisible.Visible = true;
            BorderStyleInvisible.Color = ColorHelper.GetColor(128, 40, 128, 192);
            BorderStyleInvisible.Width = 2;
            BorderStyleInvisible.DashStyleEnum = DashStyleEnum.Dotted;
        }
    }
}
