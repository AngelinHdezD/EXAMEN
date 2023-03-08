using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace EXAMEN
{
    public partial class Form1 : Form
    {
        Graphics lienzo;
        Graphics g;
        Graphics papel;
        int x = 0;
        int y = 0;
        int R = 0;
        int G = 0;
        int B = 0;
        int tamanioPincel = 3;
        bool moviendo = false;
        Pen pen;
        bool pintar = false;
        bool borrar = false;
        Bitmap bm;
        Point py, px;
       
        public Form1()
        {
            InitializeComponent();
           

            txtPapel.Image = new Bitmap(txtPapel.Height, txtPapel.Width);
            papel = txtPapel.CreateGraphics();
            papel.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.Width = 900;
            this.Height = 700;
            bm = new Bitmap(txtPapel.Width, txtPapel.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            txtPapel.Image = bm;



            tamanioPincel = trackBar1.Value;
            pen = new Pen(Color.FromArgb(R, G, B), tamanioPincel);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;


        }
    

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();

        }
      

        private void txtPapel_MouseUp(object sender, MouseEventArgs e)
        {
            moviendo = false;
        }

        private void txtPapel_MouseDown(object sender, MouseEventArgs e)
        {
            moviendo = true;

            x = e.X;
            y = e.Y;
            txtPapel.Cursor = Cursors.Cross;
         
        }

        private void btnPintar_Click(object sender, EventArgs e)
        {
            pintar = true;
            borrar = false;
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            pintar = false;
            borrar = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            pen = new Pen(Color.Black);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                pen = new Pen(colorDialog1.Color);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog guardar = new SaveFileDialog();
            guardar.Filter = "Image(*.jpg)|*.jpg|(*.*|*.*";

            cambiarPincel(R,G,B);
            if (guardar.ShowDialog() == DialogResult.OK)
            {

                Bitmap btm = bm.Clone(new Rectangle(0, 0, txtPapel.Width, txtPapel.Height), bm.PixelFormat);

                btm.Save(guardar.FileName, ImageFormat.Png);


            }
            txtPapel.Refresh();


        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                txtPapel.ImageLocation = abrir.FileName;
                txtPapel.SizeMode = PictureBoxSizeMode.StretchImage;
                this.txtPapel.Image = new Bitmap(abrir.FileName);
               

            }
            txtPapel.Refresh();

        }

        private void txtPapel_Paint(object sender, PaintEventArgs e)
        {

          

        }

        private void txtPapel_MouseMove(object sender, MouseEventArgs e)
        {
            
                if (moviendo && pintar)
            {
                cambiarPincel2();
                papel.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
                px = e.Location;
                g.DrawLine(pen, px, py);
                py = px;
               
            }
            if (moviendo && borrar)
            {
                cambiarPincel(255, 255, 255);
                papel.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;

            }

           
        }
        private void cambiarPincel(int R, int G, int B)
        {
            pen = new Pen(Color.FromArgb(R, G, B), trackBar1.Value);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }

        private void cambiarPincel2()
        {
            pen = new Pen(colorDialog1.Color,trackBar1.Value);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
        }
    } }