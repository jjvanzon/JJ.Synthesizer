using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Configuration;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class AudioFileOutputListPresenter
    {
        private IDocumentRepository _documentRepository;
        private AudioFileOutputListViewModel _viewModel;

        public AudioFileOutputListPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Can return AudioFileOutputListViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int documentID)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.DocumentID != documentID;

            if (mustCreateViewModel)
            {
                Document document = _documentRepository.TryGet(documentID);
                if (document == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                _viewModel = document.AudioFileOutputs.ToListViewModel();
                _viewModel.DocumentID = documentID;
            }

            _viewModel.Visible = true;

            return _viewModel;
        }

        /// <summary>
        /// Can return AudioFileOutputListViewModel or NotFoundViewModel.
        /// </summary>
        public object Refresh(int documentID)
        {
            Document document = _documentRepository.TryGet(documentID);
            if (document == null)
            {
                return CreateDocumentNotFoundViewModel();
            }

            _viewModel = document.AudioFileOutputs.ToListViewModel();
            _viewModel.DocumentID = document.ID;

            _viewModel.Visible = true;

            return _viewModel;
        }

        public AudioFileOutputListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyAudioFileOutputListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }

        // Helpers

        private NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            NotFoundViewModel viewModel = new NotFoundPresenter().Show(PropertyDisplayNames.Document);
            return viewModel;
        }
    }
}
