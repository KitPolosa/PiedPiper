using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace IT_KR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void buttonC1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.BMP;*.JPG;*GIF;*.PNG)|*.BMP;*.JPG;*GIF;*.PNG|All files (*.*)|*.*";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    label4.Text = ofd.FileName;
                    Image img = Image.FromFile(label4.Text);
                    pictureBox3.Image = img;
                    pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                    System.IO.FileInfo file = new System.IO.FileInfo(label4.Text);
                    label6.Text = file.Length.ToString() + " Байт";
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Создается объект ImageCodecInfo для информации о кодеке
        ImageCodecInfo jpegCodec = null;
        EncoderParameters codecParameter = new EncoderParameters(1);
        public void CompressImage(Image sourceImage, int imageQuality, string savePath)
        {
            try
            {
                //Устанавливаем коэффициент качества для сжатия
                EncoderParameter imageQualitysParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, imageQuality);

                //Список всех доступных кодеков (для всей системы)
                ImageCodecInfo[] allCodecs = ImageCodecInfo.GetImageEncoders();

                codecParameter.Param[0] = imageQualitysParameter;

                //Найдем и выберем кодек JPEG
                for (int i = 0; i < allCodecs.Length; i++)
                {
                    if (allCodecs[i].MimeType == "image/jpeg")
                    {
                        jpegCodec = allCodecs[i];
                        break;
                    }
                }

                //Сохраняем сжатое изображение
                sourceImage.Save(savePath, jpegCodec, codecParameter);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        private void buttonC3_Click(object sender, EventArgs e)
        {
            if (label4.Text == "")
            {
                MessageBox.Show("Пожалуйста, сначала загрузите изображение");
            }
            else if (label4.Text.Contains(".jpg"))
            {
                CompressImage(Image.FromFile(label4.Text), trackBar1.Value, label4.Text.Insert(label4.Text.Length - 4, " JPEG Compressed Image"));
                MessageBox.Show("Изображение сжато");
            }
            else
            {
                string x = label4.Text.Insert(label4.Text.Length - 4, " JPEG Compressed Image");
                string path = label4.Text.Insert(label4.Text.Length - 4, " JPEG Compressed Image").Substring(0, x.Length - 4) + ".jpg";

                CompressImage(Image.FromFile(label4.Text), trackBar1.Value, path);
                MessageBox.Show("Изображение сжато");
                Image img = Image.FromFile(path);
                pictureBox3.Image = img;
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
                System.IO.FileInfo file = new System.IO.FileInfo(path);
                label6.Text = file.Length.ToString() + " Байт";
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar1.Value.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar1.Minimum = 0;
            trackBar1.Maximum = 100;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}