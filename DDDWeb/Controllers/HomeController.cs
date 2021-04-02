using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DDDWeb.Controllers {
    public class HomeController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController (IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index () {
            ViewBag.developers = _unitOfWork.Developers.GetAll ();
            return View ();
        }

        public IActionResult Create () {
            return View ();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (Developer developer) {
            _unitOfWork.Developers.Add (developer);
            return RedirectToAction (nameof (Index));
        }
        public IActionResult Edit (int id) {
            var developer = _unitOfWork.Developers.GetById (id);
            return View (developer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (int id, Developer developer) {
            await _unitOfWork.Developers.Edit (id, developer);
            return View ();
        }
        public IActionResult Delete (int id) {
            var developer = _unitOfWork.Developers.GetById (id);
            _unitOfWork.Developers.Remove (developer);
            return RedirectToAction (nameof (Index));
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}