
    // Match by delegate method
    //var groupedByMethod = group.Where(x => x.Callback != null).GroupBy(x => x.Callback.Method);
    //foreach (var subGroup in groupedByMethod)
    //{
    //    TryAddPair(subGroup);
    //}
    
    //// Match by delegate type
    //var groupedByClass = group.Where(x => x.Callback != null).GroupBy(x => x.Callback.Method.DeclaringType);
    //foreach (var subGroup in groupedByClass)
    //{
    //    TryAddPair(subGroup);
    //}
            
    protected bool CanPlayForAllTapes(Tape tape)
    {
        if (tape == null) throw new NullException(() => tape);
        
        if (!tape.PlayForAllTapes.On)
        {
            return false;
        }

        if (tape.PlayForAllTapes.Done)
        {
            LogAction(tape, PlayAllTapes, "Already played");
            return false;
        }
        
        tape.PlayForAllTapes.Done = true;
        
        return true;
    }

    string actionSuffix = ActionSuffix(entity, action);


    private static string ActionSuffix(Tape tape, string action)
    {
        if (Is(action, "Play"))
        {
            string allSuffix = "";
            if (tape.PlayAllTapes.On)
            {
                allSuffix = " {all}";
            }

            if (tape.Play.On && tape.PlayChannel.On)
            {
                return " (Channel)" + allSuffix;
            }
            if (!tape.Play.On && tape.PlayChannel.On)
            {
                return " Channel" + allSuffix;
            }
            else
            {
                return allSuffix;
            }
        }
        if (Is(action, "Save"))
        {
            if (tape.Save.On && tape.SaveChannel.On)
            {
                return " (Channel)";
            }
            if (!tape.Save.On && tape.SaveChannel.On)
            {
                return " Channel";
            }
        }
        if (Is(action, "Intercept"))
        {
            if (tape.Intercept.On && tape.InterceptChannel.On)
            {
                return " (Channel)";
            }
            if (!tape.Intercept.On && tape.InterceptChannel.On)
            {
                return " Channel";
            }
        }

        return default;
    }

ActionRunners:

    // Run All Action Types

    public void RunActions(IList<Tape> tapes)
    {
        if (tapes == null)
            throw new ArgumentNullException(nameof(tapes));
        tapes.ForEach(RunActions);
    }

    public void RunActions(Tape tape)
    {
        if (tape == null)
            throw new ArgumentNullException(nameof(tape));

        InterceptIfNeeded(tape.Actions.AfterRecord);
        InterceptIfNeeded(tape.Actions.AfterRecordChannel);
        SaveIfNeeded(tape.Actions.Save);
        SaveIfNeeded(tape.Actions.SaveChannel);
        PlayIfNeeded(tape.Actions.Play);
        PlayIfNeeded(tape.Actions.PlayChannel);
        CacheToDiskIfNeeded(tape.Actions.DiskCache);
        PlayIfNeeded(tape.Actions.PlayAllTapes);
    }




    internal IList<FlowNode> ConcatSignals()
    {
        var signals = new List<FlowNode>();
        if (Signal != null) signals.Add(Signal);
        signals.AddRange(Signals.Where(FilledIn));
        return signals;
    }
    
    internal void SetSignals(IList<FlowNode> signals)
    {
        if (signals == null) 
        {
            //Signal = default;
            Signals = default;
            return;
        }
        
        switch (signals.Count)
        {
            case 0:
                Signal = default;
                Signals = default;
                break;
            case 1:
                Signal = signals[0];
                Signals = default;
                break;
            default:
                Signal = default;
                Signals = signals;
                break;
        }
    }

    /// <summary> For stereo tapes. Not null. Auto(re)created. </summary>

NameWishes:

    case IEnumerable<string> strings:
        return strings.FirstOrDefault(FilledIn);

    case IEnumerable<FlowNode> flowNodes:
        return TryGetName(flowNodes.Select(x => x?.Name));

AudioFormatTests:

    Buff signalBuffOld = RecordLegacy(this, () => Signal(freq, testName).SaveChannels(fileName + "_Channel" + GetChannel));
    signalBuffOld.Save(fileName);

    //Save(signalTapeNew, testName + "_" + fileNum++ + "_" + nameof(signalTapeNew));
    //tapes.ForEach((x, i) => x.Save(testName + "_" + fileNum++ + "_" + nameof(signalTapeNew) + "_Channel" + i));
