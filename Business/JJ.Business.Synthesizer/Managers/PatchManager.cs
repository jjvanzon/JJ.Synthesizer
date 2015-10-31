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

namespace JJ.Business.Synthesizer.Managers
{
    /// <summary>
    /// Manages a Patch and its Operators.
    /// You can supply a patch, Create a new one using the Create method
    /// or omit the Patch to only call methods that do not require it.
    /// </summary>
    public partial class PatchManager
    {
        private PatchRepositories _repositories;

        /// <summary> nullable </summary>
        public Patch Patch { get; set; }

        // Constructors

        public PatchManager(Patch patch, PatchRepositories repositories)
        {
            if (patch == null) throw new NullException(() => patch);
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            Patch = patch;
        }

        public PatchManager(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        // Create

        /// <param name="document">Nullable. Used e.g. to generate a unique name for a Patch.</param>
        /// <param name="mustGenerateName">Only possible if you also pass a document.</param>
        public Patch Create(Document document = null, bool mustGenerateName = false)
        {
            Patch = new Patch();
            Patch.ID = _repositories.IDRepository.GetID();
            _repositories.PatchRepository.Insert(Patch);

            Patch.LinkTo(document);

            if (mustGenerateName)
            {
                ISideEffect sideEffect = new Patch_SideEffect_GenerateName(Patch);
                sideEffect.Execute();
            }

            return Patch;
        }

        public Inlet CreateInlet(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var inlet = new Inlet();
            inlet.ID = _repositories.IDRepository.GetID();
            _repositories.InletRepository.Insert(inlet);

            inlet.LinkTo(op);

            return inlet;
        }

        public Outlet CreateOutlet(Operator op)
        {
            if (op == null) throw new NullException(() => op);

            var outlet = new Outlet();
            outlet.ID = _repositories.IDRepository.GetID();
            _repositories.OutletRepository.Insert(outlet);

            outlet.LinkTo(op);

            return outlet;
        }

        // Save

        public VoidResult Save()
        {
            AssertPatch();

            return Validate();
        }

        /// <summary>
        /// Related operators will also be added to the patch.
        /// If one of the related operators has a different patch assigned to it,
        /// a validation message is returned.
        /// </summary>
        public VoidResult SaveOperator(Operator op)
        {
            AssertPatch();

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

            IValidator validator = new OperatorValidator_Recursive_IsOfSamePatchOrPatchIsNull(op, Patch);
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
            op.LinkTo(Patch);

            foreach (Inlet inlet in op.Inlets)
            {
                if (inlet.InputOutlet != null)
                {
                    AddToPatchRecursive_WithoutValidation(inlet.InputOutlet.Operator);
                }
            }
        }

        // Delete

        public VoidResult DeleteWithRelatedEntities()
        {
            AssertPatch();

            bool isMainPatch = Patch.Document != null &&
                               Patch.Document.MainPatch != null &&
                               (Patch.Document.MainPatch == Patch ||
                                Patch.Document.MainPatch.ID == Patch.ID);
            if (isMainPatch)
            {
                var message = new Message
                {
                    PropertyKey = PropertyNames.Patch,
                    Text = MessageFormatter.CannotDeletePatchBecauseIsMainPatch(Patch.Name)
                };

                return new VoidResult
                {
                    Successful = false,
                    Messages = new Message[] { message }
                };
            }
            else
            {
                Patch.DeleteRelatedEntities(_repositories.OperatorRepository, _repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
                Patch.UnlinkRelatedEntities();
                _repositories.PatchRepository.Delete(Patch);

                return new VoidResult
                {
                    Successful = true
                };
            }
        }

        /// <summary>
        /// Deletes the operator, its inlets and outlets
        /// and connections to its inlets and outlets.
        /// Also applies changes to underlying documents to dependent CustomOperators.
        /// Also cleans up obsolete inlets and outlets from custom operators.
        /// </summary>
        public void DeleteOperator(Operator op)
        {
            AssertPatch();

            if (op == null) throw new NullException(() => op);
            if (op.Patch != Patch) throw new NotEqualException(() => op.Patch, Patch);

            IList<Operator> connectedCustomOperators = 
                Enumerable.Union(
                    op.Inlets.Where(x => x.InputOutlet != null).Select(x => x.InputOutlet.Operator),
                    op.Outlets.SelectMany(x => x.ConnectedInlets).Select(x => x.Operator))
                .Where(x => x.GetOperatorTypeEnum() == OperatorTypeEnum.CustomOperator)
                .ToArray();

            op.UnlinkRelatedEntities();
            op.DeleteRelatedEntities(_repositories.InletRepository, _repositories.OutletRepository, _repositories.EntityPositionRepository);
            _repositories.OperatorRepository.Delete(op);

            if (Patch.Document != null)
            {
                ISideEffect sideEffect = new Document_SideEffect_UpdateDependentCustomOperators(
                    Patch.Document,
                    _repositories.InletRepository,
                    _repositories.OutletRepository,
                    _repositories.DocumentRepository,
                    _repositories.OperatorTypeRepository,
                    _repositories.IDRepository);

                sideEffect.Execute();

                // Clean up obsolete inlets and outlets.
                // (Inlets and outlets that do not exist anymore in a CustomOperator's UnderlyingDocument
                //  are kept alive by the system until it has no connections anymore, so that a user's does not lose data.)
                foreach (Operator connectedCustomOperator in connectedCustomOperators)
                {
                    ISideEffect sideEffect2 = new Operator_SideEffect_ApplyUnderlyingDocument(
                        connectedCustomOperator,
                        _repositories.InletRepository,
                        _repositories.OutletRepository,
                        _repositories.DocumentRepository,
                        _repositories.OperatorTypeRepository,
                        _repositories.IDRepository);

                    sideEffect2.Execute();
                }
            }
        }

        public void DeleteInlet(int id)
        {
            Inlet entity = _repositories.InletRepository.Get(id);
            DeleteInlet(entity);
        }

        public void DeleteInlet(Inlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            entity.UnlinkRelatedEntities();
            _repositories.InletRepository.Delete(entity);
        }

        public void DeleteOutlet(int id)
        {
            Outlet entity = _repositories.OutletRepository.Get(id);
            DeleteOutlet(entity);
        }

        public void DeleteOutlet(Outlet entity)
        {
            if (entity == null) throw new NullException(() => entity);

            entity.UnlinkRelatedEntities();
            _repositories.OutletRepository.Delete(entity);
        }

        // Validate

        private VoidResult Validate()
        {
            var messages = new List<Message>();

            if (Patch.Document != null)
            {
                IValidator validator1 = new PatchValidator_InDocument(Patch);
                messages.AddRange(validator1.ValidationMessages.ToCanonical());
            }

            IValidator validator2 = new PatchValidator_Recursive(Patch, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository, new HashSet<object>());
            messages.AddRange(validator2.ValidationMessages.ToCanonical());

            bool successful = messages.Count == 0;

            return new VoidResult
            {
                Messages = messages,
                Successful = successful
            };
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

        // Misc

        /// <summary> Validates for instance that no operator connections are lost. </summary>
        public VoidResult SetOperatorInletCount(Operator op, int inletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetInletCount(op, inletCount);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Messages = validator.ValidationMessages.ToCanonical(),
                    Successful = false
                };
            }

            IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.SortOrder).ToArray();

            // Create additional inlets
            for (int i = sortedInlets.Count; i < inletCount; i++)
            {
                Inlet inlet = CreateInlet(op);
            }

            // Delete excessive inlets
            for (int i = sortedInlets.Count - 1; i >= inletCount; i--)
            {
                Inlet inlet = sortedInlets[i];
                DeleteInlet(inlet);
            }

            // Validate just in case some rule is not adhered to in the transformations above.
            // TODO: Uncommented. Perhaps remove completely. In practice I do a validation afterwards anyway.
            //VoidResult result = ValidateNonRecursive(entity);
            //return result;

            return new VoidResult
            {
                Successful = true
            };
        }

