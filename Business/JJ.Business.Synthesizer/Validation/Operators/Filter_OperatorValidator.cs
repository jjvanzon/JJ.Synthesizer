using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Filter_OperatorValidator : OperatorValidator_Base
    {
        public Filter_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Filter, 
                  expectedInletCount: 5, 
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.FilterType })
        { }

        protected override void Execute()
        {
            base.Execute();

            string filterTypeString = DataPropertyParser.TryGetString(Object, PropertyNames.FilterType);

            For(() => filterTypeString, PropertyDisplayNames.FilterType)
                .NotNullOrEmpty()
                .IsEnum<FilterTypeEnum>()
                .IsNot(FilterTypeEnum.Undefined);
        }
    }
}
