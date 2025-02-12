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
using static JJ.Framework.Wishes.Common.FilledInWishes;

// ReSharper disable UnusedParameter.Global

namespace JJ.Business.Synthesizer.Wishes.Config
{
    /// <inheritdoc cref="docs._configextensionwishes"/>
    public partial class ConfigWishes
    {
        // AudioFormatL: A Primary Audio Attribute
        
        // Synth-Bound
        
        public static bool IsWav(SynthWishes obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(SynthWishes obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(SynthWishes obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(SynthWishes obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        public static SynthWishes WithWav(SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes AsWav(SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes FromWav(SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes ToWav(SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes SetWav(SynthWishes obj) => SetAudioFormat(obj, Wav);
        public static SynthWishes WithRaw(SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes AsRaw(SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes FromRaw(SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes ToRaw(SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes SetRaw(SynthWishes obj) => SetAudioFormat(obj, Raw);
        public static SynthWishes AudioFormat(SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes WithAudioFormat(SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes AsAudioFormat(SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes FromAudioFormat(SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes ToAudioFormat(SynthWishes obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static SynthWishes SetAudioFormat(SynthWishes obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        public static bool IsWav(FlowNode obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(FlowNode obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(FlowNode obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(FlowNode obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }
        
        public static FlowNode WithWav(FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode AsWav(FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode FromWav(FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode ToWav(FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode SetWav(FlowNode obj) => SetAudioFormat(obj, Wav);
        public static FlowNode WithRaw(FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode AsRaw(FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode FromRaw(FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode ToRaw(FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode SetRaw(FlowNode obj) => SetAudioFormat(obj, Raw);
        public static FlowNode AudioFormat(FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode WithAudioFormat(FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode AsAudioFormat(FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode FromAudioFormat(FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode ToAudioFormat(FlowNode obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        public static FlowNode SetAudioFormat(FlowNode obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }

        internal static bool IsWav(ConfigResolver obj) => GetAudioFormat(obj) == Wav;
        internal static bool IsRaw(ConfigResolver obj) => GetAudioFormat(obj) == Raw;
        internal static AudioFileFormatEnum AudioFormat(ConfigResolver obj) => GetAudioFormat(obj);
        internal static AudioFileFormatEnum GetAudioFormat(ConfigResolver obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.GetAudioFormat;
        }

        internal static ConfigResolver WithWav(ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver AsWav(ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver FromWav(ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver ToWav(ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver SetWav(ConfigResolver obj) => SetAudioFormat(obj, Wav);
        internal static ConfigResolver WithRaw(ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver AsRaw(ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver FromRaw(ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver ToRaw(ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver SetRaw(ConfigResolver obj) => SetAudioFormat(obj, Raw);
        internal static ConfigResolver AudioFormat(ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver WithAudioFormat(ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver AsAudioFormat(ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver FromAudioFormat(ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver ToAudioFormat(ConfigResolver obj, AudioFileFormatEnum? value) => SetAudioFormat(obj, value);
        internal static ConfigResolver SetAudioFormat(ConfigResolver obj, AudioFileFormatEnum? value)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.WithAudioFormat(value);
        }
        
        // Global-Bound
        
        internal static bool IsWav(ConfigSection obj) => GetAudioFormat(obj) == Wav;
        internal static bool IsRaw(ConfigSection obj) => GetAudioFormat(obj) == Raw;
        internal static AudioFileFormatEnum? AudioFormat(ConfigSection obj) => GetAudioFormat(obj);
        internal static AudioFileFormatEnum? GetAudioFormat(ConfigSection obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat;
        }
        
        // Tape-Bound
        
        public static bool IsWav(Tape obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(Tape obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(Tape obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(Tape obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Config.AudioFormat;
        }
        
        public static Tape WithWav(Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape AsWav(Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape FromWav(Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape ToWav(Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape SetWav(Tape obj) => SetAudioFormat(obj, Wav);
        public static Tape WithRaw(Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape AsRaw(Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape FromRaw(Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape ToRaw(Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape SetRaw(Tape obj) => SetAudioFormat(obj, Raw);
        public static Tape AudioFormat(Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape WithAudioFormat(Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape FromAudioFormat(Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape ToAudioFormat(Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape AsAudioFormat(Tape obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static Tape SetAudioFormat(Tape obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Config.AudioFormat = value;
            return obj;
        }
        
        public static bool IsWav(TapeConfig obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(TapeConfig obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(TapeConfig obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(TapeConfig obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.AudioFormat;
        }
        
        public static TapeConfig WithWav(TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig AsWav(TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig FromWav(TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig ToWav(TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig SetWav(TapeConfig obj) => SetAudioFormat(obj, Wav);
        public static TapeConfig WithRaw(TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig AsRaw(TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig FromRaw(TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig ToRaw(TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig SetRaw(TapeConfig obj) => SetAudioFormat(obj, Raw);
        public static TapeConfig AudioFormat(TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig WithAudioFormat(TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig FromAudioFormat(TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig ToAudioFormat(TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig AsAudioFormat(TapeConfig obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeConfig SetAudioFormat(TapeConfig obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.AudioFormat = value;
            return obj;
        }
                
        public static bool IsWav(TapeActions obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(TapeActions obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(TapeActions obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(TapeActions obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }
        
        public static TapeActions WithWav(TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions AsWav(TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions FromWav(TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions ToWav(TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions SetWav(TapeActions obj) => SetAudioFormat(obj, Wav);
        public static TapeActions WithRaw(TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions AsRaw(TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions FromRaw(TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions ToRaw(TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions SetRaw(TapeActions obj) => SetAudioFormat(obj, Raw);
        public static TapeActions AudioFormat(TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions WithAudioFormat(TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions AsAudioFormat(TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions FromAudioFormat(TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions ToAudioFormat(TapeActions obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeActions SetAudioFormat(TapeActions obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }

        public static bool IsWav(TapeAction obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(TapeAction obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(TapeAction obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(TapeAction obj)
        {
            if (obj == null) throw new NullException(() => obj);
            return obj.Tape.Config.AudioFormat;
        }
        
        public static TapeAction WithWav(TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction AsWav(TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction FromWav(TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction ToWav(TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction SetWav(TapeAction obj) => SetAudioFormat(obj, Wav);
        public static TapeAction WithRaw(TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction AsRaw(TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction FromRaw(TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction ToRaw(TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction SetRaw(TapeAction obj) => SetAudioFormat(obj, Raw);
        public static TapeAction AudioFormat(TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction WithAudioFormat(TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction AsAudioFormat(TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction FromAudioFormat(TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction ToAudioFormat(TapeAction obj, AudioFileFormatEnum value) => SetAudioFormat(obj, value);
        public static TapeAction SetAudioFormat(TapeAction obj, AudioFileFormatEnum value)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.Tape.Config.AudioFormat = value;
            return obj;
        }
        
        // Buff-Bound
        
        public static bool IsWav(Buff obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(Buff obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(Buff obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(Buff obj)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) return Undefined;
            return obj.UnderlyingAudioFileOutput.AudioFormat();
        }

        public static Buff WithWav(Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff AsWav(Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff FromWav(Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff ToWav(Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff SetWav(Buff obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Buff WithRaw(Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff AsRaw(Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff FromRaw(Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff ToRaw(Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff SetRaw(Buff obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Buff AudioFormat(Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff WithAudioFormat(Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff AsAudioFormat(Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff FromAudioFormat(Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff ToAudioFormat(Buff obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Buff SetAudioFormat(Buff obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            if (obj.UnderlyingAudioFileOutput == null) throw new NullException(() => obj.UnderlyingAudioFileOutput);
            obj.UnderlyingAudioFileOutput.AudioFormat(value, context);
            return obj;
        }

        public static bool IsWav(AudioFileOutput obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(AudioFileOutput obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(AudioFileOutput obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(AudioFileOutput obj)
        {
            return obj.GetAudioFileFormatEnum();
        }

        public static AudioFileOutput WithWav(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static AudioFileOutput AsWav(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static AudioFileOutput ToWav(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static AudioFileOutput SetWav(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static AudioFileOutput WithRaw(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static AudioFileOutput AsRaw(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static AudioFileOutput ToRaw(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static AudioFileOutput SetRaw(AudioFileOutput obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static AudioFileOutput AudioFormat(AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static AudioFileOutput WithAudioFormat(AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static AudioFileOutput AsAudioFormat(AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static AudioFileOutput ToAudioFormat(AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static AudioFileOutput SetAudioFormat(AudioFileOutput obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }
        
        // Independent after Taping 
        
        public static bool IsWav(Sample obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(Sample obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(Sample obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(Sample obj)
        {
            return obj.GetAudioFileFormatEnum();
        }
        
        public static Sample WithWav(Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample AsWav(Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample FromWav(Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample ToWav(Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample SetWav(Sample obj, IContext context) => SetAudioFormat(obj, Wav, context);
        public static Sample WithRaw(Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample AsRaw(Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample FromRaw(Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample ToRaw(Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample SetRaw(Sample obj, IContext context) => SetAudioFormat(obj, Raw, context);
        public static Sample AudioFormat(Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample WithAudioFormat(Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample AsAudioFormat(Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample FromAudioFormat(Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample ToAudioFormat(Sample obj, AudioFileFormatEnum value, IContext context) => SetAudioFormat(obj, value, context);
        public static Sample SetAudioFormat(Sample obj, AudioFileFormatEnum value, IContext context)
        {
            if (obj == null) throw new NullException(() => obj);
            obj.SetAudioFileFormatEnum(value, context);
            return obj;
        }

        // Immutable

        public static bool IsWav(WavHeaderStruct obj) => true;
        public static bool IsRaw(WavHeaderStruct obj) => false;
        public static AudioFileFormatEnum AudioFormat(WavHeaderStruct obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(WavHeaderStruct obj)
        {
            return Wav;
        }
        
        public static bool IsWav(string fileExtension) => FileExtensionToAudioFormat(fileExtension) == Wav;
        public static bool IsRaw(string fileExtension) => FileExtensionToAudioFormat(fileExtension) == Raw;
        public static AudioFileFormatEnum AudioFormat(string fileExtension) => FileExtensionToAudioFormat(fileExtension);
        public static AudioFileFormatEnum AsAudioFormat(string fileExtension) => FileExtensionToAudioFormat(fileExtension);
        public static AudioFileFormatEnum GetAudioFormat(string fileExtension) => FileExtensionToAudioFormat(fileExtension);
        public static AudioFileFormatEnum ToAudioFormat(string fileExtension) => FileExtensionToAudioFormat(fileExtension);
        public static AudioFileFormatEnum FileExtensionToAudioFormat(string fileExtension)
        {
            if (Is(fileExtension, ".wav")) return Wav;
            if (Is(fileExtension, ".raw")) return Raw;
            if (!Has(fileExtension)) return Undefined;
            AssertFileExtension(fileExtension); return default;
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithWav(string oldFileExtension) => SetAudioFormat(oldFileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsWav(string oldFileExtension) => SetAudioFormat(oldFileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string ToWav(string oldFileExtension) => SetAudioFormat(oldFileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetWav(string oldFileExtension) => SetAudioFormat(oldFileExtension, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithRaw(string oldFileExtension) => SetAudioFormat(oldFileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsRaw(string oldFileExtension) => SetAudioFormat(oldFileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string ToRaw(string oldFileExtension) => SetAudioFormat(oldFileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetRaw(string oldFileExtension) => SetAudioFormat(oldFileExtension, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AudioFormat(string oldFileExtension, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithAudioFormat(string oldFileExtension, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsAudioFormat(string oldFileExtension, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string ToAudioFormat(string oldFileExtension, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetAudioFormat(string oldFileExtension, AudioFileFormatEnum newAudioFormat)
        {
            return newAudioFormat.FileExtension();
        }
                
        public static bool IsWav(AudioFileFormatEnum obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(AudioFileFormatEnum obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum AudioFormat(AudioFileFormatEnum obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum ToAudioFormat(AudioFileFormatEnum obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum AsAudioFormat(AudioFileFormatEnum obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(AudioFileFormatEnum obj)
        {
            return obj;
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithWav(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsWav(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum ToWav(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetWav(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithRaw(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsRaw(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum ToRaw(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetRaw(AudioFileFormatEnum oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AudioFormat(AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithAudioFormat(AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsAudioFormat(AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum ToAudioFormat(AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetAudioFormat(AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat)
        {
            return newAudioFormat;
        }
        
        public static bool IsWav(AudioFileFormatEnum? obj) => GetAudioFormat(obj) == Wav;
        public static bool IsRaw(AudioFileFormatEnum? obj) => GetAudioFormat(obj) == Raw;
        public static AudioFileFormatEnum? AudioFormat(AudioFileFormatEnum? obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum? AsAudioFormat(AudioFileFormatEnum? obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum? ToAudioFormat(AudioFileFormatEnum? obj) => GetAudioFormat(obj);
        public static AudioFileFormatEnum? GetAudioFormat(AudioFileFormatEnum? obj)
        {
            return obj;
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithWav(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsWav(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToWav(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetWav(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Wav);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithRaw(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsRaw(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToRaw(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetRaw(AudioFileFormatEnum? oldAudioFormat) => SetAudioFormat(oldAudioFormat, Raw);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AudioFormat(AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithAudioFormat(AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsAudioFormat(AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToAudioFormat(AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => SetAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetAudioFormat(AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat)
        {
            return newAudioFormat;
        }
        
        [Obsolete(ObsoleteMessage)]
        public static bool IsWav(AudioFileFormat obj) => AudioFormatEntityToEnum(obj) == Wav;
        [Obsolete(ObsoleteMessage)]
        public static bool IsRaw(AudioFileFormat obj) => AudioFormatEntityToEnum(obj) == Raw;
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AudioFormat(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsAudioFormat(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToAudioFormat(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetAudioFormat(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsAudioFormatEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToAudioFormatEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetAudioFormatEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToAudioFormat(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToAudioFormatEnum(AudioFileFormat obj) => AudioFormatEntityToEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormatEnum AudioFormatEntityToEnum(AudioFileFormat obj)
        {
            return (AudioFileFormatEnum)(obj?.ID ?? 0);
        }
        
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithWav(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsWav(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToWav(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetWav(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Wav, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithRaw(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsRaw(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToRaw(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetRaw(AudioFileFormat oldEnumEntity, IContext context) => AudioFormatEnumToEntity(Raw, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormat(AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithAudioFormat(AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsAudioFormat(AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToAudioFormat(AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetAudioFormat(AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => AudioFormatEnumToEntity(newAudioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsEntity(AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToEntity(AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat GetEntity(AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat EnumToEntity(AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormatToEntity(AudioFileFormatEnum audioFormat, IContext context) => AudioFormatEnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormatEnumToEntity(AudioFileFormatEnum audioFormat, IContext context)
        {
            return ServiceFactory.CreateRepository<IAudioFileFormatRepository>(context).Get(audioFormat.ToID());
        }
    }

    /// <inheritdoc cref="docs._configextensionwishes"/>
    public static class AudioFormatExtensionWishes
    {
        // A Primary Audio Attribute
        
        // Synth-Bound
        
        public static bool IsWav(this SynthWishes obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this SynthWishes obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this SynthWishes obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this SynthWishes obj) => ConfigWishes.GetAudioFormat(obj);
        public static SynthWishes WithWav(this SynthWishes obj) => ConfigWishes.WithWav(obj);
        public static SynthWishes AsWav(this SynthWishes obj) => ConfigWishes.AsWav(obj);
        public static SynthWishes FromWav(this SynthWishes obj) => ConfigWishes.FromWav(obj);
        public static SynthWishes ToWav(this SynthWishes obj) => ConfigWishes.ToWav(obj);
        public static SynthWishes SetWav(this SynthWishes obj) => ConfigWishes.SetWav(obj);
        public static SynthWishes WithRaw(this SynthWishes obj) => ConfigWishes.WithRaw(obj);
        public static SynthWishes AsRaw(this SynthWishes obj) => ConfigWishes.AsRaw(obj);
        public static SynthWishes FromRaw(this SynthWishes obj) => ConfigWishes.FromRaw(obj);
        public static SynthWishes ToRaw(this SynthWishes obj) => ConfigWishes.ToRaw(obj);
        public static SynthWishes SetRaw(this SynthWishes obj) => ConfigWishes.SetRaw(obj);
        public static SynthWishes AudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => ConfigWishes.AudioFormat(obj, value);
        public static SynthWishes WithAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => ConfigWishes.WithAudioFormat(obj, value);
        public static SynthWishes AsAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => ConfigWishes.AsAudioFormat(obj, value);
        public static SynthWishes FromAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => ConfigWishes.FromAudioFormat(obj, value);
        public static SynthWishes ToAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => ConfigWishes.ToAudioFormat(obj, value);
        public static SynthWishes SetAudioFormat(this SynthWishes obj, AudioFileFormatEnum? value) => ConfigWishes.SetAudioFormat(obj, value);

        public static bool IsWav(this FlowNode obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this FlowNode obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this FlowNode obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this FlowNode obj) => ConfigWishes.GetAudioFormat(obj);

        public static FlowNode WithWav(this FlowNode obj) => ConfigWishes.WithWav(obj);
        public static FlowNode AsWav(this FlowNode obj) => ConfigWishes.AsWav(obj);
        public static FlowNode FromWav(this FlowNode obj) => ConfigWishes.FromWav(obj);
        public static FlowNode ToWav(this FlowNode obj) => ConfigWishes.ToWav(obj);
        public static FlowNode SetWav(this FlowNode obj) => ConfigWishes.SetWav(obj);
        public static FlowNode WithRaw(this FlowNode obj) => ConfigWishes.WithRaw(obj);
        public static FlowNode AsRaw(this FlowNode obj) => ConfigWishes.AsRaw(obj);
        public static FlowNode FromRaw(this FlowNode obj) => ConfigWishes.FromRaw(obj);
        public static FlowNode ToRaw(this FlowNode obj) => ConfigWishes.ToRaw(obj);
        public static FlowNode SetRaw(this FlowNode obj) => ConfigWishes.SetRaw(obj);
        public static FlowNode AudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => ConfigWishes.AudioFormat(obj, value);
        public static FlowNode WithAudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => ConfigWishes.WithAudioFormat(obj, value);
        public static FlowNode AsAudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => ConfigWishes.AsAudioFormat(obj, value);
        public static FlowNode FromAudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => ConfigWishes.FromAudioFormat(obj, value);
        public static FlowNode ToAudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => ConfigWishes.ToAudioFormat(obj, value);
        public static FlowNode SetAudioFormat(this FlowNode obj, AudioFileFormatEnum? value) => ConfigWishes.SetAudioFormat(obj, value);

        internal static bool IsWav(this ConfigResolver obj) => ConfigWishes.IsWav(obj);
        internal static bool IsRaw(this ConfigResolver obj) => ConfigWishes.IsRaw(obj);
        internal static AudioFileFormatEnum AudioFormat(this ConfigResolver obj) => ConfigWishes.AudioFormat(obj);
        internal static AudioFileFormatEnum GetAudioFormat(this ConfigResolver obj) => ConfigWishes.GetAudioFormat(obj);

        internal static ConfigResolver WithWav(this ConfigResolver obj) => ConfigWishes.WithWav(obj);
        internal static ConfigResolver AsWav(this ConfigResolver obj) => ConfigWishes.AsWav(obj);
        internal static ConfigResolver FromWav(this ConfigResolver obj) => ConfigWishes.FromWav(obj);
        internal static ConfigResolver ToWav(this ConfigResolver obj) => ConfigWishes.ToWav(obj);
        internal static ConfigResolver SetWav(this ConfigResolver obj) => ConfigWishes.SetWav(obj);
        internal static ConfigResolver WithRaw(this ConfigResolver obj) => ConfigWishes.WithRaw(obj);
        internal static ConfigResolver AsRaw(this ConfigResolver obj) => ConfigWishes.AsRaw(obj);
        internal static ConfigResolver FromRaw(this ConfigResolver obj) => ConfigWishes.FromRaw(obj);
        internal static ConfigResolver ToRaw(this ConfigResolver obj) => ConfigWishes.ToRaw(obj);
        internal static ConfigResolver SetRaw(this ConfigResolver obj) => ConfigWishes.SetRaw(obj);
        internal static ConfigResolver AudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => ConfigWishes.AudioFormat(obj, value);
        internal static ConfigResolver WithAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => ConfigWishes.WithAudioFormat(obj, value);
        internal static ConfigResolver AsAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => ConfigWishes.AsAudioFormat(obj, value);
        internal static ConfigResolver FromAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => ConfigWishes.FromAudioFormat(obj, value);
        internal static ConfigResolver ToAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => ConfigWishes.ToAudioFormat(obj, value);
        internal static ConfigResolver SetAudioFormat(this ConfigResolver obj, AudioFileFormatEnum? value) => ConfigWishes.SetAudioFormat(obj, value);

        // Global-Bound

        internal static bool IsWav(this ConfigSection obj) => ConfigWishes.IsWav(obj);
        internal static bool IsRaw(this ConfigSection obj) => ConfigWishes.IsRaw(obj);
        internal static AudioFileFormatEnum? AudioFormat(this ConfigSection obj) => ConfigWishes.AudioFormat(obj);
        internal static AudioFileFormatEnum? GetAudioFormat(this ConfigSection obj) => ConfigWishes.GetAudioFormat(obj);

        // Tape-Bound

        public static bool IsWav(this Tape obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this Tape obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this Tape obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this Tape obj) => ConfigWishes.GetAudioFormat(obj);

        public static Tape WithWav(this Tape obj) => ConfigWishes.WithWav(obj);
        public static Tape AsWav(this Tape obj) => ConfigWishes.AsWav(obj);
        public static Tape FromWav(this Tape obj) => ConfigWishes.FromWav(obj);
        public static Tape ToWav(this Tape obj) => ConfigWishes.ToWav(obj);
        public static Tape SetWav(this Tape obj) => ConfigWishes.SetWav(obj);
        public static Tape WithRaw(this Tape obj) => ConfigWishes.WithRaw(obj);
        public static Tape AsRaw(this Tape obj) => ConfigWishes.AsRaw(obj);
        public static Tape FromRaw(this Tape obj) => ConfigWishes.FromRaw(obj);
        public static Tape ToRaw(this Tape obj) => ConfigWishes.ToRaw(obj);
        public static Tape SetRaw(this Tape obj) => ConfigWishes.SetRaw(obj);
        public static Tape AudioFormat(this Tape obj, AudioFileFormatEnum value) => ConfigWishes.AudioFormat(obj, value);
        public static Tape WithAudioFormat(this Tape obj, AudioFileFormatEnum value) => ConfigWishes.WithAudioFormat(obj, value);
        public static Tape AsAudioFormat(this Tape obj, AudioFileFormatEnum value) => ConfigWishes.AsAudioFormat(obj, value);
        public static Tape FromAudioFormat(this Tape obj, AudioFileFormatEnum value) => ConfigWishes.FromAudioFormat(obj, value);
        public static Tape ToAudioFormat(this Tape obj, AudioFileFormatEnum value) => ConfigWishes.ToAudioFormat(obj, value);
        public static Tape SetAudioFormat(this Tape obj, AudioFileFormatEnum value) => ConfigWishes.SetAudioFormat(obj, value);
        
        public static bool IsWav(this TapeConfig obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this TapeConfig obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this TapeConfig obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this TapeConfig obj) => ConfigWishes.GetAudioFormat(obj);

        public static TapeConfig WithWav(this TapeConfig obj) => ConfigWishes.WithWav(obj);
        public static TapeConfig AsWav(this TapeConfig obj) => ConfigWishes.AsWav(obj);
        public static TapeConfig FromWav(this TapeConfig obj) => ConfigWishes.FromWav(obj);
        public static TapeConfig ToWav(this TapeConfig obj) => ConfigWishes.ToWav(obj);
        public static TapeConfig SetWav(this TapeConfig obj) => ConfigWishes.SetWav(obj);
        public static TapeConfig WithRaw(this TapeConfig obj) => ConfigWishes.WithRaw(obj);
        public static TapeConfig AsRaw(this TapeConfig obj) => ConfigWishes.AsRaw(obj);
        public static TapeConfig FromRaw(this TapeConfig obj) => ConfigWishes.FromRaw(obj);
        public static TapeConfig ToRaw(this TapeConfig obj) => ConfigWishes.ToRaw(obj);
        public static TapeConfig SetRaw(this TapeConfig obj) => ConfigWishes.SetRaw(obj);
        public static TapeConfig AudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => ConfigWishes.AudioFormat(obj, value);
        public static TapeConfig WithAudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => ConfigWishes.WithAudioFormat(obj, value);
        public static TapeConfig AsAudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => ConfigWishes.AsAudioFormat(obj, value);
        public static TapeConfig FromAudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => ConfigWishes.FromAudioFormat(obj, value);
        public static TapeConfig ToAudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => ConfigWishes.ToAudioFormat(obj, value);
        public static TapeConfig SetAudioFormat(this TapeConfig obj, AudioFileFormatEnum value) => ConfigWishes.SetAudioFormat(obj, value);

        public static bool IsWav(this TapeActions obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this TapeActions obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this TapeActions obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this TapeActions obj) => ConfigWishes.GetAudioFormat(obj);

        public static TapeActions WithWav(this TapeActions obj) => ConfigWishes.WithWav(obj);
        public static TapeActions AsWav(this TapeActions obj) => ConfigWishes.AsWav(obj);
        public static TapeActions FromWav(this TapeActions obj) => ConfigWishes.FromWav(obj);
        public static TapeActions ToWav(this TapeActions obj) => ConfigWishes.ToWav(obj);
        public static TapeActions SetWav(this TapeActions obj) => ConfigWishes.SetWav(obj);
        public static TapeActions WithRaw(this TapeActions obj) => ConfigWishes.WithRaw(obj);
        public static TapeActions AsRaw(this TapeActions obj) => ConfigWishes.AsRaw(obj);
        public static TapeActions FromRaw(this TapeActions obj) => ConfigWishes.FromRaw(obj);
        public static TapeActions ToRaw(this TapeActions obj) => ConfigWishes.ToRaw(obj);
        public static TapeActions SetRaw(this TapeActions obj) => ConfigWishes.SetRaw(obj);
        public static TapeActions AudioFormat(this TapeActions obj, AudioFileFormatEnum value) => ConfigWishes.AudioFormat(obj, value);
        public static TapeActions WithAudioFormat(this TapeActions obj, AudioFileFormatEnum value) => ConfigWishes.WithAudioFormat(obj, value);
        public static TapeActions AsAudioFormat(this TapeActions obj, AudioFileFormatEnum value) => ConfigWishes.AsAudioFormat(obj, value);
        public static TapeActions FromAudioFormat(this TapeActions obj, AudioFileFormatEnum value) => ConfigWishes.FromAudioFormat(obj, value);
        public static TapeActions ToAudioFormat(this TapeActions obj, AudioFileFormatEnum value) => ConfigWishes.ToAudioFormat(obj, value);
        public static TapeActions SetAudioFormat(this TapeActions obj, AudioFileFormatEnum value) => ConfigWishes.SetAudioFormat(obj, value);

        public static bool IsWav(this TapeAction obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this TapeAction obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this TapeAction obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this TapeAction obj) => ConfigWishes.GetAudioFormat(obj);

        public static TapeAction WithWav(this TapeAction obj) => ConfigWishes.WithWav(obj);
        public static TapeAction AsWav(this TapeAction obj) => ConfigWishes.AsWav(obj);
        public static TapeAction FromWav(this TapeAction obj) => ConfigWishes.FromWav(obj);
        public static TapeAction ToWav(this TapeAction obj) => ConfigWishes.ToWav(obj);
        public static TapeAction SetWav(this TapeAction obj) => ConfigWishes.SetWav(obj);
        public static TapeAction WithRaw(this TapeAction obj) => ConfigWishes.WithRaw(obj);
        public static TapeAction AsRaw(this TapeAction obj) => ConfigWishes.AsRaw(obj);
        public static TapeAction FromRaw(this TapeAction obj) => ConfigWishes.FromRaw(obj);
        public static TapeAction ToRaw(this TapeAction obj) => ConfigWishes.ToRaw(obj);
        public static TapeAction SetRaw(this TapeAction obj) => ConfigWishes.SetRaw(obj);
        public static TapeAction AudioFormat(this TapeAction obj, AudioFileFormatEnum value) => ConfigWishes.AudioFormat(obj, value);
        public static TapeAction WithAudioFormat(this TapeAction obj, AudioFileFormatEnum value) => ConfigWishes.WithAudioFormat(obj, value);
        public static TapeAction AsAudioFormat(this TapeAction obj, AudioFileFormatEnum value) => ConfigWishes.AsAudioFormat(obj, value);
        public static TapeAction FromAudioFormat(this TapeAction obj, AudioFileFormatEnum value) => ConfigWishes.FromAudioFormat(obj, value);
        public static TapeAction ToAudioFormat(this TapeAction obj, AudioFileFormatEnum value) => ConfigWishes.ToAudioFormat(obj, value);
        public static TapeAction SetAudioFormat(this TapeAction obj, AudioFileFormatEnum value) => ConfigWishes.SetAudioFormat(obj, value);

        // Buff-Bound

        public static bool IsWav(this Buff obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this Buff obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this Buff obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this Buff obj) => ConfigWishes.GetAudioFormat(obj);

        public static Buff WithWav(this Buff obj, IContext context) => ConfigWishes.WithWav(obj, context);
        public static Buff AsWav(this Buff obj, IContext context) => ConfigWishes.AsWav(obj, context);
        public static Buff FromWav(this Buff obj, IContext context) => ConfigWishes.FromWav(obj, context);
        public static Buff ToWav(this Buff obj, IContext context) => ConfigWishes.ToWav(obj, context);
        public static Buff SetWav(this Buff obj, IContext context) => ConfigWishes.SetWav(obj, context);
        public static Buff WithRaw(this Buff obj, IContext context) => ConfigWishes.WithRaw(obj, context);
        public static Buff AsRaw(this Buff obj, IContext context) => ConfigWishes.AsRaw(obj, context);
        public static Buff FromRaw(this Buff obj, IContext context) => ConfigWishes.FromRaw(obj, context);
        public static Buff ToRaw(this Buff obj, IContext context) => ConfigWishes.ToRaw(obj, context);
        public static Buff SetRaw(this Buff obj, IContext context) => ConfigWishes.SetRaw(obj, context);
        public static Buff AudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.AudioFormat(obj, value, context);
        public static Buff WithAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.WithAudioFormat(obj, value, context);
        public static Buff AsAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.AsAudioFormat(obj, value, context);
        public static Buff FromAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.FromAudioFormat(obj, value, context);
        public static Buff ToAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.ToAudioFormat(obj, value, context);
        public static Buff SetAudioFormat(this Buff obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.SetAudioFormat(obj, value, context);

        public static bool IsWav(this AudioFileOutput obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this AudioFileOutput obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this AudioFileOutput obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this AudioFileOutput obj) => ConfigWishes.GetAudioFormat(obj);

        public static AudioFileOutput WithWav(this AudioFileOutput obj, IContext context) => ConfigWishes.WithWav(obj, context);
        public static AudioFileOutput AsWav(this AudioFileOutput obj, IContext context) => ConfigWishes.AsWav(obj, context);
        public static AudioFileOutput ToWav(this AudioFileOutput obj, IContext context) => ConfigWishes.ToWav(obj, context);
        public static AudioFileOutput SetWav(this AudioFileOutput obj, IContext context) => ConfigWishes.SetWav(obj, context);
        public static AudioFileOutput WithRaw(this AudioFileOutput obj, IContext context) => ConfigWishes.WithRaw(obj, context);
        public static AudioFileOutput AsRaw(this AudioFileOutput obj, IContext context) => ConfigWishes.AsRaw(obj, context);
        public static AudioFileOutput ToRaw(this AudioFileOutput obj, IContext context) => ConfigWishes.ToRaw(obj, context);
        public static AudioFileOutput SetRaw(this AudioFileOutput obj, IContext context) => ConfigWishes.SetRaw(obj, context);
        public static AudioFileOutput AudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.AudioFormat(obj, value, context);
        public static AudioFileOutput WithAudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.WithAudioFormat(obj, value, context);
        public static AudioFileOutput AsAudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.AsAudioFormat(obj, value, context);
        public static AudioFileOutput ToAudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.ToAudioFormat(obj, value, context);
        public static AudioFileOutput SetAudioFormat(this AudioFileOutput obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.SetAudioFormat(obj, value, context);

        // Independent after Taping 

        public static bool IsWav(this Sample obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this Sample obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this Sample obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this Sample obj) => ConfigWishes.GetAudioFormat(obj);

        public static Sample WithWav(this Sample obj, IContext context) => ConfigWishes.WithWav(obj, context);
        public static Sample AsWav(this Sample obj, IContext context) => ConfigWishes.AsWav(obj, context);
        public static Sample FromWav(this Sample obj, IContext context) => ConfigWishes.FromWav(obj, context);
        public static Sample ToWav(this Sample obj, IContext context) => ConfigWishes.ToWav(obj, context);
        public static Sample SetWav(this Sample obj, IContext context) => ConfigWishes.SetWav(obj, context);
        public static Sample WithRaw(this Sample obj, IContext context) => ConfigWishes.WithRaw(obj, context);
        public static Sample AsRaw(this Sample obj, IContext context) => ConfigWishes.AsRaw(obj, context);
        public static Sample FromRaw(this Sample obj, IContext context) => ConfigWishes.FromRaw(obj, context);
        public static Sample ToRaw(this Sample obj, IContext context) => ConfigWishes.ToRaw(obj, context);
        public static Sample SetRaw(this Sample obj, IContext context) => ConfigWishes.SetRaw(obj, context);
        public static Sample AudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.AudioFormat(obj, value, context);
        public static Sample WithAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.WithAudioFormat(obj, value, context);
        public static Sample AsAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.AsAudioFormat(obj, value, context);
        public static Sample FromAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.FromAudioFormat(obj, value, context);
        public static Sample ToAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.ToAudioFormat(obj, value, context);
        public static Sample SetAudioFormat(this Sample obj, AudioFileFormatEnum value, IContext context) => ConfigWishes.SetAudioFormat(obj, value, context);

        // Immutable

        public static bool IsWav(this WavHeaderStruct obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this WavHeaderStruct obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this WavHeaderStruct obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this WavHeaderStruct obj) => ConfigWishes.GetAudioFormat(obj);

        public static bool IsWav(this string fileExtension) => ConfigWishes.IsWav(fileExtension);
        public static bool IsRaw(this string fileExtension) => ConfigWishes.IsRaw(fileExtension);
        public static AudioFileFormatEnum AudioFormat(this string fileExtension) => ConfigWishes.AudioFormat(fileExtension);
        public static AudioFileFormatEnum AsAudioFormat(this string fileExtension) => ConfigWishes.AsAudioFormat(fileExtension);
        public static AudioFileFormatEnum GetAudioFormat(this string fileExtension) => ConfigWishes.GetAudioFormat(fileExtension);
        public static AudioFileFormatEnum ToAudioFormat(this string fileExtension) => ConfigWishes.ToAudioFormat(fileExtension);
        public static AudioFileFormatEnum FileExtensionToAudioFormat(this string fileExtension) => ConfigWishes.FileExtensionToAudioFormat(fileExtension);

        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithWav(this string oldFileExtension) => ConfigWishes.WithWav(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsWav(this string oldFileExtension) => ConfigWishes.AsWav(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string ToWav(this string oldFileExtension) => ConfigWishes.ToWav(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetWav(this string oldFileExtension) => ConfigWishes.SetWav(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithRaw(this string oldFileExtension) => ConfigWishes.WithRaw(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsRaw(this string oldFileExtension) => ConfigWishes.AsRaw(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string ToRaw(this string oldFileExtension) => ConfigWishes.ToRaw(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetRaw(this string oldFileExtension) => ConfigWishes.SetRaw(oldFileExtension);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => ConfigWishes.AudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string WithAudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => ConfigWishes.WithAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string AsAudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => ConfigWishes.AsAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string ToAudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => ConfigWishes.ToAudioFormat(oldFileExtension, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static string SetAudioFormat(this string oldFileExtension, AudioFileFormatEnum newAudioFormat) => ConfigWishes.SetAudioFormat(oldFileExtension, newAudioFormat);

        public static bool IsWav(this AudioFileFormatEnum obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this AudioFileFormatEnum obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum AsAudioFormat(this AudioFileFormatEnum obj) => ConfigWishes.AsAudioFormat(obj);
        public static AudioFileFormatEnum ToAudioFormat(this AudioFileFormatEnum obj) => ConfigWishes.ToAudioFormat(obj);
        public static AudioFileFormatEnum GetAudioFormat(this AudioFileFormatEnum obj) => ConfigWishes.GetAudioFormat(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithWav(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.WithWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsWav(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.AsWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum ToWav(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.ToWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetWav(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.SetWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithRaw(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.WithRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsRaw(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.AsRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum ToRaw(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.ToRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetRaw(this AudioFileFormatEnum oldAudioFormat) => ConfigWishes.SetRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => ConfigWishes.AudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum WithAudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => ConfigWishes.WithAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum AsAudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => ConfigWishes.AsAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum ToAudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => ConfigWishes.ToAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum SetAudioFormat(this AudioFileFormatEnum oldAudioFormat, AudioFileFormatEnum newAudioFormat) => ConfigWishes.SetAudioFormat(oldAudioFormat, newAudioFormat);

        public static bool IsWav(this AudioFileFormatEnum? obj) => ConfigWishes.IsWav(obj);
        public static bool IsRaw(this AudioFileFormatEnum? obj) => ConfigWishes.IsRaw(obj);
        public static AudioFileFormatEnum? AudioFormat(this AudioFileFormatEnum? obj) => ConfigWishes.AudioFormat(obj);
        public static AudioFileFormatEnum? AsAudioFormat(this AudioFileFormatEnum? obj) => ConfigWishes.AsAudioFormat(obj);
        public static AudioFileFormatEnum? ToAudioFormat(this AudioFileFormatEnum? obj) => ConfigWishes.ToAudioFormat(obj);
        public static AudioFileFormatEnum? GetAudioFormat(this AudioFileFormatEnum? obj) => ConfigWishes.GetAudioFormat(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithWav(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.WithWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsWav(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.AsWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToWav(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.ToWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetWav(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.SetWav(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithRaw(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.WithRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsRaw(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.AsRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToRaw(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.ToRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetRaw(this AudioFileFormatEnum? oldAudioFormat) => ConfigWishes.SetRaw(oldAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => ConfigWishes.AudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? WithAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => ConfigWishes.WithAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? AsAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => ConfigWishes.AsAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? ToAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => ConfigWishes.ToAudioFormat(oldAudioFormat, newAudioFormat);
        /// <inheritdoc cref="docs._quasisetter" />
        public static AudioFileFormatEnum? SetAudioFormat(this AudioFileFormatEnum? oldAudioFormat, AudioFileFormatEnum? newAudioFormat) => ConfigWishes.SetAudioFormat(oldAudioFormat, newAudioFormat);

        [Obsolete(ObsoleteMessage)]
        public static bool IsWav(this AudioFileFormat obj) => ConfigWishes.IsWav(obj);
        [Obsolete(ObsoleteMessage)]
        public static bool IsRaw(this AudioFileFormat obj) => ConfigWishes.IsRaw(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AudioFormat(this AudioFileFormat obj) => ConfigWishes.AudioFormat(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsAudioFormat(this AudioFileFormat obj) => ConfigWishes.AsAudioFormat(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToAudioFormat(this AudioFileFormat obj) => ConfigWishes.ToAudioFormat(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetAudioFormat(this AudioFileFormat obj) => ConfigWishes.GetAudioFormat(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsEnum(this AudioFileFormat obj) => ConfigWishes.AsEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToEnum(this AudioFileFormat obj) => ConfigWishes.ToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetEnum(this AudioFileFormat obj) => ConfigWishes.GetEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum AsAudioFormatEnum(this AudioFileFormat obj) => ConfigWishes.AsAudioFormatEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum ToAudioFormatEnum(this AudioFileFormat obj) => ConfigWishes.ToAudioFormatEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum GetAudioFormatEnum(this AudioFileFormat obj) => ConfigWishes.GetAudioFormatEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToEnum(this AudioFileFormat obj) => ConfigWishes.EntityToEnum(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToAudioFormat(this AudioFileFormat obj) => ConfigWishes.EntityToAudioFormat(obj);
        [Obsolete(ObsoleteMessage)] 
        public static AudioFileFormatEnum EntityToAudioFormatEnum(this AudioFileFormat obj) => ConfigWishes.EntityToAudioFormatEnum(obj);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormatEnum AudioFormatEntityToEnum(this AudioFileFormat obj) => ConfigWishes.AudioFormatEntityToEnum(obj);

        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithWav(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.WithWav(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsWav(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.AsWav(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToWav(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.ToWav(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetWav(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.SetWav(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithRaw(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.WithRaw(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsRaw(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.AsRaw(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToRaw(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.ToRaw(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetRaw(this AudioFileFormat oldEnumEntity, IContext context) => ConfigWishes.SetRaw(oldEnumEntity, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => ConfigWishes.AudioFormat(oldEnumEntity, newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat WithAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => ConfigWishes.WithAudioFormat(oldEnumEntity, newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => ConfigWishes.AsAudioFormat(oldEnumEntity, newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => ConfigWishes.ToAudioFormat(oldEnumEntity, newAudioFormat, context);
        /// <inheritdoc cref="docs._quasisetter" />
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat SetAudioFormat(this AudioFileFormat oldEnumEntity, AudioFileFormatEnum newAudioFormat, IContext context) => ConfigWishes.SetAudioFormat(oldEnumEntity, newAudioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AsEntity(this AudioFileFormatEnum audioFormat, IContext context) => ConfigWishes.AsEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat ToEntity(this AudioFileFormatEnum audioFormat, IContext context) => ConfigWishes.ToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat GetEntity(this AudioFileFormatEnum audioFormat, IContext context) => ConfigWishes.GetEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat EnumToEntity(this AudioFileFormatEnum audioFormat, IContext context) => ConfigWishes.EnumToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormatToEntity(this AudioFileFormatEnum audioFormat, IContext context) => ConfigWishes.AudioFormatToEntity(audioFormat, context);
        [Obsolete(ObsoleteMessage)]
        public static AudioFileFormat AudioFormatEnumToEntity(this AudioFileFormatEnum audioFormat, IContext context) => ConfigWishes.AudioFormatEnumToEntity(audioFormat, context);
    }
}