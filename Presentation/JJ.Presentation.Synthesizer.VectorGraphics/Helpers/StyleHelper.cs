using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    internal static class StyleHelper
    {
        private const float TOOL_TIP_FONT_SIZE = 8f;
        private const float OPERATOR_FONT_SIZE = 10.5f;
        private const float NUMBER_OPERATOR_FONT_SIZE = 9f;

        public const float OPERATOR_HEIGHT = 40f;
        public const float OPERATOR_MINIMUM_WIDTH = 40f;
        public const float NUMBER_OPERATOR_HEIGHT = 30f;
        public const float NUMBER_OPERATOR_MINIMUM_WIDTH = 30f;
        public const int DRAG_DROP_LINE_ZINDEX = 100;

        /// <summary>
        /// Tells us how much an inlet or outlet's clickable rectangle
        /// should overflow the operator rectangle's boundaries.
        /// </summary>
        public const float INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS = 10;
        public const float MINIMUM_INLET_OR_OUTLET_WIDTH_IN_PIXELS = 12;

        public static Font DefaultFont { get; private set; }
        public static Font NumberOperatorFont { get; private set; }
        public static TextStyle TextStyle { get; private set; }
        public static TextStyle NumberOperatorTextStyle { get; private set; }
        public static BackStyle BackStyle { get; private set; }
        public static BackStyle BackStyleSelected { get; private set; }
        public static BackStyle BackStyleInvisible { get; private set; }
        public static LineStyle BorderStyle { get; private set; }
        public static LineStyle BorderStyleSelected { get; private set; }
        public static LineStyle BorderStyleInvisible { get; private set; }
        public static LineStyle LineStyleDashed { get; private set; }
        public static LineStyle LineStyle { get; private set; }
        public static LineStyle LineStyleTransparent { get; private set; }
        public static LineStyle LineStyleThick { get; private set; }
        public static PointStyle PointStyle { get; private set; }
        public static PointStyle PointStyleThick { get; private set; }
        public static PointStyle PointStyleThickSelected { get; private set; }
        public static PointStyle PointStyleInvisible { get; private set; }

        public static BackStyle ToolTipBackStyle { get; private set; }
        public static LineStyle ToolTipLineStyle { get; private set; }
        public static TextStyle ToolTipTextStyle { get; private set; }

        public static float Spacing { get; private set; }
        public static float SpacingTimes2 { get; private set; }

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
                Size = OPERATOR_FONT_SIZE,
            };

            NumberOperatorFont = new Font
            {
                Name = "Verdana",
                Size = NUMBER_OPERATOR_FONT_SIZE,
            };

            TextStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
                Font = DefaultFont,
                Color = almostBlack
            };

            NumberOperatorTextStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
                Font = NumberOperatorFont,
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
                    Name = "Verdana", //"Microsoft Sans Serif",
                    Size = TOOL_TIP_FONT_SIZE,
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
