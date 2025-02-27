using System;
using System.Collections.Generic;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Obsolete.WrappersObsoleteMessages;

// ReSharper disable NotResolvedInText
// ReSharper disable InvokeAsExtensionMethod

namespace JJ.Business.Synthesizer.Wishes.Obsolete
{
    // WrapperWishes

    // Moving wrapper-related extension methods here, marking them as obsolete,
    // still available for compatibility where needed.

    // WrapperWishes From AudioFileWishes

    internal static class WrappersObsoleteMessages
    {
        public const string ObsoleteMessage =
            "Direct use of wrappers is discouraged. " +
            "Put the wrapper inside brackets if you can, like this: _[myWrapper] " +
            "Otherwise you can ignore this message.";
    }

    [Obsolete(ObsoleteMessage)]
    public static class AudioFileWrapperExtension
    {
        [Obsolete(ObsoleteMessage)]
        public static int SizeOfBitDepth(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.SizeOfBitDepth();
        }

        [Obsolete(ObsoleteMessage)]
        public static int GetBits(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.Bits();
        }

        [Obsolete(ObsoleteMessage)]
        public static int GetFrameSize(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.FrameSize();
        }

        [Obsolete(ObsoleteMessage)]
        public static int GetFrameCount(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.FrameCount();
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._fileextension"/>
        public static string GetFileExtension(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.FileExtension();
        }

        [Obsolete(ObsoleteMessage)]
        public static double GetNominalMax(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.MaxAmplitude();
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._headerlength"/>
        public static int GetHeaderLength(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.HeaderLength();
        }
    }

    // WrapperWishes From CalculationWishes

    [Obsolete(ObsoleteMessage)]
    public static class CalculationWrapperExtension
    {
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this OperatorWrapperBase wrapper, double time, ChannelEnum channelEnum)
        {
            if (!Has(channelEnum)) throw new Exception($"{nameof(channelEnum)} not defined.");
            return Calculate(wrapper, time, channelEnum.Channel().Value);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this OperatorWrapperBase wrapper, double time = 0, int channel = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Operator.Calculate(time, channel);
        }

        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this SampleOperatorWrapper wrapper, double time, ChannelEnum channelEnum)
        {
            if (!Has(channelEnum)) throw new Exception($"{nameof(channelEnum)} not defined.");
            return Calculate(wrapper, time, channelEnum.Channel().Value);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this SampleOperatorWrapper wrapper, double time = 0, int channel = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Calculate(time, channel);
        }

        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this CurveInWrapper wrapper, double time, ChannelEnum channelEnum)
        {
            if (!Has(channelEnum)) throw new Exception($"{nameof(channelEnum)} not defined.");
            return Calculate(wrapper, time, channelEnum.Channel().Value);
        }
        
