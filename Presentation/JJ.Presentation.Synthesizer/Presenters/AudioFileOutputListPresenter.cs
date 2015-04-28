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
    public class AudioFileOutputListPresenter
    {
        private IAudioFileOutputRepository _audioFileOutputRepository;

        private static int _pageSize;
        private static int _maxVisiblePageNumbers;

        public AudioFileOutputListPresenter(IAudioFileOutputRepository audioFileOutputRepository)
        {
            if (audioFileOutputRepository == null) throw new NullException(() => audioFileOutputRepository);

            _audioFileOutputRepository = audioFileOutputRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public AudioFileOutputListViewModel Show(int pageNumber)
        {
            int pageIndex = pageNumber - 1;
            IList<AudioFileOutput> audioFileOutputes = _audioFileOutputRepository.GetPage(pageIndex * _pageSize, _pageSize);

            int count = _audioFileOutputRepository.Count();

            var viewModel = new AudioFileOutputListViewModel
            {
                List = audioFileOutputes.Select(x => x.ToListItemViewModel()).ToArray(),
                Pager = PagerViewModelFactory.Create(pageIndex, _pageSize, count, _maxVisiblePageNumbers)
            };

            return viewModel;
        }
    }
}
