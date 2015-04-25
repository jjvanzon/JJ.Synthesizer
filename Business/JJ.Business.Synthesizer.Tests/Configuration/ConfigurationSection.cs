using JJ.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Configuration
{
    internal class ConfigurationSection
    {
        public PersistenceConfiguration MemoryPersistence { get; set; }
        public PersistenceConfiguration DatabasePersistence { get; set; }
    }
}
