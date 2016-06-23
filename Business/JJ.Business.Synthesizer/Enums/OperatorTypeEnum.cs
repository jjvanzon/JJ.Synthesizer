namespace JJ.Business.Synthesizer.Enums
{
    public enum OperatorTypeEnum
    {
        Undefined = 0,
        Add = 1,
        Divide = 3,
        MultiplyWithOrigin = 4,
        PatchInlet = 5,
        PatchOutlet = 6,
        Power = 7,
        Sine = 8,
        Subtract = 9,
        Delay = 10,
        SpeedUp = 11,
        SlowDown = 12,
        TimePower = 13,
        Earlier = 14,
        Number = 15,
        Curve = 16,
        Sample = 17,

        /// <summary>
        /// The Noise operator is for generating noise sound.
        /// To generate random numbers, use the Random operator instead,
        /// because white noise generates numbers between -1 and 1
        /// and Random between 0 and 1 and it has other advantanges for random number generation too.
        /// </summary>
        Noise = 18,
        Resample = 19,
        CustomOperator = 20,
        SawUp = 21,
        Square = 22,
        Triangle = 23,
        Exponent = 24,
        Loop = 25,
        Select = 26,
        Bundle = 27,
        Unbundle = 28,

        /// <summary>
        /// Almost an alias for SlowDown, except that Stretch does not track phase and has an origin parameter.
        /// It is more appropriate when you are e.g. stretching curves for filters.
        /// It makes more sense when the x-axis is not used as time.
        /// </summary>
        Stretch = 29,

        /// <summary>
        /// Almost an alias for SpeedUp, except that Narrower does not track phase and has an origin parameter.
        /// It is more appropriate when you are e.g. stretching curves for filters.
        /// It makes more sense when the x-axis is not used as time.
        /// </summary>
        Narrower = 30,

        /// <summary>
        /// A synonym to Delay. The word 'Shift' makes more sense when the x-axis is not used as time.
        /// </summary>
        Shift = 31,

        /// <summary>
        /// Allows you to reset a whole branch of a patch.
        /// The effect of this is among other things,
        /// that samples start playing from the start again,
        /// variable input is reset to default values,
        /// sines start at phase 0,
        /// and other time / phase tracking mechanisms are reset to their initial state.
        /// The most common usage for this, is starting a new note.
        /// </summary>
        Reset = 32,

        LowPassFilter = 33,
        HighPassFilter = 34,
        Spectrum = 35,

        Pulse = 36,

        /// <summary>
        /// More suited to the needs of generating random numbers than the Noise operator,
        /// because it generates numbers between 0 and 1, instead of between -1 and 1,
        /// offers interpolation options and explicit control over duration of a random value.
        /// </summary>
        Random = 37,

        Equal = 38,
        NotEqual = 39,
        LessThan = 40,
        GreaterThan = 41,
        LessThanOrEqual = 42,
        GreaterThanOrEqual = 43,
        And = 44,
        Or = 45,
        Not = 46,
        If = 47,
        MinFollower = 48,
        MaxFollower = 49,
        AverageFollower = 50,
        Scaler = 51,
        SawDown = 52,
        Absolute = 53,
        Reverse = 54,
        Round = 55,
        Negative = 56,
        OneOverX = 57,
        Cache = 58,

        /// <summary>
        /// Allows all the types of filters that NAudio BiQuadFilter provides. 
        /// Perhaps later this will replace the HighPassFilter and LowPassFilter operators.
        /// Perhaps instead useful filters will be selected and turned into a separate operator.
        /// </summary>
        Filter = 59,
        PulseTrigger = 60,
        ChangeTrigger = 61,
        ToggleTrigger = 62,
        GetDimension = 63,
        SetDimension = 64,
        Hold = 65,
        Range = 66,
        MakeDiscrete = 67,
        MakeContinuous = 68,
        Max = 69,
        Min = 70,
        Average = 71,
        MaxOverDimension = 72,
        MinOverDimension = 73,
        AverageOverDimension = 74,
        SumOverDimension = 75,
        SumFollower = 76,
        Multiply = 77
    }
}
