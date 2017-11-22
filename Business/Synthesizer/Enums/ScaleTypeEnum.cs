namespace JJ.Business.Synthesizer.Enums
{
	public enum ScaleTypeEnum
	{
		Undefined = 0,
		LiteralFrequency = 1,

		/// <summary> Base frequency times a factor. </summary>
		Factor = 2,

		/// <summary>
		/// A number between 0 and 1 indicates the relative position between one octave and then next.
		/// Mathematically means it goes from one octave's frequency to the next exponentially,
		/// which we perceive as a smooth pitch transition. 
		/// </summary>
		Exponent = 3,

		/// <summary>
		/// Tones are specified as a fraction of a semi tone.
		/// 1 is the first semi-tone, while 12 is the last semi-tone.
		/// </summary>
		SemiTone = 4,

		/// <summary>
		/// Makes you go from this octave to the next gradually specifying a number between 0 and 1200.
		/// </summary>
		Cent = 5
	}
}
