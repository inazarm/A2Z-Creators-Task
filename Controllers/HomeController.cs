using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Creatorz_Task.Models;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Creatorz_Task.Controllers
{
    public class HomeController : Controller
    {
        a2zCreatorEntities db = new a2zCreatorEntities();
        public ActionResult Index()
        {
            return View(db.tblCustomers.ToList());
            //var customerlist = db.tblCustomers.ToList<tblCustomer>();
            //return Json(new { data = customerlist }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult try1()
        {
            return View(db.tblCustomers.ToList());
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ExportToExcel()
        {
            var gv = new GridView();
            gv.DataSource = this.db.tblCustomers.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter HTW = new HtmlTextWriter(sw);
            gv.RenderControl(HTW);
            Response.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index");
        }

        public ActionResult GetList()
        {
            var customerlist = db.tblCustomers.ToList<tblCustomer>();
            return Json(new { data = customerlist }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult chkdebit(string Name, string contact)
        {
            ViewBag.data = Name + " " + contact; 
            return PartialView(db.SP_CreditDebit(Name,contact).ToList());
        }
        public ActionResult Credit(string Name, string contact)
        {
            ViewBag.data = Name + " " + contact;
            //return View(db.sp_getcreditdebit(Name, contact).ToList());
            return View(db.SP_CreditDebit(Name, contact).ToList());
        }

    }
}