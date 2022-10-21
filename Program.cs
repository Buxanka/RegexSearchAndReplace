using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.IO;

namespace _10._19
{
    class RegexSearch
    {
        public MatchCollection myRegex(string input, string pattern)
        {
            //pattern001 = @"\d{4}[./]\d{1,2}[./]\d{1,2}"; //YYYY.MM.DD
            //pattern002 = @"\d{1,2}[./]\d{1,2}[./]\d{4}"; //DD.MM.YYYY
            Regex regex001 = new Regex(pattern);
            MatchCollection match = regex001.Matches(input);
            return match;
        }
        public void PrintMatches(MatchCollection match)
        {
            foreach (var item in match)
            {
                Console.WriteLine(item);
            }
        }
        public string myReplace(string input, string pattern)
        {
            string result = "";
            result = Regex.Replace(input, pattern, "замена");
            return result;
        }
    }
    class Files
    {
        public string text;
        public string pattern;
        public string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        async public void ReadText(string args)
        {
            using (StreamReader reader = new StreamReader(path + "\\" + args))
            {
                text = await reader.ReadToEndAsync();
                //Console.WriteLine(text);
                reader.Close();
            }
            
        }
        async public void ReadPattern(string args)
        {
            using (StreamReader reader = new StreamReader(path + "\\" + args))
            {
                pattern = await reader.ReadToEndAsync();
                //Console.WriteLine(pattern);
                reader.Close();
            }
        }
        public void CreateDirectory()
        {
            DirectoryInfo _directory = new DirectoryInfo(path + "Regex");
            _directory.Create();
        }
        public void MoveFile(string NameFile)
        {
            File.Move(path + "\\" + NameFile, path + "\\Regex" + "\\" + NameFile);
        }
        public void WriteToFileMatch(MatchCollection match)
        {
            StreamWriter sw = new StreamWriter(path + "\\output.txt");
            foreach (var item in match)
            {
                sw.WriteLine(item);
            }
            sw.Close();
        }
        public void WritetoFileReplace(string input)
        {
            StreamWriter sw = new StreamWriter(path + "\\output.txt", true);
            sw.WriteLine(input);
            sw.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RegexSearch regexSearch = new RegexSearch();
            Files files = new Files();
            string pattern002 = "";
            try
            {
                files.ReadText(args[0]);
                files.ReadPattern(args[1]);
                try
                {
                    pattern002 = args[2];
                }
                catch
                {

                }
            }
            catch
            {
                Console.WriteLine("Введите текст: ");
                files.text = Console.ReadLine();
                Console.WriteLine("Введите шаблон поиска: ");
                files.pattern = Console.ReadLine();
            }
            regexSearch.PrintMatches(regexSearch.myRegex(files.text, files.pattern));
            files.CreateDirectory();
            files.MoveFile("input.txt");
            files.MoveFile("pattern.txt");
            files.WriteToFileMatch(regexSearch.myRegex(files.text, files.pattern));
            files.WritetoFileReplace(regexSearch.myReplace(files.text, files.pattern));
            files.MoveFile("output.txt");
            if (pattern002 != "")
            {
                regexSearch.PrintMatches(regexSearch.myRegex(files.text, pattern002));
            }
        }
    }
}
