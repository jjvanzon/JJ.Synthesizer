using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Validators
{
    /// <summary>
    /// Validate the Number property of the view model, which is 1-based,
    /// to not confuse the user with a validation message talking about the 0-based ListIndex property,
    /// </summary>
    internal class OperatorPropertiesViewModel_ForPatchOutlet_Validator : VersatileValidator<OperatorPropertiesViewModel_ForPatchOutlet>
    {
        public OperatorPropertiesViewModel_ForPatchOutlet_Validator(OperatorPropertiesViewModel_ForPatchOutlet obj)
            : base(obj)
        { 
            For(() => obj.Number, ResourceFormatter.Number).GreaterThanOrEqual(1);
        }
    }
}
