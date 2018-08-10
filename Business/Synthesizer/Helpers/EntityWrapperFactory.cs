using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;

namespace JJ.Business.Synthesizer.Helpers
{
	public static class EntityWrapperFactory
	{
		public static OperatorWrapper CreateOperatorWrapper(Operator op)
		{
			if (op == null) throw new NullException(() => op);

			OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
			switch (operatorTypeEnum)
			{
				case OperatorTypeEnum.Cache: return new Cache_OperatorWrapper(op);
				case OperatorTypeEnum.Number: return new Number_OperatorWrapper(op);
				case OperatorTypeEnum.PatchInlet: return new PatchInletOrOutlet_OperatorWrapper(op);
				case OperatorTypeEnum.PatchOutlet: return new PatchInletOrOutlet_OperatorWrapper(op);
				case OperatorTypeEnum.Reset: return new Reset_OperatorWrapper(op);

				case OperatorTypeEnum.AverageOverDimension:
				case OperatorTypeEnum.ClosestOverDimension:
				case OperatorTypeEnum.ClosestOverDimensionExp:
				case OperatorTypeEnum.MaxOverDimension:
				case OperatorTypeEnum.MinOverDimension:
				case OperatorTypeEnum.SortOverDimension:
				case OperatorTypeEnum.SumOverDimension:
					return new OperatorWrapper_WithCollectionRecalculation(op);

				case OperatorTypeEnum.InletsToDimension:
				case OperatorTypeEnum.Random:
				case OperatorTypeEnum.RandomStripe:
					return new OperatorWrapper_WithInterpolation(op);

				case OperatorTypeEnum.Interpolate:
					return new OperatorWrapper_WithInterpolation_AndFollowingMode(op);

				default:
					return new OperatorWrapper(op);
			}
		}
	}
}