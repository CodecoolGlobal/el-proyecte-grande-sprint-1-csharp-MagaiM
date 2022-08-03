using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using ElProyecteGrandeSprint1.Services;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;

namespace TestBackend
{
    public class ApiServiceTests: DatabaseMockInSetup
    {
        private string deals = @"{
    ""totalArticles"": 1751,
    ""articles"": [
        {
            ""title"": ""Kate Middleton and Prince William holiday in Isles of Scilly for Prince George's ninth birthday"",
            ""description"": ""The young prince spent a fortnight on Tresco, an island owned by his grandfather, with his family and their beloved dog, Orla"",
            ""content"": ""Ahead of their big move to Windsor, the Duke and Duchess of Cambridge are keen to let their three children - George, Charlotte, and Louis - enjoy one last 'normal' summer. The family-of-five will be relocating to Adelaide Cottage on the Queen's Winds... [2866 chars]"",
            ""url"": ""https://www.cornwalllive.com/news/celebs-tv/kate-middleton-prince-william-holiday-7414073"",
            ""image"": ""https://i2-prod.gloucestershirelive.co.uk/incoming/article7414175.ece/ALTERNATES/s1200/0_Prince-Georges-ninth-birthday.jpg"",
            ""publishedAt"": ""2022-08-03T07:27:35Z"",
            ""source"": {
                ""name"": ""Cornwall Live"",
                ""url"": ""https://www.cornwalllive.com""
            }
},
        {
    ""title"": ""Aldi announces new Super 6 deals on fresh meat"",
            ""description"": ""Every fortnight, the supermarket picks six of its favourites to offer to shoppers at a cut-price"",
            ""content"": ""Aldi has announced some of the latest Super 6 offers on fresh meat that will be coming to its stores this week. Every fortnight, the supermarket picks six of its favourites to offer to shoppers at a cut-price at no extra cost to its UK suppliers.\nThi... [939 chars]"",
            ""url"": ""https://www.coventrytelegraph.net/whats-on/shopping/aldi-announces-new-super-6-24652078"",
            ""image"": ""https://i2-prod.chroniclelive.co.uk/incoming/article23935653.ece/ALTERNATES/s1200/0_JS265346924.jpg"",
            ""publishedAt"": ""2022-08-02T14:21:14Z"",
            ""source"": {
        ""name"": ""Coventry Telegraph"",
                ""url"": ""https://www.coventrytelegraph.net""
            }
},
        {
    ""title"": ""For Barry Manilow, it all began with a fortnight in New England"",
            ""description"": ""He can laugh about it now, but the hitmaker's career started inauspiciously with a two-week gig at Paul's Mall. \""It was a horrible experience,\"" he says."",
            ""content"": ""“I was going to be Nelson Riddle. George Martin of the Beatles. I was going to be an arranger or conductor or composer,” Manilow, 79, says. And, for a few years, he was. Perhaps most notably as Bette Midler’s piano player, earning a Grammy nod for hi... [6600 chars]"",
            ""url"": ""https://www.bostonglobe.com/2022/08/02/arts/barry-manilow-it-all-began-with-fortnight-new-england/"",
            ""image"": ""https://bostonglobe-prod.cdn.arcpublishing.com/resizer/GHgK5vzwlm74ptZVYoMR45r69bA=/506x0/cloudfront-us-east-1.images.arcpublishing.com/bostonglobe/AZMU3M2RVND7TN434W63OURJ6A.jpg"",
            ""publishedAt"": ""2022-08-02T04:00:00Z"",
            ""source"": {
        ""name"": ""The Boston Globe"",
                ""url"": ""https://www.bostonglobe.com""
            }
}
    ]
}";

        private readonly UserServiceHelper _serviceHelper = new();
        private ServiceHelper _mockedServiceHelper;
        private ApiService _service;

        private HttpRequestMessage request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://free-to-play-games-database.p.rapidapi.com/api/games?sort-by=release-date"),
            Headers =
            {
                { "X-RapidAPI-Host", "free-to-play-games-database.p.rapidapi.com" },
                { "X-RapidAPI-Key", "44c0067568mshcd96a9adc6db887p13ebcfjsnc1d0ab0bf882" },
            },
        };

        [SetUp]
        public void Setup()
        {
            var mockedServiceHelper = NSubstitute.Substitute.For<ServiceHelper>();
            _mockedServiceHelper = mockedServiceHelper;
            _mockedServiceHelper.Configure().GetJsonStringFromApi(request).Returns(deals);
            _service = new ApiService(mockedServiceHelper);
        }

        [Test]
        public void TestGetDataFromApi()
        {
            var result = _service.GetDataFromApi<FreeGame>(request);
            var thing = _mockedServiceHelper.GetJsonStringFromApi(request);
            _mockedServiceHelper.Received().GetJsonStringFromApi(request);
            var result2 = _service.GetDataFromApi<GNewsResponse>(request);
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public void TestGetDeals()
        {
            var result = _service.GetDeals(request);
            Assert.IsInstanceOf<Task<string>>(result);
            _mockedServiceHelper.Received().GetJsonStringFromApi(request);
        }
    }
}
