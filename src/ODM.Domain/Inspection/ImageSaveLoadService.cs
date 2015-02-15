using System;
using System.IO;
using System.Windows.Media.Imaging;
using Hdc;
using Hdc.Reflection;
using Microsoft.Practices.Unity;
using ODM.Domain.Configs;

namespace ODM.Domain.Inspection
{
    public class ImageSaveLoadService : IImageSaveLoadService
    {
        [Dependency]
        public IMachineConfigProvider MachineConfigProvider { get; set; }


        [InjectionMethod]
        public void Init()
        {
            if (!Directory.Exists(ImageStorePath))
            {
                Directory.CreateDirectory(ImageStorePath);
            }
        }

        public string ImageStorePath
        {
            get
            {
                var imageStorePath = this.GetType().Assembly
                    .GetAssemblyDirectoryPath()
                    .Combine(MachineConfigProvider.MachineConfig.Reporting_ImageStorePath);

                return imageStorePath;
            }
        }


        public StoredImageInfo StoreImage(string originalImageFileName)
        {
            var sii = StoreImage(newImageFileName => File.Copy(originalImageFileName, newImageFileName));
            sii.OriginalImageFilePathEnabled = true;
            sii.OriginalImageFilePath = originalImageFileName;
            return sii;
        }

        public StoredImageInfo StoreImage(Action<string> storeImageWithNewNameAction, string prefix = null,
                                          string ext = null)
        {
            var now = DateTime.Now;
            var subDir = now.Date.ToString("yyyy-MM-dd");
            var storePath = ImageStorePath;
            var subDirPath = Path.Combine(storePath, subDir);

            if (!Directory.Exists(subDirPath))
            {
                Directory.CreateDirectory(subDirPath);
            }

            Guid guid = Guid.NewGuid();
            var fileName = prefix + guid + ext;
            var newImagePath = Path.Combine(subDirPath, fileName);

            storeImageWithNewNameAction(newImagePath);

            var relativeFileName = Path.Combine(subDir, fileName);

            var sii = new StoredImageInfo()
            {
                ImageGuid = guid,
                StoredDateTime = now,
                OriginalImageFilePathEnabled = false,
                //                OriginalImageFilePath = imageFilePath,
                StoredImageFilePath = relativeFileName,
            };

            return sii;
        }

        public void DeleteImage(DateTime inspectDateTime, Guid imageId)
        {
            var subDir = inspectDateTime.Date.ToString("yyyy-MM-dd");
            var storePath = ImageStorePath;
            var subDirPath = Path.Combine(storePath, subDir);

            if (!Directory.Exists(subDirPath))
                return;

            var imageFullPath = Path.Combine(subDirPath, imageId.ToString());
            if (!File.Exists(imageFullPath))
                return;

            try
            {
                File.Delete(imageFullPath);

                if (Directory.GetFiles(subDirPath).Length == 0)
                {
                    Directory.Delete(subDirPath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public BitmapImage GetBitmapImage(DateTime inspectDateTime, Guid imageId)
        {
            var subDir = inspectDateTime.Date.ToString("yyyy-MM-dd");
            var storePath = ImageStorePath;
            var subDirPath = Path.Combine(storePath, subDir);

            if (!Directory.Exists(subDirPath))
                return null;

            var files = new DirectoryInfo(subDirPath).GetFiles();
            FileInfo fi = null;
            foreach (var fileInfo in files)
            {
                var nameOnly = fileInfo.Name.Replace(fileInfo.Extension, "");
                var parts = nameOnly.Split('_');
                var guid = parts[parts.Length - 1];
                if (guid == imageId.ToString())
                {
                    fi = fileInfo;
                }
            }

            if (fi == null) return null;
            //            var imageFullPath = Path.Combine(subDirPath, imageId.ToString());
            //            var imageFullPath = fi.FullName;
            //            imageFullPath += ".bmp";
            //            if (!File.Exists(imageFullPath))
            //                return null;

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri(fi.FullName, UriKind.RelativeOrAbsolute);
            bi.CacheOption = BitmapCacheOption.OnLoad;
            bi.EndInit();

            return bi;
        }

        public string GetBitmapImageFileName(DateTime inspectDateTime, Guid imageId)
        {
            var subDir = inspectDateTime.Date.ToString("yyyy-MM-dd");
            var storePath = ImageStorePath;
            var subDirPath = Path.Combine(storePath, subDir);

            if (!Directory.Exists(subDirPath))
                return null;

            var imageFullPath = Path.Combine(subDirPath, imageId.ToString());
            if (!File.Exists(imageFullPath))
                return null;

            return imageFullPath;
        }

        //        public string GetImageFullPath(DateTime inspectDateTime, Guid imageId)
        //        {
        //
        //        }
    }
}