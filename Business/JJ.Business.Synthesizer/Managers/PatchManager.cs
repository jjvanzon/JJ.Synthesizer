using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Framework.Business;
using JJ.Data.Synthesizer;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Factories;

namespace JJ.Business.Synthesizer.Managers
{
    /// <summary>
    /// Manages a Patch and its Operators.
    /// If you do not supply a patch, one will be created for you.
    /// </summary>
    public partial class PatchManager
    {
        /// <summary> not nullable </summary>
        private Patch _patch;
        private PatchRepositories _repositories;
        private OperatorFactory _operatorFactory;

        /// <summary> not nullable </summary>
        public Patch Patch
        {
            get { return _patch; }
        }

        /// <summary> Creates a new patch. </summary>
        public PatchManager(PatchRepositories repositories)
        {
            Initialize(null, null, repositories, false);
        }

        /// <param name="patch">If null, creates a new Patch. </param>
        public PatchManager(Patch patch, PatchRepositories repositories)
        {
            Initialize(patch, null, repositories, false);
        }

        /// <summary> Creates a new Patch. </summary>
        /// <param name="document">nullable</param>
        public PatchManager(Document document, PatchRepositories repositories)
        {
            Initialize(null, document, repositories, false);
        }

        /// <summary> Creates a new patch. </summary>
        /// <param name="document">nullable</param>
        /// <param name="mustGenerateName">only possible if you also pass a document</param>
        public PatchManager(Document document, PatchRepositories repositories, bool mustGenerateName)
        {
            Initialize(null, document, repositories, mustGenerateName);
        }

        /// <summary> If patch is null, one with be created using the document and mustGeneratePatchName parameters. </summary>
        /// <param name="patch">nullable</param>
        /// <param name="document">nullable</param>
        /// <param name="mustGenerateName">only possible if you also pass a document</param>
        private void Initialize(Patch patch, Document document, PatchRepositories repositories, bool mustGenerateName)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _patch = patch;
            _repositories = repositories;

            if (_patch == null)
            {
                _patch = Create(document, mustGenerateName);
            }

            _operatorFactory = new OperatorFactory(repositories);
        }

        private Patch Create(Document document = null, bool mustGenerateName = false)
        {
            var patch = new Patch();
            patch.ID = _repositories.IDRepository.GetID();
            _repositories.PatchRepository.Insert(patch);

            patch.LinkTo(document);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Patch_SideEffect_GenerateName(patch);
                sideEffect.Execute();
            }

