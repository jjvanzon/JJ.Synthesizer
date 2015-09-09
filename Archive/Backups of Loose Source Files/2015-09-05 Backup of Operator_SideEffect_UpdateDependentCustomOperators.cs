//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using JJ.Framework.Business;
//using JJ.Framework.Reflection.Exceptions;
//using System;

//namespace JJ.Business.Synthesizer.SideEffects
//{
//    // TODO: Remove class.
//    // TODO: Make internal after you have encapsulated this functionality in a Manager class.
//    [Obsolete("Probably obsolete.")]
//    public class Operator_SideEffect_UpdateDependentCustomOperators : ISideEffect
//    {
//        private IInletRepository _inletRepository;
//        private IOutletRepository _outletRepository;
//        private IDocumentRepository _documentRepository;
//        private IOperatorTypeRepository _operatorTypeRepository;
//        private IIDRepository _idRepository;

//        private Operator _operator;

//        public Operator_SideEffect_UpdateDependentCustomOperators(
//            Operator op,
//            IInletRepository inletRepository,
//            IOutletRepository outletRepository,
//            IDocumentRepository documentRepository,
//            IOperatorTypeRepository operatorTypeRepository,
//            IIDRepository idRepository)
//        {
//            if (op == null) throw new NullException(() => op);

//            if (inletRepository == null) throw new NullException(() => inletRepository);
//            if (outletRepository == null) throw new NullException(() => outletRepository);
//            if (documentRepository == null) throw new NullException(() => documentRepository);
//            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
//            if (idRepository == null) throw new NullException(() => idRepository);

//            _operator = op;

//            _inletRepository = inletRepository;
//            _outletRepository = outletRepository;
//            _documentRepository = documentRepository;
//            _operatorTypeRepository = operatorTypeRepository;
//            _idRepository = idRepository;
//        }

//        public void Execute()
//        {
//            OperatorTypeEnum operatorTypeEnum = _operator.GetOperatorTypeEnum();

//            bool isPatchInletOrPatchOutlet = operatorTypeEnum == OperatorTypeEnum.PatchInlet ||
//                                             operatorTypeEnum == OperatorTypeEnum.PatchOutlet;
//            if (isPatchInletOrPatchOutlet)
//            {
//                ISideEffect sideEffect = new Document_SideEffect_UpdateDependentCustomOperators(
//                    _operator.Patch.Document,
//                    _inletRepository,
//                    _outletRepository,
//                    _documentRepository,
//                    _operatorTypeRepository,
//                    _idRepository);

//                sideEffect.Execute();
//            }
//        }
//    }
//}
