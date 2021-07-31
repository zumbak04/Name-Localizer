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
        static string nameFilePath = @"\localization\english\names\wc_character_names_l_english.yml";
        static string dynastyFilePath = @"\localization\english\dynasties\wc_dynasty_names_l_english.yml";

        static string enclosedContentPattern = @"\s*=\s*\{)(?>\{(?<c>)|[^{}]+|\}(?<-c>))*(?(c)(?!))(?=\})";
        static string maleNames = "(?<=male_names";
        static string femaleNames = "(?<=female_names";
        static string cadetDynastyNames = "(?<=cadet_dynasty_names";
        static string dynastyNames = "(?<=dynasty_names";
        static string namePattern = @"[^\s""]+";
        static string historyNamePattern = @"(?<=\bname\b\s*=\s*""+)" + namePattern;

        static Regex notCommentedLineRegex = new Regex(@"(?<!#[^#]*)[^#]+");
        static Regex emptyLine = new Regex(@"^\s*\r\n");

        static readonly Encoding fileEncoding = Encoding.GetEncoding(65001);

        static string NewLine = Environment.NewLine;

        static public void LocalizeNames()
        {
            DirectoryInfo culturesFolder = new DirectoryInfo(CK3Path + culturesPath);
            DirectoryInfo charactersFolder = new DirectoryInfo(CK3Path + charactersPath);

            List<string> nameList = new List<string>();
            List<string> dynastyList = new List<string>();

            string nameNewFile = File.ReadAllText(CK3Path + nameFilePath);
            string dynastyNewFile = File.ReadAllText(CK3Path + dynastyFilePath);

            if (culturesFolder.Exists)
            {
                foreach (FileInfo file in culturesFolder.EnumerateFiles("*.txt"))
                {
                    List<string> outDynastyList;
                    nameList.AddRange(ReadNames(file, false, out outDynastyList));
                    dynastyList.AddRange(outDynastyList);
                }
            }
            if (charactersFolder.Exists)
            {
                foreach (FileInfo file in charactersFolder.EnumerateFiles("*.txt"))
                {
                    List<string> outDynastyList;
                    nameList.AddRange(ReadNames(file, true, out outDynastyList));
                    dynastyList.AddRange(outDynastyList);
                }
            }

            nameList = nameList.Distinct().ToList();

            Debug.WriteLine($"Found {nameList.Count} and {dynastyList.Count} dynast(y/ies) name(s) total");

            foreach (string name in nameList)
            {
                if (!nameNewFile.Contains($"{name}:"))
                    nameNewFile += $" {name}:0 \"{name}\"{NewLine}";
            }
            foreach (string dynasty in dynastyList)
            {
                if (!dynastyNewFile.Contains($"{dynasty}:"))
                    dynastyNewFile += $" {dynasty}:0 \"{dynasty}\"{NewLine}";
            }

            File.WriteAllText(CK3Path + nameFilePath, nameNewFile, fileEncoding);
            File.WriteAllText(CK3Path + dynastyFilePath, dynastyNewFile, fileEncoding);

            Debug.WriteLine($"Done!");
        }
        static List<string> ReadNames(FileInfo file, bool isHistory, out List<string> dynastyList)
        {
            string fileContent = "";
            string names = "";
            string dynasties = "";

            fileContent = FileReader.ReadIgnoringComments(File.ReadAllText(file.FullName));
            if (fileContent != null)
            {
                foreach(Match match in Regex.Matches(fileContent, maleNames + enclosedContentPattern))
                    names += match.Value + Environment.NewLine;
                foreach (Match match in Regex.Matches(fileContent, femaleNames + enclosedContentPattern))
                    names += match.Value + Environment.NewLine;
                if(isHistory)
                    foreach (Match match in Regex.Matches(fileContent, historyNamePattern))
                        names += match.Value + Environment.NewLine;
                foreach (Match match in Regex.Matches(fileContent, dynastyNames + enclosedContentPattern))
                    dynasties += match.Value + Environment.NewLine;
                foreach (Match match in Regex.Matches(fileContent, cadetDynastyNames + enclosedContentPattern))
                    dynasties += match.Value + Environment.NewLine;
            }

            List<string> nameList = Regex.Matches(names, namePattern).Cast<Match>().Select(match => match.Value).ToList();
            dynastyList = Regex.Matches(dynasties, namePattern).Cast<Match>().Select(match => match.Value).ToList();

            Debug.WriteLine($"Found {nameList.Count} name(s) and {dynastyList.Count} dynast(y/ies) in {file.Name}");

            return nameList;
        }
    }
}
