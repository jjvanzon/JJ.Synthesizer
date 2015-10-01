namespace JJ.Business.Synthesizer.Enums
{
    public enum ScaleTypeEnum
    {
        Undefined = 0,
        LiteralFrequencies = 1,

        /// <summary> Base frequency times a factor. </summary>
        Factors = 2,

        /// <summary>
        /// A number between 0 and 1 indicates the relative position between one octave and then next.
        /// Mathematically means it goes from one octave's frequency to the next exponentially,
        /// which we perceive as a smooth pitch transition. 
        /// </summary>
        Exponents = 3,

        /// <summary>
        /// Tones are specified as a fraction of a semi tone.
        /// 1 is the first semi-tone, while 12 is the last semi-tone.
        /// </summary>
        SemiTones = 4
    }
}
