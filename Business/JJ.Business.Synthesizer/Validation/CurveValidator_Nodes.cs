using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class CurveValidator_Nodes : FluentValidator<Curve>
    {
        public CurveValidator_Nodes(Curve obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Curve curve = Object;

            For(() => curve.Nodes.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Nodes)).GreaterThanOrEqual(2);

            int i = 1;
            foreach (Node node in curve.Nodes)
            {
                string messagePrefix = ValidationHelper.GetMessagePrefix(node, i);

                Execute(new NodeValidator_WithoutParent(node), messagePrefix);
                Execute(new NodeValidator_Parent(node), messagePrefix);

                i++;
            }
        }
    }
}
