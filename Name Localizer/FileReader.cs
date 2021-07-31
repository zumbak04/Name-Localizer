using System.IO;
using System.Text.RegularExpressions;
using System;

namespace CK2_Character_Adapter
{
    public static class FileReader
    {
        static Regex notCommentedLineRegex = new Regex(@"(?<!#[^#]*)[^#]+");
        static Regex emptyLine = new Regex(@"^\s+$");
        static string newLine = Environment.NewLine;

        public static string ReadIgnoringComments(string inputText)
        {
            string outputText = null;
            string line;

            StringReader stringReader = new StringReader(inputText);
            while ((line = stringReader.ReadLine()) != null)
            {
                Match notCommentedLineMatch = notCommentedLineRegex.Match(line);
                if (notCommentedLineMatch.Success)
                {
                    if (!emptyLine.Match(notCommentedLineMatch.Value).Success)
                        outputText += notCommentedLineMatch.Value + newLine;
                }
            }

            return outputText;
        }
    }
}
