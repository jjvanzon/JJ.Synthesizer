using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Persistence.Synthesizer.DefaultRepositories
{
    /// <summary>
    /// Synthesizer needs its own basic repository
    /// instead of the one in JJ.Framework.Persistence,
    /// because a lot will not go by ID.
    /// </summary>
    public class RepositoryBase<T> : IRepository<T>
        where T : class, new()
    {
        protected IContext _context;

        public RepositoryBase(IContext context)
        {
            if (context == null) throw new NullException(() => context);
            _context = context;
        }

        public T Create()
        {
            return _context.Create<T>();
        }
    }
}
