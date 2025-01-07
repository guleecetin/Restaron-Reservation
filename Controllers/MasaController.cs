using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestoranRezervasyonu.Models;

namespace RestoranRezervasyonu.Controllers
{
    
    public class MasaController : Controller
    {
        private readonly IMasaRepository _masaRepository;

        public MasaController(IMasaRepository masaRepository)
        {
            _masaRepository = masaRepository;
        }

        //  public IActionResult Index()
        // {
        //     List<Masa> objMasaList = _masaRepository.GetAll().ToList();
        //   return View(objMasaList);
        // }
        public IActionResult Index()
        {
            var masalar = _masaRepository.GetAll().OrderBy(m => m.TableNumber).ToList();
            ViewBag.SelectedMasaId = TempData["selectedMasaId"]; // Seçilen masayı görünümle paylaş
            return View(masalar);
        }


        public IActionResult EkleGuncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return View();
            }
            else
            {
                Masa? masa = _masaRepository.Get(u => u.Id == id);
                if (masa == null)
                {
                    return NotFound();
                }
                return View(masa);
            }
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Masa masa)
        {
            if (ModelState.IsValid)
            {
                if (masa.Id == 0)
                {
                    _masaRepository.Ekle(masa);
                    TempData["basarili"] = "The new table has been created successfully.";
                }
                else
                {
                    _masaRepository.Guncelle(masa);
                    TempData["basarili"] = "The table has been updated successfully.";
                }

                _masaRepository.Kaydet();
                TempData["selectedMasaId"] = masa.Id; // Seçilen masanın ID'sini sakla
                return RedirectToAction("Index");
            }
            TempData["hata"] = "Please correct the errors and try again.";
            return View(masa);
        }


        [HttpPost]
        public IActionResult DurumGuncelle(int id, bool doluMu)
        {
            var masa = _masaRepository.Get(u => u.Id == id);
            if (masa == null)
            {
                return Json(new { success = false, message = "Table not found." });
            }

            masa.IsOccupied = doluMu;
            masa.LastUpdated = DateTime.Now;
            _masaRepository.Guncelle(masa);
            _masaRepository.Kaydet();

            return Json(new { success = true, message = "Table status updated" });
        }

        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Masa? masa = _masaRepository.Get(u => u.Id == id);
            if (masa == null)
            {
                return NotFound();
            }
            return View(masa);
        }

        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Masa? masa = _masaRepository.Get(u => u.Id == id);
            if (masa == null)
            {
                return NotFound();
            }
            _masaRepository.Sil(masa);
            _masaRepository.Kaydet();
            TempData["basarili"] = "Table deletion was successful.";
            return RedirectToAction("Index", "Masa");
        }
    }
}
