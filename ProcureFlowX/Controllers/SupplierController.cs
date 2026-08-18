using System;
using System.Web.Mvc;
using ProcureFlowX.DAL;
using ProcureFlowX.Models;
using ProcureFlowX.Log;

namespace ProcureFlowX.Controllers
{
    public class SupplierController : Controller
    {
        SupplierDAL dal = new SupplierDAL();

        public ActionResult Index()
        {
            try
            {
                var data = dal.GetAll();
                Logger.LogSuccess("Supplier list fetched successfully");
                return View(data);
            }
            catch (Exception ex)
            {
                Logger.LogError("Index Error: " + ex.Message);
                throw;
            }
        }

        public ActionResult Create()
        {
            Logger.LogSuccess("Create page loaded");
            return View();
        }

        [HttpPost]
        public ActionResult Create(SupplierModel s)
        {
            try
            {
                dal.Insert(s);
                Logger.LogSuccess("Supplier created: " + s.SupplierName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogError("Create Error: " + ex.Message);
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var data = dal.GetById(id);
                Logger.LogSuccess("Edit page loaded for Supplier ID: " + id);
                return View(data);
            }
            catch (Exception ex)
            {
                Logger.LogError("Edit GET Error: " + ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(SupplierModel s)
        {
            try
            {
                dal.Update(s);
                Logger.LogSuccess("Supplier updated: ID = " + s.SupplierId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogError("Edit POST Error: " + ex.Message);
                throw;
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var data = dal.GetById(id);
                Logger.LogSuccess("Details viewed for Supplier ID: " + id);
                return View(data);
            }
            catch (Exception ex)
            {
                Logger.LogError("Details Error: " + ex.Message);
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                dal.Delete(id);
                Logger.LogSuccess("Supplier deleted: ID = " + id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogError("Delete Error: " + ex.Message);
                throw;
            }
        }

        public JsonResult GetSalesData(int id, string type)
        {
            var data = dal.GetSalesReport(id, type);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}