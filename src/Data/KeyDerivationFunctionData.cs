using System;
using System.ComponentModel.DataAnnotations;
using CSCommonSecrets;

public class KeyDerivationFunctionData
{
	private readonly byte[] salt = new byte[16];

	public KeyDerivationPseudoRandomFunction SelectedPseudorandomFunction { get; set; } = KeyDerivationPseudoRandomFunction.HMACSHA256;

	public string Salt
	{
		get
		{
			return BitConverter.ToString(this.salt).Replace("-", string.Empty);
		}
	}

	[Range(1, 100_000_000, ErrorMessage = "Iterations Range invalid (1 - 100 000 000).")]
	public int Iterations { get; set; } = 100_000;

	[Required, MinLength(1)]
	public string Identifier { get; set; } = "Primary";

	public KeyDerivationFunctionData(ISecurityAsyncFunctions securityAsyncFunctions)
	{
		this.GenerateSalt(securityAsyncFunctions);
	}

	private void GenerateSalt(ISecurityAsyncFunctions securityAsyncFunctions)
	{
		securityAsyncFunctions.GenerateSecureRandomBytes(this.salt);
	}

	public byte[] GetSaltAsByteArray()
	{
		return this.salt;
	}
}