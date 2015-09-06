using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    /// <summary>
    /// Panels in the application are so intricately coordinated
    /// that one action in one part of the screen can
    /// affect several other panels on the screen.
    /// So you cannot manage each part of the screen individually.
    /// 
    /// That is why all panels are managed in a single presenter and view model.
    /// Otherwise you would get difficult coordination of application navigation 
    /// in the platform-specific application code, where it does not belong.
    /// 
    /// Also: most non-visible parts of the view model must be kept alive inside the view model,
    /// because the whole document will not be persistent until you hit save,
    /// and until that time, all the data must be kept inside the view model.
    /// </summary>
    public partial class MainPresenter
    {
        private readonly RepositoryWrapper _repositories;
        private readonly PatchRepositories _patchRepositories;

        private readonly AudioFileOutputGridPresenter _audioFileOutputGridPresenter;
        private readonly AudioFileOutputPropertiesPresenter _audioFileOutputPropertiesPresenter;
        private readonly ChildDocumentGridPresenter _effectGridPresenter;
        private readonly ChildDocumentGridPresenter _instrumentGridPresenter;
        private readonly ChildDocumentPropertiesPresenter _childDocumentPropertiesPresenter;
        private readonly CurveDetailsPresenter _curveDetailsPresenter;
        private readonly CurveGridPresenter _curveGridPresenter;
        private readonly DocumentCannotDeletePresenter _documentCannotDeletePresenter;
        private readonly DocumentDeletedPresenter _documentDeletedPresenter;
        private readonly DocumentDeletePresenter _documentDeletePresenter;
        private readonly DocumentDetailsPresenter _documentDetailsPresenter;
        private readonly DocumentGridPresenter _documentGridPresenter;
        private readonly DocumentPropertiesPresenter _documentPropertiesPresenter;
        private readonly DocumentTreePresenter _documentTreePresenter;
        private readonly MenuPresenter _menuPresenter;
        private readonly NotFoundPresenter _notFoundPresenter;
        private readonly OperatorPropertiesPresenter _operatorPropertiesPresenter;
        private readonly OperatorPropertiesPresenter_ForCustomOperator _operatorPropertiesPresenter_ForCustomOperator;
        private readonly OperatorPropertiesPresenter_ForPatchInlet _operatorPropertiesPresenter_ForPatchInlet;
        private readonly OperatorPropertiesPresenter_ForPatchOutlet _operatorPropertiesPresenter_ForPatchOutlet;
        private readonly OperatorPropertiesPresenter_ForSample _operatorPropertiesPresenter_ForSample;
        private readonly OperatorPropertiesPresenter_ForValue _operatorPropertiesPresenter_ForValue;
        private readonly PatchDetailsPresenter _patchDetailsPresenter;
        private readonly PatchGridPresenter _patchGridPresenter;
        private readonly SampleGridPresenter _sampleGridPresenter;
        private readonly SamplePropertiesPresenter _samplePropertiesPresenter;

        private readonly AudioFileOutputManager _audioFileOutputManager;
        private readonly CurveManager _curveManager;
        private readonly DocumentManager _documentManager;
        private readonly EntityPositionManager _entityPositionManager;
        private readonly SampleManager _sampleManager;

        public MainViewModel ViewModel { get; private set; }

        public MainPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            _repositories = repositoryWrapper;
            _patchRepositories = new PatchRepositories(_repositories);

            _audioFileOutputManager = new AudioFileOutputManager(
                _repositories.AudioFileOutputRepository,
                _repositories.AudioFileOutputChannelRepository,
                _repositories.SampleDataTypeRepository,
                _repositories.SpeakerSetupRepository,
                _repositories.AudioFileFormatRepository,
                _repositories.CurveRepository,
                _repositories.SampleRepository,
                _repositories.DocumentRepository,
                _repositories.IDRepository);
            _curveManager = new CurveManager(_repositories.CurveRepository, _repositories.NodeRepository, _repositories.IDRepository);
            _documentManager = new DocumentManager(_repositories);
            _entityPositionManager = new EntityPositionManager(_repositories.EntityPositionRepository, _repositories.IDRepository);
            _sampleManager = new SampleManager(new SampleRepositories(_repositories));

            _audioFileOutputGridPresenter = new AudioFileOutputGridPresenter(_repositories.DocumentRepository);
            _audioFileOutputPropertiesPresenter = new AudioFileOutputPropertiesPresenter(new AudioFileOutputRepositories(_repositories));
            _childDocumentPropertiesPresenter = new ChildDocumentPropertiesPresenter(_repositories);
            _curveDetailsPresenter = new CurveDetailsPresenter(
                _repositories.CurveRepository,
                _repositories.NodeRepository,
                _repositories.NodeTypeRepository);
            _curveGridPresenter = new CurveGridPresenter(_repositories.DocumentRepository);
            _documentCannotDeletePresenter = new DocumentCannotDeletePresenter(_repositories.DocumentRepository);
            _documentDeletedPresenter = new DocumentDeletedPresenter();
            _documentDeletePresenter = new DocumentDeletePresenter(_repositories);
            _documentDetailsPresenter = new DocumentDetailsPresenter(_repositories.DocumentRepository, _repositories.IDRepository);
            _documentGridPresenter = new DocumentGridPresenter(_repositories.DocumentRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositories.DocumentRepository);
            _documentTreePresenter = new DocumentTreePresenter(_repositories.DocumentRepository);
            _effectGridPresenter = new ChildDocumentGridPresenter(_repositories.DocumentRepository);
            _instrumentGridPresenter = new ChildDocumentGridPresenter(_repositories.DocumentRepository);
            _menuPresenter = new MenuPresenter();
            _notFoundPresenter = new NotFoundPresenter();
            _operatorPropertiesPresenter = new OperatorPropertiesPresenter(_patchRepositories);
            _operatorPropertiesPresenter_ForCustomOperator = new OperatorPropertiesPresenter_ForCustomOperator(_patchRepositories);
            _operatorPropertiesPresenter_ForPatchInlet = new OperatorPropertiesPresenter_ForPatchInlet(_patchRepositories);
            _operatorPropertiesPresenter_ForPatchOutlet = new OperatorPropertiesPresenter_ForPatchOutlet(_patchRepositories);
            _operatorPropertiesPresenter_ForSample = new OperatorPropertiesPresenter_ForSample(_patchRepositories);
            _operatorPropertiesPresenter_ForValue = new OperatorPropertiesPresenter_ForValue(_patchRepositories);
            _patchDetailsPresenter = _patchDetailsPresenter = new PatchDetailsPresenter(_patchRepositories, _entityPositionManager);
            _patchGridPresenter = new PatchGridPresenter(_repositories.DocumentRepository);
            _sampleGridPresenter = new SampleGridPresenter(_repositories.DocumentRepository, _repositories.SampleRepository);
            _samplePropertiesPresenter = new SamplePropertiesPresenter(new SampleRepositories(_repositories));

            _dispatchDelegateDictionary = CreateDispatchDelegateDictionary();
        }

        // Helpers

        private void HideAllListAndDetailViewModels()
        {
            ViewModel.DocumentGrid.Visible = false;
            ViewModel.DocumentDetails.Visible = false;

            ViewModel.Document.InstrumentGrid.Visible = false;
            ViewModel.Document.EffectGrid.Visible = false;
            ViewModel.Document.SampleGrid.Visible = false;
            ViewModel.Document.CurveGrid.Visible = false;
            ViewModel.Document.AudioFileOutputGrid.Visible = false;
            ViewModel.Document.PatchGrid.Visible = false;

            foreach (CurveDetailsViewModel curveDetailsViewModel in ViewModel.Document.CurveDetailsList)
            {
                curveDetailsViewModel.Visible = false;
            }

            foreach (PatchDetailsViewModel patchDetailsViewModel in ViewModel.Document.PatchDetailsList)
            {
                patchDetailsViewModel.Visible = false;
            }

            foreach (ChildDocumentViewModel childDocumentViewModel in ViewModel.Document.ChildDocumentList)
            {
                childDocumentViewModel.SampleGrid.Visible = false;
                childDocumentViewModel.CurveGrid.Visible = false;
                childDocumentViewModel.PatchGrid.Visible = false;

                foreach (PatchDetailsViewModel patchDetailsViewModel in childDocumentViewModel.PatchDetailsList)
                {
                    patchDetailsViewModel.Visible = false;
                }
            }
        }

        private void HideAllPropertiesViewModels()
        {
            ViewModel.DocumentDetails.Visible = false;
            ViewModel.Document.AudioFileOutputPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.CurveDetailsList.ForEach(x => x.Visible = false);
            ViewModel.Document.DocumentProperties.Visible = false;
            ViewModel.Document.OperatorPropertiesList.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForCustomOperators.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForPatchInlets.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForPatchOutlets.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForSamples.ForEach(x => x.Visible = false);
            ViewModel.Document.OperatorPropertiesList_ForValues.ForEach(x => x.Visible = false);
            ViewModel.Document.SamplePropertiesList.ForEach(x => x.Visible = false);
            
            // Note that the not all entity types have Properties view inside the child documents.
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.SamplePropertiesList).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchInlets).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchOutlets).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForSamples).ForEach(x => x.Visible = false);
            ViewModel.Document.ChildDocumentList.SelectMany(x => x.OperatorPropertiesList_ForValues).ForEach(x => x.Visible = false);
        }
    }
}
