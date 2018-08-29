using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Mathematics;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;
// ReSharper disable MemberCanBePrivate.Global

// ReSharper disable RedundantCaseLabel

namespace JJ.Presentation.Synthesizer.ToViewModel
{
	internal static partial class ToViewModelHelper
	{
		private const string DIMENSION_KEY_EMPTY = "";
		private const string STANDARD_DIMENSION_KEY_PREFIX = "0C26ADA8-0BFC-484C-BF80-774D055DAA3F-StandardDimension-";
		public const string CUSTOM_DIMENSION_KEY_PREFIX = "5133584A-BA76-42DB-BD0E-42801FCB96DF-CustomDimension-";

		private static readonly bool _idsVisible = CustomConfigurationManager.GetSection<ConfigurationSection>().IDsVisible;

		// Dimensions

		public static string GetDimensionKey(Operator op)
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			string customDimensionName = op.GetCustomDimensionNameWithFallback();
			if (!string.IsNullOrEmpty(customDimensionName))
			{
				return $"{CUSTOM_DIMENSION_KEY_PREFIX}{customDimensionName}";
			}

			return GetDimensionKey(op.GetStandardDimensionEnumWithFallback());
		}

		public static string GetDimensionKey(DimensionEnum standardDimensionEnum)
		{
			if (standardDimensionEnum != DimensionEnum.Undefined)
			{
				return $"{STANDARD_DIMENSION_KEY_PREFIX}{standardDimensionEnum}";
			}

			return DIMENSION_KEY_EMPTY;
		}

		public static string TryGetDimensionName(Operator op)
		{
			var sb = new StringBuilder();

			string customDimensionName = op.GetCustomDimensionNameWithFallback();
			DimensionEnum standardDimensionEnum = op.GetStandardDimensionEnumWithFallback();

			if (NameHelper.IsFilledIn(customDimensionName))
			{
				sb.Append(customDimensionName);
			}
			else if (standardDimensionEnum != DimensionEnum.Undefined)
			{
				sb.Append(ResourceFormatter.GetDisplayName(standardDimensionEnum));
			}
			else if (op.DimensionIsInherited())
			{
				sb.Append(ResourceFormatter.Dimension);
			}

			if (op.DimensionIsInherited())
			{
				sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
			}

			return sb.ToString();
		}

		public static bool MustStyleDimension(Operator entity)
		{
			switch (entity.GetOperatorTypeEnum())
			{
				case OperatorTypeEnum.GetPosition:
				case OperatorTypeEnum.SetPosition:
					return false;
			}

			return entity.HasDimension;
		}

		// Document

		public static bool GetCanAddToInstrument(DocumentTreeNodeTypeEnum selectedNodeType, bool documentTreeVisible)
		{
		    if (!documentTreeVisible)
		    {
		        return false;
		    }

		    switch (selectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.LibraryMidiMappingGroup:
				case DocumentTreeNodeTypeEnum.LibraryPatch:
				case DocumentTreeNodeTypeEnum.LibraryScale:
				case DocumentTreeNodeTypeEnum.MidiMappingGroup:
				case DocumentTreeNodeTypeEnum.Patch:
				case DocumentTreeNodeTypeEnum.Scale:
					return true;

				default:
					return false;
			}
		}

		public static bool GetCanCreate(DocumentTreeNodeTypeEnum selectedNodeType, bool documentTreeVisible, bool patchDetailsVisible)
		{
		    if (!documentTreeVisible)
		    {
		        return false;
		    }

		    switch (selectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.Midi:
				case DocumentTreeNodeTypeEnum.Libraries:
				case DocumentTreeNodeTypeEnum.PatchGroup:
				case DocumentTreeNodeTypeEnum.Scales:
					return true;

				case DocumentTreeNodeTypeEnum.LibraryPatch:
				case DocumentTreeNodeTypeEnum.Patch:
					return patchDetailsVisible;

				default:
					return false;
			}
		}

