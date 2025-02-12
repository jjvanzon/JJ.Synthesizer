2025-02-12 FrameCountWishesTests 2 Archive:

                
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioLength(testCase.AudioLength, x.SynthBound.SynthWishes)));

                AssertProp(x => AreEqual(x.TapeBound.Tape,              x.TapeBound.Tape       .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,        x.TapeBound.TapeConfig .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions,       x.TapeBound.TapeActions.AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,        x.TapeBound.TapeAction .AudioLength(testCase.AudioLength)));

                AssertProp(x => AreEqual(x.TapeBound.Tape,              x.TapeBound.Tape       .SamplingRate  (testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,        x.TapeBound.TapeConfig .SamplingRate  (testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions,       x.TapeBound.TapeActions.SamplingRate  (testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,        x.TapeBound.TapeAction .SamplingRate  (testCase.SamplingRate)));

                AssertProp(x => AreEqual(x.TapeBound.Tape,              x.TapeBound.Tape       .CourtesyFrames  (testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,        x.TapeBound.TapeConfig .CourtesyFrames  (testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions,       x.TapeBound.TapeActions.CourtesyFrames  (testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,        x.TapeBound.TapeAction .CourtesyFrames  (testCase.CourtesyFrames)));

                AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.AudioLength(testCase.AudioLength)));

                AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SamplingRate  (testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SamplingRate  (testCase.SamplingRate)));

            public CaseProp<int> Frames => this;
            public CaseProp<double> Len         { get => AudioLength; set => AudioLength = value; }
            public CaseProp<double> Duration    { get => AudioLength; set => AudioLength = value; }
            public CaseProp<double> seconds     { get => AudioLength; set => AudioLength = value; }


            AreEqual(test.FrameCount.Coalesced, FrameCountFromAudioLength(test.AudioLength, test.SamplingRate, test.CourtesyFrames), delta: 1);

            //AreEqual(test.FrameCount.Coalesced, () => FrameCountFromAudioLength(test.AudioLength, test.SamplingRate, test.CourtesyFrames), Tolerance);
            //AreEqual(test.FrameCount.Coalesced, FrameCountFromAudioLength(test.AudioLength, test.SamplingRate, test.CourtesyFrames), Tolerance);

            Case   test = Cases[caseKey];
            double len  = test.AudioLength;
            int    Hz   = test.SamplingRate;
            int    plus = test.CourtesyFrames;
            int    frameCount;
            
            frameCount = FrameCountFromAudioLength(len, Hz, plus); AreEqual(test.FrameCount, () => frameCount, delta: -1);
            frameCount = GetFrameCount            (len, Hz, plus); AreEqual(test.FrameCount, () => frameCount, delta: -1);
            frameCount = ToFrameCount             (len, Hz, plus); AreEqual(test.FrameCount, () => frameCount, delta: -1);
            frameCount = FrameCount               (len, Hz, plus); AreEqual(test.FrameCount, () => frameCount, delta: -1);
