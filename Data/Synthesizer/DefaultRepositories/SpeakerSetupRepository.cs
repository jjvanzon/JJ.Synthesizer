using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class SpeakerSetupRepository : RepositoryBase<SpeakerSetup, int>, ISpeakerSetupRepository
    {
        public SpeakerSetupRepository(IContext context)
            : base(context)
        { }

        /// <summary>
        /// Does not get the related entities immediately unless you override it in a specialized repository.
        /// </summary>
        public virtual SpeakerSetup GetWithRelatedEntities(int id)
        {
            return Get(id);
        }
    }
}
