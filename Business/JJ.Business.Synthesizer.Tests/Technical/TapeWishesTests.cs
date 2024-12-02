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
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
// ReSharper disable ParameterHidesMember

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class TapeWishesTests : MySynthWishes
    {
        FlowNode Envelope => DelayedPulseCurve.Stretch(GetAudioLength) * 0.4;

        public TapeWishesTests()
        {
            WithShortDuration();
            WithParallelTaping();
        }
        
        [TestMethod]
        public void Tape_NormalAdd_ForComparison_Test() => new TapeWishesTests().Tape_NormalAdd_ForComparison();
        private void Tape_NormalAdd_ForComparison()
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

            WithMono().WithAudioLength(duration).Save(() => add);
        }

        [TestMethod]
        public void Tape_WithConstSignal_Test() => new TapeWishesTests().Tape_WithConstSignal();
        private void Tape_WithConstSignal()
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
            
            accessor.RunAllTapes(new[] { adder });

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
                AreEqual(44,                         () => sample.GetHeaderLength());
                
                int extraBufferFramesFound = 0;
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
                                extraBufferFramesFound++;
                                continue;
                            }
                            
                            AreEqual(firstValue, () => nextValue);
                        }
                    }
                }
                
                if (extraBufferFramesFound > 0)
                {
                    Console.WriteLine($"Found {extraBufferFramesFound} courtesy frames in addOperand[{i}].");
                    if (extraBufferFramesFound > GetExtraBufferFrames)
                    {
                        Assert.Fail($"courtesyValuesFound = {extraBufferFramesFound} > {GetExtraBufferFrames}");
                    }
                }
            }
            
            // Calculate Values
            double adderResult = adder.Calculate(duration / 2);

            double operandValue1 = addOperands[0].Calculate(duration / 2);
            double operandValue2 = addOperands[1].Calculate(duration / 2);
            double operandValue3 = addOperands[2].Calculate(duration / 2);

            var operandValuesSorted = new[] { operandValue1, operandValue2, operandValue3 }.OrderBy(x => x).ToArray();

            Console.WriteLine($"{new { operandValue1, operandValue2, operandValue3 }}");

            // Assert Values
            Assert.AreEqual(0.1 + 0.2 + 0.3, operandValue1 + operandValue2 + operandValue3, tolerance);
            Assert.AreEqual(0.1,             operandValuesSorted[0],                        tolerance);
            Assert.AreEqual(0.2,             operandValuesSorted[1],                        tolerance);
            Assert.AreEqual(0.3,             operandValuesSorted[2],                        tolerance);
            Assert.AreEqual(0.1 + 0.2 + 0.3, adderResult,                                   tolerance);
        }

        [TestMethod]
        public void Tape_WithConstSignal_WithPlayAllTapes_Test() => new TapeWishesTests().Tape_WithConstSignal_WithPlayAllTapes();
        private void Tape_WithConstSignal_WithPlayAllTapes()
        {
            var accessor = new SynthWishesAccessor(this);

            // Arrange
            WithPlayAllTapes();

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

            accessor.RunAllTapes(new[] { adder });

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
        public void Tape_WithSinePartials_Test() => new TapeWishesTests().Tape_WithSinePartials();
        private void Tape_WithSinePartials()
        {
            var freq = A4;

            var added = Add
            (
                Sine(freq * 1).Volume(1.0).Curve(Envelope).Tape(),
                Sine(freq * 2).Volume(0.2).Curve(Envelope).Tape(),
                Sine(freq * 3).Volume(0.7).Curve(Envelope).Tape()
            ).SetName();

            WithMono().Play(() => added);
        }

        [TestMethod]
        public void SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot_Test()
            => new TapeWishesTests().SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot();
        private void SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot()
        {
            Play(() => Sine(A3).Tape() + Sine(A4));
        }
        
        [TestMethod]
        public void PlayAllTapesTest() => new TapeWishesTests().PlayAllTapes();
        private void PlayAllTapes()
        {
            WithPlayAllTapes();
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Curve(Envelope).Tape(),
                     Sine(pitch * 2).Volume(0.2).Curve(Envelope).Tape(),
                     Sine(pitch * 3).Volume(0.3).Curve(Envelope).Tape()
                 ));
        }
        
        [TestMethod]
        public void Tape_SinePartials_WithPlayAllTapes_Test() => new TapeWishesTests().Tape_SinePartials_WithPlayAllTapes();
        private void Tape_SinePartials_WithPlayAllTapes()
        {
            var freq = A4;
            
            WithPlayAllTapes();
            
            var added = Add
            (
                Sine(freq * 1).Volume(1.0).Curve(Envelope).Tape(),
                Sine(freq * 2).Volume(0.2).Curve(Envelope).Tape(),
                Sine(freq * 3).Volume(0.7).Curve(Envelope).Tape()
            ).SetName();
            
            WithMono().Play(() => added);
        }

        [TestMethod]
        public void FluentPlay_UsingTape_Test() => new TapeWishesTests().FluentPlay_UsingTape();
        private void FluentPlay_UsingTape()
        {
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Curve(Envelope).Volume(1.0).ChannelPlay(),
                     Sine(pitch * 2).Curve(Envelope).Volume(0.2),
                     Sine(pitch * 3).Curve(Envelope).ChannelPlay().Volume(0.3),
                     Sine(pitch * 4).Curve(Envelope).Volume(0.4),
                     Sine(pitch * 5).Curve(Envelope).Volume(0.2).ChannelPlay()
                 ));
        }
        
        [TestMethod]
        public void FluentSave_UsingTape_Test() => new TapeWishesTests().FluentSave_UsingTape();
        private void FluentSave_UsingTape()
        {
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).ChannelSave(MemberName() + " Partial 1"),
                     Sine(pitch * 2).Volume(0.2),
                     Sine(pitch * 3).ChannelSave("FluentSave_UsingTape Partial 2").Volume(0.3),
                     Sine(pitch * 4).Volume(0.4),
                     Sine(pitch * 5).Volume(0.2).SetName("FluentSave_UsingTape Partial 3").ChannelSave()
                 ) * Envelope).Save();
        }
        
        [TestMethod]
        public void Tape_Streaming_GoesPerChannel_Test() => new TapeWishesTests().Tape_Streaming_GoesPerChannel();
        private void Tape_Streaming_GoesPerChannel()
        {
            var pitch = A4;
            
            WithStereo();

            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Curve(Envelope).Panning(0.2).ChannelPlay(),
                     Sine(pitch * 2).Volume(0.3).Curve(Envelope).Panning(0.8).ChannelPlay()
                 ) * Envelope * 1.5);

            Play(() => Add
                 (
                     1.0 * Sine(pitch * 1).Curve(Envelope).Panbrello(3.000, 0.2).ChannelPlay(),
                     0.2 * Sine(pitch * 2).Curve(Envelope).Panbrello(5.234, 0.3).ChannelPlay(),
                     0.3 * Sine(pitch * 3).Curve(Envelope).Panbrello(7.000, 0.2).ChannelPlay()
                 ) * Envelope * 1.5);
        }
        
        [TestMethod]
        public void FluentCache_UsingTape_Test() => new TapeWishesTests().FluentCache_UsingTape();
        void FluentCache_UsingTape() 
        {
            WithStereo();
            
            var buffs = new Buff[2];
            
            // The delegate creates a non-trivial convergence point.
            
            Save(() => Sine(A4).Panning(0.1).Curve(Envelope).ChannelCache((b, i) => buffs[i] = b)).Play();
            
            IsNotNull(() => buffs[0]);
            IsNotNull(() => buffs[1]);
            
            buffs[0].Play();
            buffs[1].Play();
            
            // Recombination can only be done after running all tapes.
           
            Save(() => Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Play();
        }
    }
}