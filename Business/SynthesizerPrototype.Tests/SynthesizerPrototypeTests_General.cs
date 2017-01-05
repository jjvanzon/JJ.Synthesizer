using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Visitors;
using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.SynthesizerPrototype.Tests
{
    [TestClass]
    public class SynthesizerPrototypeTests_General
    {
        [TestMethod]
        public void Test_SynthesizerPrototype_OperatorDtoVisitors_PreCalculation()
        {
            var dto = new Sine_OperatorDto
            {
                FrequencyOperatorDto = new Multiply_OperatorDto
                {
                    AOperatorDto = new Number_OperatorDto { Number = 4 },
                    BOperatorDto = new Add_OperatorDto
                    {
                        Vars = new OperatorDtoBase[]
                        {
                            new Number_OperatorDto { Number = 100 },
                            new Number_OperatorDto { Number = 10 }
                        }
                    }
                }
            };

            var visitor = new OperatorDtoPreProcessingExecutor();
            OperatorDtoBase operatorDtoBase = visitor.Execute(dto);

            AssertHelper.IsOfType<Sine_OperatorDto_ConstFrequency_WithOriginShifting>(() => operatorDtoBase);

            var sine_OperatorDto_ConstFrequency_WithOriginShifting = operatorDtoBase as Sine_OperatorDto_ConstFrequency_WithOriginShifting;

            AssertHelper.AreEqual(440.0, () => sine_OperatorDto_ConstFrequency_WithOriginShifting.Frequency);
        }
    }
}