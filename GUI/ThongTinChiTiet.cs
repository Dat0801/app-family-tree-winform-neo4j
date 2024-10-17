using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class ThongTinChiTiet : Form
    {
        public string NameText { get; set; }
        public string GenderText { get; set; }
        public string PhoneNumberText { get; set; }
        public string BirthDateText { get; set; }
        public string AddressText { get; set; }
        public string OccupationText { get; set; }
        public ThongTinChiTiet()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
        private async Task LoadRelationshipsToGridView(string personName)
        {
            // Lấy danh sách quan hệ
            PersonBLL personBLL = new PersonBLL();
            List<PersonRelationship> relationships = await personBLL.GetPersonRelationships(personName);

            // Thêm cột vào DataGridView (nếu chưa có)
            if (dataGridView1 .Columns.Count == 0)
            {
                dataGridView1.Columns.Add("Person", "Tên");
                dataGridView1.Columns.Add("Relationship", "Quan hệ");
                dataGridView1.Columns.Add("RelatedPerson", "Người có quan hệ");
            }

            // Xóa các hàng hiện tại
            dataGridView1.Rows.Clear();

            // Thêm dữ liệu vào DataGridView
            foreach (var relationship in relationships)
            {
                // Chuyển đổi giá trị relationship thành văn bản cần hiển thị
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

                // Thêm dòng vào DataGridView với thông tin đã chuyển đổi
                dataGridView1.Rows.Add(relationship.Person.Name, displayRelationship, relationship.RelatedPerson.Name);
            }
        }



        private async void ThongTinChiTiet_Load(object sender, EventArgs e)
        {
            label_name.Text = NameText;
            label_gender.Text = GenderText;
            label_number.Text = PhoneNumberText;
            label_bd.Text = BirthDateText;
            label_addr.Text = AddressText;
            label_occ.Text = OccupationText;

            await LoadRelationshipsToGridView(NameText);
        }
    }
}
