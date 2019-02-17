using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//vector3D import biar crossproduct e gampang
using System.Windows.Media.Media3D;

/*
    Ivan Marcellino   6609
    Johan Poerwanto   6613
    Katherine Limanu  6621
    Michael Tenoyo    6635
*/
namespace triangle {
    public partial class Form1 : Form {

        public class Triangle{
            //dapet dari mouseclick
            public List<Point> titik { get; private set; }

            // ax+by+c=0 
            public List<Color> warna { get; private set; }
            public List<int> A { get; set; }
            public List<double> C { get; set; }
            public List<int> B { get; set; }

            //Red, Green, Blue : Vector ABC dipake di
            // Ax+By+Cz+D = 0
            //z : Dred, Dgreen, Dblue
            public int Dred { get; set; }
            public int Dblue { get; set; }
            public int Dgreen { get; set; }
            public Vector3D Red { get; set; }
            public Vector3D Green { get; set; }
            public Vector3D Blue { get; set; }
            public Triangle() {
                titik = new List<Point>(3);
                warna = new List<Color>(3);
                A = new List<int>();
                B = new List<int>();
                C = new List<double>();
           
            }

            public int getPointsCount() {
                return titik.Count();
            }
            public bool addPoint(Point p, Color c) {
                if (getPointsCount() < 3) {
                    this.titik.Add(p);
                    this.warna.Add(c);
                    return true;
                } else {
                    return false;
                }
            }
            
            public override string ToString() {
                string s = "";
                for (int i = 0; i < warna.Count; i++) {
                    s += "x:" + titik[i].X + " - y:" + titik[i].Y + "\nColor:R" + warna[i].R + "G" + warna[i].G + "B" + warna[i].B+"\n===";
                }
                return s;
            }
        }

        public List<Triangle> segitigas;
        private int ctr = 0;
        public Form1() {
            segitigas = new List<Triangle>();
            InitializeComponent();
            colorDialog1.AnyColor = true;
            colorDialog1.SolidColorOnly = true;
            colorLabel.Text = "Pilih warna dulu";
        }

