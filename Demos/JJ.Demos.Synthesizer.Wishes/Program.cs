new MySynth().Run();

class MySynth : SynthWishes
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