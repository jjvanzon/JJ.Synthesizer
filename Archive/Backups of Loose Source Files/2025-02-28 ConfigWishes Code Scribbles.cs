
            AssertProp(x => { if (val == Block) AreEqual(x.SynthWishes,    () => InterpolationExtensionWishes        .WithBlocky       (x.SynthWishes        ));
                              if (val == Line ) AreEqual(x.SynthWishes,    () => InterpolationExtensionWishes        .WithLinear       (x.SynthWishes        ));
                              if (!Has(val)   ) AreEqual(x.SynthWishes,          InterpolationExtensionWishes        .WithInterpolation(x.SynthWishes   , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.FlowNode,       () => InterpolationExtensionWishes        .WithBlocky       (x.FlowNode           ));
                              if (val == Line ) AreEqual(x.FlowNode,       () => InterpolationExtensionWishes        .WithLinear       (x.FlowNode           ));
                              if (!Has(val)   ) AreEqual(x.FlowNode,             InterpolationExtensionWishes        .WithInterpolation(x.FlowNode      , val)); });
            AssertProp(x => { if (val == Block) AreEqual(x.ConfigResolver, () => InterpolationExtensionWishesAccessor.WithBlocky       (x.ConfigResolver     ));
                              if (val == Line ) AreEqual(x.ConfigResolver, () => InterpolationExtensionWishesAccessor.WithLinear       (x.ConfigResolver     ));
                              if (!Has(val)   ) AreEqual(x.ConfigResolver,       InterpolationExtensionWishesAccessor.WithInterpolation(x.ConfigResolver, val)); });

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SamplingRateExtensionWishes.WithSamplingRate(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SamplingRateExtensionWishes.WithSamplingRate(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SamplingRateExtensionWishesAccessor.WithSamplingRate(x.SynthBound.ConfigResolver, value)));
