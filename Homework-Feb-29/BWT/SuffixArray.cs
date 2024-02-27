namespace BWT;

static class SuffixArray
{
    public static List<int> BuildSuffixArray(string sourceString)
    {
        var suffixArray = new List<int>();
        for (var i = 0; i < sourceString.Length; ++i)
        {
            suffixArray.Add(i);
        }
        SortSuffixArray(sourceString, suffixArray);
        return suffixArray;
    }

    private static int CompareSuffix(string sourceString, 
        int suffixStartPos1, int suffixStartPos2)
    {
        for (int i = 0; i < sourceString.Length; ++i)
        {
            int charDiff = sourceString[(suffixStartPos1 + i) % sourceString.Length]
                        - sourceString[(suffixStartPos2 + i) % sourceString.Length];
            if (charDiff != 0)
            {
                return charDiff;
            }
        }
        return 0;
    }

    private static void SortSuffixArray(string sourceString, List<int> suffixArray)
    {
        for (int i = 0; i < suffixArray.Count; ++i)
        {
            for (int j = 1; j < suffixArray.Count - i; ++j)
            {
                if (CompareSuffix(sourceString, suffixArray[j-1], suffixArray[j]) > 0)
                {
                    (suffixArray[j-1], suffixArray[j]) = (suffixArray[j], suffixArray[j-1]);
                }
            }
        }
    }
}
