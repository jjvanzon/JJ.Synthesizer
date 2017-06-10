using System.Linq;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class PatchOutlet_OperatorWrapper : OperatorWrapperBase_WithOneOutlet
    {
        public PatchOutlet_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Input
        {
            get => Inlet.InputOutlet;
            set => Inlet.LinkTo(value);
        }

        public Inlet Inlet => WrappedOperator.Inlets.Single();
        public Outlet Outlet => WrappedOperator.Outlets.Single();

        public override string GetInletDisplayName(Inlet inlet) => ResourceFormatter.Input;
    }
}
