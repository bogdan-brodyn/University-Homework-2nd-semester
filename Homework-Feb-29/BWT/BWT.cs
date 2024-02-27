using System.Text;

namespace BWT;

class BWT
{
    public static Tuple<string, int> Transform(string sourceString)
    {
        var suffixArray = SuffixArray.BuildSuffixArray(sourceString);
        var transformResult = new StringBuilder();
        int sourceStringPos = 0;
        for (int i = 0; i < suffixArray.Count; ++i)
        {
            transformResult.Append(sourceString[
                (suffixArray[i] + sourceString.Length - 1) % sourceString.Length]);
            sourceStringPos = suffixArray[i] != 0 ? sourceStringPos : i + 1;
        }
        return new Tuple<string, int>(transformResult.ToString(), sourceStringPos);
    }
}
