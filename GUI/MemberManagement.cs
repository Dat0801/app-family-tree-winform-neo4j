using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTO;
using BLL;
namespace GUI
{
    public partial class MemberManagement : Form
    {
        PersonBLL personBLL = new PersonBLL();
        public Person SelectedPerson = null;
        public MemberManagement()
        {
            InitializeComponent();
            this.Load += MemberManagement_Load;
            dataGridView1.CellClick += DataGridView1_CellClick;
            btnThem.Click += BtnThem_Click;
        }

        private async void BtnThem_Click(object sender, EventArgs e)
        {
            string name = cboMember.SelectedValue.ToString();
            Person person = new Person();
            person.Name = txtName.Text;
            person.Gender = cboGender.SelectedItem.ToString();
            person.DateOfBirth = dateTimePicker1.Value.ToString();
            person.Address = txtAddress.Text;
            person.PhoneNumber = txtPhone.Text;
            person.Occupation = txtOccupation.Text;
            string relationship = cboRelationship.SelectedItem.ToString();
            if (name == "" && relationship == "")
            {
                bool isAdded = await personBLL.AddPersonWithoutRelationship(person);
                if (isAdded)
                {
                    MessageBox.Show("Thêm thành viên thành công.");
                }
                else
                {
                    MessageBox.Show("Thêm thành viên không thành công.");
                }
                LoadThanhVien();
            } else if(name == "" || relationship == "")
            {
                MessageBox.Show("Vui lòng chọn đầy đủ thành viên và mối quan hệ");
            } else
            {
                Person relatedPerson = new Person();
                relatedPerson.Name = name;
                bool isAdded = false;
                if (relationship == "CHA/MẸ")
                {
                    isAdded = await personBLL.AddPersonWithParent(person, relatedPerson);
                }
                else
                {
                    isAdded = await personBLL.AddPersonWithSpouse(person, relatedPerson);
                }
                if (isAdded)
                {
                    MessageBox.Show("Thêm thành viên thành công.");
                }
                else
                {
                    MessageBox.Show("Thêm thành viên không thành công.");
                }
                LoadThanhVien();
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                txtName.Text = row.Cells["Name"].Value?.ToString();
                dateTimePicker1.Value = DateTime.Parse(row.Cells["DateOfBirth"].Value?.ToString());
                var gender = row.Cells["Gender"].Value?.ToString();
                if(gender == "Nam")
                {
                    cboGender.SelectedIndex = 0;
                } else
                {
                    cboGender.SelectedIndex = 1;
                }
                txtAddress.Text = row.Cells["Address"].Value?.ToString();
                txtPhone.Text = row.Cells["PhoneNumber"].Value?.ToString();
                txtOccupation.Text = row.Cells["Occupation"].Value?.ToString();
            }
        }

        private async void MemberManagement_Load(object sender, EventArgs e)
        {
            LoadThanhVien();
            string userName = UserContext.CurrentUserName;
            List<Person> persons = await personBLL.GetPersonsByUserName(userName);
            var personsWithEmpty = new List<Person>(persons); 
            personsWithEmpty.Insert(0, new Person { Name = "" }); 
            cboMember.DataSource = personsWithEmpty;
            cboMember.DisplayMember = "Name";
            cboMember.ValueMember = "Name";
            if (SelectedPerson != null)
            {
                cboMember.SelectedValue = SelectedPerson.Name;
            }
            var relationshipList = new List<string> { "", "CHA/MẸ", "VỢ/CHỒNG" };
            cboRelationship.DataSource = relationshipList;
        }

        async void LoadThanhVien()
        {
            string userName = UserContext.CurrentUserName;
            List<Person> persons = await personBLL.GetPersonsByUserName(userName);
            dataGridView1.DataSource = persons;
            dataGridView1.Columns[0].Visible = false;
            int totalColumns = dataGridView1.Columns.Count;
            if (totalColumns >= 3)
            {
                dataGridView1.Columns[totalColumns - 1].Visible = false;
                dataGridView1.Columns[totalColumns - 2].Visible = false;
                dataGridView1.Columns[totalColumns - 3].Visible = false;
            }
        }
    }
}
