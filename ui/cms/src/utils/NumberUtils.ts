export default class NumberUtils {
  public static safeParsePositiveNumber(text: string | undefined | null, defaultValue: number, minValue?: number, maxValue?: number) {
    if (text === undefined || text === null) return defaultValue;
    var val = parseInt(text, 10);
    if (isNaN(val)) return defaultValue;
    if (val < 0) return defaultValue;
    if (minValue && val < minValue) return defaultValue;
    if (maxValue && val > maxValue) return maxValue;
    return val;
  }
}
