using System.Collections.Generic;
using System.Threading.Tasks;
using CSCommonSecrets;

public sealed class ContactSimplified
{
    // Non visible
    public int zeroBasedIndexNumber { get; set; }

    // Visible elements

    public bool IsSecure { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Emails { get; set; }

    public string PhoneNumbers { get; set; }

    public string CreationTime { get; set; }

    public string ModificationTime { get; set; }

    private static readonly string dateTimeOffsetToStringConvert = "u";

    public static async Task<ContactSimplified[]> CreateContactSimplifieds(List<Contact> contacts, List<ContactSecret> contactSecrets, ISecurityAsyncFunctions securityFunctions, IReadDerivedPassword derivedPasswords)
	{
		List<ContactSimplified> returnValues = new List<ContactSimplified>();

		for (int i = 0; i < contacts.Count; i++)
		{
			ContactSimplified contactSimplified = new ContactSimplified()
			{
				zeroBasedIndexNumber = i,
				IsSecure = false,
				FirstName = contacts[i].GetFirstName(),
				LastName = contacts[i].GetLastName(),
                Emails = contacts[i].GetEmails(),
                PhoneNumbers = contacts[i].GetPhoneNumbers(),
				CreationTime = contacts[i].GetCreationTime().ToString(dateTimeOffsetToStringConvert),
				ModificationTime = contacts[i].GetModificationTime().ToString(dateTimeOffsetToStringConvert),
			};

			returnValues.Add(contactSimplified);
		}

		for (int i = 0; i < contactSecrets.Count; i++)
		{
			string keyIdentifier = contactSecrets[i].GetKeyIdentifier();
			if (derivedPasswords.DoesDerivedPasswordExist(keyIdentifier))
			{
				byte[] derivedPassword = derivedPasswords.GetDerivedPassword(keyIdentifier);
				ContactSimplified contactSimplified = new ContactSimplified()
				{
					zeroBasedIndexNumber = i,
					IsSecure = true,
					FirstName = await contactSecrets[i].GetFirstNameAsync(derivedPassword, securityFunctions),
                    LastName = await contactSecrets[i].GetLastNameAsync(derivedPassword, securityFunctions),
                    //Emails = await contactSecrets[i].GetEmailsAsync(derivedPassword, securityFunctions),
                    //PhoneNumbers = await contactSecrets[i].GetPhoneNumbersAsync(),
					CreationTime = (await contactSecrets[i].GetCreationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
					ModificationTime = (await contactSecrets[i].GetModificationTimeAsync(derivedPassword, securityFunctions)).ToString(dateTimeOffsetToStringConvert),
				};

				returnValues.Add(contactSimplified);
			}
		}

		return returnValues.ToArray();
	}
}