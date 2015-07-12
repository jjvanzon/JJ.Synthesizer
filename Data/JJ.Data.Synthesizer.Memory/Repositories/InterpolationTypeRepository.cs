using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer.Memory.Helpers;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class InterpolationTypeRepository : JJ.Data.Synthesizer.DefaultRepositories.InterpolationTypeRepository
    {
        public InterpolationTypeRepository(IContext context)
            : base(context)
        {
            RepositoryHelper.EnsureEnumEntity(this, 1, "Block");
            RepositoryHelper.EnsureEnumEntity(this, 2, "Line");
        }
    }
}