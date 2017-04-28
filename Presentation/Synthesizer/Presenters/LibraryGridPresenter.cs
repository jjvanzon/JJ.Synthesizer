using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Exceptions;
using JJ.Framework.Mathematics;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class LibraryGridPresenter : GridPresenterBase<LibraryGridViewModel>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentReferenceRepository _documentReferenceRepository;
        private readonly DocumentManager _documentManager;

        public LibraryGridPresenter([NotNull] RepositoryWrapper repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _documentRepository = repositories.DocumentRepository;
            _documentReferenceRepository = repositories.DocumentReferenceRepository;

            _documentManager = new DocumentManager(repositories);
        }

        protected override LibraryGridViewModel CreateViewModel(LibraryGridViewModel userInput)
        {
            // GetEntity
            Document document = _documentRepository.Get(userInput.HigherDocumentID);

            // ToViewModel
            LibraryGridViewModel viewModel = document.ToLibraryGridViewModel();

            return viewModel;
        }

        public LibraryGridViewModel Remove(LibraryGridViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // Business
                    VoidResult result = _documentManager.DeleteDocumentReference(documentReferenceID);

                    // Non-Persisted
                    viewModel.Successful = result.Successful;
                    viewModel.ValidationMessages = result.Messages;
                });
        }

        public LibraryGridViewModel Play(LibraryGridViewModel userInput, int documentReferenceID)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    DocumentReference documentReference = _documentReferenceRepository.Get(documentReferenceID);

                    // Business
                    IList<Outlet> outlets = documentReference.LowerDocument
                                                             .Patches
                                                             .OrderBy(x => x.Name)
                                                             .Where(x => !x.Hidden)
                                                             .Where(
                                                                 x => !x.EnumerateOperatorWrappersOfType<PatchInlet_OperatorWrapper>()
                                                                        .Where(y => y.DimensionEnum == DimensionEnum.Signal)
                                                                        .Any())
                                                             .SelectMany(x => x.EnumerateOperatorWrappersOfType<PatchOutlet_OperatorWrapper>())
                                                             .Where(x => x.DimensionEnum == DimensionEnum.Signal)
                                                             .Select(x => x.Result)
                                                             .ToArray();

                    Outlet outlet = Randomizer.TryGetRandomItem(outlets);

                    // TODO: Select the first patch with a signal inlet and use autopatch those two together.

                    if (outlet == null)
                    {
                        // Non-Persisted
                        viewModel.Successful = false;
                        viewModel.ValidationMessages.Add(new Message
                        {
                            Key = nameof(DocumentReference),
                            Text = ResourceFormatter.NoSoundFoundInLibrary
                        });
                    }
                    else
                    {
                        // Non-Persisted
                        viewModel.OutletIDToPlay = outlet.ID;
                    }
                });
        }
    }
}