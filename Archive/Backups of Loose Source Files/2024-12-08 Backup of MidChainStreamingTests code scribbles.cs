        FlowNode[] StereoDynamicsL { get; }
        FlowNode[] StereoDynamicsR { get; }

            
            StereoDynamics = new[]
            {
                StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic,
                StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic,
                StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic,
                StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic, StereoDynamic
            };


        //[TestMethod]
        //public void Stereo_WentPerChannel_Test() => Run(Stereo_WentPerChannel);
        //void Stereo_WentPerChannel()
        //{
        //    WithStereo();
            
        //    Add
        //    (
        //        Sine(RandomNotes[0] * 1).Volume(1.0).Volume(DynamicCurve).Panning(0.2).PlayChannel(),
        //        Sine(RandomNotes[0] * 2).Volume(0.3).Volume(DynamicCurve).Panning(0.8).PlayChannel()
        //    ).Play();

        //    Add
        //    (
        //        1.0 * Sine(RandomNotes[1] * 1).Volume(DynamicCurve).Panbrello(3.000, 0.2).PlayChannel(),
        //        0.2 * Sine(RandomNotes[1] * 2).Volume(DynamicCurve).Panbrello(5.234, 0.3).PlayChannel(),
        //        0.3 * Sine(RandomNotes[1] * 3).Volume(DynamicCurve).Panbrello(7.000, 0.2).PlayChannel()
        //    ).Play();
        //}
        