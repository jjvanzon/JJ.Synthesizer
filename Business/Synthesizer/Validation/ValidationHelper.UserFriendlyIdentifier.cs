using System;
using System.Collections.Generic;
using System.Text;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Common;
using JJ.Framework.Exceptions;
using JJ.Framework.Resources;
using JJ.Framework.Validation.Resources;

namespace JJ.Business.Synthesizer.Validation
{
	/// <summary>
	/// Identifiers in this case are user-friendly identifications of an entity.
	/// They are used in places where the data might not be valid yet,
	/// so contains fallbacks to other means of identification.
	/// It can happen that the identification is non-unique.
	/// But they attempt to be the clearest identification to the user.
	/// Quotes are added around names, so are already part of the identifier.
	/// </summary>
	public static partial class ValidationHelper
	{
		public static string GetUserFriendlyIdentifier(AudioFileOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);
			return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
		}

		public static string GetUserFriendlyIdentifier(AudioOutput entity)
		{
			if (entity == null) throw new NullException(() => entity);

			return ResourceFormatter.AudioOutput;
		}

		public static string GetUserFriendlyIdentifier(Curve entity)
		{
			if (entity == null) throw new NullException(() => entity);
			return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
		}

		public static string GetUserFriendlyIdentifier(Document entity)
		{
			if (entity == null) throw new NullException(() => entity);
			return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
		}

		/// <summary>
		/// Returns a string formatted like one of the following:
		/// '{0}' /
		/// 'no Name' /
		/// '{0}' (Alias '{1}') /
		/// 'no Name' (Alias '{1}')
		/// </summary>
		public static string GetUserFriendlyIdentifier_ForLowerDocumentReference(DocumentReference lowerDocumentReference)
		{
			if (lowerDocumentReference == null) throw new NullException(() => lowerDocumentReference);

			var sb = new StringBuilder();

			if (lowerDocumentReference.LowerDocument != null)
			{
				string lowerDocumentIdentifier = GetUserFriendlyIdentifier(lowerDocumentReference.LowerDocument);
				sb.Append(lowerDocumentIdentifier);
			}

			if (!string.IsNullOrWhiteSpace(lowerDocumentReference.Alias))
			{
				sb.Append($" ({ResourceFormatter.Alias} '{lowerDocumentReference.Alias}')");
			}

			return sb.ToString();
		}

		public static string GetUserFriendlyIdentifier_ForHigherDocumentReference(DocumentReference higherDocumentReference)
		{
			if (higherDocumentReference == null) throw new NullException(() => higherDocumentReference);

			if (higherDocumentReference.HigherDocument != null)
			{
				string higherDocumentIdentifier = GetUserFriendlyIdentifier(higherDocumentReference.HigherDocument);
				return higherDocumentIdentifier;
			}

			return ValidationResourceFormatter.IsEmpty(ResourceFormatter.HigherDocument);
		}

		public static string GetUserFriendlyIdentifier(IInletOrOutlet inletOrOutlet)
		{
			if (inletOrOutlet == null) throw new NullException(() => inletOrOutlet);

			string userFriendlyIdentifier = GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition(
				inletOrOutlet.GetNameWithFallback(),
				inletOrOutlet.GetDimensionEnumWithFallback(),
				inletOrOutlet.Position);

			return userFriendlyIdentifier;
		}

		/// <param name="number">1-based</param>
		public static string GetUserFriendlyIdentifier(Node entity, int number)
		{
			if (entity == null) throw new NullException(() => entity);
			return number.ToString();
		}

		public static string GetUserFriendlyIdentifier(MidiMapping entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (!string.IsNullOrWhiteSpace(entity.Name))
			{
				return $"'{entity.Name}'";
			}

			return GetNoNameIdentifier();
		}

