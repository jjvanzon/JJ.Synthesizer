using JJ.Data.Synthesizer.Entities;

namespace JJ.Data.Synthesizer.Interfaces
{
    public interface IInletOrOutlet
    {
        int ID { get; set; }
        string Name { get; set; }
        Dimension Dimension { get; set; }
        int Position { get; set; }
        bool IsRepeating { get; set; }
        int? RepetitionPosition { get; set; }
        bool IsObsolete { get; set; }
        bool NameOrDimensionHidden { get; set; }
        Operator Operator { get; set; }
    }
}
