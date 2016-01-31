using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Random : OperatorValidator_Base
    {
        public OperatorValidator_Random(Operator obj)
            : base(obj, OperatorTypeEnum.Random, expectedInletCount: 2, expectedOutletCount: 1)
        { }

        protected override void Execute()
        {
            base.Execute();

            string interpolationType = OperatorDataParser.GetString(Object, PropertyNames.InterpolationTypeEnum);

            // TODO: Enable IsNullOrEmpty validation when there is a property box for it.
            // TODO: Validate enum again, when FluentValidator can handle it.
            //For(() => interpolationType, PropertyDisplayNames.InterpolationType)
                //.NotNullOrEmpty();
                //.IsEnum<InterpolationTypeEnum>()
                //.IsNot(InterpolationTypeEnum.Undefined);
        }
    }
}