        private void pickColor_Click(object sender, EventArgs e) {
            var temp = colorDialog1.ShowDialog();
            if (temp==DialogResult.OK) {
                var warna = colorDialog1.Color;
                colorLabel.BackColor = warna;
                colorLabel.Text = "Ready to Use";
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e) {
            if (colorLabel.Text.Equals("Ready to Use")) {
                if (segitigas.Count <= 0) {
                    segitigas.Add(new Triangle());
                }
                Triangle last = segitigas[segitigas.Count - 1];
                if (!last.addPoint(e.Location, colorLabel.BackColor)) {
                    last = new Triangle();
                    last.addPoint(e.Location, colorLabel.BackColor);
                    segitigas.Add(last);
                }
                ++ctr;
            } else {
                MessageBox.Show(colorLabel.Text);
            }
            foreach (var t in segitigas) {
                Console.WriteLine(t.ToString());
            }
            //gambar ulang tiap tiga kali klik
            if (ctr % 3 == 0) {
                ctr = 0; panel1.Invalidate();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) {
            foreach (var item in segitigas) {
                if (item.getPointsCount() == 3) {
                    //kalau belum pernah dihitung sebelumnya
                    //hitung dulu
                    if (item.A.Count == 0) {
                        Point satu, dua;
                        satu = item.titik[0];
                        dua = item.titik[1];
                        int a1, b1; double c1;
                        double cek;
                        //cari C untuk setiap ax+by+c=0
                        cek = 0;
                        a1 = satu.Y - dua.Y;
                        b1 = dua.X - satu.X;
                        c1 = (a1 * (satu.X + dua.X) + b1 * (satu.Y + dua.Y)) / -2.0;

                        int a2, b2; double c2;
                        satu = item.titik[1]; dua = item.titik[2];
                        a2 = satu.Y - dua.Y;
                        b2 = dua.X - satu.X;
                        c2 = (a2 * (satu.X + dua.X) + b2 * (satu.Y + dua.Y)) / -2.0;

                        int a3, b3;  double c3;
                        satu = item.titik[2]; dua = item.titik[0];
                        a3 = satu.Y - dua.Y;
                        b3 = dua.X - satu.X;
                        c3 = (a3 * (satu.X + dua.X) + b3 * (satu.Y + dua.Y)) / -2.0;

                        cek = c1 + c2 + c3;

                        //nentuin Ax+By+Cz+D = 0;
                        //cross product
                        item.Red = Vector3D.CrossProduct(
                            new Vector3D(item.titik[2].X - item.titik[1].X, item.titik[2].Y - item.titik[1].Y, item.warna[2].R - item.warna[1].R), 
                            new Vector3D(item.titik[1].X - item.titik[0].X, item.titik[1].Y - item.titik[0].Y, item.warna[1].R - item.warna[0].R)
                            );
                        item.Green = Vector3D.CrossProduct(
                            new Vector3D(item.titik[2].X - item.titik[1].X, item.titik[2].Y - item.titik[1].Y, item.warna[2].G - item.warna[1].G),
                            new Vector3D(item.titik[1].X - item.titik[0].X, item.titik[1].Y - item.titik[0].Y, item.warna[1].G - item.warna[0].G)
                            );
                        item.Blue = Vector3D.CrossProduct(
                            new Vector3D(item.titik[2].X - item.titik[1].X, item.titik[2].Y - item.titik[1].Y, item.warna[2].B - item.warna[1].B),
                            new Vector3D(item.titik[1].X - item.titik[0].X, item.titik[1].Y - item.titik[0].Y, item.warna[1].B - item.warna[0].B)
                            );

                        //kalau urutan ngeklik salah, kali -1
                        if (cek < 0) {
                            a1 = -a1;
                            a2 = -a2;
                            a3 = -a3;
                            b1 = -b1;
                            b2 = -b2;
                            b3 = -b3;
                            c1 = -c1;
                            c2 = -c2;
                            c3 = -c3;
                        }
                        

                        Point maks, min;
                        //set bounding box for
                        maks = new Point(x: maksimumX(item), y: maksimumY(item));
                        min = new Point(x: minimumX(item), y: minimumY(item));

                        Pen pen = new Pen(Color.Black);

                        //simpan data yang sudah dihitung ke array
                        item.A.Add((int)a1); item.B.Add((int)b1);
                        item.A.Add((int)a2); item.B.Add((int)b2);
                        item.A.Add((int)a3); item.B.Add((int)b3);
                        item.C.Add(c1);
                        item.C.Add(c2);
                        item.C.Add(c3);
                        //red
                        item.Dred = -(int)(item.Red.X * item.titik[0].X + item.Red.Y * item.titik[0].Y + item.Red.Z * item.warna[0].R);
                        //green
                        item.Dgreen = -(int)(item.Green.X * item.titik[0].X + item.Green.Y * item.titik[0].Y + item.Green.Z * item.warna[0].G);
                        //blue
                        item.Dblue = -(int)(item.Blue.X * item.titik[0].X + item.Blue.Y * item.titik[0].Y + item.Blue.Z * item.warna[0].B);
                        for (int j = min.Y; j <= maks.Y; j++) {
                            for (int i = min.X; i <= maks.X; i++) {
                                if (a1 * i + b1 * j + c1 < 0) {

                                } else if (a2 * i + b2 * j + c2 < 0) {

                                } else if (a3 * i + b3 * j + c3 < 0) {

                                } else {
                                    int merah, hijau, biru;
                                    merah =(int) (-((item.Red.X * i + item.Red.Y * j + item.Dred )/item.Red.Z));
                                    hijau = (int)(-((item.Green.X * i + item.Green.Y * j + item.Dgreen) / item.Green.Z));
                                    biru = (int)(-((item.Blue.X * i + item.Blue.Y * j + item.Dblue) / item.Blue.Z));
                                    
                                    pen = new Pen(Color.FromArgb(red:merah, green:hijau, blue:biru));
                                    e.Graphics.DrawRectangle(pen, i, j, 1, 1);
                                }
                            }
                        }
                    }
                    //kalau sudah pernah dihitung sebelumnya, langsung gambar
                    else {
                        MessageBox.Show("Oke");
                        Point maks, min;
                        maks = new Point(x: maksimumX(item), y: maksimumY(item));
                        min = new Point(x: minimumX(item), y: minimumY(item));
                        Pen pen = new Pen(Color.Black);
                        for (int j = min.Y; j <= maks.Y; j++) {
                            for (int i = min.X; i <= maks.X; i++) {
                                if (item.A[0] * i + item.B[0] * j + item.C[0] < 0) {
                                } else if (item.A[1] * i + item.B[1] * j + item.C[1] < 0) {
                                } else if (item.A[2] * i + item.B[2] * j + item.C[2] < 0) {
                                } else {
                                    int merah, hijau, biru;
                                    merah = (int)(-((item.Red.X * i + item.Red.Y * j + item.Dred) / item.Red.Z));
                                    hijau = (int)(-((item.Green.X * i + item.Green.Y * j + item.Dgreen) / item.Green.Z));
                                    biru = (int)(-((item.Blue.X * i + item.Blue.Y * j + item.Dblue) / item.Blue.Z));
                                    pen = new Pen(Color.FromArgb(red: merah, green: hijau, blue: biru));
                                    e.Graphics.DrawRectangle(pen, i, j, 1, 1);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static int minimumY(Triangle item) {
            return Math.Min(Math.Min(item.titik[0].Y, item.titik[1].Y), item.titik[2].Y);
        }

        private static int minimumX(Triangle item) {
            return Math.Min(Math.Min(item.titik[0].X, item.titik[1].X), item.titik[2].X);
        }

        private static int maksimumY(Triangle item) {
            return Math.Max(Math.Max(item.titik[0].Y, item.titik[1].Y), item.titik[2].Y);
        }

        private static int maksimumX(Triangle item) {
            return Math.Max(Math.Max(item.titik[0].X, item.titik[1].X), item.titik[2].X);
        }
    }

    
}
