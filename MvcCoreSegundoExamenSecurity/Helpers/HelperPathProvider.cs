﻿using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;

namespace MvcCoreSegundoExamenSecurity.Helpers
{
    public enum Folders { Images = 0}
    public class HelperPathProvider
    {
        private IServer server;
        private IWebHostEnvironment hostEnvironment;
        public HelperPathProvider(IServer server, IWebHostEnvironment hostEnvironment)
        {
            this.server = server;
            this.hostEnvironment = hostEnvironment;
        }

        private string GetFolderPath(Folders folder)
        {
            string carpeta = "";
            if (folder == Folders.Images)
            {
                carpeta = "images";
            }
            return carpeta;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);
            string rootPath = this.hostEnvironment.WebRootPath;
            string path = Path.Combine(rootPath, carpeta, fileName);
            return path;
        }

        public string MapPathURLProvider()
        {
            var address = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string url = address.FirstOrDefault();
            return url;
        }

        public string MapPathURLProvider(string fileName, Folders folder)
        {
            string carpeta = this.GetFolderPath(folder);
            var address = this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string url = address.FirstOrDefault();
            string urlPath = url + "/" + carpeta + "/" + fileName;
            return urlPath;
        }
    }
}
