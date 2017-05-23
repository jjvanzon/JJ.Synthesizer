using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Presentation.VectorGraphics.Enums;
using JJ.Framework.Presentation.VectorGraphics.Helpers;
using JJ.Framework.Presentation.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
    internal static class StyleHelper
    {
        private const float TOOL_TIP_FONT_SIZE = 8f;
        private const float OPERATOR_FONT_SIZE = 10.5f;
        private const float NUMBER_OPERATOR_FONT_SIZE = 9f;
        private const float DIMENSION_FONT_SIZE = 9f;

        public const float OPERATOR_HEIGHT = 40f;
        public const float OPERATOR_MINIMUM_WIDTH = 40f;
        public const float NUMBER_OPERATOR_HEIGHT = 30f;
        public const float NUMBER_OPERATOR_MINIMUM_WIDTH = 30f;
        public const int DRAG_DROP_LINE_ZINDEX = 100;
        public const float DEFAULT_SPACING = 4f;

        /// <summary>
        /// Tells us how much an inlet or outlet's clickable rectangle
        /// should overflow the operator rectangle's boundaries.
        /// </summary>
        public const float INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS = 10;
        public const float MINIMUM_INLET_OR_OUTLET_WIDTH_IN_PIXELS = 12;

        public static Font DefaultFont { get; }
        public static Font NumberOperatorFont { get; }
        public static Font DimensionFont { get; }
        public static Font WaterMarkFont { get; }
        public static TextStyle DefaultTextStyle { get; }
        public static TextStyle NumberOperatorTextStyle { get; }
        public static TextStyle DimensionTextStyle { get; }
        public static TextStyle WaterMarkTextStyle { get; set; }
        public static BackStyle BackStyle { get; }
        public static BackStyle BackStyleSelected { get; }
        public static BackStyle BackStyleInvisible { get; }
        public static LineStyle BorderStyle { get; }
        public static LineStyle BorderStyleSelected { get; }
        public static LineStyle BorderStyleInvisible { get; }
        public static LineStyle LineStyleDashed { get; }
        public static LineStyle LineStyle { get; }
        public static LineStyle LineStyleWarning { get; }
        public static LineStyle LineStyleTransparent { get; }
        public static LineStyle LineStyleThick { get; }
        public static PointStyle PointStyle { get; }
        public static PointStyle PointStyleWarning { get; }
        public static PointStyle PointStyleThick { get; }
        public static PointStyle PointStyleThickSelected { get; }
        public static PointStyle PointStyleInvisible { get; }

        public static BackStyle ToolTipBackStyle { get; }
        public static LineStyle ToolTipLineStyle { get; }
        public static TextStyle ToolTipTextStyle { get; }

        public static float Spacing { get; }
        public static float SpacingTimes2 { get; }

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
            int orange = ColorHelper.GetColor(0xFFDB8E00);

            PointStyle = new PointStyle
            {
                Color = mediumGrey,
                Width = 5
            };

            PointStyleThick = new PointStyle
            {
                Color = mediumGrey,
                Width = 8
            };

            PointStyleThickSelected = new PointStyle
            {
                Color = darkerBlue,
                Width = 8
            };

            PointStyleWarning = PointStyle.Clone();
            PointStyleWarning.Color = orange;

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

            LineStyleWarning = new LineStyle
            {
                Width = 1,
                Color = orange
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
                Size = OPERATOR_FONT_SIZE
            };

            NumberOperatorFont = new Font
            {
                Name = "Verdana",
                Size = NUMBER_OPERATOR_FONT_SIZE
            };

            DimensionFont = new Font
            {
                Name = "Verdana",
                Size = DIMENSION_FONT_SIZE
            };

            WaterMarkFont = new Font
            {
                Name = "Verdana",
                Size = 20,
                Bold = true
            };

            DefaultTextStyle = new TextStyle
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

            DimensionTextStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Top,
                Font = DimensionFont,
                Color = almostBlack
            };

            WaterMarkTextStyle = new TextStyle
            {
                HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left,
                VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
                Font = WaterMarkFont,
                Color = lightGrey
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

            _styleGradeEnum_BackStyle_Dictionary = Create_StyleGradeEnum_BackStyle_Dictionary();
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

        private static readonly Dictionary<StyleGradeEnum, BackStyle> _styleGradeEnum_BackStyle_Dictionary;

        private static Dictionary<StyleGradeEnum, BackStyle> Create_StyleGradeEnum_BackStyle_Dictionary()
        {
            var dictionary = new Dictionary<StyleGradeEnum, BackStyle>();

            int gradeCount = (int)EnumHelper.GetValues<StyleGradeEnum>().Max();

            const double minBrightness = 0.8;
            const double maxBrightness = 1.075;
            const double deltaBrightness = maxBrightness - minBrightness;

            for (int i = 0; i < gradeCount; i++)
            {
                double ratio = (double)i / (gradeCount - 1);
                double grade = minBrightness + ratio * deltaBrightness;

                int gradedColor = ColorHelper.SetBrightness(BackStyle.Color, grade);
                var gradedBackStyle = new BackStyle { Color = gradedColor };

                StyleGradeEnum styleGradeEnum = (StyleGradeEnum)i + 1;

                dictionary.Add(styleGradeEnum, gradedBackStyle);
            }

            return dictionary;
        }

        public static BackStyle GetGradedBackStyle(StyleGradeEnum styleGradeEnum)
        {
            return _styleGradeEnum_BackStyle_Dictionary[styleGradeEnum];
        }
    }
}
