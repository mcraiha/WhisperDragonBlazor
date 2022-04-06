using System.Collections.Generic;
using CSCommonSecrets;

public sealed class NoteSimplified
{
    // Non visible
    public int zeroBasedIndexNumber { get; set; }

    // Visible elements

    public bool IsSecure { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public string CreationTime { get; set; }

    public string ModificationTime { get; set; }

    public string KDFIdentifier { get; set; }

    public static NoteSimplified[] CreateNoteSimplifieds(List<Note> notes)
    {
        NoteSimplified[] returnValues = new NoteSimplified[notes.Count];

        for (int i = 0; i < notes.Count; i++)
        {
            NoteSimplified noteSimplified = new NoteSimplified()
            {
                IsSecure = false,
                Title = notes[i].GetNoteTitle(),
                Text = notes[i].GetNoteText(),
            };

            returnValues[i] = noteSimplified;
        }

        return returnValues;
    }
}