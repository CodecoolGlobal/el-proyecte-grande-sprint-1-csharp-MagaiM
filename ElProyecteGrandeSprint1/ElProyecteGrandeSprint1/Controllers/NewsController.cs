using System.Diagnostics;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var news = GetVideoGamesNews();
            //GetNews();
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

        private static async Task<List<News>> GetResidentEvilNews()
        {
            var result = "";
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://video-game-news.p.rapidapi.com/resident_evil"),
                Headers =
                {
                    { "X-RapidAPI-Host", "video-game-news.p.rapidapi.com" },
                    { "X-RapidAPI-Key", "300b4f9442msh9fd4cb14cea575bp1fbf95jsn2e9fef2be340" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
            var deserialisation = JsonConvert.DeserializeObject<List<News>>(result);

            return deserialisation;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
