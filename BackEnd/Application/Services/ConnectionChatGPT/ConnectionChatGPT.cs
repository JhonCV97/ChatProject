using Application.Common.Response;
using Application.Cqrs.ConnectionChatGPT.Queries;
using Application.DTOs.User;
using Application.Interfaces.ConnectionChatGPT;
using Application.Interfaces.DataInfo;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task<string> ConnectionChat(string Question)
        {
            string apiKey = "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"; // Reemplaza con tu clave API de OpenAI

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.openai.com/v1/");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var HelpText = GetStringWithMoreCoincidences(Question);

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
                response.Data = await ConnectionChat(request.Question);
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

        public string GetStringWithMoreCoincidences(string Question)
        {
            var matchesByChain = new Dictionary<string, int>();

            var dataInfoList = _dataInfoService.GetDataInfo().Result.Data;

            // Iterar sobre cada cadena en la lista
            foreach (var String in dataInfoList)
            {
                // Dividir las cadenas en palabras
                var wordsChain = String.Response.Split(' ');
                var wordsChainTitle = String.QueryType.Split(' ');

                // Contar las coincidencias con la cadena de parámetro
                int coincidences = wordsChain.Count(word => Question.Contains(word, StringComparison.OrdinalIgnoreCase))
                                    + wordsChain.Count(word => Question.Contains(word, StringComparison.OrdinalIgnoreCase));

                // Agregar la cadena y la cantidad de coincidencias al diccionario
                matchesByChain.Add(String.Response, coincidences);
            }

            // Encontrar la cadena con más coincidencias
            var ChainWithMoreCoincidences = matchesByChain.OrderByDescending(kv => kv.Value).FirstOrDefault().Key;

            return ChainWithMoreCoincidences;
        }


    }
}
