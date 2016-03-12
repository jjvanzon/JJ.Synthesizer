using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class CustomOperator_OperatorWrapper : OperatorWrapperBase
    {
        private IPatchRepository _patchRepository;

        public CustomOperator_OperatorWrapper(Operator op, IPatchRepository patchRepository)
            : base(op)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            Operands = new CustomOperator_OperatorWrapper_Operands(op);
            Inlets = new CustomOperator_OperatorWrapper_Inlets(op);
            Outlets = new CustomOperator_OperatorWrapper_Outlets(op);
        }

        public CustomOperator_OperatorWrapper_Operands Operands { get; private set; }

        public CustomOperator_OperatorWrapper_Inlets Inlets { get; private set; }

        public CustomOperator_OperatorWrapper_Outlets Outlets { get; private set; }

        public int? UnderlyingPatchID
        {
            get { return ConversionHelper.ParseNullableInt32(WrappedOperator.Data); }
            set { WrappedOperator.Data = Convert.ToString(value); }
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

                return _patchRepository.TryGet(id.Value);
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
