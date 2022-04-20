using NUnit.Framework;

namespace tests
{
	public class GenerateRandomPasswordTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void SimpleGenerateTest()
		{
			// Arrange
			const int wantedLength = 6;

			// Act
			string generated1 = GenerateRandomPassword.Generate(wantedLength, false, true, false, false, false);
			string generated2 = GenerateRandomPassword.Generate(wantedLength, false, true, false, false, false);

			// Assert
			Assert.AreEqual(wantedLength, generated1.Length);
			Assert.AreEqual(wantedLength, generated2.Length);
			Assert.AreNotEqual(generated1, generated2);
		}
	}
}