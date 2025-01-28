
            var tapeGroups = tapes.GroupBy(x => x.Channel)
                                  .Select(x => x.ToArray())
                                  .ToArray();
            
            var tasks = new Task[tapeGroups.Length];
            for (var i = 0; i < tapeGroups.Length; i++)
            {
                Tape[] tapeGroup = tapeGroups[i];
                tasks[i] = Task.Run(() => RunTapeLeafPipeline(tapeGroup));
            }
            
            Task.WaitAll(tasks);
            
            return tapes;
