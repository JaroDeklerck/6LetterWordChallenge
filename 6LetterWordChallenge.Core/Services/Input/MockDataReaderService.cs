using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Core.Services.Input
{
	public class MockDataReaderService(string[] data) : IDataReaderService
	{
		public Task<string[]> ReadData()
		{
			return Task.FromResult(data);
		}
	}
}
