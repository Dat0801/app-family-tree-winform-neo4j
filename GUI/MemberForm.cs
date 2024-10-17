using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Report;
namespace GUI
{
    public partial class MemberForm : Form
    {
        PersonBLL personBLL = new PersonBLL();
        ExcelExport exe = new ExcelExport();
        public MemberForm()
        {
            InitializeComponent();
            btnExcel.Click += BtnExcel_Click;
        }

        private async void BtnExcel_Click(object sender, EventArgs e)
        {
            List<Person> Persons = await personBLL.GetPersonsByUserName(UserContext.CurrentUserName);
            var filename = "DanhSachThanhVien";
            exe.ExportKhoa(Persons, ref filename, false);
            System.Diagnostics.Process.Start(filename);
        }

        private async void DanhSachCayGiaPha_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Checked = false;
            dateTimePicker1.CustomFormat = " ";
            dateTimePicker1.ShowCheckBox = true;
            dateTimePicker1.ValueChanged += (s, ev) =>
            {
                if (dateTimePicker1.Checked)
                {
                    dateTimePicker1.CustomFormat = "dd/MM/yyyy";
                }
                else
                {
                    dateTimePicker1.CustomFormat = " ";
                }
            };
            fullname_txt.Clear();
            address_txt.Clear();
            gender_combo.SelectedIndex = -1;
            string userName = UserContext.CurrentUserName;
            List<Person> persons = await personBLL.GetPersonsByUserName(userName);

            if (persons != null && persons.Count > 0)
            {
                int idCounter = 1;
                foreach (Person person in persons)
                {
                    string genderText = person.Gender.ToLower() == "female" ? "Nữ" : "Nam";
                    datafamilytree.Rows.Add(
                        idCounter++,
                        person.Name,
                        person.DateOfBirth,
                        genderText,
                        person.PhoneNumber,
                        person.Address,
                        person.Occupation
                    );
                }
            }
            else
            {
                datafamilytree.DataSource = null;
                MessageBox.Show("Không có dữ liệu người dùng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void search_btn_Click(object sender, EventArgs e)
        {
            string fullname = fullname_txt.Text.Trim();
            string gender = gender_combo.SelectedItem != null ? gender_combo.SelectedItem.ToString() : string.Empty;
            if (gender == "Nam")
            { gender = "Male"; }
            else if (gender == "Nữ")
            { gender = "Female"; }
            string address = address_txt.Text.Trim();
            string phoneNumber = phonenum_txt.Text.Trim();
            string occupation = occupation_txt.Text.Trim();
            DateTime? birthDate = dateTimePicker1.Checked ? (DateTime?)dateTimePicker1.Value : null;
            Console.WriteLine($"Full Name: {fullname}, Gender: {gender}, Address: {address}, BirthDate: {birthDate}, Phone Number: {phoneNumber}, Occupation: {occupation}");
            PersonBLL personBLL = new PersonBLL();
            List<Person> result = await personBLL.SearchPersons(fullname, birthDate, gender, address, phoneNumber, occupation);
            if (result != null && result.Count > 0)
            {
                int id_data = 1;
                datafamilytree.Rows.Clear();
                foreach (var person in result)
                {
                    datafamilytree.Rows.Add(
                        id_data++,
                        person.Name,
                        person.DateOfBirth,
                        person.Gender,
                        person.PhoneNumber,
                        person.Address,
                        person.Occupation
                    );
                }
            }
            else
            {
                datafamilytree.DataSource = null;
                MessageBox.Show("Không tìm thấy thành viên nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void clear_btn_Click(object sender, EventArgs e)
        {
            fullname_txt.Clear();
            address_txt.Clear();
            phonenum_txt.Clear();
            occupation_txt.Clear();
            gender_combo.SelectedIndex = -1;
            dateTimePicker1.Checked = false;
            dateTimePicker1.CustomFormat = " ";
            datafamilytree.Rows.Clear();
        }

        private void detail_btn_Click(object sender, EventArgs e)
        {
            if (datafamilytree.SelectedRows.Count > 0)
            {
                string name = fullname_txt.Text.Trim();
                string gender = gender_combo.SelectedItem != null ? gender_combo.SelectedItem.ToString() : string.Empty;
                string phoneNumber = phonenum_txt.Text.Trim();
                string birthDate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
                string address = address_txt.Text.Trim();
                string occupation = occupation_txt.Text.Trim();

                ThongTinChiTiet detailForm = new ThongTinChiTiet();
                detailForm.NameText = name;
                detailForm.GenderText = gender;
                detailForm.PhoneNumberText = phoneNumber;
                detailForm.BirthDateText = birthDate;
                detailForm.AddressText = address;
                detailForm.OccupationText = occupation;
                detailForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người để xem thông tin chi tiết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void update_btn_Click(object sender, EventArgs e)
        {
            string fullname = fullname_txt.Text.Trim();
            DateTime? birthDate = dateTimePicker1.Checked ? (DateTime?)dateTimePicker1.Value : null;
            string gender = gender_combo.SelectedItem != null ? gender_combo.SelectedItem.ToString() : string.Empty;
            string phoneNumber = phonenum_txt.Text.Trim();
            string address = address_txt.Text.Trim();
            string occupation = occupation_txt.Text.Trim();
            PersonBLL personBLL = new PersonBLL();
            bool success = personBLL.UpdatePerson(fullname, birthDate, gender, phoneNumber, address, occupation);

            if (success)
            {
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                datafamilytree.Rows.Clear();
                DanhSachCayGiaPha_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void delete_btn_Click(object sender, EventArgs e)
        {
            string fullname = fullname_txt.Text.Trim();
            string phoneNumber = phonenum_txt.Text.Trim();

            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Vui lòng chọn thành viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa thành viên {fullname}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                PersonBLL personBLL = new PersonBLL();
                bool success = await personBLL.DeletePerson(fullname, phoneNumber);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    datafamilytree.Rows.Clear();
                    DanhSachCayGiaPha_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void datafamilytree_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            fullname_txt.Text = datafamilytree.Rows[e.RowIndex].Cells["hoten_data"].Value.ToString();
            if (datafamilytree.Rows[e.RowIndex].Cells["birthdate_data"].Value != null)
            {
                dateTimePicker1.Value = DateTime.Parse(datafamilytree.Rows[e.RowIndex].Cells["birthdate_data"].Value.ToString());
            }
            else
            {
                dateTimePicker1.CustomFormat = " ";
            }
            gender_combo.Text = datafamilytree.Rows[e.RowIndex].Cells["gender_data"].Value.ToString();
            phonenum_txt.Text = datafamilytree.Rows[e.RowIndex].Cells["phone_data"].Value.ToString();
            address_txt.Text = datafamilytree.Rows[e.RowIndex].Cells["address_data"].Value.ToString();
            occupation_txt.Text = datafamilytree.Rows[e.RowIndex].Cells["occupation"].Value.ToString();
        }
    }
}
