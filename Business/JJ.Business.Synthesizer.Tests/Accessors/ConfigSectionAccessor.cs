
namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigSectionAccessor
    {
        public object Obj { get; }
        
        private readonly AccessorCore _accessor;
        
        public ConfigSectionAccessor()
        {
            _accessor = new AccessorCore("JJ.Business.Synthesizer.Wishes.Config.ConfigSection");
            Obj = _accessor.Obj;
        }
        
        public ConfigSectionAccessor(object obj)
        {
            Obj = obj;
            _accessor = new AccessorCore(obj);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return Obj == null;
            if (obj is ConfigSectionAccessor other) return Obj == other.Obj;
            return false;
        }
        
        // Primary Audio Properties
        
        public int? Bits
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);
        }

        public int? Channels
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);            
        }

        public int? SamplingRate
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);            
        }
        
        public AudioFileFormatEnum? AudioFormat
        {
            get => _accessor.Get(() => AudioFormat);
            set => _accessor.Set(() => AudioFormat, value);
        }
                
        public InterpolationTypeEnum? Interpolation
        {
            get => _accessor.Get(() => Interpolation);
            set => _accessor.Set(() => Interpolation, value);
        }

        public int? CourtesyFrames
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);
        }
        
        // Durations
        
        /// <inheritdoc cref="_notelength" />
        public double? NoteLength
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set(value);
        }
        
        public double? BarLength
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set(value);
        }
        
        public double? BeatLength
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set(value);
        }
        
        public double? AudioLength
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set(value);
        }
        
        public double? LeadingSilence
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set(value);
        }
        
        public double? TrailingSilence
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set(value);
        }
        
        // Feature Toggles
        
        public bool? AudioPlayback
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
        
        public bool? DiskCache
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
        
        public bool? MathBoost
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
        
        public bool? ParallelProcessing
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
        
        public bool? PlayAllTapes
        {
            get => _accessor.Get<bool?>();
            set => _accessor.Set(value);
        }
                
        // Tooling

        public ConfigToolingElementAccessor AzurePipelines
        {
            get => new (_accessor.Get());
            set => _accessor.Set(value.Obj);
        }

        public ConfigToolingElementAccessor NCrunch
        {
            get => new (_accessor.Get());
            set => _accessor.Set(value.Obj);
        }
        
        // Misc
        
        /// <inheritdoc cref="_leafchecktimeout" />
        public double? LeafCheckTimeOut
        {
            get => _accessor.Get<double?>();
            set => _accessor.Set( value);            
        }
        
        /// <inheritdoc cref="_leafchecktimeout" />
        public TimeOutActionEnum? TimeOutAction
        {
            get => _accessor.Get(() => TimeOutAction);
            set => _accessor.Set(() => TimeOutAction, value);            
        }
        
        public int? FileExtensionMaxLength
        {
            get => _accessor.Get<int?>();
            set => _accessor.Set(value);
        }
        
        public string LongTestCategory
        {
            get => _accessor.Get<string>();
            set => _accessor.Set(value);            
        }
    }
}
