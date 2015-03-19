using JJ.Persistence.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    class PatchEditPresenter
    {
        public PatchEditViewModel Show(int rootOperatorID)
        {
            throw new NotImplementedException();
        }

        // TODO: StartMoveOperator and EndMoveOperator?
        public OperatorViewModel MoveOperator(PatchEditViewModel viewModel, int operatorID, float newX, float newY)
        {
            throw new NotImplementedException();
        }

        public PatchEditViewModel ChangeInletInputOutlet(PatchEditViewModel viewModel, int inletID, int inputOutletID)
        {
            throw new NotImplementedException();
        }
    }
}
