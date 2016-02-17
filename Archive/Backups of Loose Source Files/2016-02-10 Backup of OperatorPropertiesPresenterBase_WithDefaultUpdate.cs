//using JJ.Data.Canonical;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Business.Synthesizer;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Presentation.Synthesizer.ToViewModel;
//using JJ.Framework.Common;
//using System;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    /// <summary> 
//    /// Contains all code shared by operator properties presenters 
//    /// and a default implementation for the Update method.
//    /// Works when Update does not require any additional business logic than PatchManager.SaveOperator.
//    /// </summary>
//    internal abstract class OperatorPropertiesPresenterBase_WithDefaultUpdate<TViewModel>
//        : OperatorPropertiesPresenterBase<TViewModel>
//        where TViewModel : OperatorPropertiesViewModelBase
//    {
//        private PatchRepositories _repositories;

//        public OperatorPropertiesPresenterBase_WithDefaultUpdate(PatchRepositories repositories)
//            : base(repositories.OperatorRepository)
//        {
//            if (repositories == null) throw new NullException(() => repositories);

//            _repositories = repositories;
//        }

//        protected override TViewModel Update(TViewModel userInput)
//        {
//            if (userInput == null) throw new NullException(() => userInput);

//            // Set !Successful
//            userInput.Successful = false;

//            // GetEntity
//            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

//            // Business
//            PatchManager patchManager = new PatchManager(entity.Patch, _repositories);
//            VoidResult result = patchManager.SaveOperator(entity);

//            // ToViewModel
//            TViewModel viewModel = ToViewModel(entity);

//            // Non-Persisted
//            CopyNonPersistedProperties(userInput, viewModel);
//            viewModel.ValidationMessages.AddRange(result.Messages);

//            // Successful?
//            viewModel.Successful = result.Successful;

//            return viewModel;
//        }
//    }
//}