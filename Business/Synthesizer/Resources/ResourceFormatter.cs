using System;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;
using JJ.Framework.Exceptions;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Framework.Common;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Resources
{
    public static class ResourceFormatter
    {
        // Resource texts without placeholders

        public static string A => Resources.A;
        public static string Absolute => Resources.Absolute;
        public static string Add => Resources.Add;
        public static string AddToCurrentPatches => Resources.AddToCurrentPatches;
        public static string Alias => Resources.Alias;
        public static string AllPassFilter => Resources.AllPassFilter;
        public static string Amplifier => Resources.Amplifier;
        public static string And => Resources.And;
        public static string ApplicationName => Resources.ApplicationName;
        public static string AttackDuration => Resources.AttackDuration;
        public static string AudioFileFormat => Resources.AudioFileFormat;
        public static string AudioFileOutput => Resources.AudioFileOutput;
        public static string AudioFileOutputList => Resources.AudioFileOutputList;
        public static string AudioOutput => Resources.AudioOutput;
        public static string Average => Resources.Average;
        public static string AverageFollower => Resources.AverageFollower;
        public static string AverageOverDimension => Resources.AverageOverDimension;
        public static string AverageOverInlets => Resources.AverageOverInlets;
        public static string B => Resources.B;
        public static string Balance => Resources.Balance;
        public static string BandPassFilterConstantPeakGain => Resources.BandPassFilterConstantPeakGain;
        public static string BandPassFilterConstantTransitionGain => Resources.BandPassFilterConstantTransitionGain;
        public static string BandWidth => Resources.BandWidth;
        public static string Base => Resources.Base;
        public static string BaseFrequency => Resources.BaseFrequency;
        public static string Block => Resources.Block;
        public static string Brightness => Resources.Brightness;
        public static string BrightnessModulationDepth => Resources.BrightnessModulationDepth;
        public static string BrightnessModulationSpeed => Resources.BrightnessModulationSpeed;
        public static string Byte => Resources.Byte;
        public static string BytesToSkip => Resources.BytesToSkip;
        public static string Cache => Resources.Cache;
        public static string CannotDeleteBecauseHasReferences => Resources.CannotDeleteBecauseHasReferences;
        public static string Cent => Resources.Cent;
        public static string CenterFrequency => Resources.CenterFrequency;
        public static string Cents => Resources.Cents;
        public static string ChangeTrigger => Resources.ChangeTrigger;
        public static string Channel => Resources.Channel;
        public static string ChannelCountDoesNotMatchSpeakerSetup => Resources.ChannelCountDoesNotMatchSpeakerSetup;
        public static string Channels => Resources.Channels;
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
        public static string CurrentPatches => Resources.CurrentPatches;
        public static string Curve => Resources.Curve;
        public static string Curves => Resources.Curves;
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
        public static string Divide => Resources.Divide;
        public static string Document => Resources.Document;
        public static string DocumentList => Resources.DocumentList;
        public static string DocumentName => Resources.DocumentName;
        public static string Documents => Resources.Documents;
        public static string DocumentTree => Resources.DocumentTree;
        public static string Duration => Resources.Duration;
        public static string Else => Resources.Else;
        public static string End => Resources.End;
        public static string EndTime => Resources.EndTime;
        public static string Equal => Resources.Equal;
        public static string Exponent => Resources.Exponent;
        public static string Exponents => Resources.Exponents;
        public static string Factor => Resources.Factor;
        public static string Factors => Resources.Factors;
        public static string FilePath => Resources.FilePath;
        public static string Frequencies => Resources.Frequencies;
        public static string Frequency => Resources.Frequency;
        public static string From => Resources.From;
        public static string GetDimension => Resources.GetDimension;
        public static string GreaterThan => Resources.GreaterThan;
        public static string GreaterThanOrEqual => Resources.GreaterThanOrEqual;
        public static string Group => Resources.Group;
        public static string GroupName => Resources.GroupName;
        public static string Harmonic => Resources.Harmonic;
        public static string Hermite => Resources.Hermite;
        public static string High => Resources.High;
        public static string HigherDocument => Resources.HigherDocument;
        public static string HighPassFilter => Resources.HighPassFilter;
        public static string HighShelfFilter => Resources.HighShelfFilter;
        public static string Hold => Resources.Hold;
        public static string If => Resources.If;
        public static string Inlet => Resources.Inlet;
        public static string InletListIndexesAreNotUnique => Resources.InletListIndexesAreNotUnique;
        public static string InletNamesAreNotUnique => Resources.InletNamesAreNotUnique;
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
        public static string IsActive => Resources.IsActive;
        public static string IsObsolete => Resources.IsObsolete;
        public static string Left => Resources.Left;
        public static string LessThan => Resources.LessThan;
        public static string LessThanOrEqual => Resources.LessThanOrEqual;
        public static string Line => Resources.Line;
        public static string ListIndex => Resources.ListIndex;
        public static string LiteralFrequencies => Resources.LiteralFrequencies;
        public static string LiteralFrequency => Resources.LiteralFrequency;
        public static string Loop => Resources.Loop;
        public static string LoopEndMarker => Resources.LoopEndMarker;
        public static string LoopStartMarker => Resources.LoopStartMarker;
        public static string Low => Resources.Low;
        public static string LowerDocument => Resources.LowerDocument;
        public static string LowerDocuments => Resources.LowerDocuments;
        public static string LowPassFilter => Resources.LowPassFilter;
        public static string LowShelfFilter => Resources.LowShelfFilter;
        public static string Max => Resources.Max;
        public static string MaxConcurrentNotes => Resources.MaxConcurrentNotes;
        public static string MaxFollower => Resources.MaxFollower;
        public static string MaxFrequency => Resources.MaxFrequency;
        public static string MaxOverDimension => Resources.MaxOverDimension;
        public static string MaxOverInlets => Resources.MaxOverInlets;
        public static string Min => Resources.Min;
        public static string MinFollower => Resources.MinFollower;
        public static string MinFrequency => Resources.MinFrequency;
        public static string MinOverDimension => Resources.MinOverDimension;
        public static string MinOverInlets => Resources.MinOverInlets;
        public static string Mono => Resources.Mono;
        public static string Multiply => Resources.Multiply;
        public static string MultiplyWithOrigin => Resources.MultiplyWithOrigin;
        public static string Negative => Resources.Negative;
        public static string Node => Resources.Node;
        public static string Nodes => Resources.Nodes;
        public static string NodeType => Resources.NodeType;
        public static string Noise => Resources.Noise;
        public static string Not => Resources.Not;
        public static string NotchFilter => Resources.NotchFilter;
        public static string NoteDuration => Resources.NoteDuration;
        public static string NotEqual => Resources.NotEqual;
        public static string NoteStart => Resources.NoteStart;
        public static string Number => Resources.Number;
        public static string Octave => Resources.Octave;
        public static string Off => Resources.Off;
        public static string Offset => Resources.Offset;
        public static string OneOverX => Resources.OneOverX;
        public static string Operand => Resources.Operand;
        public static string Operator => Resources.Operator;
        public static string OperatorType => Resources.OperatorType;
        public static string OperatorTypeName => Resources.OperatorTypeName;
        public static string Or => Resources.Or;
        public static string Origin => Resources.Origin;
        public static string OriginalLocation => Resources.OriginalLocation;
        public static string Outlet => Resources.Outlet;
        public static string OutletListIndexesAreNotUnique => Resources.OutletListIndexesAreNotUnique;
        public static string OutletNamesAreNotUnique => Resources.OutletNamesAreNotUnique;
        public static string Outlets => Resources.Outlets;
        public static string OutletType => Resources.OutletType;
        public static string PassThrough => Resources.PassThrough;
        public static string Patch => Resources.Patch;
        public static string Patches => Resources.Patches;
        public static string PatchInlet => Resources.PatchInlet;
        public static string PatchOutlet => Resources.PatchOutlet;
        public static string PeakingEQFilter => Resources.PeakingEQFilter;
        public static string Play => Resources.Play;
        public static string Position => Resources.Position;
        public static string Power => Resources.Power;
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
        public static string Reset => Resources.Reset;
        public static string Result => Resources.Result;
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
        public static string SelectANodeFirst => Resources.SelectANodeFirst;
        public static string SelectAnOperatorFirst => Resources.SelectAnOperatorFirst;
        public static string SelectAnOperatorWithASingleOutlet => Resources.SelectAnOperatorWithASingleOutlet;
        public static string SemiTone => Resources.SemiTone;
        public static string SemiTones => Resources.SemiTones;
        public static string SetDimension => Resources.SetDimension;
        public static string Shift => Resources.Shift;
        public static string Signal => Resources.Signal;
        public static string Sine => Resources.Sine;
        public static string Single => Resources.Single;
        public static string Skip => Resources.Skip;
        public static string SliceLength => Resources.SliceLength;
        public static string Sort => Resources.Sort;
        public static string SortOverDimension => Resources.SortOverDimension;
        public static string SortOverInlets => Resources.SortOverInlets;
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
        public static string Time => Resources.Time;
        public static string TimeMultiplier => Resources.TimeMultiplier;
        public static string TimePower => Resources.TimePower;
        public static string ToggleTrigger => Resources.ToggleTrigger;
        public static string Tone => Resources.Tone;
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
        public static string Wav => Resources.Wav;
        public static string Width => Resources.Width;
        public static string X => Resources.X;
        public static string Y => Resources.Y;

        // Resource texts with placeholders

        public static string CannotChangeInletsBecauseOneIsStillFilledIn(int oneBasedInletNumber) => string.Format(Resources.CannotChangeInletCountBecauseOneIsStillFilledIn, oneBasedInletNumber);
        public static string CannotChangeOutletsBecauseOneIsStillFilledIn(int oneBasedOutletNumber) => string.Format(Resources.CannotChangeOutletCountBecauseOneIsStillFilledIn, oneBasedOutletNumber);
        [Obsolete("Replace with better delete validation messages.")]
        public static string CannotDeleteCurveBecauseHasOperators(string name) => string.Format(Resources.CannotDeleteCurveBecauseHasOperators, name);
        [Obsolete("Replace with better delete validation messages.")]
        public static string CannotDeleteSampleBecauseHasOperators(string name) => string.Format(Resources.CannotDeleteSampleBecauseHasOperators, name);
        [Obsolete("Replace with better delete validation messages.")]
        public static string DocumentIsDependentOnDocument(string dependentDocumentName, string dependentOnDocumentName) => string.Format(Resources.DocumentIsDependentOnDocument, dependentDocumentName, dependentOnDocumentName);
        public static string GetDimensionWithPlaceholder(string dimension) => string.Format(Resources.GetDimensionWithPlaceholder, dimension);
        public static string OperatorHasNoInletsFilledIn_WithOperatorName(string operatorName) => string.Format(Resources.OperatorHasNoInletsFilledIn_WithOperatorName, operatorName);
        /// <summary> Note: When OperatorTypeEnum equals Undefined it will return a text like: "Undefined operator named '...' does not have ... filled in." </summary>
        public static string InletNotSet(OperatorTypeEnum operatorTypeEnum, string operatorName, string operandName) => string.Format(Resources.InletNotSet, operatorTypeEnum, operatorName, operandName);
        public static string InletNotSet(string operatorTypeName, string operatorName, string operandName) => string.Format(Resources.InletNotSet, operatorTypeName, operatorName, operandName);
        public static string InletPropertyDoesNotMatchWithUnderlyingPatch(string propertyDisplayName) => string.Format(Resources.InletPropertyDoesNotMatchWithUnderlyingPatch, propertyDisplayName);
        public static string MustBePowerOf2(string frequencyCount) => string.Format(Resources.MustBePowerOf2, frequencyCount);

        public static string NamesNotUnique_WithEntityTypeNameAndNames(string entityTypeDisplayName, [NotNull] IList<string> duplicateNames)
        {
            if (duplicateNames == null) throw new NullException(() => duplicateNames);

            string formattedDuplicateNames = string.Join(", ", duplicateNames.Select(x => $"'{x}'"));

            return string.Format(Resources.NamesNotUnique_WithEntityTypeNameAndNames, entityTypeDisplayName, formattedDuplicateNames);
        }

        public static string OperatorIsCircularWithName(string name) => string.Format(Resources.OperatorIsCircularWithName, name);
        public static string OperatorIsInGraphButNotInList(string operatorIdentifier) => string.Format(Resources.OperatorIsInGraphButNotInList, operatorIdentifier);
        public static string OperatorPatchIsNotTheExpectedPatch(string operatorName, string expectedPatchName) => string.Format(Resources.OperatorPatchIsNotTheExpectedPatch, operatorName, expectedPatchName);
        public static string OutletPropertyDoesNotMatchWithUnderlyingPatch(string propertyDisplayName) => string.Format(Resources.OutletPropertyDoesNotMatchWithUnderlyingPatch, propertyDisplayName);
        public static string SampleNotActive(string sampleName) => string.Format(Resources.SampleNotActive, sampleName);
        public static string SampleNotLoaded(string sampleName) => string.Format(Resources.SampleNotLoaded, sampleName);
        public static string SetDimensionWithPlaceholder(string dimension) => string.Format(Resources.SetDimensionWithPlaceholder, dimension);

        // Generic methods that could return several different resource text

        /// <summary> You can use this overload if the object resourceName's ToString converts it to the resource key. </summary>
        public static string GetText(object resourceName)
        {
            return GetText(Convert.ToString(resourceName));
        }

        public static string GetText(string resourceName)
        {
            string str = Resources.ResourceManager.GetString(resourceName);

            if (string.IsNullOrEmpty(str))
            {
                str = resourceName;
            }

            return str;
        }

        public static string GetText([NotNull] Expression<Func<object>> resourceNameExpression)
        {
            if (resourceNameExpression == null) throw new NullException(() => resourceNameExpression);

            string resourceName = ExpressionHelper.GetName(resourceNameExpression);
            string str = GetText(resourceName);
            return str;
        }

        // Dimension

        public static string GetText([NotNull] Dimension entity)
        {
            if (entity == null) throw new NullException(() => entity);

            DimensionEnum dimensionEnum = (DimensionEnum)entity.ID;

            return GetText(dimensionEnum);
        }

        public static string GetText(DimensionEnum enumValue)
        {
            return GetText(enumValue.ToString());
        }

        // InterpolationType

        public static string GetText([NotNull] InterpolationType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            InterpolationTypeEnum dimensionEnum = (InterpolationTypeEnum)entity.ID;

            return GetText(dimensionEnum);
        }

        public static string GetText(InterpolationTypeEnum enumValue)
        {
            return GetText(enumValue.ToString());
        }

        // OperatorType

        public static string GetOperatorTypeText([NotNull] Operator op)
        {
            if (op == null) throw new NullException(() => op);
            return GetText(op.GetOperatorTypeEnum());
        }

        public static string GetText([NotNull] OperatorType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            OperatorTypeEnum enumValue = (OperatorTypeEnum)entity.ID;

            return GetText(enumValue);
        }

        public static string GetText(OperatorTypeEnum enumValue)
        {
            return GetText(enumValue.ToString());
        }

        // SpeakerSetup

        public static string GetText([NotNull] SpeakerSetup entity)
        {
            if (entity == null) throw new NullException(() => entity);

            SpeakerSetupEnum dimensionEnum = (SpeakerSetupEnum)entity.ID;

            return GetText(dimensionEnum);
        }

        public static string GetText(SpeakerSetupEnum enumValue)
        {
            return GetText(enumValue.ToString());
        }

        // ResampleInterpolationType

        public static string GetText(ResampleInterpolationTypeEnum enumValue)
        {
            return GetText(enumValue.ToString());
        }

        // CollectionRecalculation

        public static string GetText(CollectionRecalculationEnum enumValue)
        {
            return GetText(enumValue.ToString());
        }

        // ScaleType Singular

        // TODO: For Scale implement overloads that take entity as such that unproxy is avoided.

        public static string GetScaleTypeTextSingular([NotNull] Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            return GetTextSingular(scale.ScaleType);
        }

        public static string GetTextSingular([NotNull] ScaleType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            ScaleTypeEnum enumValue = (ScaleTypeEnum)entity.ID;

            return GetTextSingular(enumValue);
        }

        public static string GetTextSingular(ScaleTypeEnum enumValue)
        {
            return GetText(enumValue.ToString());
        }

        // TODO: Perhaps remove this overload.
        internal static string GetScaleTypeTextSingular(string scaleTypeName)
        {
            return Resources.ResourceManager.GetString(scaleTypeName);
        }

        // ScaleType Plural

        public static string GetScaleTypeTextPlural([NotNull] Scale scale)
        {
            if (scale == null) throw new NullException(() => scale);

            return GetTextPlural(scale.ScaleType);
        }

        public static string GetTextPlural([NotNull] ScaleType entity)
        {
            if (entity == null) throw new NullException(() => entity);

            ScaleTypeEnum enumValue = (ScaleTypeEnum)entity.ID;

            return GetTextPlural(enumValue);
        }

        // Notice that the deepest overload has a different parameter than the singular variation.
        public static string GetTextPlural(ScaleTypeEnum scaleTypeEnum)
        {
            switch (scaleTypeEnum)
            {
                case ScaleTypeEnum.LiteralFrequency:
                    return GetText(PropertyNames.LiteralFrequencies);

                case ScaleTypeEnum.Factor:
                    return GetText(PropertyNames.Factors);

                case ScaleTypeEnum.Exponent:
                    return GetText(PropertyNames.Exponents);

                case ScaleTypeEnum.SemiTone:
                    return GetText(PropertyNames.SemiTones);

                case ScaleTypeEnum.Cent:
                    return GetText(PropertyNames.Cents);

                case ScaleTypeEnum.Undefined:
                    // A direct call to ResourceManager.GetString does not crash if the key does not exist,
                    // so do not throw an exception here.
                    return GetTextSingular(scaleTypeEnum);

                default:
                    throw new InvalidValueException(scaleTypeEnum);
            }
        }

        // TODO: Perhaps remove this overload
        internal static string GetScaleTypeTextPlural(string scaleTypeName)
        {
            ScaleTypeEnum scaleTypeEnum = EnumHelper.Parse<ScaleTypeEnum>(scaleTypeName);
            return GetTextPlural(scaleTypeEnum);
        }
    }
}