	    public static bool GetCanClone(DocumentTreeNodeTypeEnum selectedNodeType, bool documentTreeVisible)
	    {
	        if (!documentTreeVisible)
	        {
	            return false;
	        }

	        switch (selectedNodeType)
	        {
	            case DocumentTreeNodeTypeEnum.Patch:
	                return true;

	            default:
	                return false;
	        }
	    }

        public static bool GetCanDelete(DocumentTreeNodeTypeEnum selectedNodeType, bool documentTreeVisible)
	    {
	        if (!documentTreeVisible)
	        {
	            return false;
	        }

	        switch (selectedNodeType)
	        {
	            case DocumentTreeNodeTypeEnum.MidiMappingGroup:
	            case DocumentTreeNodeTypeEnum.Library:
	            case DocumentTreeNodeTypeEnum.Patch:
	            case DocumentTreeNodeTypeEnum.Scale:
	                return true;

	            default:
	                return false;
	        }
	    }

        public static bool GetCanPlay(DocumentTreeNodeTypeEnum selectedNodeType, bool documentTreeVisible)
		{
		    if (!documentTreeVisible)
		    {
		        return false;
		    }

		    switch (selectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.AudioOutput:
				case DocumentTreeNodeTypeEnum.Libraries:
				case DocumentTreeNodeTypeEnum.Library:
				case DocumentTreeNodeTypeEnum.LibraryPatch:
				case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
				case DocumentTreeNodeTypeEnum.Patch:
				case DocumentTreeNodeTypeEnum.PatchGroup:
					return true;

				default:
					return false;
			}
		}

		public static bool GetCanOpenExternally(DocumentTreeNodeTypeEnum selectedNodeType, bool documentTreeVisible)
		{
		    if (!documentTreeVisible)
		    {
		        return false;
		    }

		    switch (selectedNodeType)
			{
				case DocumentTreeNodeTypeEnum.Library:
				case DocumentTreeNodeTypeEnum.LibraryPatch:
					return true;

				default:
					return false;
			}
		}

		// Inlet

		public static bool GetInletVisible(Inlet inlet)
		{
			if (inlet == null) throw new NullException(() => inlet);

			if (inlet.InputOutlet != null)
			{
				return true;
			}

			Operator op = inlet.Operator;

			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

			switch (operatorTypeEnum)
			{
				case OperatorTypeEnum.PatchInlet:
					return false;

				case OperatorTypeEnum.Squash:
					if (inlet.GetDimensionEnumWithFallback() == DimensionEnum.Origin)
					{
						if (op.GetStandardDimensionEnumWithFallback() == DimensionEnum.Time)
						{
							return false;
						}
					}

					break;
			}

			return true;
		}

		public static string GetInletCaption(Inlet inlet)
		{
			var sb = new StringBuilder();

			if (!inlet.NameOrDimensionHidden)
			{
				// Name or Dimension
				OperatorWrapper wrapper = EntityWrapperFactory.CreateOperatorWrapper(inlet.Operator);
				string inletDisplayName = wrapper.GetInletDisplayName(inlet);
				sb.Append(inletDisplayName);

				// RepetitionPosition
				if (inlet.RepetitionPosition.HasValue)
				{
					sb.Append($" {inlet.RepetitionPosition + 1}");
				}
			}

			// DefaultValue
			if (inlet.InputOutlet == null)
			{
				if (inlet.DefaultValue.HasValue)
				{
					if (sb.Length != 0)
					{
						sb.Append(' ');
					}

					sb.AppendFormat("= {0:0.####}", inlet.DefaultValue.Value);
				}
			}

			// IsObsolete
			if (inlet.IsObsolete)
			{
				AppendObsoleteFlag(sb);
			}

			// ID
			if (_idsVisible)
			{
				sb.Append($" ({inlet.ID})");
			}

			return sb.ToString();
		}

