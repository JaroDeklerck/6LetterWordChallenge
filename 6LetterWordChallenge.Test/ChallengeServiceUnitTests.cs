using _6LetterWordChallenge.Core.Services;
using _6LetterWordChallenge.Services;
using _6LetterWordChallenge.Services.Input;
using _6LetterWordChallenge.Services.Output;

namespace _6LetterWordChallenge.Test
{
	[TestClass]
	public class ChallengeServiceUnitTests
	{
		private static IEnumerable<object?[]> RunChallengeTestData
		{
			get
			{
				// No data
				yield return new object?[]
				{
					new List<string>(),
					4,
					new List<string>(),
					typeof(InvalidOperationException)
				};
				// No data for word length
				yield return new object?[]
				{
					new List<string> { "test", "word" },
					5,
					new List<string>(),
					null
				};
				// No parts for word length
				yield return new object?[]
				{
					new List<string> { "test" },
					4,
					new List<string>(),
					null
				};
				// Single word
				yield return new object?[]
				{
					new List<string> { "test", "t", "e", "s", "t" },
					4,
					new List<string> { "t+e+s+t=test" },
					null
				};
				// Multiple words
				yield return new object?[]
				{
					new List<string> { "test", "word", "t", "e", "s", "t", "w", "o", "r", "d" },
					4,
					new List<string> { "t+e+s+t=test", "w+o+r+d=word" },
					null
				};
				// Multiple words with multiple matches
				yield return new object?[]
				{
					new List<string> { "test", "stem", "t", "e", "s", "t", "e", "es", "te", "te", "m" },
					4,
					new List<string> { "t+e+s+t=test", "te+s+t=test", "s+t+e+m=stem", "s+te+m=stem", "t+es+t=test" },
					null
				};
				// Invalid word length
				yield return new object?[]
				{
					new List<string> { "test", "word" },
					1,
					new List<string>(),
					typeof(ArgumentException)
				};
				// Invalid word length 2
				yield return new object?[]
				{
					new List<string> { "test", "word" },
					0,
					new List<string>(),
					typeof(ArgumentException)
				};
				// Invalid word length 3
				yield return new object?[]
				{
					new List<string> { "test", "word" },
					-1,
					new List<string>(),
					typeof(ArgumentException)
				};
			}
		}
		[TestMethod]
		[DynamicData(nameof(RunChallengeTestData))]
		public async Task RunChallengeUnitTests(List<string> data, int wordLength, List<string> expectedResult, Type? expectedException)
		{
			var service = new ChallengeService();
			var dataReader = new MockDataReaderService(data.ToArray());
			var outputWriter = new MockOutputWriterService();
			var wordCombinationService = new WordCombinationService();
			if (expectedException != null)
			{
				try
				{
					await service.RunChallenge(dataReader, outputWriter, wordCombinationService, wordLength);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Expected exception: {expectedException.Name}");
					Console.WriteLine($"Actual exception: {ex.GetType().Name}");
					Assert.AreEqual(expectedException, ex.GetType());
					return;
				}
				Assert.Fail("Expected exception was not thrown");
				return;
			}
			await service.RunChallenge(dataReader, outputWriter, wordCombinationService, wordLength);
			var result = outputWriter.GetOutput();
			Console.WriteLine($"Expected: {string.Join(", ", expectedResult.Order())}");
			Console.WriteLine($"Result: {string.Join(", ", result.Select(r => r.ToString()).Order())}");
			try
			{
				Assert.AreEqual(expectedResult.Count, result.Count);
			}
			catch
			{
				Console.WriteLine($"Missing expected: {string.Join(", ", expectedResult.Where(r => !result.Contains(r)))}");
				Console.WriteLine($"Unexpected: {string.Join(", ", result.Where(r => !expectedResult.Contains(r)))}");
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
