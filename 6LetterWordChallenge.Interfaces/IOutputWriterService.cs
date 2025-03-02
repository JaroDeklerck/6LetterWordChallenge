namespace _6LetterWordChallenge.Interfaces
{
	public interface IOutputWriterService : IDisposable
	{
		/// <summary>
		/// Writes a line of output.
		/// </summary>
		void WriteLine(string output);
	}
}
