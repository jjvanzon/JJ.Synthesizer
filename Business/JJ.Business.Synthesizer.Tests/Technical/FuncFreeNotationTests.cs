//using JJ.Business.Synthesizer.Wishes;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Linq;
//using System.Reflection;
//using JJ.Business.Synthesizer.Tests.Accessors;
//using JJ.Business.Synthesizer.Tests.Helpers;
//using System.Threading;
//using static JJ.Business.Synthesizer.Wishes.NameHelper;

//// ReSharper disable PublicConstructorInAbstractClass
//// ReSharper disable ArrangeStaticMemberQualifier
//// ReSharper disable ObjectCreationAsStatement
//// ReSharper disable CheckNamespace
//// ReSharper disable ExplicitCallerInfoArgument

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.WithFunc
//{
//    /// <summary> ✔️ Not func free. But it works. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : MySynthWishes
//    {
//        public FuncFree() => WithStereo();
        
//        [TestMethod]
//        public void WithFunc() => new FuncFree().Start();
        
//        void Start() => Run(() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play());
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.MethodGroup
//{
//    /// <summary> ❌ No sound. Has instance contention. Though looks better. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : MySynthWishes
//    {
//        public FuncFree() => WithStereo();
        
//        [TestMethod]
//        public void MethodGroup() => new FuncFree().Run(Start);
        
//        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.InstanceRun
//{
//    /// <summary> ➖ No context isolation. Looks OK. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : MySynthWishes
//    {
//        public FuncFree() => WithStereo();
        
//        [TestMethod]
//        public void InstanceRun() => Run(Start);
        
//        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.AllOnOneInstance
//{
//    /// <summary> ➖ No context isolation. Doesn't look great. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : MySynthWishes
//    {
//        public FuncFree() => WithStereo();

//        [TestMethod]
//        public void AllOnOneInstance() { var x = new FuncFree(); x.Run(x.Start); }

//        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.ConstructorFunc
//{
//    /// <summary> ❌ No sound. Has instance contention. But looks ok. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : Synth
//    {
//        public FuncFree()=> WithStereo();
//        public FuncFree(Func<FlowNode> func) : base(func) => WithStereo();
        
//        [TestMethod]
//        public void ConstructorFunc() => new FuncFree(Start);
        
//        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }

//    public class Synth : MySynthWishes
//    {
//        public Synth() { }
//        public Synth(Func<FlowNode> func) => Run(func, GetType().Name);
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.LocalFunc_ConstructorFunc
//{
//    /// <summary> ❌ Confusing </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : Synth
//    {
//        public FuncFree() => WithStereo();
//        public FuncFree(Func<FlowNode> func) => WithStereo();
        
//        [TestMethod]
//        public void LocalFunc_ConstructorFunc()
//        {
//            FlowNode MySynth() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
            
//            new Synth(MySynth);
//        }
//    }

//    public class Synth : MySynthWishes
//    {
//        public Synth() { }
//        public Synth(Func<FlowNode> func) => Run(func, GetType().Name);
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.VirtualStart
//{
//    /// <summary> ❌ Constructor not run on time. Only one test per class. But concise magic! </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : Synth
//    {
//        public FuncFree() => WithStereo();
        
//        [TestMethod]
//        public void VirtualStart() { }
        
//        protected override FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }

//    public class Synth : MySynthWishes
//    {
//        public Synth() => Run(Start, GetType().Name);
        
//        protected virtual FlowNode Start() => null;
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.AbstractStart
//{
//    /// <summary> ❌ Constructor not run on time. Only one test per class. But forceful concise magic! </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : Synth
//    {
//        public FuncFree() => WithStereo();
        
//        [TestMethod]
//        public void AbstractStart() { }
        
//        protected override FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }

//    public abstract class Synth : MySynthWishes
//    {
//        public Synth() => Run(Start, GetType().Name);
        
//        protected abstract FlowNode Start();
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.StaticRun
//{
//    /// <summary> ❌ No sound. Has instance contention. But more terse. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : Synth
//    {
//        public FuncFree() => WithStereo();
        
//        [TestMethod]
//        public void StaticRun() => Synth.Run(Start);
        
//        FlowNode Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }

//    public class Synth : MySynthWishes
//    {
//        public static void Run(Func<FlowNode> func) => new Synth().Run(func, func.Method.Name);
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.NoMainFlowNode
//{
//    /// <summary> ➖ No context isolation. Looks cool though; no main flow node. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : Synth
//    {
//        public FuncFree() => WithStereo();
        
//        [TestMethod]
//        public void NoMainFlowNode() => Run(Start);
        
//        void Start() => Sine(E4).Curve(DelayedPulseCurve).Panbrello().Play();
//    }

