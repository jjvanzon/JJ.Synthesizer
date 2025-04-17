using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Config;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Testing.Core.AssertHelperCore;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable ParameterHidesMember
// ReSharper disable ExplicitCallerInfoArgument
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class TapeWishesTests : MySynthWishes
    {
        double Vol1 = 1.0;
        double Vol2 = 0.05;
        double Vol3 = 0.02;
        
        FlowNode Envelope => RecorderCurve.Stretch(GetAudioLength);
        FlowNode DelayedPulse => DelayedPulseCurve.Stretch(GetAudioLength);

        public TapeWishesTests()
        {
            WithAudioLength(0.25);
            WithParallelProcessing();
        }
        
        [TestMethod] public void Tape_Sines_Test() => Run(Tape_Sines);
        void Tape_Sines()
        {
            var freq = E4.VibratoFreq();;

            var added = Add
            (
                Sine(freq * 1).Volume(Vol1).Curve(Envelope).Tape(),
                Sine(freq * 2).Volume(Vol2).Curve(Envelope).Tape(),
                Sine(freq * 3).Volume(Vol3).Curve(Envelope).Tape()
            ).SetName();

            WithMono().Play(added).Save();
        }

        [TestMethod] public void Tape_Sines_WithPlayAllTapes_Test() => Run(Tape_Sines_WithPlayAllTapes);
        void Tape_Sines_WithPlayAllTapes()
        {
            WithPlayAllTapes();
            WithShortDuration();
            AddAudioLength(0.1);
            
            var freq = G4.VibratoFreq();
            
            var added = Add
            (
                Sine(freq * 1).Volume(Vol1).Curve(DelayedPulse).Tape(),
                Sine(freq * 2).Volume(Vol2).Curve(DelayedPulse).Tape(),
                Sine(freq * 3).Volume(Vol3).Curve(DelayedPulse).Tape()
            ).SetName();
            
            WithMono().Play(added);
        }

        [TestMethod] public void Tape_ConstSignal_Test() => Run(Tape_WithConstSignal);
        void Tape_WithConstSignal()
        {
            var accessor = new SynthWishesAccessor(this);

            // Arrange
            var duration  = 0.1;
            var tolerance = 0.001;

            // Create Entities
            var adder = WithAudioLength(duration).Add
            (
                // Values higher than 1 seem to be clipped.
                Curve(0.1, 0.1).SetName("Const Curve 0.1").Tape(),
                Curve(0.2, 0.2).SetName("Const Curve 0.2").Tape(),
                Curve(0.3, 0.3).SetName("Const Curve 0.3").Tape()
            ).SetName();

            // Assert Entities
            IsNotNull(() => adder);
            IsNotNull(() => adder.UnderlyingOutlet);
            IsNotNull(() => adder.UnderlyingOutlet.Operator);
            IsTrue(() => adder.UnderlyingOutlet.IsAdder());
            IsTrue(() => adder.UnderlyingOutlet.Operator.IsAdder());
            AreEqual("Adder", () => adder.UnderlyingOutlet.Operator.OperatorTypeName);
            
            accessor._tapeRunner.RunAllTapes();

            IsNotNull(() => adder.UnderlyingOutlet.Operator.Inlets);
            var addOperands = adder.UnderlyingOutlet.Operator.Inlets.Select(x => x.Input).ToList();
            AreEqual(3, () => addOperands.Count);

            foreach (var addOperand in addOperands)
            {
                IsNotNull(() => addOperand);
                IsNotNull(() => addOperand.Operator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample.Bytes);
                IsTrue(() => addOperand.IsSample());
                IsTrue(() => addOperand.Operator.IsSample());
                AreEqual("SampleOperator", () => addOperand.Operator.OperatorTypeName);
            }

            NotEqual(addOperands[0], () => addOperands[1]);
            NotEqual(addOperands[1], () => addOperands[2]);

            // Check Bytes Array, Read as Int16 Values
            for (var i = 0; i < addOperands.Count; i++)
            {
                Outlet addOperand = addOperands[i];
                Sample sample     = addOperand.Operator.AsSampleOperator.Sample;
                
                AreEqual(Wav,                        () => sample.GetAudioFileFormatEnum());
                AreEqual(SampleDataTypeEnum.Float32, () => sample.GetSampleDataTypeEnum());
                AreEqual(SpeakerSetupEnum.Mono,      () => sample.GetSpeakerSetupEnum());
                AreEqual(44,                         () => sample.HeaderLength());
                
                int courtesyFramesFound = 0;
                using (var stream = new MemoryStream(sample.Bytes))
                {
                    stream.Position = 44; // Skip header
                    
                    using (var reader = new BinaryReader(stream))
                    {
                        float firstValue = reader.ReadSingle();
                        
                        while (stream.Position < stream.Length)
                        {
                            float nextValue = reader.ReadSingle();
                            
                            // Account for courtesy bytes.
                            if (nextValue == 0)
                            {
                                courtesyFramesFound++;
                                continue;
                            }
                            
                            AreEqual(firstValue, () => nextValue);
                        }
                    }
                }
                
                if (courtesyFramesFound > 0)
                {
                    Log($"Found {courtesyFramesFound} courtesy frames in addOperand[{i}].");
                    if (courtesyFramesFound > GetCourtesyFrames)
                    {
                        Fail($"courtesyValuesFound = {courtesyFramesFound} > {GetCourtesyFrames}");
                    }
                }
            }
            
            // Calculate Values
            double adderResult = adder.Calculate(duration / 2);

            double operandValue1 = addOperands[0].Calculate(duration / 2);
            double operandValue2 = addOperands[1].Calculate(duration / 2);
            double operandValue3 = addOperands[2].Calculate(duration / 2);

            var operandValuesSorted = new[] { operandValue1, operandValue2, operandValue3 }.OrderBy(x => x).ToArray();

            Log($"{new { operandValue1, operandValue2, operandValue3 }}");

            // Assert Values
            AreEqual(0.1 + 0.2 + 0.3,       operandValue1 + operandValue2 + operandValue3, tolerance);
            AreEqual(0.1,             () => operandValuesSorted[0],                        tolerance);
            AreEqual(0.2,             () => operandValuesSorted[1],                        tolerance);
            AreEqual(0.3,             () => operandValuesSorted[2],                        tolerance);
            AreEqual(0.1 + 0.2 + 0.3, () => adderResult,                                   tolerance);
        }

        [TestMethod] public void Tape_ConstSignal_WithPlayAllTapes_Test() => Run(Tape_ConstSignal_WithPlayAllTapes);
        void Tape_ConstSignal_WithPlayAllTapes()
        {
            var accessor = new SynthWishesAccessor(this);

            // Arrange
            var duration = 0.1;

            // Act
            var adder = WithAudioLength(duration).Add
            (
                // Values higher than 1 seem to be clipped.
                Curve(0.1, 0.1).SetName("Const Curve 0.1").Tape(),
                Curve(0.2, 0.2).SetName("Const Curve 0.2").Tape(),
                Curve(0.3, 0.3).SetName("Const Curve 0.3").Tape()
            ).SetName();

            // Assert
            IsNotNull(() => adder);
            IsNotNull(() => adder.UnderlyingOutlet);
            IsNotNull(() => adder.UnderlyingOutlet.Operator);
            IsTrue(() => adder.UnderlyingOutlet.IsAdder());
            IsTrue(() => adder.UnderlyingOutlet.Operator.IsAdder());
            AreEqual("Adder", () => adder.UnderlyingOutlet.Operator.OperatorTypeName);

            accessor._tapeRunner.RunAllTapes();

            IsNotNull(() => adder.UnderlyingOutlet.Operator.Inlets);
            var addOperands = adder.UnderlyingOutlet.Operator.Inlets.Select(x => x.Input).ToList();
            AreEqual(3, () => addOperands.Count);

            foreach (var addOperand in addOperands)
            {
                IsNotNull(() => addOperand);
                IsNotNull(() => addOperand.Operator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample.Bytes);
                IsTrue(() => addOperand.IsSample());
                IsTrue(() => addOperand.Operator.IsSample());
                AreEqual("SampleOperator", () => addOperand.Operator.OperatorTypeName);
            }

            NotEqual(addOperands[0], () => addOperands[1]);
            NotEqual(addOperands[1], () => addOperands[2]);

            // Don't assert values. A setting can insert a delay, messing with the test values.
        }
                
        [TestMethod]
        public void Tape_NormalAdd_ForComparison_Test() => Run(Tape_NormalAdd_ForComparison);
        void Tape_NormalAdd_ForComparison()
        {
            var duration = 0.1;

            var add = Add
            (
                Curve(0.1, 0.1).SetName("Const Curve 0.1"),
                Curve(0.2, 0.2).SetName("Const Curve 0.2"),
                Curve(0.3, 0.3).SetName("Const Curve 0.3")
            );

            double addedValue = add.Calculate(duration / 2);

            AreEqual(0.1 + 0.2 + 0.3, () => addedValue);

            WithMono().WithAudioLength(duration).Save(add);
        }

        // Problem is gone after switching from Play(() => ...) to Run notation,
        // because a Play action is now taped for a specific duration.
        [TestMethod] public void Tape_Selectively_InconsistentDelays_BecauseSineIsForever_TapeIsNot_Test()
            => Run(Tape_Selectively_InconsistentDelays_BecauseSineIsForever_TapeIsNot);
        void Tape_Selectively_InconsistentDelays_BecauseSineIsForever_TapeIsNot()
        {
            WithLeadingSilence(0.4);
            
            var freq = G3.VibratoFreq(depth: _[0.007]);
            
            _[Sine(freq).Tape() + Sine(freq * 3).Volume(0.04)].Save();
        }
    }
}