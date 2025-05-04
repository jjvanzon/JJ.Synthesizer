namespace JJ.Business.Synthesizer.Tests.Accessors;

class ConfigToolingElementAccessor : AccessorCore
{
    public ConfigToolingElementAccessor() : base("ConfigToolingElement") { }
    public ConfigToolingElementAccessor(object obj) : base(obj) { }
    
    public bool? AudioPlayback           { get => (bool?)Get(); set => Set(value); }
    public int?  SamplingRate            { get => (int? )Get(); set => Set(value); }
    public int?  SamplingRateLongRunning { get => (int? )Get(); set => Set(value); }
    public bool? ImpersonationMode       { get => (bool?)Get(); set => Set(value); }
}