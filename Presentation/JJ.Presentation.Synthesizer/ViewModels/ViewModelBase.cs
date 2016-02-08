using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.ViewModels
{
    public abstract class ViewModelBase
    {
        public bool Successful { get; set; }
        public IList<Message> ValidationMessages { get; set; }
        public bool Visible { get; set; }
    }
}
