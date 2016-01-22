using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Data.Synthesizer;

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
        private const double DEFAULT_DURATION = 0.75;

        private static string _playOutputFilePath = GetPlayOutputFilePath();
        private static PatchCalculatorTypeEnum _patchCalculatorTypeEnum = GetPatchCalculatorTypeEnum();

        private readonly RepositoryWrapper _repositories;
        private readonly PatchRepositories _patchRepositories;
        private readonly SampleRepositories _sampleRepositories;
        private readonly CurveRepositories _curveRepositories;

        private readonly AudioFileOutputGridPresenter _audioFileOutputGridPresenter;
        private readonly AudioFileOutputPropertiesPresenter _audioFileOutputPropertiesPresenter;
        private readonly CurrentPatchesPresenter _currentPatchesPresenter;
        private readonly CurveDetailsPresenter _curveDetailsPresenter;
        private readonly CurveGridPresenter _curveGridPresenter;
        private readonly CurvePropertiesPresenter _curvePropertiesPresenter;
        private readonly DocumentCannotDeletePresenter _documentCannotDeletePresenter;
        private readonly DocumentDeletedPresenter _documentDeletedPresenter;
        private readonly DocumentDeletePresenter _documentDeletePresenter;
        private readonly DocumentDetailsPresenter _documentDetailsPresenter;
        private readonly DocumentGridPresenter _documentGridPresenter;
        private readonly DocumentPropertiesPresenter _documentPropertiesPresenter;
        private readonly DocumentTreePresenter _documentTreePresenter;
        private readonly MenuPresenter _menuPresenter;
        private readonly NotFoundPresenter _notFoundPresenter;
        private readonly NodePropertiesPresenter _nodePropertiesPresenter;
        private readonly OperatorPropertiesPresenter _operatorPropertiesPresenter;
        private readonly OperatorPropertiesPresenter_ForBundle _operatorPropertiesPresenter_ForBundle;
        private readonly OperatorPropertiesPresenter_ForCurve _operatorPropertiesPresenter_ForCurve;
        private readonly OperatorPropertiesPresenter_ForCustomOperator _operatorPropertiesPresenter_ForCustomOperator;
        private readonly OperatorPropertiesPresenter_ForNumber _operatorPropertiesPresenter_ForNumber;
        private readonly OperatorPropertiesPresenter_ForPatchInlet _operatorPropertiesPresenter_ForPatchInlet;
        private readonly OperatorPropertiesPresenter_ForPatchOutlet _operatorPropertiesPresenter_ForPatchOutlet;
        private readonly OperatorPropertiesPresenter_ForSample _operatorPropertiesPresenter_ForSample;
        private readonly OperatorPropertiesPresenter_ForUnbundle _operatorPropertiesPresenter_ForUnbundle;
        private readonly PatchDetailsPresenter _patchDetailsPresenter;
        private readonly PatchGridPresenter _patchGridPresenter;
        private readonly PatchPropertiesPresenter _patchPropertiesPresenter;
        private readonly SampleGridPresenter _sampleGridPresenter;
        private readonly SamplePropertiesPresenter _samplePropertiesPresenter;
        private readonly ToneGridEditPresenter _toneGridEditPresenter;
        private readonly ScaleGridPresenter _scaleGridPresenter;
        private readonly ScalePropertiesPresenter _scalePropertiesPresenter;

        private readonly AudioFileOutputManager _audioFileOutputManager;
        private readonly CurveManager _curveManager;
        private readonly DocumentManager _documentManager;
        private readonly EntityPositionManager _entityPositionManager;
        private readonly SampleManager _sampleManager;
        private readonly ScaleManager _scaleManager;

        public MainViewModel MainViewModel { get; private set; }

        public MainPresenter(RepositoryWrapper repositoryWrapper)
        {
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            // Create Repositories
            _repositories = repositoryWrapper;
            _curveRepositories = new CurveRepositories(_repositories);
            _patchRepositories = new PatchRepositories(_repositories);
            _sampleRepositories = new SampleRepositories(_repositories);
            var scaleRepositories = new ScaleRepositories(_repositories);
            var audioFileOutputRepositories = new AudioFileOutputRepositories(_repositories);

            // Create Managers
            _audioFileOutputManager = new AudioFileOutputManager(audioFileOutputRepositories);
            _curveManager = new CurveManager(_curveRepositories);
            _documentManager = new DocumentManager(_repositories);
            _entityPositionManager = new EntityPositionManager(_repositories.EntityPositionRepository, _repositories.IDRepository);
            _sampleManager = new SampleManager(_sampleRepositories);
            _scaleManager = new ScaleManager(scaleRepositories);

            // Create Presenters
            _audioFileOutputGridPresenter = new AudioFileOutputGridPresenter(_repositories.DocumentRepository);
            _audioFileOutputPropertiesPresenter = new AudioFileOutputPropertiesPresenter(audioFileOutputRepositories);
            _currentPatchesPresenter = new CurrentPatchesPresenter(_repositories.DocumentRepository);
            _curveDetailsPresenter = new CurveDetailsPresenter(_curveRepositories);
            _curveGridPresenter = new CurveGridPresenter(_repositories.DocumentRepository, _repositories.CurveRepository);
            _curvePropertiesPresenter = new CurvePropertiesPresenter(_curveRepositories);
            _documentCannotDeletePresenter = new DocumentCannotDeletePresenter(_repositories.DocumentRepository);
            _documentDeletedPresenter = new DocumentDeletedPresenter();
            _documentDeletePresenter = new DocumentDeletePresenter(_repositories);
            _documentDetailsPresenter = new DocumentDetailsPresenter(_repositories);
            _documentGridPresenter = new DocumentGridPresenter(_repositories.DocumentRepository);
            _documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositories);
            _documentTreePresenter = new DocumentTreePresenter(_repositories.DocumentRepository);
            _menuPresenter = new MenuPresenter();
            _nodePropertiesPresenter = new NodePropertiesPresenter(_curveRepositories);
            _notFoundPresenter = new NotFoundPresenter();
            _operatorPropertiesPresenter = new OperatorPropertiesPresenter(_patchRepositories);
            _operatorPropertiesPresenter_ForBundle = new OperatorPropertiesPresenter_ForBundle(_patchRepositories);
            _operatorPropertiesPresenter_ForCurve = new OperatorPropertiesPresenter_ForCurve(_patchRepositories);
            _operatorPropertiesPresenter_ForCustomOperator = new OperatorPropertiesPresenter_ForCustomOperator(_patchRepositories);
            _operatorPropertiesPresenter_ForNumber = new OperatorPropertiesPresenter_ForNumber(_patchRepositories);
            _operatorPropertiesPresenter_ForPatchInlet = new OperatorPropertiesPresenter_ForPatchInlet(_patchRepositories);
            _operatorPropertiesPresenter_ForPatchOutlet = new OperatorPropertiesPresenter_ForPatchOutlet(_patchRepositories);
            _operatorPropertiesPresenter_ForSample = new OperatorPropertiesPresenter_ForSample(_patchRepositories);
            _operatorPropertiesPresenter_ForUnbundle = new OperatorPropertiesPresenter_ForUnbundle(_patchRepositories);
            _patchDetailsPresenter = new PatchDetailsPresenter(_patchRepositories, _entityPositionManager);
            _patchGridPresenter = new PatchGridPresenter(_repositories.DocumentRepository);
            _patchPropertiesPresenter = new PatchPropertiesPresenter(_repositories);
            _sampleGridPresenter = new SampleGridPresenter(_repositories.DocumentRepository, _repositories.SampleRepository);
            _samplePropertiesPresenter = new SamplePropertiesPresenter(_sampleRepositories);
            _scaleGridPresenter = new ScaleGridPresenter(_repositories.DocumentRepository);
            _scalePropertiesPresenter = new ScalePropertiesPresenter(scaleRepositories);
            _toneGridEditPresenter = new ToneGridEditPresenter(scaleRepositories);

            _dispatchDelegateDictionary = CreateDispatchDelegateDictionary();
        }

        // Helpers

        private void HideAllListAndDetailViewModels()
        {
            MainViewModel.DocumentDetails.Visible = false;
            MainViewModel.DocumentGrid.Visible = false;

            MainViewModel.Document.AudioFileOutputGrid.Visible = false;
            MainViewModel.Document.CurveGrid.Visible = false;
            MainViewModel.Document.SampleGrid.Visible = false;
            MainViewModel.Document.ScaleGrid.Visible = false;

            MainViewModel.Document.PatchGridList.ForEach(x => x.Visible = false);
            MainViewModel.Document.CurveDetailsList.ForEach(x => x.Visible = false);
            MainViewModel.Document.ToneGridEditList.ForEach(x => x.Visible = false);

            foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
            {
                patchDocumentViewModel.SampleGrid.Visible = false;
                patchDocumentViewModel.CurveGrid.Visible = false;
                patchDocumentViewModel.CurveDetailsList.ForEach(x => x.Visible = false);
                patchDocumentViewModel.PatchDetails.Visible = false;
            }
        }

        private void HideAllPropertiesViewModels()
        {
            MainViewModel.DocumentDetails.Visible = false;
            MainViewModel.Document.AudioFileOutputPropertiesList.ForEach(x => x.Visible = false);
            MainViewModel.Document.CurvePropertiesList.ForEach(x => x.Visible = false);
            MainViewModel.Document.DocumentProperties.Visible = false;
            MainViewModel.Document.NodePropertiesList.ForEach(x => x.Visible = false);
            MainViewModel.Document.SamplePropertiesList.ForEach(x => x.Visible = false);
            MainViewModel.Document.ScalePropertiesList.ForEach(x => x.Visible = false);
            
            // Note that the not all entity types have Properties view inside the child documents.
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.CurvePropertiesList).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.NodePropertiesList).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForBundles).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCurves).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForNumbers).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchInlets).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForPatchOutlets).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForSamples).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForUnbundles).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.SelectMany(x => x.SamplePropertiesList).ForEach(x => x.Visible = false);
            MainViewModel.Document.PatchDocumentList.ForEach(x => x.PatchProperties.Visible = false);
        }

        private static string GetPlayOutputFilePath()
        {
            return ConfigurationHelper.GetSection<ConfigurationSection>().PatchPlayHackedAudioFileOutputFilePath;
        }

        private static PatchCalculatorTypeEnum GetPatchCalculatorTypeEnum()
        {
            return ConfigurationHelper.GetSection<ConfigurationSection>().PatchCalculatorType;
        }

        private IPatchCalculator CreatePatchCalculator(PatchRepositories repositories, Outlet outlet)
        {
            PatchManager patchManager = new PatchManager(outlet.Operator.Patch, repositories);

            switch (_patchCalculatorTypeEnum)
            {
                case PatchCalculatorTypeEnum.OptimizedPatchCalculator:
                    return patchManager.CreateOptimizedCalculator(outlet);

                case PatchCalculatorTypeEnum.InterpretedPatchCalculator:
                    return patchManager.CreateInterpretedCalculator(outlet);

                default:
                    throw new ValueNotSupportedException(_patchCalculatorTypeEnum);
            }
        }
    }
}
