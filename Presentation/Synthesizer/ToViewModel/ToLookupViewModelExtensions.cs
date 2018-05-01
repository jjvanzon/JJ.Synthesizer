using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
	internal static class ToLookupViewModelExtensions
	{
		// UnderlyingPatch

		public static IList<IDAndName> ToUnderlyingPatchLookupViewModel(this Document document)
		{
			if (document == null) throw new NullException(() => document);

			var list = new List<IDAndName>
			{
				new IDAndName { ID = 0, Name = null }
			};

			list.AddRange(
				PatchGrouper.GetGrouplessPatches(document.Patches, mustIncludeHidden: true)
							.OrderBy(ResourceFormatter.GetDisplayName)
							.Select(x => x.ToIDAndName()));

			list.AddRange(
				from patchGroupDto in PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(document.Patches, mustIncludeHidden: true)
				from patch in patchGroupDto.Patches
				let patchDisplayName = ResourceFormatter.GetDisplayName(patch)
				orderby patchGroupDto.FriendlyGroupName, patchDisplayName
				let name = $"{patchDisplayName} | {patchGroupDto.FriendlyGroupName}"
				select new IDAndName { ID = patch.ID, Name = name });

			IEnumerable<DocumentReference> lowerDocumentReferences = document.LowerDocumentReferences.OrderBy(x => x.GetAliasOrName());
			foreach (DocumentReference lowerDocumentReference in lowerDocumentReferences)
			{
				string lowerDocumentReferenceAliasOrName = lowerDocumentReference.GetAliasOrName();

				list.AddRange(
					from patch in PatchGrouper.GetGrouplessPatches(lowerDocumentReference.LowerDocument.Patches, mustIncludeHidden: false)
					let patchDisplayName = ResourceFormatter.GetDisplayName(patch)
					orderby patchDisplayName
					let name = $"{patchDisplayName} | {lowerDocumentReferenceAliasOrName}"
					select new IDAndName { ID = patch.ID, Name = name });

				list.AddRange(
					from patchGroupDto in PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(lowerDocumentReference.LowerDocument.Patches, mustIncludeHidden: false)
					from patch in patchGroupDto.Patches
					let patchDisplayName = ResourceFormatter.GetDisplayName(patch)
					orderby patchGroupDto.FriendlyGroupName, patchDisplayName
					let name = $"{patchDisplayName} | {patchGroupDto.FriendlyGroupName} | {lowerDocumentReferenceAliasOrName}"
					select new IDAndName { ID = patch.ID, Name = name });
			}

			return list;
		}
	}
}
