using JJ.Framework.Data;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;

namespace JJ.Data.Synthesizer.DefaultRepositories
{
    public class PatchRepository : RepositoryBase<Patch, int>, IPatchRepository
    {
        public PatchRepository(IContext context)
            : base(context)
        { }

        public Patch GetByName(string name)
        {
            Patch entity = TryGetByName(name);
            if (entity == null)
            {
                throw new NotFoundException<Patch>(new { name });
            }
            return entity;
        }

        public virtual Patch TryGetByName(string name) => throw new RepositoryMethodNotImplementedException();
    }
}
