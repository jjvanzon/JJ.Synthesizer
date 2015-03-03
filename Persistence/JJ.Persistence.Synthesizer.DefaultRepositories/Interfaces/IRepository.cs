using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces
{
    // Synthesizer needs its own basic repository interface
    // instead of the one in JJ.Framework.Persistence,
    // because a lot will not go by ID.
    public interface IRepository<T>
    {
        T Create();
    }
}
