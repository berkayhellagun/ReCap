using Core.Utilities.Helpers.GUIDHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelper : IFileHelper
    {
        public bool Delete(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }

        public string Update(IFormFile file, string path, string root)
        {
            Delete(path);
            return Upload(file, root);
        }

        public string Upload(IFormFile file, string root)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.FileName);
                string guid = GuidHelper.GetGuid();
                string fileName = guid + extension;

                using (FileStream fileStream = File.Create(root + fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return fileName;
                }
            }
            return null;
        }
    }
}
