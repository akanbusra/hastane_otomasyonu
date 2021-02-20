using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hastane_otomasyonu
{
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl =new sqlbaglantisi();
        //datagride branşları getirme
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource= dt;
            bgl.baglanti().Close();
        }

        //üstüne tıklanan veriyi forda doldurma
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtbranşid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtbranşad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();

        }

        //branş ekleme
        private void btnekle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_branslar (bransad) values (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtbranşad.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("branş kaydı eklendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //branş silme
        private void btnsil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_branslar where bransid=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtbranşid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("kayıt silindi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //branş güncelleme
        private void btngüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("update tbl_branslar set bransad=@p1 where bransid=@p2", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", txtbranşad.Text);
            komut1.Parameters.AddWithValue("@p2", txtbranşid.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("kayıt güncellendi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
