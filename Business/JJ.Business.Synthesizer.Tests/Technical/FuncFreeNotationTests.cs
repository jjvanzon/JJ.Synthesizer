using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable ArrangeStaticMemberQualifier
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable CheckNamespace
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.Run
{
    /// <summary> ✔️ Not func free. But it works. </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : MySynthWishes
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void WithFunc() => new FuncFree().Start();
        
        void Start() => Run(() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play());
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.MethodGroup
{
    /// <summary> ❌ No sound / instance contention. Though looks better. </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : MySynthWishes
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void MethodGroup() => new FuncFree().Run(Start);
        
        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.InstanceRun
{
    /// <summary> ➖ No context isolation. Looks OK. </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : MySynthWishes
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void InstanceRun() => Run(Start);
        
        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.ConstructorFunc
{
    /// <summary> ❌ No sound / instance contention. But looks ok. </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : Synth
    {
        public FuncFree()=> WithStereo();
        public FuncFree(Func<FlowNode> func) : base(func) => WithStereo();
        
        [TestMethod]
        public void ConstructorFunc() => new FuncFree(Start);
        
        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
    }

    public class Synth : MySynthWishes
    {
        public Synth() { }
        public Synth(Func<FlowNode> func) => Run(func, GetType().Name);
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.LocalFunc_ConstructorFunc
{
    /// <summary> ❌ Confusing </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : Synth
    {
        public FuncFree() => WithStereo();
        public FuncFree(Func<FlowNode> func) => WithStereo();
        
        [TestMethod]
        public void LocalFunc_ConstructorFunc()
        {
            FlowNode MySynth() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
            
            new Synth(MySynth);
        }
    }

    public class Synth : MySynthWishes
    {
        public Synth() { }
        public Synth(Func<FlowNode> func) => Run(func, GetType().Name);
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.VirtualStart
{
    /// <summary> ❌ Constructor not run on time. Only one test per class. But concise magic! </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : Synth
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void VirtualStart() { }
        
        protected override FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
    }

    public class Synth : MySynthWishes
    {
        public Synth() => Run(Start, GetType().Name);
        
        protected virtual FlowNode Start() => null;
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.AbstractStart
{
    /// <summary> ❌ Constructor not run on time. Only one test per class. But forceful concise magic! </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : Synth
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void AbstractStart() { }
        
        protected override FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
    }

    public abstract class Synth : MySynthWishes
    {
        public Synth() => Run(Start, GetType().Name);
        
        protected abstract FlowNode Start();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.StaticRun
{
    /// <summary> ❌ No sound / instance contention. But more terse. </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : Synth
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void StaticRun() => Synth.Run(Start);
        
        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
    }

    public class Synth : MySynthWishes
    {
        public static void Run(Func<FlowNode> func) => new Synth().Run(func, func.Method.Name);
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.NoMainFlowNode
{
    /// <summary> ➖ No context isolation. Looks cool though; no main flow node. </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : MySynthWishes2
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void NoMainFlowNode() => Run(Start);
        
        void Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.MoreLocals
{
    /// <summary> Sandbox </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class FuncFree : MySynthWishes2
    {
        public FuncFree() => WithStereo();
        
        [TestMethod]
        public void MoreLocals() => Run(Start);

        void Start()
        {
            var curve = Curve((0, 0), (0.2, 0), (0.3, 1),  (0.7, 1), (0.8, 0), (1.0, 0));
            
            Sine(E4).Curve(curve).Panbrello(3).Play("Note1");
            Sine(G4).Curve(curve).Panbrello(5).Play("Note2");
        }
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation
{
    public class MySynthWishes2 : MySynthWishes
    {
        public void Run(Action action)
        {
            Run(
                () =>
                {
                    /// <summary> HACK: To make tape runner run all tapes without an explicit root node specified. </summary>
                    action();
                    var accessor = new SynthWishesAccessor(this);
                    var tapeSignals = accessor._tapes.GetAll().Select(x => x.Signal).ToArray();
                    return Add(tapeSignals);
                },
                action.Method.Name
            );
        }
    }
}
