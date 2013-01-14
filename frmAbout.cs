using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Sharp
{
    partial class frmAbout : Form
    {
        private Bitmap modbit;
        private int tmrmr = 0;

        public frmAbout()
        {
            InitializeComponent();
            this.Text = String.Format("About {0}", AssemblyTitle);
            /*this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;*/
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void frmAbout_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void logoPictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void frmAbout_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            modbit = new Bitmap(pbMain.Image);
            Refregerator.Prepare(ref modbit);

            Graphics gr = Graphics.FromImage(modbit);
            gr.DrawString("      " + AssemblyProduct + "\n" + AssemblyDescription + "\n     " + String.Format("Version {0}", AssemblyVersion), new Font(FontFamily.GenericMonospace, 9), Brushes.LightBlue, new PointF(60, 180));

            pbMain.Image = modbit;
            tmrMain.Start();
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(modbit);

            tmrmr += 2;
            if (tmrmr > bmp.Height - 12) tmrmr = 0;

            Refregerator.DoCollized(tmrmr, ref bmp);
            pbMain.Image = bmp;
        }
    }

    public class Refregerator
    {
        public static void Prepare(ref Bitmap bmp)
        {
            int width = bmp.Size.Width;
            int height = bmp.Size.Height;

            for (int r = 0; r < height-4; r += 4)
                for (int i = 0; i < width - 1; i++)
                {
                    bmp.SetPixel(i, r, Color.FromArgb(0, 37, 80));
                    bmp.SetPixel(i, r + 2, Color.FromArgb(0, 32, 37));
                }
        }

        private static Color OptimizeC(int val, Color bg)
        {
            int r = bg.R + val; if (r > 255) r = 255;
            int g = bg.G + val; if (g > 255) g = 255;
            int b = bg.B + val; if (b > 255) b = 255;
            return Color.FromArgb(r, g, b);
        }

        public static void DoCollized(int begvector, ref Bitmap bmp)
        {
            int width = bmp.Size.Width;
            int height = bmp.Size.Height;
            int r = begvector;
            int[] a = new int[6] { 1, 2, 3, 3, 2, 1 };
            int p = 0;

            for (int j = 0; j < 6; j++)
            {
                p = a[j] * 16;
                for (int i = 0; i < width; i++)
                {
                    Color cl = OptimizeC(p, bmp.GetPixel(i, r + (j * 2)));
                    bmp.SetPixel(i, r + (j * 2), cl);
                }
            }
        }
    }
}
