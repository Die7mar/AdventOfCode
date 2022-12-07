using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Challenges
{
    internal class DaySeven : IDayChallenge
    {
        public void Part1()
        {

            var loadedContent = ReadFilesAndFolders();
            var calcFolders = CalculateFolders(loadedContent.folders, loadedContent.files);
            long result = calcFolders.Where(x => x.Size <= 100000).Sum(x => x.Size);

            Console.WriteLine("Result: " + result);
        }

        public void Part2()
        {
            var loadedContent = ReadFilesAndFolders();
            var calcFolders = CalculateFolders(loadedContent.folders, loadedContent.files)
                                              .OrderBy(x => x.Size).ToList();

            long totalSpace = 70000000;
            long used = calcFolders[calcFolders.Count - 1].Size;
            long currentFreeSpace = totalSpace - used;
            long needFreeSpace = 30000000 - currentFreeSpace;
            long newFreeSpace = 0;

            foreach (var calcFolder in calcFolders)
            {
                if (needFreeSpace <= calcFolder.Size)
                {
                    newFreeSpace = calcFolder.Size;
                    break;
                }
            }

            Console.WriteLine("Result: " + newFreeSpace);
        }

        private (List<Files> files, List<string> folders) ReadFilesAndFolders()
        {
            var content = File.ReadAllLines("Inputs\\day7.txt");
            var currentDirectory = "";
            //List<Folder> folders = new List<Folder>();
            List<Files> files = new List<Files>();
            List<string> folders = new List<string>();

            foreach (var item in content)
            {
                if (item.StartsWith("$")) //Input
                {
                    if (item.Contains("$ cd"))
                    {
                        currentDirectory = ExecuteCd(currentDirectory, item);
                        currentDirectory = CleanUpString(currentDirectory);
                        //Console.WriteLine(item);
                        //Console.WriteLine(currentDirectory);
                        folders.Add(currentDirectory);
                    }
                }
                else //Output
                {
                    if (item.StartsWith("dir")) { continue; } // Folder 
                    else //File
                    {
                        string[] filePath = item.Split(" ");
                        files.Add(new Files()
                        {
                            FileName = filePath[1],
                            FileSize = Convert.ToInt64(filePath[0]),
                            Path = currentDirectory
                        });
                    }
                }
            }

            folders = folders.Distinct().ToList();
            folders.Sort();

            return (files, folders);
        }

        private string ExecuteCd(string currentDirectory, string cmd)
        {
            string cleanString = cmd.Replace("$ cd ", "");

            if (cleanString == "/") { return ""; }
            else if (cleanString == "..")
            {
                var parts = currentDirectory.Split('/');
                currentDirectory = "";
                for (int i = 0; i < parts.Length - 1; i++)
                {
                    if (!string.IsNullOrEmpty(parts[i])) { currentDirectory += "/" + parts[i]; }
                }

                if (string.IsNullOrEmpty(currentDirectory))
                {
                    currentDirectory = "/";
                    currentDirectory = currentDirectory.Replace("//", "/");
                }

                return currentDirectory;
            }
            else { return currentDirectory + "/" + cleanString; }

            throw new Exception();
        }

        private string CleanUpString(string input)
        {
            string output = input;
            if (string.IsNullOrEmpty(input)) { output = "/"; }

            return output.Replace("//", "/");
        }

        private List<FolderCalc> CalculateFolders(List<string> folders, List<Files> files)
        {
            List<FolderCalc> output = new List<FolderCalc>();

            foreach (var folder in folders)
            {
                long sum = 0;
                var filesToDo = files.Where(x => x.Path.Contains(folder));
                foreach (var file in filesToDo) { sum += file.FileSize; }
                //Console.WriteLine($"{folder} Sum: {sum}");
                output.Add(new FolderCalc() { Path = folder, Size = sum });
            }

            return output;
        }



        class FolderCalc
        {
            public string Path { get; set; }
            public long Size { get; set; }
        }

        class Files
        {
            public string FileName { get; set; }
            public long FileSize { get; set; }

            public string Path { get; set; }
        }
    }
}
