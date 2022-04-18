using System.Collections.Generic;
using System.Threading.Tasks;
using CSCommonSecrets;

public sealed class LoginSimplified
{
	// Non visible
	public int zeroBasedIndexNumber { get; set; }

	// Visible elements

	public bool IsSecure { get; set; }
	public string Title { get; set; }
	public string URL { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public string Notes { get; set; }
	public byte[] Icon { get; set; }
	public string Category { get; set; }
	public string Tags { get; set; }
	public string CreationTime { get; set; }
	public string ModificationTime { get; set; }

	private static readonly string dateTimeOffsetToStringConvert = "u";

	public static async Task<LoginSimplified[]> CreatLoginInformationSimplifieds(List<LoginInformation> loginInformations, List<LoginInformationSecret> loginInformationSecrets, ISecurityAsyncFunctions securityFunctions, IReadDerivedPassword derivedPasswords)
	{
		List<LoginSimplified> returnValues = new List<LoginSimplified>();

		for (int i = 0; i < loginInformations.Count; i++)
		{
			LoginSimplified noteSimplified = new LoginSimplified()
			{
				zeroBasedIndexNumber = i,
				IsSecure = false,
				Title = loginInformations[i].GetTitle(),
				URL = loginInformations[i].GetURL(),
				Email = loginInformations[i].GetEmail(),
				Username = loginInformations[i].GetUsername(),
				Password = loginInformations[i].GetPassword(),
				Notes = loginInformations[i].GetNotes(),
				Icon = loginInformations[i].GetIcon(),
				Category = loginInformations[i].GetCategory(),
				Tags = loginInformations[i].GetTags(),
				CreationTime = loginInformations[i].GetCreationTime().ToString(dateTimeOffsetToStringConvert),
				ModificationTime = loginInformations[i].GetModificationTime().ToString(dateTimeOffsetToStringConvert),
			};

			returnValues.Add(noteSimplified);
		}

		for (int i = 0; i < loginInformationSecrets.Count; i++)
		{
			string keyIdentifier = loginInformationSecrets[i].GetKeyIdentifier();
			if (derivedPasswords.DoesDerivedPasswordExist(keyIdentifier))
			{
				byte[] derivedPassword = derivedPasswords.GetDerivedPassword(keyIdentifier);
				LoginSimplified noteSimplified = new LoginSimplified()
				{
					zeroBasedIndexNumber = i,
					IsSecure = true,
					Title = await loginInformationSecrets[i].GetTitleAsync(derivedPassword, securityFunctions),
					URL = await loginInformationSecrets[i].GetURLAsync(derivedPassword, securityFunctions),
					Email = await loginInformationSecrets[i].GetEmailAsync(derivedPassword, securityFunctions),
					Username = await loginInformationSecrets[i].GetUsernameAsync(derivedPassword, securityFunctions),
					Password = await loginInformationSecrets[i].GetPasswordAsync(derivedPassword, securityFunctions),
					Notes = await loginInformationSecrets[i].GetNotesAsync(derivedPassword, securityFunctions),
					Icon = await loginInformationSecrets[i].GetIconAsync(derivedPassword, securityFunctions),
					Category = await loginInformationSecrets[i].GetCategoryAsync(derivedPassword, securityFunctions),
					Tags = await loginInformationSecrets[i].GetTagsAsync(derivedPassword, securityFunctions),
					CreationTime = (await loginInformationSecrets[i].GetCreationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
					ModificationTime = (await loginInformationSecrets[i].GetModificationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
				};

				returnValues.Add(noteSimplified);
			}
		}

		return returnValues.ToArray();
	}
}