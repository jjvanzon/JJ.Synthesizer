        ///// <summary> nullable </summary>
        //private readonly SynthWishes _synthWishes;
        
        ///// <param name="synthWishes">nullable</param>
        //public ConfigResolver(SynthWishes synthWishes)
        //{
        //    _synthWishes = synthWishes;
        //}

        //private void AssertSynthWishes()
        //{
        //    if (_synthWishes == null)
        //    {
        //        throw new Exception("SynthWishes reference not set, needed to convert config file setting to FlowNode. Please use in a SynthWishes-aware context.");
        //    }
        //}

    
        //private static ConfigResolver DefaultConfigResolver { get; } = new ConfigResolver();

    ///// <inheritdoc cref="docs._confighelper"/>
    //internal static class ConfigHelper
    //{
    //    private static readonly ConfigSection _section = TryGetSection<ConfigSection>() ?? new ConfigSection();
    //    public static ConfigToolingElementWithDefaults AzurePipelines { get; } = new ConfigToolingElementWithDefaults(_section.AzurePipelines);
    //    public static ConfigToolingElementWithDefaults NCrunch { get; } = new ConfigToolingElementWithDefaults(_section.NCrunch);
    //}
    
    //public class ConfigToolingElementWithDefaults
    //{
    //    private readonly ConfigToolingElement _baseConfig;
    //    internal ConfigToolingElementWithDefaults(ConfigToolingElement baseConfig) => _baseConfig = baseConfig;
    //    public bool Impersonate => _baseConfig.Impersonate ?? DefaultToolingImpersonate;
    //}
