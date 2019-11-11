using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DoctorsOffice.Data;
using DoctorsOffice.DbContexts;
using DoctorsOffice.ViewModels;
using DoctorsOffice.Translators;


namespace DoctorsOffice.Helpers
{
    public class FileManipulation
    {
        public string GetExtension(string contentType)
        {
            if (contentType.Equals("image/jpeg", StringComparison.CurrentCultureIgnoreCase))
            {
                return ".jpg";
            }
            else if (contentType.Equals("image/gif", StringComparison.CurrentCultureIgnoreCase))
            {
                return ".gif";
            }
            else if (contentType.Equals("image/png", StringComparison.CurrentCultureIgnoreCase))
            {
                return ".png";
            }
            else if (contentType.Equals("file/nii", StringComparison.CurrentCultureIgnoreCase))
            {
                return ".nii";
            }
            else if (contentType.Equals("file/dcm", StringComparison.CurrentCultureIgnoreCase))
            {
                return ".dcm";
            }
            else if (contentType.Equals("file/dsr", StringComparison.CurrentCultureIgnoreCase))
            {
                return ".dsr";
            }
            else
            {
                return ".bin";
            }
        }

        //Not necessary for now
        //public static class TypeOfFileConversions
        //{
        //    public static string ToViewFormat(TypeOfFile typeOfFile)
        //    {
        //        switch (typeOfFile)
        //        {
        //            case TypeOfFile.IMAGE: return "Image";
        //            case TypeOfFile.ULTRASOUND_RECORD: return "Ultrasound records";
        //            case TypeOfFile.MRI_RECORD: return "MRI or similar records";
        //            case TypeOfFile.TEXT_FILE: return "Text document";
        //            case TypeOfFile.MISC: return "Other files...";
        //            default: return "Choose format of the file";
        //        }
        //    }
        //}

        //Not necessary for now
        //public void ValidFileType (File file)
        //{
        //    string[] validFileTypes;
        //    if (file.TypeOfFile == TypeOfFile.IMAGE)
        //    {
        //        validFileTypes = new string[]
        //        {
        //            "image/gif",
        //            "image/jpeg",
        //            "image/png"
        //        };
        //    }
        //    else if (file.TypeOfFile == TypeOfFile.ULTRASOUND_RECORD)
        //    {
        //        validFileTypes = new string[]
        //        {
        //            "dsr",
        //            "deff",
        //            "dcm",
        //            "mvl"
        //        };
        //    }
        //    else if (file.TypeOfFile == TypeOfFile.MRI_RECORD)
        //    {
        //        validFileTypes = new string[]
        //        {
        //            "dcm",
        //            "nii"
        //        };
        //    }
        //    else if (file.TypeOfFile == TypeOfFile.TEXT_FILE)
        //    {
        //        validFileTypes = new string[]
        //        {
        //            "txt",
        //            "doc",
        //            "odt",
        //            "rtf",
        //            "pdf"
        //        };
        //    }
        //    else
        //    {
        //        validFileTypes = new string[]
        //        {
        //            "xls",
        //            "xlsx",
        //            "xml"
        //        };
        //    }
        //}
        
        public void FileUpload(File file, HttpPostedFileBase fileUpload)
        {
            //var imgFileName = Guid.NewGuid().ToString() + GetExtension(imageUpload.ContentType);
            using (var reader = new System.IO.BinaryReader(fileUpload.InputStream))
            {
                file.Content = (reader.ReadBytes(fileUpload.ContentLength));
            }

            file.FileName = fileUpload.FileName;
            
        }

        public void FileUpload(List<File> files, IEnumerable<HttpPostedFileBase> fileUploads)
        {
            foreach (var fileUpload in fileUploads)
            {
                var file = new File();
                if (fileUpload != null & fileUpload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(fileUpload.InputStream))
                    {
                        file.Content = (reader.ReadBytes(fileUpload.ContentLength));
                    }

                    file.FileName = fileUpload.FileName;
                    file.ContentType = fileUpload.ContentType;
                }
                files.Add(file);
            }
        }

        //It don't work yet, resize is resolved in browser for now
        public void ResizeImage(HttpPostedFileBase imageUpload)
        {
            ImageResizer.ResizeSettings resizeSettings = new ImageResizer.ResizeSettings
            {
                Width = 200,
                Height = 200
            };
            ImageResizer.ImageBuilder.Current.Build(imageUpload, imageUpload, resizeSettings);
        }

        public void DefaultImage(Doctor doctor)
        {
            doctor.ImageID = 1;
            doctor.Image.FileUrl = "~/Img/doc-img-default.png";
            doctor.Image.TypeOfFile = TypeOfFile.IMAGE;
        }
        
    }
}