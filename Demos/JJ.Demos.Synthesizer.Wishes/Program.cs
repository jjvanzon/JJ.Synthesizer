new Synth().Run();

class Synth : SynthWishes
{
    public void Run() => Run(Wum);

    void Wum()
    {
        Sine(A4).Curve(@"
             *

           *   *
          *       *       
        *                 *")
        .Play();
    }
}