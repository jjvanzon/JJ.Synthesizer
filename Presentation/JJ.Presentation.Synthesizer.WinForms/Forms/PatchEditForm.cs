using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Persistence;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Svg.Converters;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms
{
    public partial class PatchEditForm : Form
    {
        private IContext _context;
        private PatchEditPresenter _presenter;
        private PatchEditViewModel _viewModel;
        private ViewModelToDiagramConverter.Result _svg;

        public PatchEditForm()
        {
            InitializeComponent();

            SetTitles();

            _context = PersistenceHelper.CreateContext();
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

        // Actions

        public void Edit(int patchID)
        {
            _viewModel = _presenter.Edit(patchID);

            ApplyViewModel();
        }

        private void AddOperator(string operatorTypeName)
        {
            _viewModel = _presenter.AddOperator(_viewModel, operatorTypeName);

            ApplyViewModel();
        }

        private void MoveOperator(int operatorID, float centerX, float centerY)
        {
            _viewModel = _presenter.MoveOperator(_viewModel, operatorID, centerX, centerY);

            ApplyViewModel();
        }

        private void ChangeInputOutlet(int inletID, int inputOutletID)
        {
            _viewModel = _presenter.ChangeInputOutlet(_viewModel, inletID, inputOutletID);

            ApplyViewModel();
        }

        private void Save()
        {
            _viewModel = _presenter.Save(_viewModel);

            ApplyViewModel();
        }

        private void SelectOperator(int operatorID)
        {
            _viewModel = _presenter.SelectOperator(_viewModel, operatorID);

            ApplyViewModel();
        }

        // Events

        private void DropGesture_Dropped(object sender, DroppedEventArgs e)
        {
            int inletID = (int)e.DroppedOnElement.Tag;
            int outletID = (int)e.DraggedElement.Tag;

            ChangeInputOutlet(inletID, outletID);
        }

        private void MoveGesture_Moved(object sender, MoveEventArgs e)
        {
            int operatorID = (int)e.Element.Tag;
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

        private void SelectOperatorGesture_OperatorSelected(object sender, OperatorSelectedEventArgs e)
        {
            int operatorID = (int)e.Element.Tag;

            SelectOperator(operatorID);
        }

        // ApplyViewModel

        private void ApplyViewModel()
        {
            UnbindSvgEvents();

            bool mustShowInvisibleElements = AppSettings<IAppSettings>.Get(x => x.MustShowInvisibleElements);

            ViewModelToDiagramConverter converter = new ViewModelToDiagramConverter(mustShowInvisibleElements);
            _svg = converter.Execute(_viewModel.Patch, _viewModel.SelectedOperator);
            diagramControl1.Diagram = _svg.Diagram;

            _svg.DropGesture.Dropped += DropGesture_Dropped;
            _svg.MoveGesture.Moved += MoveGesture_Moved;
            _svg.SelectOperatorGesture.OperatorSelected += SelectOperatorGesture_OperatorSelected;
            //_svg.LineGesture.Dropped += DropGesture_Dropped;

            labelSavedMessage.Visible = _viewModel.SavedMessageVisible;

            ApplyOperatorToolboxItemsViewModel(_viewModel.OperatorTypeToolboxItems);
        }

        private void UnbindSvgEvents()
        {
            if (_svg != null)
            {
                _svg.DropGesture.Dropped -= DropGesture_Dropped;
                _svg.MoveGesture.Moved -= MoveGesture_Moved;
                _svg.SelectOperatorGesture.OperatorSelected -= SelectOperatorGesture_OperatorSelected;
                //_svg.LineGesture.Dropped -= DropGesture_Dropped;
            }
        }

        private static Size _defaultToolStripLabelSize = new Size(86, 22);

        // TODO: This is a dirty way to only apply it once.
        private bool _operatorToolboxItemsViewModelApplied = false;

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

        // Helpers

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
