//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Data.Synthesizer;
//using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
//using JJ.Framework.Business;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.SideEffects
//{
//    [Obsolete("", true)]
//    internal class Operator_SideEffect_CopyPatchInletPropertiesToItsInlet : ISideEffect
//    {
//        private readonly Operator _entity;
//        private readonly IInletTypeRepository _inletTypeRepository;

//        public Operator_SideEffect_CopyPatchInletPropertiesToItsInlet(Operator entity, IInletTypeRepository inletTypeRepository)
//        {
//            if (entity == null) throw new NullException(() => entity);
//            if (inletTypeRepository == null) throw new NullException(() => inletTypeRepository);

//            _entity = entity;
//            _inletTypeRepository = inletTypeRepository;
//        }

//        public void Execute()
//        {
//            bool mustCopyProperties = MustCopyProperties();
//            if (!mustCopyProperties)
//            {
//                return;
//            }

//            var wrapper = new PatchInlet_OperatorWrapper(_entity);

//            Inlet inlet = wrapper.Inlet;

//            throw new NotImplementedException("This code is obsolete.");

//            //InletTypeEnum inletTypeEnum = wrapper.InletTypeEnum;

//            //inlet.DefaultValue = wrapper.DefaultValue;
//            //inlet.SetInletTypeEnum(inletTypeEnum, _inletTypeRepository);
//        }

//        private bool MustCopyProperties()
//        {
//            return _entity.GetOperatorTypeEnum() == OperatorTypeEnum.PatchInlet;
//        }
//    }
//}
