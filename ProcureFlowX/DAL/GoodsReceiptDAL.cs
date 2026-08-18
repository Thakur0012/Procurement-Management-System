using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ProcureFlowX.Models;
using ProcureFlowX.Log;

namespace ProcureFlowX.DAL
{
    public class GoodsReceiptDAL
    {
        private string cs = ConfigurationManager.ConnectionStrings["MVCPRACTICE"].ConnectionString;

        public List<GoodsReceiptModel> GetAll()
        {
            List<GoodsReceiptModel> list = new List<GoodsReceiptModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_GRN_GetAll", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        list.Add(new GoodsReceiptModel
                        {
                            GRNId = Convert.ToInt32(rdr["GRNId"]),
                            GRNDate = Convert.ToDateTime(rdr["GRNDate"]),
                            GRNStatus = rdr["GRNStatus"].ToString(),
                            SupplierName = rdr["SupplierName"].ToString()
                        });
                    }
                }

                Logger.LogSuccess("DAL: GetAll executed successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetAll Error: " + ex.Message);
                throw;
            }

            return list;
        }

        public GoodsReceiptModel GetDetails(int grnId)
        {
            GoodsReceiptModel grn = null;
            List<GoodsReceiptItemModel> items = new List<GoodsReceiptItemModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_GRN_GetDetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GRNId", grnId);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (grn == null)
                        {
                            grn = new GoodsReceiptModel
                            {
                                GRNId = Convert.ToInt32(rdr["GRNId"]),
                                GRNDate = Convert.ToDateTime(rdr["GRNDate"]),
                                GRNStatus = rdr["GRNStatus"].ToString(),
                                SupplierName = rdr["SupplierName"].ToString(),
                                ContactNumber = rdr["ContactNumber"].ToString(),
                                SupplierAddress = rdr["SupplierAddress"].ToString(),
                                Items = new List<GoodsReceiptItemModel>()
                            };
                        }

                        items.Add(new GoodsReceiptItemModel
                        {
                            ProductName = rdr["ProductName"].ToString(),
                            ReceivedQty = Convert.ToInt32(rdr["ReceivedQty"]),
                            UnitRate = Convert.ToDecimal(rdr["UnitRate"]),
                            LineTotal = Convert.ToDecimal(rdr["LineTotal"])
                        });
                    }

                    if (grn != null)
                    {
                        grn.Items = items;
                    }
                }

                Logger.LogSuccess("DAL: GetDetails executed for GRN ID: " + grnId);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetDetails Error: " + ex.Message);
                throw;
            }

            return grn;
        }

        public GoodsReceiptModel GetSingle(int grnId)
        {
            try
            {
                var grn = GetDetails(grnId);
                if (grn != null)
                {
                    grn.Items = new List<GoodsReceiptItemModel>();
                }

                Logger.LogSuccess("DAL: GetSingle executed for GRN ID: " + grnId);
                return grn;
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetSingle Error: " + ex.Message);
                throw;
            }
        }

        public void Create(GoodsReceiptModel grn)
        {
            try
            {
                if (grn.Items == null)
                    grn.Items = new List<GoodsReceiptItemModel>();

                DataTable dt = new DataTable();
                dt.Columns.Add("ProductId", typeof(int));
                dt.Columns.Add("ReceivedQty", typeof(int));
                dt.Columns.Add("UnitRate", typeof(decimal));

                foreach (var i in grn.Items)
                {
                    dt.Rows.Add(i.ProductId, i.ReceivedQty, i.UnitRate);
                }

                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_GRN_Create", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierId", grn.SupplierId);
                    cmd.Parameters.AddWithValue("@GRNDate", grn.GRNDate);

                    var param = cmd.Parameters.AddWithValue("@Items", dt);
                    param.SqlDbType = SqlDbType.Structured;
                    param.TypeName = "GRNItemType";

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Create executed for Supplier ID: " + grn.SupplierId);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Create Error: " + ex.Message);
                throw;
            }
        }

        public void Update(int grnId, DateTime grnDate, string grnStatus)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_GRN_Update", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@GRNId", grnId);
                    cmd.Parameters.AddWithValue("@GRNDate", grnDate);
                    cmd.Parameters.AddWithValue("@GRNStatus", grnStatus);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Update executed for GRN ID: " + grnId);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Update Error: " + ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_GRN_Delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@GRNId", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Delete executed for GRN ID: " + id);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Delete Error: " + ex.Message);
                throw;
            }
        }

        public DataTable GetAllDT()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlDataAdapter da = new SqlDataAdapter("sp_GRN_GetAll", con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }

                Logger.LogSuccess("DAL: GetAllDT executed successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetAllDT Error: " + ex.Message);
                throw;
            }

            return dt;
        }
    }
}