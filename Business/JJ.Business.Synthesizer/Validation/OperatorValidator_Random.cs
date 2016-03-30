using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_Random : OperatorValidator_Base
    {
        public OperatorValidator_Random(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Random, 
                  expectedInletCount: 2, 
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[] { PropertyNames.InterpolationType })
        { }

        protected override void Execute()
        {
            base.Execute();

            string interpolationType = DataPropertyParser.TryGetString(Object, PropertyNames.InterpolationType);

            For(() => interpolationType, PropertyDisplayNames.InterpolationType)
                .NotNullOrEmpty()
                .IsEnum<ResampleInterpolationTypeEnum>()
                .IsNot(ResampleInterpolationTypeEnum.Undefined);
        }
    }
}