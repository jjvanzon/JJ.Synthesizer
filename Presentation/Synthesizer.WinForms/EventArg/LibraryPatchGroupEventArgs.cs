using System;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
	internal class LibraryPatchGroupEventArgs : EventArgs
	{
		public LibraryPatchGroupEventArgs(int lowerDocumentReferenceID, string patchGroup)
		{
			LowerDocumentReferenceID = lowerDocumentReferenceID;
			PatchGroup = patchGroup;
		}

		public int LowerDocumentReferenceID { get; }
		public string PatchGroup { get; }

	}
}
