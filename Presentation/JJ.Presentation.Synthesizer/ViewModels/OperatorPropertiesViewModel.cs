using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public sealed class OperatorPropertiesViewModel : OperatorPropertiesViewModelBase
    {
        // Even though OperatorPropertiesViewModel does not add anything to OperatorPropertiesViewModelBase,
        // it is safer this way, to prevent that OperatorPropertiesViewModel can be polymorphic.
    }
}
