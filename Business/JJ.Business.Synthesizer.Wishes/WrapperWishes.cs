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

    [Obsolete]
    public static class AudioFileWrapperExtension
    {
        [Obsolete]
        public static int SizeOfSampleDataType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.SizeOfSampleDataType();
        }

        [Obsolete]
        public static int GetBits(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.GetBits();
        }

        [Obsolete]
        public static int GetFrameSize(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.GetFrameSize();
        }

        [Obsolete]
        public static int GetFrameCount(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.GetFrameCount();
        }

        [Obsolete]
        /// <inheritdoc cref="_fileextension"/>
        public static string GetFileExtension(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.GetFileExtension();
        }

        [Obsolete]
        public static double GetMaxAmplitude(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.GetMaxAmplitude();
        }

        [Obsolete]
        /// <inheritdoc cref="_headerlength"/>
        public static int GetHeaderLength(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.GetHeaderLength();
        }
    }

    // WrapperWishes From CalculationWishes

    [Obsolete]
    public static class CalculationWrapperExtension
    {
        [Obsolete]
        public static double Calculate(this OperatorWrapperBase wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        [Obsolete]
        public static double Calculate(this OperatorWrapperBase wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Operator.Calculate(time, channelIndex);
        }

        [Obsolete]
        public static double Calculate(this SampleOperatorWrapper wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        [Obsolete]
        public static double Calculate(this SampleOperatorWrapper wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Calculate(time, channelIndex);
        }

        [Obsolete]
        public static double Calculate(this CurveInWrapper wrapper, double time, ChannelEnum channelEnum)
            => Calculate(wrapper, time, channelEnum.ToIndex());

        [Obsolete]
        public static double Calculate(this CurveInWrapper wrapper, double time = 0, int channelIndex = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Calculate(time, channelIndex);
        }
    }

    // WrapperWishes From EnumWishes

    [Obsolete]
    public static class EnumWrapperExtensions
    {
        // SampleOperatorWrapper.AudioFileFormat

        [Obsolete]
        public static AudioFileFormat GetAudioFileFormat(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.AudioFileFormat;
        }

        [Obsolete]
        public static AudioFileFormatEnum GetAudioFileFormatEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetAudioFileFormatEnum();
        }

        [Obsolete]
        public static void SetAudioFileFormat(this SampleOperatorWrapper wrapper, AudioFileFormat enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.AudioFileFormat = enumEntity;
        }

        [Obsolete]
        public static void SetAudioFileFormatEnum(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetAudioFileFormatEnum(enumValue, repository);
        }

        [Obsolete]
        public static void SetAudioFileFormatEnum(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetAudioFileFormatEnum(enumValue, context);
        }

        // SampleOperatorWrapper.InterpolationType

        [Obsolete]
        public static InterpolationType GetInterpolationType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.InterpolationType;
        }

        [Obsolete]
        public static InterpolationTypeEnum GetInterpolationTypeEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetInterpolationTypeEnum();
        }

        [Obsolete]
        public static void SetInterpolationType(this SampleOperatorWrapper wrapper, InterpolationType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.InterpolationType = enumEntity;
        }

        [Obsolete]
        public static void SetInterpolationTypeEnum(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IInterpolationTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, repository);
        }

        [Obsolete]
        public static void SetInterpolationTypeEnum(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.SampleDataType

        [Obsolete]
        public static SampleDataType GetSampleDataType(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SampleDataType;
        }

        [Obsolete]
        public static SampleDataTypeEnum GetSampleDataTypeEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSampleDataTypeEnum();
        }

        [Obsolete]
        public static void SetSampleDataType(this SampleOperatorWrapper wrapper, SampleDataType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SampleDataType = enumEntity;
        }

        [Obsolete]
        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, repository);
        }

        [Obsolete]
        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.SpeakerSetup

        [Obsolete]
        public static SpeakerSetup GetSpeakerSetup(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SpeakerSetup;
        }

        [Obsolete]
        public static SpeakerSetupEnum GetSpeakerSetupEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSpeakerSetupEnum();
        }

        [Obsolete]
        public static void SetSpeakerSetup(this SampleOperatorWrapper wrapper, SpeakerSetup enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SpeakerSetup = enumEntity;
        }

        [Obsolete]
        public static void SetSpeakerSetupEnum(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, repository);
        }

        [Obsolete]
        public static void SetSpeakerSetupEnum(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, context);
        }

        // CurveInWrapper.NodeType

        [Obsolete]
        public static NodeType TryGetNodeType(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SpecialEnumWishes.TryGetNodeType(wrapper.Curve);
        }

        [Obsolete]
        public static NodeTypeEnum TryGetNodeTypeEnum(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return SpecialEnumWishes.TryGetNodeTypeEnum(wrapper.Curve);
        }

        [Obsolete]
        public static void SetNodeType(this CurveInWrapper wrapper, NodeType nodeType)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            SpecialEnumWishes.SetNodeType(wrapper.Curve, nodeType);
        }

        [Obsolete]
        public static void SetNodeTypeEnum(
            this CurveInWrapper wrapper, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            SpecialEnumWishes.SetNodeTypeEnum(wrapper.Curve, nodeTypeEnum, context);
        }
    }

    // WrapperWishes From NameWishes

    [Obsolete]
    public static class NameWrapperExtension
    {
        [Obsolete]
        /// <inheritdoc cref="docs._names"/>
        public static CurveInWrapper SetName(this CurveInWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");

            wrapper.Operator().SetName(name);
            wrapper.Curve.SetName(name);

            return wrapper;
        }

        [Obsolete]
        /// <inheritdoc cref="docs._names"/>
        public static SampleOperatorWrapper SetName(this SampleOperatorWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");

            wrapper.Operator().SetName(name);
            wrapper.Sample.SetName(name);

            return wrapper;
        }

        [Obsolete]
        /// <inheritdoc cref="docs._names"/>
        public static OperatorWrapperBase SetName(this OperatorWrapperBase wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Operator.SetName(name);
            return wrapper;
        }
    }

    // WrapperWishes From RelatedObjectsWishes

    [Obsolete]
    public static class RelatedObjectWrapperExtensions
    {
        [Obsolete]
        /// <inheritdoc cref="_relatedobjectextensions"/>
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        [Obsolete]
        /// <inheritdoc cref="_relatedobjectextensions"/>
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        [Obsolete]
        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Input);
        }

        [Obsolete]
        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Operator);
        }

        [Obsolete]
        /// <inheritdoc cref="_getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new CurveInWrapper(entity.AsCurveIn);
        }

        [Obsolete]
        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Input);
        }

        [Obsolete]
        /// <inheritdoc cref="_getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Operator);
        }

        [Obsolete]
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
        [Obsolete]
        public CurveInWrapper GetCurveWrapper() => _wrappedOutlet.GetCurveWrapper();

        /// <inheritdoc cref="_getsamplewrapper" />
        [Obsolete]
        public SampleOperatorWrapper GetSampleWrapper() => _wrappedOutlet.GetSampleWrapper();
    }

    // WrapperWishes From StringifyWishes

    [Obsolete]
    public static class StringifyWrapperExtensions
    {
        [Obsolete]
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this OperatorWrapperBase wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Operator);
        }

        [Obsolete]
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this SampleOperatorWrapper wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Result);
        }

        [Obsolete]
        /// <inheritdoc cref="_stringify"/>
        public static string Stringify(this CurveInWrapper wrapper, bool singleLine = false, bool mustUseShortOperators = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, mustUseShortOperators).StringifyRecursive(wrapper.Result);
        }
    }

    // WrapperWishes From ValidationWishes

    [Obsolete]
    public static class ValidationWrapperExtension
    {
        [Obsolete]
        public static Result Validate(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Operator.Validate(recursive);
        }

        [Obsolete]
        public static Result Validate(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Validate(recursive);
        }

        [Obsolete]
        public static Result Validate(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Validate(recursive);
        }

        [Obsolete]
        public static void Assert(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Operator.Assert(recursive);
        }

        [Obsolete]
        public static void Assert(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Result.Assert(recursive);
        }

        [Obsolete]
        public static void Assert(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Result.Assert(recursive);
        }

        [Obsolete]
        public static IList<string> GetWarnings(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Operator.GetWarnings(recursive);
        }

        [Obsolete]
        public static IList<string> GetWarnings(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.GetWarnings(recursive);
        }

        [Obsolete]
        public static IList<string> GetWarnings(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.GetWarnings(recursive);
        }
    }

    // From WavHeaderWishes

    [Obsolete]
    public static class WavHeaderWrapperExtensions
    {
        [Obsolete]
        public static WavHeaderStruct GetWavHeader(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.GetInfo().GetWavHeader();
        }

        [Obsolete]
        public static AudioFileInfoWish GetInfo(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.GetInfo();
        }
    }
}