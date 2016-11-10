using System;
using System.Data.OleDb;

namespace PCentar
{
    internal class Konekcija
    {
        public static OleDbConnection VratiKonekciju()
        {
            string bazaIme = "PCentarDB";
            string path = AppDomain.CurrentDomain.BaseDirectory + bazaIme;

            OleDbConnectionStringBuilder sb = new OleDbConnectionStringBuilder();
            sb.Provider = "Microsoft.ACE.OLEDB.12.0";
            sb["Jet OLEDB:Database Password"] = "drmfslsd";
            sb.DataSource = path.ToString();
            sb.PersistSecurityInfo = true;

            OleDbConnection con = new OleDbConnection(sb.ToString());
            return con;
        }
    }
}