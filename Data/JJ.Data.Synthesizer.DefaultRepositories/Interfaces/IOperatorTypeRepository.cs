using JJ.Framework.Data;
using System.Collections.Generic;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IOperatorTypeRepository : IRepository<OperatorType, int>
    {
        IList<OperatorType> GetAllOrderedBySortOrder();
    }
}