            return patch;
        }

        public VoidResult Save()
        {
            return Validate();
        }

        public VoidResult DeleteWithRelatedEntities()
        {

            bool isMainPatch = _patch.Document.MainPatch != null &&
                               (_patch.Document.MainPatch == _patch ||
                                _patch.Document.MainPatch.ID == _patch.ID);
            if (isMainPatch)
            {
                var message = new Message
                {
                    PropertyKey = PropertyNames.Patch,
                    Text = MessageFormatter.CannotDeletePatchBecauseIsMainPatch(_patch.Name)
                };

                return new VoidResult
                {
                    Successful = false,
                    Messages = new Message[] { message }
                };
            }
            else
            {
                _patch.DeleteRelatedEntities(_repositories.OperatorRepository, _repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
                _patch.UnlinkRelatedEntities();
                _repositories.PatchRepository.Delete(_patch);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }

        private VoidResult Validate()
        {
            var messages = new List<Message>();

            if (_patch.Document != null)
            {
                IValidator validator1 = new PatchValidator_InDocument(_patch);
                messages.AddRange(validator1.ValidationMessages.ToCanonical());
            }

            IValidator validator2 = new PatchValidator_Recursive(_patch, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository, new HashSet<object>());
            messages.AddRange(validator2.ValidationMessages.ToCanonical());

            bool successful = messages.Count == 0;

            return new VoidResult
            {
                Messages = messages,
                Successful = successful
            };
        }

        /// <summary>
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        public VoidResult SaveOperator(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            VoidResult result = AddToPatchRecursive(op);
            if (!result.Successful)
            {
                return result;
            }

            OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
            switch (operatorTypeEnum)
            {
                case OperatorTypeEnum.CustomOperator:
                    return SaveOperator_Custom(op);

                case OperatorTypeEnum.PatchInlet:
                case OperatorTypeEnum.PatchOutlet:
                    return SaveOperator_PatchInletOrPatchOutlet(op);

                default:
                    return SaveOperator_Other(op);
            }
        }

        private VoidResult SaveOperator_Custom(Operator op)
        {
            ISideEffect sideEffect = new Operator_SideEffect_ApplyUnderlyingDocument(
                op,
                _repositories.InletRepository,
                _repositories.OutletRepository,
                _repositories.DocumentRepository,
                _repositories.OperatorTypeRepository,
                _repositories.IDRepository);

            sideEffect.Execute();

            VoidResult result = ValidateNonRecursive(op);
            return result;
        }

        private VoidResult SaveOperator_PatchInletOrPatchOutlet(Operator op)
        {
            // TODO: Document why we need to validate the whole patch, instead of just the operator.
            // Probably due to unique constraints, but it really should be described more exactly why.
            VoidResult result = Validate();
            if (result.Successful)
            {
                ISideEffect sideEffect = new Document_SideEffect_UpdateDependentCustomOperators(
                    op.Patch.Document,
                    _repositories.InletRepository,
                    _repositories.OutletRepository,
                    _repositories.DocumentRepository,
                    _repositories.OperatorTypeRepository,
                    _repositories.IDRepository);

                sideEffect.Execute();
            }

            return result;
        }

        private VoidResult SaveOperator_Other(Operator op)
        {
            VoidResult result = ValidateNonRecursive(op);
            return result;
        }

        /// <summary>
        /// Adds an operator to the patch.
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        private VoidResult AddToPatchRecursive(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(op, _patch);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Successful = false,
                    Messages = validator.ValidationMessages.ToCanonical()
                };
            }

            AddToPatchRecursive_WithoutValidation(op);

            return new VoidResult { Successful = true };
        }

        private void AddToPatchRecursive_WithoutValidation(Operator op)
        {
            op.LinkTo(_patch);

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    AddToPatchRecursive_WithoutValidation(inlet.InputOutlet.Operator);
                }
            }
        }

        private VoidResult ValidateNonRecursive(Operator op)
        {
            IValidator validator = new OperatorValidator_Versatile(op, _repositories.DocumentRepository);

            return new VoidResult
            {
                Messages = validator.ValidationMessages.ToCanonical(),
                Successful = validator.IsValid
            };
        }

        public void DeleteOperator(Operator op)
        {
            // TODO: Lower priority: Delegate to DeleteOperator method everywhere you now have inlined it.
            // This is postponed, because it is part of a bigger concern that more must be encapsulated in the business layer,
            // and only exposed through the manager, for instance cascading and inverse property management and possibly more.

            if (op == null) throw new NullException(() => op);
            if (op.Patch != _patch) throw new NotEqualException(() => op.Patch, Patch);

            op.UnlinkRelatedEntities();
            op.DeleteRelatedEntities(_repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
            _repositories.OperatorRepository.Delete(op);

            if (_patch.Document != null)
            {
                ISideEffect sideEffect = new Document_SideEffect_UpdateDependentCustomOperators(
                    _patch.Document,
                    _repositories.InletRepository,
                    _repositories.OutletRepository,
                    _repositories.DocumentRepository,
                    _repositories.OperatorTypeRepository,
                    _repositories.IDRepository);

                sideEffect.Execute();
            }
        }

        // TODO: These overloads are ugly, e.g. CreateCalculator(true, outlet1)

        /// <param name="optimized">
        /// Set to true for slower initialization and faster sound generation (best for outputting sound).
        /// Set to false for fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </param>
        public IPatchCalculator CreateCalculator(params Outlet[] channelOutlets)
        {
            return CreateCalculator((IList<Outlet>)channelOutlets);
        }

        /// <param name="optimized">
        /// Set to true for slower initialization and faster sound generation (best for outputting sound).
        /// Set to false for fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </param>
        public IPatchCalculator CreateCalculator(bool optimized, params Outlet[] channelOutlets)
        {
            return CreateCalculator((IList<Outlet>)channelOutlets, optimized);
        }

        /// <param name="optimized">
        /// Set to true for slower initialization and faster sound generation (best for outputting sound).
        /// Set to false for fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </param>
        public IPatchCalculator CreateCalculator(IList<Outlet> channelOutlets, bool optimized = true)
        {
            // TODO: Verify channel outlets.

            int assumedSamplingRate = 44100;
            var whiteNoiseCalculator = new WhiteNoiseCalculator(assumedSamplingRate);

            if (optimized)
            {
                return new OptimizedPatchCalculator(channelOutlets, whiteNoiseCalculator, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository);
            }
            else
            {
                return new InterpretedPatchCalculator(channelOutlets, whiteNoiseCalculator, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository);
            }
        }
    }
}
