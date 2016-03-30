using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Extensions;
using System;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation
{
    internal class OperatorValidator_PatchInlet : OperatorValidator_Base
    {
        public OperatorValidator_PatchInlet(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.PatchInlet, 
                  expectedInletCount: 1, 
                  expectedOutletCount: 1,
                  expectedDataKeys: new string[] { PropertyNames.ListIndex })
        { }

        protected override void Execute()
        {
            base.Execute();

            string listIndexString = DataPropertyParser.TryGetString(Object, PropertyNames.ListIndex);
            For(() => listIndexString, PropertyDisplayNames.ListIndex)
                .NotNullOrEmpty()
                .IsInteger()
                .GreaterThanOrEqual(0);

            var patchInletWrapper = new PatchInlet_OperatorWrapper(Object);
            For(() => patchInletWrapper.Inlet.GetInletTypeEnum(), PropertyDisplayNames.InletType).IsEnum<InletTypeEnum>();

            // PatchInlet.Inlet.InletType is optional.
            // patchInlet.Name is optional.
        }
    }
}
