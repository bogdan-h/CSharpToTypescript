using CSharpToTypescript;
using System;
using System.IO;

namespace ConvertCodeConsole
{
    class Program
    {
        private static ConvertorEngine convertor;

        static void Main(string[] args)
        {
            Console.WriteLine(@"Please select one of the options:
1. Enter the folder path for conversion.
2. Open the window app to convert a C# text.");

            string option = Console.ReadLine();
            if (option == "2")
            {
                ConvertCode.Program.Main();
            }
            else if (option == "1")
            {
                Console.WriteLine(@"Please enter the path to parse:");
                string path = Console.ReadLine();
                if (!Directory.Exists(path))
                    return;

                // the string SHOULD NOT END WITH '\'
                path = path.EndsWith("\\") ? path.Substring(0, path.Length - 1) : path;

                ParseFolder(path);
            }
            else
                return;
        }

        private static ConvertorEngine Convertor
        {
            get
            {
                if (convertor == null)
                    convertor = new ConvertorEngine();
                return convertor;
            }
        }

        private static void ParseFolder(string folderPath)
        {
            // get all files with cs extension
            string[] allFiles = Directory.GetFiles(folderPath, "*.cs");

            if (allFiles.Length > 0)
            {
                // create a folder with -ts in the name at the same level
                string currentDirName = Path.GetDirectoryName(folderPath + "\\");
                string newDirName = string.Format("{0}-ts", currentDirName);

                Directory.CreateDirectory(newDirName);


                for (int i = 0; i < allFiles.Length; i++)
                {
                    // get file content
                    string fileContent = File.ReadAllText(allFiles[i]);
                    if (string.IsNullOrWhiteSpace(fileContent))
                        continue;

                    string fileName = Path.GetFileNameWithoutExtension(allFiles[i]);

                    string tsFile = Convertor.Convert(fileContent, fileName);
                    if (tsFile.StartsWith("/* ERROR:"))
                        fileName = fileName + "-error"; // append so you can spot them faster !

                    string newFilePath = Path.Combine(newDirName, fileName + ".ts");
                    File.WriteAllText(newFilePath, tsFile);
                }
            }

            string[] allDirectories = Directory.GetDirectories(folderPath);
            if (allDirectories.Length > 0)
            {
                for (int i = 0; i < allDirectories.Length; i++)
                {
                    ParseFolder(allDirectories[i]);
                }
            }
        }
    }
}


