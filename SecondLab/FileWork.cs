using System;
using System.IO;

namespace SecondLab
{
    static class FileWork
    {
        public static bool FileInput(ref String data)
        {
            String filePath = GetPath();
            try
            {
                using (StreamReader streamReader = new StreamReader(filePath))
                {
                    String encryptedData;
                    if ((encryptedData = streamReader.ReadLine()) != null)
                    {
                        data = encryptedData;
                    }
                    else
                    {
                        Console.WriteLine("Incorrect data, try to type something in file");
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Check your path");
                return false;
            }
            return true;
        }
        public static String GetPath()
        {
            Console.WriteLine("Enter path to file:");
            String path = Console.ReadLine();
            String filePath;
            try
            {
                filePath = Path.GetFullPath(path);
            }
            catch
            {
                Console.WriteLine("Check your path");
                filePath = GetPath();
            }
            return path;
        }

        public static String CreateFile()
        {
            String path = "";
            Boolean isSucced = false;
            while (!isSucced)
            {
                path = GetPath();
                FileStream fStream = null;
                try
                {
                    fStream = new FileStream(path, FileMode.CreateNew);
                    isSucced = true;
                }
                catch
                {
                    if (File.Exists(path))
                    {
                        if (Menu.AskForRewriting())
                        {
                            File.Delete(path);
                            fStream = new FileStream(path, FileMode.CreateNew);
                            isSucced = true;
                        }
                        else
                        {
                            isSucced = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unwated name or incorrect filetype");
                        isSucced = false;
                    }
                }
                if (fStream != null) try { fStream.Close(); } catch (Exception) { Console.WriteLine("Stream not closed!"); isSucced = false; }
            }
            return path;
        }
        public static void SaveToFile(String data)
        {
            String path = CreateFile();
            File.WriteAllText(path, data);
        }

    }
}