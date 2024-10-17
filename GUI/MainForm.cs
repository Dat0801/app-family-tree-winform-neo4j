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
            dsThanhVienToolStripMenuItem.Click += DsThanhVienToolStripMenuItem_Click;
        }
        private void DsThanhVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MemberForm memberForm = new MemberForm();
            memberForm.MdiParent = this;
            memberForm.Show();
        }

        private void CâyGiaPhảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FamilyTreeForm familyTreeForm = new FamilyTreeForm();
            familyTreeForm.MdiParent = this;
            familyTreeForm.Show();
        }
    }
}
