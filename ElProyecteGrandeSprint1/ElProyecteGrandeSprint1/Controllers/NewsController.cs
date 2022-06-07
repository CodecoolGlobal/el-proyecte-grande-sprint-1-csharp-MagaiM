﻿using System.Diagnostics;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;
        private readonly string apiToken = "1763716cc7950373caaf84e0980fd9da";
        private readonly string language = "en";
        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var news = GetVideoGamesNews();
            return View(news.Result);
        }

        private static async Task<List<News>?> GetVideoGamesNews()
        {
            //var client = new HttpClient();
            var result = "";
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri("https://videogames-news2.p.rapidapi.com/videogames_news/recent"),
            //    Headers =
            //    {
            //        { "X-RapidAPI-Host", "videogames-news2.p.rapidapi.com" },
            //        { "X-RapidAPI-Key", "300b4f9442msh9fd4cb14cea575bp1fbf95jsn2e9fef2be340" },
            //    },
            //};
            //using (var response = await client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();
            //    result = await response.Content.ReadAsStringAsync();
            result = "[{\"title\":\"Pokemon GO Could Be Adding Ultra Beasts Soon\",\"date\":\"Wed, 25 May 2022 07:37:42 GMT\",\"description\":\"Niantic shares a short Pokemon GO video online which hints that Ultra Beasts may be coming to the mobile game sometime in the future.\",\"image\":\"https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/05/pokemon-sun-and-moon-nihilego-ultra-beast-(1)-2.jpg\",\"link\":\"https://gamerant.com/pokemon-go-add-ultra-beasts-soon/\"},{\"title\":\"Blizzard Reveals The Most-Played Characters In The Overwatch 2 Beta\",\"date\":\"Wed, 25 May 2022 00:10:00 -0700\",\"description\":\"Overwatch 2's first PvP beta just wrapped up on May 17, leaving the development team with a lot of data to sift through before the next beta begins. Now, Blizzard has shared some of that data in a blog post, revealing some interesting stats on hero usage rates, win rates, and the effects of recent buffs and nerfs.\",\"image\":\"https://www.gamespot.com/a/uploads/screen_medium/1597/15975876/3981567-3965050-ovr_sojourn_002.jpg\",\"link\":\"https://www.gamespot.com/articles/blizzard-reveals-the-most-played-characters-in-the-overwatch-2-beta/1100-6503817/?ftag=CAD-01-10abi2f\"},{\"title\":\"$50 Off The PS5’s Pulse 3D Headset Sounds Pretty Good\",\"date\":\"Wed, 25 May 2022 07:23:26 +0000\",\"description\":\"If you're looking to take full advantage of the PS5's Tempest 3D AudioTech, here's everywhere you can snag Sony's official headset with a nice discount.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2020/09/21/pulse-3d-wireless-headset-1.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/ps5-headset-cheap/\"},{\"title\":\"The Battle of Polytopia lets you launch sneak attacks with the new Diplomacy expansion\",\"date\":\"Wed, 25 May 2022 07:00:00 +0100\",\"description\":\"Midjiwan AB has officially announced an exciting new expansion for The Battle of Polytopia, the indie developer's hit 4X strategy game on mobile and on Steam. The Diplomacy expansion lets players enjoy new features such as Peace Treaties and Tribe Relations as a free update to the game.\\n\\nThe new technology in the Tech Tree gives players the freedom to form alliances both with humans and with AI. They can find player capitals, build embassies, send out spies and even resort to sneak attacks via Cloaks. These possess two new abilities called Infiltrate and Stealth, and they are invisible to enemy units. \",\"image\":\"https://media.pocketgamer.com/artwork/na-28042-1653461169/the-battle-of-polytopia-ios-android-steam-diplomacy-update.jpg\",\"link\":\"https://www.pocketgamer.com/the-battle-of-polytopia/launch-sneak-attacks-with-diplomacy-expansion/\"},{\"title\":\"The Lord Of The Rings: Gollum Gets Its Precious Release Date\",\"date\":\"Wed, 25 May 2022 06:13:05 +0000\",\"description\":\"In Gollum news, The Lord of the Rings: Gollum, the game where you scrounge around as a scrawny little freak, now has a release date.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/05/25/Gollums-death.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/the-lord-of-the-rings-gollum-gets-its-precious-release-date/\"},{\"title\":\"The Adorable, Tragic Tale Of Osamu Tezuka’s Unico Lives Again\",\"date\":\"Wed, 25 May 2022 05:40:26 +0000\",\"description\":\"This unicorn may look cute — and he certainly is — but don’t let his whimsy fool you. Unico was created by Osamu Tezuka, the creator of Astro Boy, who gave this adorable unicorn an edge that captivated Japanese readers of the ‘70s. Now Samuel Sattin, working with the artist team Gurihiru and with the blessing of Tezuka Productions, has launched a Kickstarter to bring Unico to a new generation.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/05/25/1d50552c48d5ffbf9a6982fec6f573d8.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/the-adorable-tragic-tale-of-osamu-tezukas-unico-lives-again/\"},{\"title\":\"Skyrim Fan Shows Off Daedric Dagger Tattoo\",\"date\":\"Wed, 25 May 2022 04:05:09 GMT\",\"description\":\"A fan of The Elder Scrolls 5: Skyrim shows off a tattoo that they got of one of the Daedric weapons that can be made in the game.\",\"image\":\"https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/05/Skyrim-Daedric-Tattoo.jpg\",\"link\":\"https://gamerant.com/skyrim-daedric-dagger-tattoo/\"},{\"title\":\"Upcoming Chocobo GP Season 2 Patch Removes Microtransactions on Retail Version\",\"date\":\"Wed, 25 May 2022 03:50:03 GMT\",\"description\":\"Due to the backlash Square Enix is facing for how Chocobo GP is riddled with microtransactions, season 2 promises to be less expensive for players.\",\"image\":\"https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/05/chocobo-gp-logo-over-chocobo-farm.jpg\",\"link\":\"https://gamerant.com/chocobo-gp-season-2-patch-removes-microtransactions/\"},{\"title\":\"Homebody Is The Next Title From Game Grumps, And It’s A Spooky One\",\"date\":\"Wed, 25 May 2022 04:42:32 +0000\",\"description\":\"From the folks that brought you Dream Daddy, a dad-banging simulator, comes a new horror game called Homebody.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/05/25/unnamed-5.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/homebody-game-grumps/\"},{\"title\":\"You Can Grab The PS5 DualSense Controller For Under $80 Right Now\",\"date\":\"Wed, 25 May 2022 04:40:46 +0000\",\"description\":\"If you're currently in need of a replacement or want to pick up a spare, you can currently grab a nice deal on the PS5 DualSense controller.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/02/18/ps5-controllers-purple-blue-pink.jpeg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/ps5-controller-dualsense-wireless-red-black/\"}]";
        //}
            var deserialisation = JsonConvert.DeserializeObject<List<News>>(result);

            return deserialisation;
        }
        
        public async Task<string> GetGNews(string game)
        {
            //var result = "";
            //var client = new HttpClient();
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri($"https://gnews.io/api/v4/search?q={game}&token={apiToken}&lang={language}"),
            //};
            //using (var response = await client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();
            //    result = await response.Content.ReadAsStringAsync();
            //}

            //string newdata;
            string newdata =
            "[{\"Title\":\"Despite a new hero, Ana proved to be most popular in Overwatch 2 beta\",\"Description\":\"Blizzard released some metrics behind the Overwatch 2 beta that included the\\nhero usage, with fairly surprising results.\",\"Content\":\"Blizzard released some metrics behind the Overwatch 2 beta that included the hero usage, with fairly surprising results.\\nSojourn was eagerly awaited as she would be the hero that would finally end the content draught in Overwatch so when she ended up... [1130 chars]\",\"Link\":\"https://www.altchar.com/game-news/despite-a-new-hero-ana-proved-to-be-most-popular-in-overwatch-2-beta-aiNwF3w9oQ0W\",\"Image\":\"https://media.altchar.com/prod/images/940_530/gm-4077da2e-1959-425d-8e61-69c7951fed92-anascreenshot004.jpeg\",\"Date\":\"2022-05-26T07:03:00Z\",\"Source\":{\"Name\":\"AltChar\",\"Url\":\"https://www.altchar.com\"}},{\"Title\":\"Blizzard Reveals The Most-Played Characters In The Overwatch 2 Beta\",\"Description\":\"Here\\u0027s how much each character--including Overwatch 2\\u0027s new entry, Sojourn--was used in the beta.\",\"Content\":\"Overwatch 2\\u0027s first PvP beta just wrapped up on May 17, leaving the development team with a lot of data to sift through before the next beta begins. Now, Blizzard has shared some of that data in a blog post, revealing some interesting stats on hero u... [1762 chars]\",\"Link\":\"https://www.gamespot.com/articles/blizzard-reveals-the-most-played-characters-in-the-overwatch-2-beta/1100-6503817/\",\"Image\":\"https://www.gamespot.com/a/uploads/screen_kubrick/1597/15975876/3981567-3965050-ovr_sojourn_002.jpg\",\"Date\":\"2022-05-25T08:10:33Z\",\"Source\":{\"Name\":\"GameSpot\",\"Url\":\"https://www.gamespot.com\"}},{\"Title\":\"Luminosity, Vancouver Titans parent company CEO under fire for creating \\u0022divisive culture\\u0022 at esports org\",\"Description\":\"Overwatch: Investors call for Enthusiast Gaming CEO to be replaced.\",\"Content\":\"\\u25B2 Image via Enthusiast Gaming\\nEnthusiast Gaming\\u0027s largest shareholder is calling for the removal of the company\\u0027s CEO and board of directors.\\nEnthusiast Gaming owns Luminosity, Vancouver Titans, and the Seattle Surge. The esports org has come under f... [3345 chars]\",\"Link\":\"https://www.invenglobal.com/articles/17283/luminosity-vancouver-titans-parent-company-ceo-under-fire-for-creating-divisive-culture-at-esports-org\",\"Image\":\"https://static.invenglobal.com/upload/image/2022/05/24/o1653411306262762.jpeg\",\"Date\":\"2022-05-24T16:56:52Z\",\"Source\":{\"Name\":\"InvenGlobal\",\"Url\":\"https://www.invenglobal.com\"}},{\"Title\":\"NYC Mayor Says Overwatch\\u0027s Hanzo Is Garbage\",\"Description\":\"Wish this were clickbait, but it\\u0027s not.\",\"Content\":\"It\\u0027s rare to see a politician express opinions on video games, or at least somewhat informed opinions. NYC Mayor Eric Adams decided to break precedent and throw in his two cents about one Overwatch character in particular--Hanzo.\\nI couldn\\u0027t mash the ... [1542 chars]\",\"Link\":\"https://www.gamespot.com/articles/nyc-mayor-says-overwatchs-hanzo-is-garbage/1100-6503655/\",\"Image\":\"https://www.gamespot.com/a/uploads/screen_kubrick/1597/15971658/3979363-hanzo_thumb.jpg\",\"Date\":\"2022-05-19T19:40:25Z\",\"Source\":{\"Name\":\"GameSpot\",\"Url\":\"https://www.gamespot.com\"}},{\"Title\":\"[UPDATED May. 18] Dignitas sign Gamsu; part ways with FakeGod, DARKWINGS \\u0026 Eclipse\",\"Description\":\"League of Legends: Gamsu played for Dignitas in the LCS back in 2015 before pursuing a career in competitive Overwatch, and has spent his competitive return to League of Legends in the amateur and Academy systems with 100 Thieves up until his reunion with DIG.\",\"Content\":\"Source: Dignitas\\nUPDATE May. 18: After parting ways with FakeGod, Dignitas have signed Noh \\u0022Gamsu\\u0022 Yeong-jin as their new LCS top laner. Gamsu played for Dignitas in the LCS back in 2015 before pursuing a career in competitive Overwatch, and has spen... [2156 chars]\",\"Link\":\"https://www.invenglobal.com/articles/17064/updated-may-18-dignitas-sign-gamsu-part-ways-with-fakegod-darkwings-eclipse\",\"Image\":\"https://static.invenglobal.com/upload/image/2022/05/19/o1652975270636431.jpeg\",\"Date\":\"2022-05-19T06:59:00Z\",\"Source\":{\"Name\":\"InvenGlobal\",\"Url\":\"https://www.invenglobal.com\"}},{\"Title\":\"Second Overwatch 2 beta on the way with new content expected - is Junker Queen coming?\",\"Description\":\"Blizzard has announced that Overwatch 2\\u0027s second beta is on the way and will be detailed in a June 16 event, but what could be coming with the announcement?\",\"Content\":\"Audio player loading\\u2026\\nOverwatch 2\\u0027s PvP beta has ended, pushing players back into the void of Overwatch 1 for a while. However, developer Blizzard has teased a new event for June, meaning we shouldn\\u0027t be waiting too long before we can hop back into t... [3158 chars]\",\"Link\":\"https://www.techradar.com/news/second-overwatch-2-beta-on-the-way-with-new-content-expected-is-junker-queen-coming\",\"Image\":\"https://cdn.mos.cms.futurecdn.net/ykVwbrYmAGghaeAY3YLzdG-1200-80.jpg\",\"Date\":\"2022-05-18T16:00:00Z\",\"Source\":{\"Name\":\"TechRadar\",\"Url\":\"https://www.techradar.com\"}},{\"Title\":\"5 Things We Want in the Next Overwatch 2 Beta\",\"Description\":\"The first beta gave us a taste of 5v5 gameplay, and now we\\u0027re ready for more.\",\"Content\":\"The first Overwatch 2 PvP beta came to a close on Tuesday, May 17, capping off three weeks of frenzied hero-shooter action. It was my first hands-on experience with the game, and the first anyone had really seen of it since the PvE demo at Blizzcon 2... [6456 chars]\",\"Link\":\"https://www.cnet.com/tech/gaming/5-things-we-want-in-the-next-overwatch-2-beta/\",\"Image\":\"https://www.cnet.com/a/img/resize/274cb031e8fe2dd499e1d00e609b8c792b64b4e2/2019/11/01/9eaa873b-ecfa-4eed-92dd-f2dc7f171f42/screen-shot-2019-11-01-at-3-18-56-pm.png?auto=webp\\u0026fit=crop\\u0026height=630\\u0026width=1200\",\"Date\":\"2022-05-18T12:00:17Z\",\"Source\":{\"Name\":\"CNET\",\"Url\":\"https://www.cnet.com\"}},{\"Title\":\"Blizzard is teasing more Overwatch 2 stuff for next month\",\"Description\":\"Blizzard announced they will have something special in store for the Overwatch 2\\nfans but the next test might be a little further out.\",\"Content\":\"Blizzard announced they will have something special in store for the Overwatch 2 fans but the next test might be a little further out.\\nOverwatch 2 has recently completed a beta test run and the reception was mixed. Some players were just happy to get... [1305 chars]\",\"Link\":\"https://www.altchar.com/game-news/blizzard-is-teasing-more-overwatch-2-stuff-for-next-month-at1iA3n8ANbw\",\"Image\":\"https://media.altchar.com/prod/images/940_530/gm-2c539436-4b6e-4878-b5f9-e765d3bed175-ow2.jpg\",\"Date\":\"2022-05-18T09:10:00Z\",\"Source\":{\"Name\":\"AltChar\",\"Url\":\"https://www.altchar.com\"}},{\"Title\":\"Overwatch Anniversary Remix: Vol 2 Event Now Live - Rewards And Schedule\",\"Description\":\"New legendary skin variants and a second chance to earn previous challenge event rewards are up for grabs.\",\"Content\":\"Overatch\\u0027s Anniversary Remix event is here again, bringing back past seasonal brawls, cosmetics, and challenge skins for a limited time. The Anniversary Remix Vol. 2 event serves as a greatest hits of all of Overwatch\\u0027s past seasonal events. Brawls f... [2591 chars]\",\"Link\":\"https://www.gamespot.com/articles/overwatch-anniversary-remix-vol-2-event-now-live-rewards-and-schedule/1100-6503556/\",\"Image\":\"https://www.gamespot.com/a/uploads/screen_kubrick/1647/16470614/3978467-overwatchanniversaryremixvol2rewardsandschedule.jpg\",\"Date\":\"2022-05-17T21:01:44Z\",\"Source\":{\"Name\":\"GameSpot\",\"Url\":\"https://www.gamespot.com\"}},{\"Title\":\"As the Overwatch 2 beta ends, a new Overwatch remix event begins\",\"Description\":\"The first Overwatch 2 closed beta has ended to a lukewarm reception. Blizzard has also announced that the next installment of the Overwatch anniversary event begins today, and there will be an Overwatch 2 event on June 16th.\",\"Content\":\"The Overwatch 2 beta is now over. If you were one of the lucky handful that were able to snag an invite, congratulations. If not, don\\u2019t worry. There will likely be more opportunities to try out Overwatch 2 in the future.\\nOne of those opportunities co... [2048 chars]\",\"Link\":\"https://www.theverge.com/2022/5/17/23101045/blizzard-overwatch-2-beta-remix-event-anniversary\",\"Image\":\"https://cdn.vox-cdn.com/thumbor/W8aD0w2gd5WQM8gIFEBoDkcyLgk=/0x38:1920x1043/fit-in/1200x630/cdn.vox-cdn.com/uploads/chorus_asset/file/23520943/sombrakitty.png\",\"Date\":\"2022-05-17T20:10:58Z\",\"Source\":{\"Name\":\"The Verge\",\"Url\":\"https://www.theverge.com\"}}]";
            //try
            //{
            //    var deserialisation = JsonConvert.DeserializeObject<GNewsResponse>(result);
            //    newdata = System.Text.Json.JsonSerializer.Serialize<List<GNews>>(deserialisation.Articles);

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}

            return newdata;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
