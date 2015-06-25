using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OperatorTypeRepository : RepositoryBase<OperatorType, int>, IOperatorTypeRepository
    {
        public OperatorTypeRepository(IContext context)
            : base(context)
        { }

        public IList<OperatorType> GetAllOrderedBySortOrder()
        {
            return _context.Query<OperatorType>().OrderBy(x => x.SortOrder).ToArray();
        }
    }
}
