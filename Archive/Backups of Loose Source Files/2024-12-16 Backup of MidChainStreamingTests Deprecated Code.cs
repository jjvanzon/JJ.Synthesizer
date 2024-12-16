
        // Problems
        
        //[TestMethod]
        //public void Stereo_RecombineChannelsExplicit_Test() => new MidChainStreamingTests().Stereo_RecombineChannelsExplicit();
        //void Stereo_RecombineChannelsExplicit() 
        //{
        //    WithStereo();
        //    WithAudioLength(0.7);
        //
        //    var buffs = new Buff[2];
        //
        //    // The delegate creates a non-trivial convergence point.
        //  
        //    Save(() => Sine(RandomNotes[1]).Panning(0.1).Volume(StereoDynamics * DelayedPulse).InterceptChannel((b, i) => buffs[i] = b)).Play();
        //  
        //    IsNotNull(() => buffs[0]);
        //    IsNotNull(() => buffs[1]);
        //  
        //    buffs[0].Save().Play(); Sleep(1000);
        //    buffs[1].Save().Play();
        //  
        //    // Recombination can only be done after running all tapes.
        // 
        //    Save(() => Sample(buffs[0]).Panning(0) +
        //               Sample(buffs[1]).Panning(1)).Play();
        //}
        
        //[TestMethod]
        //public void Stereo_RecombineChannelsExplicitly_ShortTest() => new MidChainStreamingTests().Stereo_RecombineChannelsExplicitly_Short();
        //void Stereo_RecombineChannelsExplicitly_Short() 
        //{
        //    WithStereo();
        //  
        //    var buffs = new Buff[2];
        //  
        //    Save(() => Sine(RandomNotes[2]).Panning(0.9).Volume(StereoDynamics * DelayedPulse).InterceptChannel((b, i) => buffs[i] = b)).Play();
        //  
        //    Save(() => Sample(buffs[0]).Panning(0) +
        //               Sample(buffs[1]).Panning(1)).Play();
        //}
