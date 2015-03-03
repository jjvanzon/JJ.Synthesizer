using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces
{
    /// <summary>
    /// Synthesizer needs its own basic repository interface
    /// instead of the one in JJ.Framework.Persistence,
    /// because a lot will not go by ID.
    /// </summary>
    public interface IRepository<T>
    {
        T Create();
    }
}
