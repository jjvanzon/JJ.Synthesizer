using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611 
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.Technical.Attributes
{
    [TestClass]
    [TestCategory("Technical")]
    public class InterpolationWishesTests
    {
        [TestMethod, DataRow(Line) ,DataRow(Block)]
        public void Init_Interpolation(InterpolationTypeEnum init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_Interpolation(int initAsInt, int valueAsInt)
        {            
            var init  = (InterpolationTypeEnum)initAsInt;
            var value = (InterpolationTypeEnum)valueAsInt;
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, value);
                Assert_TapeBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, value);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.Interpolation(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.Interpolation(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Interpolation(value)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes.WithInterpolation(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode.WithInterpolation(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.WithInterpolation(value)));
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Blocky());
                if (value == Line) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Linear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithBlocky());
                if (value == Line) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithLinear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Blocky());
                if (value == Line) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Linear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithBlocky());
                if (value == Line) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithLinear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Blocky());
                if (value == Line) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Linear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.WithBlocky());
                if (value == Line) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.WithLinear()); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Interpolation(int initAsInt, int valueAsInt)
        {
            var init  = (InterpolationTypeEnum)initAsInt;
            var value = (InterpolationTypeEnum)valueAsInt;

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,       () => x.TapeBound.Tape.Interpolation(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Interpolation(value)));
            AssertProp(x =>                               x.TapeBound.TapeConfig.Interpolation = value);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Interpolation(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.Interpolation(value)));
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Blocky());
                if (value == Line) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Linear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Blocky());
                if (value == Line) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Linear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Blocky());
                if (value == Line) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Linear()); });
            
            AssertProp(x => {
                if (value == Block) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Blocky());
                if (value == Line) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Linear()); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_Interpolation(int initAsInt, int valueAsInt)
        {
            // Independent after Taping
            
            var init  = (InterpolationTypeEnum)initAsInt;
            var value = (InterpolationTypeEnum)valueAsInt;

            // Sample
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, value);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Interpolation(value, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (value == Block) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Blocky(x.SynthBound.Context));
                    if (value == Line ) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Linear(x.SynthBound.Context)); });
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_Interpolation(int intAsInit, int intAsValue)
        {
            var init  = (InterpolationTypeEnum)intAsInit;
            var value = (InterpolationTypeEnum)intAsValue;
            var x = CreateTestEntities(init);

            // InterpolationTypeEnum
            
            var interpolations = new List<InterpolationTypeEnum>();
            {
                void AssertProp(Func<InterpolationTypeEnum> setter)
                {
                    Assert_Independent_Getters(x.Immutable.Interpolation, init);
                    
                    InterpolationTypeEnum interpolation2 = setter();
                    
                    Assert_Independent_Getters(x.Immutable.Interpolation, init);
                    Assert_Independent_Getters(interpolation2, value);
                    
                    interpolations.Add(interpolation2);
                }

                AssertProp(() => x.Immutable.Interpolation.Interpolation(value));
                AssertProp(() => value.Interpolation());
                AssertProp(() => value == Block ? x.Immutable.Interpolation.Blocky() : x.Immutable.Interpolation.Linear());
            }

            // InterpolationType Entity
            
            var interpolationEntity = new List<InterpolationType>();
            {
                void AssertProp(Func<InterpolationType> setter)
                {
                    Assert_Independent_Getters(x.Immutable.InterpolationEntity, init);

                    InterpolationType interpolationEntity2 = setter();
                    
                    Assert_Independent_Getters(x.Immutable.InterpolationEntity, init);
                    Assert_Independent_Getters(interpolationEntity2, value);
                    
                    interpolationEntity.Add(interpolationEntity2);
                }
                
                AssertProp(() => x.Immutable.InterpolationEntity.Interpolation(value, x.SynthBound.Context));
                AssertProp(() => value.ToEntity(x.SynthBound.Context));
                AssertProp(() => value == Block ? x.Immutable.InterpolationEntity.Blocky(x.SynthBound.Context) : x.Immutable.InterpolationEntity.Linear(x.SynthBound.Context));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            interpolations.ForEach(e => Assert_Independent_Getters(e, value));
            interpolationEntity    .ForEach(s => Assert_Independent_Getters(s, value));
        }

        [TestMethod] public void ConfigSections_Interpolation()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = TestEntities.GetConfigSectionAccessor();
            
            AreEqual(DefaultInterpolation,          () => configSection.Interpolation);
            AreEqual(DefaultInterpolation,          () => configSection.Interpolation());
            AreEqual(DefaultInterpolation == Block, () => configSection.IsBlocky());
            AreEqual(DefaultInterpolation == Line,  () => configSection.IsLinear());
        }

        // Helpers
        
        private TestEntities CreateTestEntities(InterpolationTypeEnum interpolation) => new TestEntities(x => x.WithInterpolation(interpolation));

        static object TestParameters => new [] // ncrunch: no coverage
        {
            new object[] { (int)Block, (int)Line },
            new object[] { (int)Line, (int)Block }
        };

        private void Assert_All_Getters(TestEntities x, InterpolationTypeEnum interpolation)
        {
            Assert_Bound_Getters(x, interpolation);
            Assert_Independent_Getters(x, interpolation);
            Assert_Immutable_Getters(x, interpolation);
        }

        private void Assert_Bound_Getters(TestEntities x, InterpolationTypeEnum interpolation)
        {
            Assert_SynthBound_Getters(x, interpolation);
            Assert_TapeBound_Getters(x, interpolation);
        }
        
        private void Assert_Independent_Getters(TestEntities x, InterpolationTypeEnum interpolation)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, interpolation);
        }

        private void Assert_Immutable_Getters(TestEntities x, InterpolationTypeEnum interpolation)
        {
            Assert_Independent_Getters(x.Immutable.Interpolation, interpolation);
            Assert_Independent_Getters(x.Immutable.InterpolationEntity, interpolation);
        }

        private void Assert_SynthBound_Getters(TestEntities x, InterpolationTypeEnum interpolation)
        {
            AreEqual(interpolation, () => x.SynthBound.SynthWishes.Interpolation());
            AreEqual(interpolation, () => x.SynthBound.SynthWishes.GetInterpolation);
            AreEqual(interpolation, () => x.SynthBound.FlowNode.Interpolation());
            AreEqual(interpolation, () => x.SynthBound.FlowNode.GetInterpolation);
            AreEqual(interpolation, () => x.SynthBound.ConfigWishes.Interpolation());
            AreEqual(interpolation, () => x.SynthBound.ConfigWishes.GetInterpolation);
            
            AreEqual(interpolation == Block, () => x.SynthBound.SynthWishes.IsBlocky());
            AreEqual(interpolation == Block, () => x.SynthBound.SynthWishes.IsBlocky);
            AreEqual(interpolation == Block, () => x.SynthBound.FlowNode.IsBlocky());
            AreEqual(interpolation == Block, () => x.SynthBound.FlowNode.IsBlocky);
            AreEqual(interpolation == Block, () => x.SynthBound.ConfigWishes.IsBlocky());
            AreEqual(interpolation == Block, () => x.SynthBound.ConfigWishes.IsBlocky);
            
            AreEqual(interpolation == Line, () => x.SynthBound.SynthWishes.IsLinear());
            AreEqual(interpolation == Line, () => x.SynthBound.SynthWishes.IsLinear);
            AreEqual(interpolation == Line, () => x.SynthBound.FlowNode.IsLinear());
            AreEqual(interpolation == Line, () => x.SynthBound.FlowNode.IsLinear);
            AreEqual(interpolation == Line, () => x.SynthBound.ConfigWishes.IsLinear());
            AreEqual(interpolation == Line, () => x.SynthBound.ConfigWishes.IsLinear);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, InterpolationTypeEnum interpolation)
        {
            AreEqual(interpolation, () => x.TapeBound.Tape.Interpolation());
            AreEqual(interpolation, () => x.TapeBound.TapeConfig.Interpolation());
            AreEqual(interpolation, () => x.TapeBound.TapeConfig.Interpolation);
            AreEqual(interpolation, () => x.TapeBound.TapeActions.Interpolation());
            AreEqual(interpolation, () => x.TapeBound.TapeAction.Interpolation());
            
            AreEqual(interpolation == Block, () => x.TapeBound.Tape.IsBlocky());
            AreEqual(interpolation == Block, () => x.TapeBound.TapeConfig.IsBlocky());
            AreEqual(interpolation == Block, () => x.TapeBound.TapeActions.IsBlocky());
            AreEqual(interpolation == Block, () => x.TapeBound.TapeAction.IsBlocky());
        
            AreEqual(interpolation == Line, () => x.TapeBound.Tape.IsLinear());
            AreEqual(interpolation == Line, () => x.TapeBound.TapeConfig.IsLinear());
            AreEqual(interpolation == Line, () => x.TapeBound.TapeActions.IsLinear());
            AreEqual(interpolation == Line, () => x.TapeBound.TapeAction.IsLinear());
        }
        
        void Assert_Independent_Getters(Sample sample, InterpolationTypeEnum interpolation)
        {
            AreEqual(interpolation,          () => sample.Interpolation());
            AreEqual(interpolation == Block, () => sample.IsBlocky());
            AreEqual(interpolation == Line,  () => sample.IsLinear());
        }
        
        void Assert_Independent_Getters(InterpolationTypeEnum interpolationTypeEnum, InterpolationTypeEnum interpolation)
        {
            AreEqual(interpolation,          () => interpolationTypeEnum.Interpolation());
            AreEqual(interpolation == Block, () => interpolationTypeEnum.IsBlocky());
            AreEqual(interpolation == Line,  () => interpolationTypeEnum.IsLinear());
        }
        
        void Assert_Independent_Getters(InterpolationType interpolationType, InterpolationTypeEnum interpolation)
        {
            if (interpolationType == null) throw new NullException(() => interpolationType);
            AreEqual(interpolation,          () => interpolationType.Interpolation());
            AreEqual(interpolation == Block, () => interpolationType.IsBlocky());
            AreEqual(interpolation == Line,  () => interpolationType.IsLinear());
        }
    } 
}