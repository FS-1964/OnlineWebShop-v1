using System;
using System.IO;
using WebShop.PL.ViewModels;

namespace WebShop.PL.Util
{
    public class UpLoadImage : IDisposable
    {

        public string UpLoad(IViewModel rvm, string webrootpath, string uname)
        {
            string uniqueFielName = null;
            if (rvm.Photo != null)
            {
                uniqueFielName = rvm.Photo.FileName;
                var photoType = rvm.Photo.FileName.Split(new char[] { '.' });
                uniqueFielName = uname + "." + photoType[1];
                //uniqueFielName = Guid.NewGuid().ToString() + "_" + rvm.Photo.FileName;
                string uploadsFolder = Path.Combine(webrootpath, "Photo");
                string filePath = Path.Combine(uploadsFolder, uniqueFielName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    rvm.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFielName;

        }

        public void RemoveImage(string webRootPath, string imagePath)
        {
            string filePath = Path.Combine(webRootPath,
                     "Photo", imagePath);
            System.IO.File.Delete(filePath);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
