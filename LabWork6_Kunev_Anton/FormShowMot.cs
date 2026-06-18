using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LabWork6_Kunev_Anton.ModelEF;

namespace LabWork6_Kunev_Anton
{
    public partial class FormShowMot : Form
    {
        public FormShowMot()
        {
            InitializeComponent();
        }

        public static Model1 DB = new Model1();

        private void FormShowMot_Load(object sender, EventArgs e)
        {
            Directory.CreateDirectory("Pictures");
            motorbikesBindingSource.DataSource = DB.Motorbikes.ToList();

            LoadCurrentPicture();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (DB.Motorbikes.ToList().Count == 0)
            {
                MessageBox.Show("Данные отсутствуют!");
                return;
            }

            Motorbikes CurrentMoto = DB.Motorbikes.Find((int)dataGridViewMot.CurrentRow.Cells[0].Value);
            DialogResult result = MessageBox.Show(
                $@"Вы действительно хотите удалить объект с ID - {CurrentMoto.ID}",
                "Сообщение",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    DB.Motorbikes.Remove(CurrentMoto);
                    DB.SaveChanges();
                    File.Delete($@"Pictures\{CurrentMoto.Picture}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    motorbikesBindingSource.DataSource = DB.Motorbikes.ToList();
                    LoadCurrentPicture();
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormAddUPDMD form = new FormAddUPDMD();
            this.Visible = false;
            form.Show();
        }

        private void dataGridViewMot_Click(object sender, EventArgs e)
        {
            LoadCurrentPicture();
        }

        private void LoadCurrentPicture()
        {
            if (DB.Motorbikes.ToList().Count == 0 || dataGridViewMot.CurrentRow == null)
            {
                pictureBox1.Image = null;
                return;
            }

            int ID = (int)dataGridViewMot.CurrentRow.Cells[0].Value;
            string picturePath = $@"Pictures\{DB.Motorbikes.Find(ID).Picture}";
            pictureBox1.Image = File.Exists(picturePath) ? Image.FromFile(picturePath) : null;
        }
    }
}


