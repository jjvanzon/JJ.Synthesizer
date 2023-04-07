namespace JJ.Utilities.Synthesizer.RawToWav;

public partial class MainForm : SimpleProcessForm
{
    public MainForm()
    {
        InitializeComponent();

        OnRunProcess += MainForm_OnRunProcess;
    }

    private void MainForm_OnRunProcess(object? sender, EventArgs e)
    {
        
    }
}