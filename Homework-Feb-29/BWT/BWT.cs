namespace BWT;

using System.Text;

/// <summary>
/// Burrows–Wheeler transform.
/// </summary>
public static class BWT
{
    /// <summary>
    /// Direct Burrows–Wheeler transform.
    /// </summary>
    /// <param name="sourceString">String you want to transform.</param>
    /// <returns>Transformed string, source string position(is needed for reverse transform).</returns>
    public static (string, int) Transform(string sourceString)
    {
        var suffixArray = SuffixArray.BuildSuffixArray(sourceString);
        var transformResult = new StringBuilder();
        var sourceStringPosition = 0;
        for (var i = 0; i < suffixArray.Count; ++i)
        {
            var charIndex = suffixArray[i] + sourceString.Length - 1;
            charIndex %= sourceString.Length;
            transformResult.Append(sourceString[charIndex]);
            sourceStringPosition = suffixArray[i] != 0 ? sourceStringPosition : i;
        }

        return (transformResult.ToString(), sourceStringPosition);
    }

    /// <summary>
    /// Reverse Burrows–Wheeler transform.
    /// </summary>
    /// <param name="transformedString">String obtained by direct transform.</param>
    /// <param name="sourceStringPosition">Source string position obtained during the direct transform.</param>
    /// <returns>Source string.</returns>
    public static string ReverseTransform(string transformedString, int sourceStringPosition)
    {
        if (transformedString == string.Empty)
        {
            return string.Empty;
        }

        if (sourceStringPosition < 0 || sourceStringPosition >= transformedString.Length)
        {
            throw new Exception("Wrong source string position!");
        }

        var charCountDictionary = CountCharFrequency(transformedString);

        // Count chars in the transformed string that are less than current one
        var preffixSummDictionary = CountPreffixSumm(charCountDictionary);

        var reversePermutation = BuildReversePermutation(preffixSummDictionary, transformedString);

        return BuildSourceString(transformedString, sourceStringPosition, reversePermutation);
    }

    private static SortedDictionary<char, int> CountCharFrequency(string sourceString)
    {
        var charCountDictionary = new SortedDictionary<char, int>();
        foreach (var character in sourceString)
        {
            charCountDictionary.TryGetValue(character, out var count); // NB: count := 0 if key is not found
            charCountDictionary.Remove(character);
            charCountDictionary.Add(character, count + 1);
        }

        return charCountDictionary;
    }

    private static SortedDictionary<char, int> CountPreffixSumm(
        SortedDictionary<char, int> sourceDictionary)
    {
        var preffixSummDictionary = new SortedDictionary<char, int>();
        var lessCharCounter = 0;
        foreach (var (character, count) in sourceDictionary)
        {
            preffixSummDictionary.Add(character, lessCharCounter);
            lessCharCounter += count;
        }

        return preffixSummDictionary;
    }

    private static int[] BuildReversePermutation(
        SortedDictionary<char, int> preffixSummDictionary,
        string transformedString)
    {
        var reversePermutation = new int[transformedString.Length];
        var usedCharCountDictionary = new SortedDictionary<char, int>();
        for (var i = 0; i < transformedString.Length; ++i)
        {
            var currentChar = transformedString[i];
            preffixSummDictionary.TryGetValue(currentChar, out var preffixSumm);
            usedCharCountDictionary.TryGetValue(currentChar, out var usedCharCount); // NB: usedCharCount := 0 if key is not found
            usedCharCountDictionary.Remove(currentChar);
            reversePermutation[preffixSumm + usedCharCount] = i;
            usedCharCountDictionary.Add(currentChar, usedCharCount + 1);
        }

        return reversePermutation;
    }

    private static string BuildSourceString(
        string transformedString,
        int sourceStringPosition,
        int[] reversePermutation)
    {
        var sourceString = new char[transformedString.Length];
        for (var i = 0; i < transformedString.Length; ++i)
        {
            sourceString[i] = transformedString[reversePermutation[sourceStringPosition]];
            sourceStringPosition = reversePermutation[sourceStringPosition];
        }

        return string.Join(string.Empty, sourceString);
    }
}
