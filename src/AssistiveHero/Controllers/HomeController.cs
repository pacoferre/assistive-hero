using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using System.Speech.Synthesis;
using System.IO;
using AssistiveHero.Models;

namespace AssistiveHero.Controllers
{
    [ResponseCache(Duration = 60)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public JsonResult GetDemo()
        {
            return new JsonResult(Models.Item.GetDemo());
        }

        private static object lOb = new object();

        [HttpGet]
        public FileResult Play(string text)
        {
            List<Item> items = Models.Item.GetDemo();

            if (!Item.CanSay(ref items, text))
            {
                text = "Error";
            }

            lock(lOb)
            {
                using (var synthesizer = new SpeechSynthesizer())
                {
                    var ms = new MemoryStream();

                    foreach (var v in synthesizer.GetInstalledVoices())
                    {
                        if (v.VoiceInfo.Culture.Name.Contains("es"))
                        {
                            synthesizer.SelectVoice(v.VoiceInfo.Name);
                            break;
                        }
                    }

                    synthesizer.SetOutputToWaveStream(ms);
                    synthesizer.Speak(text);

                    ms.Position = 0;
                    return new FileStreamResult(ms, "audio/wav");
                }
            }
        }
    }
}
