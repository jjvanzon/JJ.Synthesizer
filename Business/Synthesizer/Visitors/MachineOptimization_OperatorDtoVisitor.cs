using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Visitors
{
    internal class MachineOptimization_OperatorDtoVisitor : OperatorDtoVisitorBase
    {
        private class ClosestOverInlets_MathPropertiesDto
        {
            public MathPropertiesDto InputMathPropertiesDto { get; set; }
            public IList<MathPropertiesDto> ItemMathPropertiesDtos { get; set; }
            public bool AllItemsAreConst { get; set; }
            public IList<double> ItemsValues { get; set; }
        }

        public OperatorDtoBase Execute(OperatorDtoBase dto)
        {
            return Visit_OperatorDto_Polymorphic(dto);
        }

        protected override OperatorDtoBase Visit_ClosestOverInlets_OperatorDto(ClosestOverInlets_OperatorDto dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto(dto);

            var mathPropertiesDto = Get_ClosestOverInlets_MathPropertiesDto(dto);

            if (mathPropertiesDto.InputMathPropertiesDto.IsConst)
            {
                return dto.InputOperatorDto;
            }
            if (dto.ItemOperatorDtos.Count == 2 && mathPropertiesDto.AllItemsAreConst)
            {
                double item1 = mathPropertiesDto.ItemsValues[0];
                double item2 = mathPropertiesDto.ItemsValues[1];

                return new ClosestOverInlets_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = item1, Item2 = item2 };
            }
            else if (mathPropertiesDto.AllItemsAreConst)
            {
                return new ClosestOverInlets_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = mathPropertiesDto.ItemsValues };
            }
            else
            {
                return new ClosestOverInlets_OperatorDto_VarInput_VarItems { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }
        }

        protected override OperatorDtoBase Visit_ClosestOverInletsExp_OperatorDto(ClosestOverInletsExp_OperatorDto dto)
        {
            base.Visit_ClosestOverInlets_OperatorDto(dto);

            var mathPropertiesDto = Get_ClosestOverInlets_MathPropertiesDto(dto);

            if (mathPropertiesDto.InputMathPropertiesDto.IsConst)
            {
                return dto.InputOperatorDto;
            }
            if (dto.ItemOperatorDtos.Count == 2 && mathPropertiesDto.AllItemsAreConst)
            {
                double item1 = mathPropertiesDto.ItemsValues[0];
                double item2 = mathPropertiesDto.ItemsValues[1];

                return new ClosestOverInletsExp_OperatorDto_VarInput_2ConstItems { InputOperatorDto = dto.InputOperatorDto, Item1 = item1, Item2 = item2 };
            }
            else if (mathPropertiesDto.AllItemsAreConst)
            {
                return new ClosestOverInletsExp_OperatorDto_VarInput_ConstItems { InputOperatorDto = dto.InputOperatorDto, Items = mathPropertiesDto.ItemsValues };
            }
            else
            {
                return new ClosestOverInletsExp_OperatorDto_VarInput_VarItems { InputOperatorDto = dto.InputOperatorDto, ItemOperatorDtos = dto.ItemOperatorDtos };
            }
        }

        protected override OperatorDtoBase Visit_MaxOverInlets_OperatorDto_Vars_1Const(MaxOverInlets_OperatorDto_Vars_1Const dto)
        {
            // TODO: Specialize to these:
            // MaxOverInlets_OperatorDto_1Var_1Const
            // MaxOverInlets_OperatorDto_2Vars

            throw new NotImplementedException();
        }

        protected override OperatorDtoBase Visit_MinOverInlets_OperatorDto_Vars_1Const(MinOverInlets_OperatorDto_Vars_1Const dto)
        {
            // TODO: Specialize to these:
            // MinOverInlets_OperatorDto_1Var_1Const
            // MinOverInlets_OperatorDto_2Vars

            throw new NotImplementedException();
        }

        // Helpers

        private ClosestOverInlets_MathPropertiesDto Get_ClosestOverInlets_MathPropertiesDto(ClosestOverInlets_OperatorDto dto)
        {
            OperatorDtoBase inputOperatorDto = dto.InputOperatorDto;
            IList<OperatorDtoBase> itemOperatorDtos = dto.ItemOperatorDtos;

            MathPropertiesDto inputMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(inputOperatorDto);

            int count = itemOperatorDtos.Count;
            IList<MathPropertiesDto> itemMathPropertiesDtos = new MathPropertiesDto[count];
            for (int i = 0; i < count; i++)
            {
                OperatorDtoBase itemOperatorDto = itemOperatorDtos[i];
                MathPropertiesDto itemMathPropertiesDto = MathPropertiesHelper.GetMathPropertiesDto(itemOperatorDto);
                itemMathPropertiesDtos[i] = itemMathPropertiesDto;
            }

            bool allItemsAreConst = itemMathPropertiesDtos.All(x => x.IsConst);
            IList<double> itemsValues = itemMathPropertiesDtos.Select(x => x.ConstValue).ToArray();

            var mathPropertiesDto = new ClosestOverInlets_MathPropertiesDto
            {
                InputMathPropertiesDto = inputMathPropertiesDto,
                ItemMathPropertiesDtos = itemMathPropertiesDtos,
                AllItemsAreConst = allItemsAreConst,
                ItemsValues = itemsValues
            };

            return mathPropertiesDto;
        }
    }
}