Code Scribbles

StereoTapeRecombiner:

    private IList<FlowNode> RecombineSignalsOld((Tape Left, Tape Right) tapePair) => _synthWishes.GetChannelSignals(
    () =>
    {
        FlowNode signal = _synthWishes.Sample(tapePair.Left).Panning(0) +
                            _synthWishes.Sample(tapePair.Right).Panning(1);

        return signal.SetName(tapePair.Left);
    });

SaveWishes:

    Save(action.Tape, filePath, callerMemberName);
    Save(action.Tape, action.GetFilePath(filePath), callerMemberName);

    string filePathResolved = tape.GetFilePath(tape.PopFilePathSuggested(filePath), callerMemberName);

BuffWishes:

    audioFileOutput.FilePath = 
        ResolveFilePath(tape.Config.AudioFormat, tape.FilePathResolved, tape.FilePathSuggested, 
            ResolveName(tape.Outlet, tape.Outlets, tape.FallBackName, callerMemberName));

    string resolvedFilePath = ResolveFilePath(
        tape.AudioFormat(), tape.GetFilePath(), ResolveName(audioFileOutput, callerMemberName));

    if (audioFileOutput.Bits() == 8)
    {
        for (int i = 0; i < bytes.Length; i++)
        {
            bytes[i] = 128;
        }
    }

Tape:

    /// <summary>
    /// Basically allows normal backing field behavior, but with a cheeky Pop option, that restors the previous value.
    /// </summary>
    private readonly Stack<string> filePathSuggestedStack = new Stack<string>();
        public string FilePathSuggested
    {
        get => filePathSuggestedStack.PeekOrDefault();
        set { if (value != null) filePathSuggestedStack.Push(value); }
    }
    
    /// <summary>
    /// Returns the last assigned FilePathSuggested, and restores FilePathSuggested to the previous value.
    /// Allows supplying your own filePath to short-circuit the action if the supplied file path is already filled in.
    /// </summary>
    internal string PopFilePathSuggested(string filePath = null)
    {
        if (Has(filePath)) return filePath;
        return filePathSuggestedStack.PopOrDefault();
    }
    
FilledInWishes:

    public static bool Has(this object obj, string value) => FilledInWishes.Has(value);

FrameworkWishes:

    /// <summary> Same as Peek(), but instead of crashing when zero items, returns default instead. </summary>
    public static T PeekOrDefault<T>(this Stack<T> stack)
    {
        if (stack == null) throw new ArgumentNullException(nameof(stack));

        if (stack.Count == 0)
        {
            return default;
        }

        return stack.Peek();
    }

    /// <summary> Same as Pop(), but instead of crashing when zero items, returns default instead. </summary>
    public static T PopOrDefault<T>(this Stack<T> stack)
    {
        if (stack == null) throw new ArgumentNullException(nameof(stack));

        if (stack.Count == 0)
        {
            return default;
        }

        return stack.Pop();
    }