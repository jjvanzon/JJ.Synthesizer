using JJ.Framework.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Configuration
{
    internal class ConfigurationSection
    {
        public PersistenceConfiguration MemoryPersistenceConfiguration { get; set; }
        public PersistenceConfiguration DatabasePersistenceConfiguration { get; set; }
    }
}
