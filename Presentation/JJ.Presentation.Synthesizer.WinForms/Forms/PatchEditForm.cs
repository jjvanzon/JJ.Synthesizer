using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Persistence;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Svg;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms
{
    public partial class PatchEditForm : Form
    {
        private IContext _context;
        private PatchEditPresenter _presenter;
        private PatchEditViewModel _viewModel;
        private ViewModelToDiagramConverter _converter;
        private ViewModelToDiagramConverter.Result _svg;
        
        private static bool _forceStateless;
        private static bool _alwaysRecreateDiagram;
        private static bool _mustShowInvisibleElements;

        static PatchEditForm()
        {
            _forceStateless = AppSettings<IAppSettings>.Get(x => x.ForceStateless);
            _alwaysRecreateDiagram = AppSettings<IAppSettings>.Get(x => x.AlwaysRecreateDiagram);
            _mustShowInvisibleElements = AppSettings<IAppSettings>.Get(x => x.MustShowInvisibleElements);
        }

        public PatchEditForm()
        {
            InitializeComponent();

            SetTitles();

            _context = CreateContext();
            _presenter = CreatePresenter(_context);

            Patch patch;

            bool mustCreateMockPatch = AppSettings<IAppSettings>.Get(x => x.MustCreateMockPatch);
            if (mustCreateMockPatch)
            {
                patch = CreateMockPatch();
            }
            else
            {
                int patchID = AppSettings<IAppSettings>.Get(x => x.TestPatchID);
                patch = PersistenceHelper.CreateRepository<IPatchRepository>(_context).Get(patchID);
            }

            Edit(patch.ID);
        }

        private void SetTitles()
        {
            buttonSave.Text = CommonTitles.Save;
            Text = CommonTitlesFormatter.EditObject(PropertyDisplayNames.Patch);
        }

        // ApplyViewModel

        private void ApplyViewModel()
        {
            if (_svg == null || _alwaysRecreateDiagram)
            {
                UnbindSvgEvents();

                _converter = new ViewModelToDiagramConverter(_mustShowInvisibleElements);
                _svg = _converter.Execute(_viewModel.Patch);

                _svg.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
                _svg.MoveGesture.Moved += MoveGesture_Moved;
                _svg.DropGesture.Dropped += DropGesture_Dropped;
                _svg.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;
                ////_svg.OperatorToolTipGesture.ToolTipTextRequested += OperatorToolTipGesture_ShowToolTipRequested;
                ////_svg.InletToolTipGesture.ToolTipTextRequested += InletToolTipGesture_ToolTipTextRequested;
                ////_svg.OutletToolTipGesture.ToolTipTextRequested += OutletToolTipGesture_ToolTipTextRequested;
                ////_svg.LineGesture.Dropped += DropGesture_Dropped;
            }
            else
            {
                _svg = _converter.Execute(_viewModel.Patch, _svg);
            }

            diagramControl1.Diagram = _svg.Diagram;

            labelSavedMessage.Visible = _viewModel.SavedMessageVisible;

            ApplyOperatorToolboxItemsViewModel(_viewModel.OperatorTypeToolboxItems);
        }

        private void UnbindSvgEvents()
        {
            if (_svg != null)
            {
                _svg.SelectOperatorGesture.OperatorSelected -= SelectOperatorGesture_OperatorSelected;
                _svg.MoveGesture.Moved -= MoveGesture_Moved;
                _svg.DropGesture.Dropped -= DropGesture_Dropped;
                _svg.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;
                ////_svg.OperatorToolTipGesture.ToolTipTextRequested -= OperatorToolTipGesture_ShowToolTipRequested;
                ////_svg.InletToolTipGesture.ToolTipTextRequested -= InletToolTipGesture_ToolTipTextRequested;
                ////_svg.OutletToolTipGesture.ToolTipTextRequested -= OutletToolTipGesture_ToolTipTextRequested;
                ////_svg.LineGesture.Dropped -= DropGesture_Dropped;
            }
        }

        private static Size _defaultToolStripLabelSize = new Size(86, 22);

        private bool _operatorToolboxItemsViewModelApplied = false; // Dirty way to only apply it once.

        private void ApplyOperatorToolboxItemsViewModel(IList<OperatorTypeViewModel> operatorTypeToolboxItems)
        {
            if (_operatorToolboxItemsViewModelApplied)
            {
                return;
            }
            _operatorToolboxItemsViewModelApplied = true;

            int i = 1;

            foreach (OperatorTypeViewModel config in operatorTypeToolboxItems)
            {
                ToolStripItem toolStripItem = new ToolStripButton
                {
                    Name = "toolStripButton" + i,
                    Size = _defaultToolStripLabelSize,
                    Text = config.Symbol,
                    DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text,
                    Tag = config.OperatorTypeName
                };

                // TODO: Clean up the event handlers too somewhere.
                toolStripItem.Click += toolStripLabel_Click;

                toolStrip1.Items.Add(toolStripItem);

                i++;
            }
        }

        // Events

        private void DropGesture_Dropped(object sender, DroppedEventArgs e)
        {
            int inletID =  TagHelper.GetInletID(e.DroppedOnElement.Tag);
            int outletID = TagHelper.GetOutletID(e.DraggedElement.Tag);

            ChangeInputOutlet(inletID, outletID);
        }

        private void MoveGesture_Moved(object sender, MoveEventArgs e)
        {
            int operatorID = TagHelper.GetOperatorID(e.Element.Tag);
            float centerX = e.Element.X + e.Element.Width / 2f;
            float centerY = e.Element.Y + e.Element.Height / 2f;

            MoveOperator(operatorID, centerX, centerY);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripLabel_Click(object sender, EventArgs e)
        {
            ToolStripItem control = (ToolStripItem)sender;
            string operatorTypeName = (string)control.Tag;

            AddOperator(operatorTypeName);
        }

        private void SelectOperatorGesture_OperatorSelected(object sender, ElementEventArgs e)
        {
            int operatorID = TagHelper.GetOperatorID(e.Element.Tag);

            SelectOperator(operatorID);
        }

        private void DeleteOperatorGesture_DeleteRequested(object sender, EventArgs e)
        {
            if (_viewModel.SelectedOperator != null)
            {
                DeleteOperator(_viewModel.SelectedOperator.ID);                
            }
        }

        private void OperatorToolTipGesture_ShowToolTipRequested(object sender, ToolTipTextEventArgs e)
        {
            int operatorID = TagHelper.GetOperatorID(e.Element.Tag);

            // TODO: You might want to do this in the presenter.
            e.ToolTipText = _viewModel.Patch.Operators.Where(x => x.ID == operatorID).Single().Name;
        }

        private void InletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            int inletID = TagHelper.GetInletID(e.Element.Tag);

            // TODO: You might want to do this in the presenter.
            InletViewModel inketViewModel = _viewModel.Patch.Operators.SelectMany(x => x.Inlets).Where(x => x.ID == inletID).Single();
            e.ToolTipText = inketViewModel.Name;
        }

        private void OutletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            int outletID = TagHelper.GetOutletID(e.Element.Tag);

            // TODO: You might want to do this in the presenter.
            OutletViewModel outletViewModel = _viewModel.Patch.Operators.SelectMany(x => x.Outlets).Where(x => x.ID == outletID).Single();
            e.ToolTipText = outletViewModel.Name;
        }

        // Actions

        public void Edit(int patchID)
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.Edit(patchID);

            ApplyViewModel();
        }

        private void AddOperator(string operatorTypeName)
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.AddOperator(_viewModel, operatorTypeName);

            ApplyViewModel();
        }

        private void MoveOperator(int operatorID, float centerX, float centerY)
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.MoveOperator(_viewModel, operatorID, centerX, centerY);

            ApplyViewModel();
        }

        private void ChangeInputOutlet(int inletID, int inputOutletID)
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.ChangeInputOutlet(_viewModel, inletID, inputOutletID);

            ApplyViewModel();
        }

        private void Save()
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.Save(_viewModel);

            ApplyViewModel();
        }

        private void SelectOperator(int operatorID)
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.SelectOperator(_viewModel, operatorID);

            ApplyViewModel();
        }

        private void DeleteOperator(int operatorID)
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.DeleteOperator(_viewModel, operatorID);

            ApplyViewModel();
        }

        // Helpers

        private IContext CreateContext()
        {
            if (_context != null)
            {
                _context.Dispose();
            }

            _context = PersistenceHelper.CreateContext();
            return _context;
        }

        private PatchEditPresenter CreatePresenter(IContext context)
        {
            IPatchRepository patchRepository = PersistenceHelper.CreateRepository<IPatchRepository>(context);
            IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
            IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
            IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);
            IEntityPositionRepository entityPositionRepository = PersistenceHelper.CreateRepository<IEntityPositionRepository>(context);
            ICurveInRepository curveInRepository = PersistenceHelper.CreateRepository<ICurveInRepository>(context);
            IValueOperatorRepository valueOperatorRepository = PersistenceHelper.CreateRepository<IValueOperatorRepository>(context);
            ISampleOperatorRepository sampleOperatorRepository = PersistenceHelper.CreateRepository<ISampleOperatorRepository>(context);
            var presenter = new PatchEditPresenter(patchRepository, operatorRepository, inletRepository, outletRepository, entityPositionRepository, curveInRepository, valueOperatorRepository, sampleOperatorRepository);
            return presenter;
        }

        private Patch CreateMockPatch()
        {
            PersistenceWrapper persistenceWrapper = PersistenceHelper.CreatePersistenceWrapper(_context);
            Patch patch = EntityFactory.CreateTestPatch2(persistenceWrapper);
            persistenceWrapper.Flush(); // Flush to get the ID.
            return patch;
        }
    }
}
