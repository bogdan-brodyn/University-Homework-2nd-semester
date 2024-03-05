namespace BWT;

using System.Text;

public static class BWT
{
    private const int AlphabetSize = 256;

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

    public static string ReverseTransform(string transformedString, int sourceStringPosition)
    {
        // Count char frequency in the transformed string (char := character)
        var charCountArray = new int[AlphabetSize];
        foreach (var character in transformedString)
        {
            ++charCountArray[character];
        }

        // Count chars in the transformed string that are less than current one
        var preffixSummArray = new int[AlphabetSize];
        for (var currentChar = 1; currentChar < AlphabetSize; ++currentChar)
        {
            preffixSummArray[currentChar] = preffixSummArray[currentChar - 1]
                                            + charCountArray[currentChar - 1];
        }

        // Build reverse permutation
        var usedCharCountArray = new int[AlphabetSize];
        var reversePermutation = new int[transformedString.Length];
        for (var i = 0; i < transformedString.Length; ++i)
        {
            var currentChar = transformedString[i];
            reversePermutation[preffixSummArray[currentChar]
                                + usedCharCountArray[currentChar]] = i;
            ++usedCharCountArray[currentChar];
        }

        // Build source string
        var sourceString = new char[transformedString.Length];
        for (var i = 0; i < transformedString.Length; ++i)
        {
            sourceString[i] = transformedString[reversePermutation[sourceStringPosition]];
            sourceStringPosition = reversePermutation[sourceStringPosition];
        }

        return string.Join(string.Empty, sourceString);
    }
}
