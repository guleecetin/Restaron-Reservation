using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestoranRezervasyonu.Models;

namespace RestoranRezervasyonu.Controllers
{
    public class RezervasyonController : Controller
    {
        private readonly IRezervasyonRepository _rezervasyonRepository;
        private readonly IRezervasyonTuruRepository _rezervasyonTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public RezervasyonController(IRezervasyonRepository context,IRezervasyonTuruRepository rezervasyonTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
           _rezervasyonRepository = context;
            _rezervasyonTuruRepository = rezervasyonTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Rezervasyon> objRezervasyonList = _rezervasyonRepository.GetAll(includeProps:"RezervasyonTuru").ToList();
             return View(objRezervasyonList);
        }
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> RezervasyonTuruList = _rezervasyonTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Name,
                    Value = k.Id.ToString()
                });
            ViewBag.RezervasyonTuruList = RezervasyonTuruList;
            if(id==null || id ==0)
            {
                return View();
            }
            else
            {
                Rezervasyon? rezervasyon = _rezervasyonRepository.Get(u => u.Id == id);
                if (rezervasyon == null)
                {
                    return NotFound();
                }
                return View(rezervasyon);
            }
           
        }
        [HttpPost]
        public IActionResult EkleGuncelle(Rezervasyon rezervasyon,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string rezervasyonPath = Path.Combine(wwwRootPath, @"img");

               

                    using (var fileStream = new FileStream(Path.Combine(rezervasyonPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    rezervasyon.ResimUrl = @"\img\" + file.FileName;
                

                if (rezervasyon.Id == 0)
                {
                    _rezervasyonRepository.Ekle(rezervasyon);
                    TempData["basarili"] = "The new reservation has been created successfully.";
                }
                else
                {
                    _rezervasyonRepository.Guncelle(rezervasyon);
                    TempData["basarili"] = "The new reservation has been successfully updated.";
                }


                _rezervasyonRepository.Kaydet();
               
                return RedirectToAction("Index","Rezervasyon");
            }
            return View();
        }
     /*   public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Rezervasyon? rezervasyon =_rezervasyonRepository.Get(u=>u.Id==id);
            if (rezervasyon == null)
            {
                return NotFound();
            }
            return View(rezervasyon);
        }
     */
       /* [HttpPost]
        public IActionResult Guncelle(Rezervasyon rezervasyon)
        {
            if (ModelState.IsValid)
            {
                _rezervasyonRepository.Guncelle(rezervasyon);
                _rezervasyonRepository.Kaydet();
                TempData["basarili"] = "Rezervasyon basariyla guncellendi.";
                return RedirectToAction("Index");
            }
            return View();
        }
       */
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Rezervasyon? rezervasyon = _rezervasyonRepository.Get(u=>u.Id==id);
            if (rezervasyon== null)
            {
                return NotFound();
            }
            return View(rezervasyon);
        }
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
                Rezervasyon? rezervasyon = _rezervasyonRepository.Get(u => u.Id == id);
            if (rezervasyon== null)
                {
                    return NotFound();
                }
              _rezervasyonRepository.Sil(rezervasyon);
              _rezervasyonRepository.Kaydet();
              TempData["basarili"] = "Registration deletion was successful.";
              return RedirectToAction("Index","Rezervasyon");

            }
        }
    }

