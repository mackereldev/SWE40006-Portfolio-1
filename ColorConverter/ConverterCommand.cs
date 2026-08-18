namespace ColorConverter;

using ConsoleAppFramework;

public static class ConverterCommand {
	public enum Form {
		RGB,
		HSL,
		HSV
	}

	/// <summary>
	/// Convert a hexadecimal color to another CSS-compatible form.
	/// </summary>
	/// <param name="color">The hexadecimal color to convert.</param>
	/// <param name="form">-f, The color form to convert to (one of RGB, HSL, or HSV).</param>
	public static void Command([Argument] string color, Form form) {
		HexColor hexColor;

		try {
			hexColor = new(color);
		} catch {
			Console.Error.WriteLine("error: Color must be a 3-digit or 6-digit hexadecimal optionally preceeded by a '#'.");
			return;
		}

		try {
			switch (form) {
				case Form.RGB:
					Console.WriteLine(hexColor.ToRgbString());
					break;
				case Form.HSL:
					Console.WriteLine(hexColor.ToHslString());
					break;
				case Form.HSV:
					Console.WriteLine(hexColor.ToHsvString());
					break;
			}
		} catch (Exception error) {
			Console.Error.WriteLine($"error: {error.Message}");
		}
	}
}
