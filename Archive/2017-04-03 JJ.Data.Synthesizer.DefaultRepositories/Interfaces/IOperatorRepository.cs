using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.DefaultRepositories.Interfaces
{
    public interface IOperatorRepository : IRepository<Operator, int>
    {
        IList<Operator> GetManyByOperatorTypeID(int operatorTypeID);
    }
}
