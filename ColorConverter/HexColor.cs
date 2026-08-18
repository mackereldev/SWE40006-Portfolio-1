namespace ColorConverter;

public class HexColor {
	public readonly string hex;

	public string RedComp => hex[..2];
	public string GreenComp => hex[2..4];
	public string BlueComp => hex[4..6];

	public HexColor(string hex) {
		hex = HexUtil.AssertHexadecimal(hex);

		if (hex.Length == 6) {
			this.hex = hex;
		} else if (hex.Length == 3) {
			this.hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}";
		} else {
			throw new ArgumentException("Must be 3 or 6 characters in length.");
		}
	}

	/// <summary>
	/// Get the luminance of this color as per the corrected WCAG 2.x definition. <see href="https://www.w3.org/WAI/GL/wiki/Relative_luminance"/>
	/// </summary>
	public double GetLuminance() {
		static double StandardToLinear(double standard) {
			if (standard <= 0.04045) {
				return standard / 12.92;
			} else {
				return Math.Pow(((standard + 0.055) / 1.055), 2.4);
			}
		}
		
		double sR = HexUtil.ToDecimal(RedComp) / 255.0;
		double sG = HexUtil.ToDecimal(GreenComp) / 255.0;
		double sB = HexUtil.ToDecimal(BlueComp) / 255.0;

		double lR = StandardToLinear(sR);
		double lG = StandardToLinear(sG);
		double lB = StandardToLinear(sB);

		return 0.2126 * lR + 0.7152 * lG + 0.0722 * lB;
	}

	public override string ToString() {
		return $"#{hex}";
	}

	public string ToRgbString() {
		return $"rgb({HexUtil.ToDecimal(RedComp)}, {HexUtil.ToDecimal(GreenComp)}, {HexUtil.ToDecimal(BlueComp)})";
	}

	public string ToHslString() {
		double R = HexUtil.ToDecimal(RedComp) / 255.0;
		double G = HexUtil.ToDecimal(GreenComp) / 255.0;
		double B = HexUtil.ToDecimal(BlueComp) / 255.0;

		double Cmax = Math.Max(Math.Max(R, G), B);
		double Cmin = Math.Min(Math.Min(R, G), B);
		double delta = Cmax - Cmin;

		int hue;
		if (delta == 0) {
			hue = 0;
		} else if (Cmax == R) {
			hue = (int)(60 * Double.Mod((G - B) / delta, 6));
		} else if (Cmax == G) {
			hue = (int)(60 * (((B - R) / delta) + 2));
		} else {
			hue = (int)(60 * (((R - G) / delta) + 4));
		}

		double lightness = (Cmax + Cmin) / 2;
		double saturation = delta == 0 ? 0 : delta / (1 - Math.Abs(2 * lightness - 1));

		return $"hsl({hue}, {(int)(saturation * 100)}%, {(int)(lightness * 100)}%)";
	}

	public string ToHsvString() {
		double R = HexUtil.ToDecimal(RedComp) / 255.0;
		double G = HexUtil.ToDecimal(GreenComp) / 255.0;
		double B = HexUtil.ToDecimal(BlueComp) / 255.0;

		double Cmax = Math.Max(Math.Max(R, G), B);
		double Cmin = Math.Min(Math.Min(R, G), B);
		double delta = Cmax - Cmin;

		int hue;
		if (delta == 0) {
			hue = 0;
		} else if (Cmax == R) {
			hue = (int)(60 * Double.Mod((G - B) / delta, 6));
		} else if (Cmax == G) {
			hue = (int)(60 * (((B - R) / delta) + 2));
		} else {
			hue = (int)(60 * (((R - G) / delta) + 4));
		}

		double saturation = Cmax == 0 ? 0 : delta / Cmax;
		double value = Cmax;

		return $"hsl({hue}, {(int)(saturation * 100)}%, {(int)(value * 100)}%)";
	}
}
