//using JJ.Framework.Reflection.Exceptions;
//using JJ.Presentation.Synthesizer.ToViewModel;
//using JJ.Presentation.Synthesizer.ViewModels;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    internal class NotFoundPresenter
//    {
//        public NotFoundViewModel Show(string entityTypeDisplayName)
//        {
//            NotFoundViewModel viewModel = ViewModelHelper.CreateNotFoundViewModel_WithEntityTypeDisplayName(entityTypeDisplayName);
//            viewModel.Visible = true;
//            return viewModel;
//        }

//        public NotFoundViewModel OK(NotFoundViewModel userInput)
//        {
//            if (userInput == null) throw new NullException(() => userInput);
//            NotFoundViewModel viewModel = ViewModelHelper.CreateNotFoundViewModel_WithMessage(userInput.Message);
//            viewModel.Visible = false;
//            return viewModel;
//        }
//    }
//}
