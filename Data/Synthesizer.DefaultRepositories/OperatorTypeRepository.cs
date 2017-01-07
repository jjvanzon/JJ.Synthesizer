using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OperatorTypeRepository : RepositoryBase<OperatorType, int>, IOperatorTypeRepository
    {
        public OperatorTypeRepository(IContext context)
            : base(context)
        { }
    }
}
