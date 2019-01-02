using System.Reflection;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Data;
using JJ.Framework.Data.JsonBlobZip;

namespace JJ.Data.Synthesizer.JsonBlobZip
{
    public class SynthesizerJsonBlobZipContext : JsonBlobZipContext<Document>
    {
        [UsedImplicitly]
        public SynthesizerJsonBlobZipContext(string location, Assembly modelAssembly, Assembly mappingAssembly, string dialect)
            : base(location, modelAssembly, mappingAssembly, dialect)
        { }

        protected override void FillMemoryContext(Document document, IContext memoryContext)
        {
            memoryContext.Insert(document);
            memoryContext.Insert(document.AudioOutput);

            foreach (AudioFileOutput audioFileOutput in document.AudioFileOutputs)
            {
                memoryContext.Insert(audioFileOutput);
            }

            foreach (DocumentReference lowerDocumentReference in document.LowerDocumentReferences)
            {
                memoryContext.Insert(lowerDocumentReference);
            }

            foreach (DocumentReference higherDocumentReference in document.HigherDocumentReferences)
            {
                memoryContext.Insert(higherDocumentReference);
            }

            foreach (MidiMappingGroup midiMappingGroup in document.MidiMappingGroups)
            {
                memoryContext.Insert(midiMappingGroup);

                foreach (MidiMapping midiMapping in midiMappingGroup.MidiMappings)
                {
                    memoryContext.Insert(midiMapping);
                    memoryContext.Insert(midiMapping.EntityPosition);
                }
            }

            foreach (Patch patch in document.Patches)
            {
                memoryContext.Insert(patch);

                foreach (Operator op in patch.Operators)
                {
                    memoryContext.Insert(op);
                    memoryContext.Insert(op.EntityPosition);

                    foreach (Inlet inlet in op.Inlets)
                    {
                        memoryContext.Insert(inlet);
                    }

                    foreach (Outlet outlet in op.Outlets)
                    {
                        memoryContext.Insert(outlet);
                    }

                    if (op.Sample != null)
                    {
                        memoryContext.Insert(op.Sample);
                    }

                    if (op.Curve != null)
                    {
                        memoryContext.Insert(op.Curve);

                        foreach (Node node in op.Curve.Nodes)
                        {
                            memoryContext.Insert(node);
                        }
                    }
                }
            }

            foreach (Scale scale in document.Scales)
            {
                memoryContext.Insert(scale);

                foreach (Tone tone in scale.Tones)
                {
                    memoryContext.Insert(tone);
                }
            }
        }
    }
}