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

        private static int _pageSize;
        private static int _maxVisiblePageNumbers;

        public SampleListPresenter(ISampleRepository sampleRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _sampleRepository = sampleRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public SampleListViewModel Show(int pageNumber)
        {
            int pageIndex = pageNumber - 1;
            IList<Sample> samplees = _sampleRepository.GetPage(pageIndex * _pageSize, _pageSize);

            int count = _sampleRepository.Count();

            var viewModel = new SampleListViewModel
            {
                List = samplees.Select(x => x.ToListItemViewModel()).ToArray(),
                Pager = PagerViewModelFactory.Create(pageIndex, _pageSize, count, _maxVisiblePageNumbers)
            };

            return viewModel;
        }
    }
}
