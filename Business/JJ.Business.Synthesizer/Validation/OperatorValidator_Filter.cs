using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Filter : OperatorValidator_Base
    {
        public OperatorValidator_Filter(Operator obj)
            : base(obj, OperatorTypeEnum.Filter, expectedInletCount: 5, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            string filterTypeString = OperatorDataParser.GetString(Object, PropertyNames.FilterType);

            For(() => filterTypeString, PropertyDisplayNames.FilterType)
                .NotNullOrEmpty()
                .IsEnum<FilterTypeEnum>()
                .IsNot(FilterTypeEnum.Undefined);
        }
    }
}
