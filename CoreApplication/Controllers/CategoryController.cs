using CoreApplication.DataAccess.Data;
using CoreApplication.DataAccess.Repository.IRepository;
using CoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        public CategoryController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitofWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            //Custom Validation
            //if(obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name");
            //}
            if (ModelState.IsValid)
            {
                _unitofWork.Category.Add(obj);
                _unitofWork.Save();
				TempData["success"] = "Category Created Successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0)
            {
                return NotFound();
            }
	
			Category? dataFromDB = _unitofWork.Category.Get(u => u.ID == id);
			//Category? dataFromDB1 = _db.Categories.Where(u => u.ID == id).FirstOrDefault();
			//Category? dataFromDB2 = _db.Categories.Find(id); //only works with primary key
            if(dataFromDB == null)
            {
                return NotFound();
            }
			return View(dataFromDB);
		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
                _unitofWork.Category.Update(obj);
                _unitofWork.Save();
				TempData["success"] = "Category Updated Successfully.";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category? dataFromDB = _unitofWork.Category.Get(u => u.ID == id);
            if (dataFromDB == null)
			{
				return NotFound();
			}
			return View(dataFromDB);
		}

		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			Category? obj = _unitofWork.Category.Get(u => u.ID == id);
            if (obj == null)
			{
				return NotFound();
			}
            _unitofWork.Category.Remove(obj);
            _unitofWork.Save();
			TempData["success"] = "Category Deleted Successfully.";
			return RedirectToAction("Index");
		}
	}
}
