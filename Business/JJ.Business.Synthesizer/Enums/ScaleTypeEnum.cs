namespace JJ.Business.Synthesizer.Enums
{
    public enum ScaleTypeEnum
    {
        Undefined = 0,
        LiteralFrequencies = 1,
        /// <summary> Base frequency times a factor. </summary>
        Factors = 2,
        Exponents = 3,
        /// <summary> Tones are specified as a fraction of a semi tone. summary>
        SemiTones = 4
    }
}
