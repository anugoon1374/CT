using Microsoft.Web.Mvc;
using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Beyond.Ct.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// The current HTTP context.
        /// This override default HTTPContext so we can mock it during test.
        /// </summary>
        protected new HttpContext HttpContext;

        /// <summary>
        /// The current user culture information.
        /// </summary>
        protected CultureInfo CurrentCultureInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseController" /> class.
        /// </summary>
        protected BaseController()
        {
            // override default HttpContext
            HttpContext = HttpContext.Current;
            if (HttpContext == null) throw new NullReferenceException(nameof(System.Web.HttpContext));

            // get current culture
            CurrentCultureInfo = CultureInfo.GetCultureInfo("en-US");
            if (HttpContext.Request.UserLanguages == null || !HttpContext.Request.UserLanguages.Any()) return;

            // validate and enforce user culture to supported
            try
            {
                CurrentCultureInfo = new CultureInfo(HttpContext.Request.UserLanguages[0]);
                if (!Equals(CurrentCultureInfo, CultureInfo.GetCultureInfo("en-US")))
                {
                    CurrentCultureInfo = CultureInfo.GetCultureInfo("en-US");
                }
            }
            catch (CultureNotFoundException)
            {
                // ignore
            }
        }

        /// <summary>
        /// Redirects <see cref="Controller"/> response to another specific <see cref="Controller"/> action.
        /// </summary>
        /// <typeparam name="TController">The MVC controller.</typeparam>
        /// <param name="action">The MVC action name.</param>
        /// <returns>ActionResult.</returns>
        protected ActionResult RedirectToAction<TController>(Expression<Action<TController>> action)
            where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }
    }
}