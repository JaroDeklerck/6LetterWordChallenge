using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Services.Output
{
	public class MockOutputWriterService : IOutputWriterService
	{
		private readonly List<string> lines = [];
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		public void WriteLine(string output)
		{
			lines.Add(output);
		}

		public List<string> GetOutput()
		{
			return lines;
		}
	}
}
