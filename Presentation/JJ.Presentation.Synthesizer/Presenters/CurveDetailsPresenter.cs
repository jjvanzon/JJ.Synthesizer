using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class CurveDetailsPresenter
    {
        private ICurveRepository _curveRepository;
        private INodeTypeRepository _nodeTypeRepository;

        public CurveDetailsViewModel ViewModel { get; set; }

        public CurveDetailsPresenter(
            ICurveRepository curveRepository,
            INodeTypeRepository nodeTypeRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            _curveRepository = curveRepository;
            _nodeTypeRepository = nodeTypeRepository;
        }

        public CurveDetailsViewModel Show(int id)
        {
            Curve entity = _curveRepository.Get(id);
            ViewModel = entity.ToDetailsViewModel(_nodeTypeRepository);
            ViewModel.Visible = true;
            return ViewModel;
        }
    }
}
