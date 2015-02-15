using System.IO;
using System.Threading.Tasks;

namespace Hdc.IO
{
    public class FileExtensions
    {
        /// <summary>
        /// http://stackoverflow.com/questions/882686/asynchronous-file-copy-move-in-c-sharp
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <returns></returns>
        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.Open(sourcePath,FileMode.Open))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }
    }
}