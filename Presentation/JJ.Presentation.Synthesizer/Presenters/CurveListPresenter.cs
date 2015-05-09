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

        public CurveListPresenter(ICurveRepository curveRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            _curveRepository = curveRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
        }

        public CurveListViewModel Show(int pageNumber)
        {
            int pageIndex = pageNumber - 1;

            IList<Curve> curves = _curveRepository.GetPage(pageIndex * _pageSize, _pageSize);
            int totalCount = _curveRepository.Count();

            CurveListViewModel viewModel = curves.ToListViewModel(pageIndex, _pageSize, totalCount);

            return viewModel;
        }
    }
}
