using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

using MVCReview.Models;
using MVCReview.Data;

namespace MVCReview.Controllers
{
    public class DiemCachLyController : Controller
    {
        private DataContext _context;

        public DiemCachLyController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<DiemCachLy> diemCachLies = _context.GetAllDiemCachLy();
            return View(diemCachLies);
        }

        public IActionResult ShowInsertDiemCachLy()
        {
            return View();
        }

        public IActionResult LietKeTheoDiemCachLy(int maDiemCachLy)
        {
            List<CongNhan> data = _context.LietKeTheoDiemCachLy(maDiemCachLy);
            return View(data);
        }

        public IActionResult InsertDiemCachLy(DiemCachLy dcl)
        {
            if (ModelState.IsValid)
            {
                int count = _context.InsertDiemCachLy(dcl);
                if (count > 0)
                {
                    ViewData["thongbao"] = "Đã thêm thành công";
                }
                else
                {
                    ViewData["thongbao"] = "Thêm thất bại";
                }
            }
            return View();
        }
    }
}