		public static string GetUserFriendlyIdentifierLong(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var sb = new StringBuilder();

			if (!string.IsNullOrWhiteSpace(entity.CustomDimensionName))
			{
				sb.Append($"{ResourceFormatter.CustomDimensionName} '{entity.CustomDimensionName}', ");
			}

			if (entity.StandardDimension != null)
			{
				sb.Append($"{ResourceFormatter.StandardDimension} '{ResourceFormatter.GetDisplayName(entity.StandardDimension)}', ");
			}

			if (entity.MidiControllerCode.HasValue)
			{
				sb.Append($"{ResourceFormatter.MidiControllerCode} {entity.MidiControllerCode}, ");
			}

			if (entity.Scale != null)
			{
				sb.Append($"{ResourceFormatter.Scale} '{GetUserFriendlyIdentifier(entity.Scale)}', ");
			}

			string[] elements =
			{
				TryGetRangeIdentifier(ResourceFormatter.DimensionValues, entity.FromDimensionValue, entity.TillDimensionValue),
				TryGetRangeIdentifier(ResourceFormatter.MidiControllerValues, entity.FromMidiControllerValue, entity.TillMidiControllerValue),
				TryGetRangeIdentifier(ResourceFormatter.MidiNoteNumbers, entity.FromMidiNoteNumber, entity.TillMidiNoteNumber),
				TryGetRangeIdentifier(ResourceFormatter.MidiVelocities, entity.FromMidiVelocity, entity.TillMidiVelocity),
				TryGetRangeIdentifier(ResourceFormatter.ToneNumbers, entity.FromToneNumber, entity.TillToneNumber),
				TryGetRangeIdentifier(ResourceFormatter.Positions, entity.FromPosition, entity.TillPosition)
			};

			foreach (string element in elements)
			{
				if (!string.IsNullOrEmpty(element))
				{
					sb.Append(element);
					sb.Append(", ");
				}
			}

			return sb.ToString().TrimEnd(", ");
		}

		public static string GetUserFriendlyIdentifierShort(MidiMappingElement entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			var sb = new StringBuilder();

			bool dimensionIsFilledIn = entity.StandardDimension != null || !string.IsNullOrWhiteSpace(entity.CustomDimensionName);
			if (dimensionIsFilledIn)
			{
				// Use StandardDimension
				if (entity.StandardDimension != null)
				{
					sb.Append(ResourceFormatter.GetDisplayName(entity.StandardDimension));
					sb.Append(' ');
				}

				// Use CustomDimensionName
				if (!string.IsNullOrWhiteSpace(entity.CustomDimensionName))
				{
					sb.Append(entity.CustomDimensionName);
				}
			}
			else if (entity.MidiControllerCode.HasValue)
			{
				// Use ControllerCode
				sb.Append(entity.MidiControllerCode);
			}
			else if (entity.HasToneNumbers())
			{
				sb.Append(ResourceFormatter.ToneNumbers);
			}
			else if (entity.HasMidiNoteNumbers())
			{
				sb.Append(ResourceFormatter.MidiNoteNumbers);
			}
			else if (entity.HasMidiVelocities())
			{
				sb.Append(ResourceFormatter.MidiVelocities);
			}
			else if (entity.HasPositions())
			{
				sb.Append(ResourceFormatter.Positions);
			}

			string userFriendlyIdentifier = sb.ToString().TrimEnd();

			return userFriendlyIdentifier;
		}

		public static string GetUserFriendlyIdentifier(Operator entity, ICurveRepository curveRepository)
		{
			if (entity == null) throw new NullException(() => entity);

			OperatorTypeEnum operatorTypeEnum = entity.GetOperatorTypeEnum();

			switch (operatorTypeEnum)
			{
				case OperatorTypeEnum.Curve: return GetUserFriendlyIdentifier_ForCurveOperator(entity, curveRepository);
				case OperatorTypeEnum.Sample: return GetUserFriendlyIdentifier_ForSampleOperator(entity);
				case OperatorTypeEnum.Number: return GetUserFriendlyIdentifier_ForNumberOperator(entity);
				case OperatorTypeEnum.PatchInlet: return GetUserFriendlyIdentifier_ForPatchInlet(entity);
				case OperatorTypeEnum.PatchOutlet: return GetUserFriendlyIdentifier_ForPatchOutlet(entity);
				default: return GetUserFriendlyIdentifier_ForOtherOperator(entity);
			}
		}

