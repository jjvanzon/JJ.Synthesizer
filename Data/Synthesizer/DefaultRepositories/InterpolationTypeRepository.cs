using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class InterpolationTypeRepository : RepositoryBase<InterpolationType, int>, IInterpolationTypeRepository
    {
        public InterpolationTypeRepository(IContext context)
            : base(context)
        { }

        public virtual IList<InterpolationType> GetAll() => _context.Query<InterpolationType>().ToArray();
    }
}
