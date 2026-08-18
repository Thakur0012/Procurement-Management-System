using System;
using System.Data;
using System.Web.Mvc;
using ProcureFlowX.DAL;
using ProcureFlowX.Models;
using ProcureFlowX.Log;

namespace ProcureFlowX.Controllers
{
    public class ProductController : Controller
    {
        ProductDAL dal = new ProductDAL();

        public ActionResult Index()
        {
            try
            {
                var data = dal.GetAll();
                Logger.LogSuccess("Product list fetched successfully");
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
            try
            {
                ViewBag.SupplierList =
                    new SelectList(dal.GetSuppliers().AsDataView(),
                                   "SupplierId", "SupplierName");

                Logger.LogSuccess("Product Create page loaded");
                return View();
            }
            catch (Exception ex)
            {
                Logger.LogError("Create GET Error: " + ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Create(ProductModel i)
        {
            try
            {
                dal.Insert(i);
                Logger.LogSuccess("Product created: " + i.ProductName);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogError("Create POST Error: " + ex.Message);
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                ProductModel item = dal.GetById(id);

                ViewBag.SupplierList =
                    new SelectList(dal.GetSuppliers().AsDataView(),
                                   "SupplierId", "SupplierName", item.SupplierId);

                Logger.LogSuccess("Edit page loaded for Product ID: " + id);
                return View(item);
            }
            catch (Exception ex)
            {
                Logger.LogError("Edit GET Error: " + ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductModel i)
        {
            try
            {
                dal.Update(i);
                Logger.LogSuccess("Product updated: ID = " + i.ProductId);
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
                Logger.LogSuccess("Product details viewed for ID: " + id);
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
                Logger.LogSuccess("Product deleted: ID = " + id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogError("Delete Error: " + ex.Message);
                throw;
            }
        }
    }
}