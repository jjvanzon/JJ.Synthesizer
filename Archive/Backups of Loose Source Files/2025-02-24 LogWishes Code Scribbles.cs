
                Console.WriteLine(message.TrimEnd());
                
                Console.Out.Flush();


            if (tape.SynthWishes == null)
            {
                LogWishes.Log(report);
            }
            else
            {
                tape.SynthWishes.Log(report);
            }

        private const bool DefaultLoggingEnabled = true;
        
        private ILogger _logger;
        
        private ILogger CreateLogger() => CreateLoggerFromConfig(Config.LoggerConfig);

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
    
    
        private T OrThrow<T>(T obj, Expression<Func<object>> expression)
        {
            if (obj == null) throw new NullException(expression);
            return obj;
        }

        public   static string ConfigLog(this SynthWishes     synthWishes                              ) => synthWishes.ConfigLog();
        public   static string ConfigLog(this SynthWishes     synthWishes,     string title            ) => synthWishes.ConfigLog(title);
        public   static string ConfigLog(this SynthWishes     synthWishes,     string title, string sep) => synthWishes.ConfigLog(title, sep);
        public   static string ConfigLog(this FlowNode        flowNode,        string title            ) => NoNull(() => flowNode).SynthWishes.ConfigLog(title, flowNode);

        public   static string ConfigLog(this FlowNode        flowNode                                 ) => NotNull(() => flowNode  ).SynthWishes.ConfigLog(       flowNode     );
        public   static string ConfigLog(this FlowNode        flowNode,        string title            ) => NotNull(() => flowNode  ).SynthWishes.ConfigLog(title, flowNode     );
        public   static string ConfigLog(this FlowNode        flowNode,        string title, string sep) => NotNull(() => flowNode  ).SynthWishes.ConfigLog(title, flowNode, sep);

        public   static string ConfigLog(this Tape            tape                                     ) => NotNull(() => tape      ).SynthWishes.ConfigLog(       tape     );
        public   static string ConfigLog(this Tape            tape,            string title            ) => NotNull(() => tape      ).SynthWishes.ConfigLog(title, tape     );
        public   static string ConfigLog(this Tape            tape,            string title, string sep) => NotNull(() => tape      ).SynthWishes.ConfigLog(title, tape, sep);
        public   static void LogConfig(this FlowNode       entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this Tape           entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        public   static void LogConfig(this TapeConfig     entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        
        public   static void LogConfig(this Buff           entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);
        internal static void LogConfig(this ConfigResolver entity) => GetLogWishes(entity, x => x.SynthWishes).LogConfig(entity);

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

        public static void Log(this SynthWishes synthWishes, string message = null)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            synthWishes.Logging.Log(message);
        }

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

            LogAction("File", ActionEnum.Save, FormatOutputFile(filePath, sourceFilePath, prefix: ""));

            string formattedOutputBytes = FormatOutputBytes(bytes, "");

        
        private string FormatOutputBytes(byte[] bytes, string prefix = "  ")
        {
            if (!Has(bytes)) return "";
            //return $"{prefix}{PrettyByteCount(bytes.Length)} written to memory.";
            return $"{prefix}{PrettyByteCount(bytes.Length)}";
        }

            lines.Add("");
            lines.Add("Output:");
            lines.Add("");


            
            if (buff.Bytes != null)
            {
                lines.Add($"  {PrettyByteCount(buff.Bytes.Length)} written to memory.");
            }

            string formattedFilePath = Static.OutputFileMessage(buff.FilePath);
            if (Has(formattedFilePath)) lines.Add(formattedFilePath);
            
            lines.Add("");
        
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

                
        public string ActionMessage(byte[] entity, string name = "")
        {
            if (!Has(entity)) return "";
            return ActionMessage("Memory", "Write", name, PrettyByteCount(entity));
        }

        public string ActionMessage(byte[] entity,   string     action,              string message = "") => ActionMessage("Memory", action,  "",   Coalesce(message, PrettyByteCount(entity)));
        public string ActionMessage(byte[] entity,                           string name = "", string message = "") => ActionMessage("Memory", "Write", "",   Coalesce(message, PrettyByteCount(entity)));
        public string ActionMessage(byte[] entity,                      string name,      string message = "") => ActionMessage("Memory", "Write", name, Coalesce(message, PrettyByteCount(entity)));
        public string ActionMessage(byte[] entity,   string     action, string name, string message = "") => ActionMessage("Memory", action,  name, Coalesce(message, PrettyByteCount(entity)));

        public string MemoryActionMessage(Tape tape                                                     ) => MemoryActionMessage(tape, ""         , ""  , ""     );
        public string MemoryActionMessage(Tape tape,                                 string message     ) => MemoryActionMessage(tape, ""         , ""  , message);
        public string MemoryActionMessage(Tape tape, ActionEnum action                                  ) => MemoryActionMessage(tape, $"{action}", ""  , ""     );
        public string MemoryActionMessage(Tape tape, ActionEnum action,              string message     ) => MemoryActionMessage(tape, $"{action}", ""  , message);
        public string MemoryActionMessage(Tape tape, ActionEnum action, string name, int dummy = default) => MemoryActionMessage(tape, $"{action}", name, ""     );
        public string MemoryActionMessage(Tape tape, ActionEnum action, string name, string message     ) => MemoryActionMessage(tape, $"{action}", name, message);
        public string MemoryActionMessage(Tape tape, string     action,              int dummy = default) => MemoryActionMessage(tape,    action  , ""  , ""     );
        public string MemoryActionMessage(Tape tape, string     action,              string message     ) => MemoryActionMessage(tape,    action  , ""  , message);
        public string MemoryActionMessage(Tape tape, string     action, string name, int dummy = default) => MemoryActionMessage(tape,    action  , name, ""     );
        public string MemoryActionMessage(Tape tape, string     action, string name, string message     )


        // TODO: Name  argument is up for use here.
        
        internal string ActionMessage(ConfigResolver entity, ActionEnum action                 ) => ActionMessage(entity, $"{action}", ""     );
        internal string ActionMessage(ConfigResolver entity, ActionEnum action,  string message) => ActionMessage(entity, $"{action}", message);
        internal string ActionMessage(ConfigResolver entity, string     action                 ) => ActionMessage(entity,    action  , ""     );
        internal string ActionMessage(ConfigResolver entity, string     message, int dummy = 0 ) => ActionMessage(entity, "",          message);
        internal string ActionMessage(ConfigResolver entity, string     action,  string message)
        {
            return ActionMessage("Config", action, message);
        }

        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes                                                ) { LogActionBase(MemoryActionMessage(entity, bytes                       )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                                 string message) { LogActionBase(MemoryActionMessage(entity, bytes,               message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action                             ) { LogActionBase(MemoryActionMessage(entity, bytes, action               )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action,              string message) { LogActionBase(MemoryActionMessage(entity, bytes, action,       message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, dummy  )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string name, int dummy = 0 ) { LogActionBase(MemoryActionMessage(entity, bytes,         name, dummy  )); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes,         name, message)); return entity; }
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, string     action, string name, string message) { LogActionBase(MemoryActionMessage(entity, bytes, action, name, message)); return entity; }

        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes                                                ) => Logging.LogMemoryAction(entity, bytes                       );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                                 string message) => Logging.LogMemoryAction(entity, bytes,               message);
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action                             ) => Logging.LogMemoryAction(entity, bytes, action               );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action,              string message) => Logging.LogMemoryAction(entity, bytes, action,       message);
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes, action, name, dummy  );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, ActionEnum action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string name, int dummy = 0 ) => Logging.LogMemoryAction(entity, bytes,         name, dummy  );
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes,                    string name, string message) => Logging.LogMemoryAction(entity, bytes,         name, message);
        public   FlowNode        LogMemoryAction(FlowNode        entity, byte[] bytes, string     action, string name, string message) => Logging.LogMemoryAction(entity, bytes, action, name, message);

        
        public string LogFileAction(string filePath                                                          ) => Logging.LogFileAction(filePath                                  );
        public string LogFileAction(string filePath,                                    string sourceFilePath) => Logging.LogFileAction(filePath,                   sourceFilePath);
        public string LogFileAction(string filePath, ActionEnum action                                       ) => Logging.LogFileAction(filePath, action                          );
        public string LogFileAction(string filePath, ActionEnum action,                 string sourceFilePath) => Logging.LogFileAction(filePath, action,           sourceFilePath);
        public string LogFileAction(string filePath, ActionEnum action, string message, string sourceFilePath) => Logging.LogFileAction(filePath, action,  message, sourceFilePath);
        public string LogFileAction(string filePath, string     action,                 int dummy = 0        ) => Logging.LogFileAction(filePath, action,           dummy         );
        public string LogFileAction(string filePath, string     action,                 string sourceFilePath) => Logging.LogFileAction(filePath, action,           sourceFilePath);
        public string LogFileAction(string filePath, string     action, string message, string sourceFilePath) => Logging.LogFileAction(filePath, action,  message, sourceFilePath);

        public   static string ActionMessage<TEntity>(             ActionEnum action                             ) => Logging.ActionMessage<TEntity>(   action               );
        public   static string ActionMessage<TEntity>(             ActionEnum action,              string message) => Logging.ActionMessage<TEntity>(   action,       message);
        public   static string ActionMessage<TEntity>(             ActionEnum action, string name, int dummy = 0 ) => Logging.ActionMessage<TEntity>(   action, name, dummy  );
        public   static string ActionMessage<TEntity>(             ActionEnum action, string name, string message) => Logging.ActionMessage<TEntity>(   action, name, message);
        public   static string ActionMessage<TEntity>(                                                           ) => Logging.ActionMessage<TEntity>(                        );
        public   static string ActionMessage<TEntity>(                                             string message) => Logging.ActionMessage<TEntity>(                 message);
        public   static string ActionMessage<TEntity>(             string     action,              int dummy = 0 ) => Logging.ActionMessage<TEntity>(   action               );
        public   static string ActionMessage<TEntity>(             string     action,              string message) => Logging.ActionMessage<TEntity>(   action,       message);
        public   static string ActionMessage<TEntity>(             string     action, string name, int dummy = 0 ) => Logging.ActionMessage<TEntity>(   action, name, dummy  );
        public   static string ActionMessage<TEntity>(             string     action, string name, string message) => Logging.ActionMessage<TEntity>(   action, name, message);

        public void LogAction<TEntity>(        ActionEnum action                             ) => Logging.LogAction<TEntity>(   action               );
        public void LogAction<TEntity>(        ActionEnum action,              string message) => Logging.LogAction<TEntity>(   action,       message);
        public void LogAction<TEntity>(        ActionEnum action, string name, int dummy = 0 ) => Logging.LogAction<TEntity>(   action, name, dummy  );
        public void LogAction<TEntity>(        ActionEnum action, string name, string message) => Logging.LogAction<TEntity>(   action, name, message);
        public void LogAction<TEntity>(                                                      ) => Logging.LogAction<TEntity>(                        );
        public void LogAction<TEntity>(                                        string message) => Logging.LogAction<TEntity>(                 message);
        public void LogAction<TEntity>(        string     action,              int dummy = 0 ) => Logging.LogAction<TEntity>(   action,       dummy  );
        public void LogAction<TEntity>(        string     action,              string message) => Logging.LogAction<TEntity>(   action,       message);
        public void LogAction<TEntity>(        string     action, string name, int dummy = 0 ) => Logging.LogAction<TEntity>(   action, name, dummy  );
        public void LogAction<TEntity>(        string     action, string name, string message) => Logging.LogAction<TEntity>(   action, name, message);

        public static void   LogMemoryAction(this int byteCount                                                ) => ResolveLogging(byteCount).LogMemoryAction(byteCount                       );
        public static void   LogMemoryAction(this int byteCount,                                 string message) => ResolveLogging(byteCount).LogMemoryAction(byteCount,               message);
        public static void   LogMemoryAction(this int byteCount, ActionEnum action                             ) => ResolveLogging(byteCount).LogMemoryAction(byteCount, action               );
        public static void   LogMemoryAction(this int byteCount, ActionEnum action,              string message) => ResolveLogging(byteCount).LogMemoryAction(byteCount, action,       message);
        public static void   LogMemoryAction(this int byteCount, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(byteCount).LogMemoryAction(byteCount, action, name, dummy  );
        public static void   LogMemoryAction(this int byteCount, ActionEnum action, string name, string message) => ResolveLogging(byteCount).LogMemoryAction(byteCount, action, name, message);
        public static void   LogMemoryAction(this int byteCount,                    string name, int dummy = 0 ) => ResolveLogging(byteCount).LogMemoryAction(byteCount,         name, dummy  );
        public static void   LogMemoryAction(this int byteCount,                    string name, string message) => ResolveLogging(byteCount).LogMemoryAction(byteCount,         name, message);
        public static void   LogMemoryAction(this int byteCount, string     action, string name, string message) => ResolveLogging(byteCount).LogMemoryAction(byteCount, action, name, message);

    
        // TODO: Review before removal.
    
         public static string ActionMessage(this FlowNode        entity, string     action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this FlowNode        entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this Tape            entity, string     action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this Tape            entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         /// <inheritdoc cref="_logtapeaction" />
         public static string Message      (this TapeAction      action,                    string message = null) => LogWishes.Resolve(action).Message      (action,         message);
         public static string ActionMessage(this TapeAction      action,                    string message = null) => LogWishes.Resolve(action).ActionMessage(action,         message);
         public static string ActionMessage(this Buff            entity, string     action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this Buff            entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this AudioFileOutput entity, string     action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this AudioFileOutput entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this Sample          entity, string     action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this Sample          entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).ActionMessage(entity, action, message);
         public static string ActionMessage(this byte[]          bytes,  string     action, string message = null) => LogWishes.Resolve(bytes ).ActionMessage(bytes,  action, message);
         public static string ActionMessage(this byte[]          bytes,  ActionEnum action, string message = null) => LogWishes.Resolve(bytes ).ActionMessage(bytes, action, message);
           
        public static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, string     action, string message = null) => LogWishes.Resolve(entity, synthWishes).ActionMessage(entity, action, message);
        public static string ActionMessage(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action, string message = null) => LogWishes.Resolve(entity, synthWishes).ActionMessage(entity, action, message);
        public static string ActionMessage(this Sample          entity, SynthWishes synthWishes, string     action, string message = null) => LogWishes.Resolve(entity, synthWishes).ActionMessage(entity, action, message);
        public static string ActionMessage(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message = null) => LogWishes.Resolve(entity, synthWishes).ActionMessage(entity, action, message);

        ublic static void LogAction(this FlowNode        entity, string     action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this FlowNode        entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this Tape            entity, string     action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this Tape            entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        // <inheritdoc cref="_logtapeaction" />
        ublic static void Log      (this TapeAction      action,                    string message = null) => LogWishes.Resolve(action).Log      (action,         message);
        ublic static void LogAction(this TapeAction      action,                    string message = null) => LogWishes.Resolve(action).LogAction(action,         message);
        ublic static void LogAction(this Buff            entity, string     action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this Buff            entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this AudioFileOutput entity, string     action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this AudioFileOutput entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this Sample          entity, string     action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this Sample          entity, ActionEnum action, string message = null) => LogWishes.Resolve(entity).LogAction(entity, action, message);
        ublic static void LogAction(this byte[]          bytes,  string     action, string message = null) => LogWishes.Resolve(bytes ).LogAction(bytes,  action, message);
        ublic static void LogAction(this byte[]          bytes,  ActionEnum action, string message = null) => LogWishes.Resolve(bytes ).LogAction(bytes,  action, message);
           
        public static void LogAction(this AudioFileOutput entity, SynthWishes synthWishes, string     action, string message = null) => LogWishes.Resolve(entity, synthWishes).LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, SynthWishes synthWishes, ActionEnum action, string message = null) => LogWishes.Resolve(entity, synthWishes).LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, SynthWishes synthWishes, string     action, string message = null) => LogWishes.Resolve(entity, synthWishes).LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, SynthWishes synthWishes, ActionEnum action, string message = null) => LogWishes.Resolve(entity, synthWishes).LogAction(entity, action, message);
           
        public   static void LogFileAction (this FlowNode       entity, string filePath, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        internal static void LogFileAction (this ConfigResolver entity, string filePath, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogFileAction (this Tape           entity, string filePath, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogFileAction (this TapeConfig     entity, string filePath, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogFileAction (this TapeActions    entity, string filePath, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogFileAction (this TapeAction     entity, string filePath, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);
        public   static void LogFileAction (this Buff           entity, string filePath, string sourceFilePath = "") => LogWishes.Resolve(entity).LogFileAction(filePath, sourceFilePath);



        public string ChannelDescriptor((int? channelCount, int? channel) tuple)
            => ChannelDescriptor(tuple.channelCount, tuple.channel);

        public   string ChannelDescriptor((int? channelCount, int? channel) tuple) => Logging.ChannelDescriptor(tuple                );

        public   static string ChannelDescriptor(this (int? channelCount, int? channel) tuple) => ResolveLogging(tuple.channelCount).ChannelDescriptor(tuple                );

        public   static string ChannelDescriptor(this FlowNode        entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this ConfigResolver  entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this ConfigSection   entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this Tape            entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this TapeConfig      entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this TapeActions     entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this TapeAction      entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this Buff            entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this AudioFileOutput entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        internal static string TapesLeftMessage (this Sample          entityForLogContext, (int? channels, int? channel) entityToDescribe) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);

        [Obsolete]
        private void LogActionBase(string actionMessage) => Log("Actions", actionMessage);

if (!_logger.WillLog("File")) return "";
string formattedFilePath = FormatFilePathIfExists(entity.FilePathResolved);
if (!Has(formattedFilePath)) return "";
return ActionMessage("File", action, formattedFilePath, message);


        internal static string IDDescriptor     (this ConfigResolver  entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this ConfigResolver  entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigResolver  entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string ChannelDescriptor(this ConfigResolver  entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigResolver  entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigResolver  entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this ConfigResolver  entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);

        internal static string IDDescriptor     (this ConfigSection   entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string IDDescriptor     (this ConfigSection   entityForLogContext, IList<int>      entityToDescribe  ) => ResolveLogging(entityForLogContext).IDDescriptor     (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, IList<Tape>     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptors      (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, TapeActions     entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, IList<FlowNode> entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string Descriptor       (this ConfigSection   entityForLogContext, AudioFileOutput entityToDescribe  ) => ResolveLogging(entityForLogContext).Descriptor       (entityToDescribe);
        internal static string ChannelDescriptor(this ConfigSection   entityForLogContext, Tape            entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigSection   entityForLogContext, TapeConfig      entityToDescribe  ) => ResolveLogging(entityForLogContext).ChannelDescriptor(entityToDescribe);
        internal static string ChannelDescriptor(this ConfigSection   entityForLogContext, int? channels, int? channel = null) => ResolveLogging(entityForLogContext).ChannelDescriptor(channels,channel);
        internal static string TapesLeftMessage (this ConfigSection   entityForLogContext, IList<Tape> tapes, int todoCount  ) => ResolveLogging(entityForLogContext).TapesLeftMessage (tapes, todoCount);


        //internal static LogWishes Resolve(SynthWishes synthWishes) => ResolveLogging(synthWishes);
        //internal static LogWishes Resolve(FlowNode flowNode) => ResolveLogging(flowNode);
        //internal static LogWishes Resolve(ConfigResolver configResolver) => ResolveLogging(configResolver);
        internal static LogWishes ResolveLogging(ConfigResolver configResolver)
        {
            if (configResolver == null) throw new NullException(() => configResolver);
            return configResolver.SynthWishes?.Logging ?? Static;
        }
        
        internal static LogWishes Resolve(Tape tape) => ResolveLogging(tape);
        internal static LogWishes Resolve(TapeConfig tapeConfig) => ResolveLogging(tapeConfig);
        internal static LogWishes Resolve(TapeActions tapeActions) => ResolveLogging(tapeActions);
        internal static LogWishes Resolve(TapeAction tapeActions) => ResolveLogging(tapeActions);
        internal static LogWishes Resolve(Buff buff) => ResolveLogging(buff);
        internal static LogWishes Resolve       (ConfigSection   configSection                           ) =>                         Static;
        internal static LogWishes ResolveLogging(ConfigSection   configSection                           ) =>                         Static;
        internal static LogWishes Resolve       (ConfigSection   configSection,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(ConfigSection   configSection,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (AudioFileOutput audioFileOutput                         ) =>                         Static;
        internal static LogWishes ResolveLogging(AudioFileOutput audioFileOutput                         ) =>                         Static;
        internal static LogWishes Resolve       (AudioFileOutput audioFileOutput, SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(AudioFileOutput audioFileOutput, SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (Sample          sample                                  ) =>                         Static;
        internal static LogWishes ResolveLogging(Sample          sample                                  ) =>                         Static;
        internal static LogWishes Resolve       (Sample          sample,          SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(Sample          sample,          SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (AudioInfoWish   audioInfoWish                           ) =>                         Static;
        internal static LogWishes ResolveLogging(AudioInfoWish   audioInfoWish                           ) =>                         Static;
        internal static LogWishes Resolve       (AudioInfoWish   audioInfoWish,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(AudioInfoWish   audioInfoWish,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (AudioFileInfo   audioFileInfo                           ) =>                         Static;
        internal static LogWishes ResolveLogging(AudioFileInfo   audioFileInfo                           ) =>                         Static;
        internal static LogWishes Resolve       (AudioFileInfo   audioFileInfo,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(AudioFileInfo   audioFileInfo,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (WavHeaderStruct wavHeader                               ) =>                         Static;
        internal static LogWishes ResolveLogging(WavHeaderStruct wavHeader                               ) =>                         Static;
        internal static LogWishes Resolve       (WavHeaderStruct wavHeader,       SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(WavHeaderStruct wavHeader,       SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (byte[]          bytes                                   ) =>                         Static;
        internal static LogWishes ResolveLogging(byte[]          bytes                                   ) =>                         Static;
        internal static LogWishes Resolve       (byte[]          bytes,           SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(byte[]          bytes,           SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (object          entity                                  ) =>                         Static;

        internal static LogWishes Logging(this SynthWishes synthWishes) // Providing method call syntax alongside property syntax.
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return synthWishes.Logging;
        }
        internal static LogWishes Logging(this FlowNode        entity                         ) => LogWishes.Resolve(entity);
        //internal static LogWishes Logging(this ConfigResolver  entity                         ) => LogWishes.Resolve(entity);
        //internal static LogWishes Logging(this ConfigSection   entity                         ) => LogWishes.Resolve(entity);
        //internal static LogWishes Logging(this ConfigSection   entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this Tape            entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this TapeConfig      entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this TapeActions     entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this TapeAction      entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this Buff            entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioFileOutput entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioFileOutput entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this Sample          entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this Sample          entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this AudioInfoWish   entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioInfoWish   entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this AudioFileInfo   entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioFileInfo   entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this WavHeaderStruct entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this WavHeaderStruct entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this IList<Tape>     tapes                          ) => LogWishes.Resolve(tapes);

        private readonly SynthWishes _synthWishes;
        public bool Enabled { get; set; } = true; // = Config.LoggerConfig.Active ?? DefaultLoggingEnabled; // TODO: Use config somehow


        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name, dummy  );
        internal static string ActionMessage(this ConfigResolver  entity, ActionEnum action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        internal static string ActionMessage(this ConfigResolver  entity                                                ) => ResolveLogging(entity).ActionMessage(entity                         );
        internal static string ActionMessage(this ConfigResolver  entity,                                 string message) => ResolveLogging(entity).ActionMessage(entity,                 message);
        internal static string ActionMessage(this ConfigResolver  entity, string     action,              int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action,       dummy  );
        internal static string ActionMessage(this ConfigResolver  entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigResolver  entity, string     action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name, dummy  );
        internal static string ActionMessage(this ConfigResolver  entity, string     action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action                             ) => ResolveLogging(entity).ActionMessage(entity,   action               );
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name, dummy  );
        internal static string ActionMessage(this ConfigSection   entity, ActionEnum action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        internal static string ActionMessage(this ConfigSection   entity                                                ) => ResolveLogging(entity).ActionMessage(entity                         );
        internal static string ActionMessage(this ConfigSection   entity,                                 string message) => ResolveLogging(entity).ActionMessage(entity,                 message);
        internal static string ActionMessage(this ConfigSection   entity, string     action,              int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action,       dummy  );
        internal static string ActionMessage(this ConfigSection   entity, string     action, string name, string message) => ResolveLogging(entity).ActionMessage(entity,   action, name, message);
        internal static string ActionMessage(this ConfigSection   entity, string     action,              string message) => ResolveLogging(entity).ActionMessage(entity,   action,       message);
        internal static string ActionMessage(this ConfigSection   entity, string     action, string name, int dummy = 0 ) => ResolveLogging(entity).ActionMessage(entity,   action, name, dummy  );

        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes                                                ) => ResolveLogging(entity).MemoryActionMessage(bytes                       );
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes,                                 string message) => ResolveLogging(entity).MemoryActionMessage(bytes,               message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).MemoryActionMessage(bytes, action               );   
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action,       message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, dummy  );
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, dummy  );
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, message);
        internal static string MemoryActionMessage(this ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes                                                ) => ResolveLogging(entity).MemoryActionMessage(bytes                       );
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes,                                 string message) => ResolveLogging(entity).MemoryActionMessage(bytes,               message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).MemoryActionMessage(bytes, action               );   
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action,       message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, dummy  );
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, dummy  );
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes,         name, message);
        internal static string MemoryActionMessage(this ConfigSection   entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).MemoryActionMessage(bytes, action, name, message);

        internal static string FileActionMessage(this ConfigResolver  entity, string filePath                                                          ) => ResolveLogging(entity).FileActionMessage(entity, filePath                                 );
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath,                  sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action                         );
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          dummy         );
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigResolver  entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);       
        internal static string FileActionMessage(this ConfigSection   entity, string filePath                                                          ) => ResolveLogging(entity).FileActionMessage(entity, filePath                                 );
        internal static string FileActionMessage(this ConfigSection   entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath,                  sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action                         );
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          dummy         );
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action,          sourceFilePath);
        internal static string FileActionMessage(this ConfigSection   entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity).FileActionMessage(entity, filePath, action, message, sourceFilePath);       

        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action                             ) { ResolveLogging(entity).LogAction(entity, action               ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, ActionEnum action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity                                                ) { ResolveLogging(entity).LogAction(entity                       ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity,                                 string message) { ResolveLogging(entity).LogAction(entity,               message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action,              int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action,       dummy  ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigResolver  LogAction(this ConfigResolver  entity, string     action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action                             ) { ResolveLogging(entity).LogAction(entity, action               ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, ActionEnum action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity                                                ) { ResolveLogging(entity).LogAction(entity                       ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity,                                 string message) { ResolveLogging(entity).LogAction(entity,               message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action,              int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action,       dummy  ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action,              string message) { ResolveLogging(entity).LogAction(entity, action,       message); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action, string name, int dummy = 0 ) { ResolveLogging(entity).LogAction(entity, action, name, dummy  ); return entity; }
        internal static ConfigSection   LogAction(this ConfigSection   entity, string     action, string name, string message) { ResolveLogging(entity).LogAction(entity, action, name, message); return entity; }

        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes                                                ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                       );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes,                                 string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,               message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action               );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action,       message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, dummy  );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, dummy  );
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, message);
        internal static ConfigResolver  LogMemoryAction(this ConfigResolver  entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes                                                ) => ResolveLogging(entity).LogMemoryAction(entity, bytes                       );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes,                                 string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,               message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action                             ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action               );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action,              string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action,       message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, dummy  );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, ActionEnum action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes,                    string name, int dummy = 0 ) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, dummy  );
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes,                    string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes,         name, message);
        internal static ConfigSection   LogMemoryAction(this ConfigSection   entity, byte[] bytes, string     action, string name, string message) => ResolveLogging(entity).LogMemoryAction(entity, bytes, action, name, message);

        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath                                                          ) => ResolveLogging(entity  ).LogFileAction(entity, filePath                                 );
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath,                  sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action                         );
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          dummy         );
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigResolver LogFileAction(this ConfigResolver entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath                                                          ) => ResolveLogging(entity  ).LogFileAction(entity, filePath                                 );
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath,                                    string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath,                  sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, ActionEnum action                                       ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action                         );
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, ActionEnum action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, ActionEnum action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, string     action,                 int dummy = 0        ) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          dummy         );
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, string     action,                 string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action,          sourceFilePath);
        internal static ConfigSection  LogFileAction(this ConfigSection  entity, string filePath, string     action, string message, string sourceFilePath) => ResolveLogging(entity  ).LogFileAction(entity, filePath, action, message, sourceFilePath);

        internal LogWishes Logging => LogWishes.Resolve(this);


    <!--<section name="jj.framework.logging" type="JJ.Framework.Configuration.ConfigurationSectionHandler, JJ.Framework.Configuration" />-->
  <!--<jj.framework.logging type="Debug" />-->
  <!--<jj.framework.logging types="Console;Debug" />--> 

        internal LogWishes Logging => LogWishes.Resolve(this);
        public LogWishes Logging => LogWishes.Resolve(this);


        internal static void LogTapeTree(this ConfigResolver entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);
        internal static void LogTapeTree(this ConfigSection entity, IList<Tape> tapes, bool includeCalculationGraphs = false)
            => entity.Logging().LogTapeTree(tapes, includeCalculationGraphs);


