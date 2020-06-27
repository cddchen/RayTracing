using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTrace
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //initial
        World world = new World();
        Light pointLight = new Light(new Point3D(3, 2, 2), new SColor(1, 1, 1));
        Light ambientLight = new Light(new Point3D(0, 0, 0), new SColor(0.3, 0.3, 0.3));

        //Camera camera = new Camera();
        Bitmap bmp;
        double beta = 0;
        private void button1_ClickAsync(object sender, EventArgs e)
        {
        }
        private async Task render()
        {
            /*
            bmp = new Bitmap(camera.HRes, camera.VRes);
            await Task.Run(() =>
            {
                for (int i = 0; i < camera.HRes; ++i)
                {
                    for (int j = 0; j < camera.VRes; ++j)
                    {
                        Ray Eye2Point_ray = camera.getRay(i, j);
                        ShadeRec rec = world.HitAll(Eye2Point_ray);

                        if (rec.IsHit)
                        {
                            Vector3D rayLight = pointLight.Position - rec.HitPoint;
                            rayLight.Normalize();

                            Ray shadowRay = new Ray(rec.HitPoint, rayLight);
                            if (world.ShadowHitAll(shadowRay))
                                bmp.SetPixel(i, j, Color.Black);
                            else
                            {
                                /*
                                if (rec.HitObject.IsTexture)
                                {
                                    //rotate
                                    double _beta = Math.PI * beta / 180.0;

                                    Color textureColor = rec.HitObject.Txture.getColor(rec.Normal.Rotate(_beta));
                                    Color lightColor = pointLight.LightColor.RGB255();

                                    Color blendColor = Color.FromArgb(
                                        (int)(textureColor.R * 0.8 + lightColor.R * 0.2),
                                        (int)(textureColor.G * 0.8 + lightColor.G * 0.2),
                                        (int)(textureColor.B * 0.8 + lightColor.B * 0.2));
                                    bmp.SetPixel(i, j, blendColor);
                                }
                                else
                                {
                                    //反射角
                                    Vector3D R = 2.0 * (rec.Normal * rayLight) * rec.Normal - rayLight;
                                    R.Normalize();
                                    Vector3D V = camera.Eye - rec.HitPoint;
                                    V.Normalize();
                                    //排除钝角
                                    double LN = rayLight * rec.Normal;
                                    if (LN < 0.0) LN = 0.0;
                                    else if (LN > 1.0) LN = 1.0;

                                    double VR = V * R;
                                    if (VR < 0.0) VR = 0.0;
                                    else if (VR > 1.0) VR = 1.0;
                                    //环境光+漫反射+镜面反射
                                    Material mat = rec.HitObject;
                                    SColor color = pointLight.LightColor * mat.Ka
                                        + mat.MatColor * mat.Kd * LN
                                        + mat.MatColor * mat.Ks * Math.Pow(VR, rec.HitObject.Ns);

                                    bmp.SetPixel(i, j, color.RGB255());
                                }
                            }
                        }
                        else
                        {
                            bmp.SetPixel(i, j, Color.LightGray);
                        }
                    }
                }
            });
            pictureBox1.BackgroundImage = bmp;
            */
        }
        private void button3_Click(object sender, EventArgs e)
        {
            world.Clear();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            beta = trackBar1.Value;
            render();
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            /*double degree = Math.PI * trackBar2.Value / 360;
            camera.Left(degree);
            render();
            */
        }
    }
}
