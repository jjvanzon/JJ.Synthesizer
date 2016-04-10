using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class SetDimension_OperatorValidator : OperatorValidator_Base
    {
        public SetDimension_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.SetDimension, 
                  expectedInletCount: 2, 
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