		public static string GetUserFriendlyIdentifier_ForCurveOperator(Operator entity, ICurveRepository curveRepository)
		{
			if (entity == null) throw new NullException(() => entity);
			if (curveRepository == null) throw new NullException(() => curveRepository);

			// Use Operator Name
			if (!string.IsNullOrWhiteSpace(entity.Name))
			{
				return $"'{entity.Name}'";
			}

			// Use Underlying Entity Name
			Curve underlyingEntity = entity.Curve;
			if (underlyingEntity != null)
			{
				return GetUserFriendlyIdentifier(underlyingEntity);
			}

			// Mention 'no name' only
			return $"'{ResourceFormatter.GetDisplayName(entity)}'";
		}

		public static string GetUserFriendlyIdentifier_ForSampleOperator(Operator entity)
		{
			if (entity == null) throw new NullException(() => entity);

			// Use Operator Name
			if (!string.IsNullOrWhiteSpace(entity.Name))
			{
				return $"'{entity.Name}'";
			}

			// Use Underlying Entity Name
			Sample underlyingEntity = entity.Sample;
			if (underlyingEntity != null)
			{
				return GetUserFriendlyIdentifier(underlyingEntity);
			}

			// Mention 'no name' only
			return $"'{ResourceFormatter.GetDisplayName(entity)}'";
		}

		private static string GetUserFriendlyIdentifier_ForOtherOperator(Operator entity)
		{
			if (entity == null) throw new NullException(() => entity);

			// Use Operator Name
			if (!string.IsNullOrWhiteSpace(entity.Name))
			{
				return $"'{entity.Name}'";
			}

			// Use Underlying Entity Name
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (entity.UnderlyingPatch != null)
			{
				return GetUserFriendlyIdentifier(entity.UnderlyingPatch);
			}

			// Mention 'no name' only
			return $"'{ResourceFormatter.GetDisplayName(entity)}'";
		}

		private static string GetUserFriendlyIdentifier_ForNumberOperator(Operator entity)
		{
			if (entity == null) throw new NullException(() => entity);

			// Use Operator Name
			if (!string.IsNullOrWhiteSpace(entity.Name))
			{
				return $"'{entity.Name}'";
			}

			// Use Number
			// ReSharper disable once InvertIf
			if (DataPropertyParser.DataIsWellFormed(entity.Data))
			{
				double? number = DataPropertyParser.TryParseDouble(entity.Data, nameof(Number_OperatorWrapper.Number));
				// ReSharper disable once InvertIf
				if (number.HasValue)
				{
					return $"'{FormatNumber(number.Value)}'";
				}
			}

			// Mention 'no name' only
			return $"'{ResourceFormatter.GetDisplayName(entity)}'";
		}

		private static string GetUserFriendlyIdentifier_ForPatchInlet(Operator entity)
		{
			if (entity == null) throw new NullException(() => entity);

			// ReSharper disable once InvertIf
			if (entity.Inlets.Count == 1)
			{
				Inlet inlet = entity.Inlets[0];
				string identifier = GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition(inlet.Name, inlet.GetDimensionEnum(), inlet.Position);
				return identifier;
			}

			// Mention 'no name' only
			return $"'{ResourceFormatter.GetDisplayName(entity)}'";
		}

		private static string GetUserFriendlyIdentifier_ForPatchOutlet(Operator entity)
		{
			if (entity == null) throw new NullException(() => entity);

			// ReSharper disable once InvertIf
			if (entity.Outlets.Count == 1)
			{
				Outlet outlet = entity.Outlets[0];
				string identifier = GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition(outlet.Name, outlet.GetDimensionEnum(), outlet.Position);
				return identifier;
			}

			// Mention 'no name' only
			return $"'{ResourceFormatter.GetDisplayName(entity)}'";
		}

