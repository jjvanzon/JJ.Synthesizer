using JJ.Framework.Persistence.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory
{
    // TODO: Make mapping optional for memory persistance.
    public class OperatorMapping : MemoryMapping<Operator>
    {
        public OperatorMapping()
        {
            IdentityType = IdentityType.NoIDs;
        }
    }

    public class InletMapping : MemoryMapping<Inlet>
    {
        public InletMapping()
        {
            IdentityType = IdentityType.NoIDs;
        }
    }

    public class OutletMapping : MemoryMapping<Outlet>
    {
        public OutletMapping()
        {
            IdentityType = IdentityType.NoIDs;
        }
    }
}
