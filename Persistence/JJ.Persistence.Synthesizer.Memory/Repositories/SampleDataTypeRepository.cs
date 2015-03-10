using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class SampleDataTypeRepository : JJ.Persistence.Synthesizer.DefaultRepositories.SampleDataTypeRepository
    {
        private readonly object _lock = new object();

        public SampleDataTypeRepository(IContext context)
            : base(context)
        {
            SampleDataType entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs
            
            lock (_lock)
            {
                entity = TryGet(1);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Byte";
                }

                entity = TryGet(2);
                if (entity == null)
                {
                    entity = Create();
                    entity.Name = "Int16";
                }
            }
        }
    }
}