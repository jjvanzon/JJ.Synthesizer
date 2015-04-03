using JJ.Framework.Persistence;
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

        public PatchEditForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();
            _presenter = CreatePresenter(_context);

            Operator op = CreateMockOperator();

            Edit(op.ID);
        }

        private Operator CreateMockOperator()
        {
            PersistenceWrapper persistenceWrapper = PersistenceHelper.CreatePersistenceWrapper(_context);
            Outlet entity = EntityFactory.CreateTestPatch2(persistenceWrapper);
            persistenceWrapper.Flush(); // Flush to get the ID.
            return entity.Operator;
        }

        public void Edit(int operatorID)
        {
            _viewModel = _presenter.Edit(operatorID);

            ViewModelToDiagramConverter converter = new ViewModelToDiagramConverter();
            ViewModelToDiagramConverter.Result converterResult = converter.Execute(_viewModel.RootOperators.Single());

            diagramControl1.Diagram = converterResult.Diagram;
        }

        private PatchEditPresenter CreatePresenter(IContext context)
        {
            IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
            IEntityPositionRepository entityPositionRepository = PersistenceHelper.CreateRepository<IEntityPositionRepository>(context);
            var presenter = new PatchEditPresenter(operatorRepository, entityPositionRepository);
            return presenter;
        }
    }
}
