using System;
using System.Diagnostics;
using System.IO;
using JetBrains.Annotations;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.IO.File;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Helpers.SampleDataTypeHelper;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.ServiceFactory;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes;

namespace JJ.Business.Synthesizer.Wishes.AttributeWishes
{
    /// <inheritdoc cref="docs._audioinfowish"/>
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class AudioInfoWish
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);

        public int Bits { get; set; }
        public int Channels { get; set; }
        public int SamplingRate { get; set; }
        /// <inheritdoc cref="docs._framecount"/>
        public int FrameCount { get; set; }
    }
    
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Primary Audio Properties

        #region SamplingRate

        public static int SamplingRate(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }

        public static SynthWishes SamplingRate(this SynthWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        public static int SamplingRate(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }

        public static FlowNode SamplingRate(this FlowNode obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        public static int SamplingRate(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetSamplingRate;
        }

        public static ConfigWishes SamplingRate(this ConfigWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithSamplingRate(value);
        }

        internal static int SamplingRate(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate ?? DefaultSamplingRate;
        }
        
        public static int SamplingRate(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.SamplingRate;
        }
        
        public static Tape SamplingRate(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }

        public static TapeConfig SamplingRate(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }

        public static TapeActions SamplingRate(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.SamplingRate;
        }

        public static TapeAction SamplingRate(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.SamplingRate = value;
            return obj;
        }

        public static int SamplingRate(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return SamplingRate(obj.UnderlyingAudioFileOutput);
        }

        public static Buff SamplingRate(this Buff obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            SamplingRate(obj.UnderlyingAudioFileOutput, value);
            return obj;
        }

        public static int SamplingRate(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }
        
        public static Sample SamplingRate(this Sample obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.SamplingRate;
        }

        public static AudioFileOutput SamplingRate(this AudioFileOutput obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SamplingRate = value;
            return obj;
        }
        
        public static int SamplingRate(this WavHeaderStruct obj)
            => obj.SamplingRate;

        public static WavHeaderStruct SamplingRate(this WavHeaderStruct obj, int value) 
            => obj.ToWish().SamplingRate(value).ToWavHeader();

        public static int SamplingRate(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.SamplingRate;
        }

        public static AudioInfoWish SamplingRate(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.SamplingRate = value;
            return infoWish;
        }
                                
        public static int SamplingRate(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SamplingRate;
        }

        public static AudioFileInfo SamplingRate(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SamplingRate = value;
            return info;
        }

        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Primary Audio Properties

        #region AudioFormat
        
        public static AudioFileFormatEnum AudioFormat(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static SynthWishes AudioFormat(this SynthWishes obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        public static AudioFileFormatEnum AudioFormat(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static FlowNode AudioFormat(this FlowNode obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        public static AudioFileFormatEnum AudioFormat(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static ConfigWishes AudioFormat(this ConfigWishes obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        internal static AudioFileFormatEnum AudioFormat(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat ?? DefaultAudioFormat;
        }

        public static AudioFileFormatEnum AudioFormat(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.AudioFormat;
        }

        public static Tape AudioFormat(this Tape obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat;
        }

        public static TapeConfig AudioFormat(this TapeConfig obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }

        public static TapeAction AudioFormat(this TapeAction obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }

        public static TapeActions AudioFormat(this TapeActions obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return AudioFormat(obj.UnderlyingAudioFileOutput);
        }

        public static Buff AudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            AudioFormat(obj.UnderlyingAudioFileOutput, value, context);
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFileFormatEnum();
        }

        public static Sample AudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }

        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFileFormatEnum();
        }

        public static AudioFileOutput AudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }
        
        // ReSharper disable once UnusedParameter.Global
        public static AudioFileFormatEnum AudioFormat(this WavHeaderStruct obj) => Wav;

        public static AudioFileFormatEnum AudioFormat(this string fileExtension) => ExtensionToAudioFormat(fileExtension);

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static string AudioFormat(this string fileExtension, AudioFileFormatEnum audioFormat) 
            => FileExtension(audioFormat);

        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum obj) => obj;

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum obj, AudioFileFormatEnum value)
            => value;

        [Obsolete(ObsoleteMessage)] public static AudioFileFormatEnum AudioFormat(this AudioFileFormat obj) => ToEnum(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat AudioFormat(this AudioFileFormat obj, AudioFileFormatEnum value, IContext context) 
            => ToEntity(value, context);
        
        // Conversion-Style AudioFormat
        
        [Obsolete(ObsoleteMessage)] public static AudioFileFormatEnum ToEnum(this AudioFileFormat enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (AudioFileFormatEnum)enumEntity.ID;
        }
        
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat ToEntity(this AudioFileFormatEnum audioFormat, IContext context) 
            => CreateRepository<IAudioFileFormatRepository>(context).Get(audioFormat.ToID());

        public static AudioFileFormatEnum ExtensionToAudioFormat(this string fileExtension)
        {
            if (Is(fileExtension, ".wav")) return Wav;
            if (Is(fileExtension, ".raw")) return Raw;
            throw new Exception($"{new{fileExtension}} not supported.");
        }

        public static string AudioFormatToExtension(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case Wav: return ".wav";
                case Raw: return ".raw";
                default: throw new ValueNotSupportedException(obj);
            }
        }

        // AudioFormat Shorthand
        
        public   static bool IsWav(this SynthWishes         obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this FlowNode            obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this ConfigWishes        obj) => AudioFormat(obj) == Wav;
        internal static bool IsWav(this ConfigSection       obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this TapeConfig          obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this TapeAction          obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this TapeActions         obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this Buff                obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this Sample              obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this AudioFileOutput     obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this WavHeaderStruct     obj) => AudioFormat(obj) == Wav;
        public   static bool IsWav(this string    fileExtension) => AudioFormat(fileExtension) == Wav;
        public   static bool IsWav(this AudioFileFormatEnum obj) => AudioFormat(obj) == Wav;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsWav(this AudioFileFormat     obj) => AudioFormat(obj) == Wav;
        
        public   static bool IsRaw(this SynthWishes         obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this FlowNode            obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this ConfigWishes        obj) => AudioFormat(obj) == Raw;
        internal static bool IsRaw(this ConfigSection       obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this TapeConfig          obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this TapeAction          obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this TapeActions         obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this Buff                obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this Sample              obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this AudioFileOutput     obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this WavHeaderStruct     obj) => AudioFormat(obj) == Raw;
        public   static bool IsRaw(this string    fileExtension) => AudioFormat(fileExtension) == Raw;
        public   static bool IsRaw(this AudioFileFormatEnum obj) => AudioFormat(obj) == Raw;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsRaw(this AudioFileFormat  obj) => AudioFormat(obj) == Raw;
        
        public   static SynthWishes         AsWav(this SynthWishes         obj) => AudioFormat(obj, Wav);
        public   static FlowNode            AsWav(this FlowNode            obj) => AudioFormat(obj, Wav);
        public   static ConfigWishes        AsWav(this ConfigWishes        obj) => AudioFormat(obj, Wav);
        public   static Buff                AsWav(this Buff                obj, IContext context) => AudioFormat(obj, Wav, context);
        public   static Tape                AsWav(this Tape                obj) => AudioFormat(obj, Wav);
        public   static TapeConfig          AsWav(this TapeConfig          obj) => AudioFormat(obj, Wav);
        public   static TapeAction          AsWav(this TapeAction          obj) => AudioFormat(obj, Wav);
        public   static TapeActions         AsWav(this TapeActions         obj) => AudioFormat(obj, Wav);
        public   static Sample              AsWav(this Sample              obj, IContext context) => AudioFormat(obj, Wav, context);
        public   static AudioFileOutput     AsWav(this AudioFileOutput     obj, IContext context) => AudioFormat(obj, Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string              AsWav(this string    fileExtension) => AudioFormat(fileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsWav(this AudioFileFormatEnum obj) => AudioFormat(obj, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static AudioFileFormat     AsWav(this AudioFileFormat     obj, IContext context) => AudioFormat(obj, Wav, context);
        
        public   static SynthWishes         AsRaw(this SynthWishes         obj) => AudioFormat(obj, Raw);
        public   static FlowNode            AsRaw(this FlowNode            obj) => AudioFormat(obj, Raw);
        public   static ConfigWishes        AsRaw(this ConfigWishes        obj) => AudioFormat(obj, Raw);
        public   static Buff                AsRaw(this Buff                obj, IContext context) => AudioFormat(obj, Raw, context);
        public   static Tape                AsRaw(this Tape                obj) => AudioFormat(obj, Raw);
        public   static TapeConfig          AsRaw(this TapeConfig          obj) => AudioFormat(obj, Raw);
        public   static TapeAction          AsRaw(this TapeAction          obj) => AudioFormat(obj, Raw);
        public   static TapeActions         AsRaw(this TapeActions         obj) => AudioFormat(obj, Raw);
        public   static Sample              AsRaw(this Sample              obj, IContext context) => AudioFormat(obj, Raw, context);
        public   static AudioFileOutput     AsRaw(this AudioFileOutput     obj, IContext context) => AudioFormat(obj, Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static string              AsRaw(this string    fileExtension) => AudioFormat(fileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public   static AudioFileFormatEnum AsRaw(this AudioFileFormatEnum obj) => AudioFormat(obj, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static AudioFileFormat     AsRaw(this AudioFileFormat     obj, IContext context) => AudioFormat(obj, Raw, context);

        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Primary Audio Properties

        #region Interpolation

        public static InterpolationTypeEnum Interpolation(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }

        public static SynthWishes Interpolation(this SynthWishes obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        public static InterpolationTypeEnum Interpolation(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }

        public static FlowNode Interpolation(this FlowNode obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        public static InterpolationTypeEnum Interpolation(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolation;
        }

        public static ConfigWishes Interpolation(this ConfigWishes obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithInterpolation(value);
        }

        internal static InterpolationTypeEnum Interpolation(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation ?? DefaultInterpolation;
        }

        public static InterpolationTypeEnum Interpolation(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.Interpolation;
        }

        public static Tape Interpolation(this Tape obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Interpolation;
        }

        public static TapeConfig Interpolation(this TapeConfig obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }

        public static TapeAction Interpolation(this TapeAction obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.Interpolation;
        }

        public static TapeActions Interpolation(this TapeActions obj, InterpolationTypeEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.Interpolation = value;
            return obj;
        }

        public static InterpolationTypeEnum Interpolation(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetInterpolationTypeEnum();
        }

        public static Sample Interpolation(this Sample obj, InterpolationTypeEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetInterpolationTypeEnum(value, context);
            return obj;
        }
    
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj) => obj;

        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static InterpolationTypeEnum Interpolation(this InterpolationTypeEnum obj, InterpolationTypeEnum value) => value;

        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum Interpolation(this InterpolationType obj) => ToEnum(obj);
        
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static InterpolationType Interpolation(this InterpolationType obj, InterpolationTypeEnum value, IContext context) => ToEntity(value, context);

        // Interpolation, Conversion-Style
        
        [Obsolete(ObsoleteMessage)] public static InterpolationTypeEnum ToEnum(this InterpolationType enumEntity)
        {
            if (enumEntity == null) throw new ArgumentNullException(nameof(enumEntity));
            return (InterpolationTypeEnum)enumEntity.ID;
        }

        [Obsolete(ObsoleteMessage)] public static InterpolationType ToEntity(this InterpolationTypeEnum enumValue, IContext context)
        {
            if (enumValue == default) return default;
            var repository = CreateRepository<IInterpolationTypeRepository>(context);
            return repository.Get((int)enumValue);
        }

        // Interpolation Shorthand
        
        public   static bool IsLinear(this SynthWishes           obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this FlowNode              obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this ConfigWishes          obj) => Interpolation(obj) == Line;
        internal static bool IsLinear(this ConfigSection         obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this TapeConfig            obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this TapeAction            obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this TapeActions           obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this Sample                obj) => Interpolation(obj) == Line;
        public   static bool IsLinear(this InterpolationTypeEnum obj) => Interpolation(obj) == Line;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsLinear(this InterpolationType     obj) => Interpolation(obj) == Line;
        
        public   static bool IsBlocky(this SynthWishes           obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this FlowNode              obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this ConfigWishes          obj) => Interpolation(obj) == Block;
        internal static bool IsBlocky(this ConfigSection         obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this TapeConfig            obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this TapeAction            obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this TapeActions           obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this Sample                obj) => Interpolation(obj) == Block;
        public   static bool IsBlocky(this InterpolationTypeEnum obj) => Interpolation(obj) == Block;
        [Obsolete(ObsoleteMessage)]
        public   static bool IsBlocky(this InterpolationType  obj) => Interpolation(obj) == Block;
        
        public   static SynthWishes           Linear(this SynthWishes         obj) => Interpolation(obj, Line);
        public   static FlowNode              Linear(this FlowNode            obj) => Interpolation(obj, Line);
        public   static ConfigWishes          Linear(this ConfigWishes        obj) => Interpolation(obj, Line);
        public   static Tape                  Linear(this Tape                obj) => Interpolation(obj, Line);
        public   static TapeConfig            Linear(this TapeConfig          obj) => Interpolation(obj, Line);
        public   static TapeAction            Linear(this TapeAction          obj) => Interpolation(obj, Line);
        public   static TapeActions           Linear(this TapeActions         obj) => Interpolation(obj, Line);
        public   static Sample                Linear(this Sample              obj, IContext context) => Interpolation(obj, Line, context);
        /// <inheritdoc cref="docs._quisetter" />
        public   static InterpolationTypeEnum Linear(this InterpolationTypeEnum obj) => Interpolation(obj, Block);
        /// <inheritdoc cref="docs._quisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static InterpolationType     Linear(this InterpolationType     obj, IContext context) => Interpolation(obj, Line, context);
        
        public   static SynthWishes           Blocky(this SynthWishes           obj) => Interpolation(obj, Block);
        public   static FlowNode              Blocky(this FlowNode              obj) => Interpolation(obj, Block);
        public   static ConfigWishes          Blocky(this ConfigWishes          obj) => Interpolation(obj, Block);
        public   static Tape                  Blocky(this Tape                  obj) => Interpolation(obj, Block);
        public   static TapeConfig            Blocky(this TapeConfig            obj) => Interpolation(obj, Block);
        public   static TapeAction            Blocky(this TapeAction            obj) => Interpolation(obj, Block);
        public   static TapeActions           Blocky(this TapeActions           obj) => Interpolation(obj, Block);
        public   static Sample                Blocky(this Sample                obj, IContext context) => Interpolation(obj, Block, context);
        /// <inheritdoc cref="docs._quisetter" />
        public   static InterpolationTypeEnum Blocky(this InterpolationTypeEnum obj) => Interpolation(obj, Block);
        /// <inheritdoc cref="docs._quisetter" />
        [Obsolete(ObsoleteMessage)]
        public   static InterpolationType     Blocky(this InterpolationType     obj, IContext context) => Interpolation(obj, Block, context);
        
        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Primary Audio Properties

        #region CourtesyFrames
        
        public static int CourtesyFrames(int courtesyBytes, int frameSize)
        {
            if (courtesyBytes < 0) throw new Exception(nameof(frameSize) + " less than 0.");
            if (frameSize < 1) throw new Exception(nameof(frameSize) + " less than 1.");
            return courtesyBytes / frameSize;
        }
        
        public static int CourtesyFrames(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static SynthWishes CourtesyFrames(this SynthWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }
        
        public static int CourtesyFrames(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static FlowNode CourtesyFrames(this FlowNode obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }

        public static int CourtesyFrames(this ConfigWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetCourtesyFrames;
        }
        
        public static ConfigWishes CourtesyFrames(this ConfigWishes obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithCourtesyFrames(value);
        }

        internal static int CourtesyFrames(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames ?? DefaultCourtesyFrames;
        }

        public static int CourtesyFrames(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.CourtesyFrames;
        }
        
        public static Tape CourtesyFrames(this Tape obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.CourtesyFrames = value;
            return obj;
        }

        public static int CourtesyFrames(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.CourtesyFrames;
        }

        public static TapeConfig CourtesyFrames(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.CourtesyFrames = value;
            return obj;
        }

        public static int CourtesyFrames(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyFrames(obj.Tape);
        }

        public static TapeActions CourtesyFrames(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyFrames(obj.Tape, value);
            return obj;
        }

        public static int CourtesyFrames(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyFrames(obj.Tape);
        }

        public static TapeAction CourtesyFrames(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyFrames(obj.Tape, value);
            return obj;
        }

        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
        #region SizeOfBitDepth
        
        public   static int                SizeOfBitDepth(this SynthWishes        obj) => Bits(obj) / 8;
        public   static SynthWishes        SizeOfBitDepth(this SynthWishes        obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this FlowNode           obj) => Bits(obj) / 8;
        public   static FlowNode           SizeOfBitDepth(this FlowNode           obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this ConfigWishes       obj) => Bits(obj) / 8;
        public   static ConfigWishes       SizeOfBitDepth(this ConfigWishes       obj, int byteSize) => Bits(obj, byteSize * 8);
        internal static int                SizeOfBitDepth(this ConfigSection      obj) => Bits(obj) / 8;
        public   static int                SizeOfBitDepth(this Tape               obj) => Bits(obj) / 8;
        public   static Tape               SizeOfBitDepth(this Tape               obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this TapeConfig         obj) => Bits(obj) / 8;
        public   static TapeConfig         SizeOfBitDepth(this TapeConfig         obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this TapeActions        obj) => Bits(obj) / 8;
        public   static TapeActions        SizeOfBitDepth(this TapeActions        obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this TapeAction         obj) => Bits(obj) / 8;
        public   static TapeAction         SizeOfBitDepth(this TapeAction         obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this Buff               obj) => Bits(obj) / 8;
        public   static Buff               SizeOfBitDepth(this Buff               obj, int byteSize, IContext context) => Bits(obj, byteSize * 8, context);
        public   static int                SizeOfBitDepth(this Sample             obj) => Bits(obj) / 8;
        public   static Sample             SizeOfBitDepth(this Sample             obj, int byteSize, IContext context) => Bits(obj, byteSize * 8, context);
        public   static int                SizeOfBitDepth(this AudioFileOutput    obj) => Bits(obj) / 8;
        public   static AudioFileOutput    SizeOfBitDepth(this AudioFileOutput    obj, int byteSize, IContext context) => Bits(obj, byteSize * 8, context);
        public   static int                SizeOfBitDepth(this WavHeaderStruct    obj) => Bits(obj) / 8;
        public   static WavHeaderStruct    SizeOfBitDepth(this WavHeaderStruct    obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this AudioInfoWish      obj) => Bits(obj) / 8;
        public   static AudioInfoWish      SizeOfBitDepth(this AudioInfoWish      obj, int byteSize) => Bits(obj, byteSize * 8);
        public   static int                SizeOfBitDepth(this AudioFileInfo      obj) => Bits(obj) / 8;
        public   static AudioFileInfo      SizeOfBitDepth(this AudioFileInfo      obj, int byteSize) => Bits(obj, byteSize * 8);
        
        public static int SizeOfBitDepth(this int bits) => bits / 8;
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static int SizeOfBitDepth(this int bits, int byteSize) => byteSize;
        public static int SizeOfBitDepth(this Type obj) => TypeToBits(obj) / 8;
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        public static Type SizeOfBitDepth(this Type obj, int byteSize) => BitsToType(byteSize * 8);
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataTypeEnum obj) => SizeOf(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataTypeEnum SizeOfBitDepth(this SampleDataTypeEnum obj, int byteSize) => BitsToEnum(byteSize * 8);
        [Obsolete(ObsoleteMessage)] public static int SizeOfBitDepth(this SampleDataType obj) => SizeOf(obj);
        /// <inheritdoc cref="docs._quasisetter" />
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static SampleDataType SizeOfBitDepth(this SampleDataType obj, int byteSize, IContext context) => BitsToEntity(byteSize * 8, context);

        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
        #region FrameSize
        
        public static int FrameSize(this SynthWishes obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this FlowNode obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this ConfigWishes obj) => SizeOfBitDepth(obj) * Channels(obj);
        internal static int FrameSize(this ConfigSection obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Tape obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeConfig obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeAction obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this TapeActions obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Buff obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this Sample obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this AudioFileOutput obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this WavHeaderStruct obj) => SizeOfBitDepth(obj) * Channels(obj);
        public static int FrameSize(this AudioInfoWish infoWish) => SizeOfBitDepth(infoWish) * Channels(infoWish);
        public static int FrameSize(this AudioFileInfo info) => SizeOfBitDepth(info) * Channels(info);
        
        [Obsolete(ObsoleteMessage)] 
        public static int FrameSize(this (SampleDataType sampleDataType, SpeakerSetup speakerSetup) entities) 
            => SizeOfBitDepth(entities.sampleDataType) * Channels(entities.speakerSetup);
        
        [Obsolete(ObsoleteMessage)] 
        public static int FrameSize(this (SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum) enums) 
            => SizeOfBitDepth(enums.sampleDataTypeEnum) * Channels(enums.speakerSetupEnum);

        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
        #region MaxValue
        
        public   static double MaxValue(this SynthWishes     obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this FlowNode        obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this ConfigWishes    obj) => Bits(obj).MaxValue();
        internal static double MaxValue(this ConfigSection   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Buff            obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Tape            obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeConfig      obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeAction      obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this TapeActions     obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Sample          obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioFileOutput obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this WavHeaderStruct obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioFileInfo   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this AudioInfoWish   obj) => Bits(obj).MaxValue();
        public   static double MaxValue(this Type valueType) => Bits(valueType).MaxValue();
        public   static double MaxValue<TValue>() => Bits<TValue>().MaxValue();
        
        [Obsolete(ObsoleteMessage)] public static double MaxValue(this SampleDataType     obj) => Bits(obj).MaxValue();
        [Obsolete(ObsoleteMessage)] public static double MaxValue(this SampleDataTypeEnum obj) => Bits(obj).MaxValue();
        
        public static double MaxValue(this int bits)
        {
            switch (AssertBits(bits))
            {
                case 32: return 1;
                case 16: return Int16.MaxValue; // ReSharper disable once PossibleLossOfFraction
                case 8: return byte.MaxValue / 2;
                default: return default;
            }
        }
        
        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
                
        #region FileExtension

        /// <inheritdoc cref="docs._fileextension"/>
        public static string      FileExtension(this SynthWishes obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static SynthWishes FileExtension(this SynthWishes obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string      FileExtension(this FlowNode obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static FlowNode     FileExtension(this FlowNode obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this ConfigWishes obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static ConfigWishes FileExtension(this ConfigWishes obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        internal static string FileExtension(this ConfigSection obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Tape obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Tape FileExtension(this Tape obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeConfig obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeConfig FileExtension(this TapeConfig obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeActions obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeActions FileExtension(this TapeActions obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this TapeAction obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static TapeAction FileExtension(this TapeAction obj, string value) => AudioFormat(obj, AudioFormat(value));
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Buff obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Buff FileExtension(this Buff obj, string value, IContext context) => AudioFormat(obj, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this Sample obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static Sample FileExtension(this Sample obj, string value, IContext context) => AudioFormat(obj, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileOutput obj) => AudioFormat(obj).FileExtension();
        /// <inheritdoc cref="docs._fileextension"/>
        public static AudioFileOutput FileExtension(this AudioFileOutput obj, string value, IContext context) => AudioFormat(obj, AudioFormat(value), context);
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension([UsedImplicitly] this WavHeaderStruct obj) => AudioFormat(obj).FileExtension();
        
        /// <inheritdoc cref="docs._fileextension"/>
        public static string FileExtension(this AudioFileFormatEnum obj)
            => AudioFormatToExtension(obj);

        /// <inheritdoc cref="docs._fileextension"/>
        // ReSharper disable once UnusedParameter.Global
        public static AudioFileFormatEnum FileExtension(this AudioFileFormatEnum obj, string value)
            => ExtensionToAudioFormat(value);

        /// <inheritdoc cref="docs._fileextension"/>
        [Obsolete(ObsoleteMessage)] public static string FileExtension(this AudioFileFormat obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.ToEnum().FileExtension();
        }
        
        /// <inheritdoc cref="docs._fileextension"/>
        // ReSharper disable once UnusedParameter.Global
        [Obsolete(ObsoleteMessage)] public static AudioFileFormat FileExtension(this AudioFileFormat obj, string value, IContext context)
            => ExtensionToAudioFormat(value).ToEntity(context);

        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
                
        #region HeaderLength
        
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this SynthWishes obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this FlowNode obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this ConfigWishes obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        internal static int HeaderLength(this ConfigSection obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Buff obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Tape obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeConfig obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeAction obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this TapeActions obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this Sample obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileOutput obj) => AudioFormat(obj).HeaderLength();
        /// <inheritdoc cref="docs._headerlength"/>
        // ReSharper disable once UnusedParameter.Global
        public static int HeaderLength(this WavHeaderStruct obj) => HeaderLength(Wav);
                
        /// <inheritdoc cref="docs._headerlength"/>
        public static int HeaderLength(this AudioFileFormatEnum obj)
        {
            switch (obj)
            {
                case Wav: return 44;
                case Raw: return 0;
                default: throw new ValueNotSupportedException(obj);
            }
        }

        /// <inheritdoc cref="docs._headerlength"/>
        [Obsolete(ObsoleteMessage)] public static int HeaderLength(this AudioFileFormat obj) => AudioFormat(obj).HeaderLength();
        
        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Derived Properties
        
        #region CourtesyBytes
        
        public static int CourtesyBytes(int courtesyFrames, int frameSize)
        {
            if (courtesyFrames < 0) throw new Exception(nameof(frameSize) + " less than 0.");
            if (frameSize < 1) throw new Exception(nameof(frameSize) + " less than 1.");
            return courtesyFrames * frameSize;
        }
        
        public static int CourtesyBytes(this SynthWishes obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static SynthWishes CourtesyBytes(this SynthWishes obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        public static int CourtesyBytes(this FlowNode obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static FlowNode CourtesyBytes(this FlowNode obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));
        
        public static int CourtesyBytes(this ConfigWishes obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static ConfigWishes CourtesyBytes(this ConfigWishes obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));

        internal static int CourtesyBytes(this ConfigSection obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static int CourtesyBytes(this Tape obj)
            => CourtesyBytes(CourtesyFrames(obj), FrameSize(obj));

        public static Tape CourtesyBytes(this Tape obj, int value) 
            => CourtesyFrames(obj, CourtesyFrames(value, FrameSize(obj)));

        public static int CourtesyBytes(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }

        public static TapeConfig CourtesyBytes(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }

        public static int CourtesyBytes(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }

        public static TapeActions CourtesyBytes(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }

        public static int CourtesyBytes(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return CourtesyBytes(obj.Tape);
        }

        public static TapeAction CourtesyBytes(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            CourtesyBytes(obj.Tape, value);
            return obj;
        }

        #endregion
    }

    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Durations
        
        #region AudioLength
        
        public static double AudioLength(int frameCount, int samplingRate)
            => (double)frameCount / samplingRate;
        
        public static double AudioLength(int byteCount, int frameSize, int samplingRate, int headerLength, int courtesyFrames = 0)
            => (double)(byteCount - headerLength) / frameSize / samplingRate - courtesyFrames * frameSize;

        public static double AudioLength(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength.Value;
        }

        public static SynthWishes AudioLength(this SynthWishes obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value);
            return obj;
        }

        public static double AudioLength(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength.Value;
        }

        public static FlowNode AudioLength(this FlowNode obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value);
            return obj;
        }

        public static double AudioLength(this ConfigWishes obj, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioLength(synthWishes).Value;
        }

        public static ConfigWishes AudioLength(this ConfigWishes obj, double value, SynthWishes synthWishes)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.WithAudioLength(value, synthWishes);
            return obj;
        }

        internal static double AudioLength(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioLength ?? DefaultAudioLength;
        }

        public static double AudioLength(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            // TODO: From bytes[] / filePath?
            return AudioLength(obj.UnderlyingAudioFileOutput);
        }

        public static Buff AudioLength(this Buff obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj.UnderlyingAudioFileOutput);
            obj.UnderlyingAudioFileOutput.AudioLength(value);
            return obj;
        }

        public static double AudioLength(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }

        public static Tape AudioLength(this Tape obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }

        public static double AudioLength(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }

        public static TapeConfig AudioLength(this TapeConfig obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }

        public static double AudioLength(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }

        public static TapeAction AudioLength(this TapeAction obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }

        public static double AudioLength(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Duration;
        }

        public static TapeActions AudioLength(this TapeActions obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Duration = value;
            return obj;
        }

        public static double AudioLength(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetDuration();
        }

        public static Sample AudioLength(this Sample obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            double originalAudioLength = obj.AudioLength();
            obj.SamplingRate = (int)(obj.SamplingRate * value / originalAudioLength);
            return obj;
        }

        public static double AudioLength(this AudioFileOutput obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Duration;
        }

        public static AudioFileOutput AudioLength(this AudioFileOutput obj, double value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Duration = value;
            return obj;
        }

        public static double AudioLength(this WavHeaderStruct obj) 
            => obj.ToWish().AudioLength();

        public static WavHeaderStruct AudioLength(this WavHeaderStruct obj, double value)
        {
            return obj.ToWish().AudioLength(value).ToWavHeader();
        }
        
        public static double AudioLength(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            if (infoWish.FrameCount == 0) return 0;
            if (infoWish.Channels == 0) throw new Exception("info.Channels == 0");
            if (infoWish.SamplingRate == 0) throw new Exception("info.SamplingRate == 0");
            return (double)infoWish.FrameCount / infoWish.Channels / infoWish.SamplingRate;
        }
        
        public static AudioInfoWish AudioLength(this AudioInfoWish infoWish, double value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = (int)(value * infoWish.SamplingRate);
            return infoWish;
        }

        public static double AudioLength(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.ToWish().AudioLength();
        }

        public static AudioFileInfo AudioLength(this AudioFileInfo info, double value)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = (int)(value * info.SamplingRate);
            return info;
        }

        #endregion
    }
    
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Durations

        #region FrameCount

        public static int FrameCount(int byteCount, int frameSize, int headerLength)
            => (byteCount - headerLength) / frameSize;

        public static int FrameCount(byte[] bytes, string filePath, int frameSize, int headerLength) 
            => (ByteCount(bytes, filePath) - headerLength) / frameSize;

        public static int FrameCount(double audioLength, int samplingRate)
            => (int)(audioLength * samplingRate);

        public static int FrameCount(this SynthWishes obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));

        public static SynthWishes FrameCount(this SynthWishes obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this FlowNode obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));

        public static FlowNode FrameCount(this FlowNode obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this ConfigWishes obj, SynthWishes synthWishes) 
            => FrameCount(AudioLength(obj, synthWishes), SamplingRate(obj));

        public static ConfigWishes FrameCount(this ConfigWishes obj, int value, SynthWishes synthWishes) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)), synthWishes);
        
        internal static int FrameCount(this ConfigSection obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));
        
        public static int FrameCount(this Tape obj)
        {
            if (obj.IsBuff)
            {
                return FrameCount(obj.Bytes, obj.FilePathResolved, FrameSize(obj), HeaderLength(obj));
            }
            else
            {
                return FrameCount(AudioLength(obj), SamplingRate(obj));
            }
        }

        public static Tape FrameCount(this Tape obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }

        public static TapeConfig FrameCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }

        public static int FrameCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }

        public static TapeAction FrameCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }

        public static int FrameCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Tape);
        }

        public static TapeActions FrameCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            FrameCount(obj.Tape, value);
            return obj;
        }

        public static int FrameCount(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);

            int frameCount = FrameCount(obj.Bytes, obj.FilePath, FrameSize(obj), HeaderLength(obj));

            if (Has(frameCount))
            {
                return frameCount;
            }

            if (obj.UnderlyingAudioFileOutput != null)
            {
                return FrameCount(obj.UnderlyingAudioFileOutput);
            }

            return 0;
        }

        public static int FrameCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return FrameCount(obj.Bytes, obj.Location, FrameSize(obj), HeaderLength(obj));
        }

        public static int FrameCount(this AudioFileOutput obj) 
            => FrameCount(AudioLength(obj), SamplingRate(obj));

        public static AudioFileOutput FrameCount(this AudioFileOutput obj, int value) 
            => AudioLength(obj, AudioLength(value, SamplingRate(obj)));
        
        public static int FrameCount(this WavHeaderStruct obj) 
            => obj.ToWish().FrameCount();

        public static WavHeaderStruct FrameCount(this WavHeaderStruct obj, int value)
        {
            AudioInfoWish infoWish = obj.ToWish();
            AudioLength(infoWish, AudioLength(value, infoWish.SamplingRate));
            return infoWish.ToWavHeader();
        }

        public static int FrameCount(this AudioInfoWish infoWish)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            return infoWish.FrameCount;
        }

        public static AudioInfoWish FrameCount(this AudioInfoWish infoWish, int value)
        {
            if (infoWish == null) throw new NullException(() => infoWish);
            infoWish.FrameCount = value;
            return infoWish;
        }

        public static int FrameCount(this AudioFileInfo info)
        {
            if (info == null) throw new NullException(() => info);
            return info.SampleCount;
        }

        public static AudioFileInfo FrameCount(this AudioFileInfo info, int value)
        {
            if (info == null) throw new NullException(() => info);
            info.SampleCount = value;
            return info;
        }

        #endregion
    }
    
    /// <inheritdoc cref="docs._attributewishes"/>
    public static partial class AttributeExtensionWishes
    {
        // Durations
        
        #region ByteCount

        public static int ByteCount(byte[] bytes, string filePath)
        {
            if (Has(bytes))
            {
                return bytes.Length;
            }

            if (Exists(filePath))
            {
                long fileSize = new FileInfo(filePath).Length;
                int maxSize = int.MaxValue;
                if (fileSize > maxSize) throw new Exception($"File is too large. Max size = {PrettyByteCount(maxSize)}");
                return (int)fileSize;
            }

            return 0;
        }

        public static int ByteCount(int frameCount, int frameSize, int headerLength, int courtesyFrames = 0)
            => frameCount * frameSize + headerLength + CourtesyBytes(courtesyFrames, frameSize);

        public static int ByteCount(this SynthWishes obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));

        public static SynthWishes ByteCount(this SynthWishes obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));
        
        public static int ByteCount(this FlowNode obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));
        
        public static FlowNode ByteCount(this FlowNode obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));

        public static int ByteCount(this ConfigWishes obj, SynthWishes synthWishes) 
            => ByteCount(FrameCount(obj, synthWishes), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));
       
        public static ConfigWishes ByteCount(this ConfigWishes obj, int value, SynthWishes synthWishes)
        {
            double audioLength = AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj));
            AudioLength(obj, audioLength, synthWishes);
            return obj;
        }

        internal static int ByteCount(this ConfigSection obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), CourtesyFrames(obj));

        public static int ByteCount(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            
            if (obj.IsBuff)
            {
                return ByteCount(obj.Bytes, obj.FilePathResolved);
            }
            else
            {
                return ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), obj.Config.CourtesyFrames);
            }
        }

        public static Tape ByteCount(this Tape obj, int value) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), CourtesyFrames(obj)));
        
        public static int ByteCount(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeConfig ByteCount(this TapeConfig obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }
        
        public static int ByteCount(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeActions ByteCount(this TapeActions obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }

        public static int ByteCount(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Tape);
        }

        public static TapeAction ByteCount(this TapeAction obj, int value)
        {
            if (obj == null) throw new NullException(() => obj);
            ByteCount(obj.Tape, value);
            return obj;
        }

        public static int ByteCount(this Buff obj, int courtesyFrames = 0)
        {
            if (obj == null) throw new NullException(() => obj);

            int byteCount = ByteCount(obj.Bytes, obj.FilePath);

            if (Has(byteCount))
            {
                return byteCount;
            }

            if (obj.UnderlyingAudioFileOutput != null)
            {
                return BytesNeeded(obj.UnderlyingAudioFileOutput, courtesyFrames);
            }

            return 0;
        }

        public static int ByteCount(this Sample obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return ByteCount(obj.Bytes, obj.Location);
        }

        public static int ByteCount(this AudioFileOutput obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj));

        public static int BytesNeeded(this AudioFileOutput obj, int courtesyFrames = 0) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj), courtesyFrames);

        public static AudioFileOutput ByteCount(this AudioFileOutput obj, int value, int courtesyFrames = 0) 
            => AudioLength(obj, AudioLength(value, FrameSize(obj), SamplingRate(obj), HeaderLength(obj), courtesyFrames));

        public static int ByteCount(this WavHeaderStruct obj) 
            => ByteCount(FrameCount(obj), FrameSize(obj), HeaderLength(obj));

        public static WavHeaderStruct ByteCount(this WavHeaderStruct obj, int value, int courtesyFrames = 0)
        {
            var wish = obj.ToWish();
            double audioLength = AudioLength(value, FrameSize(wish), SamplingRate(wish), HeaderLength(Wav), courtesyFrames);
            return wish.AudioLength(audioLength).ToWavHeader();
        }
 
        #endregion
    }
}
