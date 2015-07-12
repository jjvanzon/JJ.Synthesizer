using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JJ.Data.Synthesizer.Memory.Repositories
{
    public class IndentityRepository : JJ.Data.Synthesizer.DefaultRepositories.IdentityRepository
    {
        private static object _lock = new object();
        private static int _id = 1;

        public IndentityRepository(IContext context)
            : base(context)
        { }

        public override int GenerateID()
        {
            lock (_lock)
            {
                return _id++;
            }
        }
    }
}
