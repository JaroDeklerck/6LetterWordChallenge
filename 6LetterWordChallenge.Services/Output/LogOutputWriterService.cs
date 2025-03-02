using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Services.Output
{
	public class LogOutputWriterService : IOutputWriterService
	{
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Writes a line to the console.
		/// </summary>
		/// <param name="output">The line to write</param>
		public void WriteLine(string output)
		{
			Console.WriteLine(output);
		}
	}
}
