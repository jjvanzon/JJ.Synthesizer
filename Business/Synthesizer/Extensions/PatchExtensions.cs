using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Extensions
{
	public static class PatchExtensions
	{
		public static IEnumerable<Operator> EnumerateOperatorsOfType(this Patch patch, OperatorTypeEnum operatorTypeEnum)
		{
			if (patch == null) throw new NullException(() => patch);

			return patch.Operators.Where(x => x.GetOperatorTypeEnum() == operatorTypeEnum);
		}

		public static IList<Operator> GetOperatorsOfType(this Patch patch, OperatorTypeEnum operatorTypeEnum)
		{
			return EnumerateOperatorsOfType(patch, operatorTypeEnum).ToArray();
		}

		public static bool IsSystemPatch(this Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);
			if (patch.Document == null) throw new NullException(() => patch.Document);

			return patch.Document.IsSystemDocument();
		}

		public static bool IsSamplePatch(this Patch patch)
		{
			if (patch == null) throw new NullException(() => patch);

			if (!patch.IsSystemPatch())
			{
				return false;
			}

			return string.Equals(patch.Name, nameof(SystemPatchNames.Sample));
		}

		public static bool IsExternal(this Patch patch, Document currentDocument) => patch.Document?.ID != currentDocument?.ID;
	}
}