using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchInletOrOutlet_OperatorWrapper : OperatorWrapper
    {
        public PatchInletOrOutlet_OperatorWrapper(Operator op)
            : base(op)
        { }

        /// <summary> nullable </summary>
        public Outlet Input
        {
            get => Inlet.InputOutlet;
            set => Inlet.LinkTo(value);
        }

        /// <summary> not nullable </summary>
        public Inlet Inlet => WrappedOperator.Inlets.Single();
        /// <summary> not nullable </summary>
        public Outlet Outlet => WrappedOperator.Outlets.Single();
    }
}
