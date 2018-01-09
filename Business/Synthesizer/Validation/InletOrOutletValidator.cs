using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Interfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation
{
	internal class InletOrOutletValidator : VersatileValidator
	{
		public InletOrOutletValidator(IInletOrOutlet inletOrOutlet)
		{
			if (inletOrOutlet == null) throw new NullException(() => inletOrOutlet);

			ExecuteValidator(new IDValidator(inletOrOutlet.ID));

			ExecuteValidator(new NameValidator(inletOrOutlet.Name, required: false));

			OperatorTypeEnum operatorTypeEnum = inletOrOutlet.Operator.GetOperatorTypeEnum();

			bool repetitionPositionMustBeNull = !inletOrOutlet.IsRepeating ||
												operatorTypeEnum == OperatorTypeEnum.PatchInlet ||
												operatorTypeEnum == OperatorTypeEnum.PatchOutlet;
			if (repetitionPositionMustBeNull)
			{
				For(inletOrOutlet.RepetitionPosition, ResourceFormatter.RepetitionPosition).IsNull();
			}
			else
			{
				For(inletOrOutlet.RepetitionPosition, ResourceFormatter.RepetitionPosition).NotNull();
			}
		}
	}
}
