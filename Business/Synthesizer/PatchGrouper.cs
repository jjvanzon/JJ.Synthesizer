using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer
{
	public static class PatchGrouper
	{
		public static IList<PatchGroupDto> GetPatchGroupDtos_ExcludingGroupless(IList<Patch> patchesInDocument, bool mustIncludeHidden)
		{
			if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

			List<PatchGroupDto> results = GetPatchGroupNames(patchesInDocument, mustIncludeHidden)
			                              .Select(
				                              x => new PatchGroupDto
				                              {
					                              FriendlyGroupName = x,
					                              CanonicalGroupName = NameHelper.ToCanonical(x),
					                              Patches = GetPatchesInGroup(patchesInDocument, x, mustIncludeHidden)
				                              })
			                              .ToList();
			return results;
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public static IList<string> GetPatchGroupNames(IList<Patch> patchesInDocument, bool mustIncludeHidden)
		{
			if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

			IList<string> groupNames = patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
			                                            .Where(x => NameHelper.IsFilledIn(x.GroupName))
			                                            .Distinct(x => NameHelper.ToCanonical(x.GroupName))
			                                            .Select(x => x.GroupName)
			                                            .ToList();
			return groupNames;
		}

		public static IList<Patch> GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(
			IList<Patch> patchesInDocument,
			string groupName,
			bool mustIncludeHidden)
		{
			if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

			if (string.IsNullOrWhiteSpace(groupName))
			{
				return GetGrouplessPatches(patchesInDocument, mustIncludeHidden);
			}

			return GetPatchesInGroup(patchesInDocument, groupName, mustIncludeHidden);
		}

		public static IList<Patch> GetGrouplessPatches(IList<Patch> patchesInDocument, bool mustIncludeHidden)
		{
			if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

			IList<Patch> list = patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
			                                     .Where(x => string.IsNullOrWhiteSpace(x.GroupName))
			                                     .ToArray();
			return list;
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public static IList<Patch> GetPatchesInGroup(IList<Patch> patchesInDocument, string groupName, bool mustIncludeHidden)
		{
			if (patchesInDocument == null) throw new NullException(() => patchesInDocument);
			if (string.IsNullOrWhiteSpace(groupName)) throw new NullOrWhiteSpaceException(() => groupName);

			IList<Patch> patchesInGroup =
				patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
				                 .Where(x => NameHelper.IsFilledIn(x.GroupName))
				                 .Where(x => NameHelper.AreEqual(x.GroupName, groupName))
				                 .ToArray();

			return patchesInGroup;
		}
	}
}