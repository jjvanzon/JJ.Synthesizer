using JJ.Framework.Data;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class IDRepository : JJ.Data.Synthesizer.DefaultRepositories.IDRepository
    {
        private static object _lock = new object();
        private static int _id = 1;

        public IDRepository(IContext context)
            : base(context)
        { }

        public override int GetID()
        {
            lock (_lock)
            {
                return _id++;
            }
        }
    }
}
