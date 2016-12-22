using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Exceptions;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class RecursiveUnderlyingPatchEntityToDtoVisitor : OperatorDtoVisitorBase
    {
        private readonly ICurveRepository _curveRepository;
        private readonly IPatchRepository _patchRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly ISpeakerSetupRepository _speakerSetupRepository;
        private readonly IOperatorRepository _operatorRepository;

        public RecursiveUnderlyingPatchEntityToDtoVisitor(
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            ISampleRepository sampleRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IOperatorRepository operatorRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (speakerSetupRepository == null) throw new NullException(() => speakerSetupRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);

            _curveRepository = curveRepository;
            _patchRepository = patchRepository;
            _sampleRepository = sampleRepository;
            _speakerSetupRepository = speakerSetupRepository;
            _operatorRepository = operatorRepository;
        }

        public OperatorDtoBase Execute(Outlet outlet)
        {
            var visitor2 = new OperatorEntityToDtoVisitor(_curveRepository, _patchRepository, _sampleRepository, _speakerSetupRepository);

            OperatorDtoBase dto = visitor2.Execute(outlet);

            return Execute(dto);
        }

        private OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_CustomOperator_OperatorDto(CustomOperator_OperatorDto dto)
        {
            base.Visit_CustomOperator_OperatorDto(dto);
            // Get Underlying Patch
            if (!dto.UnderlyingPatchID.HasValue)
            {
                return new Number_OperatorDto_Zero();
            }
            Patch underlyingPatch = _patchRepository.Get(dto.UnderlyingPatchID.Value);

            // Get CustomOperator Outlet
            Operator customOperator = _operatorRepository.Get(dto.OperatorID);
            Outlet customOperatorOutlet = customOperator.Outlets[dto.OutletListIndex];

            // Get Matching PatchOutlet
            Operator patchOutlet = InletOutletMatcher.TryGetPatchOutlet(customOperatorOutlet, _patchRepository);

            // Visit PatchOutlet's Input.
            var patchOutletWrapper = new PatchOutlet_OperatorWrapper(patchOutlet);
            var visitor2 = new RecursiveUnderlyingPatchEntityToDtoVisitor(_curveRepository, _patchRepository, _sampleRepository, _speakerSetupRepository, _operatorRepository);
            return visitor2.Execute(patchOutletWrapper.Input);
        }

        protected override OperatorDtoBase Visit_PatchInlet_OperatorDto(PatchInlet_OperatorDto dto)
        {

            return base.Visit_PatchInlet_OperatorDto(dto);
        }

        /// <summary> Entity-as-source version, which will not work, because we want to apply a DTO in-place transformation trick. </summary>
        //protected override void VisitCustomOperatorOutlet(Outlet customOperatorOutlet)
        //{
        //    // Get Underlying Patch
        //    var wrapper = new CustomOperator_OperatorWrapper(customOperatorOutlet.Operator, _patchRepository);
        //    Patch patch = wrapper.UnderlyingPatch;

        //    // Get Matching PatchOutlet
        //    Operator patchOutlet = InletOutletMatcher.TryGetPatchOutlet(customOperatorOutlet, _patchRepository;
        //    if (patchOutlet == null)
        //    {
        //        // TODO: Handle this edge case.
        //        throw new NotImplementedException();
        //    }

        //    // Visit PatchOutlet's Input.
        //    var patchOutletWrapper = new PatchOutlet_OperatorWrapper(patchOutlet);
        //    VisitOutletPolymorphic(patchOutletWrapper.Input);
        //  
        //    TODO: You need a _stack.
        //}
    }
}
