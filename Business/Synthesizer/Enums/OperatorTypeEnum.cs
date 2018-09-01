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
		Divide,
		PatchInlet,
		PatchOutlet,
		Power,
		Subtract,
		Number,
		Curve,
		Sample,

		/// <summary>
		/// The Noise operator is for generating noise sound.
		/// To generate random numbers, use the Random operator instead,
		/// because white noise generates numbers between -1 and 1
		/// and Random between 0 and 1 and it has other advantanges for random number generation too.
		/// </summary>
		Noise,
		Interpolate,
		Loop,
		Squash,

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

		LowPassFilter,
		HighPassFilter,
		Spectrum,

		/// <summary>
		/// More suited to the needs of generating random numbers than the Noise operator,
		/// because it generates numbers between 0 and 1, instead of between -1 and 1,
		/// offers interpolation options and explicit control over duration of a random value.
		/// </summary>
		Random,

		Equal,
		NotEqual,
		LessThan,
		GreaterThan,
		LessThanOrEqual,
		GreaterThanOrEqual,
		And,
		Or,
		Not,
		If,
		MinFollower,
		MaxFollower,
		AverageFollower,
		Round,
		Negative,
		Cache,
		PulseTrigger,
		ChangeTrigger,
		ToggleTrigger,
		GetPosition,
		SetPosition,
		Hold,
		RangeOverDimension,
		DimensionToOutlets,
		InletsToDimension,
		MaxOverInlets,
		MinOverInlets,
		AverageOverInlets,
		MaxOverDimension,
		MinOverDimension,
		AverageOverDimension,
		SumOverDimension,
		SumFollower,
		Multiply,
		ClosestOverInlets,
		ClosestOverDimension,
		ClosestOverInletsExp,
		ClosestOverDimensionExp,
		SortOverInlets,
		SortOverDimension,
		BandPassFilterConstantTransitionGain,
		BandPassFilterConstantPeakGain,
		NotchFilter,
		AllPassFilter,
		PeakingEQFilter,
		LowShelfFilter,
		HighShelfFilter,
		RangeOverOutlets,
		DoubleToBoolean,
		BooleanToDouble,
		Remainder,
		SampleWithRate1,
		SineWithRate1,
		TriangleWithRate1,
		VariableInput,
		RandomStripe,
        Sin,
        Cos,
        Tan,
        SinH,
        CosH,
        TanH,
        ArcSin,
        ArcCos,
        ArcTan,
        LogN,
        Ln,
        SquareRoot,
        CubeRoot,
        NthRoot,
        Sign,
        Xor,
        Nand,
        Ceiling,
        Floor,
        Truncate
	}
}
