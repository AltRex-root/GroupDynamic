using System.Text;

namespace GroupDynamic;

public static class DocumentSaveService
{
    private static readonly UTF8Encoding Utf8WithoutBom = new(encoderShouldEmitUTF8Identifier: false);

    public static void Save(string path, RichTextBox editor)
    {
        string extension = Path.GetExtension(path).ToLowerInvariant();

        if (extension == ".rtf")
        {
            editor.SaveFile(path, RichTextBoxStreamType.RichText);
            return;
        }

        File.WriteAllText(path, editor.Text, Utf8WithoutBom);
    }
}
