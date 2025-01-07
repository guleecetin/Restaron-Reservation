using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using RestoranRezervasyonu.Models;
using RestoranRezervasyonu.Utility;

namespace RestoranRezervasyonu.Controllers
{
    public class RezervasyonTuruController : Controller
    {
        private readonly IRezervasyonTuruRepository _rezervasyonTuruRepository;
        public RezervasyonTuruController(IRezervasyonTuruRepository context)
        {
           _rezervasyonTuruRepository = context;
        }
        public IActionResult Index()
        {
            List<RezervasyonTuru> objRezervasyonTuruList = _rezervasyonTuruRepository.GetAll().ToList();
            return View(objRezervasyonTuruList);
        }
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(RezervasyonTuru rezervasyonTuru)
        {
            if (ModelState.IsValid)
            {
               _rezervasyonTuruRepository.Ekle(rezervasyonTuru);
                _rezervasyonTuruRepository.Kaydet();
                TempData["basarili"] = "The new reservation type has been created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            RezervasyonTuru? rezervasyonTuru =_rezervasyonTuruRepository.Get(u=>u.Id==id);
            if (rezervasyonTuru == null)
            {
                return NotFound();
            }
            return View(rezervasyonTuru);
        }
        [HttpPost]
        public IActionResult Guncelle(RezervasyonTuru rezervasyonTuru)
        {
            if (ModelState.IsValid)
            {
                _rezervasyonTuruRepository.Guncelle(rezervasyonTuru);
                _rezervasyonTuruRepository.Kaydet();
                TempData["basarili"] = "The reservation type has been updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            RezervasyonTuru? rezervasyonTuru = _rezervasyonTuruRepository.Get(u=>u.Id==id);
            if (rezervasyonTuru == null)
            {
                return NotFound();
            }
            return View(rezervasyonTuru);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
                RezervasyonTuru? rezervasyonTuru = _rezervasyonTuruRepository.Get(u => u.Id == id);
            if (rezervasyonTuru == null)
                {
                    return NotFound();
                }
              _rezervasyonTuruRepository.Sil(rezervasyonTuru);
              _rezervasyonTuruRepository.Kaydet();
              TempData["basarili"] = "Registration deletion was successful.";
              return RedirectToAction("Index","RezervasyonTuru");

            }
        }
    }

