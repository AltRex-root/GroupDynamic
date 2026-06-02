using System.Text.RegularExpressions;

namespace GroupDynamic;

public static class SearchService
{
    public static SearchResult Find(string sourceText, string searchText, int startIndex, SearchOptions options)
    {
        return options.UseRegex
            ? FindByRegex(sourceText, searchText, startIndex, options)
            : FindBySimpleText(sourceText, searchText, startIndex, options);
    }

    private static SearchResult FindBySimpleText(string sourceText, string searchText, int startIndex, SearchOptions options)
    {
        StringComparison comparison = options.MatchCase
            ? StringComparison.CurrentCulture
            : StringComparison.CurrentCultureIgnoreCase;

        int currentIndex = Math.Max(0, startIndex);

        while (currentIndex <= sourceText.Length)
        {
            int foundIndex = sourceText.IndexOf(searchText, currentIndex, comparison);
            if (foundIndex < 0)
            {
                return SearchResult.NotFound();
            }

            if (!options.WholeWord || IsWholeWordMatch(sourceText, foundIndex, searchText.Length))
            {
                return SearchResult.Success(foundIndex, searchText.Length);
            }

            currentIndex = foundIndex + searchText.Length;
        }

        return SearchResult.NotFound();
    }

    private static SearchResult FindByRegex(string sourceText, string searchPattern, int startIndex, SearchOptions options)
    {
        string pattern = options.WholeWord ? $@"\b(?:{searchPattern})\b" : searchPattern;
        RegexOptions regexOptions = RegexOptions.Multiline;

        if (!options.MatchCase)
        {
            regexOptions |= RegexOptions.IgnoreCase;
        }

        try
        {
            Match match = Regex.Match(sourceText, pattern, regexOptions, TimeSpan.FromSeconds(1));
            while (match.Success && match.Index < startIndex)
            {
                match = match.NextMatch();
            }

            return match.Success
                ? SearchResult.Success(match.Index, match.Length)
                : SearchResult.NotFound();
        }
        catch (RegexParseException ex)
        {
            return SearchResult.InvalidPattern(ex.Message);
        }
    }

    private static bool IsWholeWordMatch(string sourceText, int startIndex, int length)
    {
        int leftIndex = startIndex - 1;
        int rightIndex = startIndex + length;

        bool hasLeftLetter = leftIndex >= 0 && IsWordCharacter(sourceText[leftIndex]);
        bool hasRightLetter = rightIndex < sourceText.Length && IsWordCharacter(sourceText[rightIndex]);

        return !hasLeftLetter && !hasRightLetter;
    }

    private static bool IsWordCharacter(char symbol)
    {
        return char.IsLetterOrDigit(symbol) || symbol == '_';
    }
}

public readonly record struct SearchOptions(bool MatchCase, bool WholeWord, bool UseRegex);

public readonly record struct SearchResult(int Start, int Length, bool Found, string? ErrorMessage)
{
    public static SearchResult Success(int start, int length)
    {
        return new SearchResult(start, length, true, null);
    }

    public static SearchResult NotFound()
    {
        return new SearchResult(-1, 0, false, null);
    }

    public static SearchResult InvalidPattern(string message)
    {
        return new SearchResult(-1, 0, false, message);
    }
}
