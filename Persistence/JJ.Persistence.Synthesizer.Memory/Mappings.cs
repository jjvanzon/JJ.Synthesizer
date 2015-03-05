using JJ.Framework.Persistence.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Persistence.Synthesizer.Memory
{
    // TODO: Make mapping optional for memory persistance?

    public class CurveMapping : MemoryMapping<Curve>
    {
        public CurveMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }

    public class InletMapping : MemoryMapping<Inlet>
    {
        public InletMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }

    public class NodeMapping : MemoryMapping<Node>
    {
        public NodeMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }

    public class NodeTypeMapping : MemoryMapping<NodeType>
    {
        public NodeTypeMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }

    public class OperatorMapping : MemoryMapping<Operator>
    {
        public OperatorMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }

    public class OutletMapping : MemoryMapping<Outlet>
    {
        public OutletMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }

    public class PatchMapping : MemoryMapping<Patch>
    {
        public PatchMapping()
        {
            IdentityType = IdentityType.AutoIncrement;
            IdentityPropertyName = PropertyNames.ID;
        }
    }
}
