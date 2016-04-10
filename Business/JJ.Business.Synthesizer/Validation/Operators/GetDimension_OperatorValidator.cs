using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class GetDimension_OperatorValidator : OperatorValidator_Base
    {
        public GetDimension_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.GetDimension, 
                  expectedInletCount: 0, 
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.Dimension })
        { }

        protected override void Execute()
        {
            base.Execute();

            string dimensionString = DataPropertyParser.TryGetString(Object, PropertyNames.Dimension);

            For(() => dimensionString, PropertyDisplayNames.Dimension)
                .NotNullOrEmpty()
                .IsEnum<DimensionEnum>();
        }
    }
}
