using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_CustomOperator : OperatorWrapperBase
    {
        private IPatchRepository _patchRepository;

        public OperatorWrapper_CustomOperator(Operator op, IPatchRepository patchRepository)
            : base(op)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);

            _patchRepository = patchRepository;

            Operands = new OperatorWrapper_CustomOperator_Operands(op);
            Inlets = new OperatorWrapper_CustomOperator_Inlets(op);
            Outlets = new OperatorWrapper_CustomOperator_Outlets(op);
        }

        public OperatorWrapper_CustomOperator_Operands Operands { get; private set; }

        public OperatorWrapper_CustomOperator_Inlets Inlets { get; private set; }

        public OperatorWrapper_CustomOperator_Outlets Outlets { get; private set; }

        public int? UnderlyingPatchID
        {
            get { return ConversionHelper.ParseNullableInt32(_operator.Data); }
            set { _operator.Data = Convert.ToString(value); }
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
