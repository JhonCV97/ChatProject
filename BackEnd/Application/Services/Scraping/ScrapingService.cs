using Application.Common.Response;
using Application.DTOs.DataInfo;
using Application.Interfaces.DataInfo;
using Application.Interfaces.History;
using Application.Interfaces.Scraping;
using Domain.Interfaces;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Scraping
{
    public class ScrapingService : IScrapingService
    {
        public async Task<List<DataInfoPostDto>> WebScraping()
        {
            try
            {
                // URL de la página que deseas rastrear
                string url = "https://www.ucaldas.edu.co/portal/";

                // Configura un cliente HTTP
                HttpClient httpClient = new HttpClient();
                string html = await httpClient.GetStringAsync(url);

                // Analiza el HTML usando HtmlAgilityPack
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                // Rastrea el título de la página
                string pageTitle = htmlDocument.DocumentNode.SelectSingleNode("//title")?.InnerText;

                // Crea una lista para almacenar los enlaces encontrados
                List<string> Urls = new List<string>();

                var ListDataInfo = new List<DataInfoPostDto>();

                // Rastrea los enlaces de la página y agrégalos a la lista
                var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
                if (links != null)
                {
                    foreach (var link in links)
                    { 
                        string href = link.GetAttributeValue("href", "");
                        string linkText = link.InnerText;
                        var divAsociado = "";
                        if (href != "#" && href != "" && linkText != "" && href.Contains("http") && href.Contains("ucaldas") && !Urls.Contains(href))
                        {
                            var UrlsError = new string[] {
                                "http://sig.ucaldas.edu.co/admiarchigestion/H0010-097-021-1.PDF",
                                "http://sig.ucaldas.edu.co/admiarchigestion/A-178.PDF",
                                "http://sig.ucaldas.edu.co/admiarchigestion/A-178.PDF",
                                "http://sig.ucaldas.edu.co/admiarchigestion/F-8960.PDF",
                                "http://sig.ucaldas.edu.co/admiarchigestion/1100-109.PDF"
                            };

                            if (!UrlsError.Contains(href))
                            {                                
                                HttpResponseMessage linkResponse = await httpClient.GetAsync(href);
                                if (linkResponse.IsSuccessStatusCode)
                                {
                                    string htmlHref = await linkResponse.Content.ReadAsStringAsync();
                                    HtmlDocument htmlDocumentHref = new HtmlDocument();
                                    htmlDocumentHref.LoadHtml(htmlHref);
                                    divAsociado = htmlDocumentHref.DocumentNode.SelectSingleNode("//p")?.InnerText.Replace("\n", " ").Trim();
                                }
                            }

                            if (divAsociado != null)
                            {
                                divAsociado = divAsociado.Contains("This page is having a slideshow that uses Javascript") ? "" : divAsociado;
                            }

                            var dataInfoPostDto = new DataInfoPostDto
                            {
                                QueryType = linkText.Replace("\n", " ").Trim(),
                                Response = divAsociado + " Para mas información consultar la siguiente URL: " + href,
                                RoleId = 2
                            };

                            Urls.Add(href);
                            ListDataInfo.Add(dataInfoPostDto);

                        }
                    }
                }


                return ListDataInfo;

            }
            catch (Exception ex)
            {

            }

            return null;
        }
    }
}
