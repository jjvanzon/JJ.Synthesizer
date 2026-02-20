// ReSharper disable UnusedMember.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace JJ.Business.Synthesizer.Tests.Accessors;

internal class ConfigSectionAccessor : AccessorCore
{
    public ConfigSectionAccessor() : base("JJ.Business.Synthesizer.Wishes.Config.ConfigSection") { }
    public ConfigSectionAccessor(object obj) : base(obj) { }
    
    public override bool Equals(object obj)
    {
        if (obj == null) return Obj == null;
        if (obj is ConfigSectionAccessor other) return Obj == other.Obj;
        return obj == this;
    }
    
    // Primary Audio Properties
    
    public int? Bits            { get => (int?)Get(); set => Set(value); }
    public int? Channels        { get => (int?)Get(); set => Set(value); }
    public int? SamplingRate    { get => (int?)Get(); set => Set(value); }
    public int? CourtesyFrames  { get => (int?)Get(); set => Set(value); }
    public AudioFileFormatEnum?   AudioFormat   { get => Get(() => AudioFormat  ); set => Set(value); }
    public InterpolationTypeEnum? Interpolation { get => Get(() => Interpolation); set => Set(value); }
    
    // Durations
    
    /// <inheritdoc cref="_notelength" />
    public double? NoteLength      { get => (double?)Get(); set => Set(value); }
    public double? BarLength       { get => (double?)Get(); set => Set(value); }
    public double? BeatLength      { get => (double?)Get(); set => Set(value); }
    public double? AudioLength     { get => (double?)Get(); set => Set(value); }
    public double? LeadingSilence  { get => (double?)Get(); set => Set(value); }
    public double? TrailingSilence { get => (double?)Get(); set => Set(value); }
    
    // Feature Toggles
    
    public bool? AudioPlayback      { get => (bool?)Get(); set => Set(value); }
    public bool? DiskCache          { get => (bool?)Get(); set => Set(value); }
    public bool? MathBoost          { get => (bool?)Get(); set => Set(value); }
    public bool? ParallelProcessing { get => (bool?)Get(); set => Set(value); }
    public bool? PlayAllTapes       { get => (bool?)Get(); set => Set(value); }
            
    // Tooling

    public ConfigToolingElementAccessor AzurePipelines { get => new(Get()); set => Set(value.Obj); }
    public ConfigToolingElementAccessor NCrunch        { get => new(Get()); set => Set(value.Obj); }
    
    // Misc
    
    public int?    FileExtensionMaxLength { get => (int?   )Get(); set => Set(value); }
    // TODO: Remove outcommented
    // Removing "IsLongTestCategory" feature gets rid of Testing.Core dependency
    //public string  LongTestCategory       { get => (string )Get(); set => Set(value); }
    /// <inheritdoc cref="_leafchecktimeout" />
    public double? LeafCheckTimeOut       { get => (double?)Get(); set => Set(value); }
    /// <inheritdoc cref="_leafchecktimeout" />
    public TimeOutActionEnum? TimeOutAction { get => Get(() => TimeOutAction); set => Set(value); }
}
