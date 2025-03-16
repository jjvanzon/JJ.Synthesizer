        
        internal static RootLoggerConfig CreateDefaultRootLoggerConfig()
            => new RootLoggerConfig { Loggers = new List<LoggerConfig> { new LoggerConfig 
            { 
                Active = DefaultActive, 
                Format = DefaultFormat, 
                Type = $"{LoggerEnum.Console}", 
                Categories = new List<string>(),
                ExcludedCategories = new List<string>()
            }}};
        
