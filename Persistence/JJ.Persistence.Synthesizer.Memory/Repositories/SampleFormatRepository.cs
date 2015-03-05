using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class SampleFormatRepository : JJ.Persistence.Synthesizer.DefaultRepositories.SampleFormatRepository
    {
        public SampleFormatRepository(IContext context)
            : base(context)
        {
            SampleFormat entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Undefined";

            entity = Create();
            entity.Name = "Raw";

            entity = Create();
            entity.Name = "Wav";
        }
    }
}