using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using ElProyecteGrandeSprint1.Helpers;
using ElProyecteGrandeSprint1.Models.Entities.ApiEntities;
using NUnit.Framework;

namespace TestBackend
{
    public class ServiceHelperTests
    {
        private ServiceHelper _serviceHelper = new ServiceHelper();

        private List<Deal> _deserialzedApidealsMock = new List<Deal>()
        {
            new Deal()
            {
                StoreName = "1",
                Title = "Something",
                SalePrice = 50,
                NormalPrice = 100,
                IsOnSale = 10,
                MetacriticScore = 10,
                SteamRatingText = "dunno",
                SteamRatingPercent = 100,
                SteamRatingCount = 10,
                DealRating = 20,
                Image = "link"

            },
            new Deal()
            {
                StoreName = "2",
                Title = "Something",
                SalePrice = 50,
                NormalPrice = 100,
                IsOnSale = 10,
                MetacriticScore = 10,
                SteamRatingText = "dunno",
                SteamRatingPercent = 100,
                SteamRatingCount = 10,
                DealRating = 20,
                Image = "link"
            }
        };
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

        [Test]
        public void TestSerialize()
        {
            var deals = "[{\"Title\":\"Xbox Games Will Be Streamed On TVs Without A Console - But With A Catch\",\"Date\":\"2022-06-09T14:19:21Z\",\"Description\":\"Microsoft has revealed that Xbox Game Pass will be streamable on Samsung 2022 Smart TVs this month, without the need for extra hardware.\",\"Image\":\"https://static3.srcdn.com/wordpress/wp-content/uploads/2022/06/Xbox-Samsung-TV-Cover.jpg\",\"Link\":\"https://screenrant.com/xbox-game-pass-tv-streaming-no-console/\"},{\"Title\":\"PS5\\u0027s DualSense Firmware Appears On Steam Database\",\"Date\":\"2022-06-09T14:18:15Z\",\"Description\":\"This could mean the DualSense will soon be officially supported on Steam.\",\"Image\":\"https://static1.thegamerimages.com/wordpress/wp-content/uploads/2022/05/Astro-Boy-Hugs-DualSense-Controller.jpg\",\"Link\":\"https://www.thegamer.com/ps5-dualsense-firmware-steam-database/\"},{\"Title\":\"Call of Duty: Warzone 2 release date rumours: upgrades and crossplay\",\"Date\":\"2022-06-09T16:10:41+02:00\",\"Description\":\"Want to know more about Call of Duty: Warzone 2 release date? In a special briefing earlier this year, Infinity Ward confirmed their hugely successful Call of Duty battle royale game is getting a sequel. Warzone 2 looks set to be built on a new engine, with an all-new playspace and a sandbox mode, resulting in a \\u201Cmassive evolution of Battle Royale\\u201D. With Call of Duty Warzone Season 3 Reloaded in full swing and Activision Blizzard confirming the game reached over 100 million active players last year, Warzone 2 is undoubtedly one of the biggest upcoming games in some time. We finally know a lot more about Warzone 2 thanks to the recent reveal of Modern Warfare 2\\u0027s release date, including the host of engine upgrades coming to the FPS game. Sadly, we still don\\u0027t know when the Warzone 2 release date is. We\\u2019ve put together this guide with everything we know about Warzone 2, including its rumoured release date, what the next Warzone map might look like, and much more.\",\"Image\":\"https://www.pcgamesn.com/wp-content/uploads/2022/05/call-of-duty-warzone-2-release-date-goggles.jpg\",\"Link\":\"https://www.pcgamesn.com/call-of-duty-warzone-2/release-date\"},{\"Title\":\"Microsoft Details Activision Blizzard Acquisition, Exclusives, and Game Pass\",\"Date\":\"2022-06-09T14:07:46Z\",\"Description\":\"Microsoft executive Matt Booty discusses the Activision Blizzard acquisition and how it plans to handle exclusive titles and Xbox Game Pass.\",\"Image\":\"https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/06/microsoft-activision-game-pass.jpg\",\"Link\":\"https://gamerant.com/microsoft-activision-blizzard-acquisition-exclusives-game-pass/\"},{\"Title\":\"7 Games That Got Ray Tracing Post Launch\",\"Date\":\"2022-06-09T14:05:13Z\",\"Description\":\"These games saw the light after they were released.\",\"Image\":\"https://static1.thegamerimages.com/wordpress/wp-content/uploads/2022/06/Ray-tracing-update-post-launch-featured.jpg\",\"Link\":\"https://www.thegamer.com/video-games-ray-tracing-post-launch/\"},{\"Title\":\"Starfield could launch on Xbox Game Pass in early 2023\",\"Date\":\"2022-06-09T16:02:14+02:00\",\"Description\":\"Starfield is expected to release at some point in 2023, but a subtle Xbox Game Pass disclaimer suggests the sci-fi romp could arrive within the first few months. The rumour should bring comfort to Bethesda fans waiting for the publisher\\u0027s next best RPG game, but there\\u0027s room for debate over the exact meaning of the potentially unintentional blurb. Highlighted by Reddit user Ganndalf, the spotted disclaimer states that Starfield is \\u0022expected early 2023.\\u0022 This means the space adventure may show up as early as January, but it also hints at a Q1 release date window. Of course, the Xbox Game Pass information might also be a reference to Bethesda\\u0027s previous Starfield delay, which claimed the game\\u0027s launch was pushed back to \\u0022the first half\\u0022 of next year. Just like with all rumours and speculation, you should take this Starfield release date tidbit with a grain of salt. That said, it perhaps helps reassure those eagerly awaiting the RPG\\u0027s arrival, as some Starfield fans have resorted to staring at doors to uncover any news.\",\"Image\":\"https://www.pcgamesn.com/wp-content/uploads/2022/06/starfield-xbox-game-pass-early-2023-rumour.jpg\",\"Link\":\"https://www.pcgamesn.com/starfield/xbox-game-pass-early-2023-rumour\"},{\"Title\":\"The Decade-Long Struggle To Fund Oakland\\u2019s Scrappy Video Game Museum\",\"Date\":\"2022-06-09T14:00:00Z\",\"Description\":\"Back in September 2021, The Museum of Art and Digital Entertainment in Oakland, California, better known as The MADE, began tweeting about how impossibly difficult it seemed to fund a video game museum. Read more...\",\"Image\":\"https://i.kinja-img.com/gawker-media/image/upload/s--vnlgvTpg--/c_fit,fl_progressive,q_80,w_636/ec8fea1df2cde7293433b4081bff74fb.jpg\",\"Link\":\"https://kotaku.com/the-made-oakland-game-museum-funding-dolby-ea-ubisoft-g-1849030490\"},{\"Title\":\"The Quarry system requirements\",\"Date\":\"2022-06-09T15:57:04+02:00\",\"Description\":\"The masterminds behind Until Dawn are back with another horror game, but before you can enjoy the thrill of the chase, you\\u0027ll first need to check whether your gaming PC meets The Quarry system requirements. You have a good chance of being able to run the frightfest if your rig can play any of The Dark Pictures Anthology series, but the recommended specs are reserved for modern systems built within the past couple of years. It\\u0027s not clear what kind of resolutions or frame rates Supermassive Games aims for with The Quarry system requirements, but hitting the minimum is relatively easy. Much like other games in the developer\\u0027s catalogue, you\\u0027ll be able to boot with a decade-old gaming CPU, a six-year-old graphics card, the standard 8GB of RAM, and 50GB of space. It doesn\\u0027t even require the speed of the best SSD for gaming, although it couldn\\u0027t hurt if you wanted to pick up the pace of loading times. To hit the recommended system requirements, The Quarry doesn\\u0027t just demand processors released in 2020 onwards, but top-of-the-line chips at that. It lists the Intel Core i9 10900K and AMD Ryzen 7 3800 XT,  setting a high bar for those that haven\\u0027t upgraded in a while. Recommended GPU requirements aren\\u0027t quite as extreme considering it targets entry-to-mid range and new Nvidia RTX 4000 graphics cards are just around the corner, but some may find these specs somewhat punishing.\",\"Image\":\"https://www.pcgamesn.com/wp-content/uploads/2022/05/The-Quarry-system-requirements.jpg\",\"Link\":\"https://www.pcgamesn.com/the-quarry/system-requirements\"},{\"Title\":\"Watch Summer Game Fest Live: Official Stream Links | Screen Rant\",\"Date\":\"2022-06-09T13:54:14Z\",\"Description\":\"Summer Game Fest 2022 is ready to fill the void that E3 left once again, and here are all the official streams - including YouTube, Twitch, and Steam!\",\"Image\":\"https://static0.srcdn.com/wordpress/wp-content/uploads/2022/06/Summer-Game-Fest-Watch-Online.jpg\",\"Link\":\"https://screenrant.com/summer-game-fest-2022-watch-official-stream-links/\"},{\"Title\":\"Some Future Activision Blizzard Games Will Be Xbox-Exclusive, Microsoft Confirms\",\"Date\":\"2022-06-09T15:44:00+02:00\",\"Description\":\"Microsoft has revealed more details on its plans for Activision Blizzard, once its planned acquisition of the gaming company has been completed. During a media briefing, Xbox Game Studios head Matt Booty spoke about exclusivity and how Microsoft wants to ensure that the communities for some of Activision Blizzard\\u0027s biggest multiplatform properties are looked after.\",\"Image\":\"https://www.gamespot.com/a/uploads/screen_medium/1601/16018044/3987987-xboxactiblizz.jpg\",\"Link\":\"https://www.gamespot.com/articles/some-future-activision-blizzard-games-will-be-xbox-exclusive-microsoft-confirms/1100-6504322/?ftag=CAD-01-10abi2f\"}]";
            var deserializzeddata = _serviceHelper.DeserializeData<News>(deals);
            var serializeData = _serviceHelper.SerializeData(deserializzeddata);
        }

        [Test]
        public void TestMakeDealsObject()
        {
            List<Deal> dealsObjectfinalized =  _serviceHelper.MakeDealsObject(_deserialzedApidealsMock);
            Assert.AreEqual("Steam", dealsObjectfinalized[0].StoreName);
            Assert.AreEqual("GamersGate", dealsObjectfinalized[1].StoreName);
        }

        [Test]
        public async Task TestGetJsonStringFromApi()
        {
            var response = await _serviceHelper.GetJsonStringFromApi(request);
            Assert.IsNotNull(response);

        }
    }
}
