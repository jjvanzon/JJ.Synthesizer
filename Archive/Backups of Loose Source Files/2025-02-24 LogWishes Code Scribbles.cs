
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
    
    
        //private T OrThrow<T>(T obj, Expression<Func<object>> expression)
        //{
        //    if (obj == null) throw new NullException(expression);
        //    return obj;
        //}

        //public   static string ConfigLog(this SynthWishes     synthWishes                              ) => synthWishes.ConfigLog();
        //public   static string ConfigLog(this SynthWishes     synthWishes,     string title            ) => synthWishes.ConfigLog(title);
        //public   static string ConfigLog(this SynthWishes     synthWishes,     string title, string sep) => synthWishes.ConfigLog(title, sep);
        //public   static string ConfigLog(this FlowNode        flowNode,        string title            ) => NoNull(() => flowNode).SynthWishes.ConfigLog(title, flowNode);

        public   static string ConfigLog(this FlowNode        flowNode                                 ) => NotNull(() => flowNode  ).SynthWishes.ConfigLog(       flowNode     );
        public   static string ConfigLog(this FlowNode        flowNode,        string title            ) => NotNull(() => flowNode  ).SynthWishes.ConfigLog(title, flowNode     );
        public   static string ConfigLog(this FlowNode        flowNode,        string title, string sep) => NotNull(() => flowNode  ).SynthWishes.ConfigLog(title, flowNode, sep);

        public   static string ConfigLog(this Tape            tape                                     ) => NotNull(() => tape      ).SynthWishes.ConfigLog(       tape     );
        public   static string ConfigLog(this Tape            tape,            string title            ) => NotNull(() => tape      ).SynthWishes.ConfigLog(title, tape     );
        public   static string ConfigLog(this Tape            tape,            string title, string sep) => NotNull(() => tape      ).SynthWishes.ConfigLog(title, tape, sep);
        public   static void LogConfig(this FlowNode       entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this Tape           entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this TapeConfig     entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        //public   static void LogConfig(this Buff           entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        //internal static void LogConfig(this ConfigResolver entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);

        public   static void LogConfig(this TapeActions    entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this TapeAction     entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);


        internal static LogWishes GetLogWishes(object entity, SynthWishes synthWishes)
        {
            if (entity == null) throw new NullException(() => entity);
            return synthWishes?.LogWishes ?? Static;
        }

        internal static string ConfigLog(this ConfigResolver  configResolver,  SynthWishes synthWishes                          ) => NotNull(() => synthWishes).ConfigLog(       configResolver      );
        internal static string ConfigLog(this ConfigResolver  configResolver,  SynthWishes synthWishes, string title            ) => NotNull(() => synthWishes).ConfigLog(title, configResolver      );
        internal static string ConfigLog(this ConfigResolver  configResolver,  SynthWishes synthWishes, string title, string sep) => NotNull(() => synthWishes).ConfigLog(title, configResolver,  sep);

        internal static void   LogConfig(this ConfigResolver  configResolver,  SynthWishes synthWishes                          ) => NotNull(() => synthWishes).LogConfig(       configResolver      );
        internal static void   LogConfig(this ConfigResolver  configResolver,  SynthWishes synthWishes, string title            ) => NotNull(() => synthWishes).LogConfig(title, configResolver      );
        internal static void   LogConfig(this ConfigResolver  configResolver,  SynthWishes synthWishes, string title, string sep) => NotNull(() => synthWishes).LogConfig(title, configResolver,  sep);

            return Resolve(tapes, x => x.Where(y => y != null).Select(y => y.SynthWishes).FirstOrDefault());

        
        private static LogWishes Resolve<T>(T entity, Func<T, SynthWishes> getSynthWishes)
        {
            if (entity == null) throw new NullException(() => entity);
            if (getSynthWishes == null) throw new NullException(() => getSynthWishes);
            SynthWishes synthWishes = getSynthWishes.Invoke(entity);
            return synthWishes?.Logging ?? Static;
        }

                LogMathBoostDone(GetMathBoost);

        
        internal void LogMathBoostDone(bool mathBoost) 
            => Logging.LogMathBoostDone(mathBoost);
        
        internal void LogMathBoostDone(bool mathBoost)
        {
            if (!mathBoost) return;
            //LogLine("Done");
        }
                
                LogMathBoostDone(GetMathBoost);

        
        private void CleanUpFile(string filePath/*, SynthWishes synthWishes*/)
        {
            try
            {
                if (!Exists(filePath)) return;
                Delete(filePath);
                //synthWishes?.LogAction("File", "Deleted", filePath);
                Static.LogAction("File", "Deleted", filePath);
            }
            catch (Exception ex)
            {
                // Don't let clean-up fail other tests.
                //synthWishes?.LogAction("File", "Delete", filePath, "Failed: " + ex.Message);
                Static.LogAction("File", "Delete", filePath, "Failed: " + ex.Message);
            }
        }
