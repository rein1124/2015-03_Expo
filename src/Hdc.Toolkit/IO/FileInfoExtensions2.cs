using System.IO;

namespace Hdc.IO
{
    public static class FileInfoExtensions2
    {
        public static string GetFileNameWithoutExt(this string fileName, string ext)
        {
            return fileName.Substring(0, fileName.Length - ext.Length);
        } 

        public static string GetFileNameWithoutExt(this FileInfo fileInfo)
        {
            return GetFileNameWithoutExt(fileInfo.Name, fileInfo.Extension);
        }

        public static string GetFileName(this string fileName)
        {
            return Path.GetFileName(fileName);
        }

        public static string CombilePath(this string path1, string path2)
        {
            var p1 = path1 ?? string.Empty;
            var p2 = path2 ?? string.Empty;
            return Path.Combine(p1, p2);
        }

        public static string GetFileFullNameWithoutExt(this FileInfo fileInfo)
        {
            return GetFileNameWithoutExt(fileInfo.FullName, fileInfo.Extension);
        }

        public static string GetFileNameWithoutExt(this string fileName)
        {
            return GetFileNameWithoutExt(new FileInfo(fileName));
        }

        public static string GetFileFullNameWithoutExt(this string fileName)
        {
            return GetFileFullNameWithoutExt(new FileInfo(fileName));
        }

        public static string ReplaceFileFullNameExt(this string fileName, string newExt)
        {
            var fileFullNameWithoutExt = GetFileFullNameWithoutExt(fileName);
            fileFullNameWithoutExt += newExt;
            return fileFullNameWithoutExt;
        }

        public static string ReplaceFileNameExt(this string fileName, string newExt)
        {
            var fileNameWithoutExt = GetFileNameWithoutExt(fileName);
            fileNameWithoutExt += newExt;
            return fileNameWithoutExt;
        }

    }
}