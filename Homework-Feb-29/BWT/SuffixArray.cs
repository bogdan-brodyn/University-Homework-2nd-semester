// <copyright file="SuffixArray.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BWT;

/// <summary>
/// Class that can build suffix array.
/// </summary>
public static class SuffixArray
{
    /// <summary>
    /// Build suffix array by the source string.
    /// </summary>
    /// <param name="sourceString">String by which you want to build a suffix array.</param>
    /// <returns>Suffix array.</returns>
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
