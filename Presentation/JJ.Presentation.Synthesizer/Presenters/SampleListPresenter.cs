using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Configuration;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class SampleListPresenter
    {
        private ISampleRepository _sampleRepository;

        private SampleListViewModel _viewModel;

        private static int _pageSize;

        public SampleListPresenter(ISampleRepository sampleRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
        }

        public SampleListViewModel Show(int documentID)
        {
            IList<Sample> samples = _sampleRepository.GetManyByDocumentID(documentID);

            _viewModel = samples.ToListViewModel();
            _viewModel.DocumentID = documentID;
            _viewModel.Visible = true;

            return _viewModel;
        }

        public SampleListViewModel Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptySampleListViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
