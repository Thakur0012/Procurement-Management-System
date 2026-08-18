using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;
using ProcureFlowX.Models;
using ProcureFlowX.Log;

namespace ProcureFlowX.DAL
{
    public class ProductDAL
    {
        // Connection string from Web.config
        private string cs = ConfigurationManager.ConnectionStrings["MVCPRACTICE"].ConnectionString;

        public List<ProductModel> GetAll()
        {
            List<ProductModel> list = new List<ProductModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Product_GetAll", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        list.Add(new ProductModel
                        {
                            ProductId = (int)dr["ProductId"],
                            SupplierId = (int)dr["SupplierId"],
                            SupplierName = dr["SupplierName"].ToString(),
                            ProductName = dr["ProductName"].ToString(),
                            UnitPrice = (decimal)dr["UnitPrice"],
                            StockQty = (int)dr["StockQty"],
                            IsActive = (bool)dr["IsActive"]
                        });
                    }
                }

                Logger.LogSuccess("DAL: GetAll Products executed");
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetAll Products Error: " + ex.Message);
                throw;
            }

            return list;
        }

        public void Insert(ProductModel i)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Product_Insert", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierId", i.SupplierId);
                    cmd.Parameters.AddWithValue("@ProductName", i.ProductName);
                    cmd.Parameters.AddWithValue("@UnitPrice", i.UnitPrice);
                    cmd.Parameters.AddWithValue("@StockQty", i.StockQty);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Product inserted - " + i.ProductName);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Insert Product Error: " + ex.Message);
                throw;
            }
        }

        public ProductModel GetById(int id)
        {
            ProductModel i = new ProductModel();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Product_GetById", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", id);

                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        i.ProductId = (int)dr["ProductId"];
                        i.SupplierId = (int)dr["SupplierId"];
                        i.ProductName = dr["ProductName"].ToString();
                        i.UnitPrice = (decimal)dr["UnitPrice"];
                        i.StockQty = (int)dr["StockQty"];
                        i.IsActive = (bool)dr["IsActive"];
                    }
                }

                Logger.LogSuccess("DAL: Product fetched ID = " + id);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetById Product Error: " + ex.Message);
                throw;
            }

            return i;
        }

        public void Update(ProductModel i)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Product_Update", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductId", i.ProductId);
                    cmd.Parameters.AddWithValue("@SupplierId", i.SupplierId);
                    cmd.Parameters.AddWithValue("@ProductName", i.ProductName);
                    cmd.Parameters.AddWithValue("@UnitPrice", i.UnitPrice);
                    cmd.Parameters.AddWithValue("@StockQty", i.StockQty);
                    cmd.Parameters.AddWithValue("@IsActive", i.IsActive);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Product updated ID = " + i.ProductId);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Update Product Error: " + ex.Message);
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Product_Delete", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductId", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Logger.LogSuccess("DAL: Product deleted ID = " + id);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL Delete Product Error: " + ex.Message);
                throw;
            }
        }

        public DataTable GetSuppliers()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand cmd = new SqlCommand("sp_Supplier_GetActive", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                Logger.LogSuccess("DAL: Suppliers fetched");
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetSuppliers Error: " + ex.Message);
                throw;
            }

            return dt;
        }

        public List<ProductModel> GetProductsBySupplier(int supplierId)
        {
            var list = new List<ProductModel>();

            try
            {
                using (var conn = new SqlConnection(cs))
                using (var cmd = new SqlCommand("sp_GRN_GetProductsBySupplier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SupplierId", supplierId);

                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ProductModel
                            {
                                ProductId = Convert.ToInt32(reader["ProductId"]),
                                ProductName = reader["ProductName"].ToString(),
                                StockQty = Convert.ToInt32(reader["StockQty"]),
                                UnitPrice = Convert.ToDecimal(reader["UnitPrice"]),
                                SupplierId = supplierId,
                                IsActive = true
                            });
                        }
                    }
                }

                Logger.LogSuccess("DAL: Products fetched for Supplier ID = " + supplierId);
            }
            catch (Exception ex)
            {
                Logger.LogError("DAL GetProductsBySupplier Error: " + ex.Message);
                throw;
            }

            return list;
        }
    }
}