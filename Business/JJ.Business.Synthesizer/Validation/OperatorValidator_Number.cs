using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Number : OperatorValidator_Base
    {
        public OperatorValidator_Number(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Number, 
                  expectedInletCount: 0, 
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[] { PropertyNames.Number })
        { }

        protected override void Execute()
        {
            base.Execute();

            string numberString = DataPropertyParser.TryGetString(Object, PropertyNames.Number);

            For(() => numberString, PropertyDisplayNames.Number, DataPropertyParser.FormattingCulture)
                .NotNullOrEmpty()
                .IsDouble()
                .NotInfinity()
                .NotNaN();
        }
    }
}
