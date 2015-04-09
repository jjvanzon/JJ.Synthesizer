using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Extensions
{
    public static class DeleteRelatedEntitiesExtensions
    {
        public static void DeleteRelatedEntities(
            this Operator op, 
            IInletRepository inletRepository, 
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (op == null) throw new NullException(() => op);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            foreach (Inlet inlet in op.Inlets)
            {
                inlet.UnlinkRelatedEntities();
                inletRepository.Delete(inlet);
            }

            foreach (Outlet outlet in op.Outlets)
            {
                outlet.UnlinkRelatedEntities();
                outletRepository.Delete(outlet);
            }

            entityPositionRepository.DeleteByEntityTypeAndEntityID(typeof(Operator).Name, op.ID);
        }
    }
}
