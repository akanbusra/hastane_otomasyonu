using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace hastane_otomasyonu
{
    class SqlBağlantısı
    {
        public SqlConnection bağlanti()
        {
            SqlConnection bağlan = new SqlConnection("Data Source=DESKTOP-6QDVVSR\\SQLEXPRESS;Initial Catalog=HastaneProje;Integrated Security=True");
            bağlan.Open();
            return bağlan;
        }
    }
}
