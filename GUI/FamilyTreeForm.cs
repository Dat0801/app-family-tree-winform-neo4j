using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
namespace GUI
{
    public partial class FamilyTreeForm : Form
    {
        private readonly PersonBLL personBLL;
        public FamilyTreeForm()
        {
            InitializeComponent();
            personBLL = new PersonBLL();
            btnSearch.Click += BtnSearch_Click;
            btnViewDetails.Click += BtnViewDetails_Click;
        }

        private void BtnViewDetails_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có node nào được chọn trong TreeView không
            if (treeViewGiaPha.SelectedNode != null)
            {
                // Lấy tên của thành viên được chọn
                string selectedPersonName = treeViewGiaPha.SelectedNode.Text;

                // Tạo và hiển thị thông tin chi tiết cho thành viên
                // Ở đây bạn có thể gọi một hàm để lấy thông tin từ cơ sở dữ liệu hoặc từ danh sách đã lưu
                //Person details = GetPerson(selectedPersonName); // Giả sử bạn có một phương thức này

                // Hiển thị thông tin chi tiết
                //string message = $"Tên: {details.Name}\n" +
                //                 $"Ngày sinh: {details.DateOfBirth}\n" +
                //                 $"Giới tính: {details.Gender}\n" +
                //                 $"Địa chỉ: {details.Address}\n" +
                //                 $"Số điện thoại: {details.PhoneNumber}\n" +
                //                 $"Nghề nghiệp: {details.Occupation}\n" +
                //                 $"Tiểu sử: {details.Biography}";

                //MessageBox.Show(message, "Thông Tin Chi Tiết", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thành viên để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            string name = txtMember.Text;
            string username = UserContext.CurrentUserName;

            // Lấy thông tin từ BLL
            List<PersonRelationship> familyTree = await personBLL.GetFamilyTree(name, username);

            // Xóa dữ liệu cũ trong TreeView
            treeViewGiaPha.Nodes.Clear();

            // Dictionary để theo dõi các nút đã thêm
            var memberNodes = new Dictionary<string, TreeNode>();

            // Hàm đệ quy để thêm nút cho cây gia đình
            void AddFamilyNode(TreeNode parentNode, string personName)
            {
                // Lấy tất cả các mối quan hệ của người này
                var relationships = familyTree.Where(r => r.Person.Name == personName).ToList();

                // Thêm các nút cho mối quan hệ
                foreach (var relationship in relationships)
                {
                    var relatedNode = new TreeNode($"({relationship.Relationship}) {relationship.RelatedPerson.Name}");
                    parentNode.Nodes.Add(relatedNode);

                    // Gọi đệ quy để thêm con cái
                    AddFamilyNode(relatedNode, relationship.RelatedPerson.Name);
                }
            }

            // Tạo nút gốc cho Bob
            var rootNode = new TreeNode(name);
            treeViewGiaPha.Nodes.Add(rootNode);
            memberNodes[name] = rootNode;

            // Bắt đầu thêm các nút cho cây gia đình từ Bob
            AddFamilyNode(rootNode, name);

            treeViewGiaPha.ExpandAll(); // Mở rộng tất cả các nút
        }


    }
}
