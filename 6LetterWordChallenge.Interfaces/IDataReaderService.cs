namespace _6LetterWordChallenge.Interfaces
{
	public interface IDataReaderService
	{
		/// <summary>
		/// Read the line data from the source.
		/// </summary>
		/// <returns>The data read from the source.</returns>
		Task<string[]> ReadData();
	}
}
