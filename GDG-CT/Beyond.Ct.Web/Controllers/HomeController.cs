// ***********************************************************************
// Author           : Anugoon Leelaphattarakij
// ***********************************************************************
// <copyright file="HomeController.cs" company="GDG">
//     Copyright (c) Anugoon Leelaphattarakij. All rights reserved.
// </copyright>
// ***********************************************************************

using System.Web.Mvc;

namespace Beyond.Ct.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}