using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer
{
    public static class PatchGrouper
    {
        public static IList<DocumentReferencePatchGroupDto> GetDocumentReferencePatchGroupDtos_IncludingGrouplessIfAny([NotNull] IList<DocumentReference> documentReferences, bool mustIncludeHidden)
        {
            if (documentReferences == null) throw new NullException(() => documentReferences);

            var dtos = new List<DocumentReferencePatchGroupDto>();

            IEnumerable<DocumentReference> lowerDocumentReferences = documentReferences.Where(x => x.LowerDocument != null);
            foreach (DocumentReference lowerDocumentReference in lowerDocumentReferences)
            {
                var dto = new DocumentReferencePatchGroupDto
                {
                    LowerDocumentReference = lowerDocumentReference,
                    Groups = new List<PatchGroupDto>()
                };

                IList<PatchGroupDto> patchGroupDtos = GetPatchGroupDtos_IncludingGrouplessIfAny(lowerDocumentReference.LowerDocument.Patches, mustIncludeHidden);
                dto.Groups.AddRange(patchGroupDtos);

                dtos.Add(dto);
            }

            return dtos;
        }

        public static IList<PatchGroupDto> GetPatchGroupDtos_IncludingGrouplessIfAny(IList<Patch> patchesInDocument, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            var dtos = new List<PatchGroupDto>();

            IList<Patch> grouplessPatches = GetGrouplessPatches(patchesInDocument, mustIncludeHidden);
            dtos.Add(new PatchGroupDto { Patches = grouplessPatches });

            dtos.AddRange(GetPatchGroupDtos_ExcludingGroupless(patchesInDocument, mustIncludeHidden));

            return dtos;
        }

        public static IList<PatchGroupDto> GetPatchGroupDtos_ExcludingGroupless(IList<Patch> patchesInDocument, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            // TODO: Low Priority:
            // This reuses the logic in the other methods, so there can be no inconsistencies,
            // but it would be faster to put all the code here.
            // Perhaps you can group in one method and delegate the rest of the methods to
            // the grouping method.

            var dtos = new List<PatchGroupDto>();

            IList<string> groupNames = GetPatchGroupNames(patchesInDocument, mustIncludeHidden);

            foreach (string groupName in groupNames)
            {
                string canonicalGroupName = NameHelper.ToCanonical(groupName);

                IList<Patch> patchesInGroup = GetPatchesInGroup(patchesInDocument, groupName, mustIncludeHidden);

                dtos.Add(
                    new PatchGroupDto
                    {
                        FriendlyGroupName = groupName,
                        CanonicalGroupName = canonicalGroupName,
                        Patches = patchesInGroup
                    });
            }

            return dtos;
        }

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

        public static IList<Patch> GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(IList<Patch> patchesInDocument, string groupName, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            if (string.IsNullOrWhiteSpace(groupName))
            {
                return GetGrouplessPatches(patchesInDocument, mustIncludeHidden);
            }
            else
            {
                return GetPatchesInGroup(patchesInDocument, groupName, mustIncludeHidden);
            }
        }

        public static IList<Patch> GetGrouplessPatches(IList<Patch> patchesInDocument, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);

            IList<Patch> list = patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
                                                 .Where(x => string.IsNullOrWhiteSpace(x.GroupName))
                                                 .ToArray();

            return list;
        }

        public static IList<Patch> GetPatchesInGroup(IList<Patch> patchesInDocument, string groupName, bool mustIncludeHidden)
        {
            if (patchesInDocument == null) throw new NullException(() => patchesInDocument);
            if (string.IsNullOrWhiteSpace(groupName)) throw new NullOrWhiteSpaceException(() => groupName);

            string canonicalGroupName = NameHelper.ToCanonical(groupName);

            IList<Patch> patchesInGroup =
                patchesInDocument.Where(x => !x.Hidden || mustIncludeHidden)
                                 .Where(x => NameHelper.IsFilledIn(x.GroupName))
                                 .Where(x => string.Equals(NameHelper.ToCanonical(x.GroupName), canonicalGroupName))
                                 .ToArray();

            return patchesInGroup;
        }
    }
}
