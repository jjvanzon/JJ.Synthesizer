using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Validators
{
    internal class OperatorPropertiesViewModel_ForPatchInlet_Validator : VersatileValidator<OperatorPropertiesViewModel_ForPatchInlet>
    {
        public OperatorPropertiesViewModel_ForPatchInlet_Validator(OperatorPropertiesViewModel_ForPatchInlet obj)
            : base(obj)
        { 
            For(() => obj.DefaultValue, ResourceFormatter.DefaultValue).IsDouble();
        }
    }
}
