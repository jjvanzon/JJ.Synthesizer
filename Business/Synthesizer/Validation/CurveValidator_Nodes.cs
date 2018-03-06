using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Resources;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class CurveValidator_Nodes : VersatileValidator
	{
		public CurveValidator_Nodes(Curve curve)
		{
			if (curve == null) throw new NullException(() => curve);

			For(curve.Nodes.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Nodes)).GreaterThanOrEqual(2);

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
