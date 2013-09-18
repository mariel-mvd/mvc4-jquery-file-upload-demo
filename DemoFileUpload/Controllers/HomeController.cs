﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoFileUpload.Controllers
{
    public class HomeController : Controller {
        private string StorageRoot {
            get { return Path.Combine(Server.MapPath("~/Files")); }
        }

        public ActionResult Index() {
            return View();
        }


        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Delete(string id) {
            var filename = id.Replace('|', '.');
            var filePath = Path.Combine(Server.MapPath("~/Files"), filename);

            if (System.IO.File.Exists(filePath)) {
                System.IO.File.Delete(filePath);
            }
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpGet]
        public void Download(string id) {
            var filename = id;
            var filePath = Path.Combine(Server.MapPath("~/Files"), filename);

            var context = HttpContext;

            if (System.IO.File.Exists(filePath)) {
                context.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                context.Response.ContentType = "application/octet-stream";
                context.Response.ClearContent();
                context.Response.WriteFile(filePath);
            } else
                context.Response.StatusCode = 404;
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        [HttpPost]
        public JsonResult UploadFiles() {

            var statuses = new List<ResultadoArchivo>();
            var headers = Request.Headers;

            foreach (string file in Request.Files) {
                if (string.IsNullOrEmpty(headers["X-File-Name"])) {
                    UploadWholeFile(Request, statuses);
                } else {
                    UploadPartialFile(headers["X-File-Name"], Request, statuses);
                }
            }
            var res = new { files = statuses };

            JsonResult result = Json(res);

            result.ContentType = "text/plain";
            return result;

        }

        private string EncodeFile(string fileName) {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadPartialFile(string fileName, HttpRequestBase request, List<ResultadoArchivo> statuses) {
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;

            var fullName = Path.Combine(StorageRoot, Path.GetFileName(fileName));

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write)) {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0) {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(new ResultadoArchivo()
            {
                name = fileName,
                size = file.ContentLength,
                type = file.ContentType,
                url = "/Home/Download/" + fileName,
                deleteUrl = "/Home/Delete/" + fileName.Replace('.', '|'),  //the replace needed for the routing to work on the delete action, which reverses the replace
                thumbnailUrl = @"data:image/png;base64," + EncodeFile(fullName),
                deleteType = "GET",
            });
        }

        //DONT USE THIS IF YOU NEED TO ALLOW LARGE FILES UPLOADS
        //Credit to i-e-b and his ASP.Net uploader for the bulk of the upload helper methods - https://github.com/i-e-b/jQueryFileUpload.Net
        private void UploadWholeFile(HttpRequestBase request, List<ResultadoArchivo> statuses) {
            for (int i = 0; i < request.Files.Count; i++) {
                var file = request.Files[i];

                var fullPath = Path.Combine(StorageRoot, Path.GetFileName(file.FileName));

                file.SaveAs(fullPath);

                statuses.Add(new ResultadoArchivo()
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    type = file.ContentType,
                    url = "/Home/Download/" + file.FileName,
                    deleteUrl = "/Home/Delete/" + file.FileName.Replace('.', '|'),
                    thumbnailUrl = @"data:image/png;base64," + EncodeFile(fullPath),
                    deleteType = "GET",
                });
            }
        }
    }

    public class ResultadoArchivo {


        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteUrl { get; set; }
        public string deleteType { get; set; }
        public string error { get; set; }

    }

}
