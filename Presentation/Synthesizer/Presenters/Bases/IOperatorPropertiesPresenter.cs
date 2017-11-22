using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
	internal interface IOperatorPropertiesPresenter
	{
		OperatorPropertiesViewModelBase Play(OperatorPropertiesViewModelBase userInput);
		OperatorPropertiesViewModelBase Delete(OperatorPropertiesViewModelBase userInput);
	}
}