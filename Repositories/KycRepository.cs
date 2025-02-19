using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient; 
using KycApi.Data;
using KycApi.Models;

namespace KycApi.Repositories
{
    public class KycRepository : IKycRepository
    {
        private readonly DbHelper _dbHelper;
 

        public KycRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public List<KycModel> GetAll()
        {
            List<KycModel> list = new List<KycModel>();
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM KYC", conn))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(new KycModel
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Province = reader["Province"].ToString(),
                            District = reader["District"].ToString(),
                            VDC = reader["VDC"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public KycModel GetById(int id)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM KYC WHERE Id = @Id", conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return new KycModel
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Phone = reader["Phone"].ToString(),
                            Email = reader["Email"].ToString(),
                            Province = reader["Province"].ToString(),
                            District = reader["District"].ToString(),
                            VDC = reader["VDC"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public bool Create(KycModel kyc)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string checkQuery = "SELECT COUNT(1) FROM KYC WHERE Email = @Email OR Phone = @Phone";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", kyc.Email);
                    checkCmd.Parameters.AddWithValue("@Phone", kyc.Phone);
                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        throw new Exception("KYC record with the same Email or Phone already exists.");
                    }
                }

                //  Query
                string insertQuery = "INSERT INTO KYC (Name, Address, Phone, Email, Province, District, VDC) " +
                                     "VALUES (@Name, @Address, @Phone, @Email, @Province, @District, @VDC)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", kyc.Name);
                    cmd.Parameters.AddWithValue("@Address", kyc.Address);
                    cmd.Parameters.AddWithValue("@Phone", kyc.Phone);
                    cmd.Parameters.AddWithValue("@Email", kyc.Email);
                    cmd.Parameters.AddWithValue("@Province", kyc.Province);
                    cmd.Parameters.AddWithValue("@District", kyc.District);
                    cmd.Parameters.AddWithValue("@VDC", kyc.VDC);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Update(int id, KycModel kyc)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(1) FROM KYC WHERE (Email = @Email OR Phone = @Phone) AND Id != @Id";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", kyc.Email);
                    checkCmd.Parameters.AddWithValue("@Phone", kyc.Phone);
                    checkCmd.Parameters.AddWithValue("@Id", id);

                    int exists = (int)checkCmd.ExecuteScalar();
                    if (exists > 0)
                    {
                        throw new Exception("Another KYC record with the same Email or Phone already exists.");
                    }
                }

                // Update Query
                string updateQuery = "UPDATE KYC SET Name = @Name, Address = @Address, Phone = @Phone, " +
                                     "Email = @Email, Province = @Province, District = @District, VDC = @VDC " +
                                     "WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", kyc.Name);
                    cmd.Parameters.AddWithValue("@Address", kyc.Address);
                    cmd.Parameters.AddWithValue("@Phone", kyc.Phone);
                    cmd.Parameters.AddWithValue("@Email", kyc.Email);
                    cmd.Parameters.AddWithValue("@Province", kyc.Province);
                    cmd.Parameters.AddWithValue("@District", kyc.District);
                    cmd.Parameters.AddWithValue("@VDC", kyc.VDC);
                    cmd.Parameters.AddWithValue("@Id", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }


        public bool Delete(int id)
        {
            using (var conn = _dbHelper.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM KYC WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
