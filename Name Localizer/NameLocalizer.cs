using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Linq;
using CK2_Character_Adapter;

namespace Name_Localizer
{
    static class NameLocalizer
    {
        static string CK3Path = @"C:\Users\zumba\Documents\Paradox Interactive\Crusader Kings III\mod\Warcraft-Guardians-of-Azeroth-2";
        static string culturesPath = @"\common\culture\cultures";
        static string charactersPath = @"\history\characters";
        static string namesPath = @"\localization\english\names\wc_character_names_l_english.yml";

        static string enclosedContentPattern = @"\s*=\s*\{)(?>\{(?<c>)|[^{}]+|\}(?<-c>))*(?(c)(?!))(?=\})";
        static string maleNames = "(?<=male_names";
        static string femaleNames = "(?<=female_names";
        static string namePattern = @"[^\s" + "^\"]+";
        static string historyNamePattern = @"(?<=\bname\b\s*=\s*" + "\"*)" + namePattern;

        static Regex notCommentedLineRegex = new Regex(@"(?<!#[^#]*)[^#]+");
        static Regex emptyLine = new Regex(@"^\s*\r\n");

        static readonly Encoding fileEncoding = Encoding.GetEncoding(65001);

        static string NewLine = Environment.NewLine;

        static public void LocalizeNames()
        {
            DirectoryInfo culturesFolder = new DirectoryInfo(CK3Path + culturesPath);
            DirectoryInfo charactersFolder = new DirectoryInfo(CK3Path + charactersPath);

            List<string> nameList = new List<string>();

            string newFile = File.ReadAllText(CK3Path + namesPath);

            if (culturesFolder.Exists)
            {
                foreach (FileInfo file in culturesFolder.EnumerateFiles("*.txt"))
                {
                    nameList.AddRange(ReadNames(file.FullName, false));
                }
            }
            if (charactersFolder.Exists)
            {
                foreach (FileInfo file in charactersFolder.EnumerateFiles("*.txt"))
                {
                    nameList.AddRange(ReadNames(file.FullName, true));
                }
            }

            //nameList.Sort();
            List<string> sortedList = nameList.Distinct().ToList();
            Debug.WriteLine($"Found {sortedList.Count} name(s) total");
            foreach (string name in sortedList)
            {
                if (!newFile.Contains($"\"{name}\""))
                {
                    newFile += $" {name}:0 \"{name}\"{NewLine}";
                }
            }

            File.WriteAllText(CK3Path + namesPath, newFile, fileEncoding);

            Debug.WriteLine($"Done!");
        }
        static List<string> ReadNames(string file, bool history)
        {
            string fileContent = "";
            string names = "";

            fileContent = FileReader.ReadIgnoringComments(File.ReadAllText(file));
            if (fileContent != null)
            {
                foreach(Match match in Regex.Matches(fileContent, maleNames + enclosedContentPattern))
                    names += match.Value + Environment.NewLine;
                foreach (Match match in Regex.Matches(fileContent, femaleNames + enclosedContentPattern))
                    names += match.Value + Environment.NewLine;
                if (history)
                {
                    foreach (Match match in Regex.Matches(fileContent, historyNamePattern))
                        names += match.Value + Environment.NewLine;
                }
            }

            Debug.WriteLine(names);

            List<string> nameList = Regex.Matches(names, namePattern).Cast<Match>().Select(match => match.Value).ToList();
            Debug.WriteLine($"Found {nameList.Count} name(s) in {file}");

            return nameList;
        }
    }
}
