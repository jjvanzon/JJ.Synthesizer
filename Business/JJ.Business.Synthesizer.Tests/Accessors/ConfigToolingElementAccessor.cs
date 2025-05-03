namespace JJ.Business.Synthesizer.Tests.Accessors
{
    internal class ConfigToolingElementAccessor
    {
        public object Obj { get; }
        
        private readonly AccessorCore _accessor;
                
        public ConfigToolingElementAccessor()
        {
            Type type = GetUnderlyingType();
            Obj       = Activator.CreateInstance(type);
            //_accessor = new AccessorCore(Obj, Obj.GetType());
            _accessor = new AccessorCore(Obj);
        }

        public ConfigToolingElementAccessor(object obj)
        {
            Obj = obj;
            //_accessor = new AccessorCore(obj, GetUnderlyingType());
            _accessor = new AccessorCore(obj);
        }
        
        private Type GetUnderlyingType()
        {
            Assembly assembly = typeof(SynthWishes).Assembly;
            string   typeName = "JJ.Business.Synthesizer.Wishes.Config.ConfigToolingElement";
            return   assembly.GetType(typeName, true);
        }
        
        public bool? AudioPlayback
        {
            get => (bool?)_accessor.Get(MemberName());
            set =>        _accessor.Set(MemberName(), value);
        }
        
        public int? SamplingRate
        {
            get => (int?)_accessor.Get(MemberName());
            set =>       _accessor.Set(MemberName(), value);            
        }

        public int? SamplingRateLongRunning
        {
            get => (int?)_accessor.Get(MemberName());
            set =>       _accessor.Set(MemberName(), value);            
        }
                
        public bool? ImpersonationMode
        {
            get => (bool?)_accessor.Get(MemberName());
            set =>        _accessor.Set(MemberName(), value);
        }
    }
}