using System.Collections.Generic;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SampleGridPresenter : GridPresenterBase<SampleGridViewModel>
    {
        private readonly PatchRepositories _repositories;
        private readonly DocumentManager _documentManager;
        private readonly SampleManager _sampleManager;

        public SampleGridPresenter(RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = new PatchRepositories(repositories);
            _documentManager = new DocumentManager(repositories);
            _sampleManager = new SampleManager(new SampleRepositories(repositories));
        }

        protected override SampleGridViewModel CreateViewModel(SampleGridViewModel userInput)
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

            // Business
            IList<UsedInDto<Sample>> dtos = _documentManager.GetUsedIn(document.Samples);

            // ToViewModel
            SampleGridViewModel viewModel = dtos.ToGridViewModel(document.ID);

            return viewModel;
        }

        public SampleGridViewModel Play(SampleGridViewModel userInput, int sampleID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Sample entity = _repositories.SampleRepository.Get(sampleID);

                    // Business
                    var x = new PatchManager(_repositories);
                    x.CreatePatch();
                    Outlet outlet = x.Sample(entity);
                    VoidResultDto result = x.SavePatch();

                    // Non-Persisted
                    viewModel.OutletIDToPlay = outlet?.ID;
                    viewModel.ValidationMessages = result.Messages;

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }

        public SampleGridViewModel Delete(SampleGridViewModel userInput, int sampleID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // Business
                    IResult result = _sampleManager.Delete(sampleID);

                    // Non-Persisted
                    viewModel.ValidationMessages = result.Messages.ToCanonical();

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }
    }
}