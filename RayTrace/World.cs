using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    public class World
    {
        private List<GeometryObject> geometrys = new List<GeometryObject>();
        public ShadeRec HitAll(Ray ray)
        {
            ShadeRec srNearest = new ShadeRec(); srNearest.IsHit = false;
            foreach (GeometryObject geometry in geometrys)
            {
                ShadeRec tmp;
                geometry.Hit(ray, 1e-3, 1e15, out tmp);
                if (tmp.IsHit)
                {
                    if (!srNearest.IsHit) srNearest = tmp;
                    else if (tmp.HitT < srNearest.HitT) srNearest = tmp;
                }
            }
            return srNearest;
        }
        public bool ShadowHitAll(Ray ray)
        {
            foreach (GeometryObject geometryObject in geometrys)
            {
                if (geometryObject.ShadowHit(ray))
                    return true;
            }
            return false;
        }
        public void AddGeoObj(GeometryObject geometryObject)
        {
            geometrys.Add(geometryObject);
        }

        public void PerlinSphere()
        {
            Texture prlText = new NoiseTexture(3.0);
            List<GeometryObject> list = new List<GeometryObject>();
            Sphere a = new Sphere(new Point3D(0, -1000, 0), 1000);
            a.GeoMaterial = new Lambert(prlText);
            list.Add(a);
            Sphere b = new Sphere(new Point3D(0, 2, 0), 2);
            b.GeoMaterial = new Lambert(prlText);
            //list.Add(b);

            Sphere c = new Sphere(new Point3D(0, 2, 0), 2);
            Bitmap bitmap = new Bitmap(@"C:\Users\cdd13\Desktop\学习\桌面程序开发\光线追踪素材\材质\EarthHighRes.jpg");
            ImageTexture imageTexture = new ImageTexture(new Bitmap(@"C:\Users\cdd13\Desktop\学习\桌面程序开发\光线追踪素材\材质\EarthHighRes.jpg"));
            c.GeoMaterial = new Lambert(imageTexture);
            list.Add(c);

            geometrys.Add(new GlobalGeometryList(list));
        }

        public void CornellBox()
        {
            List<GeometryObject> list = new List<GeometryObject>();
            GeoMaterial red = new Lambert(new ConstantTexture(new SColor(0.65, 0.05, 0.05)));
            GeoMaterial white = new Lambert(new ConstantTexture(new SColor(0.73, 0.73, 0.73)));
            GeoMaterial green = new Lambert(new ConstantTexture(new SColor(0.12, 0.45, 0.15)));
            GeoMaterial light = new DiffuseLight(new ConstantTexture(new SColor(15, 15, 15)));

            FilpNormals filpNormals = new FilpNormals(new YZRect(0, 555, 0, 555, 555, green));
            list.Add(filpNormals);
            YZRect yzRect = new YZRect(0, 555, 0, 555, 0, red);
            list.Add(yzRect);
            XZRect xzRect = new XZRect(213, 343, 227, 332, 554, light);
            list.Add(xzRect);
            FilpNormals filpNormals1 = new FilpNormals(new XZRect(0, 555, 0, 555, 555, white));
            list.Add(filpNormals1);
            XZRect xzRect1 = new XZRect(0, 555, 0, 555, 0, white);
            list.Add(xzRect1);
            FilpNormals filpNormals2 = new FilpNormals(new XYRect(0, 555, 0, 555, 555, white));
            list.Add(filpNormals2);

            AddGeoObj(new bvh_node(list));
        }
        public void BoxWorld()
        {
            this.geometrys.Clear();
            List<GeometryObject> list = new List<GeometryObject>();
            list.Add(new FilpNormals(new YZRect(0, 555, 0, 555, 555, new Lambert(new ConstantTexture(new SColor(0.12, 0.45, 0.15))))));
            list.Add(new YZRect(0, 555, 0, 555, 0, new Lambert(new ConstantTexture(new SColor(0.65, 0.05, 0.05)))));
            list.Add(new XZRect(213, 343, 227, 332, 554, new DiffuseLight(new ConstantTexture(new SColor(15, 15, 15)))));
            list.Add(new FilpNormals(new XZRect(0, 555, 0, 555, 555, new Lambert(new ConstantTexture(new SColor(0.73, 0.73, 0.73))))));
            list.Add(new XZRect(0, 555, 0, 555, 0, new Lambert(new ConstantTexture(new SColor(0.73, 0.73, 0.73)))));
            list.Add(new FilpNormals(new XYRect(0, 555, 0, 555, 555, new Lambert(new ConstantTexture(new SColor(0.73, 0.73, 0.73))))));
            GeometryObject a = new Translate(new Box(new Point3D(0, 0, 0), new Point3D(165, 165, 165), new Lambert(new ConstantTexture(new SColor(0.73, 0.73, 0.73)))), new Vector3D(130, 0, 65));
            GeometryObject b = new Translate(new RotateY(new Box(new Point3D(0, 0, 0), new Point3D(165, 330, 165), new Lambert(new ConstantTexture(new SColor(0.73, 0.73, 0.73)))), 15), new Vector3D(265, 0, 295));
            //list.Add(a); list.Add(b);
            list.Add(new ConstantMedium(a, 1e-2, new ConstantTexture(new SColor(1, 1, 1))));
            list.Add(new ConstantMedium(b, 1e-2, new ConstantTexture(new SColor(0, 0, 0))));
            AddGeoObj(new GlobalGeometryList(list));
        }
        public void LightWorld()
        {
            Texture perlinTex = new NoiseTexture(6.3);
            GeoMaterial redlight = new DiffuseLight(new ConstantTexture(new SColor(0.98, 0.1, 0.08)));
            GeoMaterial bluelight = new DiffuseLight(new ConstantTexture(new SColor(0.05, 0.05, 1)));

            List<GeometryObject> list = new List<GeometryObject>();
            list.Add(new Sphere(new Point3D(-2, 3, -3), 1.5, redlight));
            list.Add(new Sphere(new Point3D(-2.2, 3.2, 2.8), 1.5, bluelight));
            list.Add(new Sphere(new Point3D(0, 0, 2.2), 1, new Metal(new ConstantTexture(new SColor(1, 1, 1)))));
            list.Add(new Sphere(new Point3D(0, 0, 0), 1, new Lambert(new ConstantTexture(new SColor(1, 1, 1)))));
            list.Add(new Sphere(new Point3D(0, 0, -2), 1, new Dielectric(1.5)));
            list.Add(new Sphere(new Point3D(0, 0, -2), -0.18, new Dielectric(1.5)));
            list.Add(new Sphere(new Point3D(0, -1000, 0), 999, new Dielectric(1.5)));
            AddGeoObj(new bvh_node(list));
        }
        public void FinalFace()
        {
            List<GeometryObject> list = new List<GeometryObject>();
            List<GeometryObject> boxList = new List<GeometryObject>();
            GeoMaterial white = new Lambert(new ConstantTexture(new SColor(0.73, 0.73, 0.73)));
            GeoMaterial ground = new Lambert(new ConstantTexture(new SColor(0.48, 0.83, 0.52)));
            for (int i = 0; i < 20; ++i)
            {
                for (int j = 0; j < 20; ++j)
                {
                    double w = 100.0;
                    double x0 = -1000.0 + i * w;
                    double z0 = -1000.0 + j * w;
                    double y0 = 0.0;
                    double x1 = x0 + w;
                    double y1 = 100.0 * (Form2.random() + 0.01);
                    double z1 = z0 + w;
                    boxList.Add(new Box(new Point3D(x0, y0, z0), new Point3D(x1, y1, z1), ground));
                }
            }

            list.Add(new bvh_node(boxList));
            GeoMaterial light = new DiffuseLight(new ConstantTexture(new SColor(7, 7, 7)));
            list.Add(new XZRect(123, 423, 147, 412, 554, light));
            Point3D center = new Point3D(400, 400, 200);
            list.Add(new Sphere(center, 50, new Lambert(new ConstantTexture(new SColor(0.7, 0.3, 0.1)))));
            list.Add(new Sphere(new Point3D(260, 150, 45), 50, new Dielectric(1.5)));
            list.Add(new Sphere(new Point3D(0, 150, 145), 50, new Metal(new ConstantTexture(new SColor(0.8, 0.8, 0.9)), 10)));

            GeometryObject boundary = new Sphere(new Point3D(360, 150, 145), 70, new Dielectric(1.5));
            list.Add(boundary);
            list.Add(new ConstantMedium(boundary, 0.2, new ConstantTexture(new SColor(0.2, 0.4, 0.9))));
            GeometryObject boundary1 = new Sphere(new Point3D(0, 0, 0), 5000, new Dielectric(1.5));
            list.Add(new ConstantMedium(boundary1, 1e-4, new ConstantTexture(new SColor(1, 1, 1))));

            list.Add(new Sphere(new Point3D(400, 200, 400), 100, new Lambert(new ImageTexture(new Bitmap(@"C:\Users\cdd13\Desktop\学习\桌面程序开发\光线追踪素材\材质\EarthHighRes.jpg")))));
            list.Add(new Sphere(new Point3D(220, 280, 300), 80, new Lambert(new NoiseTexture(0.1))));
            List<GeometryObject> boxList2 = new List<GeometryObject>();
            for (int i = 0; i < 1000; ++i)
            {
                boxList2.Add(new Sphere(new Point3D(165.0 * Form2.random(), 165.0 * Form2.random(), 165.0 * Form2.random()), 10, white));
            }
            list.Add(new Translate(new GlobalGeometryList(boxList2), new Vector3D(-100, 270, 395)));
            AddGeoObj(new GlobalGeometryList(list));
        }
        public void Build_World()
        {
            List<GeometryObject> list = new List<GeometryObject>();
            Sphere sphere = new Sphere(new Point3D(0, -1000, 0), 1000);
            CheckerTexture texture = new CheckerTexture(new ConstantTexture(new SColor(0.2, 0.3, 0.1)), 
                new ConstantTexture(new SColor(0.9, 0.9, 0.9)));
            NoiseTexture noiseTexture = new NoiseTexture();
            //sphere.GeoMaterial = new Lambert(noiseTexture);
            sphere.GeoMaterial = new Lambert(texture);
            list.Add(sphere);
            /*
            for (int a0 = -11; a0 < 11; ++a0)
            {
                for (int b0 = -11; b0 < 11; ++b0)
                {
                    double chose_mat = Form2.random();
                    Point3D center = new Point3D(a0 + 0.9 * Form2.random(), 0.2, b0 + 0.9 * Form2.random());
                    if ((center - new Point3D(4, 0.2, 0)).Magnitude() > 0.9)
                    {
                        if (chose_mat < 0.8)
                        {
                            Sphere tmp = new Sphere(center, 0.2);
                            tmp.GeoMaterial = new Lambert(
                                new ConstantTexture(new SColor(Form2.random() * Form2.random(), Form2.random() * Form2.random(), Form2.random() * Form2.random())));
                            list.Add(tmp);
                        }
                        else if (chose_mat < 0.95)
                        {
                            Sphere tmp = new Sphere(center, 0.2);
                            tmp.GeoMaterial = new Metal(
                                new ConstantTexture(new SColor(0.5 * (1 + Form2.random()), 0.5 * (1 + Form2.random()), 0.5 * (1 + Form2.random()))), 0.5 + Form2.random());
                            list.Add(tmp);
                        }
                        else
                        {
                            Sphere tmp = new Sphere(center, 0.2);
                            tmp.GeoMaterial = new Dielectric(1.5);
                            list.Add(tmp);
                        }
                    }
                }
            }
            */
            Sphere a = new Sphere(new Point3D(0, 1, 0), 1);
            a.GeoMaterial = new Dielectric(1.5);
            list.Add(a);
            Sphere b = new Sphere(new Point3D(-4, 1, 0), 1);
            b.GeoMaterial = new Lambert(
                new ConstantTexture(new SColor(0.4, 0.2, 0.1)));
            list.Add(b);
            Sphere c = new Sphere(new Point3D(4, 1, 0), 1);
            c.GeoMaterial = new Metal(
                new ConstantTexture(new SColor(0.7, 0.6, 0.5)), 0);
            list.Add(c);
            AddGeoObj(new bvh_node(list));
        }
        public void Clear()
        {
            this.geometrys.Clear();
        }
    }
}
