using JJ.Business.Synthesizer.Tests.ConfigTests;

// ReSharper disable once UnusedMember.Local

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class CaseExamples
    {
        static FrameCountWishesTests.Case[] _caseExamples =
        {
            // Example with all values specified
            new FrameCountWishesTests.Case
            {
                FrameCount     = { From = { Nully = 3 * 22050, Coalesced = 3 * 22050 }, To = { Nully = 5 * 22050, Coalesced = 5 * 22050 }},
                SamplingRate   = { From = { Nully =     22050, Coalesced =     22050 }, To = { Nully =     22050, Coalesced =     22050 }},
                AudioLength    = { From = { Nully = 3        , Coalesced = 3         }, To = { Nully = 5        , Coalesced = 5         }},
                CourtesyFrames = { From = { Nully = 4        , Coalesced = 4         }, To = { Nully = 4        , Coalesced = 4         }},
            },
            
            // Example with same value for Nully and Coalesced
            new FrameCountWishesTests.Case
            {
                FrameCount     = { From = 22050 * 3 , To = 22050 * 5 },
                SamplingRate   = { From = 22050     , To = 22050     },
                AudioLength    = { From =         3 , To =         5 },
                CourtesyFrames = { From = 4         , To = 4         },
            },
            
            // Example with single mentioning of values that don't change.
            new FrameCountWishesTests.Case
            {
                SamplingRate = 22050, CourtesyFrames = 4,
                AudioLength  = { From = 3         , To = 5         },
                FrameCount   = { From = 3 * 22050 , To = 5 * 22050 }
            },
            
            // Using inherited properties From and To to set main property FrameCount.
            new FrameCountWishesTests.Case
            {
                From = 3 * 22050 , To = 5 * 22050,
                AudioLength = { From = 3, To = 5 },
                SamplingRate = 22050, CourtesyFrames = 4,
            },
            
            // Example using constructor parameters for side-issues
            new FrameCountWishesTests.Case(courtesyFrames: 4)
            {
                FrameCount   = { From = 3 * 22050, To = 5 * 22050 },
                AudioLength  = { From = 3        , To = 5         },
                SamplingRate = 22050
            },
            
            // Examples initializing main property in constructor.
            new FrameCountWishesTests.Case(    48000           ),
            new FrameCountWishesTests.Case(    22050,       96000),
            new FrameCountWishesTests.Case(3       * 22050, 5     * 22050) { SamplingRate = 22050, AudioLength = { From = 3, To = 5 } },
            new FrameCountWishesTests.Case(from: 3 * 22050, to: 5 * 22050) { SamplingRate = 22050, AudioLength = { From = 3, To = 5 } }
        };

        // ncrunch: no coverage end
    }
}