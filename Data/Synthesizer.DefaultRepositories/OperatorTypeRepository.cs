using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OperatorTypeRepository : RepositoryBase<OperatorType, int>, IOperatorTypeRepository
    {
        public OperatorTypeRepository(IContext context)
            : base(context)
        { }
    }
}
