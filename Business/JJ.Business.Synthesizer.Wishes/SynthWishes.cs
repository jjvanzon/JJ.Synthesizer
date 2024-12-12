using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public IContext Context { get; }

        private ConfigWishes _config;
        public ConfigWishes Config
        {
            get => _config;
            set => _config = value ?? throw new ArgumentException(nameof(Config));
        }

        /// <inheritdoc cref="docs._captureindexer" />
        public readonly CaptureIndexer _;

        private readonly OperatorFactory _operatorFactory;
        private readonly CurveFactory _curveFactory;
        private readonly SampleManager _sampleManager;
        private readonly TapeCollection _tapes;
        private readonly TapeRunner _tapeRunner;

        public SynthWishes()
            : this(ServiceFactory.CreateContext())
        { }
        
        public SynthWishes(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            _operatorFactory = ServiceFactory.CreateOperatorFactory(context);
            _curveFactory = ServiceFactory.CreateCurveFactory(context);
            _sampleManager = ServiceFactory.CreateSampleManager(context);
            
            Config = new ConfigWishes();
            _tapes = new TapeCollection(this);
            _tapeRunner = new TapeRunner(this, _tapes);
            
            _ = new CaptureIndexer(this);
            bar = new BarIndexer(this);
            bars = new BarsIndexer(this);
            beat = new BeatIndexer(this);
            b = new BeatIndexer(this);
            beats = new BeatsIndexer(this);
            l = new BeatsIndexer(this);
            len = new BeatsIndexer(this);
            length = new BeatsIndexer(this);
            t = new TimeIndexer(this);
        }
        
        public void Run(Action action)
        {
            // Work-around for Delegate.CreateDelegate ArgumentException:
            // "Cannot bind to the target method because its signature or security transparency
            // is not compatible with that of the delegate type."
            // Honestly I don't know how to solve other than catch the exception.
            try
            {
                RunOnNewInstance(action);
            }
            catch (ArgumentException ex)
            {
                RunOnThisInstance(action);
            }
        }
        
        private void RunOnNewInstance(Action action)
        {
            // Create a new instance of the derived class
            Type concreteType = this.GetType();
            var newInstance = (SynthWishes)Activator.CreateInstance(concreteType);
            
            // Yield over settings
            newInstance.Config = Config;
            
            // Rebind the delegate to the new instance
            MethodInfo methodInfo = action.Method;
            var newAction = (Action)Delegate.CreateDelegate(typeof(Action), newInstance, methodInfo);
            
            // Run the action on the new instance
            newInstance.RunOnThisInstance(() => newAction());
        }
        
        private void RunOnThisInstance(Action action)
        {
            RunChannelSignals(action);
            
            if (GetParallelTaping)
            {
                _tapeRunner.RunAllTapes();
            }
            else
            {
                throw new Exception("Run method cannot work without ParallelTaping.");
            }
        }
        
        internal IList<FlowNode> GetChannelSignals(Func<FlowNode> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            LogMathOptimizationTitle();
            
            var originalChannel = GetChannel;
            try
            {
                WithChannel(0);
                var channel0Signal = func();
                
                if (IsMono)
                {
                    return new[] { channel0Signal };
                }
                if (IsStereo)
                {
                    WithRight(); var channel1Signal = func();
                    return new[] { channel0Signal, channel1Signal };
                }
                
                throw new ValueNotSupportedException(GetChannels);
            }
            finally
            {
                WithChannel(originalChannel);
            }
        }

        private void RunChannelSignals(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            LogMathOptimizationTitle();
            
            var originalChannel = GetChannel;
            try
            {
                WithChannel(0);
                action();
                
                if (IsStereo)
                {
                    WithRight(); action();
                }
            }
            finally
            {
                WithChannel(originalChannel);
            }
        }

        // Command Indexers
        
        // No Parameters
        
        public FlowNode this[Func<FlowNode> func] 
            => func();
        
        // 1 Parameter
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func, 
            FlowNode arg1 = null] 
            => func(arg1);
        
        public FlowNode this[
            Func<FlowNode, FlowNode> func, 
            double arg1] 
            => func(_[arg1]);
        
        // 2 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null] 
            => func(arg1, arg2);
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            double arg1, FlowNode arg2 = null] 
            => func(_[arg1], arg2);
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, double arg2] 
            => func(arg1, _[arg2]);
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode> func, 
            double arg1, double arg2] 
            => func(_[arg1], _[arg2]);
        
        // 3 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null] 
            => func(arg1, arg2, arg3);     
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, FlowNode arg2 = null, FlowNode arg3 = null] 
            => func(_[arg1], arg2, arg3);     
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, double arg2, FlowNode arg3 = null] 
            => func(arg1, _[arg2], arg3);     
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, FlowNode arg2, double arg3] 
            => func(arg1, arg2, _[arg3]);     
               
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, double arg2, FlowNode arg3] 
            => func(_[arg1], _[arg2], arg3);     
               
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, FlowNode arg2, double arg3] 
            => func(_[arg1], arg2, _[arg3]);     

        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1, double arg2, double arg3] 
            => func(arg1, _[arg2], _[arg3]);     
               
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
            double arg1, double arg2, double arg3] 
            => func(_[arg1], _[arg2], _[arg3]);     

        // 4 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null] 
            => func(arg1, arg2, arg3, arg4);

        // 5 Parameters
        
        public FlowNode this[
            Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> func, 
            FlowNode arg1 = null, FlowNode arg2 = null, FlowNode arg3 = null, FlowNode arg4 = null, FlowNode arg5 = null] 
            => func(arg1, arg2, arg3, arg4, arg5);
        
        // TODO: Dynamic for more parameters
        
        //public FlowNode this[
        //    Func<FlowNode, FlowNode, FlowNode, FlowNode> func, 
        //    params FlowNode[] args] 
        //    => func(args[0], args[1], args[2]);
            
        // Helpers

        private static string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string filePath = SanitizeFilePath(name);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = audioFileFormatEnum.GetFileExtension();
            return fileNameWithoutExtension + fileExtension;
        }
    }
}