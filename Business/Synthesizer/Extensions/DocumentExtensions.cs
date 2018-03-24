using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class DocumentExtensions
	{
		public static bool IsSystemDocument(this Document document)
		{
			if (document == null) throw new NullException(() => document);

			bool isSystemDocument = string.Equals(document.Name, DocumentHelper.SYSTEM_DOCUMENT_NAME);
			return isSystemDocument;
		}

		public static IList<Curve> GetCurves(this Document document)
		{
			if (document == null) throw new ArgumentNullException(nameof(document));

			return document.Patches
						   .SelectMany(x => x.Operators)
						   .Select(x => x.Curve)
						   .Where(x => x != null)
						   .ToArray();
		}

		public static IList<Patch> GetPatchesAndHigherDocumentPatches(this Document document)
		{
			if (document == null) throw new NullException(() => document);

			IList<Patch> patches = document.HigherDocumentReferences
										   .SelectMany(x => x.HigherDocument.Patches)
										   .Union(document.Patches)
										   .ToArray();
			return patches;
		}

		public static IList<Patch> GetPatchesAndVisibleLowerDocumentPatches(this Document document)
		{
			if (document == null) throw new NullException(() => document);

			IList<Patch> patches = document.LowerDocumentReferences
										   .SelectMany(x => x.LowerDocument.Patches)
										   .Where(x => !x.Hidden)
										   .Union(document.Patches)
										   .ToArray();
			return patches;
		}
	}
}
