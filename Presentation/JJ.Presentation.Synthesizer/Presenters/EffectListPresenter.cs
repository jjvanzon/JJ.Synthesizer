using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class EffectListPresenter
    {
        private IDocumentRepository _documentRepository;
        private ChildDocumentListViewModel _viewModel;

        public EffectListPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public ChildDocumentListViewModel Show(int documentID)
        {
            IList<Document> documents = _documentRepository.GetEffects(documentID);

            _viewModel = documents.ToChildDocumentListViewModel();
            _viewModel.ParentDocumentID = documentID;
            _viewModel.Visible = true;

            _documentRepository.Rollback();

            return _viewModel;
        }

        public ChildDocumentListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyEffectListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
