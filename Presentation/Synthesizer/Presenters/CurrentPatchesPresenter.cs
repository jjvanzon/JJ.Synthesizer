using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurrentPatchesPresenter : PresenterBase<CurrentPatchesViewModel>
    {
        private readonly IPatchRepository _patchRepository;

        public CurrentPatchesPresenter(IPatchRepository patchRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;
        }

        public CurrentPatchesViewModel Show(CurrentPatchesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> ids = userInput.List.Select(x => x.ID);
            IList<Patch> entities = ids.Select(x => _patchRepository.Get(x)).ToList();

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Close(CurrentPatchesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> ids = userInput.List.Select(x => x.ID);
            IList<Patch> entities = ids.Select(x => _patchRepository.Get(x)).ToList();

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Add(CurrentPatchesViewModel userInput, int patchID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> ids = userInput.List.Select(x => x.ID);
            IList<Patch> entities = ids.Select(x => _patchRepository.Get(x)).ToList();

            // Business
            Patch entity = _patchRepository.Get(patchID);
            entities.Add(entity);

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Remove(CurrentPatchesViewModel userInput, int patchID)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> ids = userInput.List.Select(x => x.ID);
            IList<Patch> entities = ids.Select(x => _patchRepository.Get(x)).ToList();

            // Business
            entities.RemoveFirst(x => x.ID == patchID);

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentPatchesViewModel Move(CurrentPatchesViewModel userInput, int patchID, int newPosition)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> ids = userInput.List.Select(x => x.ID);
            IList<Patch> entities = ids.Select(x => _patchRepository.Get(x)).ToList();

            // Business
            int currentPosition = entities.IndexOf(x => x.ID == patchID);
            Patch entity = entities[currentPosition];
            entities.RemoveAt(currentPosition);
            entities.Insert(newPosition, entity);

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        /// <summary> No new view model is created. Just the child view models are replaced. </summary>
        public CurrentPatchesViewModel Refresh(CurrentPatchesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> ids = userInput.List.Select(x => x.ID);
            IList<Patch> entites = ids.Select(x => _patchRepository.Get(x)).ToList();

            // ToViewModel
            CurrentPatchesViewModel viewModel = ViewModelHelper.CreateCurrentPatchesViewModel(entites);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}
