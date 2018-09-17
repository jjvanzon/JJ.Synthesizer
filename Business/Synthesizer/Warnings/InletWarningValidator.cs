using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Warnings
{
	internal class InletWarningValidator : VersatileValidator
	{
		public InletWarningValidator(Inlet inlet)
		{
			if (inlet == null) throw new NullException(() => inlet);

			bool isPatchInlet = inlet.Operator.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet;
			if (isPatchInlet)
			{
				return;
			}

			if (inlet.WarnIfEmpty && inlet.InputOutlet == null)
			{
				Messages.AddNotFilledInMessage(nameof(Inlet));
			}

			if (inlet.IsObsolete && inlet.InputOutlet != null)
			{
				Messages.Add(ResourceFormatter.ObsoleteButStillUsed);
			}
		}
	}
}
