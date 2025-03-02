using _6LetterWordChallenge.Interfaces;
using _6LetterWordChallenge.Models;

namespace _6LetterWordChallenge.Services
{
	public class WordCombinationService : IWordCombinationService
	{
		public async Task<IEnumerable<WordCombination>> GetWordCombinations(IEnumerable<string> words, IDictionary<string, int> wordParts)
		{
			ArgumentNullException.ThrowIfNull(words, nameof(words));
			ArgumentNullException.ThrowIfNull(wordParts, nameof(wordParts));
			return (await Task.WhenAll(words.Select(word => GetWordCombinations(word, wordParts)))).SelectMany(r => r);
		}

		public async Task<IEnumerable<WordCombination>> GetWordCombinations(string word, IDictionary<string, int> wordParts)
		{
			ArgumentNullException.ThrowIfNull(word, nameof(word));
			ArgumentNullException.ThrowIfNull(wordParts, nameof(wordParts));
			var possibleParts = GetPossibleParts(word, wordParts);
			if (!possibleParts.Any())
				return [];
			var wordCombinations = new List<WordCombination>();
			foreach (var possiblePart in possibleParts)
			{
				var remainingWord = word[possiblePart.Length..];
				if (remainingWord.Length == 0)
					wordCombinations.Add(new WordCombination(word, [possiblePart]));
				else
				{
					var remainingLength = word.Length - possiblePart.Length;
					// Get the remaining possible parts
					// If the current part has multiple occurrences, we can use it again
					var remainingParts = wordParts
						.Where(part => part.Key.Length <= remainingLength && (part.Key != possiblePart || part.Value > 1))
						.ToDictionary(part => part.Key, part => part.Key == possiblePart ? part.Value - 1 : part.Value);
					var subWordCombinations = await GetWordCombinations(remainingWord, remainingParts);
					foreach (var subWordCombination in subWordCombinations)
					{
						var parts = new List<string> { possiblePart };
						parts.AddRange(subWordCombination.Parts);
						wordCombinations.Add(new WordCombination(word, parts));
					}
				}
			}
			return wordCombinations;
		}

		private IEnumerable<string> GetPossibleParts(string word, IDictionary<string, int> wordParts)
		{
			var possibleParts = new List<string>();
			foreach (var part in wordParts.Where(part => part.Value > 0))
				if (word.StartsWith(part.Key))
					possibleParts.Add(part.Key);
			return possibleParts;
		}
	}
}
