using System.Collections.Generic;
using System.Diagnostics;
using JJ.Data.Synthesizer.Helpers;
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace JJ.Data.Synthesizer.Entities
{
	[DebuggerDisplay("{" + nameof(DebuggerDisplay) + "}")]
	public class Document
	{
		public Document()
		{
			AudioFileOutputs = new List<AudioFileOutput>();
			Patches = new List<Patch>();
			Scales = new List<Scale>();
			MidiMappingGroups = new List<MidiMappingGroup>();
			LowerDocumentReferences = new List<DocumentReference>();
			HigherDocumentReferences = new List<DocumentReference>();
		}

		public virtual int ID { get; set; }
		public virtual string Name { get; set; }
		public virtual IList<AudioFileOutput> AudioFileOutputs { get; set; }
		public virtual AudioOutput AudioOutput { get; set; }
		public virtual IList<DocumentReference> LowerDocumentReferences { get; set; }
		public virtual IList<DocumentReference> HigherDocumentReferences { get; set; }
		public virtual IList<MidiMappingGroup> MidiMappingGroups { get; set; }
		public virtual IList<Patch> Patches { get; set; }
		public virtual IList<Scale> Scales { get; set; }

		private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
	}
}
