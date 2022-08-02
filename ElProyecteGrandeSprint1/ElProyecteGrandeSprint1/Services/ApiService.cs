using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Services
{

    public class ApiService
    {
        private readonly ServiceHelper _serviceHelper;

        public ApiService(ServiceHelper serviceHelper)
        {
            _serviceHelper = serviceHelper;
        }

        public string GetDataFromApi<T>(HttpRequestMessage request)
        {
            var body = _serviceHelper.GetJsonStringFromApi(request);
            string newData;

            try
            {
                if (typeof(T) == typeof(GNewsResponse))
                {
                    var deserialization = JsonConvert.DeserializeObject<GNewsResponse>(body.Result);
                    if (deserialization == null) throw new Exception("deserialization was null");
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
                Console.WriteLine(e.Message);
                throw;
            }

            return newData;
        }

        public async Task<string> GetDeals(HttpRequestMessage request)
        {
            var body = await _serviceHelper.GetJsonStringFromApi(request);
            var deserializedData = _serviceHelper.DeserializeData<Deal>(body);
            if (deserializedData == null) throw new Exception("deserializedData was null");
            deserializedData = new List<Deal>(deserializedData.DistinctBy(x => x.Title));
            var newListDeal = _serviceHelper.MakeDealsObject(deserializedData);
            var newData = _serviceHelper.SerializeData(newListDeal ?? throw new NullReferenceException());
            return newData;
        }
    }
}