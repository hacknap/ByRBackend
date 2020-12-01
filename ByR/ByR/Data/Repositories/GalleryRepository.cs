using ByR.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByR.Data.Repositories
{
    public class GalleryRepository : GenericRepository<Gallery>, IGallery
    {
        private readonly DataContext context;

        public GalleryRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<ActionResult<List<Gallery>>> GetGalleryByPropertyId(string id)
        {
            List<Gallery> listPhotos = null;

            //listPhotos = context.Gallery.FirstOrDefault(p => p.Property == id);
            listPhotos = context.Gallery.Where(p => p.Property == id).Where(x => x.IsDelete == false).ToList();
                
            return listPhotos;

        }
        //public string EncodeImage(Image objImage, System.Drawing.Imaging.ImageFormat intFormat)
        //{
        //    byte[] arrBytImages;

            //    // Abre un stream en memoria para obtener los bytes de la imagen		
            //    using (System.IO.MemoryStream stmMemory = new System.IO.MemoryStream())
            //    { // Graba la imagen en el stream en memoria
            //        objImage.Save(stmMemory, intFormat);
            //        // Convierte la imagen en un array de bytes
            //        arrBytImages = stmMemory.ToArray();
            //        // Cierra el stream
            //        stmMemory.Close();
            //    }
            //    // Devuelve el array de bytes convertidos
            //    return Convert.ToBase64String(arrBytImages);
            //}

        }
}
