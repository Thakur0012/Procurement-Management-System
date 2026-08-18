using System;
using System.Linq;
using System.Web.Mvc;
using ProcureFlowX.DAL;
using ProcureFlowX.Models;
using ProcureFlowX.Log;

namespace ProcureFlowX.Controllers
{
    public class GoodsReceiptController : Controller
    {
        private GoodsReceiptDAL dal = new GoodsReceiptDAL();
        private SupplierDAL supplierDal = new SupplierDAL();
        private readonly ProductDAL productDal = new ProductDAL();


        public ActionResult Index()
        {
            try
            {
                var data = dal.GetAllDT();
                Logger.LogSuccess("GoodsReceipt list fetched");
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
                    new SelectList(supplierDal.GetActive(), "SupplierId", "SupplierName");

                Logger.LogSuccess("GoodsReceipt Create page loaded");
                return View();
            }
            catch (Exception ex)
            {
                Logger.LogError("Create GET Error: " + ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Create(GoodsReceiptModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    dal.Create(model);
                    Logger.LogSuccess("GoodsReceipt created for Supplier ID: " + model.SupplierId);
                    return RedirectToAction("Index");
                }

                // Repopulate active suppliers if validation fails
                ViewBag.SupplierList =
                    new SelectList(supplierDal.GetActive(), "SupplierId", "SupplierName", model.SupplierId);

                Logger.LogError("Create validation failed");
                return View(model);
            }
            catch (Exception ex)
            {
                Logger.LogError("Create POST Error: " + ex.Message);
                throw;
            }
        }

        // Load products for selected supplier (AJAX)
        public JsonResult LoadProducts(int supplierId)
        {
            try
            {
                var products = productDal.GetProductsBySupplier(supplierId)
                                         .Where(p => p.IsActive)
                                         .Select(p => new {
                                             p.ProductId,
                                             p.ProductName,
                                             p.StockQty,
                                             UnitPrice = p.UnitPrice
                                         }).ToList();

                Logger.LogSuccess("Products loaded for Supplier ID: " + supplierId);
                return Json(products, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError("LoadProducts Error: " + ex.Message);
                throw;
            }
        }


        public ActionResult Details(int id)
        {
            try
            {
                var grn = dal.GetDetails(id);
                if (grn == null)
                {
                    Logger.LogError("Details not found for GRN ID: " + id);
                    return HttpNotFound();
                }

                Logger.LogSuccess("Details viewed for GRN ID: " + id);
                return View(grn);
            }
            catch (Exception ex)
            {
                Logger.LogError("Details Error: " + ex.Message);
                throw;
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var grn = dal.GetSingle(id);
                if (grn == null)
                {
                    Logger.LogError("Edit not found for GRN ID: " + id);
                    return HttpNotFound();
                }

                Logger.LogSuccess("Edit page loaded for GRN ID: " + id);
                return View(grn);
            }
            catch (Exception ex)
            {
                Logger.LogError("Edit GET Error: " + ex.Message);
                throw;
            }
        }

        [HttpPost]
        public ActionResult Edit(int GRNId, DateTime GRNDate, string GRNStatus)
        {
            try
            {
                dal.Update(GRNId, GRNDate, GRNStatus);
                Logger.LogSuccess("GoodsReceipt updated ID: " + GRNId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Logger.LogError("Edit POST Error: " + ex.Message);
                throw;
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                dal.Delete(id);
                Logger.LogSuccess("GoodsReceipt deleted ID: " + id);
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