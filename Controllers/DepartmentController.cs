using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SiparisTakip.Models;
using SiparisTakip.Models.Tables;

namespace SiparisTakip.Controllers
{

    public class DepartmentController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public DepartmentController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
        }


        public IActionResult Index()
        {
            return View(_siparisTakipDB.Departments.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind("depName")] Department dep)
        {
     
            if (ModelState.IsValid)
            {
                _siparisTakipDB.Add(dep);
                _siparisTakipDB.SaveChanges();
            
            }
            return RedirectToAction("Index", "Department");
        }

        public IActionResult Delete(int? id)
        {

            if (id==null)
            {
                return NotFound();

            }

            var deps =  _siparisTakipDB.Departments.FirstOrDefault(m => m.depId == id);
            if (deps == null)
            {
                return RedirectToAction("Index", "Department");
            }


            _siparisTakipDB.Departments.Remove(deps);
             _siparisTakipDB.SaveChanges();

            return RedirectToAction("Index", "Department");
        }
    }
}
