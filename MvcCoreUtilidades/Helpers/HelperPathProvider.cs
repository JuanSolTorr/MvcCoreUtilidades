using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcCoreUtilidades.Helpers
{
    //Enumeracion con las carpetas que deseemos subir ficheros
    public enum Folders { Uploads, Images, Facturas, Temporal, Productos }
    public class HelperPathProvider
    {
        private IWebHostEnvironment hostEnvironment;
        private IHttpContextAccessor contextAccessor;
        private IServer server;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment, IHttpContextAccessor contextAccessor, IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.contextAccessor = contextAccessor;
            this.server = server;
        }

        //TENDREMOS UN METODO QUE SE ENCARGAR DE RESOLVER LA RUTA 
        //COMO STRING CUANDO RECIBAMOS EL FICHERO Y LA CARPETA
        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Productos)
            {
                carpeta = Path.Combine("images", "productos");
            }
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }

        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            else if (folder == Folders.Uploads)
            {
                carpeta = "uploads";
            }
            else if (folder == Folders.Facturas)
            {
                carpeta = "facturas";
            }
            else if (folder == Folders.Temporal)
            {
                carpeta = "temp";
            }
            else if (folder == Folders.Productos)
            {
                // Esta si va a cambiar porque esto es sistema de ficheros
                // y necesitamos web
                carpeta = "images/productos";
            }
            // http:localhost:999/i8mages/productos/1.png
            // Quiero buscar la forma de recuperar la URL de nuestro Server
            // en MVC Net Core.
            //var request = this.contextAccessor.HttpContext?.Request;
            //if(request == null)
            //{
            //    return $"/{carpeta}/{fileName}";
            //}

            //string baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            //return $"{baseUrl}/{carpeta}/{fileName}";
            var addresses = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = addresses.FirstOrDefault();
            // Devolvemos la ruta URL
            string urlPath = $"{serverUrl}/{carpeta}/{fileName}";
            return urlPath;
        }
    }
}
