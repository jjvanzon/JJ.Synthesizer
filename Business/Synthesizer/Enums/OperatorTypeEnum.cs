namespace JJ.Business.Synthesizer.Enums
{
	/// <summary>
	/// This enum is in transition.
	/// OperatorType is no longer an entity.
	/// Whenever no logic is tied anymore to an OperatorTypeEnum member, it will be removed.
	/// New OperatorTypeEnums could be added, that do not even have an operator implementation.
	/// It might just represent an operation in the generated code.
	/// Eventually it might be called OperationEnum instead.
	/// </summary>
	public enum OperatorTypeEnum
	{
	    Undefined,
		Add,
		AllPassFilter,
		And,
        ArcCos,
        ArcSin,
        ArcTan,
		AverageFollower,
		AverageOverDimension,
		AverageOverInlets,
		BandPassFilterConstantPeakGain,
		BandPassFilterConstantTransitionGain,
		BooleanToDouble,
		Cache,
        Ceiling,
		ChangeTrigger,
		ClosestOverDimension,
		ClosestOverDimensionExp,
		ClosestOverInlets,
		ClosestOverInletsExp,
        Cos,
        CosH,
        CubeRoot,
		Curve,
		DimensionToOutlets,
		Divide,
		DoubleToBoolean,
		Equal,
        Floor,
		GetPosition,
		GreaterThan,
		GreaterThanOrEqual,
		HighPassFilter,
		HighShelfFilter,
		Hold,
		If,
		InletsToDimension,
		Interpolate,
		LessThan,
		LessThanOrEqual,
        Ln,
        LogN,
		Loop,
		LowPassFilter,
		LowShelfFilter,
		MaxFollower,
		MaxOverDimension,
		MaxOverInlets,
		MinFollower,
		MinOverDimension,
		MinOverInlets,
		Multiply,
		Negative,

		/// <summary>
		/// The Noise operator is for generating noise sound.
		/// To generate random numbers, use the Random operator instead,
		/// because white noise generates numbers between -1 and 1
		/// and Random between 0 and 1 and it has other advantages for random number generation too.
		/// </summary>
		Noise,
		Not,
		NotchFilter,
		NotEqual,
		Number,
		Or,
		PatchInlet,
		PatchOutlet,
		PeakingEQFilter,
		Power,
		PulseTrigger,

		/// <summary>
		/// More suited to the needs of generating random numbers than the Noise operator,
		/// because it generates numbers between 0 and 1, instead of between -1 and 1,
		/// offers interpolation options and explicit control over duration of a random value.
		/// </summary>
		Random,
		RandomStripe,
		RangeOverDimension,
		RangeOverOutlets,
		Remainder,

		/// <summary>
		/// Allows you to reset a whole branch of a patch.
		/// The effect of this is among other things,
		/// that samples start playing from the start again,
		/// variable input is reset to default values,
		/// sines start at phase 0,
		/// and other time / phase tracking mechanisms are reset to their initial state.
		/// The most common usage for this, is starting a new note.
		/// </summary>
		Reset,
		Round,
		Sample,
		SampleWithRate1,
		SetPosition,
        Sign,
        Sin,
		SineWithRate1,
        SinH,
		SortOverDimension,
		SortOverInlets,
		Spectrum,
        SquareRoot,
		Squash,
		Subtract,
		SumFollower,
		SumOverDimension,
        Tan,
        TanH,
		ToggleTrigger,
		TriangleWithRate1,
        Truncate,
		VariableInput,
        Xor
	}
}
