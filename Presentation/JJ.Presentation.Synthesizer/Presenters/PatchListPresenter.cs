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
    public class PatchListPresenter
    {
        private IPatchRepository _patchRepository;

        private static int _pageSize;
        private static int _maxVisiblePageNumbers;

        public PatchListPresenter(IPatchRepository patchRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public PatchListViewModel Show(int pageNumber)
        {
            int pageIndex = pageNumber - 1;
            IList<Patch> patches = _patchRepository.GetPage(pageIndex * _pageSize, _pageSize);

            int count = _patchRepository.Count();

            var viewModel = new PatchListViewModel
            {
                List = patches.Select(x => x.ToListItemViewModel()).ToArray(),
                Pager = PagerViewModelFactory.Create(pageIndex, _pageSize, count, _maxVisiblePageNumbers)
            };

            return viewModel;
        }
    }
}
