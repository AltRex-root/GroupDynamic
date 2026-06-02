namespace GroupDynamic;

public readonly record struct DocumentStatistics(int Lines, int Symbols)
{
    public static DocumentStatistics From(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new DocumentStatistics(1, 0);
        }

        int lines = 1;
        foreach (char character in text)
        {
            lines += character == '\n' ? 1 : 0;
        }

        return new DocumentStatistics(lines, text.Length);
    }
}
