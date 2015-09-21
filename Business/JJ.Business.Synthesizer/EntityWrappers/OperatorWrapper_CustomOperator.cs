using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using System;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class OperatorWrapper_CustomOperator : OperatorWrapperBase
    {
        private IDocumentRepository _documentRepository;

        public OperatorWrapper_CustomOperator(Operator op, IDocumentRepository documentRepository)
            : base(op)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            Operands = new OperatorWrapper_CustomOperator_Operands(op);
            Inlets = new OperatorWrapper_CustomOperator_Inlets(op);
            Outlets = new OperatorWrapper_CustomOperator_Outlets(op);
        }

        public OperatorWrapper_CustomOperator_Operands Operands { get; private set; }

        public OperatorWrapper_CustomOperator_Inlets Inlets { get; private set; }

        public OperatorWrapper_CustomOperator_Outlets Outlets { get; private set; }

        public int? UnderlyingDocumentID
        {
            get { return ConversionHelper.ParseNullableInt32(_operator.Data); }
            set { _operator.Data = Convert.ToString(value); }
        }

        /// <summary> nullable </summary>
        public Document UnderlyingDocument
        {
            get
            {
                int? underlyingDocumentID = UnderlyingDocumentID;
                if (!underlyingDocumentID.HasValue)
                {
                    return null;
                }

                return _documentRepository.TryGet(underlyingDocumentID.Value);
            }
            set
            {
                if (value == null)
                {
                    UnderlyingDocumentID = null;
                    return;
                }

                UnderlyingDocumentID = value.ID;
            }
        }

        //// TODO: These operations must enfore rules and should be integrated in the members above.

        //private void SetUnderlyingDocument(Operator op, Document document)
        //{
        //    if (op == null) throw new NullException(() => op);
        //    if (document == null) throw new NullException(() => document);
        //    if (op.GetOperatorTypeEnum() != OperatorTypeEnum.CustomOperator) throw new NotEqualException(() => op.GetOperatorTypeEnum(), OperatorTypeEnum.CustomOperator);

        //    // What can go wrong? Everything.
        //    throw new NotImplementedException();
        //}

        //private void SetName(Document document, string name)
        //{
        //    if (document == null) throw new NullException(() => document);

        //    //if (document.Name 
        //}

    }
}
