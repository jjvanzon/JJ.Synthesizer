using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
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
        private PatchEditPresenter _presenter;
        private PatchEditViewModel _viewModel;

        public PatchEditForm()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            IContext context = PersistenceHelper.CreateContext();
            PersistenceWrapper persistenceWrapper = PersistenceHelper.CreatePersistenceWrapper(context);

            Outlet entity = EntityFactory.CreateTestPatch2(persistenceWrapper);

            _presenter = CreatePresenter(context);

            persistenceWrapper.Flush(); // Flush necessary to get the entity.Operator.ID.

            _viewModel = _presenter.Edit(entity.Operator.ID);

            diagramControl1.Diagram = _viewModel.Diagram;
        }

        private PatchEditPresenter CreatePresenter(IContext context)
        {
            IOperatorRepository operatorRepository = PersistenceHelper.CreateRepository<IOperatorRepository>(context);
            IEntityPositionRepository entityPositionRepository = PersistenceHelper.CreateRepository<IEntityPositionRepository>(context);
            var presenter = new PatchEditPresenter(operatorRepository, entityPositionRepository);
            return presenter;
        }

        public PatchEditPresenter _presetner { get; set; }
    }
}
