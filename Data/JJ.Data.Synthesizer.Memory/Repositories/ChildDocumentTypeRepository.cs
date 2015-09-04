using JJ.Framework.Data;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class ChildDocumentTypeRepository : JJ.Data.Synthesizer.DefaultRepositories.ChildDocumentTypeRepository
    {
        public ChildDocumentTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Instrument");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Effect");
       }
    }
}