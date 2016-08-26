using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public abstract class OperatorPropertiesViewModelBase : ViewModelBase
    {
        public int ID { get; set; }
        public int PatchID { get; set; }
        public string Name { get; set; }
    }
}
