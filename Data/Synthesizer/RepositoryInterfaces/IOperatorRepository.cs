using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
    public interface IOperatorRepository : IRepository<Operator, int>
    {
        IList<Operator> GetAll();
        IList<Operator> GetManyByOperatorTypeID(int operatorTypeID);
        IList<Operator> GetManyByOperatorTypeID_AndSingleDataKeyAndValue(int operatorTypeID, string dataKey, string dataValue);
    }
}
