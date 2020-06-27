using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTrace
{
    class Cmp_x : IComparer<GeometryObject>
    {
        public int Compare(GeometryObject a, GeometryObject b)
        {
            aabb box_left = null, box_right = null;
            if (!a.bounding_box(0, 0, out box_left) || !b.bounding_box(0, 0, out box_right))
            {
                Console.WriteLine("no bounding box!");
            }
            if (box_left.Min.X - box_right.Min.X < 0) return -1;
            else return 1;
        }
    }
    class Cmp_y : IComparer<GeometryObject>
    {
        public int Compare(GeometryObject a, GeometryObject b)
        {
            aabb box_left = null, box_right = null;
            if (!a.bounding_box(0, 0, out box_left) || !b.bounding_box(0, 0, out box_right))
            {
                Console.WriteLine("no bounding box!");
            }
            if (box_left.Min.Y - box_right.Min.Y < 0) return -1;
            else return 1;
        }
    }
    class Cmp_z : IComparer<GeometryObject>
    {
        public int Compare(GeometryObject a, GeometryObject b)
        {
            aabb box_left = null, box_right = null;
            if (!a.bounding_box(0, 0, out box_left) || !b.bounding_box(0, 0, out box_right))
            {
                Console.WriteLine("no bounding box!");
            }
            if (box_left.Min.Z - box_right.Min.Z < 0) return -1;
            else return 1;
        }
    }
    class bvh_node : GeometryObject
    {
        private aabb _box;
        private GeometryObject left, right;
        public bvh_node() { }
        public bvh_node(List<GeometryObject> l)
        {
            int axis = (int)(3 * Form2.random());
            if (axis == 0) l.Sort(new Cmp_x().Compare);
            else if (axis == 1) l.Sort(new Cmp_y().Compare);
            else l.Sort(new Cmp_z().Compare);

            int n = l.Count();
            if (n == 1)
            {
                left = right = l[0];
            }
            else if (n == 2)
            {
                left = l[0];
                right = l[1];
            }
            else
            {
                List<GeometryObject> left_l = new List<GeometryObject>(), right_l = new List<GeometryObject>();
                for (int i = 0; i < n / 2; ++i) left_l.Add(l[i]);
                for (int i = n / 2; i < n; ++i) right_l.Add(l[i]);
                left = new bvh_node(left_l);
                right = new bvh_node(right_l);
            }

            aabb box_left = null, box_right = null;
            if (!left.bounding_box(0, 1, out box_left) || !right.bounding_box(0, 1, out box_right))
            {
                Console.WriteLine("no bounding box!");
            }
            _box = aabb.surrounding_box(box_left, box_right);
        }
        public override bool bounding_box(double t0, double t1, out aabb box)
        {
            box = _box;
            return true;
        }

        public override bool Hit(Ray ray, double tMin, double tMax, out ShadeRec shadeRec)
        {
            shadeRec = new ShadeRec(); shadeRec.IsHit = false;
            if (_box.hit(ray))
            {
                ShadeRec left_rec, right_rec;
                bool hit_left = left.Hit(ray, tMin, tMax, out left_rec);
                bool hit_right = right.Hit(ray, tMin, tMax, out right_rec);
                if (hit_left && hit_right)
                {
                    if (left_rec.HitT < right_rec.HitT)
                        shadeRec = left_rec;
                    else shadeRec = right_rec;
                    return true;
                }
                else if (hit_left)
                {
                    shadeRec = left_rec;
                    return true;
                }
                else if (hit_right)
                {
                    shadeRec = right_rec;
                    return true;
                }
                else return false;
            }
            return false;
        }

        public override bool ShadowHit(Ray ray)
        {
            return false;
        }
    }
}
