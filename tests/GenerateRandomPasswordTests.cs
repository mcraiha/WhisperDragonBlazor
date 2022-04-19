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
			string generated = GenerateRandomPassword.Generate(wantedLength, false, true, false, false, false);

			// Assert
			Assert.AreEqual(wantedLength, generated.Length);
		}
	}
}