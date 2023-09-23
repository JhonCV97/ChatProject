using Application.Common.Response;
using Application.Interfaces.SaveImage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SaveImage
{
    public class SaveImage : ISaveImage
    {
        public async Task<ApiResponse<string>> SaveImg(IFormFile file)
        {
            var response = new ApiResponse<string>();

            try
            {
                string UrlPath = @"Resources\\PublicFiles\\Images";
                string Pathfinal = Path.Combine(Directory.GetCurrentDirectory(), UrlPath);

                var fullPath = "";

                if (file.Length > 0)
                {
                    // Se valida si la ruta existe o si no se crea.
                    if (!Directory.Exists(Pathfinal))
                        Directory.CreateDirectory(Pathfinal);

                    fullPath = Path.Combine(Pathfinal, file.FileName);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    file.CopyTo(stream);
                }

                response.Data = "";
                response.Result = true;
                response.Message = "OK";

                response.Url = fullPath;
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al guardar la imagen, consulte con el administrador. {ex.Message} ";
            }
            return await Task.Run(() => response);
        }
    }
}
