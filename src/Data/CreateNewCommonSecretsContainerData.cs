using System;
using System.ComponentModel.DataAnnotations;

public class CreateNewCommonSecretsContainerData
{
	[Required]
	public string Password { get; set; } = "";

	public string RepeatPassword { get; set; } = "";
}