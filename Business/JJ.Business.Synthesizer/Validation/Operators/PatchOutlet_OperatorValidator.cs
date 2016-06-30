using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.OperatorData;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PatchOutlet_OperatorValidator : OperatorValidator_Base
    {
        public PatchOutlet_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.PatchOutlet, 
                  expectedInletCount: 1, 
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.ListIndex })
        { }

        protected override void Execute()
        {
            base.Execute();

            Execute(new ListIndex_OperatorData_Validator(Object.Data));

            var patchOutletWrapper = new PatchOutlet_OperatorWrapper(Object);

            For(() => patchOutletWrapper.Result.GetDimensionEnum(), PropertyDisplayNames.Dimension)
                .IsEnum<DimensionEnum>();

            // PatchOutlet.Result.Dimension is optional.
            // PatchOutlet.Name is optional.
        }
    }
}
