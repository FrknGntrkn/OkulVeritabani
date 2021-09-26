using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;

namespace OkulProje
{
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-Q87MGS4;Initial Catalog=Okul;Integrated Security=True");

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.Ogrencilistesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Kulupler", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Cmbkulup.DisplayMember = "Kulupad";
            Cmbkulup.ValueMember = "Kulupid";
            Cmbkulup.DataSource = dt;
            baglanti.Close();

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            string Ad = Txtad.Text.ToLower();
            string Soyad = Txtsoyad.Text.ToLower();

            string c="";
            if (radioButton1.Checked == true)
            {
                c = "Kız";
            }
            if(radioButton2.Checked== true)
            {
                c = "Erkek";
            }
            

            ds.OgrenciEkle(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Ad), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Soyad), byte.Parse(Cmbkulup.SelectedValue.ToString()),c);
            MessageBox.Show("Ekleme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.Ogrencilistesi();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.Ogrencilistesi();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string c = "";
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            Txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            Cmbkulup.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            c = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            if (c == "Erkek")
            {
                radioButton2.Checked = true;
            }
            if (c == "Kız")
            {
                radioButton1.Checked = true;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(Txtid.Text));
            MessageBox.Show("Silme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.Ogrencilistesi();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            string Ad = Txtad.Text.ToLower();
            string Soyad = Txtsoyad.Text.ToLower();

            string c = "";
            if (radioButton1.Checked == true)
            {
                c = "Kız";
            }
            if (radioButton2.Checked == true)
            {
                c = "Erkek";
            }
            ds.OgrenciGuncelle(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Ad), CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Soyad), byte.Parse(Cmbkulup.SelectedValue.ToString()), c,Convert.ToInt16(Txtid.Text));
           
            MessageBox.Show("Güncelleme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.Ogrencilistesi();

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
           dataGridView1.DataSource= ds.OgrenciGetir(txtara.Text);
        }
    }
}
