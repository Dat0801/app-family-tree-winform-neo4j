using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
namespace GUI
{
    public partial class FormCayGiaPha : Form
    {
        private readonly PersonBLL personBLL;
        public FormCayGiaPha()
        {
            InitializeComponent();
            personBLL = new PersonBLL();
            btnSearch.Click += BtnSearch_Click;
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            string name = txtMember.Text;

            // Lấy thông tin từ BLL
            List<PersonRelationship> familyTree = await personBLL.GetFamilyTree(name);

            // Xóa dữ liệu cũ trong TreeView
            treeView1.Nodes.Clear();

            var memberNodes = new Dictionary<string, TreeNode>(); // Dictionary để theo dõi các nút đã thêm

            foreach (var relationship in familyTree)
            {
                // Kiểm tra nếu người chính chưa được thêm vào
                if (!memberNodes.ContainsKey(relationship.Person.Name))
                {
                    // Tạo nút cho người chính mà không có mối quan hệ
                    var personNode = new TreeNode(relationship.Person.Name);
                    memberNodes[relationship.Person.Name] = personNode; // Thêm vào dictionary

                    // Thêm vào TreeView
                    treeView1.Nodes.Add(personNode);
                }

                // Lấy nút người chính từ dictionary
                var mainPersonNode = memberNodes[relationship.Person.Name];

                // Tạo nút cho mối quan hệ và thành viên liên quan
                var relatedNode = new TreeNode($"({relationship.Relationship}) {relationship.RelatedPerson.Name}");

                // Thêm thành viên liên quan vào người chính
                mainPersonNode.Nodes.Add(relatedNode);
            }

            treeView1.ExpandAll(); // Mở rộng tất cả các nút
        }
    }
}
