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
		private readonly ScaleFacade _scaleFacade;

		public ScalePropertiesPresenter(IScaleRepository scaleRepository, ScaleFacade scaleFacade)
		{
			_scaleRepository = scaleRepository ?? throw new ArgumentNullException(nameof(scaleRepository));
			_scaleFacade = scaleFacade ?? throw new ArgumentNullException(nameof(scaleFacade));
		}

		protected override Scale GetEntity(ScalePropertiesViewModel userInput) => _scaleRepository.Get(userInput.Entity.ID);

	    protected override ScalePropertiesViewModel ToViewModel(Scale entity) => entity.ToPropertiesViewModel();

	    protected override IResult Save(Scale entity, ScalePropertiesViewModel userInput) => _scaleFacade.SaveWithoutTones(entity);

	    public ScalePropertiesViewModel Delete(ScalePropertiesViewModel userInput) => ExecuteAction(
	        userInput,
	        entity =>
	        {
	            _scaleFacade.DeleteWithRelatedEntities(entity);
	            return null;
	        });
	}
}