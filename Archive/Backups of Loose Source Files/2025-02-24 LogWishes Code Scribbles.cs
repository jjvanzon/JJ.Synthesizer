
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
