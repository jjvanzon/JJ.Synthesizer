using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.VectorGraphics.Enums;
using JJ.Framework.VectorGraphics.Helpers;
using JJ.Framework.VectorGraphics.Models.Elements;
using JJ.Framework.VectorGraphics.Models.Styling;
using JJ.Presentation.Synthesizer.Helpers;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Presentation.Synthesizer.VectorGraphics.Helpers
{
	public static class StyleHelper
	{
		private const string DEFAULT_FONT_NAME = "Verdana";

		private const float DEFAULT_FONT_SIZE = 10.5f;
		private const float SMALLER_FONT_SIZE = 9f;
		private const float TOOL_TIP_FONT_SIZE = 8f;

		public const float SPACING = 8f;
		public const float SPACING_TIMES_2 = SPACING * 2;
		public const float SPACING_SMALL = 4f;
		public const float SPACING_SMALL_TIMES_2 = SPACING_SMALL * 2;

		public const float DEFAULT_OBJECT_SIZE = 40f;
		public const float SMALLER_OBJECT_SIZE = 30f;
		public const float ROW_HEIGHT = 24;
		// ReSharper disable once UnusedMember.Global
		public const float ROW_HEIGHT_SMALL = 16;
		public const float PICTURE_BUTTON_SIZE = 24;
		public const float PICTURE_BUTTON_MARGIN = 4;
		public const float PICTURE_BUTTON_PICTURE_SIZE = 16;
		public const int DRAG_DROP_LINE_ZINDEX = 100;

		/// <summary>
		/// Tells us how much an inlet or outlet's clickable rectangle
		/// should overflow the operator rectangle's boundaries.
		/// </summary>
		public const float INLET_OUTLET_RECTANGLE_HEIGHT_OVERFLOW_IN_PIXELS = 10;

		public const float MINIMUM_INLET_OR_OUTLET_WIDTH_IN_PIXELS = 12;

	    public static int AlmostBlack { get; } = ColorHelper.GetColor(20, 20, 20);
	    public static int DarkGray { get; } = ColorHelper.GetColor(80, 80, 80);
		public static int MediumGray { get; } = ColorHelper.GetColor(120, 120, 120);
		public static int MediumLightGray { get; } = ColorHelper.GetColor(160, 160, 160);
		public static int LightGray { get; } = ColorHelper.GetColor(200, 200, 200);
		public static int LighterGray { get; } = ColorHelper.GetColor(220, 220, 220);
		public static int TransparentGray { get; } = ColorHelper.GetColor(128, 45, 45, 45);
		public static int Blue { get; } = ColorHelper.GetColor(0xFF99C9F7);
		public static int DarkerBlue { get; } = ColorHelper.SetBrightness(Blue, 0.8);
        public static int Orange { get; } = ColorHelper.GetColor(0xFFDB8E00);

		public static Font DefaultFont { get; } = new Font
		{
			Name = DEFAULT_FONT_NAME,
			Size = DEFAULT_FONT_SIZE
		};

		// ReSharper disable once MemberCanBePrivate.Global
		public static Font TitleFont { get; } = new Font
		{
			Name = DEFAULT_FONT_NAME,
			Size = DEFAULT_FONT_SIZE,
			Bold = true
		};

		public static Font NumberOperatorFont { get; } = new Font
		{
			Name = DEFAULT_FONT_NAME,
			Size = SMALLER_FONT_SIZE
		};

		// ReSharper disable once MemberCanBePrivate.Global
		public static Font DimensionFont { get; } = new Font
		{
			Name = DEFAULT_FONT_NAME,
			Size = SMALLER_FONT_SIZE
		};

		// ReSharper disable once MemberCanBePrivate.Global
		public static Font WaterMarkFont { get; } = new Font
		{
			Name = DEFAULT_FONT_NAME,
			Size = 20,
			Bold = true
		};

		public static TextStyle DefaultTextStyle { get; } = new TextStyle
		{
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
			Font = DefaultFont,
			Color = AlmostBlack
		};

		public static TextStyle TitleTextStyle { get; } = new TextStyle
		{
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
			Font = TitleFont,
			Color = ColorHelper.Black,
			Wrap = true
		};

		public static TextStyle NumberOperatorTextStyle { get; } = new TextStyle
		{
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
			Font = NumberOperatorFont,
			Color = AlmostBlack
		};

		public static TextStyle DimensionTextStyle { get; } = new TextStyle
		{
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Top,
			Font = DimensionFont,
			Color = DarkGray
		};

		public static TextStyle CenterWaterMarkTextStyle { get; } = new TextStyle
		{
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Center,
			Font = WaterMarkFont,
			Color = LighterGray
		};

		public static TextStyle TopWaterMarkTextStyle { get; } = new TextStyle
		{
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Left,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Top,
			Font = WaterMarkFont,
			Color = MediumLightGray
		};

		public static TextStyle TextStyleSmallerTransparent { get; } = new TextStyle
		{
			Color = ColorHelper.GetColor(128, 0, 0, 0),
			Font = new Font
			{
				Name = DefaultFont.Name,
				Size = DefaultFont.Size / 1.2f
			}
		};

		public static TextStyle MidiMappingTextStyle { get; } = new TextStyle
		{
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Top,
			Font = DimensionFont,
			Color = DarkGray,
			Wrap = true
		};

		public static TextStyle MidiMappingTextStyleInactive { get; } = HalfenOpacity(MidiMappingTextStyle);

		public static BackStyle OperatorBackStyleSelected { get; } = new BackStyle
		{
			Color = Blue
		};

		public static BackStyle CircleBackStyleSelected { get; } = new BackStyle
		{
			Color = Blue
		};

		public static BackStyle BackStyleInvisible { get; } = new BackStyle
		{
			Visible = false
		};

		public static LineStyle OperatorBorderStyle { get; } = new LineStyle
		{
			Width = 1,
			Color = LightGray
		};

		public static LineStyle OperatorBorderStyleSelected { get; } = new LineStyle
		{
			Width = 1,
			Color = LightGray
		};

		public static LineStyle BorderStyleInvisible { get; } = new LineStyle
		{
			Visible = false
		};

		public static LineStyle LineStyleDashed { get; } = new LineStyle
		{
			Width = 2,
			Color = TransparentGray,
			DashStyleEnum = DashStyleEnum.Dotted
		};

		public static LineStyle LineStyle { get; } = new LineStyle
		{
			Width = 1,
			Color = MediumGray
		};

		public static LineStyle LineStyleWarning { get; } = new LineStyle
		{
			Width = 1,
			Color = Orange
		};

		public static LineStyle LineStyleWarningThick { get; } = new LineStyle
		{
			Width = 2,
			Color = Orange
		};

		public static LineStyle LineStyleTransparent { get; } = new LineStyle
		{
			Width = 1,
			Color = TransparentGray
		};

		public static LineStyle LineStyleThick { get; } = new LineStyle
		{
			Width = 2,
			Color = MediumGray
		};

		public static LineStyle LineStyleThickDark { get; } = new LineStyle
		{
			Width = 2,
			Color = AlmostBlack
		};

		public static LineStyle CircleLineStyle { get; } = new LineStyle
		{
			Width = 2,
			Color = MediumLightGray
		};

		public static LineStyle CircleLineStyleSelected { get; } = new LineStyle
		{
			Width = 2,
			Color = MediumGray
		};

		public static LineStyle CircleLineStyleInactive { get; } = HalfenOpacity(CircleLineStyle);

		public static LineStyle CircleLineStyleSelectedInactive { get; } = HalfenOpacity(CircleLineStyleSelected);

		public static PointStyle PointStyle { get; } = new PointStyle
		{
			Color = MediumGray,
			Width = 5
		};

		public static PointStyle PointStyleThick { get; } = new PointStyle
		{
			Color = MediumGray,
			Width = 8
		};

		public static PointStyle PointStyleThickSelected { get; } = new PointStyle
		{
			Color = DarkerBlue,
			Width = 8
		};

		public static PointStyle PointStyleWarning { get; } = new PointStyle
		{
			Color = Orange,
			Width = 5
		};

		public static PointStyle PointStyleInvisible { get; } = new PointStyle
		{
			Visible = false
		};

		public static PointStyle DentPointStyle { get; } = new PointStyle
		{
			Color = MediumLightGray,
			Width = 5
		};

		public static PointStyle DentPointStyleSelected { get; } = new PointStyle
		{
			Width = 5,
			Color = MediumGray
		};

		public static PointStyle DentPointStyleInactive { get; } = HalfenOpacity(DentPointStyle);

		public static PointStyle DentPointStyleSelectedInactive { get; } = HalfenOpacity(DentPointStyleSelected);

		public static BackStyle ToolTipBackStyle { get; } = new BackStyle
		{
			Color = ColorHelper.GetColor(0xDDFEFEFE)
		};

		public static LineStyle ToolTipLineStyle { get; } = new LineStyle
		{
			Color = ColorHelper.GetColor(0xFF575757)
		};

		public static TextStyle ToolTipTextStyle { get; } = new TextStyle
		{
			Color = ColorHelper.GetColor(0xFF575757),
			Font = new Font
			{
				Name = DEFAULT_FONT_NAME,
				Size = TOOL_TIP_FONT_SIZE
			},
			HorizontalAlignmentEnum = HorizontalAlignmentEnum.Center,
			VerticalAlignmentEnum = VerticalAlignmentEnum.Center
		};

		public static PictureStyle IconPictureStyle { get; } = new PictureStyle
		{
			Scale = true
		};

		public static PictureStyle IconPictureStyleLight { get; } = new PictureStyle
		{
			Scale = true,
			Opacity = 0.6f
		};

		private static readonly Dictionary<StyleGradeEnum, BackStyle> _gradedBackStyleDictionary = Create_StyleGradeEnum_BackStyle_Dictionary();

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

				int gradedColor = ColorHelper.SetBrightness(LighterGray, grade);
				var gradedBackStyle = new BackStyle { Color = gradedColor };

				StyleGradeEnum styleGradeEnum = (StyleGradeEnum)i + 1;

				dictionary.Add(styleGradeEnum, gradedBackStyle);
			}

			return dictionary;
		}

		public static BackStyle GetGradedBackStyle(StyleGradeEnum styleGradeEnum) => _gradedBackStyleDictionary[styleGradeEnum];

	    // Order-Dependence: Do after _gradedBackStyleDictionary declaration
		// ReSharper disable once MemberCanBePrivate.Global
		public static BackStyle NeutralBackStyle { get; } = GetGradedBackStyle(StyleGradeEnum.StyleGradeNeutral);

		public static BackStyle CircleBackStyle { get; } = NeutralBackStyle;

		public static BackStyle CircleBackStyleInactive { get; } = HalfenOpacity(CircleBackStyle);

		public static BackStyle CircleBackStyleSelectedInactive { get; } = HalfenOpacity(CircleBackStyleSelected);

		public static RectangleStyle RectangleStyleInvisible { get; } = new RectangleStyle
		{
			BackStyle = BackStyleInvisible,
			LineStyle = BorderStyleInvisible
		};

		public static RectangleStyle RectangleStyleNeutralGrayBack { get; } = new RectangleStyle
		{
			BackStyle = NeutralBackStyle,
			LineStyle = BorderStyleInvisible
		};

		private static PointStyle HalfenOpacity(PointStyle sourceStyle)
		{
			PointStyle destStyle = sourceStyle.Clone();
			destStyle.Color = ColorHelper.SetOpacity(destStyle.Color, ColorHelper.GetOpacity(destStyle.Color) / 2);
			return destStyle;
		}

		private static LineStyle HalfenOpacity(LineStyle sourceStyle)
		{
			LineStyle destStyle = sourceStyle.Clone();
			destStyle.Color = ColorHelper.SetOpacity(destStyle.Color, ColorHelper.GetOpacity(destStyle.Color) / 2);
			return destStyle;
		}

		private static TextStyle HalfenOpacity(TextStyle sourceStyle)
		{
			TextStyle destStyle = sourceStyle.Clone();
			destStyle.Color = ColorHelper.SetOpacity(destStyle.Color, ColorHelper.GetOpacity(destStyle.Color) / 2);
			return destStyle;
		}

		private static BackStyle HalfenOpacity(BackStyle sourceStyle)
		{
			BackStyle destStyle = sourceStyle.Clone();
			destStyle.Color = ColorHelper.SetOpacity(destStyle.Color, ColorHelper.GetOpacity(destStyle.Color) / 2);
			return destStyle;
		}
	}
}