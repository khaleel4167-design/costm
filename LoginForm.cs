using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity; // ضروري لعمل EF6
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

            try
            {
                // إنشاء قاعدة البيانات والجداول إذا لم تكن موجودة
                AppDbContext.InitializeDatabase();

                // تأخير بسيط لضمان اكتمال إنشاء الجداول
                System.Threading.Thread.Sleep(100);

                // محاولة تحميل المستخدمين مع إعادة المحاولة في حالة الفشل
                bool success = false;
                int retryCount = 0;
                Exception lastException = null;

                while (!success && retryCount < 3)
                {
                    try
                    {
                        // تحميل المستخدمين باستخدام SQLite مباشرة
                        using (var connection = new System.Data.SQLite.SQLiteConnection("Data Source=AppDatabase.db;Version=3;"))
                        {
                            connection.Open();

                            // التحقق من وجود مستخدمين
                            using (var checkCmd = connection.CreateCommand())
                            {
                                checkCmd.CommandText = "SELECT COUNT(*) FROM UserInfoes";
                                var count = Convert.ToInt32(checkCmd.ExecuteScalar());

                                // إذا لم يكن هناك مستخدمين، أضف الأدمن
                                if (count == 0)
                                {
                                    using (var insertCmd = connection.CreateCommand())
                                    {
                                        insertCmd.CommandText = "INSERT INTO UserInfoes (UserName, Password, Role) VALUES (@username, @password, @role)";
                                        insertCmd.Parameters.AddWithValue("@username", "Admin");
                                        insertCmd.Parameters.AddWithValue("@password", "2222");
                                        insertCmd.Parameters.AddWithValue("@role", (int)UserRole.Admin);
                                        insertCmd.ExecuteNonQuery();
                                    }
                                }
                            }

                            // تحميل المستخدمين في الكومبوبوكس
                            var users = new List<UserInfo>();
                            using (var selectCmd = connection.CreateCommand())
                            {
                                selectCmd.CommandText = "SELECT UserName, Password, Role FROM UserInfoes";
                                using (var reader = selectCmd.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        users.Add(new UserInfo
                                        {
                                            UserName = reader.GetString(0),
                                            Password = reader.GetString(1),
                                            Role = (UserRole)reader.GetInt32(2)
                                        });
                                    }
                                }
                            }

                            comboBoxUsers.DataSource = users;
                            comboBoxUsers.DisplayMember = "UserName";
                        }

                        success = true;
                    }
                    catch (Exception ex)
                    {
                        lastException = ex;
                        retryCount++;
                        if (retryCount < 3)
                        {
                            System.Threading.Thread.Sleep(200);
                        }
                    }
                }

                if (!success && lastException != null)
                {
                    throw lastException;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في تحميل قاعدة البيانات: {ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void buttonLogin_Click(object sender, EventArgs e)
        {
            var selectedUser = comboBoxUsers.SelectedItem as UserInfo;
            if (selectedUser == null) return;

            // التحقق من كلمة المرور (بشكل مباشر من الكائن المختار)
            if (textBoxPassword.Text == selectedUser.Password)
            {
                Program.LoggedInUserName = selectedUser.UserName;
                Program.LoggedInUserRole = selectedUser.Role;

                var main = new Customer(Program.LoggedInUserRole, Program.LoggedInUserName);
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("كلمة المرور غير صحيحة");
            }
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
