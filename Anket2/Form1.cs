using DAL;
using DomainEntity.Models;
using DomainEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anket2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        AnketV2Context db = new AnketV2Context();
        private void Form1_Load(object sender, EventArgs e)
        {
            CevapEkle();
            SorulariYenile();
        }
        public void SorulariYenile()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (Soru item in db.Sorular)
            {
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Text = item.SoruCumlesi;
                flowLayoutPanel1.Controls.Add(lbl);
                flowLayoutPanel1.SetFlowBreak(lbl, true);
                flowLayoutPanel1.AutoScroll = true;
                ComboBox cmb = new ComboBox();
                cmb.Name = "Soru_" + item.SoruID;
                cmb.Items.Add("Evet");
                cmb.Items.Add("Hayır");
                flowLayoutPanel1.Controls.Add(cmb);
                flowLayoutPanel1.SetFlowBreak(cmb, true);
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = db.Sorular.ToList();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            Soru s = new Soru();
            s.SoruCumlesi = txtSoruCumlesi.Text;
            db.Sorular.Add(s);
            db.SaveChanges();
            SorulariYenile();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if (item is ComboBox)
                {
                    string soruID = item.Name.Replace("Soru_", "");
                    int SID = Convert.ToInt32(soruID);
                    Cevap c = new Cevap();
                    c.SoruID = SID;

                    int y = (((ComboBox)item).SelectedIndex + 1) % 2;
                    c.Yanit = (Yanit)y;
                    Kisi k = db.Kisiler.Where(x => x.AdSoyad == txtAdSoyad.Text).FirstOrDefault();
                    if (k != null)
                    {
                        c.KisiID = k.KisiID;
                    }
                    else
                    {
                        k = new Kisi();
                        k.AdSoyad = txtAdSoyad.Text;
                        db.Kisiler.Add(k);
                        db.SaveChanges();
                        c.KisiID = k.KisiID;
                    }
                    db.Cevaplar.Add(c);
                    db.SaveChanges();
                    CevapEkle();
                }
            }
        }
        public void CevapEkle()
        {
            dataGridView2.DataSource = null;
            dataGridView2.DataSource = db.Cevaplar.Select(x => new CevapViewModel()
            {
                AdSoyad = x.CevabıVerenKisi.AdSoyad,
                Soru = x.Sorusu.SoruCumlesi,
                CevapID = x.CevapID,
                Cevap = x.Yanit.ToString()
            }).ToList();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("soru seciniz");
            else
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    int soruID = (int)item.Cells[0].Value;
                    Soru silinecek = db.Sorular.Find(soruID);
                    db.Sorular.Remove(silinecek);
                }
                db.SaveChanges();
                SorulariYenile();
                CevapEkle();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("birşey seçmelisiniz");
            }
            else
            {
                SoruDuzenle s = new SoruDuzenle();
                int ID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                Soru duzenlenecek = db.Sorular.Find(ID);
                s.GelenSoru = duzenlenecek;
                s.Show();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
                MessageBox.Show("cevap seciniz");
            else
            {
                foreach (DataGridViewRow item in dataGridView2.SelectedRows)
                {
                    int ID = (int)item.Cells[0].Value;
                    Cevap silinecek = db.Cevaplar.Find(ID);
                    db.Cevaplar.Remove(silinecek);
                }
                db.SaveChanges();
                CevapEkle();
            }
        }
    }
}