//    /// <summary> Sandbox ➖ No context isolation. Looks cool though; no main flow node. </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree2 : Synth
//    {
//        public FuncFree2() => WithStereo();
        
//        [TestMethod]
//        public void NoMainFlowNode2() => Run(Start);

//        void Start()
//        {
//            var curve = Curve((0, 0), (0.2, 0), (0.3, 1),  (0.7, 1), (0.8, 0), (1.0, 0));
            
//            Sine(E4).Curve(curve).Panbrello(3).Play("Note1");
//            Sine(G4).Curve(curve).Panbrello(5).Play("Note2");
//        }
//    }

//    /// <summary>
//    /// Offers a Run overload that takes an Action, not require returning a FlowNode,
//    /// making the notation more concise and flexible.
//    /// </summary>
//    public class Synth : MySynthWishes
//    {
//        public void Run(Action action)
//        {
//            Run(
//                () =>
//                {
//                    action();
                    
//                    // HACK: To avoid the FlowNode return value,
//                    // and make tape runner run all tapes without an explicit root node specified,
//                    // create a root node here after all.
//                    FlowNode[] tapeSignals = new SynthWishesAccessor(this)._tapes.GetAll().Select(x => x.Signal).ToArray();
//                    FlowNode root = Add(tapeSignals);
//                    return root;
//                },
//                action.Method.Name
//            );
//        }
//    }
//}

//namespace JJ.Business.Synthesizer.Tests.Technical.FuncFreeNotation.DelegateRebind
//{
//    /// <summary> ✔️ Looks clean, works good! </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree : Synth
//    {
//        public FuncFree() => WithStereo();

//        [TestMethod]
//        public void DelegateRebind()
//        {
//            Run(Sound1);
//            Run(Sound2);
//        }

//        void Sound1() => Sine(E4).Curve(DelayedPulseCurve).Panbrello(3).Play();
        
//        void Sound2() => Sine(G4).Curve(DelayedPulseCurve).Panbrello(5).Play();
//    }

//    /// <summary>  </summary>
//    [TestClass]
//    [TestCategory("Technical")]
//    public class FuncFree_DelegateRebind_WithInstanceVerification : Synth
//    {
//        private static   int _instanceCounter;
//        private readonly int _instanceId;
        
//        public FuncFree_DelegateRebind_WithInstanceVerification()
//        {
//            _instanceId = Interlocked.Increment(ref _instanceCounter);
//            WithStereo();
//        }

//        [TestMethod]
//        public void DelegateRebind_WithInstanceVerification()
//        {
//            Run(Sound1);
//            Run(Sound2);
//        }

//        void Sound1()
//        {
//            Console.WriteLine($"Instance Integrity: {MemberName()} running on instance : {_instanceId}");
//            Sine(E4).Curve(DelayedPulseCurve).Panbrello(3).Play();
//        }
        
//        void Sound2()
//        {
//            Console.WriteLine($"Instance Integrity: {MemberName()} running on instance : {_instanceId}");
//            Sine(G4).Curve(DelayedPulseCurve).Panbrello(5).Play();
//        }
//    }

//    public class Synth : MySynthWishes
//    {
//        public void Run(Action action)
//        {
//            // Create a new instance of the derived class
//            Type concreteType = this.GetType();
//            Synth newInstance = (Synth)Activator.CreateInstance(concreteType);

//            // Rebind the delegate to the new instance
//            MethodInfo methodInfo = action.Method;
//            Action newAction = (Action)Delegate.CreateDelegate(typeof(Action), newInstance, methodInfo);

//            // Run the action on the new instance
//            newInstance.Run(
//                () =>
//                {
//                    newAction();
                    
//                    // HACK: To avoid the FlowNode return value,
//                    // and make tape runner run all tapes without an explicit root node specified,
//                    // create a root node here after all.
//                    FlowNode[] tapeSignals = new SynthWishesAccessor(this)._tapes.GetAll().Select(x => x.Signal).ToArray();
//                    FlowNode root = Add(tapeSignals);
//                    return root;
//                },
//                newAction.Method.Name
//            );
//        }
        
//        //public void Run(Func<FlowNode> startMethod)
//        //{
//        //    // Create a new instance of the derived class
//        //    var instance = (MySynthWishes3)Activator.CreateInstance(this.GetType());

//        //    // Rebind the delegate to the new instance
//        //    MethodInfo methodInfo = startMethod.Method;
//        //    Func<FlowNode> newStartMethod = (Func<FlowNode>)Delegate.CreateDelegate(typeof(Func<FlowNode>), instance, methodInfo);

//        //    // Run the start method on the new instance
//        //    instance.Run(newStartMethod);
//        //}
//    }
//}