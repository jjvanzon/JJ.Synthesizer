Code Scribbles ByteCountWishesTests:

    //public CaseProp<int>                 FrameSize      { get; set; }
    //public CaseProp<AudioFileFormatEnum> AudioFormat    { get; set; }

    //if (testCase.FrameSize.From != testCase.FrameSize.To)
    //{
    //    AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FrameSize(testCase.FrameSize.To)));
    //    AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FrameSize(testCase.FrameSize.To)));
    //    AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FrameSize(testCase.FrameSize.To)));
    //}

        
        private IList<PropertyInfo> GetCasePropInfos()
            => GetType().GetProperties(BINDING_FLAGS_ALL)
                        .Where(x => x.PropertyType.HasInterfaceRecursive<ICaseProp>())
                        .ToArray();


        //static object SynthBoundCases => SimpleCases.Concat(DependencyCases);
        //[DynamicData(nameof(SynthBoundCases))]

                //x.SynthBound.SynthWishes.CourtesyFrames(test.CourtesyFrames.Val.Nully);
                //AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .CourtesyFrames(test.CourtesyFrames.Val.Nully).ByteCount(value)));
                //AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .CourtesyFrames(test.CourtesyFrames.Val.Nully).ByteCount(value)));
                //AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.CourtesyFrames(test.CourtesyFrames.Val.Nully).ByteCount(value, x.SynthBound.SynthWishes)));



                //Assert_All_Getters(x, init, test.SizeOfBitDepth.Init, x.SynthBound.ConfigResolver.CourtesyFrames());
                //Assert_BuffBound_Getters  (x, init, x.SynthBound.ConfigResolver.CourtesyFrames());
                //Assert_All_Getters(x, value, test.SizeOfBitDepth.Init, x.SynthBound.ConfigResolver.CourtesyFrames());

            //if (test.CourtesyFrames.From == test.CourtesyFrames.To)
            {
                //AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .ByteCount(value)));
                //AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .ByteCount(value)));
                //AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.ByteCount(value, x.SynthBound.SynthWishes)));
            }

            //if (Has(x.Immutable.SampleDataTypeEnum)) // TODO: Necessary? Isn't SampleDataTypeEnum Undefined a valid value?


                
                // SamplingRate doesn't actually change the ByteCount here.
                // It just changes the SamplingRate field; the SampleCount stays untouched.
                //if (testCase.SamplingRate.Changed)
                //    AssertProp(() => x.Immutable.WavHeader.SamplingRate(testCase.SamplingRate.To));
                
                if (testCase.SizeOfBitDepth.Changed) 
                    AssertProp(() => x.Immutable.WavHeader.SizeOfBitDepth(testCase.SizeOfBitDepth.To));
