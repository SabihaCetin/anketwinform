using DAL;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anket2
{
    public partial class SoruDuzenle : Form
    {
        public SoruDuzenle()
        {
            InitializeComponent();
        }
        public Soru GelenSoru { get; set; }
        AnketV2Context db = new AnketV2Context();
        private void SoruDuzenle_Load(object sender, EventArgs e)
        {
            txtSoruDuzenle.Text = GelenSoru.SoruCumlesi;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            AnketV2Context db = new AnketV2Context();
            var eskihal = db.Sorular.Find(GelenSoru.SoruID);
            eskihal.SoruCumlesi = txtSoruDuzenle.Text;
            db.Entry(eskihal).State =EntityState.Modified;
            db.SaveChanges();
            Form1 f = (Form1)Application.OpenForms["Form1"];
            f.SorulariYenile();
            f.CevapEkle();
            this.Close();
        }
    }
}
