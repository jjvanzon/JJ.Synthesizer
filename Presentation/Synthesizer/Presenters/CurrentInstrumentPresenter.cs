using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurrentInstrumentPresenter : PresenterBase<CurrentInstrumentViewModel>
    {
        private readonly IPatchRepository _patchRepository;

        public CurrentInstrumentPresenter(IPatchRepository patchRepository)
        {
            _patchRepository = patchRepository ?? throw new NullException(() => patchRepository);
        }

        public CurrentInstrumentViewModel Show(CurrentInstrumentViewModel userInput)
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
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentInstrumentViewModel Close(CurrentInstrumentViewModel userInput)
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
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = false;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentInstrumentViewModel Add(CurrentInstrumentViewModel userInput, int patchID)
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
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentInstrumentViewModel Remove(CurrentInstrumentViewModel userInput, int patchID)
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
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentInstrumentViewModel Move(CurrentInstrumentViewModel userInput, int patchID, int newPosition)
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
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        /// <summary> No new view model is created. Just the child view models are replaced. </summary>
        public CurrentInstrumentViewModel Refresh(CurrentInstrumentViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IEnumerable<int> ids = userInput.List.Select(x => x.ID);
            IList<Patch> entites = ids.Select(x => _patchRepository.Get(x)).ToList();

            // ToViewModel
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entites);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }
    }
}
