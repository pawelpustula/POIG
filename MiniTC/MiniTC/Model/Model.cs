using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace MiniTC.Model
{
    class Model
    {
        public string[] GetDrives()
        {
            return Directory.GetLogicalDrives();
        }

        public string[] GetFiles(string path)
        {
            List<string> AllFiles = new List<string>();
            try
            {
                if (Directory.GetParent(path) != null)
                {
                    AllFiles.Add("...");
                }
                string[] Directories = Directory.GetDirectories(path);
                string[] Files = Directory.GetFiles(path);

                for (int i = 0; i < Directories.Length; i++)
                {
                    AllFiles.Add("<D>" + Path.GetFileName(Directories[i]));
                }

                for (int j = 0; j < Files.Length; j++)
                {
                    AllFiles.Add(Path.GetFileName(Files[j]));
                }

            }
            catch { }
            return AllFiles.ToArray();
        }


        public string ChangePath(string path, string selectedFile)
        {
            if (selectedFile != null && selectedFile.Substring(0, 3) == "<D>"
                && selectedFile != "...")
            {
                selectedFile = selectedFile.Replace("<D>", "");
                string newPath = Path.Combine(path, selectedFile);
                return newPath;
            }
            else if (selectedFile == "...")
            {
                path = GetParentOfFile(path);
            }

            return path;
        }

        public static string GetParentOfFile(string path)
        {
            return Directory.GetParent(path).FullName;
        }

        public void CopyFile(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
            }
            catch { }
        }
    }
}
