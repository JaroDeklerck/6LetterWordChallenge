using _6LetterWordChallenge.Core.Services;
using _6LetterWordChallenge.Services;
using _6LetterWordChallenge.Services.Input;
using _6LetterWordChallenge.Services.Output;

var dataReader = new FileDataReaderService("input.txt");
using var outputWriter = new FileOutputWriterService("output.txt", true);
var wordCombinationService = new WordCombinationService();

var challengeService = new ChallengeService();
await challengeService.RunChallenge(dataReader, outputWriter, wordCombinationService, 6);

