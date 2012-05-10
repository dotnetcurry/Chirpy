using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chirpy.Domain.Model;
using Chirpy.Models;
using Chirpy.Domain.Repository;
using Chirpy.Domain.Service;

namespace Chirpy.Controllers
{
    public class AdministrationController : Controller
    {
        private IChirpRepository _chirpRepository;

        public AdministrationController(IChirpRepository chirpRepository)
        {
            _chirpRepository = chirpRepository;
        }

        //
        // GET: /Administration/

        public ActionResult Index()
        {
            ChirpRepositoryService service = new ChirpRepositoryService(_chirpRepository);
            return View(service.GetAllChirps());
        }

        //
        // GET: /Administration/Details/5

        public ActionResult Details(int id = 0)
        {
            ChirpRepositoryService service = new ChirpRepositoryService(_chirpRepository);
            Chirp chirp = service.GetChirp(id);
            if (chirp == null)
            {
                return HttpNotFound();
            }
            return View(chirp);
        }

        //
        // GET: /Administration/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Administration/Create

        [HttpPost]
        public ActionResult Create(Chirp chirp)
        {
            if (ModelState.IsValid)
            {
                ChirpRepositoryService service = new ChirpRepositoryService(_chirpRepository);
                service.AddChirp(chirp);
                return RedirectToAction("Index", "Home");
            }
            return View(chirp);
        }

        //
        // GET: /Administration/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ChirpRepositoryService service = new ChirpRepositoryService(_chirpRepository);
            Chirp chirp = service.GetChirp(id);
            if (chirp == null)
            {
                return HttpNotFound();
            }
            return View(chirp);
        }

        //
        // POST: /Administration/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ChirpRepositoryService service = new ChirpRepositoryService(_chirpRepository);
            service.DeleteChirp(id);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}