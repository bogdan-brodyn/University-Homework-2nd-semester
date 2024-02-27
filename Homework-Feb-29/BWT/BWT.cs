using System.Text;

namespace BWT;

class BWT
{
    const int AlphabetSize = 256;

    public static Tuple<string, int> Transform(string sourceString)
    {
        var suffixArray = SuffixArray.BuildSuffixArray(sourceString);
        var transformResult = new StringBuilder();
        int sourceStringPos = 0;
        for (int i = 0; i < suffixArray.Count; ++i)
        {
            transformResult.Append(sourceString[
                (suffixArray[i] + sourceString.Length - 1) % sourceString.Length]);
            sourceStringPos = suffixArray[i] != 0 ? sourceStringPos : i;
        }
        return new Tuple<string, int>(transformResult.ToString(), sourceStringPos);
    }

    public static string ReverseTransform(Tuple<string, int> directTransformResult)
    {
        return ReverseTransform(directTransformResult.Item1, directTransformResult.Item2);
    }

    public static string ReverseTransform(string transformedString, int suffixArrayPos)
    {
        // Count char frequency in the transformed string (char := character)
        var charCountArray = new int[AlphabetSize];
        foreach (var character in transformedString)
        {
            ++charCountArray[character];
        }
        // Count chars in the transformed string that are less than current one
        var preffixSummArray = new int[AlphabetSize];
        for (int currentChar = 1; currentChar < AlphabetSize; ++currentChar)
        {
            preffixSummArray[currentChar] = preffixSummArray[currentChar - 1] 
                                            + charCountArray[currentChar - 1];
        }
        // Build reverse permutation
        var usedCharCountArray = new int[AlphabetSize];
        var reversePermutation = new int[transformedString.Length];
        for (int i = 0; i < transformedString.Length; ++i)
        {
            var currentChar = transformedString[i];
            reversePermutation[preffixSummArray[currentChar] 
                                        + usedCharCountArray[currentChar]] = i;
            ++usedCharCountArray[currentChar];
        }
        // Build source string
        var sourceString = new char[transformedString.Length];
        for (int i = 0; i < transformedString.Length; ++i)
        {
            sourceString[i] = transformedString[reversePermutation[suffixArrayPos]];
            suffixArrayPos = reversePermutation[suffixArrayPos];
        }
        return string.Join("", sourceString);
    }
}
