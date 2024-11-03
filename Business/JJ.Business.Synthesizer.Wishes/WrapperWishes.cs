using System;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Persistence.Synthesizer;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Structs;
// ReSharper disable NotResolvedInText
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes
{
    // WrapperWishes

    // Moving wrapper-related extension methods here, marking them as obsolete,
    // still available for compatibility where needed.

    // WrapperWishes From AudioFileWishes

    public static partial class AudioFileExtensionWishes
    {
        public static int SizeOfSampleDataType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SizeOfSampleDataType(wrapper.Sample);
        }

        public static int GetBits(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetBits(wrapper.Sample);
        }

        public static int GetFrameSize(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFrameSize(wrapper.Sample);
        }

        public static int GetFrameCount(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFrameCount(wrapper.Sample);
        }

        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetFileExtension(wrapper.Sample);
        }

        public static double GetMaxAmplitude(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetMaxAmplitude(wrapper.Sample);
        }

        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetHeaderLength(wrapper.Sample);
        }
    }

    // WrapperWishes From CalculationWishes

    public static partial class CalculationExtensionWishes
    {
        public static double Calculate(this OperatorWrapperBase wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        public static double Calculate(this OperatorWrapperBase wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Calculate(wrapper.Operator, time, channelIndex);
        }

        public static double Calculate(this SampleOperatorWrapper wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        public static double Calculate(this SampleOperatorWrapper wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Calculate(wrapper.Result, time, channelIndex);
        }

        public static double Calculate(this CurveInWrapper wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        public static double Calculate(this CurveInWrapper wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));

            return Calculate(wrapper.Result, time, channelIndex);
        }
    }

    // WrapperWishes From EnumWishes

    public static partial class AlternativeEntryPointEnumExtensionWishes
    {

        // SampleOperatorWrapper.AudioFileFormat

        public static AudioFileFormat GetAudioFileFormat(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.AudioFileFormat;
        }

        public static AudioFileFormatEnum GetAudioFileFormatEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetAudioFileFormatEnum();
        }

        public static void SetAudioFileFormat(this SampleOperatorWrapper wrapper, AudioFileFormat enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.AudioFileFormat = enumEntity;
        }

        public static void SetAudioFileFormatEnum(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetAudioFileFormatEnum(enumValue, repository);
        }

        public static void SetAudioFileFormatEnum(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetAudioFileFormatEnum(enumValue, context);
        }

        // SampleOperatorWrapper.InterpolationType

        public static InterpolationType GetInterpolationType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.InterpolationType;
        }

        public static InterpolationTypeEnum GetInterpolationTypeEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetInterpolationTypeEnum();
        }

        public static void SetInterpolationType(this SampleOperatorWrapper wrapper, InterpolationType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.InterpolationType = enumEntity;
        }

        public static void SetInterpolationTypeEnum(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IInterpolationTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, repository);
        }

        public static void SetInterpolationTypeEnum(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.SampleDataType

        public static SampleDataType GetSampleDataType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SampleDataType;
        }

        public static SampleDataTypeEnum GetSampleDataTypeEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSampleDataTypeEnum();
        }

        public static void SetSampleDataType(this SampleOperatorWrapper wrapper, SampleDataType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SampleDataType = enumEntity;
        }

        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, repository);
        }

        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.SpeakerSetup

        public static SpeakerSetup GetSpeakerSetup(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SpeakerSetup;
        }

        public static SpeakerSetupEnum GetSpeakerSetupEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSpeakerSetupEnum();
        }

        public static void SetSpeakerSetup(this SampleOperatorWrapper wrapper, SpeakerSetup enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SpeakerSetup = enumEntity;
        }

        public static void SetSpeakerSetupEnum(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, repository);
        }

        public static void SetSpeakerSetupEnum(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, context);
        }

        // CurveInWrapper.NodeType

        public static NodeType TryGetNodeType(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SpecialEnumWishes.TryGetNodeType(wrapper.Curve);
        }

        public static NodeTypeEnum TryGetNodeTypeEnum(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SpecialEnumWishes.TryGetNodeTypeEnum(wrapper.Curve);
        }

        public static void SetNodeType(this CurveInWrapper wrapper, NodeType nodeType)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            SpecialEnumWishes.SetNodeType(wrapper.Curve, nodeType);
        }

        public static void SetNodeTypeEnum(
            this CurveInWrapper wrapper, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            SpecialEnumWishes.SetNodeTypeEnum(wrapper.Curve, nodeTypeEnum, context);
        }
    }

    // WrapperWishes From NameWishes

    public static partial class NameExtensionWishes
    {

        /// <inheritdoc cref="docs._names"/>
        public static CurveInWrapper SetName(this CurveInWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");

            wrapper.Operator().SetName(name);
            wrapper.Curve.SetName(name);

            return wrapper;
        }

        /// <inheritdoc cref="docs._names"/>
        public static SampleOperatorWrapper SetName(this SampleOperatorWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");

            wrapper.Operator().SetName(name);
            wrapper.Sample.SetName(name);

            return wrapper;
        }

        /// <inheritdoc cref="docs._names"/>
        public static OperatorWrapperBase SetName(this OperatorWrapperBase wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Operator.SetName(name);
            return wrapper;
        }
    }

    // WrapperWishes From RelatedObjectsWishes

    /// <inheritdoc cref="_relatedobjectextensions"/>
    public static partial class RelatedObjectExtensionWishes
    {
        /// <inheritdoc cref="_relatedobjectextensions"/>
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        /// <inheritdoc cref="_relatedobjectextensions"/>
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Input);
        }

        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Operator);
        }

        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new CurveInWrapper(entity.AsCurveIn);
        }

        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Input);
        }

        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Operator);
        }

        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new SampleOperatorWrapper(entity.AsSampleOperator);
        }
    }

    public partial class FluentOutlet
    {
        /// <inheritdoc cref="_getcurvewrapper"/>
        public CurveInWrapper GetCurveWrapper() => _wrappedOutlet.GetCurveWrapper();

        /// <inheritdoc cref="_getsamplewrapper" />
        public SampleOperatorWrapper GetSampleWrapper() => _wrappedOutlet.GetSampleWrapper();
    }

    // WrapperWishes From StringifyWishes

    public static partial class StringifyExtensionWishes
    {
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this OperatorWrapperBase wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Operator);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this SampleOperatorWrapper wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Result);
        }

        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this CurveInWrapper wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Result);
        }
    }

    // WrapperWishes From ValidationWishes

    public static partial class ValidationExtensionWishes
    {
        public static Result Validate(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate(wrapper.Operator, recursive);
        }

        public static Result Validate(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate(wrapper.Result, recursive);
        }

        public static Result Validate(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return Validate(wrapper.Result, recursive);
        }

        public static void Assert(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert(wrapper.Operator, recursive);
        }

        public static void Assert(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert(wrapper.Result, recursive);
        }

        public static void Assert(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            Assert(wrapper.Result, recursive);
        }

        public static IList<string> GetWarnings(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings(wrapper.Operator, recursive);
        }

        public static IList<string> GetWarnings(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings(wrapper.Result, recursive);
        }

        public static IList<string> GetWarnings(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetWarnings(wrapper.Result, recursive);
        }
    }

    // From WavHeaderWishes

    public static partial class WavHeaderExtensionWishes_HeaderFromObjects
    {
        public static WavHeaderStruct GetWavHeader(this SampleOperatorWrapper wrapper)
            => WavHeaderExtensionWishes_GetInfo.GetInfo(wrapper).GetWavHeader();
    }

    public static partial class WavHeaderExtensionWishes_GetInfo
    {
        public static AudioFileInfoWish GetInfo(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return GetInfo(wrapper.Sample);
        }
    }
}