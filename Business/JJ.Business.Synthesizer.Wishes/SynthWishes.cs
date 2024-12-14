﻿using System;
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
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;

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
        
        public void Run(Action action) => RunOnNewInstance(action);
        
        private void RunOnNewInstance(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            // Create a new instance of the derived class
            Type concreteType = this.GetType();
            var newInstance = (SynthWishes)Activator.CreateInstance(concreteType);
            
            // Yield over settings
            newInstance.Config = Config;

            // Try run on new instance
            MethodInfo methodInfo = action.Method;
            try
            {
                // Rebind the delegate to the new instance
                var newAction = (Action)Delegate.CreateDelegate(typeof(Action), newInstance, methodInfo);
            
                // Run the action on the new instance
                newInstance.RunOnThisInstance(() => newAction());
            }
            catch (ArgumentException ex)
            {
                // Work-around for exception:
                // "Cannot bind to the target method because its signature or security transparency
                // is not compatible with that of the delegate type."
                // Honestly I don't know how to solve other than catch the exception.
                Console.WriteLine(
                    $"{PrettyTime()} [RUN] " +
                    $"Unable to create new {concreteType.Name} instance for action {methodInfo.Name}. " +
                    $"Using current instance instead. Exception: {NewLine}{ex.Message}");
                RunOnThisInstance(action);
            }            
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
        
        private bool _isRunning;
        
        internal IList<FlowNode> GetChannelSignals(Func<FlowNode> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            AssertNotRunning();
            
            var originalChannel = GetChannel;
            try
            {
                _isRunning = true;
                
                LogMathOptimizationTitle();
                
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
                _isRunning = false;
                WithChannel(originalChannel);
            }
        }
        
        private void RunChannelSignals(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            AssertNotRunning();

            var originalChannel = GetChannel;
            try
            {
                _isRunning = true;
                
                LogMathOptimizationTitle();
            
                WithChannel(0);
                action();
                
                if (IsStereo)
                {
                    WithRight(); action();
                }
            }
            finally
            {
                _isRunning = false;
                WithChannel(originalChannel);
            }
        }
        
        private void AssertNotRunning()
        {
            if (_isRunning)
            {
                throw new Exception(
                    "Process already running. Nested usage, such as " +
                    "`Run(() => Save(() => ...))`, is not supported due to potential performance penalties. " +
                    "Use alternatives like `Run(() => ...).Save()` or `Save(() => ...)` to avoid nesting, " +
                    "or try `Run(() => mySound.Save())` for deferred audio streaming.");
            }
        }
        
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