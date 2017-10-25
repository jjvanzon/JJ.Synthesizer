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

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurrentInstrumentPresenter 
        : PresenterBaseWithoutSave<(Document document, IList<Patch> patches), CurrentInstrumentViewModel>
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
            return ToViewModelHelper.CreateCurrentInstrumentViewModel(x.patches, x.document);
        }

        public CurrentInstrumentViewModel Load(CurrentInstrumentViewModel userInput) => ExecuteAction(userInput, x => x.Visible = true);

        public CurrentInstrumentViewModel Add(CurrentInstrumentViewModel userInput, int patchID)
        {
            return ExecuteAction(
                userInput,
                viewModel =>
                {
                    Patch entity = _patchRepository.Get(patchID);
                    viewModel.List.Add(entity.ToIDAndName());
                });
        }

        public CurrentInstrumentViewModel Remove(CurrentInstrumentViewModel userInput, int patchID)
        {
            return ExecuteAction(userInput, viewModel => viewModel.List.RemoveFirst(x => x.ID == patchID));
        }

        public CurrentInstrumentViewModel Move(CurrentInstrumentViewModel userInput, int patchID, int newPosition)
        {
            return ExecuteAction(userInput, viewModel =>
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
    }
}
