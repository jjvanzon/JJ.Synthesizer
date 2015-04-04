using JJ.Framework.Persistence;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Svg.Converters;
using JJ.Presentation.Synthesizer.ViewModels;
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

            _context = PersistenceHelper.CreateContext();
            _presenter = CreatePresenter(_context);

            Patch patch = CreateMockPatch();

            Edit(patch.ID);
        }

        // Actions

        public void Edit(int patchID)
        {
            _viewModel = _presenter.Edit(patchID);

            Render();
        }

        private void ChangeInputOutlet(int inletID, int inputOutletID)
        {
            _viewModel = _presenter.ChangeInputOutlet(_viewModel, inletID, inputOutletID);

            Render();
        }

        // Events

        private void DropGesture_OnDragDrop(object sender, DragDropEventArgs e)
        {
            // TODO: Temporarily (2015-04-04) disabled. Enable again
            return;

            int inletID = Int32.Parse(e.DroppedOnElement.Tag);
            int outletID = Int32.Parse(e.DraggedElement.Tag);

            ChangeInputOutlet(inletID, outletID);
        }

        // Other

        private PatchEditPresenter CreatePresenter(IContext context)
        {
            IPatchRepository patchRepository = PersistenceHelper.CreateRepository<IPatchRepository>(context);
            IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
            IInletRepository inletRepository = PersistenceHelper.CreateRepository<IInletRepository>(context);
            IOutletRepository outletRepository = PersistenceHelper.CreateRepository<IOutletRepository>(context);
            IEntityPositionRepository entityPositionRepository = PersistenceHelper.CreateRepository<IEntityPositionRepository>(context);
            var presenter = new PatchEditPresenter(patchRepository, operatorRepository, inletRepository, outletRepository, entityPositionRepository);
            return presenter;
        }

        private void Render()
        {
            if (_svg != null)
            {
                _svg.DropGesture.OnDragDrop -= DropGesture_OnDragDrop;
            }

            var converter = new ViewModelToDiagramConverter();
            _svg = converter.Execute(_viewModel.Patch);

            diagramControl1.Diagram = _svg.Diagram;

            _svg.DropGesture.OnDragDrop += DropGesture_OnDragDrop;
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
