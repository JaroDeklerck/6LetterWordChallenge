using _6LetterWordChallenge.Models;

namespace _6LetterWordChallenge.Interfaces
{
	public interface IWordCombinationService
	{
		/// <summary>
		/// Gets all possible word combinations that can be formed from the given words and parts.
		/// </summary>
		/// <param name="words">The words to form.</param>
		/// <param name="wordParts">The parts to use to form the words.</param>
		/// <returns>The possible word combinations that can be formed.</returns>
		Task<IEnumerable<WordCombination>> GetWordCombinations(IEnumerable<string> words, IDictionary<string, int> wordParts);
		/// <summary>
		/// Gets all possible word combinations that can be formed from the given word and parts.
		/// </summary>
		/// <param name="word">The word to form.</param>
		/// <param name="wordParts">The parts to use to form the words.</param>
		/// <returns>The possible word combinations that can be formed.</returns>
		Task<IEnumerable<WordCombination>> GetWordCombinations(string word, IDictionary<string, int> wordParts);
	}
}
