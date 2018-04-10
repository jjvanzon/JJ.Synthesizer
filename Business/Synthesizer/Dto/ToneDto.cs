using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Business.Synthesizer.Dto
{
	public class ToneDto : ITone
	{
		/// <summary> 0 for calculated tones, that were not entered directly by the user. </summary>
		[Obsolete("Obsolete?")]
		public int ID { get; set; }
		/// <summary> Not from entity, means it was calculated instead of entered by the user. </summary>
		[Obsolete("Obsolete?")]
		public bool IsFromEntity { get; set; }
		/// <inheritdoc />
		public int Octave { get; set; }
		/// <inheritdoc />
		public double Value { get; set; }
		public int Ordinal { get; set; }
		public double Frequency { get; set; }
		public int Number { get; set; }
		public ScaleTypeEnum ScaleTypeEnum { get; set; }
		public double ScaleBaseFrequency { get; set; }
	}
}
