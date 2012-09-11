using System;
using System.Reflection;
using System.Windows.Forms;

namespace cartographer
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = String.Format("Version: {0}.{1}", version.Major, version.Minor);
        }
    }
}
