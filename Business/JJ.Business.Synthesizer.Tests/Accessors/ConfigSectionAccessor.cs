using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using wishdocs = JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigSectionAccessor
    {
        public object Obj { get; }
        
        private readonly Accessor _accessor;
        
        // Add constructors to FrameworkWishes
        
        public ConfigSectionAccessor()
        {
            Type type = GetUnderlyingType();
            Obj       = Activator.CreateInstance(type);
            _accessor = new Accessor(Obj, Obj.GetType());
        }
        
        public ConfigSectionAccessor(object obj)
        {
            Obj = obj;
            _accessor = new Accessor(obj, GetUnderlyingType());
        }
        
        private Type GetUnderlyingType()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Configuration.ConfigSection";
            return   assembly.GetType(typeName, true);
        }
        
        // Primary Audio Properties
        
        public int? Bits
        {
            // TODO: Add more terse accessor helper to FrameworkWishes.
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set =>       _accessor.SetPropertyValue(MemberName(), value);            
        }

        public int? Channels
        {
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set =>       _accessor.SetPropertyValue(MemberName(), value);            
        }

        public int? SamplingRate
        {
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set =>       _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public AudioFileFormatEnum? AudioFormat
        {
            get => (AudioFileFormatEnum?)_accessor.GetPropertyValue(MemberName());
            set =>                       _accessor.SetPropertyValue(MemberName(), value);
        }
                
        public InterpolationTypeEnum? Interpolation
        {
            get => (InterpolationTypeEnum?)_accessor.GetPropertyValue(MemberName());
            set =>                         _accessor.SetPropertyValue(MemberName(), value);
        }

        public int? CourtesyFrames
        {
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set =>       _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        // Durations
        
        /// <inheritdoc cref="wishdocs._notelength" />
        public double? NoteLength
        {
            get => (double?)_accessor.GetPropertyValue(MemberName());
            set =>          _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public double? BarLength
        {
            get => (double?)_accessor.GetPropertyValue(MemberName());
            set =>          _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public double? BeatLength
        {
            get => (double?)_accessor.GetPropertyValue(MemberName());
            set =>          _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public double? AudioLength
        {
            get => (double?)_accessor.GetPropertyValue(MemberName());
            set =>          _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public double? LeadingSilence
        {
            get => (double?)_accessor.GetPropertyValue(MemberName());
            set =>          _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public double? TrailingSilence
        {
            get => (double?)_accessor.GetPropertyValue(MemberName());
            set =>          _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        // Feature Toggles
        
        public bool? AudioPlayback
        {
            get => (bool?)_accessor.GetPropertyValue(MemberName());
            set =>        _accessor.SetPropertyValue(MemberName(), value);
        }
        
        public bool? DiskCache
        {
            get => (bool?)_accessor.GetPropertyValue(MemberName());
            set =>        _accessor.SetPropertyValue(MemberName(), value);
        }
        
        public bool? MathBoost
        {
            get => (bool?)_accessor.GetPropertyValue(MemberName());
            set =>        _accessor.SetPropertyValue(MemberName(), value);
        }
        
        public bool? ParallelProcessing
        {
            get => (bool?)_accessor.GetPropertyValue(MemberName());
            set =>        _accessor.SetPropertyValue(MemberName(), value);
        }
        
        public bool? PlayAllTapes
        {
            get => (bool?)_accessor.GetPropertyValue(MemberName());
            set =>        _accessor.SetPropertyValue(MemberName(), value);
        }
                
        // Tooling

        public ConfigToolingElementAccessor AzurePipelines
        {
            get => new ConfigToolingElementAccessor(_accessor.GetPropertyValue(MemberName()));
            set => _accessor.SetPropertyValue(MemberName(), value.Obj);
        }

        public ConfigToolingElementAccessor NCrunch
        {
            get => new ConfigToolingElementAccessor(_accessor.GetPropertyValue(MemberName()));
            set => _accessor.SetPropertyValue(MemberName(), value.Obj);
        }
        
        // Misc
        
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public double? LeafCheckTimeOut
        {
            get => (double?)_accessor.GetPropertyValue(MemberName());
            set =>          _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        /// <inheritdoc cref="wishdocs._leafchecktimeout" />
        public TimeOutActionEnum? TimeOutAction
        {
            get => (TimeOutActionEnum?)_accessor.GetPropertyValue(MemberName());
            set =>                     _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public int? FileExtensionMaxLength
        {
            get => (int?)_accessor.GetPropertyValue(MemberName());
            set =>       _accessor.SetPropertyValue(MemberName(), value);            
        }
        
        public string LongTestCategory
        {
            get => (string)_accessor.GetPropertyValue(MemberName());
            set =>         _accessor.SetPropertyValue(MemberName(), value);            
        }
    }
}
