using JJ.Business.Synthesizer.Tests.NanoOptimization.Dto;
using JJ.Business.Synthesizer.Tests.NanoOptimization.Visitors;
using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.NanoOptimization
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
                    AOperatorDto = new Add_OperatorDto
                    {
                        Vars = new OperatorDtoBase[]
                        {
                            new Number_OperatorDto { Number = 100 },
                            new Number_OperatorDto { Number = 10 }
                        }
                    },
                    BOperatorDto = new Number_OperatorDto { Number = 4 }
                }
            };

            var visitor = new OperatorDtoVisitor_PreProcessing();
            OperatorDtoBase operatorDtoBase = visitor.Execute(dto);

            AssertHelper.IsOfType<Sine_OperatorDto_ConstFrequency_WithOriginShifting>(() => operatorDtoBase);

            var sine_OperatorDto_ConstFrequency_WithOriginShifting = operatorDtoBase as Sine_OperatorDto_ConstFrequency_WithOriginShifting;

            AssertHelper.AreEqual(440.0, () => sine_OperatorDto_ConstFrequency_WithOriginShifting.Frequency);
        }
    }
}