		public static float? TryGetConnectionDistance(Inlet entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (entity.InputOutlet == null)
			{
				return null;
			}

			Operator operator1 = entity.Operator;
			Operator operator2 = entity.InputOutlet.Operator;

			EntityPosition entityPosition1 = operator1.EntityPosition;
			EntityPosition entityPosition2 = operator2.EntityPosition;

			float distance = Geometry.AbsoluteDistance(
				entityPosition1.X,
				entityPosition1.Y,
				entityPosition2.X,
				entityPosition2.Y);

			return distance;
		}

		// MidiMapping

		public static string GetCaption(MidiMapping entity)
		{
			string caption = ValidationHelper.GetUserFriendlyIdentifier(entity);

			if (_idsVisible)
			{
				caption += $" ({entity.ID})";
			}

			// Prevent range strings "[0-10]" from getting broken off and spread over multiple lines, which is ugly.
			caption = caption.Replace(" [", $"{Environment.NewLine}[");

			return caption;
		}

		// MonitoringBar

		public static string Format_MonitoringBar_MidiValue(int value) => $"{value}";

		public static string Format_MonitoringBar_DimensionValue(double value) => $"{MathHelper.RoundToSignificantDigits(value, 3)}";

		public static string Format_MonitoringBar_MidiControllerName(int midiControllerCode)
			=> $"{ResourceFormatter.Controller} {Format_MonitoringBar_MidiValue(midiControllerCode)}";

		public static string Format_MonitoringBar_MidiControllerValue(int absoluteMidiControllerValue, int relativeMidiControllerValue)
		{
			if (absoluteMidiControllerValue == relativeMidiControllerValue)
			{
				string formattedValue = Format_MonitoringBar_MidiValue(absoluteMidiControllerValue);
				return formattedValue;
			}
			else
			{
				string formattedRelativeValue = Format_MonitoringBar_MidiValue(relativeMidiControllerValue);
				string formattedAbsoluteValue = Format_MonitoringBar_MidiValue(absoluteMidiControllerValue);
				string formattedValue = $"{formattedAbsoluteValue} ({formattedRelativeValue})";
				return formattedValue;
			}
		}

		// Node

		public static string GetCaption(Node entity)
		{
			if (entity == null) throw new NullException(() => entity);

			return $"{entity.X:0.####}, {entity.Y:0.####}";
		}

		// Operator

		public static bool GetOperatorIsSmaller(Operator entity) => entity.GetOperatorTypeEnum() == OperatorTypeEnum.Number;

		public static string GetCaption(Operator op)
		{
			if (op == null) throw new NullException(() => op);

			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

			string operatorCaption;

			switch (operatorTypeEnum)
			{
				case OperatorTypeEnum.AverageOverDimension:
				case OperatorTypeEnum.AverageOverInlets:
					operatorCaption = ResourceFormatter.Average;
					break;

				case OperatorTypeEnum.ClosestOverDimension:
				case OperatorTypeEnum.ClosestOverInlets:
					operatorCaption = ResourceFormatter.Closest;
					break;

				case OperatorTypeEnum.ClosestOverDimensionExp:
				case OperatorTypeEnum.ClosestOverInletsExp:
					operatorCaption = ResourceFormatter.ClosestExp;
					break;

				case OperatorTypeEnum.Curve:
					operatorCaption = GetOperatorCaption_ForCurve(op);
					break;

				case OperatorTypeEnum.GetPosition:
					operatorCaption = GetOperatorCaption_ForGetPosition(op);
					break;

				case OperatorTypeEnum.MaxOverDimension:
				case OperatorTypeEnum.MaxOverInlets:
					operatorCaption = ResourceFormatter.Max;
					break;

				case OperatorTypeEnum.MinOverDimension:
				case OperatorTypeEnum.MinOverInlets:
					operatorCaption = ResourceFormatter.Min;
					break;

				case OperatorTypeEnum.Number:
					operatorCaption = GetOperatorCaption_ForNumber(op);
					break;

				case OperatorTypeEnum.PatchInlet:
					operatorCaption = GetOperatorCaption_ForPatchInlet(op);
					break;

				case OperatorTypeEnum.PatchOutlet:
					operatorCaption = GetOperatorCaption_ForPatchOutlet(op);
					break;

				case OperatorTypeEnum.RangeOverDimension:
				case OperatorTypeEnum.RangeOverOutlets:
					operatorCaption = ResourceFormatter.Range;
					break;

				case OperatorTypeEnum.Sample:
					operatorCaption = GetOperatorCaption_ForSample(op);
					break;

				case OperatorTypeEnum.SetPosition:
					operatorCaption = GetOperatorCaption_ForSetPosition(op);
					break;

				case OperatorTypeEnum.SortOverDimension:
				case OperatorTypeEnum.SortOverInlets:
					operatorCaption = ResourceFormatter.Sort;
					break;

				case OperatorTypeEnum.SumOverDimension:
					operatorCaption = ResourceFormatter.Sum;
					break;

				default:
					operatorCaption = GetOperatorCaption_ForOtherOperators(op);
					break;
			}

			if (_idsVisible)
			{
				operatorCaption += $" ({op.ID})";
			}

			return operatorCaption;
		}

