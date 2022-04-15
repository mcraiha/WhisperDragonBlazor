using System;
using System.Collections.Generic;
using System.Security.Cryptography;

public static class GeneratePronounceablePassword
{
	private static readonly List<char> specialCharactersPronounceable = new List<char>()
	{
		'!', '#', '$', '%', '*', '+', '-', '.', '?', '@', '_',
	};

	private static readonly List<(string, string)> languageToCommonWords = new List<(string, string)>()
	{
		("English", "CommonWords/English-Common.txt")
	};

	public static string Generate(int HowManyWords, bool StartWithUpperCase, bool IncludeNumbers, bool IncludeSpecialCharSimple)
	{
		List<string> commonWords = EmbedResourceLoader.ReadAsList(languageToCommonWords[0].Item2);

        string currentPronounceablePassword = "";

        int wordCount = HowManyWords; 

        using (var generator = RandomNumberGenerator.Create())
        {
            int bigIndex = GetPositiveRandomInt(generator);
            int smallIndex = bigIndex % commonWords.Count;

            string firstWord = commonWords[smallIndex];

            if (StartWithUpperCase)
            {
                firstWord = char.ToUpper(firstWord[0]) + firstWord.Substring(1);
            }

            currentPronounceablePassword = firstWord;

            for (int i = 1; i < wordCount; i++)
            {
                bigIndex = GetPositiveRandomInt(generator);
                smallIndex = bigIndex % commonWords.Count;
                string tempWord = commonWords[smallIndex];

                currentPronounceablePassword += tempWord;
            }

            if (IncludeNumbers)
            {
                bigIndex = GetPositiveRandomInt(generator);
                smallIndex = bigIndex % 99;
                currentPronounceablePassword += smallIndex;
            }

            if (IncludeSpecialCharSimple)
            {
                int index = GetPositiveRandomInt(generator) % specialCharactersPronounceable.Count;
                currentPronounceablePassword += specialCharactersPronounceable[index];
            }		
        }

        return currentPronounceablePassword;
	}

	private static int GetPositiveRandomInt(RandomNumberGenerator rng)
	{
		int returnValue = -1;

		byte[] byteArray = new byte[4];

		while (returnValue < 0)
		{
			rng.GetBytes(byteArray);
			returnValue = BitConverter.ToInt32(byteArray, 0);
		}

		return returnValue;
	}
}