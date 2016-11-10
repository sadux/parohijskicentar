using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Text;
using System.Windows;

namespace PCentar
{
    internal class DomacinDal
    {
        public string[] BracnoStanje()
        {
            string[] Naziv = { "У браку", "Није у браку", "Разведен/а" };
            return Naziv;
        }

        public string[] DaNe()
        {
            string[] Naziv = { "Да", "Не" };
            return Naziv;
        }

        public string[] OsvecujeZito()
        {
            string[] Naziv = { "Код куће", "У цркви" };
            return Naziv;
        }

        private void DBNullProvera(string param, string polje, OleDbCommand komanda)
        {
            if (string.IsNullOrWhiteSpace(polje))
            {
                komanda.Parameters.AddWithValue(param, DBNull.Value);
            }
            else
            {
                komanda.Parameters.AddWithValue(param, polje.Trim());
            }
        }

        public List<Domacin> VratiDomacine()
        {
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand("Select * From tblDomacin ORDER BY prezimeDomacina", con);

            List<Domacin> listaDomacina = new List<Domacin>();

            try
            {
                con.Open();
                OleDbDataReader dr = kom.ExecuteReader();
                while (dr.Read())
                {
                    Domacin d = new Domacin();
                    d.idDomacin = (int)dr["idDomacin"];
                    d.imeDomacina = dr["imeDomacina"].ToString();
                    d.prezimeDomacina = dr["prezimeDomacina"].ToString();
                    d.deca = dr["deca"].ToString();
                    d.adresa = dr["adresa"].ToString();
                    d.nazivSlave = dr["nazivSlave"].ToString();
                    d.ukupnoClanova = (int)dr["ukupnoClanova"];
                    d.bracnoStanje = dr["bracnoStanje"].ToString();
                    d.parohijal = dr["parohijal"].ToString();
                    d.mesto = dr["mesto"].ToString();
                    d.sumaParohijal = dr["sumaParohijal"].ToString();
                    d.primanjeZaVaskrs = dr["primanjeVaskrs"].ToString();
                    d.primanjeZaSlavu = dr["primanjeSlava"].ToString();
                    d.daLiSlavi = dr["daLiSlavi"].ToString();
                    d.zastoNeSlavi = dr["zastoNeSlavi"].ToString();
                    d.donosiZito = dr["donosiZito"].ToString();
                    d.zapazanja = dr["zapazanja"].ToString();
                    d.email = dr["email"].ToString();
                    d.telefon1 = dr["telefon1"].ToString();
                    d.telefon2 = dr["telefon2"].ToString();
                    d.poreklo = dr["poreklo"].ToString();
                    d.datumUnosa = DateTime.Now.ToShortDateString();

                    listaDomacina.Add(d);
                }
                return listaDomacina;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Domacin> VratiKojiSlave()
        {
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand("Select * From tblDomacin WHERE daLiSlavi = 'Да' ORDER BY prezimeDomacina", con);

            List<Domacin> listaDomacina = new List<Domacin>();

            try
            {
                con.Open();
                OleDbDataReader dr = kom.ExecuteReader();
                while (dr.Read())
                {
                    Domacin d = new Domacin();
                    d.idDomacin = (int)dr["idDomacin"];
                    d.imeDomacina = dr["imeDomacina"].ToString();
                    d.prezimeDomacina = dr["prezimeDomacina"].ToString();
                    d.deca = dr["deca"].ToString();
                    d.adresa = dr["adresa"].ToString();
                    d.nazivSlave = dr["nazivSlave"].ToString();
                    d.ukupnoClanova = (int)dr["ukupnoClanova"];
                    d.bracnoStanje = dr["bracnoStanje"].ToString();
                    d.parohijal = dr["parohijal"].ToString();
                    d.mesto = dr["mesto"].ToString();
                    d.sumaParohijal = dr["sumaParohijal"].ToString();
                    d.primanjeZaVaskrs = dr["primanjeVaskrs"].ToString();
                    d.primanjeZaSlavu = dr["primanjeSlava"].ToString();
                    d.daLiSlavi = dr["daLiSlavi"].ToString();
                    d.zastoNeSlavi = dr["zastoNeSlavi"].ToString();
                    d.donosiZito = dr["donosiZito"].ToString();
                    d.zapazanja = dr["zapazanja"].ToString();
                    d.email = dr["email"].ToString();
                    d.telefon1 = dr["telefon1"].ToString();
                    d.telefon2 = dr["telefon2"].ToString();
                    d.poreklo = dr["poreklo"].ToString();
                    d.datumUnosa = DateTime.Now.ToShortDateString();

                    listaDomacina.Add(d);
                }
                return listaDomacina;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Domacin> VratiKojiDajuParohijal()
        {
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand("Select * From tblDomacin WHERE parohijal = 'Да' ORDER BY prezimeDomacina", con);

            List<Domacin> listaDomacina = new List<Domacin>();

            try
            {
                con.Open();
                OleDbDataReader dr = kom.ExecuteReader();
                while (dr.Read())
                {
                    Domacin d = new Domacin();
                    d.idDomacin = (int)dr["idDomacin"];
                    d.imeDomacina = dr["imeDomacina"].ToString();
                    d.prezimeDomacina = dr["prezimeDomacina"].ToString();
                    d.deca = dr["deca"].ToString();
                    d.adresa = dr["adresa"].ToString();
                    d.nazivSlave = dr["nazivSlave"].ToString();
                    d.ukupnoClanova = (int)dr["ukupnoClanova"];
                    d.bracnoStanje = dr["bracnoStanje"].ToString();
                    d.parohijal = dr["parohijal"].ToString();
                    d.mesto = dr["mesto"].ToString();
                    d.sumaParohijal = dr["sumaParohijal"].ToString();
                    d.primanjeZaVaskrs = dr["primanjeVaskrs"].ToString();
                    d.primanjeZaSlavu = dr["primanjeSlava"].ToString();
                    d.daLiSlavi = dr["daLiSlavi"].ToString();
                    d.zastoNeSlavi = dr["zastoNeSlavi"].ToString();
                    d.donosiZito = dr["donosiZito"].ToString();
                    d.zapazanja = dr["zapazanja"].ToString();
                    d.email = dr["email"].ToString();
                    d.telefon1 = dr["telefon1"].ToString();
                    d.telefon2 = dr["telefon2"].ToString();
                    d.poreklo = dr["poreklo"].ToString();
                    d.datumUnosa = DateTime.Now.ToShortDateString();

                    listaDomacina.Add(d);
                }
                return listaDomacina;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Domacin> VratiKojiPrimajuUskrs()
        {
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand("Select * From tblDomacin WHERE primanjeVaskrs = 'Да' ORDER BY prezimeDomacina", con);

            List<Domacin> listaDomacina = new List<Domacin>();

            try
            {
                con.Open();
                OleDbDataReader dr = kom.ExecuteReader();
                while (dr.Read())
                {
                    Domacin d = new Domacin();
                    d.idDomacin = (int)dr["idDomacin"];
                    d.imeDomacina = dr["imeDomacina"].ToString();
                    d.prezimeDomacina = dr["prezimeDomacina"].ToString();
                    d.deca = dr["deca"].ToString();
                    d.adresa = dr["adresa"].ToString();
                    d.nazivSlave = dr["nazivSlave"].ToString();
                    d.ukupnoClanova = (int)dr["ukupnoClanova"];
                    d.bracnoStanje = dr["bracnoStanje"].ToString();
                    d.parohijal = dr["parohijal"].ToString();
                    d.mesto = dr["mesto"].ToString();
                    d.sumaParohijal = dr["sumaParohijal"].ToString();
                    d.primanjeZaVaskrs = dr["primanjeVaskrs"].ToString();
                    d.primanjeZaSlavu = dr["primanjeSlava"].ToString();
                    d.daLiSlavi = dr["daLiSlavi"].ToString();
                    d.zastoNeSlavi = dr["zastoNeSlavi"].ToString();
                    d.donosiZito = dr["donosiZito"].ToString();
                    d.zapazanja = dr["zapazanja"].ToString();
                    d.email = dr["email"].ToString();
                    d.telefon1 = dr["telefon1"].ToString();
                    d.telefon2 = dr["telefon2"].ToString();
                    d.poreklo = dr["poreklo"].ToString();
                    d.datumUnosa = DateTime.Now.ToShortDateString();

                    listaDomacina.Add(d);
                }
                return listaDomacina;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Domacin> Pretraga(string polje, string textBox)
        {
            OleDbConnection con = Konekcija.VratiKonekciju();
            string upit = string.Format("Select * From tblDomacin Where {0} LIKE '{1}%' ORDER BY prezimeDomacina ", polje, textBox);
            OleDbCommand kom = new OleDbCommand(upit, con);
            List<Domacin> listaDomacina = new List<Domacin>();

            try
            {
                con.Open();
                OleDbDataReader dr = kom.ExecuteReader();
                while (dr.Read())
                {
                    Domacin d = new Domacin();
                    d.idDomacin = (int)dr["idDomacin"];
                    d.imeDomacina = dr["imeDomacina"].ToString();
                    d.prezimeDomacina = dr["prezimeDomacina"].ToString();
                    d.deca = dr["deca"].ToString();
                    d.adresa = dr["adresa"].ToString();
                    d.nazivSlave = dr["nazivSlave"].ToString();
                    d.ukupnoClanova = (int)dr["ukupnoClanova"];
                    d.bracnoStanje = dr["bracnoStanje"].ToString();
                    d.parohijal = dr["parohijal"].ToString();
                    d.sumaParohijal = dr["sumaParohijal"].ToString();
                    d.primanjeZaVaskrs = dr["primanjeVaskrs"].ToString();
                    d.primanjeZaSlavu = dr["primanjeSlava"].ToString();
                    d.daLiSlavi = dr["daLiSlavi"].ToString();
                    d.zastoNeSlavi = dr["zastoNeSlavi"].ToString();
                    d.donosiZito = dr["donosiZito"].ToString();
                    d.zapazanja = dr["zapazanja"].ToString();
                    d.email = dr["email"].ToString();
                    d.telefon1 = dr["telefon1"].ToString();
                    d.telefon2 = dr["telefon2"].ToString();
                    d.poreklo = dr["poreklo"].ToString();
                    d.datumUnosa = DateTime.Now.ToShortDateString();

                    listaDomacina.Add(d);
                }
                return listaDomacina;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public List<Domacin> Mailovi(string textBox, ref string email)
        {
            OleDbConnection con = Konekcija.VratiKonekciju();
            string upit = string.Format("Select * From tblDomacin Where nazivSlave LIKE '{0}%' AND email IS NOT NULL ORDER BY prezimeDomacina ", textBox);
            OleDbCommand kom = new OleDbCommand(upit, con);
            List<Domacin> listaDomacina = new List<Domacin>();
            List<string> listaMailova = new List<string>();
            try
            {
                con.Open();
                OleDbDataReader dr = kom.ExecuteReader();
                while (dr.Read())
                {
                    Domacin d = new Domacin();
                    d.idDomacin = (int)dr["idDomacin"];
                    d.imeDomacina = dr["imeDomacina"].ToString();
                    d.prezimeDomacina = dr["prezimeDomacina"].ToString();
                    d.deca = dr["deca"].ToString();
                    d.adresa = dr["adresa"].ToString();
                    d.nazivSlave = dr["nazivSlave"].ToString();
                    d.ukupnoClanova = (int)dr["ukupnoClanova"];
                    d.bracnoStanje = dr["bracnoStanje"].ToString();
                    d.parohijal = dr["parohijal"].ToString();
                    d.sumaParohijal = dr["sumaParohijal"].ToString();
                    d.primanjeZaVaskrs = dr["primanjeVaskrs"].ToString();
                    d.primanjeZaSlavu = dr["primanjeSlava"].ToString();
                    d.daLiSlavi = dr["daLiSlavi"].ToString();
                    d.zastoNeSlavi = dr["zastoNeSlavi"].ToString();
                    d.donosiZito = dr["donosiZito"].ToString();
                    d.zapazanja = dr["zapazanja"].ToString();
                    d.email = dr["email"].ToString();
                    d.telefon1 = dr["telefon1"].ToString();
                    d.telefon2 = dr["telefon2"].ToString();
                    d.poreklo = dr["poreklo"].ToString();
                    d.datumUnosa = DateTime.Now.ToShortDateString();
                    listaDomacina.Add(d);
                    if (!Convert.IsDBNull(d.email))
                    {
                        listaMailova.Add(d.email);
                    }
                }

                email = null;

                for (int i = 0; i < listaMailova.Count; i++)
                {
                    if (i == listaMailova.Count - 1)
                    {
                        email += listaMailova[i];
                        continue;
                    }

                    email += listaMailova[i] + ",";
                }
                return listaDomacina;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public int UbaciDomacina(Domacin d)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO tblDomacin");
            sb.AppendLine("(imeDomacina,prezimeDomacina,bracnoStanje,deca,adresa,mesto,ukupnoClanova,parohijal,sumaParohijal,primanjeVaskrs,");
            sb.AppendLine("primanjeSlava,daLiSlavi,zastoNeSlavi,donosiZito,zapazanja,email,poreklo,telefon1,telefon2,nazivSlave,datumUnosa)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(@imeDomacina,@prezimeDomacina,@bracnoStanje,@deca,@adresa,@mesto,@ukupnoClanova,@parohijal,@sumaParohijal,");
            sb.AppendLine("@primanjeVaskrs,@primanjeSlava,@daLiSlavi,@zastoNeSlavi,@donosiZito,@zapazanja,@email,@poreklo,@telefon1,");
            sb.AppendLine("@telefon2,@nazivSlave,@datumUnosa)");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            DBNullProvera("@imeDomacina", d.imeDomacina, kom);
            DBNullProvera("@prezimeDomacina", d.prezimeDomacina, kom);
            DBNullProvera("@bracnoStanje", d.bracnoStanje, kom);
            DBNullProvera("@deca", d.deca, kom);
            DBNullProvera("@adresa", d.adresa, kom);
            DBNullProvera("@mesto", d.mesto, kom);
            DBNullProvera("@ukupnoClanova", d.ukupnoClanova.ToString(), kom);
            DBNullProvera("@parohijal", d.parohijal, kom);
            DBNullProvera("@sumaParohijal", d.sumaParohijal, kom);
            DBNullProvera("@primanjeVaskrs", d.primanjeZaVaskrs, kom);
            DBNullProvera("@primanjeSlava", d.primanjeZaSlavu, kom);
            DBNullProvera("@daLiSlavi", d.daLiSlavi, kom);
            DBNullProvera("@zastoNeSlavi", d.zastoNeSlavi, kom);
            DBNullProvera("@donosiZito", d.donosiZito, kom);
            DBNullProvera("@zapazanja", d.zapazanja, kom);
            DBNullProvera("@email", d.email, kom);
            DBNullProvera("@poreklo", d.poreklo, kom);
            DBNullProvera("@telefon1", d.telefon1, kom);
            DBNullProvera("@telefon2", d.telefon2, kom);
            DBNullProvera("@nazivSlave", d.nazivSlave, kom);
            kom.Parameters.AddWithValue("@datumUnosa", d.datumUnosa);

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

        public int IspraviDomacina(Domacin d)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE tblDomacin SET imeDomacina=@imeDomacina,prezimeDomacina=@prezimeDomacina,bracnoStanje=@bracnoStanje,deca=@deca,adresa=@adresa,");
            sb.AppendLine("mesto=@mesto,ukupnoClanova=@ukupnoClanova,parohijal=@parohijal,sumaParohijal=@sumaParohijal,primanjeVaskrs=@primanjeVaskrs,primanjeSlava=@primanjeSlava,");
            sb.AppendLine("daLiSlavi=@daLiSlavi,zastoNeSlavi=@zastoNeSlavi,donosiZito=@donosiZito,zapazanja=@zapazanja,email=@email,poreklo=@poreklo,telefon1=@telefon1,");
            sb.AppendLine("telefon2=@telefon2,nazivSlave=@nazivSlave WHERE idDomacin = @idDomacin");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            DBNullProvera("@imeDomacina", d.imeDomacina, kom);
            DBNullProvera("@prezimeDomacina", d.prezimeDomacina, kom);
            DBNullProvera("@bracnoStanje", d.bracnoStanje, kom);
            DBNullProvera("@deca", d.deca, kom);
            DBNullProvera("@adresa", d.adresa, kom);
            DBNullProvera("@mesto", d.mesto, kom);
            DBNullProvera("@ukupnoClanova", d.ukupnoClanova.ToString(), kom);
            DBNullProvera("@parohijal", d.parohijal, kom);
            DBNullProvera("@sumaParohijal", d.sumaParohijal, kom);
            DBNullProvera("@primanjeVaskrs", d.primanjeZaVaskrs, kom);
            DBNullProvera("@primanjeSlava", d.primanjeZaSlavu, kom);
            DBNullProvera("@daLiSlavi", d.daLiSlavi, kom);
            DBNullProvera("@zastoNeSlavi", d.zastoNeSlavi, kom);
            DBNullProvera("@donosiZito", d.donosiZito, kom);
            DBNullProvera("@zapazanja", d.zapazanja, kom);
            DBNullProvera("@email", d.email, kom);
            DBNullProvera("@poreklo", d.poreklo, kom);
            DBNullProvera("@telefon1", d.telefon1, kom);
            DBNullProvera("@telefon2", d.telefon2, kom);
            DBNullProvera("@nazivSlave", d.nazivSlave, kom);
            kom.Parameters.AddWithValue("@idDomacin", d.idDomacin);

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

        public int ObrisiDomacina(Domacin d)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM tblDomacin WHERE idDomacin = @idDomacin");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            kom.Parameters.AddWithValue("@idDomacin", d.idDomacin);

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

        public int ObrisiBazu()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE * FROM tblDomacin");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

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

        public string UkupnoBaza()
        {
            string ukupno = null;
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand komanda = new OleDbCommand("SELECT COUNT (idDomacin) FROM tblDomacin", con);

            try
            {
                con.Open();
                ukupno = komanda.ExecuteScalar().ToString();
                return ukupno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public string UkupnoParohijana()
        {
            string ukupno = null;
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand komanda = new OleDbCommand("SELECT SUM (ukupnoClanova) FROM tblDomacin", con);

            try
            {
                con.Open();
                ukupno = komanda.ExecuteScalar().ToString();
                return ukupno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public string UkupnoKojiSlave()
        {
            string ukupno = null;
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand komanda = new OleDbCommand("SELECT COUNT (idDomacin) FROM tblDomacin WHERE daLiSlavi = 'Да'", con);

            try
            {
                con.Open();
                ukupno = komanda.ExecuteScalar().ToString();
                return ukupno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public string UkupnoPrimanjeVaskrs()
        {
            string ukupno = null;
            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand komanda = new OleDbCommand("SELECT COUNT (idDomacin) FROM tblDomacin WHERE primanjeVaskrs = 'Да'", con);

            try
            {
                con.Open();
                ukupno = komanda.ExecuteScalar().ToString();
                return ukupno;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                con.Close();
            }
        }

        public int SacuvajPutanjuSlike(Slika s)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Insert Into tblSlika (Path) Values (@Path)");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            kom.Parameters.AddWithValue("@Path", s.Path);

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

        public string VratiPoslednjuPutanjuSlike()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT TOP 1 Path FROM tblSlika ORDER BY SlikaId DESC ");

            OleDbConnection con = Konekcija.VratiKonekciju();
            OleDbCommand kom = new OleDbCommand(sb.ToString(), con);

            string pathSlike = null;

            try
            {
                con.Open();
                OleDbDataReader dr = kom.ExecuteReader();
                while (dr.Read())
                {
                    pathSlike = dr["Path"].ToString();
                }
                return pathSlike;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public bool PostojiDomacin(Domacin domacin)
        {
            foreach (Domacin d in VratiDomacine())
            {
                if (d.imeDomacina == domacin.imeDomacina && d.prezimeDomacina == domacin.prezimeDomacina)
                {
                    return true;
                }
            }
            return false;
        }
    }
}