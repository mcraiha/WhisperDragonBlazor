using NUnit.Framework;

namespace tests
{
	public class GeneratePronounceablePasswordTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void SimpleGenerateTest()
		{
			// Arrange
			const int HowManyWords = 2;

			// Act
			string generated1 = GeneratePronounceablePassword.Generate(HowManyWords, false, false, false);
			string generated2 = GeneratePronounceablePassword.Generate(HowManyWords, false, false, false);

			// Assert
			Assert.Greater(generated1.Length, 3, "Two words should be at least 4 characters");
			Assert.Greater(generated2.Length, 3, "Two words should be at least 4 characters");
			Assert.AreNotEqual(generated1, generated2);
		}
	}
}