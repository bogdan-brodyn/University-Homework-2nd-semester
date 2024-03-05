// <copyright file="BWT.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

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
        // Count char frequency in the transformed string (char := character)
        var charCountDictionary = new SortedDictionary<char, int>();
        foreach (var character in transformedString)
        {
            charCountDictionary.TryGetValue(character, out var count); // NB: count := 0 if key is not found
            charCountDictionary.Remove(character);
            charCountDictionary.Add(character, count + 1);
        }

        // Count chars in the transformed string that are less than current one
        var lessCharCounter = 0;
        var preffixSummDictionary = new SortedDictionary<char, int>();
        foreach (var (character, count) in charCountDictionary)
        {
            preffixSummDictionary.Add(character, lessCharCounter);
            lessCharCounter += count;
        }

        // Build reverse permutation
        var usedCharCountDictionary = new SortedDictionary<char, int>();
        var reversePermutation = new int[transformedString.Length];
        for (var i = 0; i < transformedString.Length; ++i)
        {
            var currentChar = transformedString[i];
            preffixSummDictionary.TryGetValue(currentChar, out var preffixSumm);
            usedCharCountDictionary.TryGetValue(currentChar, out var usedCharCount); // NB: usedCharCount := 0 if key is not found
            usedCharCountDictionary.Remove(currentChar);
            reversePermutation[preffixSumm + usedCharCount] = i;
            usedCharCountDictionary.Add(currentChar, usedCharCount + 1);
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
