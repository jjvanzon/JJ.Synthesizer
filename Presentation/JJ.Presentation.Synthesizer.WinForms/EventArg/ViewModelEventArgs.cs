using JJ.Framework.Reflection.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.EventArg
{
    internal class ViewModelEventArgs<TViewModel> : EventArgs
    {
        public TViewModel ViewModel { get; private set; }

        public ViewModelEventArgs(TViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            ViewModel = viewModel;
        }
    }
}
