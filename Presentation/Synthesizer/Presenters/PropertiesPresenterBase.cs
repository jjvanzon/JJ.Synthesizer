using System;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal abstract class PropertiesPresenterBase<TViewModel> : PresenterBase<TViewModel>
        where TViewModel : ViewModelBase
    {
        private TViewModel TemplateMethod(TViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            throw new NotImplementedException();
        }
    }
}
