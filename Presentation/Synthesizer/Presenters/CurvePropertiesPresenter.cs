using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurvePropertiesPresenter
        : PropertiesPresenterBase<Curve, CurvePropertiesViewModel>
    {
        private readonly ICurveRepository _curveRepository;
        private readonly CurveManager _curveManager;

        public CurvePropertiesPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);
            _curveRepository = repositories.CurveRepository;
            _curveManager = new CurveManager(repositories);
        }

        protected override Curve GetEntity(CurvePropertiesViewModel userInput)
        {
            return _curveRepository.Get(userInput.ID);
        }

        protected override IResult Save(Curve entity)
        {
            return _curveManager.SaveCurveWithRelatedEntities(entity);
        }

        protected override CurvePropertiesViewModel ToViewModel(Curve entity)
        {
            return entity.ToPropertiesViewModel();
        }

        public CurvePropertiesViewModel Delete(CurvePropertiesViewModel userInput)
        {
            return TemplateAction(userInput, x => _curveManager.DeleteWithRelatedEntities(x.ID));
        }
    }
}