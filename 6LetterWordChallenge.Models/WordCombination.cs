namespace _6LetterWordChallenge.Models
{
	public class WordCombination(string word, List<string> parts)
	{
		public string Word { get; set; } = word;
		public List<string> Parts
		{
			get; set;
		} = parts;

		public override string ToString()
		{
			return $"{string.Join("+", Parts)}={Word}";
		}
	}
}
