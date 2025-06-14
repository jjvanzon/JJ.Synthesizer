using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Testing.Core.AssertCore;
using static JJ.Framework.Existence.Core.FilledInHelper;
using static JJ.Business.Synthesizer.Enums.InterpolationTypeEnum;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;

// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611 

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class InterpolationWishesTests
    {
        [TestMethod, DataRow(Line) ,DataRow(Block), DataRow(Undefined), DataRow((InterpolationTypeEnum)0), DataRow(null)]
        public void Init_Interpolation(InterpolationTypeEnum? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceInterpolation(init));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_Interpolation(int? initAsInt, int? valueAsInt)
        {            
            var init  = (InterpolationTypeEnum?)initAsInt;
            var val = (InterpolationTypeEnum?)valueAsInt;
            
            void AssertProp(Action<SynthBoundEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceInterpolation(init));
                
                setter(x.SynthBound);
                
                Assert_SynthBound_Getters(x, CoalesceInterpolation(val));
                Assert_TapeBound_Getters(x, CoalesceInterpolation(init));
                Assert_Independent_Getters(x, CoalesceInterpolation(init));
                Assert_Immutable_Getters(x, CoalesceInterpolation(init));
                
                x.Record();
                Assert_All_Getters(x, CoalesceInterpolation(val));
            }

            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .Interpolation    (val)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .Interpolation    (val)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.Interpolation    (val)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .WithInterpolation(val)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .WithInterpolation(val)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.WithInterpolation(val)));
            AssertProp(x => AreEqual(x.SynthWishes,    x.SynthWishes   .SetInterpolation (val)));
            AssertProp(x => AreEqual(x.FlowNode,       x.FlowNode      .SetInterpolation (val)));
            AssertProp(x => AreEqual(x.ConfigResolver, x.ConfigResolver.SetInterpolation (val)));
            AssertProp(x => AreEqual(x.SynthWishes,    Interpolation    (x.SynthWishes   , val)));
            AssertProp(x => AreEqual(x.FlowNode,       Interpolation    (x.FlowNode      , val)));
            AssertProp(x => AreEqual(x.ConfigResolver, Interpolation    (x.ConfigResolver, val)));
            AssertProp(x => AreEqual(x.SynthWishes,    WithInterpolation(x.SynthWishes   , val)));
            AssertProp(x => AreEqual(x.FlowNode,       WithInterpolation(x.FlowNode      , val)));
            AssertProp(x => AreEqual(x.ConfigResolver, WithInterpolation(x.ConfigResolver, val)));
            AssertProp(x => AreEqual(x.SynthWishes,    SetInterpolation (x.SynthWishes   , val)));
            AssertProp(x => AreEqual(x.FlowNode,       SetInterpolation (x.FlowNode      , val)));
            AssertProp(x => AreEqual(x.ConfigResolver, SetInterpolation (x.ConfigResolver, val)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .Interpolation    (x.SynthWishes   , val)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .Interpolation    (x.FlowNode      , val)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.Interpolation    (x.ConfigResolver, val)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .WithInterpolation(x.SynthWishes   , val)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .WithInterpolation(x.FlowNode      , val)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.WithInterpolation(x.ConfigResolver, val)));
            AssertProp(x => AreEqual(x.SynthWishes,    ConfigWishes        .SetInterpolation (x.SynthWishes   , val)));
            AssertProp(x => AreEqual(x.FlowNode,       ConfigWishes        .SetInterpolation (x.FlowNode      , val)));
            AssertProp(x => AreEqual(x.ConfigResolver, ConfigWishesAccessor.SetInterpolation (x.ConfigResolver, val)));
            
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Blocky           (   ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => x.SynthWishes   .Linear           (   ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          x.SynthWishes   .Interpolation    (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithBlocky       (   ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => x.SynthWishes   .WithLinear       (   ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          x.SynthWishes   .WithInterpolation(val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsBlocky         (   ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => x.SynthWishes   .AsLinear         (   ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          x.SynthWishes   .AsInterpolation  (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetBlocky        (   ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => x.SynthWishes   .SetLinear        (   ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          x.SynthWishes   .SetInterpolation (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => x.FlowNode      .Blocky           (   ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => x.FlowNode      .Linear           (   ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             x.FlowNode      .Interpolation    (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => x.FlowNode      .WithBlocky       (   ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => x.FlowNode      .WithLinear       (   ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             x.FlowNode      .WithInterpolation(val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => x.FlowNode      .AsBlocky         (   ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => x.FlowNode      .AsLinear         (   ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             x.FlowNode      .AsInterpolation  (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => x.FlowNode      .SetBlocky        (   ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => x.FlowNode      .SetLinear        (   ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             x.FlowNode      .SetInterpolation (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Blocky           (   ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => x.ConfigResolver.Linear           (   ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       x.ConfigResolver.Interpolation    (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithBlocky       (   ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => x.ConfigResolver.WithLinear       (   ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       x.ConfigResolver.WithInterpolation(val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsBlocky         (   ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => x.ConfigResolver.AsLinear         (   ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       x.ConfigResolver.AsInterpolation  (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetBlocky        (   ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => x.ConfigResolver.SetLinear        (   ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       x.ConfigResolver.SetInterpolation (val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => Blocky           (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => Linear           (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          Interpolation    (x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => WithBlocky       (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => WithLinear       (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          WithInterpolation(x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => AsBlocky         (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => AsLinear         (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          AsInterpolation  (x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => SetBlocky        (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => SetLinear        (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          SetInterpolation (x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => Blocky           (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => Linear           (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             Interpolation    (x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => WithBlocky       (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => WithLinear       (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             WithInterpolation(x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => AsBlocky         (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => AsLinear         (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             AsInterpolation  (x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => SetBlocky        (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => SetLinear        (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             SetInterpolation (x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => Blocky           (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => Linear           (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       Interpolation    (x.ConfigResolver, val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => WithBlocky       (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => WithLinear       (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       WithInterpolation(x.ConfigResolver, val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => AsBlocky         (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => AsLinear         (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       AsInterpolation  (x.ConfigResolver, val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => SetBlocky        (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => SetLinear        (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       SetInterpolation (x.ConfigResolver, val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => ConfigWishes        .Blocky           (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => ConfigWishes        .Linear           (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          ConfigWishes        .Interpolation    (x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithBlocky       (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => ConfigWishes        .WithLinear       (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          ConfigWishes        .WithInterpolation(x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsBlocky         (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => ConfigWishes        .AsLinear         (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          ConfigWishes        .AsInterpolation  (x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetBlocky        (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => ConfigWishes        .SetLinear        (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          ConfigWishes        .SetInterpolation (x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => ConfigWishes        .Blocky           (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => ConfigWishes        .Linear           (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             ConfigWishes        .Interpolation    (x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => ConfigWishes        .WithBlocky       (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => ConfigWishes        .WithLinear       (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             ConfigWishes        .WithInterpolation(x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => ConfigWishes        .AsBlocky         (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => ConfigWishes        .AsLinear         (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             ConfigWishes        .AsInterpolation  (x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => ConfigWishes        .SetBlocky        (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => ConfigWishes        .SetLinear        (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             ConfigWishes        .SetInterpolation (x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Blocky           (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.Linear           (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       ConfigWishesAccessor.Interpolation    (x.ConfigResolver, val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithBlocky       (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.WithLinear       (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       ConfigWishesAccessor.WithInterpolation(x.ConfigResolver, val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsBlocky         (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.AsLinear         (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       ConfigWishesAccessor.AsInterpolation  (x.ConfigResolver, val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetBlocky        (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => ConfigWishesAccessor.SetLinear        (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       ConfigWishesAccessor.SetInterpolation (x.ConfigResolver, val)); });
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

            AssertProp(x =>                                         x.TapeBound.TapeConfig .Interpolation   = value);
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Interpolation    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Interpolation    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Interpolation    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Interpolation    (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithInterpolation(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithInterpolation(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithInterpolation(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithInterpolation(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AsInterpolation  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AsInterpolation  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsInterpolation  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AsInterpolation  (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetInterpolation (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetInterpolation (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetInterpolation (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetInterpolation (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => Interpolation    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => Interpolation    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => Interpolation    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => Interpolation    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithInterpolation(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithInterpolation(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithInterpolation(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithInterpolation(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => AsInterpolation  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => AsInterpolation  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => AsInterpolation  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => AsInterpolation  (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetInterpolation (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetInterpolation (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetInterpolation (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetInterpolation (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.Interpolation    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.Interpolation    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Interpolation    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.Interpolation    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithInterpolation(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithInterpolation(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithInterpolation(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithInterpolation(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AsInterpolation  (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AsInterpolation  (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsInterpolation  (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AsInterpolation  (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetInterpolation (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetInterpolation (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetInterpolation (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetInterpolation (x.TapeBound.TapeAction , value)));
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .Blocky    ());
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .Linear    ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .Blocky    ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .Linear    ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Blocky    ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Linear    ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .Blocky    ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .Linear    ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .WithBlocky());
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .WithLinear()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .WithBlocky());
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .WithLinear()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithBlocky());
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithLinear()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .WithBlocky());
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .WithLinear()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .AsBlocky  ());
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .AsLinear  ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .AsBlocky  ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .AsLinear  ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsBlocky  ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsLinear  ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .AsBlocky  ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .AsLinear  ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .SetBlocky ());
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => x.TapeBound.Tape       .SetLinear ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .SetBlocky ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => x.TapeBound.TapeConfig .SetLinear ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetBlocky ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetLinear ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .SetBlocky ());
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => x.TapeBound.TapeAction .SetLinear ()); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => Blocky    (x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => Linear    (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => Blocky    (x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => Linear    (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => Blocky    (x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => Linear    (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => Blocky    (x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => Linear    (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => WithBlocky(x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => WithLinear(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => WithBlocky(x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => WithLinear(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => WithBlocky(x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => WithLinear(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => WithBlocky(x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => WithLinear(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => AsBlocky  (x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => AsLinear  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => AsBlocky  (x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => AsLinear  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => AsBlocky  (x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => AsLinear  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => AsBlocky  (x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => AsLinear  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => SetBlocky (x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => SetLinear (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => SetBlocky (x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => SetLinear (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => SetBlocky (x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => SetLinear (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => SetBlocky (x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => SetLinear (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.Blocky(x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.Linear(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.Blocky(x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.Linear(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Blocky(x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.Linear(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.Blocky(x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.Linear(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.WithBlocky(x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.WithLinear(x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.WithBlocky(x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.WithLinear(x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithBlocky(x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithLinear(x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.WithBlocky(x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.WithLinear(x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.AsBlocky  (x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.AsLinear  (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.AsBlocky  (x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.AsLinear  (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsBlocky  (x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsLinear  (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.AsBlocky  (x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.AsLinear  (x.TapeBound.TapeAction )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.SetBlocky (x.TapeBound.Tape       ));
                              if (value == Line ) AreEqual(x.TapeBound.Tape       , () => ConfigWishes.SetLinear (x.TapeBound.Tape       )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.SetBlocky (x.TapeBound.TapeConfig ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeConfig , () => ConfigWishes.SetLinear (x.TapeBound.TapeConfig )); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetBlocky (x.TapeBound.TapeActions));
                              if (value == Line ) AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetLinear (x.TapeBound.TapeActions)); });
            AssertProp(x => { if (value == Block) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.SetBlocky (x.TapeBound.TapeAction ));
                              if (value == Line ) AreEqual(x.TapeBound.TapeAction , () => ConfigWishes.SetLinear (x.TapeBound.TapeAction )); });
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

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Interpolation    (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithInterpolation(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsInterpolation  (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetInterpolation (value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => Interpolation    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithInterpolation(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => AsInterpolation  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetInterpolation (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.Interpolation    (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithInterpolation(x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.AsInterpolation  (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetInterpolation (x.Independent.Sample, value, x.SynthBound.Context)));
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Blocky    (x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Linear    (x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithBlocky(x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithLinear(x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsBlocky  (x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsLinear  (x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetBlocky (x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetLinear (x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => Blocky    (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => Linear    (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => WithBlocky(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => WithLinear(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => AsBlocky  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => AsLinear  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => SetBlocky (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => SetLinear (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => ConfigWishes.Blocky    (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => ConfigWishes.Linear    (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => ConfigWishes.WithBlocky(x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => ConfigWishes.WithLinear(x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => ConfigWishes.AsBlocky  (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => ConfigWishes.AsLinear  (x.Independent.Sample, x.SynthBound.Context)); });
                AssertProp(() => { if (value == Block) AreEqual(x.Independent.Sample, () => ConfigWishes.SetBlocky (x.Independent.Sample, x.SynthBound.Context));
                                   if (value == Line ) AreEqual(x.Independent.Sample, () => ConfigWishes.SetLinear (x.Independent.Sample, x.SynthBound.Context)); });
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_Interpolation(int initAsInt, int valueAsInt)
        {
            var init  = (InterpolationTypeEnum)initAsInt;
            var value = (InterpolationTypeEnum)valueAsInt;
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

                AssertProp(() => x.Immutable.Interpolation.Interpolation    (value));
                AssertProp(() => x.Immutable.Interpolation.WithInterpolation(value));
                AssertProp(() => x.Immutable.Interpolation.AsInterpolation  (value));
                AssertProp(() => x.Immutable.Interpolation.ToInterpolation  (value));
                AssertProp(() => x.Immutable.Interpolation.SetInterpolation (value));
                AssertProp(() => Interpolation    (x.Immutable.Interpolation, value));
                AssertProp(() => WithInterpolation(x.Immutable.Interpolation, value));
                AssertProp(() => AsInterpolation  (x.Immutable.Interpolation, value));
                AssertProp(() => ToInterpolation  (x.Immutable.Interpolation, value));
                AssertProp(() => SetInterpolation (x.Immutable.Interpolation, value));
                AssertProp(() => ConfigWishes.Interpolation    (x.Immutable.Interpolation, value));
                AssertProp(() => ConfigWishes.WithInterpolation(x.Immutable.Interpolation, value));
                AssertProp(() => ConfigWishes.AsInterpolation  (x.Immutable.Interpolation, value));
                AssertProp(() => ConfigWishes.ToInterpolation  (x.Immutable.Interpolation, value));
                AssertProp(() => ConfigWishes.SetInterpolation (x.Immutable.Interpolation, value));
                AssertProp(() => value == Block ? x.Immutable.Interpolation.Blocky    () : x.Immutable.Interpolation.Linear    ());
                AssertProp(() => value == Block ? x.Immutable.Interpolation.WithBlocky() : x.Immutable.Interpolation.WithLinear());
                AssertProp(() => value == Block ? x.Immutable.Interpolation.AsBlocky  () : x.Immutable.Interpolation.AsLinear  ());
                AssertProp(() => value == Block ? x.Immutable.Interpolation.ToBlocky  () : x.Immutable.Interpolation.ToLinear  ());
                AssertProp(() => value == Block ? x.Immutable.Interpolation.SetBlocky () : x.Immutable.Interpolation.SetLinear ());
                AssertProp(() => value == Block ? Blocky    (x.Immutable.Interpolation) : Linear    (x.Immutable.Interpolation));
                AssertProp(() => value == Block ? WithBlocky(x.Immutable.Interpolation) : WithLinear(x.Immutable.Interpolation));
                AssertProp(() => value == Block ? AsBlocky  (x.Immutable.Interpolation) : AsLinear  (x.Immutable.Interpolation));
                AssertProp(() => value == Block ? ToBlocky  (x.Immutable.Interpolation) : ToLinear  (x.Immutable.Interpolation));
                AssertProp(() => value == Block ? SetBlocky (x.Immutable.Interpolation) : SetLinear (x.Immutable.Interpolation));
                AssertProp(() => value == Block ? ConfigWishes.Blocky    (x.Immutable.Interpolation) : ConfigWishes.Linear    (x.Immutable.Interpolation));
                AssertProp(() => value == Block ? ConfigWishes.WithBlocky(x.Immutable.Interpolation) : ConfigWishes.WithLinear(x.Immutable.Interpolation));
                AssertProp(() => value == Block ? ConfigWishes.AsBlocky  (x.Immutable.Interpolation) : ConfigWishes.AsLinear  (x.Immutable.Interpolation));
                AssertProp(() => value == Block ? ConfigWishes.ToBlocky  (x.Immutable.Interpolation) : ConfigWishes.ToLinear  (x.Immutable.Interpolation));
                AssertProp(() => value == Block ? ConfigWishes.SetBlocky (x.Immutable.Interpolation) : ConfigWishes.SetLinear (x.Immutable.Interpolation));
                AssertProp(() => value.Interpolation());
            }

            // InterpolationType Entity
            
            var interpolationEntities = new List<InterpolationType>();
            {
                InterpolationType entity = x.Immutable.InterpolationEntity;
                IContext context = x.SynthBound.Context;
                
                void AssertProp(Func<InterpolationType> setter)
                {
                    Assert_Independent_Getters(entity, init);

                    InterpolationType entity2 = setter();
                    
                    Assert_Independent_Getters(entity, init);
                    Assert_Independent_Getters(entity2, value);
                    
                    interpolationEntities.Add(entity2);
                }
                
                AssertProp(() => entity.Interpolation    (value, context));
                AssertProp(() => entity.WithInterpolation(value, context));
                AssertProp(() => entity.AsInterpolation  (value, context));
                AssertProp(() => entity.ToInterpolation  (value, context));
                AssertProp(() => entity.SetInterpolation (value, context));
                AssertProp(() => value.InterpolationEnumToEntity(context));
                AssertProp(() => value.InterpolationEnumAsEntity(context));
                AssertProp(() => value.ToEntity                 (context));
                AssertProp(() => value.AsEntity                 (context));
                AssertProp(() => value.EnumToEntity             (context));
                AssertProp(() => value.EnumAsEntity             (context));
                AssertProp(() => Interpolation    (entity, value, context));
                AssertProp(() => WithInterpolation(entity, value, context));
                AssertProp(() => AsInterpolation  (entity, value, context));
                AssertProp(() => ToInterpolation  (entity, value, context));
                AssertProp(() => SetInterpolation (entity, value, context));
                AssertProp(() => InterpolationEnumToEntity(value, context));
                AssertProp(() => InterpolationEnumAsEntity(value, context));
                AssertProp(() => ToEntity                 (value, context));
                AssertProp(() => AsEntity                 (value, context));
                AssertProp(() => EnumToEntity             (value, context));
                AssertProp(() => EnumAsEntity             (value, context));
                AssertProp(() => ConfigWishes.Interpolation    (entity, value, context));
                AssertProp(() => ConfigWishes.WithInterpolation(entity, value, context));
                AssertProp(() => ConfigWishes.AsInterpolation  (entity, value, context));
                AssertProp(() => ConfigWishes.ToInterpolation  (entity, value, context));
                AssertProp(() => ConfigWishes.SetInterpolation (entity, value, context));
                AssertProp(() => ConfigWishes.InterpolationEnumToEntity(value, context));
                AssertProp(() => ConfigWishes.InterpolationEnumAsEntity(value, context));
                AssertProp(() => ConfigWishes.ToEntity                 (value, context));
                AssertProp(() => ConfigWishes.AsEntity                 (value, context));
                AssertProp(() => ConfigWishes.EnumToEntity             (value, context));
                AssertProp(() => ConfigWishes.EnumAsEntity             (value, context));
                AssertProp(() => value == Block ? entity.Blocky    (context) : entity.Linear    (context));
                AssertProp(() => value == Block ? entity.WithBlocky(context) : entity.WithLinear(context));
                AssertProp(() => value == Block ? entity.AsBlocky  (context) : entity.AsLinear  (context));
                AssertProp(() => value == Block ? entity.ToBlocky  (context) : entity.ToLinear  (context));
                AssertProp(() => value == Block ? entity.SetBlocky (context) : entity.SetLinear (context));
                AssertProp(() => value == Block ? Blocky    (entity, context) : Linear    (entity, context));
                AssertProp(() => value == Block ? WithBlocky(entity, context) : WithLinear(entity, context));
                AssertProp(() => value == Block ? AsBlocky  (entity, context) : AsLinear  (entity, context));
                AssertProp(() => value == Block ? ToBlocky  (entity, context) : ToLinear  (entity, context));
                AssertProp(() => value == Block ? SetBlocky (entity, context) : SetLinear (entity, context));
                AssertProp(() => value == Block ? ConfigWishes.Blocky    (entity, context) : ConfigWishes.Linear    (entity, context));
                AssertProp(() => value == Block ? ConfigWishes.WithBlocky(entity, context) : ConfigWishes.WithLinear(entity, context));
                AssertProp(() => value == Block ? ConfigWishes.AsBlocky  (entity, context) : ConfigWishes.AsLinear  (entity, context));
                AssertProp(() => value == Block ? ConfigWishes.ToBlocky  (entity, context) : ConfigWishes.ToLinear  (entity, context));
                AssertProp(() => value == Block ? ConfigWishes.SetBlocky (entity, context) : ConfigWishes.SetLinear (entity, context));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            interpolations       .ForEach(e => Assert_Independent_Getters(e, value));
            interpolationEntities.ForEach(s => Assert_Independent_Getters(s, value));
        }

        [TestMethod]
        public void ConfigSection_Interpolation()
        {
            var x = CreateTestEntities(default);
            AreEqual(DefaultInterpolation,          () => x.SynthBound.ConfigSection.Interpolation);
            AreEqual(DefaultInterpolation,          () => x.SynthBound.ConfigSection.Interpolation());
            AreEqual(DefaultInterpolation,          () => x.SynthBound.ConfigSection.GetInterpolation());
            AreEqual(DefaultInterpolation == Block, () => x.SynthBound.ConfigSection.IsBlocky());
            AreEqual(DefaultInterpolation == Line,  () => x.SynthBound.ConfigSection.IsLinear());
        }

        [TestMethod]
        public void Default_Interpolation()
        {
            AreEqual(Line, () => DefaultInterpolation);
        }

        // Getter Helpers
        
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
            AreEqual(interpolation,          () => x.SynthBound.SynthWishes   .GetInterpolation);
            AreEqual(interpolation,          () => x.SynthBound.FlowNode      .GetInterpolation);
            AreEqual(interpolation,          () => x.SynthBound.ConfigResolver.GetInterpolation);
            AreEqual(interpolation == Block, () => x.SynthBound.SynthWishes   .IsBlocky        );
            AreEqual(interpolation == Block, () => x.SynthBound.FlowNode      .IsBlocky        );
            AreEqual(interpolation == Block, () => x.SynthBound.ConfigResolver.IsBlocky        );
            AreEqual(interpolation == Line,  () => x.SynthBound.FlowNode      .IsLinear        );
            AreEqual(interpolation == Line,  () => x.SynthBound.SynthWishes   .IsLinear        );
            AreEqual(interpolation == Line,  () => x.SynthBound.ConfigResolver.IsLinear        );
            AreEqual(interpolation,          () => x.SynthBound.SynthWishes   .Interpolation   ());
            AreEqual(interpolation,          () => x.SynthBound.FlowNode      .Interpolation   ());
            AreEqual(interpolation,          () => x.SynthBound.ConfigResolver.Interpolation   ());
            AreEqual(interpolation,          () => x.SynthBound.SynthWishes   .GetInterpolation());
            AreEqual(interpolation,          () => x.SynthBound.FlowNode      .GetInterpolation());
            AreEqual(interpolation,          () => x.SynthBound.ConfigResolver.GetInterpolation());
            AreEqual(interpolation == Block, () => x.SynthBound.SynthWishes   .IsBlocky        ());
            AreEqual(interpolation == Block, () => x.SynthBound.FlowNode      .IsBlocky        ());
            AreEqual(interpolation == Block, () => x.SynthBound.ConfigResolver.IsBlocky        ());
            AreEqual(interpolation == Line,  () => x.SynthBound.SynthWishes   .IsLinear        ());
            AreEqual(interpolation == Line,  () => x.SynthBound.FlowNode      .IsLinear        ());
            AreEqual(interpolation == Line,  () => x.SynthBound.ConfigResolver.IsLinear        ());
            AreEqual(interpolation,          () => Interpolation   (x.SynthBound.SynthWishes   ));
            AreEqual(interpolation,          () => Interpolation   (x.SynthBound.FlowNode      ));
            AreEqual(interpolation,          () => Interpolation   (x.SynthBound.ConfigResolver));
            AreEqual(interpolation,          () => GetInterpolation(x.SynthBound.SynthWishes   ));
            AreEqual(interpolation,          () => GetInterpolation(x.SynthBound.FlowNode      ));
            AreEqual(interpolation,          () => GetInterpolation(x.SynthBound.ConfigResolver));
            AreEqual(interpolation == Block, () => IsBlocky        (x.SynthBound.SynthWishes   ));
            AreEqual(interpolation == Block, () => IsBlocky        (x.SynthBound.FlowNode      ));
            AreEqual(interpolation == Block, () => IsBlocky        (x.SynthBound.ConfigResolver));
            AreEqual(interpolation == Line,  () => IsLinear        (x.SynthBound.SynthWishes   ));
            AreEqual(interpolation == Line,  () => IsLinear        (x.SynthBound.FlowNode      ));
            AreEqual(interpolation == Line,  () => IsLinear        (x.SynthBound.ConfigResolver));
            AreEqual(interpolation,          () => ConfigWishes        .Interpolation   (x.SynthBound.SynthWishes   ));
            AreEqual(interpolation,          () => ConfigWishes        .Interpolation   (x.SynthBound.FlowNode      ));
            AreEqual(interpolation,          () => ConfigWishesAccessor.Interpolation   (x.SynthBound.ConfigResolver));
            AreEqual(interpolation,          () => ConfigWishes        .GetInterpolation(x.SynthBound.SynthWishes   ));
            AreEqual(interpolation,          () => ConfigWishes        .GetInterpolation(x.SynthBound.FlowNode      ));
            AreEqual(interpolation,          () => ConfigWishesAccessor.GetInterpolation(x.SynthBound.ConfigResolver));
            AreEqual(interpolation == Block, () => ConfigWishes        .IsBlocky        (x.SynthBound.SynthWishes   ));
            AreEqual(interpolation == Block, () => ConfigWishes        .IsBlocky        (x.SynthBound.FlowNode      ));
            AreEqual(interpolation == Block, () => ConfigWishesAccessor.IsBlocky        (x.SynthBound.ConfigResolver));
            AreEqual(interpolation == Line,  () => ConfigWishes        .IsLinear        (x.SynthBound.SynthWishes   ));
            AreEqual(interpolation == Line,  () => ConfigWishes        .IsLinear        (x.SynthBound.FlowNode      ));
            AreEqual(interpolation == Line,  () => ConfigWishesAccessor.IsLinear        (x.SynthBound.ConfigResolver));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, InterpolationTypeEnum interpolation)
        {
            AreEqual(interpolation, () => x.TapeBound.TapeConfig.Interpolation);

            AreEqual(interpolation, () => x.TapeBound.Tape       .Interpolation   ());
            AreEqual(interpolation, () => x.TapeBound.TapeConfig .Interpolation   ());
            AreEqual(interpolation, () => x.TapeBound.TapeActions.Interpolation   ());
            AreEqual(interpolation, () => x.TapeBound.TapeAction .Interpolation   ());
            AreEqual(interpolation, () => x.TapeBound.Tape       .GetInterpolation());
            AreEqual(interpolation, () => x.TapeBound.TapeConfig .GetInterpolation());
            AreEqual(interpolation, () => x.TapeBound.TapeActions.GetInterpolation());
            AreEqual(interpolation, () => x.TapeBound.TapeAction .GetInterpolation());
            AreEqual(interpolation == Block, () => x.TapeBound.Tape       .IsBlocky());
            AreEqual(interpolation == Block, () => x.TapeBound.TapeConfig .IsBlocky());
            AreEqual(interpolation == Block, () => x.TapeBound.TapeActions.IsBlocky());
            AreEqual(interpolation == Block, () => x.TapeBound.TapeAction .IsBlocky());
            AreEqual(interpolation == Line,  () => x.TapeBound.Tape       .IsLinear());
            AreEqual(interpolation == Line,  () => x.TapeBound.TapeConfig .IsLinear());
            AreEqual(interpolation == Line,  () => x.TapeBound.TapeActions.IsLinear());
            AreEqual(interpolation == Line,  () => x.TapeBound.TapeAction .IsLinear());
            AreEqual(interpolation, () => Interpolation   (x.TapeBound.Tape       ));
            AreEqual(interpolation, () => Interpolation   (x.TapeBound.TapeConfig ));
            AreEqual(interpolation, () => Interpolation   (x.TapeBound.TapeActions));
            AreEqual(interpolation, () => Interpolation   (x.TapeBound.TapeAction ));
            AreEqual(interpolation, () => GetInterpolation(x.TapeBound.Tape       ));
            AreEqual(interpolation, () => GetInterpolation(x.TapeBound.TapeConfig ));
            AreEqual(interpolation, () => GetInterpolation(x.TapeBound.TapeActions));
            AreEqual(interpolation, () => GetInterpolation(x.TapeBound.TapeAction ));
            AreEqual(interpolation == Block, () => IsBlocky(x.TapeBound.Tape       ));
            AreEqual(interpolation == Block, () => IsBlocky(x.TapeBound.TapeConfig ));
            AreEqual(interpolation == Block, () => IsBlocky(x.TapeBound.TapeActions));
            AreEqual(interpolation == Block, () => IsBlocky(x.TapeBound.TapeAction ));
            AreEqual(interpolation == Line,  () => IsLinear(x.TapeBound.Tape       ));
            AreEqual(interpolation == Line,  () => IsLinear(x.TapeBound.TapeConfig ));
            AreEqual(interpolation == Line,  () => IsLinear(x.TapeBound.TapeActions));
            AreEqual(interpolation == Line,  () => IsLinear(x.TapeBound.TapeAction ));
            AreEqual(interpolation, () => ConfigWishes.Interpolation   (x.TapeBound.Tape       ));
            AreEqual(interpolation, () => ConfigWishes.Interpolation   (x.TapeBound.TapeConfig ));
            AreEqual(interpolation, () => ConfigWishes.Interpolation   (x.TapeBound.TapeActions));
            AreEqual(interpolation, () => ConfigWishes.Interpolation   (x.TapeBound.TapeAction ));
            AreEqual(interpolation, () => ConfigWishes.GetInterpolation(x.TapeBound.Tape       ));
            AreEqual(interpolation, () => ConfigWishes.GetInterpolation(x.TapeBound.TapeConfig ));
            AreEqual(interpolation, () => ConfigWishes.GetInterpolation(x.TapeBound.TapeActions));
            AreEqual(interpolation, () => ConfigWishes.GetInterpolation(x.TapeBound.TapeAction ));
            AreEqual(interpolation == Block, () => ConfigWishes.IsBlocky(x.TapeBound.Tape       ));
            AreEqual(interpolation == Block, () => ConfigWishes.IsBlocky(x.TapeBound.TapeConfig ));
            AreEqual(interpolation == Block, () => ConfigWishes.IsBlocky(x.TapeBound.TapeActions));
            AreEqual(interpolation == Block, () => ConfigWishes.IsBlocky(x.TapeBound.TapeAction ));
            AreEqual(interpolation == Line,  () => ConfigWishes.IsLinear(x.TapeBound.Tape       ));
            AreEqual(interpolation == Line,  () => ConfigWishes.IsLinear(x.TapeBound.TapeConfig ));
            AreEqual(interpolation == Line,  () => ConfigWishes.IsLinear(x.TapeBound.TapeActions));
            AreEqual(interpolation == Line,  () => ConfigWishes.IsLinear(x.TapeBound.TapeAction ));
        }
        
        void Assert_Independent_Getters(Sample sample, InterpolationTypeEnum interpolation)
        {
            AreEqual(interpolation,          () => sample.Interpolation   ());
            AreEqual(interpolation,          () => sample.GetInterpolation());
            AreEqual(interpolation == Block, () => sample.IsBlocky        ());
            AreEqual(interpolation == Line,  () => sample.IsLinear        ());
            AreEqual(interpolation,          () => Interpolation   (sample));
            AreEqual(interpolation,          () => GetInterpolation(sample));
            AreEqual(interpolation == Block, () => IsBlocky        (sample));
            AreEqual(interpolation == Line,  () => IsLinear        (sample));
            AreEqual(interpolation,          () => ConfigWishes.Interpolation   (sample));
            AreEqual(interpolation,          () => ConfigWishes.GetInterpolation(sample));
            AreEqual(interpolation == Block, () => ConfigWishes.IsBlocky        (sample));
            AreEqual(interpolation == Line,  () => ConfigWishes.IsLinear        (sample));
        }
        
        void Assert_Independent_Getters(InterpolationTypeEnum interpolationTypeEnum, InterpolationTypeEnum interpolation)
        {
            AreEqual(interpolation,          () => interpolationTypeEnum.Interpolation   ());
            AreEqual(interpolation,          () => interpolationTypeEnum.GetInterpolation());
            AreEqual(interpolation == Block, () => interpolationTypeEnum.IsBlocky        ());
            AreEqual(interpolation == Line,  () => interpolationTypeEnum.IsLinear        ());
            AreEqual(interpolation,          () => Interpolation   (interpolationTypeEnum));
            AreEqual(interpolation,          () => GetInterpolation(interpolationTypeEnum));
            AreEqual(interpolation == Block, () => IsBlocky        (interpolationTypeEnum));
            AreEqual(interpolation == Line,  () => IsLinear        (interpolationTypeEnum));
            AreEqual(interpolation,          () => ConfigWishes.Interpolation   (interpolationTypeEnum));
            AreEqual(interpolation,          () => ConfigWishes.GetInterpolation(interpolationTypeEnum));
            AreEqual(interpolation == Block, () => ConfigWishes.IsBlocky        (interpolationTypeEnum));
            AreEqual(interpolation == Line,  () => ConfigWishes.IsLinear        (interpolationTypeEnum));
        }
        
        void Assert_Independent_Getters(InterpolationType interpolationType, InterpolationTypeEnum interpolation)
        {
            IsNotNull(() => interpolationType);
            AreEqual(interpolation,          () => interpolationType.Interpolation            ());
            AreEqual(interpolation,          () => interpolationType.GetInterpolation         ());
            AreEqual(interpolation,          () => interpolationType.AsInterpolation          ());
            AreEqual(interpolation,          () => interpolationType.ToInterpolation          ());
            AreEqual(interpolation,          () => interpolationType.InterpolationEntityToEnum());
            AreEqual(interpolation,          () => interpolationType.InterpolationEntityAsEnum());
            AreEqual(interpolation,          () => interpolationType.ToEnum                   ());
            AreEqual(interpolation,          () => interpolationType.AsEnum                   ());
            AreEqual(interpolation,          () => interpolationType.EntityAsEnum             ());
            AreEqual(interpolation,          () => interpolationType.EntityToEnum             ());
            AreEqual(interpolation == Block, () => interpolationType.IsBlocky                 ());
            AreEqual(interpolation == Line,  () => interpolationType.IsLinear                 ());
            AreEqual(interpolation,          () => Interpolation            (interpolationType));
            AreEqual(interpolation,          () => GetInterpolation         (interpolationType));
            AreEqual(interpolation,          () => AsInterpolation          (interpolationType));
            AreEqual(interpolation,          () => ToInterpolation          (interpolationType));
            AreEqual(interpolation,          () => InterpolationEntityToEnum(interpolationType));
            AreEqual(interpolation,          () => InterpolationEntityAsEnum(interpolationType));
            AreEqual(interpolation,          () => ToEnum                   (interpolationType));
            AreEqual(interpolation,          () => AsEnum                   (interpolationType));
            AreEqual(interpolation,          () => EntityAsEnum             (interpolationType));
            AreEqual(interpolation,          () => EntityToEnum             (interpolationType));
            AreEqual(interpolation == Block, () => IsBlocky                 (interpolationType));
            AreEqual(interpolation == Line,  () => IsLinear                 (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.Interpolation            (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.GetInterpolation         (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.AsInterpolation          (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.ToInterpolation          (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.InterpolationEntityToEnum(interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.InterpolationEntityAsEnum(interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.ToEnum                   (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.AsEnum                   (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.EntityAsEnum             (interpolationType));
            AreEqual(interpolation,          () => ConfigWishes.EntityToEnum             (interpolationType));
            AreEqual(interpolation == Block, () => ConfigWishes.IsBlocky                 (interpolationType));
            AreEqual(interpolation == Line,  () => ConfigWishes.IsLinear                 (interpolationType));

        }
 
        // Test Data Helpers
        
        private TestEntities CreateTestEntities(InterpolationTypeEnum? interpolation, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().WithInterpolation(interpolation).SamplingRate(HighPerfHz), name);
        
        static object TestParametersWithEmpty => new [] // ncrunch: no coverage
        {
            new object[] {  null         , (int)Block     },
            new object[] { (int)Block    ,  null          },
            new object[] { (int)0        , (int)Block     },
            new object[] { (int)Block    , (int)0         },
            new object[] { (int)Undefined, (int)Block     },
            new object[] { (int)Block    , (int)Undefined },
            new object[] { (int)Block    , (int)Line      },
            new object[] { (int)Line     , (int)Block     }
        };

        static object TestParameters => new [] // ncrunch: no coverage
        {
            new object[] { (int)Block, (int)Line },
            new object[] { (int)Line, (int)Block }
        };
   } 
} 
