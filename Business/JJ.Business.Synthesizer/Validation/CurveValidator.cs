using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;

namespace JJ.Business.Synthesizer.Validation
{
    internal class CurveValidator : FluentValidator<Curve>
    {
        public CurveValidator(Curve obj)
            : base(obj)
        { }

        protected override void Execute()
        {
            Curve curve = Object;

            Execute(new NameValidator(curve.Name, required: false));

            For(() => curve.Nodes.Count, CommonTitleFormatter.EntityCount(PropertyDisplayNames.Nodes)).MinValue(2);

            int i = 1;
            foreach (Node node in curve.Nodes)
            {
                Execute(new NodeValidator_WithoutParent(node), ValidationHelper.GetMessagePrefix(node, i));
                Execute(new NodeValidator_Parent(node), ValidationHelper.GetMessagePrefix(node, i));

                i++;
            }
        }
    }
}
