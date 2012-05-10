using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chirpy.Domain.Service;

namespace Chirpy.Controllers
{
    public class HomeController : Controller
    {
        private Domain.Repository.IChirpRepository _chirpRepository;

        public HomeController(Domain.Repository.IChirpRepository chirpRepository)
        {
            this._chirpRepository = chirpRepository;
        }

        public ActionResult Index()
        {
            ChirpRepositoryService service = new ChirpRepositoryService(_chirpRepository);
            return View(service.GetAllChirps());
        }

        public ActionResult Tags()
        {
            ChirpRepositoryService service = new ChirpRepositoryService(_chirpRepository);
            return View(_chirpRepository.GetAllChirpTags());
        }

        public ViewResult Search()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Search(FormCollection form)
        {
            IList<Domain.Model.Chirp> chirps = _chirpRepository.GetAllChirpsByTag(form["query"]);
            return View("Search", chirps);
        }
    }
}
