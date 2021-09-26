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

namespace OkulProje
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-Q87MGS4;Initial Catalog=Okul;Integrated Security=True");
        public string Numara;
        //public string Ogrenciisim;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select DersAd,Sinav1,Sinav2,Sinav3,proje,Ortalama,Durum from Tbl_Notlar inner join Tbl_Dersler on Tbl_Notlar.Dersid = Tbl_Dersler.Dersid where Ogrenciid = @p1",baglanti);
            komut.Parameters.AddWithValue("@p1", Numara);
            // this.Text = Numara.ToString();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            //öğrenci ismini çekme
           // baglanti.Open();
            //SqlCommand komut2 = new SqlCommand("Select OgrenciAd from Tbl_Ogrenciler where ogrenciid=@o1",baglanti);
            //komut2.Parameters.AddWithValue("o1", Numara);
            //SqlDataReader dr = komut2.ExecuteReader();
            //while (dr.Read())
            //{
              //  this.Text = dr[1].ToString();
           // }
            //baglanti.Close();
        }
    }
}
