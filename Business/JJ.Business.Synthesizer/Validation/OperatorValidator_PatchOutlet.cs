using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.Validation
{
    public class OperatorValidator_PatchOutlet : OperatorValidator_Base_WithoutData
    {
        public OperatorValidator_PatchOutlet(Operator obj)
            : base(obj, OperatorTypeEnum.PatchOutlet, 1, PropertyNames.Input, PropertyNames.Result)
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Name, CommonTitles.Name).NotNullOrEmpty();

            For(() => Object.Data, PropertyDisplayNames.SortOrder)
                // TODO: Uncomment code line after PatchInlet and PatchOutlet operators get their own Properties view in which you can enter a SortOrder.
                //.NotNullOrEmpty()
                .IsInteger();
        }
    }
}
