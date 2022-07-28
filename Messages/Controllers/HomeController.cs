using Messages.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Messages.Data;

namespace Messages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MessagesDbContext _db;
        public HomeController(ILogger<HomeController> logger, MessagesDbContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowMessages()
        {
            if (Profile.UserName == "")
                return RedirectToAction("Index");

            List<Note> mess = new List<Note>();
            foreach (Message message in _db.Messages)
            {
                if (message.Receiver == Profile.UserName)
                {
                    Note tmp = new Note();
                    tmp.Body = message.Body;
                    tmp.Title = message.Title;
                    tmp.Sender = message.Sender;
                    mess.Add(tmp);
                }
            }
            return View(mess);
        }
        [HttpPost]
        public IActionResult GetUser(string user)
        {
            Profile.UserName = user;
            return RedirectToAction("ShowMessages");
            
        }

        [HttpPost]
        public ActionResult AddMessage(List<string> message)
        {
            Message tmp = new Message();
            tmp.Receiver = message[0];
            tmp.Title = message[1];
            tmp.Body = message[2];
            tmp.Sender = Profile.UserName;
            if (message[0] != null && message[1] != null && message[2] != null)
            {
                _db.Messages.Add(tmp);
                _db.SaveChanges();
            }
            return RedirectToAction("ShowMessages");
        }

        public JsonResult GetSearchValue(string search)
        {
            List<string> allsearch = _db.Messages.Where(x => x.Receiver.Contains(search)).Select(x => new string(x.Receiver))
                .ToList();
            var unique = allsearch.Distinct().ToList();
            return Json (unique);
        }

        public JsonResult UpdateMessages()
        {
            List<Note> mess = new List<Note>();
            foreach (Message message in _db.Messages)
            {
                if (message.Receiver == Profile.UserName)
                {
                    Note tmp = new Note();
                    tmp.Body = message.Body;
                    tmp.Title = message.Title;
                    tmp.Sender = message.Sender;
                    mess.Add(tmp);
                }
            }
            return Json(mess);
        }

        public ActionResult Update()
        {
            if (Profile.UserName == "")
                return RedirectToAction("Index");

            List<Note> mess = new List<Note>();
            foreach (Message message in _db.Messages)
            {
                if (message.Receiver == Profile.UserName)
                {
                    Note tmp = new Note();
                    tmp.Body = message.Body;
                    tmp.Title = message.Title;
                    tmp.Sender = message.Sender;
                    mess.Add(tmp);
                }
            }
            return PartialView("_Messages", mess);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}