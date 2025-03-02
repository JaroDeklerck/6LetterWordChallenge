using _6LetterWordChallenge.Services;

namespace _6LetterWordChallenge.Test
{
	[TestClass]
	public class WordCombinationServiceUnitTests
	{
		private static IEnumerable<object?[]> FindWordCombinationsTestData
		{
			get
			{
				// No matches
				yield return new object?[] {
					"test",
					new Dictionary<string, int> { { "t", 1 }, { "e", 1 }, { "s", 1 } },
					new List<string>()
				};
				// No matches, longer part than word
				yield return new object?[] {
					"test",
					new Dictionary<string, int> { { "tester", 1 } },
					new List<string>()
				};
				// Single match
				yield return new object?[] {
					"word",
					new Dictionary<string, int> { { "w", 1 }, { "o", 1 }, { "r", 1 }, { "d", 1 } },
					new List<string> { "w+o+r+d=word" }
				};
				// Single match, part is word
				yield return new object?[] {
					"word",
					new Dictionary<string, int> { { "word", 1 } },
					new List<string> { "word=word" }
				};
				// Single match, part is word, multiple occurrences
				yield return new object?[] {
					"word",
					new Dictionary<string, int> { { "word", 2 } },
					new List<string> { "word=word" }
				};
				// Single match, multiple occurrences
				yield return new object?[] {
					"test",
					new Dictionary<string, int> { { "t", 2 }, { "e", 1 }, { "s", 1 } },
					new List<string> { "t+e+s+t=test" }
				};
				// Multiple matches
				yield return new object?[] {
					"word",
					new Dictionary<string, int> { { "w", 1 }, { "o", 1 }, { "r", 1 }, { "d", 1 }, { "wor", 1 } },
					new List<string> { "w+o+r+d=word", "wor+d=word" }
				};
				// Multiple matches, multiple occurrences
				yield return new object?[] {
					"testee",
					new Dictionary<string, int> { { "t", 2 }, { "e", 2 }, { "s", 1 }, { "te", 1 }, { "es", 1 }, { "st", 1 } },
					new List<string> { "te+s+t+e+e=testee", "t+e+s+te+e=testee", "t+es+te+e=testee", "te+st+e+e=testee", "t+es+t+e+e=testee" }
				};
				// Multiple matches, a lot of occurrences
				yield return new object?[] {
					"testeeee",
					new Dictionary<string, int> { { "t", 5 }, { "e", 10 }, { "s", 1 }, { "te", 1 }, { "es", 1 }, { "st", 1 } },
					new List<string> { "t+e+s+t+e+e+e+e=testeeee", "t+e+st+e+e+e+e=testeeee", "te+s+t+e+e+e+e=testeeee", "t+e+s+te+e+e+e=testeeee", "t+es+te+e+e+e=testeeee", "te+st+e+e+e+e=testeeee", "t+es+t+e+e+e+e=testeeee" }
				};
				// With whitespace
				yield return new object?[] {
					"test with space",
					new Dictionary<string, int> { { "test with", 1 }, { " space", 1 }, { "test ", 1 }, { "with space", 1 } },
					new List<string> { "test with+ space=test with space", "test +with space=test with space" }
				};
				// With null word
				yield return new object?[] {
					null,
					new Dictionary<string, int> { { "w", 1 }, { "o", 1 }, { "r", 1 }, { "d", 1 } },
					null
				};
				// With null word parts
				yield return new object?[] {
					"word",
					null,
					null
				};
				// With negative or zero word part occurence count
				yield return new object?[] {
					"word",
					new Dictionary<string, int> { { "w", 0}, { "o", -1 }, { "r", 1 }, {"d", 1 } },
					new List<string>()
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(FindWordCombinationsTestData))]
		public async Task TestFindWordCombinations(string word, IDictionary<string, int> wordParts, List<string>? expectedResult)
		{
			var service = new WordCombinationService();
			if (expectedResult == null)
			{
				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.GetWordCombinations(word, wordParts));
				return;
			}
			var result = await service.GetWordCombinations(word, wordParts);
			var resultStrings = result.Select(r => r.ToString()).Order();
			Console.WriteLine($"Expected: {string.Join(", ", expectedResult.Order())}");
			Console.WriteLine($"Result: {string.Join(", ", result.Select(r => r.ToString()).Order())}");
			try
			{
				Assert.AreEqual(expectedResult.Count, result.Count());
			}
			catch
			{
				Console.WriteLine($"Missing expected: {string.Join(", ", expectedResult.Where(r => !resultStrings.Contains(r)))}");
				Console.WriteLine($"Unexpected: {string.Join(", ", resultStrings.Where(r => !expectedResult.Contains(r)))}");
				throw;
			}
			foreach (var wordCombination in result)
			{
				try
				{
					Assert.IsTrue(expectedResult.Contains(wordCombination.ToString()));
				}
				catch
				{
					Console.WriteLine($"Failed: {wordCombination}");
					throw;
				}
			}
		}
		private static IEnumerable<object?[]> FindWordCombinationsWithMultipleWordsTestData
		{
			get
			{
				// Single word, no matches
				yield return new object?[] {
					new List<string> { "test" },
					new Dictionary<string, int> { { "t", 1 }, { "e", 1 }, { "s", 1 } },
					new List<string>()
				};
				// Single word, single match
				yield return new object?[] {
					new List<string> { "test" },
					new Dictionary<string, int> { { "t", 2 }, { "e", 1 }, { "s", 1 } },
					new List<string> { "t+e+s+t=test" }
				};
				// Single word, multiple matches
				yield return new object?[] {
					new List<string> { "test" },
					new Dictionary<string, int> { { "t", 2 }, { "e", 1 }, { "s", 1 }, { "es", 1 } },
					new List<string> { "t+e+s+t=test", "t+es+t=test" }
				};
				// Multiple words, single match
				yield return new object?[] {
					new List<string> { "test", "word" },
					new Dictionary<string, int> { { "t", 2 }, { "e", 1 }, { "s", 1 } },
					new List<string> { "t+e+s+t=test" }
				};
				// Multiple words, multiple matches
				yield return new object?[] {
					new List<string> { "test", "testee" },
					new Dictionary<string, int> { { "t", 2 }, { "e", 2 }, { "s", 1 }, { "te", 1 }, { "es", 1 }, { "st", 1 } },
					new List<string> { "t+e+s+t=test", "t+es+t=test", "t+e+st=test", "te+s+t=test", "te+st=test", "te+s+t+e+e=testee", "t+e+s+te+e=testee", "t+es+te+e=testee", "te+st+e+e=testee", "t+es+t+e+e=testee" }
				};
				// With whitespace in parts
				yield return new object?[] {
					new List<string> { "test with space", "word with space" },
					new Dictionary<string, int> { { "test with", 1 }, { " space", 1 }, { "test ", 1 }, { "with space", 1 }, { "word", 1 } },
					new List<string> { "test with+ space=test with space", "test +with space=test with space" }
				};
				// With whitespace as part
				yield return new object?[] {
					new List<string> { "test with space", "word with space" },
					new Dictionary<string, int> { { "test", 1 }, { "with", 1 }, { "space", 1 }, { "word", 1 }, { " ", 5 } },
					new List<string> { "test+ +with+ +space=test with space", "word+ +with+ +space=word with space" }
				};
				// With null words
				yield return new object?[] {
					null,
					new Dictionary<string, int> { { "w", 1 }, { "o", 1 }, { "r", 1 }, { "d", 1 } },
					null
				};
				// With null word parts
				yield return new object?[] {
					new List<string> { "word" },
					null,
					null
				};
			}
		}

