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
    public class CurveListPresenter
    {
        private ICurveRepository _curveRepository;

        private static int _pageSize;
        private static int _maxVisiblePageNumbers;

        public CurveListPresenter(ICurveRepository curveRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            _curveRepository = curveRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public CurveListViewModel Show(int pageNumber)
        {
            int pageIndex = pageNumber - 1;
            IList<Curve> curvees = _curveRepository.GetPage(pageIndex * _pageSize, _pageSize);

            int count = _curveRepository.Count();

            var viewModel = new CurveListViewModel
            {
                List = curvees.Select(x => x.ToIDName()).ToArray(),
                Pager = PagerViewModelFactory.Create(pageIndex, _pageSize, count, _maxVisiblePageNumbers)
            };

            return viewModel;
        }
    }
}
