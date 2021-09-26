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
using System.Globalization;


namespace OkulProje
{
    public partial class FrmSinavNotlar : Form
    {
        public FrmSinavNotlar()
        {
            InitializeComponent();
        }
        DataSet1TableAdapters.Tbl_NotlarTableAdapter ds = new DataSet1TableAdapters.Tbl_NotlarTableAdapter();
       
        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.NotListesi(int.Parse(txtara.Text));
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-Q87MGS4;Initial Catalog=Okul;Integrated Security=True");
        
        private void FrmSinavNotlar_Load(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Dersler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cmbders.DisplayMember = "DersAd";
            Cmbders.ValueMember = "Dersid";
            Cmbders.DataSource = dt;
            baglanti.Close();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            Txts1.Text= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            Txts2.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            Txts3.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txtProje.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            txtDurum.Text = dataGridView1.Rows[secilen].Cells[8].Value.ToString();
            txtOrtalama.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();

            

        }

        int sinav1, sinav2, sinav3, proje;
        double ortalama;
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
            
           
            sinav1 = Convert.ToInt16(Txts1.Text);
            sinav2 = Convert.ToInt16(Txts2.Text);
            sinav3 = Convert.ToInt16(Txts3.Text);
            proje = Convert.ToInt16(txtProje.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            txtOrtalama.Text = ortalama.ToString();
            if (ortalama >= 50)
            {
                txtDurum.Text = "True";
            }
            else
            {
                txtDurum.Text = "False";
            } 

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(Cmbders.SelectedValue.ToString()), int.Parse(Txtid.Text), byte.Parse(Txts1.Text), byte.Parse(Txts2.Text), byte.Parse(Txts3.Text), byte.Parse(txtProje.Text), decimal.Parse(txtOrtalama.Text), bool.Parse(txtDurum.Text), byte.Parse(Txtid.Text));
        }
    }
}
