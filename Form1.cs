using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace GMAPSBETA
{
    public partial class Form1 : Form
    {
        private DataManager dataM = new DataManager();


        private List<PointLatLng> points;
        private List<PointLatLng> polygons;
        private List<PointLatLng> routes;

        private GMapOverlay pointsOverlay;
        private GMapOverlay polygonsOverlay;
        private GMapOverlay RoutesOverlay;

        public Form1()
        {
            InitializeComponent();
            dataM = new DataManager();

            points = new List<PointLatLng>();
            polygons = new List<PointLatLng>();
            routes = new List<PointLatLng>();

            pointsOverlay = new GMapOverlay("points");
            polygonsOverlay = new GMapOverlay("polygons");
            RoutesOverlay = new GMapOverlay("routes");

            comboBox1.Items.Add("Point");
            comboBox1.Items.Add("Polygon");
            comboBox1.Items.Add("Route");
        }


        //private void Clear_Click(object sender, EventArgs e)
        //{
        //    points.Clear();
        //    polygons.Clear();
        //    routes.Clear();
        //    pointsOverlay.Clear();
        //    polygonsOverlay.Clear();
        //    RoutesOverlay.Clear();
        //}

        private void setPoint()
        {
            foreach (PointLatLng p in points)
            {
                GMapMarker gm = new GMarkerGoogle(p, GMarkerGoogleType.arrow);
                pointsOverlay.Markers.Add(gm);
            }
        }

        private void setPolygons()
        {
            GMapPolygon gr = new GMapPolygon(polygons, "Polygon");
            gr.Fill = new SolidBrush(Color.FromArgb(50, Color.Coral));
            gr.Stroke = new Pen(Color.Coral, 2);

            polygonsOverlay.Polygons.Add(gr);
        }

        private void setRoutes()
        {
            GMapRoute gr = new GMapRoute(routes, "Routes");
            gr.Stroke = new Pen(Color.Coral, 2);
            RoutesOverlay.Routes.Add(gr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("HOLA");
            try
            {

                double la = double.Parse(textBox1.Text);
                textBox1.Text = "";
                double lo = Double.Parse(textBox2.Text);
                textBox2.Text = "";

                PointLatLng p = new PointLatLng(la, lo);

                if (comboBox1.Text == "Point")
                {
                    points.Add(p);
                }
                else if (comboBox1.Text == "Polygon")
                {
                    polygons.Add(p);
                }
                else
                {
                    routes.Add(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Empty lines");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("HOLA");
            if (comboBox1.Text == "Point")
            {
                setPoint();
            }
            else if (comboBox1.Text == "Polygon")
            {
                setPolygons();
            }
            else
            {
                setRoutes();
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            gMapControl1.Position = new PointLatLng(4.6097100, -74.0817500);

            gMapControl1.Overlays.Add(pointsOverlay);
            gMapControl1.Overlays.Add(polygonsOverlay);
            gMapControl1.Overlays.Add(RoutesOverlay);
        }
    }
}