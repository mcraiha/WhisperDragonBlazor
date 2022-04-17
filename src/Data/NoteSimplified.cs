using System.Collections.Generic;
using System.Threading.Tasks;
using CSCommonSecrets;

public sealed class NoteSimplified
{
    // Non visible, index is only for secure/non-secure so they aren't combined
    public int zeroBasedIndexNumber { get; set; }

    // Visible elements

    public bool IsSecure { get; set; }

    public string Title { get; set; }

    public string Text { get; set; }

    public string CreationTime { get; set; }

    public string ModificationTime { get; set; }

    public string KDFIdentifier { get; set; }

    private static readonly string dateTimeOffsetToStringConvert = "u";

    public static async Task<NoteSimplified[]> CreateNoteSimplifieds(List<Note> notes, List<NoteSecret> noteSecret, ISecurityAsyncFunctions securityFunctions, IReadDerivedPassword derivedPasswords)
    {
        List<NoteSimplified> returnValues = new List<NoteSimplified>();

        for (int i = 0; i < notes.Count; i++)
        {
            NoteSimplified noteSimplified = new NoteSimplified()
            {
                zeroBasedIndexNumber = i,
                IsSecure = false,
                Title = notes[i].GetNoteTitle(),
                Text = notes[i].GetNoteText(),
                CreationTime = notes[i].GetCreationTime().ToString(dateTimeOffsetToStringConvert),
                ModificationTime = notes[i].GetModificationTime().ToString(dateTimeOffsetToStringConvert),
            };

            returnValues.Add(noteSimplified);
        }

        for (int i = 0; i < noteSecret.Count; i++)
        {
            string keyIdentifier = noteSecret[i].GetKeyIdentifier();
            if (derivedPasswords.DoesDerivedPasswordExist(keyIdentifier))
            {
                byte[] derivedPassword = derivedPasswords.GetDerivedPassword(keyIdentifier);
                NoteSimplified noteSimplified = new NoteSimplified()
                {
                    zeroBasedIndexNumber = i,
                    IsSecure = true,
                    Title = await noteSecret[i].GetNoteTitleAsync(derivedPassword, securityFunctions),
                    Text = await noteSecret[i].GetNoteTextAsync(derivedPassword, securityFunctions),
                    CreationTime = (await noteSecret[i].GetCreationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
                    ModificationTime = (await noteSecret[i].GetModificationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
                };

                returnValues.Add(noteSimplified);
            }
        }

        return returnValues.ToArray();
    }
}