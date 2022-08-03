using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Controllers;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace TestBackend
{
    public class TestDealsController
    {
        private DealsController _dealsController;
        private ServiceHelper _serviceHelper2 = new ServiceHelper();
        private ApiService _mockedApiService;

        [SetUp]
        public void Setup()
        {
            var mockedIlogger = NSubstitute.Substitute.For<ILogger<DealsController>>();
            var mockedApiService = NSubstitute.Substitute.For<ApiService>(_serviceHelper2);
            _mockedApiService = mockedApiService;
            _dealsController = new DealsController(mockedIlogger, _mockedApiService);
        }

        [Test]
        public async Task TestGetDeals()
        {
            var request1 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,

                RequestUri = new Uri($"https://www.cheapshark.com/api/1.0/deals?pageSize={60}&desc={0}&sortBy={"Deal Rating"}")
            };
            var request2 = new HttpRequestMessage
            {
                Method = HttpMethod.Get,

                RequestUri = new Uri($"https://www.cheapshark.com/api/1.0/deals?pageSize={60}&desc={0}&sortBy={"Deal Rating"}&storeID={1}")
            };
            await _dealsController.GetDeals();
            await _dealsController.GetDeals(storeId:"1");
            await _mockedApiService.Received().GetDeals(request1);
            await _mockedApiService.Received().GetDeals(request2);
        }
    }
}
