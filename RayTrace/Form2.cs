using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTrace
{
    public partial class Form2 : Form
    {
        
        public static double random()
        {
            Random rdm = new Random(Guid.NewGuid().GetHashCode());
            return rdm.Next(0, 100000) / 100000.0;
        }
        public Form2()
        {
            InitializeComponent();
        }
        
        SColor Render(Ray ray, int depth)
        {
            ShadeRec sr = world.HitAll(ray);
            Ray scattered = new Ray();
            if (sr.IsHit)
            {
                SColor emited = sr.HitObjGloMat.emit(sr.U, sr.V, sr.HitPoint);
                SColor attenuation = new SColor(0, 0, 0);
                if (depth < 50 && sr.HitObjGloMat.scatter(ray, sr, out attenuation, out scattered))
                {
                    return emited + attenuation * Render(scattered, depth + 1);
                }
                else return emited;
            }
            else return new SColor(0, 0, 0);
        }
        
        World world = new World();
        private async Task Build()
        {   
            Vector3D from = new Vector3D(478, 278, -600), at = new Vector3D(278, 278, 0);
            int width = 800, height = 400;
            Camera camera = new Camera(from, at, new Vector3D(0, 1, 0), 45, 1.0 * width / height, 0.0, (from - at).Magnitude());
            Bitmap bitmap = new Bitmap(width, height);
            int sp = 500;
            timer1.Enabled = true;
            timer1.Start();
            await Task.Run(() =>
            {
                //world.PerlinSphere();
                //world.CornellBox();
                //world.BoxWorld();
                world.FinalFace();
                for (int i = 0; i < width; ++i)
                {
                    for (int j = 0; j < height; ++j)
                    {
                        SColor clr = new SColor(0, 0, 0);
                        for (int k = 0; k < sp; ++k)
                        {
                            Ray primaryRay = camera.getRay((i + random()) / width, (j + random()) / height);
                            clr += Render(primaryRay, 0);
                        }
                        clr *= 1.0 / sp;
                        clr = new SColor(Math.Sqrt(clr.R), Math.Sqrt(clr.G), Math.Sqrt(clr.B));
                        bitmap.SetPixel(i, j, clr.RGB255());
                    }
                }
            });

            timer1.Stop();
            pictureBox1.BackgroundImage = bitmap;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Build();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = (int.Parse(label2.Text) + 1).ToString();
        }
    }
}
 