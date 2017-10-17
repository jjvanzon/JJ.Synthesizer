//using JJ.Framework.Exceptions;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Presentation.Synthesizer.ToViewModel;
//using JJ.Business.Synthesizer;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Business;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    internal class SamplePropertiesPresenter : PropertiesPresenterBase<Sample, SamplePropertiesViewModel>
//    {
//        private readonly RepositoryWrapper _repositories;
//        private readonly SampleRepositories _sampleRepositories;
//        private readonly SampleManager _sampleManager;
//        private readonly PatchManager _patchManager;

//        public SamplePropertiesPresenter(RepositoryWrapper repositories)
//        {
//            _repositories = repositories ?? throw new NullException(() => repositories);
//            _sampleRepositories = new SampleRepositories(repositories);
//            _sampleManager = new SampleManager(_sampleRepositories);
//            _patchManager = new PatchManager(repositories);
//        }

//        protected override Sample GetEntity(SamplePropertiesViewModel userInput)
//        {
//            return _repositories.SampleRepository.Get(userInput.Entity.ID);
//        }

//        protected override SamplePropertiesViewModel ToViewModel(Sample entity)
//        {
//            return entity.ToPropertiesViewModel(_sampleRepositories);
//        }

//        protected override IResult Save(Sample entity)
//        {
//            return _sampleManager.Save(entity);
//        }

//        public SamplePropertiesViewModel Play(SamplePropertiesViewModel userInput)
//        {
//            Outlet outlet = null;

//            return TemplateAction(
//                userInput,
//                sample =>
//                {
//                    Patch patch = _patchManager.CreatePatch();
//                    var operatorFactory = new OperatorFactory(patch, _repositories);
//                    outlet = operatorFactory.Sample(sample);
//                    return _patchManager.SavePatch(patch);
//                },
//                viewModel =>
//                {
//                    if (viewModel.Successful)
//                    {
//                        viewModel.OutletIDToPlay = outlet.ID;
//                    }
//                });
//        }

//        public SamplePropertiesViewModel Delete(SamplePropertiesViewModel userInput)
//        {
//            return TemplateAction(
//                userInput,
//                entity => _sampleManager.Delete(userInput.Entity.ID));
//        }
//    }
//}
