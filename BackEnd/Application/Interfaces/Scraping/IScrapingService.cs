using Application.DTOs.DataInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Scraping
{
    public interface IScrapingService
    {
        Task<List<DataInfoPostDto>> WebScraping();
    }
}