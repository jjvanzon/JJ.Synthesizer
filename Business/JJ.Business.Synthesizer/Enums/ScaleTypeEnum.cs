namespace JJ.Business.Synthesizer.Enums
{
    public enum ScaleTypeEnum
    {
        Undefined = 0,
        LiteralFrequency = 1,
        /// <summary> Base frequency times a factor. </summary>
        Factor = 2,
        Exponent = 3,
        /// <summary> Tones are specified as a fraction of a semi tone. summary>
        SemiTone = 4
    }
}
