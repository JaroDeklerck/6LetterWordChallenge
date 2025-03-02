using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Services.Output
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
			_writer.Dispose();
			_stream.Dispose();
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Writes a line to the output file.
		/// </summary>
		/// <param name="output">The line to write</param>
		public void WriteLine(string output)
		{
			_writer.WriteLine(output);
		}
	}
}
