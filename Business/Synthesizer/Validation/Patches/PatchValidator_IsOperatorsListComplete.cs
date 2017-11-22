using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;

namespace JJ.Business.Synthesizer.Validation.Patches
{
	internal class PatchValidator_IsOperatorsListComplete : ValidatorBase
	{
		public PatchValidator_IsOperatorsListComplete(Patch patch, ICurveRepository curveRepository)
		{
			if (patch == null) throw new NullException(() => patch);
			if (curveRepository == null) throw new NullException(() => curveRepository);

			IList<Operator> operatorsInGraph = patch.GetOperatorsRecursive();
			IList<Operator> operatorsMissingInList = operatorsInGraph.Except(patch.Operators).ToArray();

			foreach (Operator operatorMissingInList in operatorsMissingInList)
			{
				string messagePrefix = ValidationHelper.GetMessagePrefix(operatorMissingInList, curveRepository);
				string message = ResourceFormatter.OperatorIsInGraphButNotInList;

				Messages.Add(messagePrefix + message);
			}
		}
	}
}