		private static string GetOperatorCaption_ForCurve(Operator op)
		{
			string operatorTypeDisplayName = ResourceFormatter.Curve;

			// Use Operator.Name
			if (!string.IsNullOrWhiteSpace(op.Name))
			{
				return $"{operatorTypeDisplayName}: {op.Name}";
			}

			// Use Curve.Name
			Curve underlyingEntity = op.Curve;
			if (!string.IsNullOrWhiteSpace(underlyingEntity?.Name))
			{
				return $"{operatorTypeDisplayName}: {underlyingEntity.Name}";
			}

			// Use OperatorTypeDisplayName
			string caption = operatorTypeDisplayName;
			return caption;
		}

		/// <summary> Value Operator: display name and value or only value. </summary>
		private static string GetOperatorCaption_ForNumber(Operator op)
		{
			var wrapper = new Number_OperatorWrapper(op);
			string formattedValue = wrapper.Number.ToString("0.####");

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (string.IsNullOrWhiteSpace(op.Name))
			{
				return formattedValue;
			}

			return $"{op.Name}: {formattedValue}";
		}

		private static string GetOperatorCaption_ForPatchInlet(Operator op)
		{
			var sb = new StringBuilder();

			var wrapper = new PatchInletOrOutlet_OperatorWrapper(op);

			Inlet inlet = wrapper.Inlet;

			string name = inlet.GetNameWithFallback();
			DimensionEnum dimensionEnum = inlet.GetDimensionEnumWithFallback();

			// Use OperatorType DisplayName
			sb.Append(ResourceFormatter.Inlet);

			// Try Use Operator Name
			if (!string.IsNullOrWhiteSpace(name))
			{
				sb.Append($": {name}");
			}
			// Try Use Dimension
			else if (dimensionEnum != DimensionEnum.Undefined)
			{
				sb.Append($": {ResourceFormatter.GetDisplayName(dimensionEnum)}");
			}
			// Try Say 'Dimension'
			else if (inlet.DimensionIsInherited())
			{
				sb.AppendFormat($": {ResourceFormatter.Dimension}");
			}
			// Try Use List Index
			else
			{
				sb.Append($" {inlet.Position}");
			}

			if (inlet.DimensionIsInherited())
			{
				sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
			}

			// Try Use DefaultValue
			double? defaultValue = inlet.DefaultValue;
			if (defaultValue.HasValue)
			{
				sb.Append($" = {defaultValue.Value}");
			}

			return sb.ToString();
		}

