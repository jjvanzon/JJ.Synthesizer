using System;
using System.Linq.Expressions;
using System.Resources;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Conversion;
using JJ.Framework.Exceptions;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Resources
{
	public static class ResourceFormatter
	{
		public static ResourceManager ResourceManager => Resources.ResourceManager;

		// Resource texts without placeholders

		public static string A => Resources.A;
		public static string Absolute => Resources.Absolute;
		public static string Add => Resources.Add;
		public static string AddToInstrument => Resources.AddToInstrument;
		public static string Alias => Resources.Alias;
		public static string Aliases => Resources.Aliases;
		public static string AllPassFilter => Resources.AllPassFilter;
		public static string Amplifier => Resources.Amplifier;
		public static string And => Resources.And;
		public static string ApplicationName => Resources.ApplicationName;
		public static string AttackDuration => Resources.AttackDuration;
		public static string AudioFileFormat => Resources.AudioFileFormat;
		public static string AudioFileOutput => Resources.AudioFileOutput;
		public static string AudioFileOutputList => Resources.AudioFileOutputList;
		public static string AudioOutput => Resources.AudioOutput;
		public static string AutoPatch => Resources.AutoPatch;
		public static string Average => Resources.Average;
		public static string AverageFollower => Resources.AverageFollower;
		public static string AverageOverDimension => Resources.AverageOverDimension;
		public static string AverageOverInlets => Resources.AverageOverInlets;
		public static string B => Resources.B;
		public static string Balance => Resources.Balance;
		public static string BandPassFilterConstantPeakGain => Resources.BandPassFilterConstantPeakGain;
		public static string BandPassFilterConstantTransitionGain => Resources.BandPassFilterConstantTransitionGain;
		public static string Base => Resources.Base;
		public static string BaseFrequency => Resources.BaseFrequency;
		public static string Block => Resources.Block;
		public static string Brightness => Resources.Brightness;
		public static string BrightnessModulationDepth => Resources.BrightnessModulationDepth;
		public static string BrightnessModulationSpeed => Resources.BrightnessModulationSpeed;
		public static string Byte => Resources.Byte;
		public static string Bytes => Resources.Bytes;
		public static string BytesToSkip => Resources.BytesToSkip;
		public static string Cache => Resources.Cache;
		public static string CannotSetInletCountWithoutRepeatingInlets => Resources.CannotSetInletCountWithoutRepeatingInlets;
		public static string CannotSetOutletCountWithoutRepeatingOutlets => Resources.CannotSetOutletCountWithoutRepeatingOutlets;
		public static string Cent => Resources.Cent;
		public static string CenterFrequency => Resources.CenterFrequency;
		public static string Cents => Resources.Cents;
		public static string ChangeTrigger => Resources.ChangeTrigger;
		public static string Channel => Resources.Channel;
		public static string Channels => Resources.Channels;
		public static string CircularInputOutputReference => Resources.CircularInputOutputReference;
		public static string Closest => Resources.Closest;
		public static string ClosestExp => Resources.ClosestExp;
		public static string ClosestOverDimension => Resources.ClosestOverDimension;
		public static string ClosestOverDimensionExp => Resources.ClosestOverDimensionExp;
		public static string ClosestOverInlets => Resources.ClosestOverInlets;
		public static string ClosestOverInletsExp => Resources.ClosestOverInletsExp;
		public static string CollectionRecalculation => Resources.CollectionRecalculation;
		public static string Condition => Resources.Condition;
		public static string Continuous => Resources.Continuous;
		public static string Cubic => Resources.Cubic;
		public static string CubicAbruptSlope => Resources.CubicAbruptSlope;
		public static string CubicEquidistant => Resources.CubicEquidistant;
		public static string CubicSmoothSlope => Resources.CubicSmoothSlope;
		public static string CurrentInstrument => Resources.CurrentInstrument;
		public static string Curve => Resources.Curve;
		public static string Custom => Resources.Custom;
		public static string CustomDimension => Resources.CustomDimension;
		public static string CustomDimensionName => Resources.CustomDimensionName;
		public static string CustomOperator => Resources.CustomOperator;
		public static string Data => Resources.Data;
		public static string DataKey => Resources.DataKey;
		public static string DataKeys => Resources.DataKeys;
		public static string DBGain => Resources.DBGain;
		public static string DecayDuration => Resources.DecayDuration;
		public static string DefaultValue => Resources.DefaultValue;
		public static string DesiredBufferDuration => Resources.DesiredBufferDuration;
		public static string Difference => Resources.Difference;
		public static string Dimension => Resources.Dimension;
		public static string DimensionToOutlets => Resources.DimensionToOutlets;
		public static string DimensionValue => Resources.DimensionValue;
		public static string DimensionValues => Resources.DimensionValues;
		public static string Divide => Resources.Divide;
		public static string Document => Resources.Document;
		public static string DocumentCannotReferenceItself => Resources.DocumentCannotReferenceItself;
		public static string DocumentList => Resources.DocumentList;
		public static string DocumentName => Resources.DocumentName;
		public static string Documents => Resources.Documents;
		public static string DocumentTree => Resources.DocumentTree;
		public static string Duration => Resources.Duration;
		public static string Else => Resources.Else;
		public static string End => Resources.End;
		public static string EntityPosition => Resources.EntityPosition;
		public static string Equal => Resources.Equal;
		public static string Exponent => Resources.Exponent;
		public static string Exponents => Resources.Exponents;
		public static string Factor => Resources.Factor;
		public static string Factors => Resources.Factors;
		public static string FilePath => Resources.FilePath;
		public static string FirstRepetitionPosition => Resources.FirstRepetitionPosition;
		public static string Frequencies => Resources.Frequencies;
		public static string Frequency => Resources.Frequency;
		public static string From => Resources.From;
		public static string FromDimensionValue => Resources.FromDimensionValue;
		public static string FromMidiControllerValue => Resources.FromMidiControllerValue;
		public static string FromMidiNoteNumber => Resources.FromMidiNoteNumber;
		public static string FromMidiVelocity => Resources.FromMidiVelocity;
		public static string FromPosition => Resources.FromPosition;
		public static string FromToneNumber => Resources.FromToneNumber;
		public static string GetPosition => Resources.GetPosition;
		public static string GreaterThan => Resources.GreaterThan;
		public static string GreaterThanOrEqual => Resources.GreaterThanOrEqual;
		public static string Group => Resources.Group;
		public static string GroupName => Resources.GroupName;
		public static string Harmonic => Resources.Harmonic;
		public static string HasDimension => Resources.HasDimension;
		public static string HasDimensionMinMaxButNoFromOrTill => Resources.HasDimensionMinMaxButNoFromOrTill;
		public static string Hermite => Resources.Hermite;
		public static string Hidden => Resources.Hidden;
		public static string High => Resources.High;
		public static string HigherDocument => Resources.HigherDocument;
		public static string HighPassFilter => Resources.HighPassFilter;
		public static string HighShelfFilter => Resources.HighShelfFilter;
		public static string Hold => Resources.Hold;
		public static string If => Resources.If;
		public static string Inherit => Resources.Inherit;
		public static string Inherited => Resources.Inherited;
		public static string Inlet => Resources.Inlet;
		public static string Inlets => Resources.Inlets;
		public static string InletsToDimension => Resources.InletsToDimension;
		public static string Input => Resources.Input;
		public static string Int16 => Resources.Int16;
		public static string Intensity => Resources.Intensity;
		public static string IntensityModulationDepth => Resources.IntensityModulationDepth;
		public static string IntensityModulationSpeed => Resources.IntensityModulationSpeed;
		public static string Interpolate => Resources.Interpolate;
		public static string Interpolation => Resources.Interpolation;
		public static string InterpolationType => Resources.InterpolationType;
		public static string IsObsolete => Resources.IsObsolete;
		public static string IsRelative => Resources.IsRelative;
		public static string IsRepeating => Resources.IsRepeating;
		public static string Left => Resources.Left;
		public static string LessThan => Resources.LessThan;
		public static string LessThanOrEqual => Resources.LessThanOrEqual;
		public static string Libraries => Resources.Libraries;
		public static string Library => Resources.Library;
		public static string Line => Resources.Line;
		public static string LiteralFrequencies => Resources.LiteralFrequencies;
		public static string LiteralFrequency => Resources.LiteralFrequency;
		public static string Loop => Resources.Loop;
		public static string LoopEndMarker => Resources.LoopEndMarker;
		public static string LoopStartMarker => Resources.LoopStartMarker;
		public static string Low => Resources.Low;
		public static string LowPassFilter => Resources.LowPassFilter;
		public static string LowShelfFilter => Resources.LowShelfFilter;
		public static string MainVolume => Resources.MainVolume;
		public static string Max => Resources.Max;
		public static string MaxConcurrentNotes => Resources.MaxConcurrentNotes;
		public static string MaxDimensionValue => Resources.MaxDimensionValue;
		public static string MaxFollower => Resources.MaxFollower;
		public static string MaxFrequency => Resources.MaxFrequency;
		public static string MaxOverDimension => Resources.MaxOverDimension;
		public static string MaxOverInlets => Resources.MaxOverInlets;
		public static string Midi => Resources.Midi;
		public static string MidiControllerCode => Resources.MidiControllerCode;
		public static string MidiControllerValue => Resources.MidiControllerValue;
		public static string MidiControllerValues => Resources.MidiControllerValues;
		public static string MidiMapping => Resources.MidiMapping;
		public static string MidiMappingElement => Resources.MidiMappingElement;
		public static string MidiNoteNumber => Resources.MidiNoteNumber;
		public static string MidiNoteNumbers => Resources.MidiNoteNumbers;
		public static string MidiVelocities => Resources.MidiVelocities;
		public static string MidiVelocity => Resources.MidiVelocity;
		public static string Min => Resources.Min;
		public static string MinDimensionValue => Resources.MinDimensionValue;
		public static string MinFollower => Resources.MinFollower;
		public static string MinFrequency => Resources.MinFrequency;
		public static string MinMaxDimensionValues => Resources.MinMaxDimensionValues;
		public static string MinOverDimension => Resources.MinOverDimension;
		public static string MinOverInlets => Resources.MinOverInlets;
		public static string MismatchWithUnderlyingPatch => Resources.MismatchWithUnderlyingPatch;
		public static string Mono => Resources.Mono;
		public static string Multiply => Resources.Multiply;
		public static string MultiplyWithOrigin => Resources.MultiplyWithOrigin;
		public static string NameOrDimensionHidden => Resources.NameOrDimensionHidden;
		public static string Negative => Resources.Negative;
		public static string Node => Resources.Node;
		public static string Nodes => Resources.Nodes;
		public static string NodeType => Resources.NodeType;
		public static string Noise => Resources.Noise;
		public static string NoSoundFound => Resources.NoSoundFoundInLibrary;
		public static string Not => Resources.Not;
		public static string NotchFilter => Resources.NotchFilter;
		public static string NoteDuration => Resources.NoteDuration;
		public static string NotEqual => Resources.NotEqual;
		public static string NoteStart => Resources.NoteStart;
		public static string NotLoaded => Resources.NotLoaded;
		public static string Number => Resources.Number;
		public static string ObsoleteButStillUsed => Resources.ObsoleteButStillUsed;
		public static string Octave => Resources.Octave;
		public static string Off => Resources.Off;
		public static string Offset => Resources.Offset;
		public static string OneOverX => Resources.OneOverX;
		public static string Operator => Resources.Operator;
		public static string OperatorIsInGraphButNotInList => Resources.OperatorIsInGraphButNotInList;
		public static string Or => Resources.Or;
		public static string Ordinal => Resources.Ordinal;
		public static string Origin => Resources.Origin;
		public static string Outlet => Resources.Outlet;
		public static string Outlets => Resources.Outlets;
		public static string Output => Resources.Output;
		public static string PassThrough => Resources.PassThrough;
		public static string Patch => Resources.Patch;
		public static string Patches => Resources.Patches;
		public static string PatchesInLibrary => Resources.PatchesInLibrary;
		public static string PatchHasMoreThanOneRepeatingInlet => Resources.PatchHasMoreThanOneRepeatingInlet;
		public static string PatchHasMoreThanOneRepeatingOutlet => Resources.PatchHasMoreThanOneRepeatingOutlet;
		public static string PatchHasNoOutlets => Resources.PatchHasNoOutlets;
		public static string PatchInlet => Resources.PatchInlet;
		public static string PatchOutlet => Resources.PatchOutlet;
		public static string PeakingEQFilter => Resources.PeakingEQFilter;
		public static string Phase => Resources.Phase;
		public static string PitchBend => Resources.PitchBend;
		public static string Play => Resources.Play;
		public static string Position => Resources.Position;
		public static string Positions => Resources.Positions;
		public static string Power => Resources.Power;
		public static string PreviousPosition => Resources.PreviousPosition;
		public static string Pulse => Resources.Pulse;
		public static string PulseTrigger => Resources.PulseTrigger;
		public static string Random => Resources.Random;
		public static string Range => Resources.Range;
		public static string RangeOverDimension => Resources.RangeOverDimension;
		public static string RangeOverOutlets => Resources.RangeOverOutlets;
		public static string Rate => Resources.Rate;
		public static string Ratio => Resources.Ratio;
		public static string Raw => Resources.Raw;
		public static string ReleaseDuration => Resources.ReleaseDuration;
		public static string ReleaseEndMarker => Resources.ReleaseEndMarker;
		public static string RepetitionPosition => Resources.RepetitionPosition;
		public static string Reset => Resources.Reset;
		public static string Reverse => Resources.Reverse;
		public static string Right => Resources.Right;
		public static string Round => Resources.Round;
		public static string Sample => Resources.Sample;
		public static string SampleDataType => Resources.SampleDataType;
		public static string Samples => Resources.Samples;
		public static string SamplingRate => Resources.SamplingRate;
		public static string SawDown => Resources.SawDown;
		public static string SawUp => Resources.SawUp;
		public static string Scale => Resources.Scale;
		public static string Scaler => Resources.Scaler;
		public static string Scales => Resources.Scales;
		public static string ScaleType => Resources.ScaleType;
		public static string SelectALibraryFirst => Resources.SelectALibraryFirst;
		public static string SelectAnElementFirst => Resources.SelectAnElementFirst;
		public static string SelectANodeFirst => Resources.SelectANodeFirst;
		public static string SelectAnOperatorFirst => Resources.SelectAnOperatorFirst;
		public static string SelectedOperatorHasNoOutlets => Resources.SelectedOperatorHasNoOutlets;
		public static string SemiTone => Resources.SemiTone;
		public static string SemiTones => Resources.SemiTones;
		public static string SetPosition => Resources.SetPosition;
		public static string Shift => Resources.Shift;
		public static string Signal => Resources.Signal;
		public static string Sine => Resources.Sine;
		public static string Single => Resources.Single;
		public static string Skip => Resources.Skip;
		public static string SliceLength => Resources.SliceLength;
		public static string Sort => Resources.Sort;
		public static string SortOverDimension => Resources.SortOverDimension;
		public static string SortOverInlets => Resources.SortOverInlets;
		public static string Sound => Resources.Sound;
		public static string SourceValueA => Resources.SourceValueA;
		public static string SourceValueB => Resources.SourceValueB;
		public static string SpeakerSetup => Resources.SpeakerSetup;
		public static string Spectrum => Resources.Spectrum;
		public static string Square => Resources.Square;
		public static string Squash => Resources.Squash;
		public static string Standard => Resources.Standard;
		public static string StandardDimension => Resources.StandardDimension;
		public static string Start => Resources.Start;
		public static string StartTime => Resources.StartTime;
		public static string Step => Resources.Step;
		public static string Stereo => Resources.Stereo;
		public static string Stretch => Resources.Stretch;
		public static string Stripe => Resources.Stripe;
		public static string Subtract => Resources.Subtract;
		public static string Sum => Resources.Sum;
		public static string SumFollower => Resources.SumFollower;
		public static string SumOverDimension => Resources.SumOverDimension;
		public static string SustainVolume => Resources.SustainVolume;
		public static string TargetValueA => Resources.TargetValueA;
		public static string TargetValueB => Resources.TargetValueB;
		public static string Then => Resources.Then;
		public static string Till => Resources.Till;
		public static string TillDimensionValue => Resources.TillDimensionValue;
		public static string TillMidiControllerValue => Resources.TillMidiControllerValue;
		public static string TillMidiNoteNumber => Resources.TillMidiNoteNumber;
		public static string TillMidiVelocity => Resources.TillMidiVelocity;
		public static string TillPosition => Resources.TillPosition;
		public static string TillToneNumber => Resources.TillToneNumber;
		public static string Time => Resources.Time;
		public static string TimeMultiplier => Resources.TimeMultiplier;
		public static string TimePower => Resources.TimePower;
		public static string ToggleTrigger => Resources.ToggleTrigger;
		public static string Tone => Resources.Tone;
		public static string ToneNumber => Resources.ToneNumber;
		public static string ToneNumbers => Resources.ToneNumbers;
		public static string Tones => Resources.Tones;
		public static string TransitionFrequency => Resources.TransitionFrequency;
		public static string TransitionSlope => Resources.TransitionSlope;
		public static string TremoloDepth => Resources.TremoloDepth;
		public static string TremoloSpeed => Resources.TremoloSpeed;
		public static string Triangle => Resources.Triangle;
		public static string UnderlyingPatch => Resources.UnderlyingPatch;
		public static string UnderlyingPatchIsCircular => Resources.UnderlyingPatchIsCircular;
		public static string UponReset => Resources.UponReset;
		public static string UsedIn => Resources.UsedIn;
		public static string Value => Resources.Value;
		public static string VibratoDepth => Resources.VibratoDepth;
		public static string VibratoSpeed => Resources.VibratoSpeed;
		public static string Volume => Resources.Volume;
		public static string Volumes => Resources.Volumes;
		public static string WarnIfEmpty => Resources.WarnIfEmpty;
		public static string Warnings => Resources.Warnings;
		public static string Wav => Resources.Wav;
		public static string Width => Resources.Width;
		public static string X => Resources.X;
		public static string Y => Resources.Y;

		// Resource texts with placeholders

		public static string CannotChangeInletsBecauseOneIsStillFilledIn(int oneBasedInletNumber) => string.Format(Resources.CannotChangeInletCountBecauseOneIsStillFilledIn, oneBasedInletNumber);
		public static string CannotChangeOutletsBecauseOneIsStillFilledIn(int oneBasedOutletNumber) => string.Format(Resources.CannotChangeOutletCountBecauseOneIsStillFilledIn, oneBasedOutletNumber);
		public static string CannotHide_WithName_AndDependentItem(string name, string dependentItem) => string.Format(Resources.CannotHide_WithName_AndDependentItem, name, dependentItem);
		public static string GetPositionWithPlaceholder(string dimension) => string.Format(Resources.GetPositionWithPlaceholder, dimension);
		public static string LibraryAlreadyAdded_WithName(string name) => string.Format(Resources.LibraryAlreadyAdded_WithName, name);
		public static string MustBePowerOf2(string name) => string.Format(Resources.MustBePowerOf2, name);
		public static string OperatorPatchIsNotTheExpectedPatch(string operatorName, string expectedPatchName) => string.Format(Resources.OperatorPatchIsNotTheExpectedPatch, operatorName, expectedPatchName);
		public static string RepetitionPositionsNotConsecutive(string concatinatedRepetitionPositions) => string.Format(Resources.RepetitionPositionsNotConsecutive, concatinatedRepetitionPositions);
		public static string SetPositionWithPlaceholder(string dimension) => string.Format(Resources.SetPositionWithPlaceholder, dimension);

		// Generic methods that could return several different resource text

		public static string GetDisplayName(string resourceName)
		{
			string str = Resources.ResourceManager.GetString(resourceName);

			if (string.IsNullOrEmpty(str))
			{
				str = resourceName;
			}

			return str;
		}

		public static string GetDisplayName(Expression<Func<object>> resourceNameExpression)
		{
			if (resourceNameExpression == null) throw new NullException(() => resourceNameExpression);

			string resourceName = ExpressionHelper.GetName(resourceNameExpression);
			string str = GetDisplayName(resourceName);
			return str;
		}

		// AudioFileFormat

		internal static string GetDisplayName(AudioFileFormatEnum audioFileFormatEnum) => GetDisplayName(audioFileFormatEnum.ToString());

		internal static string GetDisplayName(AudioFileFormat audioFileFormat) => GetDisplayName(audioFileFormat.ToEnum());

		// CollectionRecalculation

		public static string GetDisplayName(CollectionRecalculationEnum enumValue)
		{
			return GetDisplayName(enumValue.ToString());
		}

		// Dimension

		public static string GetDisplayName(Dimension entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var dimensionEnum = (DimensionEnum)entity.ID;

			return GetDisplayName(dimensionEnum);
		}

		public static string GetDisplayName(DimensionEnum enumValue)
		{
			return GetDisplayName(enumValue.ToString());
		}

		// Inlet

		public static string GetDisplayName(Inlet inlet)
		{
			if (inlet == null) throw new NullException(() => inlet);

			bool isFromSystem = inlet.Operator?.UnderlyingPatch?.IsSystemPatch() ?? false;
			if (isFromSystem)
			{
				string displayName = GetDisplayName(inlet.Name);
				if (!string.IsNullOrEmpty(displayName))
				{
					return displayName;
				}
			}

			return inlet.Name;
		}

		// InterpolationType

		public static string GetDisplayName(InterpolationType entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var dimensionEnum = (InterpolationTypeEnum)entity.ID;

			return GetDisplayName(dimensionEnum);
		}

		public static string GetDisplayName(InterpolationTypeEnum enumValue)
		{
			return GetDisplayName(enumValue.ToString());
		}

		// Operator

		public static string GetDisplayName(Operator op)
		{
			if (op == null) throw new NullException(() => op);

			if (NameHelper.IsFilledIn(op.Name))
			{
				return op.Name;
			}

			if (op.UnderlyingPatch != null)
			{
				return GetDisplayName(op.UnderlyingPatch);
			}

			return null;
		}

		// Outlet

		public static string GetDisplayName(Outlet outlet)
		{
			if (outlet == null) throw new NullException(() => outlet);

			bool isFromSystem = outlet.Operator?.UnderlyingPatch?.IsSystemPatch() ?? false;
			if (isFromSystem)
			{
				string displayName = GetDisplayName(outlet.Name);
				if (!string.IsNullOrEmpty(displayName))
				{
					return displayName;
				}
			}

			return outlet.Name;
		}

		// Patch

		/// <summary>
		/// Will use a resource string in case of system patches, otherwise just returns the Patch.Name.
		/// </summary>
		public static string GetDisplayName(Patch entity)
		{
			if (entity == null) throw new NullException(() => entity);

			if (entity.IsSystemPatch())
			{
				return GetDisplayName(entity.Name);
			}
			else
			{
				return entity.Name;
			}
		}

		// ResampleInterpolationType

		public static string GetDisplayName(ResampleInterpolationTypeEnum enumValue)
		{
			return GetDisplayName(enumValue.ToString());
		}

		// SampleDataType

		internal static string GetDisplayName(SampleDataTypeEnum sampleDataTypeEnum) => GetDisplayName(sampleDataTypeEnum.ToString());

		internal static string GetDisplayName(SampleDataType sampleDataType) => GetDisplayName(sampleDataType.ToEnum());

		// ScaleType Singular

		// TODO: For Scale implement overloads that take entity as such that unproxy is avoided.

		public static string GetScaleTypeDisplayNameSingular(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);

			return GetDisplayNameSingular(scale.ScaleType);
		}

		public static string GetDisplayNameSingular(ScaleType entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var enumValue = (ScaleTypeEnum)entity.ID;

			return GetDisplayNameSingular(enumValue);
		}

		public static string GetDisplayNameSingular(ScaleTypeEnum enumValue)
		{
			return GetDisplayName(enumValue.ToString());
		}

		// TODO: Perhaps remove this overload.
		internal static string GetScaleTypeDisplayNameSingular(string scaleTypeName)
		{
			return Resources.ResourceManager.GetString(scaleTypeName);
		}

		// ScaleType Plural

		public static string GetScaleTypeDisplayNamePlural(Scale scale)
		{
			if (scale == null) throw new NullException(() => scale);

			return GetDisplayNamePlural(scale.ScaleType);
		}

		public static string GetDisplayNamePlural(ScaleType entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var enumValue = (ScaleTypeEnum)entity.ID;

			return GetDisplayNamePlural(enumValue);
		}

		// Notice that the deepest overload has a different parameter than the singular variation.
		public static string GetDisplayNamePlural(ScaleTypeEnum scaleTypeEnum)
		{
			switch (scaleTypeEnum)
			{
				case ScaleTypeEnum.LiteralFrequency:
					return LiteralFrequencies;

				case ScaleTypeEnum.Factor:
					return Factors;

				case ScaleTypeEnum.Exponent:
					return Exponents;

				case ScaleTypeEnum.SemiTone:
					return SemiTones;

				case ScaleTypeEnum.Cent:
					return Cents;

				case ScaleTypeEnum.Undefined:
					// A direct call to ResourceManager.GetString does not crash if the key does not exist,
					// so do not throw an exception here.
					return GetDisplayNameSingular(scaleTypeEnum);

				default:
					throw new InvalidValueException(scaleTypeEnum);
			}
		}

		// TODO: Perhaps remove this overload
		internal static string GetScaleTypeDisplayNamePlural(string scaleTypeName)
		{
			var scaleTypeEnum = EnumParser.Parse<ScaleTypeEnum>(scaleTypeName);
			return GetDisplayNamePlural(scaleTypeEnum);
		}

		// SpeakerSetup

		public static string GetDisplayName(SpeakerSetup entity)
		{
			if (entity == null) throw new NullException(() => entity);

			var dimensionEnum = (SpeakerSetupEnum)entity.ID;

			return GetDisplayName(dimensionEnum);
		}

		public static string GetDisplayName(SpeakerSetupEnum enumValue)
		{
			return GetDisplayName(enumValue.ToString());
		}
	}
}