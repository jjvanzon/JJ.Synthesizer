using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
// ReSharper disable ObjectCreationAsStatement
// ReSharper disable CheckNamespace

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation1
{
    public class Synth : SynthWishes
    { }

    [TestClass]
    public class MySound : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        
        [TestMethod]
        public void FuncFreeTest1() => new MySound().FuncFree();
        void FuncFree() => Run(() => Sine(E4).Curve(RecorderCurve).Play());
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation2
{
    public class Synth : SynthWishes
    {
    }

    [TestClass]
    public class MySound : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));

        [TestMethod]
        public void FuncFreeNotationTest2() => new MySound().Run(FuncFreeNotation2);
        FlowNode FuncFreeNotation2() => Sine(E4).Curve(RecorderCurve).Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation3
{
    public class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected virtual FlowNode Start() => null;
    }

    [TestClass]
    public class MySound : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        
        public MySound() 
        { }

        public MySound(Func<FlowNode> func) 
            : base(func)
        { }

        
        [TestMethod]
        public void FuncFreeNotationTest3() => new MySound(FuncFreeNotation3);
        FlowNode FuncFreeNotation3() => Sine(E4).Curve(RecorderCurve).Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation4
{
    public class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected virtual FlowNode Start() => null;
    }

    [TestClass]
    public class FuncFreeNotationTests4 : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        
        public FuncFreeNotationTests4() 
        { }

        public FuncFreeNotationTests4(Func<FlowNode> func) 
            : base(func)
        { }

        [TestMethod]
        public void FuncFreeNotationTest4() => new Synth(FuncFreeNotation4);
        FlowNode FuncFreeNotation4() => Sine(E4).Curve(RecorderCurve).Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation5
{
    public class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected virtual FlowNode Start() => null;
    }

    [TestClass]
    public class FuncFreeNotationTests5 : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        
        public FuncFreeNotationTests5() 
        { }

        public FuncFreeNotationTests5(Func<FlowNode> func) 
            : base(func)
        { }

        
        [TestMethod]
        public void FuncFreeNotationTest5()
        {
            FlowNode MySynth()
            {
                return Sine(E4).Curve(RecorderCurve).Play();
            }
            
            new Synth(MySynth);
        }
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation6
{
    public class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected virtual FlowNode Start() => null;
    }

    [TestClass]
    public class FuncFreeNotationTests6 : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        
        public FuncFreeNotationTests6() 
        { }

        public FuncFreeNotationTests6(Func<FlowNode> func) 
            : base(func)
        { }

        
        [TestMethod]
        public void FuncFreeNotationTest6() { }
        
        protected override FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation7
{
    public class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected virtual FlowNode Start() => null;
    }

    [TestClass]
    public class FuncFreeNotationTests7 : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        
        public FuncFreeNotationTests7() 
        { }

        public FuncFreeNotationTests7(Func<FlowNode> func) 
            : base(func)
        { }

                
        //[TestMethod]
        //public void FuncFreeNotationTest7() => SynthWishes.Run(Start7);
        
        //FlowNode Start7() => Sine(E4).Curve(RecorderCurve).Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation8
{
    public abstract class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected abstract FlowNode Start();
    }

    [TestClass]
    public class FuncFreeNotationTests8 : Synth
    {
        FlowNode RecorderCurve => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
        
        protected override FlowNode Start() => Sine(E4).Curve(RecorderCurve).Play();
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation9
{
    public abstract class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected abstract FlowNode Start();
    }

    [TestClass]
    public class FuncFreeNotationTests9 : Synth
    {
        protected override FlowNode Start()
        {
            var recorderCurve = Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));
            
            var flow = Sine(E4).Curve(recorderCurve).Play();
            
            return flow;
        }
    }
}

namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation10
{
    public abstract class Synth : SynthWishes
    {
        public Synth(IContext context = null)
            : base(context)
            => Run(Start, GetType().Name);

        public Synth(Func<FlowNode> func, IContext context = null)
            : base(context)
            => Run(func, GetType().Name);
        
        protected abstract FlowNode Start();
    }

    [TestClass]
    public class FuncFreeNotationTests10 : Synth
    {
        // TODO: Where'd my TestMethod go?
        
        protected override FlowNode Start()
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
    }}
