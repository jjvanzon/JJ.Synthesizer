﻿using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class OperatorRepository : RepositoryBase<Operator, int>, IOperatorRepository
    {
        public OperatorRepository(IContext context)
            : base(context)
        { }

        public virtual IList<Operator> GetManyByOperatorTypeID(int operatorTypeID)
        {
            return _context.Query<Operator>()
                           .Where(x => x.OperatorType.ID == operatorTypeID)
                           .ToArray();
        }
    }
}
