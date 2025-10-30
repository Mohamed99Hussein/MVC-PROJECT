using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Presentation_Layer.AttachmentService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {

        public AttachmentService(IHostEnvironment webHost)
        {
            this.webHost = webHost;
        }

        private readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };
        private readonly long MaxFileSize = 5 * 1024 * 1024; // 5MB
        private readonly IHostEnvironment webHost;

        public string? Upload(string folderName, IFormFile file)
        {
            try
            {
                if (folderName is null || file is null || file.Length == 0) return null;

                if (file.Length > MaxFileSize) return null;

                var Extension = Path.GetExtension(file.FileName).ToLower();
                if (!AllowedExtensions.Contains(Extension)) return null;

                var FolderPath = Path.Combine(webHost.ContentRootPath, "images", folderName);

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                var FileName = Guid.NewGuid().ToString() + Extension;

                var FilePath = Path.Combine(FolderPath, FileName);

                using var FileStream = new FileStream(FilePath, FileMode.Create);
                file.CopyTo(FileStream);

                return FileName;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to Upload File to folder= {folderName} : {ex}");
                return null;
            }
        }


        public bool Delete(string fileName, string folderName)
        {
            try
            {
                if (String.IsNullOrEmpty(fileName) || String.IsNullOrEmpty(folderName))
                    return false;

                var FullPath = Path.Combine(webHost.ContentRootPath, "images", folderName, fileName);
                if (File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to delete file with name {fileName} : {ex}");

                return false;
            }
        }





    }


}



