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
    public class ImageManipulation
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
            else
            {
                return ".bin";
            }
        }

        public void ImageUpload(Doctor doctor,Image image, HttpPostedFileBase imageUpload)
        {
            var imgFileName = Guid.NewGuid().ToString() + GetExtension(imageUpload.ContentType);
            using (var reader = new System.IO.BinaryReader(imageUpload.InputStream))
            {
                image.Content = (reader.ReadBytes(imageUpload.ContentLength));
            }
            
            doctor.Image = image;

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
            doctor.Image.ImgUrl = "~/Img/doc-img-default.png";

        }
    }
}