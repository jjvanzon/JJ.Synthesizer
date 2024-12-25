Code Scribbles

    internal static void LogCreated(Sample entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        LogLine(ActionMessage(entity.GetType(), entity.Name, "Create", ConfigLog(entity)));
    }

    internal static void LogCreated(AudioFileOutput entity)
    {
        LogLine(ActionMessage("Audio File Out", entity.Name, "Create", ConfigLog(entity)));
    }

    private static bool _lastLineWasEmpty = true; // Start of log is considered empty.

    public static void LogLine(string message = default)
    {
        message = message ?? "";

        bool filledIn = Has(message);

        if (_lastLineWasEmpty && !filledIn)
        {
            // Avoid duplicate blank lines.
            return;
        }

        Console.WriteLine(message);

        _lastLineWasEmpty = !filledIn;
    }

    public static void LogLine(string message = default)
    {
        message = message ?? "";
        
        if (!Has(message) && _lastLineWasEmpty)
        {
            return;
        }
        
        Console.WriteLine(message);
        
        _lastLineWasEmpty = LastLineIsEmpty(message);
    }
            
    if (_lastLineWasEmpty)
    {
        int dummy = 0;
    }

    public void AppendLine(string line = "")
    {
        bool isEmptyLine = !FilledIn(line);
        //if (isEmptyLine)
        //{
        //    Append(_enter);
        //    AppendTabs();
        //    return;
        //}
        
        if (_sb.Length > 0 || isEmptyLine)
        {
            _sb.Append(_enter);
        }
        
        AppendTabs();
        Append(line);
    }

    public class StringBuilderWithIndentationWish
    {
        private readonly StringBuilder _sb = new StringBuilder();

        private readonly string _tabString;
        private readonly string _enter;
        private int _tabCount;

        public StringBuilderWithIndentationWish()
            : this("  ", Environment.NewLine)
        { }

        public StringBuilderWithIndentationWish(string tabString, string enter)
        {
            _tabString = tabString;
            _enter = enter;
        }

        public void Append(object obj) => _sb.Append(obj);
        public void Append(string str) => _sb.Append(str);
        public void Append(char chr) => _sb.Append(chr);

        public void AppendLine(string line = "")
        {
            Append(_enter);
            AppendTabs();
            Append(line);
        }

        private void AppendTabs()
        {
            for (int i = 0; i < _tabCount; i++)
            {
                _sb.Append(_tabString);
            }
        }

        public void Outdent() => _tabCount--;

        public void Indent() => _tabCount++;

        public override string ToString() => _sb.ToString();
    }

    LogAction(nameof(Tape), "Wait", $"No Leaves ({waitCount}) ...");
