using _6LetterWordChallenge.Interfaces;
namespace _6LetterWordChallenge.Services.Input
{
	public class FileDataReaderService(string filePath) : IDataReaderService
	{
		/// <summary>
		/// Reads the line data from the file.
		/// </summary>
		/// <returns>The line data+</returns>
		/// <exception cref="FileNotFoundException">Thrown when the file is not found</exception>
		public async Task<string[]> ReadData()
		{
			if (!File.Exists(filePath))
				throw new FileNotFoundException("File not found", filePath);
			using var fileStream = File.OpenRead(filePath);
			using var streamReader = new StreamReader(fileStream);
			var data = new List<string>();
			while (!streamReader.EndOfStream)
			{
				var line = await streamReader.ReadLineAsync();
				if (!string.IsNullOrWhiteSpace(line))
					data.Add(line.Trim());
			}
			return [.. data];
		}
	}
}