        public VoidResult SetOperatorOutletCount(Operator op, int outletCount)
        {
            if (op == null) throw new NullException(() => op);

            IValidator validator = new OperatorValidator_SetOutletCount(op, outletCount);
            if (!validator.IsValid)
            {
                return new VoidResult
                {
                    Messages = validator.ValidationMessages.ToCanonical(),
                    Successful = false
                };
            }

            IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.SortOrder).ToArray();

            // Create additional outlets
            for (int i = sortedOutlets.Count; i < outletCount; i++)
            {
                Outlet outlet = CreateOutlet(op);
            }

            // Delete excessive outlets
            for (int i = sortedOutlets.Count - 1; i >= outletCount; i--)
            {
                Outlet outlet = sortedOutlets[i];
                DeleteOutlet(outlet);
            }

            return new VoidResult
            {
                Successful = true
            };
        }

        /// <summary>
        /// Optimized has slower initialization and faster sound generation (best for outputting sound).
        /// </summary>
        public IPatchCalculator CreateOptimizedCalculator(params Outlet[] channelOutlets)
        {
            return CreateOptimizedCalculator((IList<Outlet>)channelOutlets);
        }

        /// <summary>
        /// Optimized has slower initialization and faster sound generation (best for outputting sound).
        /// </summary>
        public IPatchCalculator CreateOptimizedCalculator(IList<Outlet> channelOutlets)
        {
            int assumedSamplingRate = 44100;
            var whiteNoiseCalculator = new WhiteNoiseCalculator(assumedSamplingRate);

            return new OptimizedPatchCalculator(channelOutlets, whiteNoiseCalculator, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository);
        }

        /// <summary>
        /// Interpreted mode has fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </summary>
        public IPatchCalculator CreateInterpretedCalculator(params Outlet[] channelOutlets)
        {
            return CreateInterpretedCalculator((IList<Outlet>)channelOutlets);
        }

        /// <summary>
        /// Interpreted mode has fast initialization and slow sound generation (best for previewing values or drawing out plots).
        /// </summary>
        public IPatchCalculator CreateInterpretedCalculator(IList<Outlet> channelOutlets)
        {
            int assumedSamplingRate = 44100;
            var whiteNoiseCalculator = new WhiteNoiseCalculator(assumedSamplingRate);
            return new InterpretedPatchCalculator(channelOutlets, whiteNoiseCalculator, _repositories.CurveRepository, _repositories.SampleRepository, _repositories.DocumentRepository);
        }

        private void AssertPatch()
        {
            if (Patch == null) throw new NullException(() => Patch);
        }
    }
}
