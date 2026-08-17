namespace ColorConverter;

internal class Program {
	static void Main() {
		while (true) {
			HexColor? color = null;
			do {
				string response = InputUtil.Prompt("Enter Hex Code: ");

				if (response == "") {
					Console.WriteLine("Exiting...");
					Environment.Exit(0);
				}

				try {
					color = new HexColor(response);
				} catch {
					Console.Error.WriteLine("error: Must be a 3-digit or 6-digit hexadecimal optionally preceeded by a '#'.");
				}
			} while (color == null);

			Console.WriteLine($"Convert {color} to...");
			Console.WriteLine("1: RGB");
			Console.WriteLine("2: HSL");
			Console.WriteLine("3: HSV");
			string option = InputUtil.Prompt("Enter Choice: ", (response) => InputUtil.IsInRange(response, 1, 3), "Must select option 1, 2, or 3.");
			int optionNum = Int32.Parse(option);

			try {
				switch (optionNum) {
					case 1:
						Console.WriteLine(color.ToRgbString());
						break;
					case 2:
						Console.WriteLine(color.ToHslString());
						break;
					case 3:
						Console.WriteLine(color.ToHsvString());
						break;
				}
				Console.WriteLine();
			} catch (Exception error) {
				Console.Error.WriteLine($"error: {error.Message}");
			}
		}
	}
}
