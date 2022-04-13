using System.Collections.Generic;
using System.Threading.Tasks;
using CSCommonSecrets;

public sealed class FileSimplified
{
    // Non visible
    public int zeroBasedIndexNumber { get; set; }

    // Visible elements

    public bool IsSecure { get; set; }

    public string Filename { get; set; }

    public string Filesize { get; set; }

    public string Filetype { get; set; }

    public string CreationTime { get; set; }

    public string ModificationTime { get; set; }

    private static readonly string dateTimeOffsetToStringConvert = "u";

    public static async Task<FileSimplified[]> CreateFileSimplifieds(List<FileEntry> files, List<FileEntrySecret> fileSecret, ISecurityAsyncFunctions securityFunctions, IReadDerivedPassword derivedPasswords)
    {
        List<FileSimplified> returnValues = new List<FileSimplified>();

        for (int i = 0; i < files.Count; i++)
        {
            FileSimplified fileeSimplified = new FileSimplified()
            {
                zeroBasedIndexNumber = i,
                IsSecure = false,
                Filename = files[i].GetFilename(),
                Filesize = files[i].GetFileContentLengthInBytes().ToString(),
                CreationTime = files[i].GetCreationTime().ToString(dateTimeOffsetToStringConvert),
                ModificationTime = files[i].GetModificationTime().ToString(dateTimeOffsetToStringConvert),
            };

            returnValues.Add(fileeSimplified);
        }

        for (int i = 0; i < fileSecret.Count; i++)
        {
            string keyIdentifier = fileSecret[i].GetKeyIdentifier();
            if (derivedPasswords.DoesDerivedPasswordExist(keyIdentifier))
            {
                byte[] derivedPassword = derivedPasswords.GetDerivedPassword(keyIdentifier);
                FileSimplified fileeSimplified = new FileSimplified()
                {
                    zeroBasedIndexNumber = i,
                    IsSecure = true,
                    Filename = await fileSecret[i].GetFilenameAsync(derivedPassword, securityFunctions),
                    Filesize = (await fileSecret[i].GetFileContentLengthInBytesAsync(derivedPassword, securityFunctions)).ToString(),
                    CreationTime = (await fileSecret[i].GetCreationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
                    ModificationTime = (await fileSecret[i].GetModificationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
                };

                returnValues.Add(fileeSimplified);
            }
        }

        return returnValues.ToArray();
    }
}