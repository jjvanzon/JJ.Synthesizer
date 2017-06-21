using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScalePropertiesPresenter : PropertiesPresenterBase<Scale, ScalePropertiesViewModel>
    {
        private readonly ScaleRepositories _repositories;
        private readonly ScaleManager _scaleManager;

        public ScalePropertiesPresenter(ScaleRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _scaleManager = new ScaleManager(_repositories);
        }

        protected override Scale GetEntity(ScalePropertiesViewModel userInput)
        {
            return _repositories.ScaleRepository.Get(userInput.Entity.ID);
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
            return TemplateAction(
                userInput,
                entity =>
                {
                    _scaleManager.DeleteWithRelatedEntities(entity);
                    return null;
                });
        }
    }
}