		private static string GetOperatorCaption_ForPatchOutlet(Operator op)
		{
			var sb = new StringBuilder();

			var wrapper = new PatchInletOrOutlet_OperatorWrapper(op);
			Outlet outlet = wrapper.Outlet;

			string name = outlet.GetNameWithFallback();
			DimensionEnum dimensionEnum = outlet.GetDimensionEnumWithFallback();

			// Use OperatorType DisplayName
			sb.Append(ResourceFormatter.Outlet);

			// Try Use Operator Name
			if (!string.IsNullOrWhiteSpace(name))
			{
				sb.Append($": {name}");
			}
			// Try Use Dimension
			else if (dimensionEnum != DimensionEnum.Undefined)
			{
				sb.Append($": {ResourceFormatter.GetDisplayName(dimensionEnum)}");
			}
			// Try Say 'Dimension'
			else if (outlet.DimensionIsInherited())
			{
				sb.AppendFormat($": {ResourceFormatter.Dimension}");
			}
			// Try Use List Index
			else
			{
				sb.Append($" {outlet.Position}");
			}

			// Inherited
			if (outlet.DimensionIsInherited())
			{
				sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
			}

			return sb.ToString();
		}

		private static string GetOperatorCaption_ForSample(Operator op)
		{
			string operatorTypeDisplayName = ResourceFormatter.Sample;

			// Use Operator.Name
			if (!string.IsNullOrWhiteSpace(op.Name))
			{
				return $"{operatorTypeDisplayName}: {op.Name}";
			}

			// Use Sample.Name
			Sample underlyingEntity = op.Sample;
			if (!string.IsNullOrWhiteSpace(underlyingEntity?.Name))
			{
				return $"{operatorTypeDisplayName}: {underlyingEntity.Name}";
			}

			// Use OperatorType DisplayName
			string caption = operatorTypeDisplayName;
			return caption;
		}

		private static string GetOperatorCaption_ForGetPosition(Operator op)
			=> GetOperatorCaption_WithDimensionPlaceholder(op, ResourceFormatter.GetPositionWithPlaceholder("{0}"));

		private static string GetOperatorCaption_ForSetPosition(Operator op)
			=> GetOperatorCaption_WithDimensionPlaceholder(op, ResourceFormatter.SetPositionWithPlaceholder("{0}"));

		private static string GetOperatorCaption_WithDimensionPlaceholder(Operator op, string operatorTypeDisplayNameWithPlaceholder)
		{
			var sb = new StringBuilder();

			DimensionEnum standardDimensionEnum = op.GetStandardDimensionEnumWithFallback();
			string customDimensionName = op.GetCustomDimensionNameWithFallback();

			if (standardDimensionEnum != DimensionEnum.Undefined)
			{
				// StandardDimension
				sb.AppendFormat(operatorTypeDisplayNameWithPlaceholder, ResourceFormatter.GetDisplayName(standardDimensionEnum));
			}
			else if (NameHelper.IsFilledIn(customDimensionName))
			{
				// CustomDimensionName
				sb.AppendFormat(operatorTypeDisplayNameWithPlaceholder, customDimensionName);
			}
			else
			{
				// No Dimension FilledIn
				sb.Append(ResourceFormatter.GetDisplayName(op));
			}

			// Inherited
			if (op.DimensionIsInherited())
			{
				sb.Append($" ({ResourceFormatter.Inherited.ToLower()})");
			}

			// Use Operator.Name
			if (!string.IsNullOrWhiteSpace(op.Name))
			{
				sb.Append($": {op.Name}");
			}

			return sb.ToString();
		}

		private static string GetOperatorCaption_ForOtherOperators(Operator op) => ResourceFormatter.GetDisplayName(op);

		// Outlet

		public static bool GetOutletVisible(Outlet outlet)
		{
			if (outlet == null) throw new NullException(() => outlet);

			OperatorTypeEnum operatorTypeEnum = outlet.Operator.GetOperatorTypeEnum();

			bool outletVisible = operatorTypeEnum != OperatorTypeEnum.PatchOutlet;
			return outletVisible;
		}

		public static string GetCaption(Outlet outlet)
		{
			if (outlet == null) throw new NullException(() => outlet);

			switch (outlet.Operator.GetOperatorTypeEnum())
			{
				case OperatorTypeEnum.RangeOverOutlets:
					return GetOutletCaption_ForRangeOverOutlets(outlet);

				default:
					return GetOutletCaption_ForOtherOperatorType(outlet);
			}
		}

