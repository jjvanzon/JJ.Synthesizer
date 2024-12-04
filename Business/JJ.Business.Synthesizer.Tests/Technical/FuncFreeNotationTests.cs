using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JJ.Business.Synthesizer.Tests.Helpers;
// ReSharper disable PublicConstructorInAbstractClass
// ReSharper disable ArrangeStaticMemberQualifier
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable CheckNamespace

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.Run
{
    /// <summary> Not func free </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : MySynthWishes
    {
        [TestMethod]
        public void FuncFree_Run() => new MySound().Start();
        
        void Start() => Run(() => Sine(E4).Curve(RecorderCurve).Play());
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.MethodGroup
{
    /// <summary> Better </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : MySynthWishes
    {
        [TestMethod]
        public void FuncFree_MethodGroup() => new MySound().Run(Start);
        
        FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.ConstructorFunc
{
    /// <summary> Not better </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        public MySound() { }
        public MySound(Func<FlowNode> func) : base(func) { }
        
        [TestMethod]
        public void FuncFree_ConstructorFunc() => new MySound(Start);
        
        FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }

    public class Synth : MySynthWishes
    {
        public Synth() { }
        public Synth(Func<FlowNode> func) => Run(func, GetType().Name);
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.ConstructorFunc2
{
    /// <summary> Same </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        public MySound() { }
        public MySound(Func<FlowNode> func) : base(func) { }
        
        [TestMethod]
        public void FuncFree_ConstructorFunc2() => new Synth(Start);
        
        FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }

    public class Synth : MySynthWishes
    {
        public Synth() { }
        public Synth(Func<FlowNode> func) => Run(func, GetType().Name);
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.LocalFunc_ConstructorFunc
{
    /// <summary> Confusing </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        public MySound() { }
        public MySound(Func<FlowNode> func) : base(func) { }
        
        [TestMethod]
        public void FuncFree_LocalFunc_ConstructorFunc()
        {
            FlowNode MySynth()
            {
                return Sine(E4).Curve(RecorderCurve).Play();
            }
            
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
    /// <summary> It's magic </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        [TestMethod]
        public void FuncFree_VirtualStart() { }
        
        protected override FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }

    public class Synth : MySynthWishes
    {
        public Synth() => Run(Start, GetType().Name);
        
        protected virtual FlowNode Start() => null;
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.AbstractStart
{
    /// <summary> It's forceful magic </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        [TestMethod]
        public void FuncFree_AbstractStart() { }
        
        protected override FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }

    public abstract class Synth : MySynthWishes
    {
        public Synth() => Run(Start, GetType().Name);
        
        protected abstract FlowNode Start();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.StaticRun
{
    /// <summary>
    /// More terse.
    /// Context contention?
    /// </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        [TestMethod]
        public void FuncFree_StaticRun() => Synth.Run(Start);
        
        FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }

    public class Synth : MySynthWishes
    {
        public static void Run(Func<FlowNode> func) => new Synth().Run(func, func.Method.Name);
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.MoreLocals
{
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        [TestMethod]
        public void FuncFree_MoreLocals() => Synth.Run(Start);

        FlowNode Start()
        {
            var recorderCurve = Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
            
            var flow = Sine(E4).Curve(recorderCurve).Play();
            
            return flow;
        }
    }

    public class Synth : MySynthWishes
    {
        public static void Run(Func<FlowNode> func) => new Synth().Run(func, func.Method.Name);

    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.LocalProblems
{
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        [TestMethod]
        public void FuncFree_LocalProblems() => Synth.Run(Start);

        FlowNode Start()
        {
            var recorderCurve = Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
            
            // Problem: Might not run, because it's not connected to the flow.
            Sine(G4).Curve(recorderCurve).Play();
            // Actually it registers a tape, so it might still run.
            // So why am I returning a flow node here?
            // Is that legacy that I do not even need?
            
            var flow = Sine(E4).Curve(recorderCurve).Play();
            
            return flow;
        }
    }

    public class Synth : MySynthWishes
    {
        public static void Run(Func<FlowNode> func) => new Synth().Run(func, func.Method.Name);
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.StaticRun2
{
    /// <summary>
    /// Cool.
    /// Takes no main flow node.
    /// Will it work?
    /// Context contention?
    /// </summary>
    [TestClass]
    [TestCategory("Technical")]
    public class MySound : Synth
    {
        [TestMethod]
        public void FuncFree_StaticRun2() => Synth.Run(Start);
        
        void Start() => Sine(E4).Curve(RecorderCurve).Play();
    }

    public class Synth : MySynthWishes
    {
        public static void Run(Action action) => new Synth().Run(() => { action(); return null; }, action.Method.Name);
    }
}
