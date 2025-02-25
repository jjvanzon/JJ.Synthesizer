
                //Console.WriteLine(message.TrimEnd());
                
                //Console.Out.Flush();


            if (tape.SynthWishes == null)
            {
                LogWishes.Log(report);
            }
            else
            {
                tape.SynthWishes.Log(report);
            }

        //private const bool DefaultLoggingEnabled = true;
        
        //private ILogger _logger;
        
        //private ILogger CreateLogger() => CreateLoggerFromConfig(Config.LoggerConfig);

        // ReSharper disable once UnusedParameter.Global
        public static void Log(AudioFileOutput    entity, string message = "") => Static.Log(message);
        // ReSharper disable once UnusedParameter.Global
        public static void Log(Sample             entity, string message = "") => Static.Log(message);
        // ReSharper disable once UnusedParameter.Global
        public static void Log(AudioInfoWish      entity, string message = "") => Static.Log(message);

// TODO: Give Buff a Tape (? reference) and a delegated SynthWishes reference too?

        public   static void Log(FlowNode           entity, string message = "") => (entity ?? throw new NullException(() => entity)).SynthWishes.Log(message);
        internal static void Log(ConfigResolver     entity, string message = "") => (entity?.SynthWishes?.Logging ?? Static).Log(message);
        public   static void Log(Tape               entity, string message = "") => (entity ?? throw new NullException(() => entity)).SynthWishes.Log(message);
        public   static void Log(TapeConfig         entity, string message = "") => (entity ?? throw new NullException(() => entity)).SynthWishes.Log(message);
        public   static void Log(TapeActions        entity, string message = "") => (entity ?? throw new NullException(() => entity)).SynthWishes.Log(message);
        public   static void Log(TapeAction         entity, string message = "") => (entity ?? throw new NullException(() => entity)).SynthWishes.Log(message);
        public   static void Log(Buff               entity, string message = "") => (entity?.SynthWishes?.Logging ?? Static).Log(message);
