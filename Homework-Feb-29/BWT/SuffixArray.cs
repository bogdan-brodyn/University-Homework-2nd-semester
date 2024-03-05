namespace BWT;

public static class SuffixArray
{
    public static List<int> BuildSuffixArray(string sourceString)
    {
        var suffixArray = Enumerable.Range(0, sourceString.Length).ToList();
        SortSuffixArray(sourceString, suffixArray);

        return suffixArray;
    }

    private static int CompareSuffixes(
        string sourceString, 
        int firstSuffixStartPosition, 
        int secondSuffixStartPosition)
    {
        for (var i = 0; i < sourceString.Length; ++i)
        {

            int charDiff = sourceString[(firstSuffixStartPosition + i) % sourceString.Length]
                            - sourceString[(secondSuffixStartPosition + i) % sourceString.Length];
            if (charDiff != 0)
            {
                return charDiff;
            }
        }
        return 0;
    }

    private static void SortSuffixArray(string sourceString, List<int> suffixArray)
    {
        for (var i = 0; i < suffixArray.Count; ++i)
        {
            for (var j = 1; j < suffixArray.Count - i; ++j)
            {
                if (CompareSuffixes(sourceString, suffixArray[j - 1], suffixArray[j]) > 0)
                {
                    (suffixArray[j - 1], suffixArray[j]) = (suffixArray[j], suffixArray[j - 1]);
                }
            }
        }
    }
}
