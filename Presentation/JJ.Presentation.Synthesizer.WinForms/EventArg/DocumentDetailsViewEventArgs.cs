using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    /// <summary>
    /// Requred because an event with a generic EventArgs does not show up in the WinForms designer.
    /// </summary>
    internal class DocumentDetailsViewEventArgs : ViewModelEventArgs<DocumentDetailsViewModel>
    {
        public DocumentDetailsViewEventArgs(DocumentDetailsViewModel viewModel)
            : base(viewModel)
        { }
    }
}