		private static string GetOutletCaption_ForRangeOverOutlets(Outlet outlet)
		{
			var sb = new StringBuilder();

			double? from = outlet.Operator.Inlets
			                     .Where(x => x.GetDimensionEnum() == DimensionEnum.From)
			                     .Select(x => x.TryGetConstantNumber())
			                     .FirstOrDefault();
			if (from.HasValue)
			{
				double? step = outlet.Operator.Inlets
				                     .Where(x => x.GetDimensionEnum() == DimensionEnum.Step)
				                     .Select(x => x.TryGetConstantNumber())
				                     .FirstOrDefault();
				if (step.HasValue)
				{
					int listIndex = outlet.Operator.Outlets.IndexOf(outlet);

					double value = from.Value + step.Value * listIndex;

					sb.Append(value.ToString("0.####"));
				}
			}

			if (outlet.IsObsolete)
			{
				AppendObsoleteFlag(sb);
			}

			if (_idsVisible)
			{
				sb.Append($" ({outlet.ID})");
			}

			return sb.ToString();
		}

		private static string GetOutletCaption_ForOtherOperatorType(Outlet outlet)
		{
			var sb = new StringBuilder();

			if (!outlet.NameOrDimensionHidden)
			{
				// Dimension or Name
				OperatorWrapper wrapper = EntityWrapperFactory.CreateOperatorWrapper(outlet.Operator);
				string inletDisplayName = wrapper.GetOutletDisplayName(outlet);
				sb.Append(inletDisplayName);

				// RepetitionPosition
				if (outlet.RepetitionPosition.HasValue)
				{
					sb.Append($" {outlet.RepetitionPosition + 1}");
				}
			}

			// IsObsolete
			if (outlet.IsObsolete)
			{
				AppendObsoleteFlag(sb);
			}

			// ID
			if (_idsVisible)
			{
				sb.Append($" ({outlet.ID})");
			}

			return sb.ToString();
		}

		public static float? TryGetAverageConnectionDistance(Outlet entity)
		{
			if (entity == null) throw new NullException(() => entity);

			int connectedInletCount = entity.ConnectedInlets.Count;

			if (connectedInletCount == 0)
			{
				return null;
			}

			Operator operator1 = entity.Operator;

			float aggregate = 0f;

			// ReSharper disable once LoopCanBeConvertedToQuery
			foreach (Inlet connectedInlet in entity.ConnectedInlets)
			{
				Operator operator2 = connectedInlet.Operator;

				float distance = Geometry.AbsoluteDistance(
					operator1.EntityPosition.X,
					operator1.EntityPosition.Y,
					operator2.EntityPosition.X,
					operator2.EntityPosition.Y);

				aggregate += distance;
			}

			aggregate /= connectedInletCount;

			return aggregate;
		}

		// Patch

		public static string GetPatchNodeToolTipText(Patch patch, IList<IDAndName> usedInDtos)
		{
			if (patch == null) throw new ArgumentNullException(nameof(patch));
			if (usedInDtos == null) throw new ArgumentNullException(nameof(usedInDtos));

			string formattedUsedInList = FormatUsedInList(usedInDtos);

			if (string.IsNullOrEmpty(formattedUsedInList))
			{
				return patch.Name;
			}

			return $"{patch.Name}. {ResourceFormatter.UsedIn}: {formattedUsedInList}";
		}

		// Tone

		public static string GetToneGridEditValueTitle(Scale entity) => ResourceFormatter.GetScaleTypeDisplayNameSingular(entity);

		// Helpers

		private static void AppendObsoleteFlag(StringBuilder sb)
		{
			if (sb.Length != 0)
			{
				sb.Append(' ');
			}

			sb.Append($"({ResourceFormatter.IsObsolete})");
		}
	}
}