		public static string GetUserFriendlyIdentifier(Patch entity)
		{
			if (entity == null) throw new NullException(() => entity);
			return GetUserFriendlyIdentifier_WithName_AndNoNameFallback(entity.Name);
		}

		public static string GetUserFriendlyIdentifier(Sample entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (!string.IsNullOrWhiteSpace(entity.Name))
			{
				return $"'{entity.Name}'";
			}

			// Message prefix would become something like: 
			// Sample '16-Bit Mono 44100Hz WAV-File'

			var identifierElements = new List<string>();
			if (entity.SampleDataType != null)
			{
				string sampleDataTypeDisplayName = ResourceFormatter.GetDisplayName(entity.SampleDataType);
				identifierElements.Add(sampleDataTypeDisplayName);
			}

			if (entity.SpeakerSetup != null)
			{
				string speakerSetupDisplayName = ResourceFormatter.GetDisplayName(entity.SpeakerSetup);
				identifierElements.Add(speakerSetupDisplayName);
			}

			string samplingRateDisplayText = $"{entity.SamplingRate}Hz";
			identifierElements.Add(samplingRateDisplayText);

			if (entity.AudioFileFormat != null)
			{
				string audioFileFormatDisplayName = ResourceFormatter.GetDisplayName(entity.AudioFileFormat);
				identifierElements.Add(audioFileFormatDisplayName);
			}

			string indentifier = StringHelper.Join(' ', identifierElements);

			return $"'{indentifier}'";
		}

		public static string GetUserFriendlyIdentifier(Scale entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (!string.IsNullOrWhiteSpace(entity.Name))
			{
				return $"'{entity.Name}'";
			}

			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (entity.ScaleType != null)
			{
				return $"'{ResourceFormatter.GetScaleTypeDisplayNamePlural(entity)}'";
			}

			return GetNoNameIdentifier();
		}

		public static string GetUserFriendlyIdentifier(Tone entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var sb = new StringBuilder();

			sb.Append('\'');
			sb.Append($"{ResourceFormatter.Octave} {entity.Octave}, ");

			// ReSharper disable once ConstantConditionalAccessQualifier
			if (entity.Scale?.ScaleType != null)
			{
				sb.Append($"{ResourceFormatter.GetScaleTypeDisplayNameSingular(entity.Scale)} ");
			}

			sb.Append($"{FormatNumber(entity.Value)}");
			sb.Append('\'');

			return sb.ToString();
		}

		// Helpers

		private static string GetUserFriendlyIdentifier_WithName_AndNoNameFallback(string name)
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (!string.IsNullOrWhiteSpace(name))
			{
				return $"'{name}'";
			}

			return GetNoNameIdentifier();
		}

		private static string GetUserFriendlyIdentifier_WithName_DimensionEnum_AndPosition(string name, DimensionEnum dimensionEnum, int position)
		{
			// Use Name
			if (!string.IsNullOrWhiteSpace(name))
			{
				return $"'{name}'";
			}

			// Use Dimension
			if (dimensionEnum != DimensionEnum.Undefined)
			{
				// Downside: Dimension might not be unique among an operator's inlets or outlets.
				string dimensionDisplayName = ResourceFormatter.GetDisplayName(dimensionEnum);
				return $"'{dimensionDisplayName}'";
			}

			// Use Position
			string identifier = $"({ResourceFormatter.Position} = {position})";
			return identifier;
		}

		private static string GetNoNameIdentifier() => $"'{CommonResourceFormatter.NoObject_WithName(CommonResourceFormatter.Name)}'";

		private static string FormatNumber(double number) => $"{number:0.######}";

		private static string TryGetRangeIdentifier<T>(string displayName, T? from, T? till)
			where T : struct
		{
			if (from.HasValue || till.HasValue)
			{
				return $"{displayName} [{@from}-{till}]";
			}

			return null;
		}
	}
}