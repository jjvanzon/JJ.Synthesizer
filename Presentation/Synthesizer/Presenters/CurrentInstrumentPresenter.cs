using JJ.Business.Synthesizer;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
#pragma warning disable CS0809 // Obsolete member overrides non-obsolete member

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurrentInstrumentPresenter 
        : EntityPresenterWithoutSaveBase<(Document document, IList<Patch> patches), CurrentInstrumentViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly AutoPatcher _autoPatcher;

        public CurrentInstrumentPresenter(AutoPatcher autoPatcher, IDocumentRepository documentRepository, IPatchRepository patchRepository)
        {
            _autoPatcher = autoPatcher ?? throw new ArgumentNullException(nameof(autoPatcher));
            _documentRepository = documentRepository ?? throw new ArgumentNullException(nameof(documentRepository));
            _patchRepository = patchRepository ?? throw new ArgumentNullException(nameof(patchRepository));
        }

        protected override (Document document, IList<Patch> patches) GetEntity(CurrentInstrumentViewModel userInput)
        {
            Document document = _documentRepository.Get(userInput.DocumentID);

            IList<Patch> patches = userInput.List
                                            .Select(x => x.ID)
                                            .Select(x => _patchRepository.Get(x))
                                            .ToList();

            return (document, patches);
        }

        protected override CurrentInstrumentViewModel ToViewModel((Document document, IList<Patch> patches) x)
        {
            return x.document.ToCurrentInstrumentViewModel(x.patches);
        }

        public CurrentInstrumentViewModel Add(CurrentInstrumentViewModel userInput, int patchID)
        {
            Patch patch = null;

            return ExecuteAction(
                userInput,
                entities => patch = _patchRepository.Get(patchID),
                viewModel => viewModel.List.Add(patch.ToIDAndName()));
        }

        public void Remove(CurrentInstrumentViewModel viewModel, int patchID)
        {
            ExecuteNonPersistedAction(viewModel, () => viewModel.List.RemoveFirst(x => x.ID == patchID));
        }

        public void Move(CurrentInstrumentViewModel viewModel, int patchID, int newPosition)
        {
            ExecuteNonPersistedAction(viewModel, () =>
            {
                int currentPosition = viewModel.List.IndexOf(x => x.ID == patchID);
                IDAndName item = viewModel.List[currentPosition];
                viewModel.List.RemoveAt(currentPosition);
                viewModel.List.Insert(newPosition, item);
            });
        }

        public CurrentInstrumentViewModel Play(CurrentInstrumentViewModel userInput)
        {
            Outlet outlet = null;

            return ExecuteAction(
                userInput,
                entities =>
                {
                    Patch autoPatch = _autoPatcher.AutoPatch(entities.patches);
                    _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(autoPatch);
                    Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(autoPatch);
                    outlet = result.Data;
                    return result;
                },
                viewModel =>
                {
                    viewModel.OutletIDToPlay = outlet?.ID;
                });

        }

        [Obsolete("Use Load instead.", true)]
        public override void Show(CurrentInstrumentViewModel viewModel)
        {
            throw new NotSupportedException("Call Load instead.");
        }
    }
}
