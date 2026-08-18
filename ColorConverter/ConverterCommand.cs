namespace ColorConverter;

using ConsoleAppFramework;
using Spectre.Console;

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
	public static void Command([Argument] string? color = null, Form? form = null) {
		HexColor? hexColor = null;

		if (color == null) {
			do {
				string response = AnsiConsole.Ask<string>("Enter Hex Code:");

				if (response == "") {
					Console.WriteLine("Exiting...");
					Environment.Exit(0);
				}

				try {
					hexColor = new HexColor(response);
				} catch {
					Console.Error.WriteLine("error: Must be a 3-digit or 6-digit hexadecimal optionally preceeded by a '#'.");
				}
			} while (hexColor == null);
		} else {
			try {
				hexColor = new(color);
			} catch {
				Console.Error.WriteLine("error: Color must be a 3-digit or 6-digit hexadecimal optionally preceeded by a '#'.");
				return;
			}
		}

		string bg = hexColor.GetLuminance() > 0.5 ? "black" : "white";
		AnsiConsole.MarkupLine($"Converting [{hexColor} on {bg}]{hexColor}[/]");

		form ??= Enum.Parse<Form>(AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.Title("Choose a conversion form:")
					.AddChoices(Enum.GetNames<Form>())));

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
