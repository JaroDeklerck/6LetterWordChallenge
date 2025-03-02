﻿using _6LetterWordChallenge.Interfaces;

namespace _6LetterWordChallenge.Core.Services
{
	public class ChallengeService : IChallengeService
	{
		/// <summary>
		/// Runs the word challenge by reading data, filtering words of a specified length,
		/// and identifying possible word parts that can be used to form words of that length.
		/// </summary>
		/// <param name="dataReader">The service used to read input data.</param>
		/// <param name="outputWriter">The service used to write output data.</param>
		/// <param name="wordLength">The length of the words to filter and form.</param>
		/// <exception cref="ArgumentException">Thrown when the word length is less than or equal to 1.</exception>
		/// <exception cref="InvalidOperationException">Thrown when no data is found.</exception>
		/// <returns>A task that represents the asynchronous operation.</returns>
		public async Task RunChallenge(IDataReaderService dataReader, IOutputWriterService outputWriter, int wordLength)
		{
			if (wordLength <= 1)
				throw new ArgumentException("Word length must be greater than 1");

			var data = await dataReader.ReadData();
			if (data.Length == 0)
				throw new InvalidOperationException("No data found");

			var words = data.Where(x => x.Length == wordLength).Distinct().ToList();
			if (words.Count == 0)
				// If there are no words, we can't create combinations
				return;
			words.Sort();

			// Get all possible parts with their count to know how many times we can use them for a word
			var wordParts = data.Where(x => x.Length < wordLength).Distinct().ToDictionary(part => part, part => data.Count(d => d == part));
			if (wordParts.Count == 0)
				// If there are no parts, we can't create combinations
				return;
		}
	}
}
