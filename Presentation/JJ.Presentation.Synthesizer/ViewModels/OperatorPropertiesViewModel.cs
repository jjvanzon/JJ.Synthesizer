using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel : OperatorPropertiesViewModelBase
    {
        // Properties put directly here, instead of entity view model,
        // because entity view model is too elaborate.

        public string Name { get; set; }
        /// <summary> not editable </summary>
        public OperatorTypeViewModel OperatorType { get; set; }
    }
}
