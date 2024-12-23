SynthLogOld:

    IList<string> warnings = buff.Messages.ToArray();
    if (warnings.Any())
    {
        lines.Add("Warnings:");
        lines.AddRange(warnings.Select(warning => $"- {warning}"));
        lines.Add("");
    }

RunTape:

    LogAction(tape, "Leaf Found", "Running");
    LogAction(tape, "Start");

    LogAction(tape, "Stop");
    LogAction(tape, "Task Finished", "Check for Leaves");

PlayWishes:

    internal static Buff InternalPlay(
        SynthWishes synthWishes,
        string typeName, string objectName,
        string filePath, byte[] bytes, AudioFileFormatEnum audioFormat) 
        => InternalPlayBase(synthWishes, typeName, objectName, filePath, bytes, Has(audioFormat) ? audioFormat.FileExtension() : null);

LogWishes:

    internal static string GetActionMessage(Tape tape, string action) 
        => GetActionMessage(nameof(Tape), tape.Descriptor(), action, null);

    internal static string GetActionMessage(Tape tape, string action, string message) 
        => GetActionMessage(nameof(Tape), tape.Descriptor(), action, message);

    internal static string GetActionMessage(string typeName, string message) 
        => GetActionMessage(typeName, null, null, message);

    internal static string GetActionMessage(string typeName, string action, string message) 
        => GetActionMessage(typeName, null, action, message);

    if (Is(action, "Play"))
    {
        if (entity.IsPlay && entity.IsPlayChannel) action += "(Channel)";
        if (!entity.IsPlay && entity.IsPlayChannel) action += "Channel";
    }
    if (Is(action, "Save"))
    {
        if (entity.IsSave && entity.IsSaveChannel) action += "(Channel)";
        if (!entity.IsSave && entity.IsSaveChannel) action += "Channel";
    }
    if (Is(action, "Intercept"))
    {
        if (entity.IsIntercept && entity.IsInterceptChannel) action += "(Channel)";
        if (!entity.IsIntercept && entity.IsInterceptChannel) action += "Channel";
    }

    internal static void LogPlayAction(Tape tape, string action, string message = default)
    {
        string actionSuffix = "";
        if (tape.PlayAllTapes)
        {
            actionSuffix = " {all}";
        }

        LogAction(tape, action + actionSuffix, message);
    }

    internal static void LogOutputFileIfExists(string filePath, string sourceFilePath = null)
    {
        try
        {
            if (Exists(filePath)) LogOutputFile(filePath, sourceFilePath);
        }
        catch 
        {
            // Do not a garbled file path stop the main process.
        }
    }