        [Obsolete(ObsoleteMessage)]
        public static double Calculate(this CurveInWrapper wrapper, double time = 0, int channel = 0)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Calculate(time, channel);
        }
    }

    // WrapperWishes From EnumWishes

    [Obsolete(ObsoleteMessage)]
    public static class EnumWrapperExtensions
    {
        // SampleOperatorWrapper.AudioFileFormat

        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat GetAudioFormatEnumEntity(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.AudioFileFormat;
        }

        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormatEnum GetAudioFormat(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetAudioFileFormatEnum();
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetAudioFormatEnumEntity(this SampleOperatorWrapper wrapper, AudioFileFormat enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.AudioFileFormat = enumEntity;
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetAudioFormat(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IAudioFileFormatRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetAudioFileFormatEnum(enumValue, repository);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetAudioFormat(
            this SampleOperatorWrapper wrapper, AudioFileFormatEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.AudioFormat(enumValue, context);
        }

        // SampleOperatorWrapper.InterpolationType

        [Obsolete(ObsoleteMessage)]
        public static InterpolationType GetInterpolationEnumEntity(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.InterpolationType;
        }

        [Obsolete(ObsoleteMessage)]
        public static InterpolationTypeEnum GetInterpolation(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetInterpolationTypeEnum();
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetInterpolationEnumEntity(this SampleOperatorWrapper wrapper, InterpolationType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.InterpolationType = enumEntity;
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetInterpolation(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IInterpolationTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, repository);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetInterpolation(
            this SampleOperatorWrapper wrapper, InterpolationTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetInterpolationTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.SampleDataType

        [Obsolete(ObsoleteMessage)]
        public static SampleDataType GetSampleDataTypeEnumEntity(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SampleDataType;
        }

        [Obsolete(ObsoleteMessage)]
        public static SampleDataTypeEnum GetSampleDataTypeEnum(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSampleDataTypeEnum();
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnumEntity(this SampleOperatorWrapper wrapper, SampleDataType enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SampleDataType = enumEntity;
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, ISampleDataTypeRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, repository);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSampleDataTypeEnum(
            this SampleOperatorWrapper wrapper, SampleDataTypeEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSampleDataTypeEnum(enumValue, context);
        }

        // SampleOperatorWrapper.SpeakerSetup

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetup GetSpeakerSetupEnumEntity(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.SpeakerSetup;
        }

        [Obsolete(ObsoleteMessage)]
        public static SpeakerSetupEnum GetSpeakerSetup(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            return wrapper.Sample.GetSpeakerSetupEnum();
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSpeakerSetupEnumEntity(this SampleOperatorWrapper wrapper, SpeakerSetup enumEntity)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SpeakerSetup = enumEntity;
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSpeakerSetup(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, ISpeakerSetupRepository repository)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, repository);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetSpeakerSetup(
            this SampleOperatorWrapper wrapper, SpeakerSetupEnum enumValue, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Sample == null) throw new NullException(() => wrapper.Sample);
            wrapper.Sample.SetSpeakerSetupEnum(enumValue, context);
        }

        // CurveInWrapper.NodeType

        [Obsolete(ObsoleteMessage)]
        public static NodeType TryGetNodeTypeEnumEntity(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Curve == null) throw new NullException(() => wrapper.Curve);
            return wrapper.Curve.TryGetNodeTypeEnumEntity();
        }

        [Obsolete(ObsoleteMessage)]
        public static NodeTypeEnum TryGetNodeType(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return EnumExtensionWishes.TryGetNodeType(wrapper.Curve);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetNodeTypeEnumEntity(this CurveInWrapper wrapper, NodeType nodeType)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Curve == null) throw new NullException(() => wrapper.Curve);
            wrapper.Curve.SetNodeTypeEnumEntity(nodeType);
        }

        [Obsolete(ObsoleteMessage)]
        public static void SetNodeType(
            this CurveInWrapper wrapper, NodeTypeEnum nodeTypeEnum, IContext context = null)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            EnumExtensionWishes.SetNodeType(wrapper.Curve, nodeTypeEnum, context);
        }
    }

    // WrapperWishes From NameWishes

    [Obsolete(ObsoleteMessage)]
    public static class NameWrapperExtension
    {
        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._names"/>
        public static CurveInWrapper SetName(this CurveInWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");

            wrapper.Operator().SetName(name);
            wrapper.Curve.SetName(name);

            return wrapper;
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._names"/>
        public static SampleOperatorWrapper SetName(this SampleOperatorWrapper wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Operator() == null) throw new ArgumentNullException("wrapper.Operator()");

            wrapper.Operator().SetName(name);
            wrapper.Sample.SetName(name);

            return wrapper;
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._names"/>
        public static OperatorWrapperBase SetName(this OperatorWrapperBase wrapper, string name)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Operator.SetName(name);
            return wrapper;
        }
    }

    // WrapperWishes From UnderlyingWishes

    [Obsolete(ObsoleteMessage)]
    public static class UnderlyingWrapperExtensions
    {
        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._underlyingextensions"/>
        public static Operator Operator(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._underlyingextensions"/>
        public static Operator Operator(this CurveInWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            if (wrapper.Result == null) throw new NullException(() => wrapper.Result);
            return wrapper.Result.Operator;
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Input);
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetCurveWrapper(entity.Operator);
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getcurvewrapper"/>
        public static CurveInWrapper GetCurveWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new CurveInWrapper(entity.AsCurveIn);
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Inlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Input);
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Outlet entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return GetSampleWrapper(entity.Operator);
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._getsamplewrapper" />
        public static SampleOperatorWrapper GetSampleWrapper(this Operator entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            return new SampleOperatorWrapper(entity.AsSampleOperator);
        }
    }

    // WrapperWishes From StringifyWishes

    [Obsolete(ObsoleteMessage)]
    public static class StringifyWrapperExtensions
    {
        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._stringify"/>
        public static string Stringify(this OperatorWrapperBase wrapper, bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, canOmitNameForBasicMath).StringifyRecursive(wrapper.Operator);
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._stringify"/>
        public static string Stringify(this SampleOperatorWrapper wrapper, bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, canOmitNameForBasicMath).StringifyRecursive(wrapper.Result);
        }

        [Obsolete(ObsoleteMessage)]
        /// <inheritdoc cref="docs._stringify"/>
        public static string Stringify(this CurveInWrapper wrapper, bool singleLine = false, bool canOmitNameForBasicMath = false)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return new OperatorStringifier(singleLine, canOmitNameForBasicMath).StringifyRecursive(wrapper.Result);
        }
    }

    // WrapperWishes From ValidationWishes

    [Obsolete(ObsoleteMessage)]
    public static class ValidationWrapperExtension
    {
        [Obsolete(ObsoleteMessage)]
        public static Result Validate(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Operator.Validate(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static Result Validate(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Validate(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static Result Validate(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.Validate(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static void Assert(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Operator.Assert(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static void Assert(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Result.Assert(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static void Assert(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            wrapper.Result.Assert(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static IList<string> GetWarnings(this OperatorWrapperBase wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Operator.GetWarnings(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static IList<string> GetWarnings(this CurveInWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.GetWarnings(recursive);
        }

        [Obsolete(ObsoleteMessage)]
        public static IList<string> GetWarnings(this SampleOperatorWrapper wrapper, bool recursive = true)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Result.GetWarnings(recursive);
        }
    }

    // From WavWishes

    [Obsolete(ObsoleteMessage)]
    public static class WavHeaderWrapperExtensions
    {
        [Obsolete(ObsoleteMessage)]
        public static WavHeaderStruct GetWavHeader(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.GetInfo().ToWavHeader();
        }

        [Obsolete(ObsoleteMessage)]
        public static AudioInfoWish GetInfo(this SampleOperatorWrapper wrapper)
        {
            if (wrapper == null) throw new ArgumentNullException(nameof(wrapper));
            return wrapper.Sample.ToInfo();
        }
    }
}