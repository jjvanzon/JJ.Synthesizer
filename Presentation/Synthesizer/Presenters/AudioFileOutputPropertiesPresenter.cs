using System;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class AudioFileOutputPropertiesPresenter 
		: EntityPresenterWithSaveBase<AudioFileOutput, AudioFileOutputPropertiesViewModel>
	{
		private readonly AudioFileOutputFacade _audioFileOutputFacade;
		private readonly IAudioFileOutputRepository _audioFileOutputRepository;

		public AudioFileOutputPropertiesPresenter(AudioFileOutputFacade audioFileOutputFacade, IAudioFileOutputRepository audioFileOutputRepository)
		{
			_audioFileOutputRepository = audioFileOutputRepository ?? throw new ArgumentNullException(nameof(audioFileOutputRepository));
			_audioFileOutputFacade = audioFileOutputFacade ?? throw new ArgumentNullException(nameof(audioFileOutputFacade));
		}

		protected override AudioFileOutput GetEntity(AudioFileOutputPropertiesViewModel userInput)
		{
			return _audioFileOutputRepository.Get(userInput.Entity.ID);
		}

		protected override IResult Save(AudioFileOutput entity)
		{
			return _audioFileOutputFacade.Save(entity);
		}

		protected override AudioFileOutputPropertiesViewModel ToViewModel(AudioFileOutput entity)
		{
			return entity.ToPropertiesViewModel();
		}

		public AudioFileOutputPropertiesViewModel Delete(AudioFileOutputPropertiesViewModel userInput)
		{
			return ExecuteAction(
				userInput,
				entity =>
				{
					_audioFileOutputFacade.Delete(entity.ID);
					return null;
				});
		}
	}
}