using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
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
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using System.ComponentModel;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.WinForms
{
    public partial class PatchDetailsForm_Old : Form
    {
        private PatchDetailsPresenter _presenter;
        private PatchDetailsViewModel _viewModel;
        private ViewModelToDiagramConverter _converter;
        private ViewModelToDiagramConverterResult _svg;

        /// <summary>
        /// For testing.
        /// </summary>
        private Patch _patch;

        private static bool _forceStateless;
        private static bool _alwaysRecreateDiagram;
        private static bool _mustShowInvisibleElements;
        private static bool _mustCreateMockPatch;
        private static int _testPatchID;
        private static bool _toolTipFeatureEnabled;

        // Constructors

        static PatchDetailsForm_Old()
        {
            ConfigurationSection config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            _forceStateless = config.Testing.ForceStateless;
            _alwaysRecreateDiagram = config.Testing.AlwaysRecreateDiagram;
            _mustShowInvisibleElements = config.Testing.MustShowInvisibleElements;
            _mustCreateMockPatch = config.Testing.MustCreateMockPatch;
            _testPatchID = config.Testing.TestPatchID;
            _toolTipFeatureEnabled = config.Testing.ToolTipsFeatureEnabled;
        }

        public PatchDetailsForm_Old()
        {
            InitializeComponent();

            SetTitles();
        }

        private void PatchDetailsForm_Load(object sender, EventArgs e)
        {
            if (_mustCreateMockPatch)
            {
                _patch = CreateMockPatch();
            }
            else
            {
                _patch = PersistenceHelper.CreateRepository<IPatchRepository>(_context).Get(_testPatchID);
            }

            Edit(_patch.ID);
        }

        // Persistence

        private IContext _context;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IContext Context
        {
            get { return _context; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _context = value;
                _presenter = CreatePresenter(_context);
            }
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

        private void SetValue(string value)
        {
            if (_forceStateless)
            {
                _context = CreateContext();
                _presenter = CreatePresenter(_context);
            }

            _viewModel = _presenter.SetValue(_viewModel, value);

            ApplyViewModel();
        }

        // ApplyViewModel

        private void SetTitles()
        {
            buttonSave.Text = CommonTitles.Save;
            Text = CommonTitlesFormatter.EditObject(PropertyDisplayNames.Patch);
        }

        private void ApplyViewModel()
        {
            if (_svg == null || _alwaysRecreateDiagram)
            {
                UnbindSvgEvents();

                _converter = new ViewModelToDiagramConverter(_mustShowInvisibleElements, _toolTipFeatureEnabled);
                _svg = _converter.Execute(_viewModel.Patch);

                _svg.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
                _svg.MoveGesture.Moved += MoveGesture_Moved;
                _svg.DropGesture.Dropped += DropGesture_Dropped;
                _svg.DeleteOperatorGesture.DeleteRequested += DeleteOperatorGesture_DeleteRequested;

                if (_toolTipFeatureEnabled)
                {
                    _svg.OperatorToolTipGesture.ToolTipTextRequested += OperatorToolTipGesture_ShowToolTipRequested;
                    _svg.InletToolTipGesture.ToolTipTextRequested += InletToolTipGesture_ToolTipTextRequested;
                    _svg.OutletToolTipGesture.ToolTipTextRequested += OutletToolTipGesture_ToolTipTextRequested;
                }

                //_svg.LineGesture.Dropped += DropGesture_Dropped;
            }
            else
            {
                _svg = _converter.Execute(_viewModel.Patch, _svg);
            }

            diagramControl1.Diagram = _svg.Diagram;

            labelSavedMessage.Visible = _viewModel.SavedMessageVisible;

            ApplyOperatorToolboxItemsViewModel(_viewModel.OperatorTypeToolboxItems);

            textBoxValue.Text = _viewModel.SelectedValue;
        }

        private void UnbindSvgEvents()
        {
            if (_svg != null)
            {
                _svg.SelectOperatorGesture.OperatorSelected -= SelectOperatorGesture_OperatorSelected;
                _svg.MoveGesture.Moved -= MoveGesture_Moved;
                _svg.DropGesture.Dropped -= DropGesture_Dropped;
                _svg.DeleteOperatorGesture.DeleteRequested -= DeleteOperatorGesture_DeleteRequested;

                if (_toolTipFeatureEnabled)
                {
                    _svg.OperatorToolTipGesture.ToolTipTextRequested -= OperatorToolTipGesture_ShowToolTipRequested;
                    _svg.InletToolTipGesture.ToolTipTextRequested -= InletToolTipGesture_ToolTipTextRequested;
                    _svg.OutletToolTipGesture.ToolTipTextRequested -= OutletToolTipGesture_ToolTipTextRequested;
                }

                //_svg.LineGesture.Dropped -= DropGesture_Dropped;
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
            // TODO: The 'if' belongs in the presenter.
            if (_viewModel.SelectedOperator != null)
            {
                DeleteOperator(_viewModel.SelectedOperator.ID);                
            }
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            SetValue(textBoxValue.Text);
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            VoidResult result = PlayHelper.Play(_patch);
            if (!result.Successful)
            {
                string messages = String.Join(Environment.NewLine, result.ValidationMessages.Select(x => x.Text));
                MessageBox.Show(messages);
            }
        }

        // TODO: You might want to use the presenter for the the following 3 things.

        private void OperatorToolTipGesture_ShowToolTipRequested(object sender, ToolTipTextEventArgs e)
        {
            int operatorID = TagHelper.GetOperatorID(e.Element.Tag);
            e.ToolTipText = _viewModel.Patch.Operators.Where(x => x.ID == operatorID).Single().Name;
        }

        private void InletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            int inletID = TagHelper.GetInletID(e.Element.Tag);
            InletViewModel inketViewModel = _viewModel.Patch.Operators.SelectMany(x => x.Inlets).Where(x => x.ID == inletID).Single();
            e.ToolTipText = inketViewModel.Name;
        }

        private void OutletToolTipGesture_ToolTipTextRequested(object sender, ToolTipTextEventArgs e)
        {
            int outletID = TagHelper.GetOutletID(e.Element.Tag);
            OutletViewModel outletViewModel = _viewModel.Patch.Operators.SelectMany(x => x.Outlets).Where(x => x.ID == outletID).Single();
            e.ToolTipText = outletViewModel.Name;
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

        private PatchDetailsPresenter CreatePresenter(IContext context)
        {
            IPatchRepository patchRepository = PersistenceHelper.CreateRepository<IPatchRepository>(context);
            IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
            IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
            IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);
            IEntityPositionRepository entityPositionRepository = PersistenceHelper.CreateRepository<IEntityPositionRepository>(context);
            ICurveRepository curveRepository = PersistenceHelper.CreateRepository<ICurveRepository>(context);
            ISampleRepository sampleRepository = PersistenceHelper.CreateRepository<ISampleRepository>(context);
            var presenter = new PatchDetailsPresenter(patchRepository, operatorRepository, inletRepository, outletRepository, entityPositionRepository, curveRepository, sampleRepository);
            return presenter;
        }

        private Patch CreateMockPatch()
        {
            PersistenceWrapper persistenceWrapper = PersistenceHelper.CreatePersistenceWrapper(_context);
            Patch patch = EntityFactory.CreateTestPatch1(persistenceWrapper);
            persistenceWrapper.Flush(); // Flush to get the ID.
            return patch;
        }
    }
}
