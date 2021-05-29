using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Data;
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    [UsedImplicitly]
    public class SpeakerSetupRepository : RepositoryBase<SpeakerSetup, int>, ISpeakerSetupRepository
    {
        public SpeakerSetupRepository(IContext context)
            : base(context)
        { }

        /// <summary>
        /// Does not get the related entities immediately unless you override it in a specialized repository.
        /// </summary>
        public virtual SpeakerSetup GetWithRelatedEntities(int id) => Get(id);
    }
}
