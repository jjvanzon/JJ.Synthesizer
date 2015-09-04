using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OperatorRepository : RepositoryBase<Operator, int>, IOperatorRepository
    {
        public OperatorRepository(IContext context)
            : base(context)
        { }
    }
}
