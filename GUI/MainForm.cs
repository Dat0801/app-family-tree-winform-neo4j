using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            câyGiaPhảToolStripMenuItem.Click += CâyGiaPhảToolStripMenuItem_Click;
        }

        private void CâyGiaPhảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FamilyTreeForm familyTreeForm = new FamilyTreeForm();
            familyTreeForm.MdiParent = this;
            familyTreeForm.Show();
        }
    }
}
