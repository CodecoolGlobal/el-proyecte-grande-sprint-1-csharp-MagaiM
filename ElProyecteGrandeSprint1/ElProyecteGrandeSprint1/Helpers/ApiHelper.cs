﻿using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Controllers
{

    public class ApiHelper
    {
        private readonly ServiceHelper _serviceHelper = new ServiceHelper();
        public string GetDataFromApi<T>(HttpRequestMessage request)
        {
            var body = _serviceHelper.GetJsonStringFromApi(request);
            string newData;

            try
            {
                if (typeof(T) == typeof(GNewsResponse))
                {
                    var deserialization = JsonConvert.DeserializeObject<GNewsResponse>(body.Result);
                    newData = _serviceHelper.SerializeData(deserialization.Articles);
                }
                else
                {
                    var deserializedData = _serviceHelper.DeserializeData<T>(body.Result);
                    newData = _serviceHelper.SerializeData(deserializedData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return newData;
        }

        public async Task<string> GetDeals(HttpRequestMessage request)
        {
            var body = await _serviceHelper.GetJsonStringFromApi(request);
            List<Deal> deserializedData = _serviceHelper.DeserializeData<Deal>(body);
            deserializedData = new List<Deal>(deserializedData.DistinctBy(x => x.Title));
            var newListDeal = _serviceHelper.MakeDealsObject(deserializedData);
            var newData = _serviceHelper.SerializeData(newListDeal ?? throw new NullReferenceException());
            return newData;
        }
    }
}