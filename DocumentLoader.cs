using System.Text;
using UglyToad.PdfPig;

namespace GroupDynamic;

public static class DocumentLoader
{
    static DocumentLoader()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public static LoadedDocument Load(string path)
    {
        string extension = Path.GetExtension(path).ToLowerInvariant();

        if (extension == ".pdf")
        {
            return new LoadedDocument(LoadPdfText(path), true);
        }

        return new LoadedDocument(LoadTextFile(path), false);
    }

    private static string LoadTextFile(string path)
    {
        byte[] bytes = File.ReadAllBytes(path);

        if (bytes.Length >= 3 && bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
        {
            return Encoding.UTF8.GetString(bytes[3..]);
        }

        if (bytes.Length >= 2)
        {
            if (bytes[0] == 0xFF && bytes[1] == 0xFE)
            {
                return Encoding.Unicode.GetString(bytes[2..]);
            }

            if (bytes[0] == 0xFE && bytes[1] == 0xFF)
            {
                return Encoding.BigEndianUnicode.GetString(bytes[2..]);
            }
        }

        try
        {
            return new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true).GetString(bytes);
        }
        catch (DecoderFallbackException)
        {
            return Encoding.GetEncoding(1251).GetString(bytes);
        }
    }

    private static string LoadPdfText(string path)
    {
        StringBuilder builder = new();

        using PdfDocument document = PdfDocument.Open(path);

        foreach (var page in document.GetPages())
        {
            string pageText = page.Text;

            if (!string.IsNullOrWhiteSpace(pageText))
            {
                builder.AppendLine(pageText.Trim());
                builder.AppendLine();
            }
        }

        if (builder.Length == 0)
        {
            return "Текст из PDF извлечь не удалось. Возможно, документ состоит из сканов или изображений.";
        }

        return builder.ToString().TrimEnd();
    }
}

public record LoadedDocument(string Text, bool IsPdf);
