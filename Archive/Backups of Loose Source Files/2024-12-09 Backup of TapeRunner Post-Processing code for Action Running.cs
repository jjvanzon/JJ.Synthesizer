
 private void ExecutePostProcessing_Old(IList<Tape> tapes)
 {
     // Run Channel Actions (in post-processing, that might otherwise hold up the taping tasks)
     foreach (Tape tape in tapes)
     {
         _channelTapeActionRunner.SaveIfNeeded(tape);
         _channelTapeActionRunner.PlayIfNeeded(tape);
     }
     
     if (_synthWishes.IsMono)
     {
         foreach (Tape tape in tapes)
         {
             _monoTapeActionRunner.RunActions(tape);
         }
     }
     
     if (_synthWishes.IsStereo)
     {
         var tapesWithActions = tapes.Where(x => x.IsPlay ||
                                                 x.IsSave ||
                                                 x.Callback != null).ToArray();

         var tapePairs = _stereoTapeMatcher.PairTapes(tapesWithActions);
         var stereoTapes = _stereoTapeRecombiner.RecombineChannelsConcurrent(tapePairs);
         
         foreach (Tape stereoTape in stereoTapes)
         {
             _stereoTapeActionRunner.RunActions(stereoTape);
         }
     }
 }