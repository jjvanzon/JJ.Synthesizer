using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Reflection;
using System.Reflection;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Wishes.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigResolverAccessor
    {
        private static readonly Type _underlyingType = GetUnderlyingType();
        private static readonly Accessor _staticAccessor = new Accessor(_underlyingType);
        private readonly Accessor_Copied_Adapted _accessor;
        public object Obj { get; }
        
        private static Type GetUnderlyingType()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Configuration.ConfigResolver";
            Type     type     = assembly.GetType(typeName, true);
            return type;
        }
        
        public ConfigResolverAccessor(object obj)
        {
            _accessor = new Accessor_Copied_Adapted(obj, _underlyingType);
            Obj = obj;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null) return Obj == null;
            if (obj is ConfigResolverAccessor other) return Obj == other.Obj;
            return false;
        }
        
        public static ConfigSectionAccessor _section 
            => new ConfigSectionAccessor(_staticAccessor.GetFieldValue(MemberName()));
        
        public int _channel
        {
            get => (int)_accessor.GetFieldValue(MemberName());
            set => _accessor.SetFieldValue(MemberName(), value);
        }
        
        // Bits
        
        public int GetBits => (int)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithBits(int? value) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new object[] { value }, new [] { typeof(int?) }));
        public bool Is32Bit => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor With32Bit() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        public bool Is16Bit => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor With16Bit() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        public bool Is8Bit => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor With8Bit() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        
        // Channels

        public int GetChannels => (int)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithChannels(int? channels) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new object[] { channels }, new[] { typeof(int?) }));
        public bool IsMono => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithMono() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        public bool IsStereo => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithStereo() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
                
        // Channel

        public int? GetChannel => (int?)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithChannel(int? channels) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new object[] { channels }, new[] { typeof(int?) }));
        public bool IsCenter => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithCenter() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        public bool IsLeft => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithLeft() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        public bool IsRight => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithRight() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        
        // SamplingRate
        
        public ConfigResolverAccessor WithSamplingRate(int? value) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new object[] { value }, new[] { typeof(int?) }));
        public int GetSamplingRate => (int)_accessor.GetPropertyValue(MemberName());
        
        // AudioFormat

        public AudioFileFormatEnum GetAudioFormat => (AudioFileFormatEnum)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithAudioFormat(AudioFileFormatEnum? value) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new object[] { value }, new[] { typeof(AudioFileFormatEnum?) }));
        public bool IsRaw => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor AsRaw() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        public bool IsWav => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor AsWav() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));

        // Interpolation
        
        public InterpolationTypeEnum GetInterpolation => (InterpolationTypeEnum)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithInterpolation(InterpolationTypeEnum? interpolation) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new object[] { interpolation }, new[] { typeof(InterpolationTypeEnum?) }));
        public bool IsLinear => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithLinear() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        public bool IsBlocky => (bool)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithBlocky() => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName()));
        
        // CourtesyFrames
        
        public int GetCourtesyFrames => (int)_accessor.GetPropertyValue(MemberName());
        public ConfigResolverAccessor WithCourtesyFrames(int? value) => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), new object[] { value }, new[] { typeof(int?) }));
        
        // AudioLength
                
        public ConfigResolverAccessor WithAudioLength(double? value, SynthWishes synthWishes) 
            => new ConfigResolverAccessor(_accessor.InvokeMethod(MemberName(), value, synthWishes));
    }
}
