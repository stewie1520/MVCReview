using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

using MVCReview.Models;
using MVCReview.ViewModel;

namespace MVCReview.Data
{
    public class DataContext
    {
        public string ConnectionString { get; set; }

        public DataContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public List<DiemCachLy> GetAllDiemCachLy()
        {
            using SqlConnection connection = GetConnection();
            connection.Open();

            string sql = "SELECT * FROM DiemCachLy";
            SqlCommand command = new SqlCommand(sql, connection);

            List<DiemCachLy> data = new List<DiemCachLy>();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                data.Add(new DiemCachLy()
                {
                    MaDiemCachLy = (int)reader["MaDiemCachLy"],
                    TenDiemCachLy = (string)reader["TenDiemCachLy"],
                    DiaChi = (string)reader["DiaChi"],
                });
            }

            reader.Close();
            return data;
        }

        public List<CongNhan> LietKeTheoDiemCachLy(int maDiemCachLy)
        {
            using SqlConnection connection = GetConnection();
            connection.Open();

            string sql = "SELECT * FROM CongNhan WHERE MaDiemCachLy = @maDiemCachLy";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("maDiemCachLy", maDiemCachLy);

            SqlDataReader reader = command.ExecuteReader();
            List<CongNhan> data = new List<CongNhan>();

            while (reader.Read())
            {
                data.Add(new CongNhan()
                {
                    MaCongNhan = (int)reader["MaCongNhan"],
                    TenCongNhan = reader["TenCongNhan"].ToString(),
                    GioiTinh = (bool)reader["GioiTinh"],
                    NamSinh = ((DateTime)reader["NamSinh"]).Year,
                    NuocVe = reader["NuocVe"].ToString(),
                    MaDiemCachLy = (int)reader["MaDiemCachLy"],
                });
            }

            return data;
        }

        public int InsertDiemCachLy(DiemCachLy dcl)
        {
            using SqlConnection connection = GetConnection();
            connection.Open();
            var sql = "INSERT INTO DiemCachLy VALUES (@MaDiemCachLy, @TenDiemCachLy, @DiaChi)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("MaDiemCachLy", dcl.MaDiemCachLy);
            command.Parameters.AddWithValue("TenDiemCachLy", dcl.TenDiemCachLy);
            command.Parameters.AddWithValue("DiaChi", dcl.DiaChi);

            return command.ExecuteNonQuery();
        }

        public List<CongNhanSoTrieuChung> LietKeTheoSoTrieuChung(int SoTrieuChung)
        {
            using SqlConnection connection = GetConnection();
            connection.Open();

            string sql = "SELECT CN.MaCongNhan, TenCongNhan, GioiTinh, NamSinh, NuocVe, MaDiemCachLy FROM CongNhan CN JOIN CN_TC CNTC ON CN.MaCongNhan = CNTC.MaCongNhan";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("SoTrieuChung", SoTrieuChung);

            SqlDataReader reader = command.ExecuteReader();
            List<CongNhanSoTrieuChung> congNhanSoTrieuChung = new List<CongNhanSoTrieuChung>();

            while (reader.Read())
            {
                int maCongNhan = (int)reader["MaCongNhan"];
                CongNhanSoTrieuChung cn = congNhanSoTrieuChung.Find(x => x.MaCongNhan == maCongNhan);
                if (cn != null)
                {
                    cn.SoTrieuChung += 1;
                }
                else
                {
                    congNhanSoTrieuChung.Add(new CongNhanSoTrieuChung()
                    {
                        MaCongNhan = (int)reader["MaCongNhan"],
                        TenCongNhan = reader["TenCongNhan"].ToString(),
                        GioiTinh = (bool)reader["GioiTinh"],
                        NamSinh = ((DateTime)reader["NamSinh"]).Year,
                        NuocVe = reader["NuocVe"].ToString(),
                        MaDiemCachLy = (int)reader["MaDiemCachLy"],
                        SoTrieuChung = 1,
                    });
                }
            }

            reader.Close();
            return congNhanSoTrieuChung.Where(x => x.SoTrieuChung >= SoTrieuChung).ToList();
        }
    }
}
