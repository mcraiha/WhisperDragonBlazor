using System;
using System.ComponentModel.DataAnnotations;

public class CreateNewCommonSecretsContainerData
{
	private string password = "";
	[Required]
	public string Password 
	{ 
		get 
		{ 
			return password;
		}

		set 
		{
			password = value;
			PasswordChanged();
		}
	}

	public string RepeatPassword { get; set; } = "";

	public string PasswordEntropy { get; set; } = "";

	public string ShannonEntropy { get; set; } = "";

	#region Password entropies

	private void PasswordChanged()
	{
		UpdatePasswordEntropy(Password);
		UpdateShannonEntropy(Password);
	}

	private void UpdatePasswordEntropy(string pw)
	{
		int entropyInBits = EntropyCalcs.CalcutePasswordEntropy(pw);
		PasswordSecurityLevel level = EntropyCalcs.GetPasswordSecurityLevel(entropyInBits);
		this.PasswordEntropy = $"{entropyInBits} bits ({level})";
	}

	private void UpdateShannonEntropy(string pw)
	{
		double entropyInBits = EntropyCalcs.ShannonEntropy(pw);
		this.ShannonEntropy = $"{entropyInBits.ToString("F2")} bits";
	}

	#endregion // Password entropies
}