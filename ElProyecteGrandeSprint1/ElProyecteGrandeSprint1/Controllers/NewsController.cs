﻿using System.Diagnostics;
using ElProyecteGrandeSprint1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElProyecteGrandeSprint1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        private readonly ApiController _apiController = new ApiController();
        private readonly ILogger<NewsController> _logger;

        private readonly string apiToken = "1763716cc7950373caaf84e0980fd9da";
        private readonly string language = "en";

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetVideoGamesNews()
        {
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

            //var deals = _apiController.GetDataFromApi<News>(request);
            var deals = "[{\"title\":\"Pokemon GO Could Be Adding Ultra Beasts Soon\",\"date\":\"Wed, 25 May 2022 07:37:42 GMT\",\"description\":\"Niantic shares a short Pokemon GO video online which hints that Ultra Beasts may be coming to the mobile game sometime in the future.\",\"image\":\"https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/05/pokemon-sun-and-moon-nihilego-ultra-beast-(1)-2.jpg\",\"link\":\"https://gamerant.com/pokemon-go-add-ultra-beasts-soon/\"},{\"title\":\"Blizzard Reveals The Most-Played Characters In The Overwatch 2 Beta\",\"date\":\"Wed, 25 May 2022 00:10:00 -0700\",\"description\":\"Overwatch 2's first PvP beta just wrapped up on May 17, leaving the development team with a lot of data to sift through before the next beta begins. Now, Blizzard has shared some of that data in a blog post, revealing some interesting stats on hero usage rates, win rates, and the effects of recent buffs and nerfs.\",\"image\":\"https://www.gamespot.com/a/uploads/screen_medium/1597/15975876/3981567-3965050-ovr_sojourn_002.jpg\",\"link\":\"https://www.gamespot.com/articles/blizzard-reveals-the-most-played-characters-in-the-overwatch-2-beta/1100-6503817/?ftag=CAD-01-10abi2f\"},{\"title\":\"$50 Off The PS5’s Pulse 3D Headset Sounds Pretty Good\",\"date\":\"Wed, 25 May 2022 07:23:26 +0000\",\"description\":\"If you're looking to take full advantage of the PS5's Tempest 3D AudioTech, here's everywhere you can snag Sony's official headset with a nice discount.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2020/09/21/pulse-3d-wireless-headset-1.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/ps5-headset-cheap/\"},{\"title\":\"The Battle of Polytopia lets you launch sneak attacks with the new Diplomacy expansion\",\"date\":\"Wed, 25 May 2022 07:00:00 +0100\",\"description\":\"Midjiwan AB has officially announced an exciting new expansion for The Battle of Polytopia, the indie developer's hit 4X strategy game on mobile and on Steam. The Diplomacy expansion lets players enjoy new features such as Peace Treaties and Tribe Relations as a free update to the game.\\n\\nThe new technology in the Tech Tree gives players the freedom to form alliances both with humans and with AI. They can find player capitals, build embassies, send out spies and even resort to sneak attacks via Cloaks. These possess two new abilities called Infiltrate and Stealth, and they are invisible to enemy units. \",\"image\":\"https://media.pocketgamer.com/artwork/na-28042-1653461169/the-battle-of-polytopia-ios-android-steam-diplomacy-update.jpg\",\"link\":\"https://www.pocketgamer.com/the-battle-of-polytopia/launch-sneak-attacks-with-diplomacy-expansion/\"},{\"title\":\"The Lord Of The Rings: Gollum Gets Its Precious Release Date\",\"date\":\"Wed, 25 May 2022 06:13:05 +0000\",\"description\":\"In Gollum news, The Lord of the Rings: Gollum, the game where you scrounge around as a scrawny little freak, now has a release date.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/05/25/Gollums-death.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/the-lord-of-the-rings-gollum-gets-its-precious-release-date/\"},{\"title\":\"The Adorable, Tragic Tale Of Osamu Tezuka’s Unico Lives Again\",\"date\":\"Wed, 25 May 2022 05:40:26 +0000\",\"description\":\"This unicorn may look cute — and he certainly is — but don’t let his whimsy fool you. Unico was created by Osamu Tezuka, the creator of Astro Boy, who gave this adorable unicorn an edge that captivated Japanese readers of the ‘70s. Now Samuel Sattin, working with the artist team Gurihiru and with the blessing of Tezuka Productions, has launched a Kickstarter to bring Unico to a new generation.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/05/25/1d50552c48d5ffbf9a6982fec6f573d8.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/the-adorable-tragic-tale-of-osamu-tezukas-unico-lives-again/\"},{\"title\":\"Skyrim Fan Shows Off Daedric Dagger Tattoo\",\"date\":\"Wed, 25 May 2022 04:05:09 GMT\",\"description\":\"A fan of The Elder Scrolls 5: Skyrim shows off a tattoo that they got of one of the Daedric weapons that can be made in the game.\",\"image\":\"https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/05/Skyrim-Daedric-Tattoo.jpg\",\"link\":\"https://gamerant.com/skyrim-daedric-dagger-tattoo/\"},{\"title\":\"Upcoming Chocobo GP Season 2 Patch Removes Microtransactions on Retail Version\",\"date\":\"Wed, 25 May 2022 03:50:03 GMT\",\"description\":\"Due to the backlash Square Enix is facing for how Chocobo GP is riddled with microtransactions, season 2 promises to be less expensive for players.\",\"image\":\"https://static0.gamerantimages.com/wordpress/wp-content/uploads/2022/05/chocobo-gp-logo-over-chocobo-farm.jpg\",\"link\":\"https://gamerant.com/chocobo-gp-season-2-patch-removes-microtransactions/\"},{\"title\":\"Homebody Is The Next Title From Game Grumps, And It’s A Spooky One\",\"date\":\"Wed, 25 May 2022 04:42:32 +0000\",\"description\":\"From the folks that brought you Dream Daddy, a dad-banging simulator, comes a new horror game called Homebody.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/05/25/unnamed-5.jpg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/homebody-game-grumps/\"},{\"title\":\"You Can Grab The PS5 DualSense Controller For Under $80 Right Now\",\"date\":\"Wed, 25 May 2022 04:40:46 +0000\",\"description\":\"If you're currently in need of a replacement or want to pick up a spare, you can currently grab a nice deal on the PS5 DualSense controller.\",\"image\":\"https://www.kotaku.com.au/wp-content/uploads/sites/3/2022/02/18/ps5-controllers-purple-blue-pink.jpeg?quality=80=1280,720\",\"link\":\"https://www.kotaku.com.au/2022/05/ps5-controller-dualsense-wireless-red-black/\"}]";
            return deals;
        }
        
        [HttpGet("{game}")]
        public string GetGNews(string game)
        {
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Get,
            //    RequestUri = new Uri($"https://gnews.io/api/v4/search?q={game}&token={apiToken}&lang={language}"),
            //};

            //var deals = _apiController.GetDataFromApi<GNewsResponse>(request);
            var deals = "[{\"Title\":\"Arlington plays host to Overwatch League major tournament\",\"Description\":\"There was a lot of hype and energy as the line of esports fans wrapped around the walkway waiting to get in the Esports Stadium Arlington for the first Overwatch League major tournament of 2022.\",\"Content\":\"There was a lot of hype and energy as the line of esports fans wrapped around the walkway waiting to get in the Esports Stadium Arlington for the first Overwatch League major tournament of 2022.\\n\\u0022I\\u2019m really excited. My team\\u2019s here, so I get to see th... [904 chars]\",\"Link\":\"https://www.fox4news.com/news/arlington-overwatch-league-major-tournament\",\"Image\":\"https://images.foxtv.com/static.fox4news.com/www.fox4news.com/content/uploads/2022/06/1280/720/060222-ARLINGTON-ESPORTS-PKG-ANGLIN_00.00.42.15.png?ve=1\\u0026tl=1\",\"Date\":\"2022-06-02T23:53:05Z\",\"Source\":{\"Name\":\"FOX 4 News\",\"Url\":\"https://www.fox4news.com\"}},{\"Title\":\"Paris\\u2019 Overwatch and Call of Duty teams are moving to Las Vegas\",\"Description\":\"The Overwatch League team Paris Eternal and its sister Call of Duty League team, Paris Legion, will relocate to Las Vegas in 2023.\",\"Content\":\"The Overwatch League has its first official team relocation. The Paris Eternal, one of the League\\u2019s two European teams, will move to Las Vegas for the 2023 season.\\nRumors of a potential relocation started after an eagle-eyed fan noticed that DM-Espor... [2314 chars]\",\"Link\":\"https://www.theverge.com/2022/6/2/23151691/overwatch-league-paris-eternal-relocation-las-vegas\",\"Image\":\"https://cdn.vox-cdn.com/thumbor/E4D7XeNxem6Vm8bMdhoq0P9x_kY=/0x86:1200x714/fit-in/1200x630/cdn.vox-cdn.com/uploads/chorus_asset/file/23603742/OWL_Press_Asset_BZ66H0.jpg\",\"Date\":\"2022-06-02T16:43:43Z\",\"Source\":{\"Name\":\"The Verge\",\"Url\":\"https://www.theverge.com\"}},{\"Title\":\"Paris Legion, Paris Eternal to relocate to Las Vegas in 2023\",\"Description\":\"The Call of Duty League and Overwatch League franchises are set to leave the French capital for a Sin City swap ahead of the 2023 season.\",\"Content\":\"The French capital is losing two of its premier esports teams.\\nCall of Duty League and Overwatch League franchises Paris Legion and Paris Eternal will relocate to Las Vegas ahead of their respective 2023 seasons, the ownership company behind both tea... [2913 chars]\",\"Link\":\"https://dotesports.com/news/paris-legion-paris-eternal-to-relocate-to-las-vegas-in-2023\",\"Image\":\"https://cdn1.dotesports.com/wp-content/uploads/2022/06/01190755/o1554786249671216-768x512.jpeg\",\"Date\":\"2022-06-02T00:09:48Z\",\"Source\":{\"Name\":\"Dot Esports\",\"Url\":\"https://dotesports.com\"}},{\"Title\":\"Loot box laws reportedly keep Diablo Immortal out of 2 countries\",\"Description\":\"Regulators in Belgium and the Netherlands have previously ruled against Blizzard\\u2019s loot boxes in other games, like Overwatch. Blizzard Entertainment reportedly won\\u2019t release the upcoming Diablo Immortal in those countries.\",\"Content\":\"Diablo Immortal, Blizzard Entertainment\\u2019s free-to-play Diablo game, is set to launch on Windows PC and mobile Wednesday. But it\\u2019ll reportedly stay offline in two European countries: Belgium and the Netherlands.\\nThat\\u2019s because of those countries\\u2019 stri... [1653 chars]\",\"Link\":\"https://www.polygon.com/23149045/diablo-immortal-netherlands-belgium-loot-boxes\",\"Image\":\"https://cdn.vox-cdn.com/thumbor/j0SGLDffzHAjX0B157yabzoMoSY=/0x8:3480x1830/fit-in/1200x630/cdn.vox-cdn.com/uploads/chorus_asset/file/13381973/Diablo_Bone.jpg\",\"Date\":\"2022-05-31T21:49:14Z\",\"Source\":{\"Name\":\"Polygon\",\"Url\":\"https://www.polygon.com\"}},{\"Title\":\"Overwatch League may be attempting to use free artwork, labor again\",\"Description\":\"Overwatch: This time, Blizzard is having freelancers sign a document that states they can use photos and videos as they please.\",\"Content\":\"Source: Activision Blizzard\\nThe Overwatch League may be attempting to get free labor once again.\\nBack in March, the Overwatch community grew frustrated with the Overwatch League over the conditions of an art contest. It seemed innocent enough at firs... [2422 chars]\",\"Link\":\"https://www.invenglobal.com/articles/17342/overwatch-league-may-be-attempting-to-use-free-artwork-labor-again\",\"Image\":\"https://static.invenglobal.com/upload/image/2022/05/31/r1654031057081986.jpeg\",\"Date\":\"2022-05-31T21:06:20Z\",\"Source\":{\"Name\":\"InvenGlobal\",\"Url\":\"https://www.invenglobal.com\"}},{\"Title\":\"Overwatch 2\\u2019s beta ending has left players with a demoralizing hangover\",\"Description\":\"Overwatch 2\\u0027s beta ending has left the franchise in a strange purgatory, leaving players, both casual and professional in an awkward spot.\",\"Content\":\"Overwatch 2\\u2019s first beta period was always planned to end. But that doesn\\u2019t make it any easier to accept.\\nA new presentation (opens in new tab) unveiling what\\u2019s coming to Overwatch 2 in the next beta has been set for June 16. The hope is that we\\u2019ll g... [5159 chars]\",\"Link\":\"https://www.techradar.com/opinion/overwatch-2s-beta-ending-has-left-players-in-a-demoralizing-hangover\",\"Image\":\"https://cdn.mos.cms.futurecdn.net/cvVtvWkFyrMzwAEPX6gkwi-1200-80.jpg\",\"Date\":\"2022-05-28T12:00:00Z\",\"Source\":{\"Name\":\"TechRadar\",\"Url\":\"https://www.techradar.com\"}},{\"Title\":\"Despite a new hero, Ana proved to be most popular in Overwatch 2 beta\",\"Description\":\"Blizzard released some metrics behind the Overwatch 2 beta that included the\\nhero usage, with fairly surprising results.\",\"Content\":\"Blizzard released some metrics behind the Overwatch 2 beta that included the hero usage, with fairly surprising results.\\nSojourn was eagerly awaited as she would be the hero that would finally end the content draught in Overwatch so when she ended up... [1130 chars]\",\"Link\":\"https://www.altchar.com/game-news/despite-a-new-hero-ana-proved-to-be-most-popular-in-overwatch-2-beta-aiNwF3w9oQ0W\",\"Image\":\"https://media.altchar.com/prod/images/940_530/gm-4077da2e-1959-425d-8e61-69c7951fed92-anascreenshot004.jpeg\",\"Date\":\"2022-05-26T07:03:00Z\",\"Source\":{\"Name\":\"AltChar\",\"Url\":\"https://www.altchar.com\"}},{\"Title\":\"Blizzard Reveals The Most-Played Characters In The Overwatch 2 Beta\",\"Description\":\"Here\\u0027s how much each character--including Overwatch 2\\u0027s new entry, Sojourn--was used in the beta.\",\"Content\":\"Overwatch 2\\u0027s first PvP beta just wrapped up on May 17, leaving the development team with a lot of data to sift through before the next beta begins. Now, Blizzard has shared some of that data in a blog post, revealing some interesting stats on hero u... [1762 chars]\",\"Link\":\"https://www.gamespot.com/articles/blizzard-reveals-the-most-played-characters-in-the-overwatch-2-beta/1100-6503817/\",\"Image\":\"https://www.gamespot.com/a/uploads/screen_kubrick/1597/15975876/3981567-3965050-ovr_sojourn_002.jpg\",\"Date\":\"2022-05-25T08:10:33Z\",\"Source\":{\"Name\":\"GameSpot\",\"Url\":\"https://www.gamespot.com\"}},{\"Title\":\"Luminosity, Vancouver Titans parent company CEO under fire for creating \\u0022divisive culture\\u0022 at esports org\",\"Description\":\"Overwatch: Investors call for Enthusiast Gaming CEO to be replaced.\",\"Content\":\"\\u25B2 Image via Enthusiast Gaming\\nEnthusiast Gaming\\u0027s largest shareholder is calling for the removal of the company\\u0027s CEO and board of directors.\\nEnthusiast Gaming owns Luminosity, Vancouver Titans, and the Seattle Surge. The esports org has come under f... [3345 chars]\",\"Link\":\"https://www.invenglobal.com/articles/17283/luminosity-vancouver-titans-parent-company-ceo-under-fire-for-creating-divisive-culture-at-esports-org\",\"Image\":\"https://static.invenglobal.com/upload/image/2022/05/24/o1653411306262762.jpeg\",\"Date\":\"2022-05-24T16:56:52Z\",\"Source\":{\"Name\":\"InvenGlobal\",\"Url\":\"https://www.invenglobal.com\"}},{\"Title\":\"NYC Mayor Says Overwatch\\u0027s Hanzo Is Garbage\",\"Description\":\"Wish this were clickbait, but it\\u0027s not.\",\"Content\":\"It\\u0027s rare to see a politician express opinions on video games, or at least somewhat informed opinions. NYC Mayor Eric Adams decided to break precedent and throw in his two cents about one Overwatch character in particular--Hanzo.\\nI couldn\\u0027t mash the ... [1542 chars]\",\"Link\":\"https://www.gamespot.com/articles/nyc-mayor-says-overwatchs-hanzo-is-garbage/1100-6503655/\",\"Image\":\"https://www.gamespot.com/a/uploads/screen_kubrick/1597/15971658/3979363-hanzo_thumb.jpg\",\"Date\":\"2022-05-19T19:40:25Z\",\"Source\":{\"Name\":\"GameSpot\",\"Url\":\"https://www.gamespot.com\"}}]";
            return deals;
        }
    }
}
