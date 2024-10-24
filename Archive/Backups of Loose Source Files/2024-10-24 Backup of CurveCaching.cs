SynthWishesAccessor:


        //public CurveInWrapper GetOrCreateCurveIn(string name, Func<FluentOutlet> func)
        //{
        //    return (CurveInWrapper)_accessor.InvokeMethod(nameof(GetOrCreateCurveIn), name, func);
        //}

CurveWishesTests:

        //[TestMethod]
        //public void CurveWishes_SynthesizerSugar_GetCurve() 
        //    => new CurveWishesTests().CurveWishes_SynthesizerSugar_GetCurve_RunTest();

        //void CurveWishes_SynthesizerSugar_GetCurve_RunTest()
        //{
        //    // Arrange
        //    var curve1_cached = Curve("Curve1", (0, 1), (1, 0));
        //    var curve2_cached = Curve("Curve2", (0, 0), (0.5, 1), (1, 0));

        //    // Act
        //    var curve1_reused = GetCurve("Curve1");
        //    var curve2_reused = GetCurve("Curve2");

        //    // Assert
        //    AssertHelper.AreEqual(curve1_cached, () => curve1_reused);
        //    AssertHelper.AreEqual(curve2_cached, () => curve2_reused);
            
        //    // Diagnostics
        //    SaveAudioMono(() => curve1_cached, fileName: Name() + "_Curve1.wav");
        //    SaveAudioMono(() => curve2_cached, fileName: Name() + "_Curve2.wav");
        //}

CurveWishes.SynthWishes:

        // Curve Caching

        //public FluentOutlet GetCurve(string name)
        //{
        //    lock (_curveLock)
        //    {
        //        if (_curveDictionary.TryGetValue(name, out FluentOutlet fluentOutlet))
        //        {
        //            return fluentOutlet;

        //        }

        //        throw new Exception($"{nameof(Persistence.Synthesizer.Curve)} with {nameof(name)} '{name}' not found.");
        //    }
        //}

        //private readonly object _curveLock = new object();

        //private readonly Dictionary<string, FluentOutlet> _curveDictionary =
        //    new Dictionary<string, FluentOutlet>();

        ///// <inheritdoc cref="docs._createcurve" />
        //internal FluentOutlet GetOrCreateCurveIn(string name, Func<CurveInWrapper> func)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        throw new Exception(
        //            "Cache key could not be resolved from context. " +
        //            "Consider explicitly specifying the name parameter.");
        //    }

        //    lock (_curveLock)
        //    {
        //        if (_curveDictionary.TryGetValue(name, out FluentOutlet curveOutlet))
        //        {
        //            Console.WriteLine($"Curve {name} reused");

        //            return curveOutlet;
        //        }


        //        CurveInWrapper wrapper = func();

        //        // Assign names
        //        {
        //            curveOutlet = _[wrapper];
        //            curveOutlet.Outlet.Operator.Name = name;
        //            wrapper.Curve.Name = name;
        //        }

        //        _curveDictionary[name] = curveOutlet;

        //        Console.WriteLine($"Curve {name} cached");

        //        return curveOutlet;
        //    }
        //}
