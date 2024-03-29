﻿//using JJ.Data.Synthesizer;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Presentation.Synthesizer.ToViewModel;
//using JJ.Presentation.Synthesizer.ViewModels;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    internal class PatchGridPresenter
//    {
//        private IDocumentRepository _documentRepository;

//        public PatchGridViewModel ViewModel { get; set; }

//        public PatchGridPresenter(IDocumentRepository documentRepository)
//        {
//            if (documentRepository == null) throw new NullException(() => documentRepository);

//            _documentRepository = documentRepository;
//        }

//        public void Show()
//        {
//            AssertViewModel();

//            ViewModel.Visible = true;
//        }

//        /// <summary> Can return PatchGridViewModel or NotFoundViewModel. </summary>
//        public object Refresh()
//        {
//            AssertViewModel();

//            Document document = _documentRepository.TryGet(ViewModel.DocumentID);
//            if (document == null)
//            {
//                ViewModelHelper.CreateDocumentNotFoundViewModel();
//            }

//            bool visible = ViewModel.Visible;

//            ViewModel = document.Patches.ToGridViewModel(ViewModel.DocumentID);

//            ViewModel.Visible = visible;

//            return ViewModel;
//        }

//        public void Close()
//        {
//            AssertViewModel();

//            ViewModel.Visible = false;
//        }

//        // Helpers

//        private void AssertViewModel()
//        {
//            if (ViewModel == null) throw new NullException(() => ViewModel);
//        }
//    }
//}
