using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using MVCReview.Data;
using MVCReview.ViewModel;

namespace MVCReview.Controllers
{
    public class TrieuChungController : Controller
    {
        public DataContext _context;

        public TrieuChungController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LietKe(int SoTrieuChung)
        {
            List<CongNhanSoTrieuChung> data = _context.LietKeTheoSoTrieuChung(SoTrieuChung);
            return View(data);
        }
    }
}
