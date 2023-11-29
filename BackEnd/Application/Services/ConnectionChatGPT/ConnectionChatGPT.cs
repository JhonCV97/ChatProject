using Application.Common.Response;
using Application.Cqrs.ConnectionChatGPT.Queries;
using Application.DTOs.User;
using Application.Interfaces.ConnectionChatGPT;
using Application.Interfaces.DataInfo;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using DocumentFormat.OpenXml.Spreadsheet;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ConnectionChatGPT
{
    public class ConnectionChatGPT : IConnectionChatGPT
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _autoMapper;
        private readonly IDataInfoService _dataInfoService;

        public ConnectionChatGPT(IUnitOfWork unitOfWork, IMapper autoMapper, IDataInfoService dataInfoService)
        {
            _unitOfWork = unitOfWork;
            _autoMapper = autoMapper;
            _dataInfoService = dataInfoService;
        }

        public async Task<string> ConnectionChat(string Question, int RoleId)
        {
            string apiKey = "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"; 

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.openai.com/v1/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var HelpText = GetStringWithMoreCoincidences(Question, RoleId);

                var requestBody = new
                {
                    model = "gpt-3.5-turbo-0613",
                    messages = new[]
                    {
                        new
                        {
                            role = "assistant",
                            content = HelpText
                        },
                        new
                        {
                            role = "assistant",
                            content = Question
                        }
                    },
                    temperature = 0.7
                };

                var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("chat/completions", content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonResponse);
                    string chatResponse = result.choices[0].message.content;
                    return chatResponse;
                }


                return response.Content.ToString();
            }
        }

        public async Task<ApiResponse<string>> GetMessageChat(GetMessageChatGPTQuery request)
        {
            var response = new ApiResponse<string>();

            try
            {
                response.Data = await ConnectionChat(request.Question, request.RoleId);
                response.Result = true;
                response.Message = "OK";
            }
            catch (Exception ex)
            {
                response.Result = false;
                response.Message = $"Error al obtener respuesta chat, consulte con el administrador. {ex.Message} ";
            }

            return response;
        }

        public string GetStringWithMoreCoincidences(string Question, int RoleId)
        {
            var matchesByChain = new Dictionary<string, int>();

            var dataInfoList = _dataInfoService.GetDataInfo().Result.Data;

            dataInfoList = dataInfoList.Where(x => x.RoleId <= RoleId).ToList();

            var wordsChain = Question.Split(' ');

            var FilterDataInfo = wordsChain.Where(x => x.Length > 3).ToArray();

            var UnitedList = dataInfoList.Select(x => x.QueryType + " " + x.Response).ToList();

            var matchesByData = UnitedList.Select(data => new
            {
                Data = data,
                Coincidences = FilterDataInfo.Count(word => data.ToLower().Contains(word.ToLower()))
            }).OrderByDescending(item => item.Coincidences)
              .ToList();

            return matchesByData.FirstOrDefault().Data;
        }


    }
}
