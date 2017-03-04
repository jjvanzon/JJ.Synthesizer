using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation.Curves
{
    internal class CurveValidator_Nodes : VersatileValidator<Curve>
    {
        public CurveValidator_Nodes(Curve obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Curve curve = Obj;

            For(() => curve.Nodes.Count, CommonResourceFormatter.ObjectCount_WithNamePlural(ResourceFormatter.Nodes)).GreaterThanOrEqual(2);

            int i = 1;
            foreach (Node node in curve.Nodes)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(node, i);

                ExecuteValidator(new NodeValidator_WithoutParent(node), messagePrefix);
                ExecuteValidator(new NodeValidator_Parent(node), messagePrefix);

                i++;
            }
        }
    }
}
