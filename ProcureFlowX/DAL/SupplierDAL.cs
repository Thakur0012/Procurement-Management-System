using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ProcureFlowX.Models;
using ProcureFlowX.Log;

namespace ProcureFlowX.DAL
{
    public class SupplierDAL
    {
        // Connection string from Web.config
        private string cs = ConfigurationManager.ConnectionStrings["MVCPRACTICE"].ConnectionString;

        public List<SupplierModel> GetAll()
        {
            List<SupplierModel> list = new List<SupplierModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Supplier_GetAll", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        list.Add(new SupplierModel
                        {
                            SupplierId = (int)dr["SupplierId"],
                            SupplierName = dr["SupplierName"].ToString(),
                            ContactNumber = dr["ContactNumber"].ToString(),
                            EmailAddress = dr["EmailAddress"].ToString(),
                            SupplierAddress = dr["SupplierAddress"].ToString(),
                            IsActive = (bool)dr["IsActive"]
                        });
                    }
                }

                Logger.LogSuccess("DAL: GetAll Suppliers executed");
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetAll Suppliers Error: " + ex.Message);
                throw;
            }

            return list;
        }

        public List<SupplierModel> GetActive()
        {
            List<SupplierModel> list = new List<SupplierModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Supplier_GetActive", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        list.Add(new SupplierModel
                        {
                            SupplierId = (int)dr["SupplierId"],
                            SupplierName = dr["SupplierName"].ToString()
                        });
                    }
                }

                Logger.LogSuccess("DAL: GetActive Suppliers executed");
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetActive Suppliers Error: " + ex.Message);
                throw;
            }

            return list;
        }

        public void Insert(SupplierModel s)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Supplier_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierName", s.SupplierName);
                    cmd.Parameters.AddWithValue("@ContactNumber", s.ContactNumber);
                    cmd.Parameters.AddWithValue("@EmailAddress", s.EmailAddress);
                    cmd.Parameters.AddWithValue("@SupplierAddress", s.SupplierAddress);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Supplier inserted - " + s.SupplierName);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Insert Supplier Error: " + ex.Message);
                throw;
            }
        }

        public SupplierModel GetById(int id)
        {
            SupplierModel s = new SupplierModel();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Supplier_GetById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierId", id);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        s.SupplierId = (int)dr["SupplierId"];
                        s.SupplierName = dr["SupplierName"].ToString();
                        s.ContactNumber = dr["ContactNumber"].ToString();
                        s.EmailAddress = dr["EmailAddress"].ToString();
                        s.SupplierAddress = dr["SupplierAddress"].ToString();
                        s.IsActive = (bool)dr["IsActive"];
                    }
                }

                Logger.LogSuccess("DAL: Supplier fetched ID = " + id);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetById Supplier Error: " + ex.Message);
                throw;
            }

            return s;
        }

        public void Update(SupplierModel s)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Supplier_Update", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierId", s.SupplierId);
                    cmd.Parameters.AddWithValue("@SupplierName", s.SupplierName);
                    cmd.Parameters.AddWithValue("@ContactNumber", s.ContactNumber);
                    cmd.Parameters.AddWithValue("@EmailAddress", s.EmailAddress);
                    cmd.Parameters.AddWithValue("@SupplierAddress", s.SupplierAddress);
                    cmd.Parameters.AddWithValue("@IsActive", s.IsActive);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Supplier updated ID = " + s.SupplierId);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Update Supplier Error: " + ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Supplier_Delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierId", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Supplier deleted ID = " + id);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Delete Supplier Error: " + ex.Message);
                throw;
            }
        }

        public List<SupplierSalesModel> GetSalesReport(int supplierId, string type)
        {
            List<SupplierSalesModel> list = new List<SupplierSalesModel>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("sp_Supplier_Sales_Report", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                cmd.Parameters.AddWithValue("@Type", type);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new SupplierSalesModel
                    {
                        ProductName = dr["ProductName"].ToString(),
                        TotalQty = Convert.ToInt32(dr["TotalQty"])
                    });
                }
            }

            return list;
        }
    }
}