// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

using JJ.Data.Synthesizer.Interfaces;

namespace JJ.Data.Synthesizer.Entities
{
	public class Tone : ITone
	{
		public virtual int ID { get; set; }

		/// <summary> parent, not nullable </summary>
		public virtual Scale Scale { get; set; }

		/// <inheritdoc />
		public virtual int Octave { get; set; }

		/// <inheritdoc />
		public virtual double Value { get; set; }
	}
}