using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Business.Synthesizer.Managers
{
    public class PatchManager
    {
        private IPatchRepository _patchRepository;
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private IEntityPositionRepository _entityPositionRepository;

        public PatchManager(
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);

            _patchRepository = patchRepository;
            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _entityPositionRepository = entityPositionRepository;
        }

        /// <summary>
        /// Adds an operator to the patch.
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        public VoidResult AddToPatchRecursive(Operator op, Patch patch)
        {
            if (op == null) throw new NullException(() => op);
            if (patch == null) throw new NullException(() => patch);

            IValidator validator = new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(op, patch);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }

            FillInPatchRecursive(op, patch);

            return new VoidResult
            {
                Successful = true,
                Messages = new JJ.Business.CanonicalModel.Message[0]
            };
        }

        private void FillInPatchRecursive(Operator op, Patch patch)
        {
            op.LinkTo(patch);

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    FillInPatchRecursive(inlet.InputOutlet.Operator, patch);
                }
            }
        }

        public VoidResult DeletePatchWithRelatedEntities(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            bool isMainPatch = patch.Document.MainPatch != null &&
                               (patch.Document.MainPatch == patch ||
                                patch.Document.MainPatch.ID == patch.ID);
            if (isMainPatch)
            {
                var message = new Message { PropertyKey = PropertyNames.Patch, Text = MessageFormatter.CannotDeletePatchBecauseIsMainPatch(patch.Name) };
                return new VoidResult
                {
                    Successful = false,
                    Messages = new Message[] { message }
                };
            }
            else
            {
                patch.DeleteRelatedEntities(_operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
                patch.UnlinkRelatedEntities();
                _patchRepository.Delete(patch);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }
    }
}
