
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

        //public static void Log(this SynthWishes synthWishes, string message = null)
        //{
        //    if (synthWishes == null) throw new NullException(() => synthWishes);
        //    synthWishes.Logging.Log(message);
        //}

        public static void LogTitle(this SynthWishes synthWishes, string message = null)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.Logging.LogTitle(message);
        }

        public static void LogSpaced(this SynthWishes synthWishes, string message = null)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.Logging.LogSpaced(message);
        }

        public static void LogTitleStrong(this SynthWishes synthWishes, string message = null)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.Logging.LogTitleStrong(message);
        }

        public static void LogOutputFile (this SynthWishes synthWishes, string filePath, string sourceFilePath = null)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.Logging.LogOutputFile(filePath, sourceFilePath);
        }

            //LogAction("File", ActionEnum.Save, FormatOutputFile(filePath, sourceFilePath, prefix: ""));

            //string formattedOutputBytes = FormatOutputBytes(bytes, "");

        
        private string FormatOutputBytes(byte[] bytes, string prefix = "  ")
        {
            if (!Has(bytes)) return "";
            //return $"{prefix}{PrettyByteCount(bytes.Length)} written to memory.";
            return $"{prefix}{PrettyByteCount(bytes.Length)}";
        }

            lines.Add("");
            lines.Add("Output:");
            lines.Add("");


            
            //if (buff.Bytes != null)
            //{
            //    lines.Add($"  {PrettyByteCount(buff.Bytes.Length)} written to memory.");
            //}

            //string formattedFilePath = Static.OutputFileMessage(buff.FilePath);
            //if (Has(formattedFilePath)) lines.Add(formattedFilePath);
            
            //lines.Add("");
        
        internal const string LogOutputFileCategoryNotSupported = 
            "Category parameter would clash with filePath parameter." +
            "Use this instead: Log(category, FormatOutputFile(filePath, sourceFilePath));";

        /// <inheritdoc cref="_logoutputfilewithcategory" />
        // ReSharper disable UnusedParameter.Global
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal void LogOutputFile(string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        // ReSharper restore once UnusedParameter.Global

        // ReSharper disable UnusedParameter.Global
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal void LogOutputFile(string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        // ReSharper restore once UnusedParameter.Global

        // ReSharper disable UnusedParameter.Global
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this FlowNode       entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this Tape           entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this TapeConfig     entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this TapeActions    entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this TapeAction     entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this Buff           entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this ConfigResolver entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        // ReSharper restore once UnusedParameter.Global

                
        //public string ActionMessage(byte[] entity, string name = "")
        //{
        //    if (!Has(entity)) return "";
        //    return ActionMessage("Memory", "Write", name, PrettyByteCount(entity));
        //}

        //public string ActionMessage(byte[] entity,   string     action,              string message = "") => ActionMessage("Memory", action,  "",   Coalesce(message, PrettyByteCount(entity)));
        //public string ActionMessage(byte[] entity,                           string name = "", string message = "") => ActionMessage("Memory", "Write", "",   Coalesce(message, PrettyByteCount(entity)));
        //public string ActionMessage(byte[] entity,                      string name,      string message = "") => ActionMessage("Memory", "Write", name, Coalesce(message, PrettyByteCount(entity)));
        //public string ActionMessage(byte[] entity,   string     action, string name, string message = "") => ActionMessage("Memory", action,  name, Coalesce(message, PrettyByteCount(entity)));
