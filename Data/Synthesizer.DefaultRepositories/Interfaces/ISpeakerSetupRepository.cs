using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface ISpeakerSetupRepository : IRepository<SpeakerSetup, int>
    {
        SpeakerSetup GetWithRelatedEntities(int id);
    }
}
