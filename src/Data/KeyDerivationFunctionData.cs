using System;
using System.ComponentModel.DataAnnotations;
//using System.Security.Cryptography;

public class KeyDerivationFunctionData
{
	private byte[] salt = new byte[16];

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

	public KeyDerivationFunctionData()
	{
		this.GenerateSalt();
	}

	private void GenerateSalt()
	{
		// TODO: Remove unsafe way when doing actual release
		Random random = new Random();
		random.NextBytes(this.salt);
		/*using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
		{
			rng.GetBytes(this.salt);
		}*/
	}
}