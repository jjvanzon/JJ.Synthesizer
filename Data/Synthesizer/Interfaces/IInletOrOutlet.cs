using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.Interfaces
{
	public interface IInletOrOutlet
	{
		int ID { get; set; }
		/// <summary> optional </summary>
		string Name { get; set; }
		/// <summary> nullable </summary>
		Dimension Dimension { get; set; }
		/// <summary> Not sequential. Could start with any number. </summary>
		int Position { get; set; }
		/// <summary>
		/// Can only be done for one of the patch inlets, 
		/// but can be repeated in the operator inlets.
		/// </summary>
		bool IsRepeating { get; set; }
		/// <summary>
		/// Sequential, starts at 0.
		/// Not relevant for patch inlets, only relevant for the derived operator inlets.
		/// </summary>
		int? RepetitionPosition { get; set; }
		/// <summary>
		/// If an operator's UnderlyingPatch is changed,
		/// obsolete inlets and outlets that still have connections are kept alive,
		/// but marked as obsolete.
		/// </summary>
		bool IsObsolete { get; set; }
		bool NameOrDimensionHidden { get; set; }
		/// <summary> parent, not nullable </summary>
		Operator Operator { get; set; }
	}
}
