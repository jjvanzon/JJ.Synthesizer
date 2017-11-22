using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Enums;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
	public class OperatorWrapper_WithCollectionRecalculation : OperatorWrapper
	{
		public OperatorWrapper_WithCollectionRecalculation(Operator op)
			: base(op)
		{ }

		public CollectionRecalculationEnum CollectionRecalculation
		{
			get => DataPropertyParser.GetEnum<CollectionRecalculationEnum>(WrappedOperator, nameof(CollectionRecalculation));
			set => DataPropertyParser.SetValue(WrappedOperator, nameof(CollectionRecalculation), value);
		}
	}
}