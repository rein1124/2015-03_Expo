using System;
using System.Diagnostics;
using System.IO;

namespace Hdc.IO
{
    public static class IOExtensions
    {
        public static void CheckFileExist(this string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("file <" + path + "> does not exists");
        }

        public static void CheckDirectoryExist(this string path)
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("directory <" + path + "> does not exists");
        }

        public static bool IsFileExist(this string path)
        {
            return File.Exists(path);
        }

        public static bool IsFileNotExist(this string path)
        {
            return !IsFileExist(path);
        }

        public static bool IsDirectoryExist(this string path)
        {
            return Directory.Exists(path);
        }

        public static bool IsDirectoryNotExist(this string path)
        {
            return !IsDirectoryExist(path);
        }

        public static bool IsDirectoryExistAndContainFiles(this string path)
        {
            if (!Directory.Exists(path))
                return false;

            if (Directory.GetFiles(path).Length == 0)
                return false;

            return true;
        }

        /// <summary>
        /// Blocks until the file is not locked any more.
        /// </summary>
        /// <param name="fullPath"></param>
        public static bool WaitForFile(this string fullPath)
        {
            int numTries = 0;
            while (true)
            {
                ++numTries;
                try
                {
                    // Attempt to open the file exclusively.
                    using (FileStream fs = new FileStream(fullPath,
                        FileMode.Open, FileAccess.ReadWrite,
                        FileShare.None, 100))
                    {
                        fs.ReadByte();

                        // If we got this far the file is ready
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(
                       "WaitForFile {0} failed to get an exclusive lock: {1}",
                        fullPath, ex.ToString());

                    if (numTries > 10)
                    {
                        Debug.WriteLine(
                            "Error: WaitForFile {0} giving up after 10 tries",
                            fullPath);
                        return false;
                    }

                    // Wait for the lock to be released
                    System.Threading.Thread.Sleep(500);
                }
            }

            Debug.WriteLine("WaitForFile {0} returning true after {1} tries",
                fullPath, numTries);
            return true;
        }

    }
}