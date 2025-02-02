using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Wishes.Obsolete.ObsoleteEnumWishesMessages;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class AudioFormatExtensionWishes
    {
        // A Primary Audio Attribute
        
        // Synth-Bound
        
        public static bool IsWav(this SynthWishes obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this SynthWishes obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this SynthWishes obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static SynthWishes WithWav(this SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes AsWav(this SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes SetWav(this SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes WithRaw(this SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes AsRaw(this SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes SetRaw(this SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes AudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes WithAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes AsAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes SetAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }


        public static bool IsWav(this FlowNode obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this FlowNode obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this FlowNode obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }
        
        public static FlowNode WithWav(this FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode AsWav(this FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode SetWav(this FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode WithRaw(this FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode AsRaw(this FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode SetRaw(this FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode AudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode WithAudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode AsAudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode SetAudioFormat(this FlowNode obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        internal static bool IsWav(this ConfigResolver obj) => GetAudioFormat(obj) == Wav;
        internal static bool IsRaw(this ConfigResolver obj) => GetAudioFormat(obj) == Raw;
        internal static AudioFileFormatEnum AudioFormat(this ConfigResolver obj) => GetAudioFormat(obj);
        internal static AudioFileFormatEnum GetAudioFormat(this ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        internal static ConfigResolver WithWav(this ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver AsWav(this ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver SetWav(this ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver WithRaw(this ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver AsRaw(this ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver SetRaw(this ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver AudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver WithAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver AsAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver SetAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }
        
        // Global-Bound
        
        internal static bool IsWav(this ConfigSection obj) => GetAudioFormat(obj) == Wav;
        internal static bool IsRaw(this ConfigSection obj) => GetAudioFormat(obj) == Raw;
        internal static AudioFileFormatEnum? AudioFormat(this ConfigSection obj) => GetAudioFormat(obj);
        internal static AudioFileFormatEnum? GetAudioFormat(this ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat;
        }
        
        // Tape-Bound
        
        public static bool IsWav(this Tape obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this Tape obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this Tape obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.AudioFormat;
        }
        
        public static Tape WithWav(this Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape AsWav(this Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape SetWav(this Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape WithRaw(this Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape AsRaw(this Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape SetRaw(this Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape AudioFormat(this Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape WithAudioFormat(this Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape AsAudioFormat(this Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape SetAudioFormat(this Tape obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.AudioFormat = value;
            return obj;
        }
        
        public static bool IsWav(this TapeConfig obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this TapeConfig obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this TapeConfig obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat;
        }
        
        public static TapeConfig WithWav(this TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig AsWav(this TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig SetWav(this TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig WithRaw(this TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig AsRaw(this TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig SetRaw(this TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig AudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig WithAudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig AsAudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig SetAudioFormat(this TapeConfig obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.AudioFormat = value;
            return obj;
        }
                
        public static bool IsWav(this TapeActions obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this TapeActions obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this TapeActions obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }
        
        public static TapeActions WithWav(this TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions AsWav(this TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions SetWav(this TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions WithRaw(this TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions AsRaw(this TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions SetRaw(this TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions AudioFormat(this TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions WithAudioFormat(this TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions AsAudioFormat(this TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions SetAudioFormat(this TapeActions obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }

        public static bool IsWav(this TapeAction obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this TapeAction obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this TapeAction obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }
        
        public static TapeAction WithWav(this TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction AsWav(this TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction SetWav(this TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction WithRaw(this TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction AsRaw(this TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction SetRaw(this TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction AudioFormat(this TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction WithAudioFormat(this TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction AsAudioFormat(this TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction SetAudioFormat(this TapeAction obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static bool IsWav(this Buff obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this Buff obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this Buff obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return Undefined;
            return obj.UnderlyingAudioFileOutput.AudioFormat();
        }

        public static Buff WithWav(this Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff AsWav(this Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff SetWav(this Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff WithRaw(this Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff AsRaw(this Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff SetRaw(this Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff AudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff WithAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff AsAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff SetAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj.UnderlyingAudioFileOutput);
            obj.UnderlyingAudioFileOutput.AudioFormat(value, context);
            return obj;
        }

        public static bool IsWav(this AudioFileOutput obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this AudioFileOutput obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this AudioFileOutput obj)
        {
            return obj.GetAudioFileFormatEnum();
        }

        public static AudioFileOutput WithWav(this AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static AudioFileOutput AsWav(this AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static AudioFileOutput SetWav(this AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static AudioFileOutput WithRaw(this AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static AudioFileOutput AsRaw(this AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static AudioFileOutput SetRaw(this AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static AudioFileOutput AudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static AudioFileOutput WithAudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static AudioFileOutput AsAudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static AudioFileOutput SetAudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }
        
        // Independent after Taping 
        
        public static bool IsWav(this Sample obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this Sample obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this Sample obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this Sample obj)
        {
            return obj.GetAudioFileFormatEnum();
        }
        
        public static Sample WithWav(this Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample AsWav(this Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample SetWav(this Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample WithRaw(this Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample AsRaw(this Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample SetRaw(this Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample AudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample WithAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample AsAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample SetAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }

        // Immutable

        public static bool IsWav(this WavHeaderStruct obj) => true;
        public static bool IsRaw(this WavHeaderStruct obj) => false;
        public static AudioFileFormatEnum AudioFormat(this WavHeaderStruct obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this WavHeaderStruct obj)
        {
            return Wav;
        }
        
        public static bool IsWav(this string fileExtension) => GetAudioFormat(fileExtension) == Wav;
        public static bool IsRaw(this string fileExtension) => GetAudioFormat(fileExtension) == Raw;
        public static AudioFileFormatEnum AudioFormat(this string fileExtension) => GetAudioFormat(fileExtension);
        public static AudioFileFormatEnum ToAudioFormat(this string fileExtension) => GetAudioFormat(fileExtension);
        public static AudioFileFormatEnum GetAudioFormat(this string fileExtension)
        {
            return fileExtension.FileExtensionToAudioFormat();
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithWav(this string oldFileExtension) => SetAudioFormat(oldFileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsWav(this string oldFileExtension) => SetAudioFormat(oldFileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetWav(this string oldFileExtension) => SetAudioFormat(oldFileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithRaw(this string oldFileExtension) => SetAudioFormat(oldFileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsRaw(this string oldFileExtension) => SetAudioFormat(oldFileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetRaw(this string oldFileExtension) => SetAudioFormat(oldFileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithAudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsAudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetAudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat)
        {
            return newAudioFormat.FileExtension();
        }
                
        public static bool IsWav(this AudioFileFormatEnum obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this AudioFileFormatEnum obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum ToAudioFormat(this AudioFileFormatEnum obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this AudioFileFormatEnum obj)
        {
            return obj;
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithWav(this AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsWav(this AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetWav(this AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithRaw(this AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsRaw(this AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetRaw(this AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithAudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsAudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetAudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat)
        {
            return newAudioFormat;
        }
        
        public static bool IsWav(this AudioFileFormatEnum? obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(this AudioFileFormatEnum? obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum? AudioFormat(this AudioFileFormatEnum? obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum? ToAudioFormat(this AudioFileFormatEnum? obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum? GetAudioFormat(this AudioFileFormatEnum? obj)
        {
            return obj;
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithWav(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsWav(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToWav(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetWav(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithRaw(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsRaw(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToRaw(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetRaw(this AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat)
        {
            return newAudioFormat;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsWav(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj) == Wav;
        [Obsolete(ObsoleteMessage)]
        public static bool IsRaw(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj) == Raw;
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsAudioFormat(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToAudioFormat(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetAudioFormat(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsAudioFormatEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToAudioFormatEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetAudioFormatEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToAudioFormat(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToAudioFormatEnum(this AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormatEnum AudioFormatEntityToEnum(this AudioFileFormat obj)
        {
            return (AudioFileFormatEnum)(obj?.ID ?? 0);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithWav(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsWav(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToWav(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetWav(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithRaw(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsRaw(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToRaw(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetRaw(this AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsEntity(this AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToEntity(this AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat GetEntity(this AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat EnumToEntity(this AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormatToEntity(this AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormatEnumToEntity(this AudioFileFormatEnum audioFormat, IContext context)
        {
            return ServiceFactory.CreateRepository<IAudioFileFormatRepository>(context).Get(audioFormat.ToID());
        }
    }
}