using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace A05
{
    class Aktivnost
    {
        public int AktivnostID { get; set; }
        public string NazivAktivnosti { get; set; }
        public int DanID { get; set; }
        public DateTime Pocetak { get; set; }
        public DateTime Zavrsetak { get; set; }
        public string Message { get; set; }

        public Aktivnost() { }

        public Aktivnost(DataRow dr)
        {
            this.InicijalizujPolja(dr);
        }

        public void InicijalizujPolja(DataRow dr)
        {
            this.AktivnostID = Convert.ToInt32(dr["AktivnostID"]);
            this.NazivAktivnosti = Convert.ToString(dr["NazivAktivnosti"]);
            this.DanID = Convert.ToInt32(dr["DanID"]);
            this.Pocetak = Convert.ToDateTime(dr["Pocetak"]);
            this.Zavrsetak = Convert.ToDateTime(dr["Zavrsetak"]);
        }

        public static List<Aktivnost> UcitajSve()
        {
            List<Aktivnost> lista = new List<Aktivnost>();
            SqlCommand cmd = Konekcija.GetCommand();
            cmd.CommandText = "usp_Aktivnost";
            cmd.Parameters.AddWithValue("@akcija", 0);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                        lista.Add(new Aktivnost(dr));
                    return lista;
                }
                else
                    throw new Exception("Ništa nije pronađeno u tabeli");
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public Aktivnost(int sifra)
        {
            this.AktivnostID = sifra;
            this.Ucitaj();
        }

        public bool Ucitaj()
        {
            SqlCommand cmd = Konekcija.GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Aktivnost";
            cmd.Parameters.AddWithValue("@akcija", 0);
            cmd.Parameters.AddWithValue("@sifra", this.AktivnostID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection.Open();
                da.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    this.InicijalizujPolja(dt.Rows[0]);
                    this.Message = "Podatak je pronađen";
                    return true;
                }
                else
                    throw new Exception(String.Format("Podatak sa sifrom: {0} nije pronađen", this.AktivnostID));
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                return false;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public bool Unesi()
        {
            SqlCommand cmd = Konekcija.GetCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Aktivnost";
            cmd.Parameters.AddWithValue("@akcija", 1);
            cmd.Parameters.AddWithValue("@nazivaktivnosti", this.NazivAktivnosti);
            cmd.Parameters.AddWithValue("@danid", this.DanID);
            cmd.Parameters.AddWithValue("@pocetak", this.Pocetak);
            cmd.Parameters.AddWithValue("@zavrsetak", this.Zavrsetak);
            try
            {
                cmd.Connection.Open();
                if (cmd.ExecuteNonQuery() == 1)
                {
                    this.Message = "Uspešan upis";
                    return true;
                }
                else
                    throw new Exception("Upis nije izvršen");
            }
            catch (Exception ex)
            {
                this.Message = ex.Message;
                return false;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }

        public static DataTable Statistika()
        {
            SqlCommand cmd = Konekcija.GetCommand();
            cmd.CommandText = "usp_Aktivnost";
            cmd.Parameters.AddWithValue("@akcija", 2);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                cmd.Connection.Open();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                    return dt;
                else
                    throw new Exception("Nisu nadjeni podaci");
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
}