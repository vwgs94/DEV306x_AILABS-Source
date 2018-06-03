using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AIVisionExplorer.Helpers
{
    public static class StorageHelper
    {
        public async static Task<string> SaveToTemporaryFileAsync(string folderName, string fileName, byte[] imageBytes)
        {
            string tempFilePath = "";

            var folder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);

            var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteBytesAsync(file, imageBytes);

            tempFilePath = $"ms-appdata:///local/{folderName}/{file.Name}";

            return tempFilePath;
        }
    }
}
