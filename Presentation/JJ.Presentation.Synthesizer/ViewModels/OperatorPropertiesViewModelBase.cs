using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    /// <summary>
    /// Exists to make a base presenter for OperatorProperties work.
    /// </summary>
    public abstract class OperatorPropertiesViewModelBase : ViewModelBase
    {
        public int ID { get; set; }
    }
}
