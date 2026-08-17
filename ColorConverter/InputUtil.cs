namespace ColorConverter;

public static class InputUtil {
	public static string Prompt(string prompt) {
		Console.Write(prompt);
		string response = Console.ReadLine() ?? "";
		return response;
	}

	public static string Prompt(string prompt, Func<string, bool> criteria, string? complaint = null) {
		string response;

		while (true) {
			Console.Write(prompt);
			response = Console.ReadLine() ?? "";

			if (criteria(response)) {
				break;
			} else if (complaint != null) {
				Console.Error.WriteLine($"error: {complaint}");
			}
		}

		return response;
	}

	public static bool IsInRange(string value, int min, int max) {
		if (Int32.TryParse(value, out int num)) {
			return num >= min && num <= max;
		} else {
			return false;
		}
	}
}
