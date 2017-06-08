using System.Collections.Generic;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.RepositoryInterfaces
{
    public interface IInterpolationTypeRepository : IRepository<InterpolationType, int>
    {
        IList<InterpolationType> GetAll();
    }
}
