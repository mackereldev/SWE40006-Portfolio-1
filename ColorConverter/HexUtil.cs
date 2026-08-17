using System.Numerics;

namespace ColorConverter;

public static class HexUtil {
	public static readonly char[] HEX_CHARS = ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'];

	public static string AssertHexadecimal(string hex) {
		string trimmed = hex.Trim().TrimFirst('#').ToLower();
		if (!trimmed.All(HEX_CHARS.Contains))
			throw new ArgumentException("Must be a hexadecimal optionally preceeded by a '#'.");
		return trimmed;
	}

	public static bool IsHexadecimal(string hex) {
		try {
			AssertHexadecimal(hex);
			return true;
		} catch {
			return false;
		}
	}

	public static int ToDecimal(string hex) {
		hex = AssertHexadecimal(hex);
		int dec = (int)hex.Select((c, i) => BigInteger.Pow(16, hex.Length - i - 1) * HEX_CHARS.IndexOf(Char.ToLower(c))).Aggregate(BigInteger.Add);
		return dec;
	}
}
