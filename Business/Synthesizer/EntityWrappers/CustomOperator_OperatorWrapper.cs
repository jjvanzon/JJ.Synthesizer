using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;
using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper : OperatorWrapperBase
    {
        private readonly IPatchRepository _patchRepository;

        public CustomOperator_OperatorWrapper(Operator op, IPatchRepository patchRepository)
            : base(op)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            Operands = new CustomOperator_OperatorWrapper_Operands(op);
            Inlets = new CustomOperator_OperatorWrapper_Inlets(op);
            Outlets = new CustomOperator_OperatorWrapper_Outlets(op);
        }

        public CustomOperator_OperatorWrapper_Operands Operands { get; }

        public CustomOperator_OperatorWrapper_Inlets Inlets { get; }

        public CustomOperator_OperatorWrapper_Outlets Outlets { get; }

        public int? UnderlyingPatchID
        {
            get { return DataPropertyParser.TryGetInt32(WrappedOperator, PropertyNames.UnderlyingPatchID); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.UnderlyingPatchID, value); }
        }

        /// <summary> nullable </summary>
        public Patch UnderlyingPatch
        {
            get
            {
                int? id = UnderlyingPatchID;
                if (!id.HasValue)
                {
                    return null;
                }

                return _patchRepository.Get(id.Value);
            }
            set
            {
                if (value == null)
                {
                    UnderlyingPatchID = null;
                    return;
                }

                UnderlyingPatchID = value.ID;
            }
        }

        public override string GetInletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => Inlets.Count);

            Inlet inlet = Inlets[listIndex];

            // Use Name
            if (!string.IsNullOrEmpty(inlet.Name))
            {
                return inlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = inlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                return ResourceHelper.GetDisplayName(dimensionEnum);
            }

            // Use List Position (not ListIndex, becuase it does not have to be consecutive).
            int listPosition = WrappedOperator.Inlets.IndexOf(inlet);
            string displayName = string.Format("{0} {1}", PropertyDisplayNames.Inlet, listPosition + 1);
            return displayName;
        }

        public override string GetOutletDisplayName(int listIndex)
        {
            if (listIndex < 0) throw new InvalidIndexException(() => listIndex, () => Outlets.Count);

            Outlet outlet = Outlets[listIndex];

            // Use Name
            if (!string.IsNullOrEmpty(outlet.Name))
            {
                return outlet.Name;
            }

            // Use Dimension
            DimensionEnum dimensionEnum = outlet.GetDimensionEnum();
            if (dimensionEnum != DimensionEnum.Undefined)
            {
                return ResourceHelper.GetDisplayName(dimensionEnum);
            }

            // Use List Position (not ListIndex, becuase it does not have to be consecutive).
            int listPosition = WrappedOperator.Outlets.IndexOf(outlet);
            string displayName = string.Format("{0} {1}", PropertyDisplayNames.Outlet, listPosition + 1);
            return displayName;

        }

        //// TODO: These operations must enfore rules and should be integrated in the members above.

        //private void SetUnderlyingPatch(Operator op, Patch patch)
        //{
        //    if (op == null) throw new NullException(() => op);
        //    if (patch == null) throw new NullException(() => patch);
        //    if (op.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => op.GetOperatorTypeEnum(), OperatorTypeEnum.CustomOperator);

        //    // What can go wrong? Everything.
        //    throw new NotImplementedException();
        //}

        //private void SetName(Patch patch, string name)
        //{
        //    if (patch == null) throw new NullException(() => patch);

        //    //if (patch.Name 
        //}
    }
}
