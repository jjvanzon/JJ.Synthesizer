using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory.Repositories
{
    public class InterpolationTypeRepository : JJ.Persistence.Synthesizer.DefaultRepositories.InterpolationTypeRepository
    {
        public InterpolationTypeRepository(IContext context)
            : base(context)
        {
            InterpolationType entity;

            // TODO: I need to be able to specify identity explicit or something
            // Not just auto-increment or NoIDs

            entity = Create();
            entity.Name = "Block";

            entity = Create();
            entity.Name = "Line";
        }
    }
}