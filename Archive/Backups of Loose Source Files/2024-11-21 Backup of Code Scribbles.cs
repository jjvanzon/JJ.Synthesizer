Play fallback:

                //if (UnderlyingAudioFileOutput != null &&
                //    UnderlyingAudioFileOutput.AudioFileOutputChannels != null &&
                //    UnderlyingAudioFileOutput.AudioFileOutputChannels.Count > 0)
                //{
                //    IList<Outlet> filledOutlets = UnderlyingAudioFileOutput.AudioFileOutputChannels
                //                                                           .Select(x => x.Outlet)
                //                                                           .Where(x => x != null)
                //                                                           .ToList();
                //    if (filledOutlets.Count > 0)
                //    {
                //        SynthWishes.StreamAudio(this, UnderlyingAudioFileOutput);
                //        //SynthWishes.InternalPlay(this, SynthWishes, UnderlyingAudioFileOutput);
                //    }
                //}


FluentCache_UsingTape_Test scribbles:
"
            //WithDiskCacheOn(); // Bug: Causes no bytes to be there.
            //Play(() => Sine(G4).Panning(0.2));
            //AreEqual(2, () => buffers.Count);
            //IsNotNull(() => buffers[0]);
            //IsNotNull(() => buffers[1]);
            
            // Causes index out of range on the buffers array.
            // While replacing the 1st Cache with Play does not.
            // Save is also good.
            Cache(() => Sine(G4).Panning(0.2).Cache(x => buffers.Add(x)));
