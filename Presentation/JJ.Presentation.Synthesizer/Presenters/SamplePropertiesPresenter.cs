using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class SamplePropertiesPresenter
    {
        private ISampleRepository _sampleRepository;
        private IAudioFileFormatRepository _audioFileFormatRepository;
        private ISampleDataTypeRepository _sampleDataTypeRepository;
        private ISpeakerSetupRepository _speakerSetupRepository;
        private IInterpolationTypeRepository _interpolationTypeRepository;

        private SamplePropertiesViewModel _viewModel;

        public SamplePropertiesPresenter(
            ISampleRepository sampleRepository,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository)
        {
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (audioFileFormatRepository == null) throw new NullException(() => audioFileFormatRepository);
            if (sampleDataTypeRepository == null) throw new NullException(() => sampleDataTypeRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);

            _sampleRepository = sampleRepository;
            _audioFileFormatRepository = audioFileFormatRepository;
            _sampleDataTypeRepository = sampleDataTypeRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _interpolationTypeRepository = interpolationTypeRepository;
        }

        /// <summary>
        /// Can return SamplePropertiesViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int id)
        {
            Sample entity = _sampleRepository.TryGet(id);
            if (entity == null)
            {
                var presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel2 = presenter2.Show(PropertyDisplayNames.Sample);
                return viewModel2;
            }
            else
            {
                _viewModel = entity.ToPropertiesViewModel(_audioFileFormatRepository, _sampleDataTypeRepository, _speakerSetupRepository, _interpolationTypeRepository);
                _viewModel.Visible = true;
                return _viewModel;
            }
        }
    }
}
