using System.Windows.Forms;

namespace cartographer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Logger.init(textBoxLog);
        }

        private void quitMenu_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void optionMenu_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem choice = (ToolStripMenuItem)sender;

            if (choice.CheckState == CheckState.Checked)
                return;

            if (sender == showChangedMapsOnlyMenu)
            {
                showChangedMapsOnlyMenu.CheckState = CheckState.Checked;
                showAllMapsMenu.CheckState = CheckState.Unchecked;
            }
            else
            {
                showChangedMapsOnlyMenu.CheckState = CheckState.Unchecked;
                showAllMapsMenu.CheckState = CheckState.Checked;
            }
        }

        private void helpRegexMenu_Click(object sender, System.EventArgs e)
        {
            System.Diagnostics.Process.Start("http://msdn.microsoft.com/en-us/library/az24scfc.aspx");
        }

        private void aboutMenu_Click(object sender, System.EventArgs e)
        {
            AboutForm a = new AboutForm();
            a.ShowDialog();
            a.Dispose();
        }
    }
}
