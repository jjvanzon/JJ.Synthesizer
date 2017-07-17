namespace JJ.Business.Synthesizer.Enums
{
    public enum DimensionEnum
    {
        Undefined = 0,
        /// <summary> Abstract dimension. Can be anything that varies over another dimension, usually over time. </summary>
        Signal = 1,
        Frequency = 2,
        Volume = 3,
        Frequencies = 4,
        Volumes = 5,
        VibratoSpeed = 6,
        VibratoDepth = 7,
        TremoloSpeed = 8,
        TremoloDepth = 9,
        NoteDuration = 10,
        AttackDuration = 11,
        ReleaseDuration = 12,
        NoteStart = 13,
        Brightness = 14,
        Intensity = 15,
        BrightnessModulationSpeed = 16,
        BrightnessModulationDepth = 17,
        IntensityModulationSpeed = 18,
        IntensityModulationDepth = 19,
        DecayDuration = 20,
        SustainVolume = 21,
        Time = 22,
        Channel = 23,
        Harmonic = 24,
        Balance = 25,
        Sound = 26,
        Phase = 27,
        PreviousPosition = 28,
        /// <summary> Origin is usually over the X dimension. Offset is usually over the Y dimension. </summary>
        Origin = 29,
        Input = 30,
        /// <summary> Abstract dimension. Common used with mathematical operators that have only 2 inlets. </summary>
        A = 32,
        /// <summary> Abstract dimension. Common used with mathematical operators that have only 2 inlets. </summary>
        B = 33,
        /// <summary> Mathematical dimension. Used in powers or logarithms. </summary>
        Base = 36,
        /// <summary> Mathematical dimension. Used in powers or logarithms. </summary>
        Exponent = 37,
        /// <summary> Abstract dimension. Represents any kind of change. </summary>
        Difference = 38,
        /// <summary> Abstract dimension. </summary>
        Factor = 39,
        SamplingRate = 40,
        /// <summary> Usually a starting point in time. </summary>
        Start = 41,
        /// <summary> Usually a ending point in time. </summary>
        End = 42,
        /// <summary> Abstract dimension. </summary>
        Low = 43,
        /// <summary> Abstract dimension. </summary>
        High = 44,
        /// <summary> Mathematica dimension. A fraction commonly between 0 and 1. </summary>
        Ratio = 45,
        PitchBend = 46,
        MainVolume = 47,
        /// <summary> Abstract dimension. Could be anything coming out of an outlet. As an inlet it is usually used if the operator has only one inlet. </summary>
        Number = 48,
        SliceLength = 49,
        SampleCount = 50,
        /// <summary> Usually a starting point in a discrete set. </summary>
        From = 51,
        /// <summary> Usually a final point in a discrete set. </summary>
        Till = 52,
        /// <summary> Usually the distance between taking samples to form a discrete set out of a continuous signal. </summary>
        Step = 53,
        /// <summary> Abstract dimension. Could be anything, but usually denotes one out of many discrete samples. </summary>
        Item = 54,
        Width = 55,
        BlobVolume = 56,
        PassThrough = 58,
        Reset = 59,
        Collection = 60,
        Slope = 61,
        Decibel = 62,
        Condition = 63,
        Then = 64,
        Else = 65,
        Rate = 66,
        /// <summary> Offset is usually over the Y dimension. Origin is usually over the X dimension. </summary>
        Offset = 67,
        FrequencyCount = 68,
        Skip = 69,
        LoopStartMarker = 70,
        LoopEndMarker = 71,
        ReleaseEndMarker = 72,
        Radians = 73,
        HighestFrequency = 74,
        YesNo = 75,
        /// <summary>
        /// Dimension is determined by the parent.
        /// For example: if an operator's dimension is Inherit,
        /// then it falls back to the patch's dimension.
        /// </summary>
        Inherit = 76
    }
}
