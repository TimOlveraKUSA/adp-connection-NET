using ADPClient;
using System;
using System.Web.Mvc;

namespace ADPClientWebDemo.Controllers
{
    public class callbackController : Controller
    {
        // GET: callback
        public ActionResult Index()
        {
            string returncode = null;
            AuthorizationCodeConnection connection = null;
            string error = Request.QueryString["error"];
            ViewBag.IsError = true;

            // checking if there were error/s from the api communication
            if (!String.IsNullOrEmpty(error))
            {
                ViewBag.Message = String.Format("Callback Error: {0}", error);
            }
            else {
                returncode = Request.QueryString["code"];

                // a successfull communication should result in an authorization code
                if (String.IsNullOrEmpty(returncode))
                {
                    ViewBag.Message = "Callback Error: Unauthorized";
                }
                else {
                    // callback was successfull so get connection from session
                    connection = HttpContext.Session["AuthorizationCodeConnection"] as AuthorizationCodeConnection;

                    if (connection == null)
                    {
                        ViewBag.Message = "Error: Session expired. Re-Authorization required.";
                    }
                    else {
                        // update connection's authorization code
                        ((AuthorizationCodeConfiguration)connection.connectionConfiguration).authorizationCode = returncode;
                        ViewBag.IsError = false;
                    }
                }

            }

            // cache connection in Session
            HttpContext.Session["AuthorizationCodeConnection"] = connection;

            return View("../marketplace/Index");
        }
    }
}