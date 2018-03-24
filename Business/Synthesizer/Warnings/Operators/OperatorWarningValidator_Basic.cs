using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings.Operators
{
	internal class OperatorWarningValidator_Basic : VersatileValidator
	{
		public OperatorWarningValidator_Basic(Operator op)
		{
			if (op == null) throw new NullException(() => op);

			For(op.UnderlyingPatch, ResourceFormatter.UnderlyingPatch).NotNull();

			foreach (Inlet inlet in op.Inlets)
			{
				ExecuteValidator(new InletWarningValidator(inlet), ValidationHelper.GetMessagePrefix(inlet));
			}

			foreach (Outlet outlet in op.Outlets)
			{
				ExecuteValidator(new OutletWarningValidator(outlet), ValidationHelper.GetMessagePrefix(outlet));
			}
		}
	}
}