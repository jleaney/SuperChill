using Random = System.Random;

public static class Utilities
{
	public static T GetRandom<T>(this T[] array)
	{
		var rand = new Random();
		return array[rand.Next(0, array.Length)];
	}
}
