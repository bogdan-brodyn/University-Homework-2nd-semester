namespace MapFilterFold;

/// <summary>
/// Implementation of Map, Filter and Fold functions.
/// </summary>
public static class MapFilterFold
{
    /// <summary>
    /// Apply function to each element of enumerable collection.
    /// </summary>
    /// <typeparam name="TValue">Collection elements' type.</typeparam>
    /// <param name="sourceCollection">Source collection.</param>
    /// <param name="function">Function you want to apply to each element of collection.</param>
    /// <returns>Function application results list.</returns>
    public static List<TValue> Map<TValue>(
        IEnumerable<TValue> sourceCollection,
        Func<TValue, TValue> function)
    {
        var functionApplicationResultsList = new List<TValue>();
        foreach (var element in sourceCollection)
        {
            functionApplicationResultsList.Add(function(element));
        }

        return functionApplicationResultsList;
    }

    /// <summary>
    /// Filter collection elements by function application result.
    /// </summary>
    /// <typeparam name="TValue">Collection elements' type.</typeparam>
    /// <param name="sourceCollection">Source collection.</param>
    /// <param name="selector">Function to be applied to each element.</param>
    /// <returns>Source collection elements for which the function returned true.</returns>
    public static List<TValue> Filter<TValue>(
        IEnumerable<TValue> sourceCollection,
        Func<TValue, bool> selector)
    {
        var selectedElements = new List<TValue>();
        foreach (var element in sourceCollection)
        {
            if (selector(element))
            {
                selectedElements.Add(element);
            }
        }

        return selectedElements;
    }

    /// <summary>
    /// Updates the accumulated value by its current value and the current element of the collection for each element.
    /// </summary>
    /// <typeparam name="TCollectionValue">Collection elements' type.</typeparam>
    /// <typeparam name="TAccumulatedValue">Accumulated value type.</typeparam>
    /// <param name="sourceCollection">Source collection.</param>
    /// <param name="accumulatedValue">Start value you must to set.</param>
    /// <param name="accumulator">Fuction to update accumulated value by its current value and current collection element.</param>
    /// <returns>Finite accumulated value.</returns>
    public static TAccumulatedValue Fold<TCollectionValue, TAccumulatedValue>(
        IEnumerable<TCollectionValue> sourceCollection,
        TAccumulatedValue accumulatedValue,
        Func<TAccumulatedValue, TCollectionValue, TAccumulatedValue> accumulator)
    {
        foreach (var element in sourceCollection)
        {
            accumulatedValue = accumulator(accumulatedValue, element);
        }

        return accumulatedValue;
    }
}
