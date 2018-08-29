using System;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Helpers
{
    internal class PatchCloner
    {
        private readonly RepositoryWrapper _repositories;

        public PatchCloner(RepositoryWrapper repositories) => _repositories = repositories;

        public Patch CloneWithRelatedEntities(Patch sourcePatch)
        {
            if (sourcePatch == null) throw new ArgumentNullException(nameof(sourcePatch));

            Patch destPatch = Clone(sourcePatch);



            throw new NotImplementedException();

            return destPatch;
        }

        private Patch Clone(Patch sourcePatch)
        {
            var destPatch = new Patch
            {
                ID = _repositories.IDRepository.GetID(),
                Name = sourcePatch.Name,
                GroupName = sourcePatch.GroupName,
                Hidden = sourcePatch.Hidden,
                HasDimension = sourcePatch.HasDimension,
                CustomDimensionName = sourcePatch.CustomDimensionName
            };

            destPatch.LinkTo(sourcePatch.Document);
            destPatch.LinkTo(sourcePatch.StandardDimension);

            _repositories.PatchRepository.Insert(destPatch);

            return destPatch;
        }
    }
}