Code Scribbles

        internal ConfigResolver _config
        {
            get => _config;
            set => _config = value ?? throw new ArgumentException(nameof(_config));
        }


        /// <summary> Null for ConfigResolver.Static. Otherwise filled in. </summary>
        public SynthWishes SynthWishes { get; private set; }

            //SynthWishes = synthWishes;

            //if (synthWishes == null) throw new NullException(() => synthWishes);
            //clone.SynthWishes = synthWishes;


        //private readonly ConfigResolver _configResolver;
        //private RootLoggingConfig GetLoggingConfig() => _configResolver.LoggingConfig;
        
            //_configResolver = configResolver ?? throw new NullException(() => configResolver);
            
        //private void UpdateLogger() => _logger = CreateLogger(GetLoggingConfig());
        
        //public void UpdateLogger(RootLoggingConfig loggingConfig)
        //{
        //    _logger = CreateLogger(loggingConfig);
        //}
        
        //public bool Enabled
        //{
        //    get => GetLoggingConfig().Active;
        //    set
        //    {
        //        GetLoggingConfig().Active = value;
        //        UpdateLogger();
        //    }
        //}
        
        //public LogWishes SetCategories(IList<string> categories)
        //{
        //    if (categories == null) throw new NullException(() => categories);
            
        //    foreach (LoggerConfig loggerConfig in GetLoggingConfig().Loggers)
        //    {
        //        loggerConfig.Categories = categories.Select(x => new CategoryConfig { Name = x }).ToArray();
        //    }
            
        //    UpdateLogger();
            
        //    return this;
        //}