		[TestMethod]
		[DynamicData(nameof(FindWordCombinationsWithMultipleWordsTestData))]
		public async Task TestFindWordCombinationsWithMultipleWords(List<string> words, IDictionary<string, int> wordParts, List<string>? expectedResult)
		{
			var service = new WordCombinationService();
			if (expectedResult == null)
			{
				await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => service.GetWordCombinations(words, wordParts));
				return;
			}
			var result = await service.GetWordCombinations(words, wordParts);
			var resultStrings = result.Select(r => r.ToString()).Order();
			Console.WriteLine($"Expected: {string.Join(", ", expectedResult.Order())}");
			Console.WriteLine($"Result: {string.Join(", ", result.Select(r => r.ToString()).Order())}");
			try
			{
				Assert.AreEqual(expectedResult.Count, result.Count());
			}
			catch
			{
				Console.WriteLine($"Missing expected: {string.Join(", ", expectedResult.Where(r => !resultStrings.Contains(r)))}");
				Console.WriteLine($"Unexpected: {string.Join(", ", resultStrings.Where(r => !expectedResult.Contains(r)))}");
				throw;
			}
			foreach (var wordCombination in result)
			{
				try
				{
					Assert.IsTrue(expectedResult.Contains(wordCombination.ToString()));
				}
				catch
				{
					Console.WriteLine($"Failed: {wordCombination}");
					throw;
				}
			}
		}

	}
}
