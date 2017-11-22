using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class ScalePropertiesPresenter : EntityPresenterWithSaveBase<Scale, ScalePropertiesViewModel>
	{
		private readonly IScaleRepository _scaleRepository;
		private readonly ScaleManager _scaleManager;

		public ScalePropertiesPresenter(IScaleRepository scaleRepository, ScaleManager scaleManager)
		{
			_scaleRepository = scaleRepository ?? throw new ArgumentNullException(nameof(scaleRepository));
			_scaleManager = scaleManager ?? throw new ArgumentNullException(nameof(scaleManager));
		}

		protected override Scale GetEntity(ScalePropertiesViewModel userInput)
		{
			return _scaleRepository.Get(userInput.Entity.ID);
		}

		protected override ScalePropertiesViewModel ToViewModel(Scale entity)
		{
			return entity.ToPropertiesViewModel();
		}

		protected override IResult Save(Scale entity)
		{
			return _scaleManager.SaveWithoutTones(entity);
		}

		public ScalePropertiesViewModel Delete(ScalePropertiesViewModel userInput)
		{
			return ExecuteAction(
				userInput,
				entity =>
				{
					_scaleManager.DeleteWithRelatedEntities(entity);
					return null;
				});
		}
	}
}