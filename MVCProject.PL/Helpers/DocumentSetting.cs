using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace MVCProject.PL.Helpers
{
    public static class DocumentSetting
    {

        public static string UploadFile(IFormFile file,string FolderName)
        {
            //E:\Backend-Rout\MVC\MyDemo\MVCProject\MVCProject.PL\wwwroot\Files\Images\
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            string FilePath = Path.Combine(FolderPath, FileName);
            using var FS = new FileStream(FilePath, FileMode.Create);
            return FileName;
        }
        public static void DeleteFile(string fileName,string foldername)
        {
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", foldername, fileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
