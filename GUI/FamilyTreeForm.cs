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
        public string SelectedName = null;
        public FamilyTreeForm()
        {
            InitializeComponent();
            personBLL = new PersonBLL();
            btnSearch.Click += BtnSearch_Click;
            btnViewDetails.Click += BtnViewDetails_Click;
            btnAddMember.Click += BtnAddMember_Click;
            this.Load += FamilyTreeForm_Load;
        }

        private void FamilyTreeForm_Load(object sender, EventArgs e)
        {
            if(SelectedName != null)
            {
                txtMember.Text = SelectedName;
                loadCayGiaPha(SelectedName, UserContext.CurrentUserName);
            }
        }

        private async void BtnAddMember_Click(object sender, EventArgs e)
        {
            if (treeViewGiaPha.SelectedNode != null)
            {
                string selectedPersonName = treeViewGiaPha.SelectedNode.Text;
                if (selectedPersonName.Contains(')'))
                {
                    selectedPersonName = selectedPersonName.Substring(selectedPersonName.IndexOf(')') + 1).Trim();
                }
                Person person = await personBLL.GetPersonsByName(selectedPersonName);
                MemberManagement memberManagement = new MemberManagement();
                memberManagement.SelectedPerson = person;
                memberManagement.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thành viên để thêm thành viên mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void BtnViewDetails_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có node nào được chọn trong TreeView không
            if (treeViewGiaPha.SelectedNode != null)
            {
                string selectedPersonName = treeViewGiaPha.SelectedNode.Text;
                if (selectedPersonName.Contains(')'))
                {
                    selectedPersonName = selectedPersonName.Substring(selectedPersonName.IndexOf(')') + 1).Trim();
                }
                Person person = await personBLL.GetPersonsByName(selectedPersonName);
                ThongTinChiTiet detailForm = new ThongTinChiTiet();
                detailForm.NameText = person.Name;
                detailForm.GenderText = person.Gender;
                detailForm.PhoneNumberText = person.PhoneNumber;
                detailForm.BirthDateText = person.DateOfBirth;
                detailForm.AddressText = person.Address;
                detailForm.OccupationText = person.Occupation;
                detailForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một thành viên để xem chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string name = txtMember.Text;
            string username = UserContext.CurrentUserName;
            loadCayGiaPha(name, username);
        }

        public async void loadCayGiaPha(string name, string username)
        {
            // Lấy thông tin từ BLL
            List<PersonRelationship> familyTree = await personBLL.GetFamilyTree(name, username);

            // Xóa dữ liệu cũ trong TreeView
            treeViewGiaPha.Nodes.Clear();

            // Dictionary để theo dõi các nút đã thêm
            var memberNodes = new Dictionary<string, TreeNode>();

            // Tạo nút gốc cho Bob
            var rootNode = new TreeNode(name);
            treeViewGiaPha.Nodes.Add(rootNode);
            memberNodes[name] = rootNode;

            // Bắt đầu thêm các nút cho cây gia đình từ Bob
            AddFamilyNode(rootNode, name, familyTree);

            treeViewGiaPha.ExpandAll(); // Mở rộng tất cả các nút
        }

        // Hàm đệ quy để thêm nút cho cây gia đình
        void AddFamilyNode(TreeNode parentNode, string personName, List<PersonRelationship> familyTree)
        {
            // Lấy tất cả các mối quan hệ của người này
            var relationships = familyTree.Where(r => r.Person.Name == personName).ToList();

            // Thêm các nút cho mối quan hệ
            foreach (var relationship in relationships)
            {
                string displayRelationship;
                if (relationship.Relationship == "PARENT_OF")
                {
                    displayRelationship = "Cha/Mẹ";
                }
                else if (relationship.Relationship == "MARRIED_TO")
                {
                    displayRelationship = "Vợ/Chồng";
                }
                else
                {
                    displayRelationship = relationship.Relationship;  // Nếu không khớp, giữ nguyên
                }
                var relatedNode = new TreeNode($"({displayRelationship}) {relationship.RelatedPerson.Name}");
                parentNode.Nodes.Add(relatedNode);

                // Gọi đệ quy để thêm con cái
                AddFamilyNode(relatedNode, relationship.RelatedPerson.Name, familyTree);
            }
        }
    }
}
