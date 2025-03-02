using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Core.Services.Output
{
	public class LogOutputWriterService : IOutputWriterService
	{
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public void WriteLine(string output)
		{
			Console.WriteLine(output);
		}
	}
}
