using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Framework.Reflection.Core;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using wishdocs = JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigSectionAccessor
    {
        public object Obj { get; }
        
        private readonly AccessorCore _accessor;
        
        // Add constructors to FrameworkWishes
        
        public ConfigSectionAccessor()
        {
            Type type = GetUnderlyingType();
            Obj       = Activator.CreateInstance(type);
            //_accessor = new AccessorCore(Obj, Obj.GetType());
            _accessor = new AccessorCore(Obj);
        }
        
        public ConfigSectionAccessor(object obj)
        {
            Obj = obj;
            //_accessor = new AccessorCore(obj, GetUnderlyingType());
            _accessor = new AccessorCore(obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return Obj == null;
            if (obj is ConfigSectionAccessor other) return Obj == other.Obj;
            return false;
        }

        private Type GetUnderlyingType()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Config.ConfigSection";
            return   assembly.GetType(typeName, true);
        }
        
        // Primary Audio Properties
        
        public int? Bits
        {
            // TODO: Add more terse accessor helper to FrameworkWishes.
            get => (int?)_accessor.Get(MemberName());
            set =>       _accessor.Set(MemberName(), value);            
        }

        public int? Channels
        {
            get => (int?)_accessor.Get(MemberName());
            set =>       _accessor.Set(MemberName(), value);            
        }

        public int? SamplingRate
        {
            get => (int?)_accessor.Get(MemberName());
            set =>       _accessor.Set(MemberName(), value);            
        }
        
        public AudioFileFormatEnum? AudioFormat
        {
            get => (AudioFileFormatEnum?)_accessor.Get(MemberName());
            set =>                       _accessor.Set(MemberName(), value);
        }
                
        public InterpolationTypeEnum? Interpolation
        {
            get => (InterpolationTypeEnum?)_accessor.Get(MemberName());
            set =>                         _accessor.Set(MemberName(), value);
        }

        public int? CourtesyFrames
        {
            get => (int?)_accessor.Get(MemberName());
            set =>       _accessor.Set(MemberName(), value);            
        }
        
        // Durations
        
        /// <inheritdoc cref="wishdocs._notelength" />
        public double? NoteLength
        {
            get => (double?)_accessor.Get(MemberName());
            set =>          _accessor.Set(MemberName(), value);            
        }
        
        public double? BarLength
        {
            get => (double?)_accessor.Get(MemberName());
            set =>          _accessor.Set(MemberName(), value);            
        }
        
        public double? BeatLength
        {
            get => (double?)_accessor.Get(MemberName());
            set =>          _accessor.Set(MemberName(), value);            
        }
        
        public double? AudioLength
        {
            get => (double?)_accessor.Get(MemberName());
            set =>          _accessor.Set(MemberName(), value);            
        }
        
        public double? LeadingSilence
        {
            get => (double?)_accessor.Get(MemberName());
            set =>          _accessor.Set(MemberName(), value);            
        }
        
        public double? TrailingSilence
        {
            get => (double?)_accessor.Get(MemberName());
            set =>          _accessor.Set(MemberName(), value);            
        }
        
        // Feature Toggles
        
        public bool? AudioPlayback
        {
            get => (bool?)_accessor.Get(MemberName());
            set =>        _accessor.Set(MemberName(), value);
        }
        
        public bool? DiskCache
        {
            get => (bool?)_accessor.Get(MemberName());
            set =>        _accessor.Set(MemberName(), value);
        }
        
        public bool? MathBoost
        {
            get => (bool?)_accessor.Get(MemberName());
            set =>        _accessor.Set(MemberName(), value);
        }
        
        public bool? ParallelProcessing
        {
            get => (bool?)_accessor.Get(MemberName());
            set =>        _accessor.Set(MemberName(), value);
        }
        
        public bool? PlayAllTapes
        {
            get => (bool?)_accessor.Get(MemberName());
            set =>        _accessor.Set(MemberName(), value);
        }
                
        // Tooling

        public ConfigToolingElementAccessor AzurePipelines
        {
            get => new ConfigToolingElementAccessor(_accessor.Get(MemberName()));
            set => _accessor.Set(MemberName(), value.Obj);
        }

        public ConfigToolingElementAccessor NCrunch
        {
            get => new ConfigToolingElementAccessor(_accessor.Get(MemberName()));
            set => _accessor.Set(MemberName(), value.Obj);
        }
        
        // Misc
        
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public double? LeafCheckTimeOut
        {
            get => (double?)_accessor.Get(MemberName());
            set =>          _accessor.Set(MemberName(), value);            
        }
        
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public TimeOutActionEnum? TimeOutAction
        {
            get => (TimeOutActionEnum?)_accessor.Get(MemberName());
            set =>                     _accessor.Set(MemberName(), value);            
        }
        
        public int? FileExtensionMaxLength
        {
            get => (int?)_accessor.Get(MemberName());
            set =>       _accessor.Set(MemberName(), value);            
        }
        
        public string LongTestCategory
        {
            get => (string)_accessor.Get(MemberName());
            set =>         _accessor.Set(value);            
        }
    }
}
