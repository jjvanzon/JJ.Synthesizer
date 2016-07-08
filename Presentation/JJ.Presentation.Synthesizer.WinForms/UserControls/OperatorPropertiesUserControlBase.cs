using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal class OperatorPropertiesUserControlBase<TViewModel> : PropertiesUserControlBase<TViewModel>
         where TViewModel : OperatorPropertiesViewModelBase
    {
        protected override int GetID()
        {
            return ViewModel.ID;
        }
    }
}
