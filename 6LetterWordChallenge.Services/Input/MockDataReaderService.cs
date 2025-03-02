using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Services.Input
{
	public class MockDataReaderService(string[] data) : IDataReaderService
	{
		/// <summary>
		/// Returns the mock data.
		/// </summary>
		/// <returns>The mock data</returns>
		public Task<string[]> ReadData()
		{
			return Task.FromResult(data);
		}
	}
}
