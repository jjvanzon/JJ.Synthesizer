using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal class OperatorPropertiesUserControlBase : PropertiesUserControlBase
    {
        private new OperatorPropertiesViewModelBase ViewModel => (OperatorPropertiesViewModelBase)base.ViewModel;

        protected override int GetID()
        {
            return ViewModel.ID;
        }
    }
}
