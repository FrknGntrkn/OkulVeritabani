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
    public partial class FrmDersler : Form
    {
        public FrmDersler()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-Q87MGS4;Initial Catalog=Okul;Integrated Security=True");
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtDersid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        DataSet1TableAdapters.Tbl_DerslerTableAdapter ds = new DataSet1TableAdapters.Tbl_DerslerTableAdapter();
        

        private void FrmDersler_Load(object sender, EventArgs e)
        {
             dataGridView1.DataSource = ds.DersListesi();
        }
        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.DersListesi();
        }
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            string ders = Txtad.Text.ToLower();
            ds.DersEkle(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ders));
            MessageBox.Show("Ders Başarıyla Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.DersListesi();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            
            ds.DersSil(byte.Parse(TxtDersid.Text));
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            string ders = Txtad.Text.ToLower();
            ds.DersGuncelle(CultureInfo.CurrentCulture.TextInfo.ToTitleCase(ders),byte.Parse(TxtDersid.Text));
            MessageBox.Show("Ders Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = ds.DersListesi();
        }
    }
}
