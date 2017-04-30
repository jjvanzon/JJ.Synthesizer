using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurrentInstrumentPresenter : PresenterBase<CurrentInstrumentViewModel>
    {
        private readonly PatchRepositories _repositories;

        public CurrentInstrumentPresenter(PatchRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
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
            IList<Patch> entities = ids.Select(x => _repositories.PatchRepository.Get(x)).ToList();

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
            IList<Patch> entities = ids.Select(x => _repositories.PatchRepository.Get(x)).ToList();

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
            IList<Patch> entities = ids.Select(x => _repositories.PatchRepository.Get(x)).ToList();

            // Business
            Patch entity = _repositories.PatchRepository.Get(patchID);
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
            IList<Patch> entities = ids.Select(x => _repositories.PatchRepository.Get(x)).ToList();

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
            IList<Patch> entities = ids.Select(x => _repositories.PatchRepository.Get(x)).ToList();

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
            IList<Patch> entites = ids.Select(x => _repositories.PatchRepository.Get(x)).ToList();

            // ToViewModel
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entites);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public CurrentInstrumentViewModel Play(CurrentInstrumentViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntities
            IList<Patch> entities = userInput.List.Select(x => _repositories.PatchRepository.Get(x.ID)).ToArray();

            // Business
            var patchManager = new PatchManager(_repositories);
            patchManager.AutoPatch(entities);
            Patch autoPatch = patchManager.Patch;
            Result<Outlet> result = patchManager.AutoPatch_TryCombineSignals(autoPatch);
            Outlet outlet = result.Data;

            // ToViewModel
            CurrentInstrumentViewModel viewModel = ViewModelHelper.CreateCurrentInstrumentViewModel(entities);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
            viewModel.OutletIDToPlay = outlet?.ID;

            // Successful
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}
