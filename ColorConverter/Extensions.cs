namespace ColorConverter;

public static class Extensions {
	extension(string str) {
		public string TrimFirst(char trimChar) => str.StartsWith(trimChar) ? str[1..] : str;
	}

	extension (double) {
		public static double Mod(double modulus, double divisor) {
			return (modulus % divisor + divisor) % divisor;
		}
	}
}
