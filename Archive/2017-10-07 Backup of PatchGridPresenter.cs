﻿//using System.Collections.Generic;
//using JJ.Business.Synthesizer;
//using JJ.Business.Synthesizer.Dto;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Business;
//using JJ.Framework.Collections;
//using JJ.Framework.Exceptions;
//using JJ.Presentation.Synthesizer.ToViewModel;
//using JJ.Presentation.Synthesizer.ViewModels;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    internal class PatchGridPresenter : GridPresenterBase<PatchGridViewModel>
//    {
//        private readonly RepositoryWrapper _repositories;
//        private readonly DocumentManager _documentManager;
//        private readonly PatchManager _patchManager;
//        private readonly AutoPatcher _autoPatcher;

//        public PatchGridPresenter(RepositoryWrapper repositories)
//        {
//            _repositories = repositories ?? throw new NullException(() => repositories);
//            _documentManager = new DocumentManager(repositories);
//            _patchManager = new PatchManager(repositories);
//            _autoPatcher = new AutoPatcher(_repositories);
//        }

//        protected override PatchGridViewModel CreateViewModel(PatchGridViewModel userInput)
//        {
//            // GetEntity
//            Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

//            // Business
//            IList<Patch> patchesInGroup = PatchGrouper.GetPatchesInGroup_OrGrouplessIfGroupNameEmpty(document.Patches, userInput.Group, mustIncludeHidden: true);
//            IList<UsedInDto<Patch>> usedInDtos = _documentManager.GetUsedIn(patchesInGroup);

//            // ToViewModel
//            PatchGridViewModel viewModel = usedInDtos.ToGridViewModel(userInput.DocumentID, userInput.Group);

//            return viewModel;
//        }

//        public PatchGridViewModel Play(PatchGridViewModel userInput, int patchID)
//        {
//            return TemplateMethod(userInput, viewModel =>
//            {
//                // GetEntity
//                Patch patch = _repositories.PatchRepository.Get(patchID);

//                // Business
//                Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(patch);
//                Outlet outlet = result.Data;
//                if (outlet != null)
//                {
//                    _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
//                }

//                // Non-Persisted
//                viewModel.OutletIDToPlay = outlet?.ID;
//                viewModel.ValidationMessages.AddRange(result.Messages);
//                viewModel.Successful = result.Successful;
//            });
//        }

//        public PatchGridViewModel Delete(PatchGridViewModel userInput, int patchID)
//        {
//            return TemplateMethod(
//                userInput,
//                viewModel =>
//                {
//                    // GetEntity
//                    Patch patch = _repositories.PatchRepository.Get(patchID);

//                    // Businesss
//                    IResult result = _patchManager.DeletePatchWithRelatedEntities(patch);

//                    // Non-Persisted
//                    viewModel.ValidationMessages.AddRange(result.Messages);

//                    // Successful?
//                    viewModel.Successful = result.Successful;
//                });
//        }

//    }
//}
