using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicatonDBContext _db;

        public CategoryController(ApplicatonDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var objCategoryList = _db.Categories.ToList();
            return View(objCategoryList); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category Obj)
        {
            if (Obj.Name == Obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("NamE", "Name==DisplayOrder!!!");// key is not case sensitive
            }
            if(ModelState.IsValid)
            {
                _db.Categories.Add(Obj);
                _db.SaveChanges();
                TempData["success"] = "Nice ga chesi dobbavu ga!!!";
                return RedirectToAction("Index");
            }
            else { return View(Obj); }
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id); // SingleandDefault Single First FirstOrDefault
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            else
            {
                return View(categoryFromDb);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category Obj)
        {
            if (Obj.Name == Obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("NamE", "Name==DisplayOrder!!!");// key is not case sensitive
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Obj); // check all the options
                _db.SaveChanges();
                TempData["success"] = "Marpulu sahajame";
                return RedirectToAction("Index");
            }
            else { return View(Obj); }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id); // SingleandDefault Single First FirstOrDefault
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            else
            {
                return View(categoryFromDb);
            }
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(Category Obj) { 
            var id = Obj.Id;
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);// check all the options
            _db.SaveChanges();
            TempData["success"] = $"{obj.Name} ni thisesavu kada ra!!!";
            return RedirectToAction("Index");
        }
    }
}
