using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurrentPatchesPresenter
    {
        private IDocumentRepository _documentRepository;

        public CurrentPatchesViewModel ViewModel { get; set; }

        public CurrentPatchesPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Close()
        {
            AssertViewModel();

            ViewModel.Visible = false;
        }

        public void Add(int childDocumentID)
        {
            AssertViewModel();

            Document document = _documentRepository.Get(childDocumentID);
            CurrentPatchItemViewModel itemViewModel = document.ToCurrentPatchViewModel();
            ViewModel.List.Add(itemViewModel);
        }

        public void Remove(int childDocumentID)
        {
            AssertViewModel();

            ViewModel.List.RemoveFirst(x => x.ChildDocumentID == childDocumentID);
        }

        public void Move(int childDocumentID, int newPosition)
        {
            AssertViewModel();

            int currentPosition = ViewModel.List.IndexOf(x => x.ChildDocumentID == childDocumentID);
            CurrentPatchItemViewModel itemViewModel = ViewModel.List[currentPosition];
            ViewModel.List.RemoveAt(currentPosition);
            ViewModel.List.Insert(newPosition, itemViewModel);
        }

        /// <summary> No new view model is created. Just the child view models are replaced. </summary>
        public void Refresh()
        {
            AssertViewModel();

            for (int i = 0; i < ViewModel.List.Count; i++)
            {
                CurrentPatchItemViewModel itemViewModel = ViewModel.List[i];
                Document document = _documentRepository.Get(itemViewModel.ChildDocumentID);
                CurrentPatchItemViewModel itemViewModel2 = document.ToCurrentPatchViewModel();
                ViewModel.List[i] = itemViewModel2;
            }
        }

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
