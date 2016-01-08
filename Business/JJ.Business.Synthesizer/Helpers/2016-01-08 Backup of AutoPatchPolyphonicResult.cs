//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Reflection.Exceptions;

//namespace JJ.Business.Synthesizer.Helpers
//{
//    public class AutoPatchPolyphonicResult
//    {
//        private class Note
//        {
//            public Note()
//            {
//                int[] inletTypeEnumValues = (int[])Enum.GetValues(typeof(InletTypeEnum));
//                int maxInletTypeEnumValue = inletTypeEnumValues.Max();
//                InletType_To_ListIndex_Array = new int[maxInletTypeEnumValue];
//            }

//            public int[] InletType_To_ListIndex_Array { get; private set; }
//        }

//        private Note[] _notes;

//        public AutoPatchPolyphonicResult(
//            Outlet signalOutlet,
//            IList<string> volumeInletNames,
//            IList<string> frequencyInletNames,
//            IList<string> delayInletNames)
//        {
//            if (signalOutlet == null) throw new NullException(() => signalOutlet);
//            if (volumeInletNames == null) throw new NullException(() => volumeInletNames);
//            if (frequencyInletNames == null) throw new NullException(() => frequencyInletNames);
//            if (delayInletNames == null) throw new NullException(() => delayInletNames);

//            SignalOutlet = signalOutlet;
//            VolumeInletNames = volumeInletNames.ToArray();
//            FrequencyInletNames = frequencyInletNames.ToArray();
//            DelayInletNames = delayInletNames.ToArray();

//            int noteCount = 5; // TODO: Get the value from the outside.

//            _notes = new Note[noteCount];

//            for (int i = 0; i < noteCount; i++)
//            {
//                //_notes[i].InletType_To_ListIndex_Array[
//            }
//        }

//        public Outlet SignalOutlet { get; private set; }
//        public IList<string> VolumeInletNames { get; private set; }
//        public IList<string> FrequencyInletNames { get; private set; }
//        public IList<string> DelayInletNames { get; private set; }

//        public int GetInletListIndex(int value, InletTypeEnum frequency)
//        {

//            throw new NotImplementedException();
//        }
//    }
//}
