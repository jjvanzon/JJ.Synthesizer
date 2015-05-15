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
        private EffectListViewModel _viewModel;

        public EffectListPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public EffectListViewModel Show(int documentID)
        {
            IList<Document> documents = _documentRepository.GetEffects(documentID);

            _viewModel = documents.ToEffectListViewModel();
            _viewModel.ParentDocumentID = documentID;

            _documentRepository.Rollback();

            return _viewModel;
        }

        public EffectListViewModel Close()
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
