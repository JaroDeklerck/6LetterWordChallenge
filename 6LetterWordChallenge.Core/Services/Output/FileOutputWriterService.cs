using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Core.Services.Output
{
	public class FileOutputWriterService : IOutputWriterService
	{
		private readonly FileStream _stream;
		private readonly StreamWriter _writer;
		public FileOutputWriterService(string path, bool overwrite = false)
		{
			if (File.Exists(path) && !overwrite)
				throw new InvalidOperationException("File already exists");
			_stream = File.OpenWrite(path);
			_writer = new StreamWriter(_stream);
		}

		public void Dispose()
		{
			_stream.Dispose();
			_writer.Dispose();
			GC.SuppressFinalize(this);
		}

		public void WriteLine(string output)
		{
			_writer.WriteLine(output);
		}
	}
}
