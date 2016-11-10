using System;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace PCentar
{
    internal class SvestenikDal
    {
        public int UbaciSvestenika(Svestenik s)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO tblSvestenik (imePrezimeSvestenika,nazivParohije,logUsername,logPassword,telFiksni,telMobilni,emailUserName,emailPassword) VALUES ");
            sb.AppendLine("(@imePrezimeSvestenika, @nazivParohije,@logUsername,@logPassword,@telFiksni,@telMobilni,@emailUserName,@emailPassword )");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            kom.Parameters.AddWithValue("@imePrezimeSvestenika", s.imePrezimeSvestenika);
            kom.Parameters.AddWithValue("@nazivParohije", s.nazivParohije);
            kom.Parameters.AddWithValue("@username", s.logUserName);
            kom.Parameters.AddWithValue("@logPassword", s.logPassword);
            kom.Parameters.AddWithValue("@telFiksni", s.telFiksni);
            kom.Parameters.AddWithValue("@telMobilni", s.telMobilni);
            kom.Parameters.AddWithValue("@emailUserName", s.emailUserName);
            kom.Parameters.AddWithValue("@emailPassword", s.emailPassword);

            try
            {
                con.Open();
                kom.ExecuteNonQuery();
                return 1;
            }
            catch (Exception)
            {
                return -1;
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public Svestenik VratiIDSvestenika(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM tblSvestenik WHERE SvestenikId = @SvestenikId");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            kom.Parameters.AddWithValue("@SvestenikId", id);

            Svestenik s = new Svestenik();

            try
            {
                con.Open();
                OleDbDataReader rider = kom.ExecuteReader();

                while (rider.Read())
                {
                    s.SvestenikId = Convert.ToInt32(rider["SvestenikId"].ToString());
                    s.imePrezimeSvestenika = rider["imePrezimeSvestenika"].ToString();
                    s.nazivParohije = rider["nazivParohije"].ToString();
                    s.telFiksni = rider["telFiksni"].ToString();
                    s.telMobilni = rider["telMobilni"].ToString();
                    s.emailUserName = rider["emailUserName"].ToString();
                    s.emailPassword = rider["emailPassword"].ToString();
                    s.logUserName = rider["logUserName"].ToString();
                    s.logPassword = rider["logPassword"].ToString();
                }
                return s;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        public int SacuvajSvestenika(Svestenik s)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE tblSvestenik SET imePrezimeSvestenika = @imePrezimeSvestenika, nazivParohije = @nazivParohije, logUserName = @logUserName,");
            sb.AppendLine("logPassword = @logPassword, telFiksni = @telFiksni, telMobilni = @telMobilni, emailUserName = @emailUserName, emailPassword = @emailPassword");
            sb.AppendLine("WHERE SvestenikId = @SvestenikId");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            kom.Parameters.AddWithValue("@imePrezimeSvestenika", s.imePrezimeSvestenika);
            kom.Parameters.AddWithValue("@nazivParohije", s.nazivParohije);
            kom.Parameters.AddWithValue("@logUserName", s.logUserName);
            kom.Parameters.AddWithValue("@logPassword", s.logPassword);
            kom.Parameters.AddWithValue("@telFiksni", s.telFiksni);
            kom.Parameters.AddWithValue("@telMobilni", s.telMobilni);
            kom.Parameters.AddWithValue("@emailUserName", s.emailUserName);
            kom.Parameters.AddWithValue("@emailPassword", s.emailPassword);
            kom.Parameters.AddWithValue("@SvestenikId", s.SvestenikId);

            try
            {
                con.Open();
                kom.ExecuteNonQuery();
                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            finally
            {
                con.Close();
            }
        }
    }
}