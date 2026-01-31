using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Customer
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // عند إغلاق شاشة تسجيل الدخول، انهي التطبيق بالكامل
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

            // تكبير الخطوط
            comboBoxUsers.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            
            comboBoxUsers.DropDownHeight = 250;
            comboBoxUsers.IntegralHeight = false;

            textBoxPassword.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        
            // تحميل البيانات
            if (AppDataStore.Current == null)
                AppDataStore.Load();

            // ربط الكومبوبوكس بقائمة المستخدمين المحفوظة في JSON
            comboBoxUsers.DataSource = null;
            comboBoxUsers.DisplayMember = "UserName";
            comboBoxUsers.ValueMember = "UserName";

            comboBoxUsers.DataSource = AppDataStore.Current.Users.ToList();

            if (comboBoxUsers.Items.Count > 0)
                comboBoxUsers.SelectedIndex = 0;
        }

        

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var selectedUser = comboBoxUsers.SelectedItem as UserInfo;
            if (selectedUser == null)
            {
                MessageBox.Show("اختر اسم المستخدم أولاً");
                return;
            }

            if (textBoxPassword.Text != selectedUser.Password)
            {
                MessageBox.Show("كلمة المرور غير صحيحة", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // نخزن بيانات المستخدم الحالية في Program
            Program.LoggedInUserName = selectedUser.UserName;
            Program.LoggedInUserRole = selectedUser.Role;

            // نفتح فورم الكاشير
            var main = new Customer(Program.LoggedInUserRole, Program.LoggedInUserName);
            main.Show();
            this.Hide();
        
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }

}
