using System;
using System.ComponentModel.DataAnnotations;

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

	[Required]
	public string Identifier { get; set; } = "Master";
}