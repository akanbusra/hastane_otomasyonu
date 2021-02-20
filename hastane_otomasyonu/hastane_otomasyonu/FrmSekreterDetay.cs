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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        public string tcno;
        
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text=tcno;

            //ad soyad çekme
            SqlCommand komut = new SqlCommand("select sekreteradsoyad from tbl_sekreter where sekretertc=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",tcno);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();


            //branşları datagride çekme
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_branslar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            bgl.baglanti().Close();

            //doktorları datagride çekme
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("select (doktorad + ' '+doktorsoyad) as 'Doktolar',doktortc,doktorbrans from tbl_doktorlar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
            bgl.baglanti().Close();

            //branşalrı comboboxa çekme
            SqlCommand komut1 = new SqlCommand("select bransad from tbl_branslar",bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                cmbbranş.Items.Add(dr1[0]);
            }
            bgl.baglanti().Close();
        }

        private void randevuKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("insert into tbl_randevular (randevutarih,randevusaat,randevubrans,randevudoktor) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",msktarih.Text);
            komut1.Parameters.AddWithValue("@p2",msksaat.Text);
            komut1.Parameters.AddWithValue("@p3",cmbbranş.Text);
            komut1.Parameters.AddWithValue("@p4",cmbdoktor.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("randevu oluşturuldu");
        }

        private void cmbbranş_SelectedIndexChanged(object sender, EventArgs e)
        {
            //doktorları comboboxa çekme
            cmbdoktor.Items.Clear();

            SqlCommand komut2 = new SqlCommand("select doktorad,doktorsoyad from tbl_doktorlar where doktorbrans=@b1", bgl.baglanti());
            komut2.Parameters.AddWithValue("@b1", cmbbranş.Text);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                cmbdoktor.Items.Add(dr2[0] + " " + dr2[1]);
            }
            bgl.baglanti().Close();
        }

        private void duyuruOluştur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into tbl_duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",rchduyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");

        }

        //doktor panelini açma
        private void btndoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frdoktor = new FrmDoktorPaneli();
            frdoktor.Show();
        }

        private void btnBranşPaneli_Click(object sender, EventArgs e)
        {
            FrmBrans frb = new FrmBrans();
            frb.Show();
        }

        private void btnrandevuListesi_Click(object sender, EventArgs e)
        {
            FrmRadevuListesi frl = new FrmRadevuListesi();
            frl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frd = new FrmDuyurular();
            frd.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmGirisler frg = new FrmGirisler();
            frg.Show();
            this.Hide();
        }
    }
}
