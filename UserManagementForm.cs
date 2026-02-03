using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Customer
{
    public partial class UserManagementForm : Form
    {
        private BindingList<UserAccount> _bindingUsers;

        private bool VerifyAdminPassword()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "أدخل رمز الأدمن للسماح بتنفيذ العملية:",
                "تأكيد الأدمن",
                "",
                -1, -1);

            if (string.IsNullOrEmpty(input))
                return false;

            // نحضر حساب الأدمن الحقيقي من قاعدة البيانات
            using (var db = new AppDbContext())
            {
                var admin = db.Users.FirstOrDefault(u => u.Role == UserRole.Admin);

                if (admin == null)
                {
                    MessageBox.Show("لا يوجد حساب أدمن مسجل.", "خطأ",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (input != admin.Password)
                {
                    MessageBox.Show("رمز الأدمن غير صحيح.", "رفض",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        public UserManagementForm()
        {
            InitializeComponent();
            SetupGrid();
            LoadUsers();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            // تكبير الصفوف
            dataGridViewUsers.RowTemplate.Height = 70;

            // تكبير رؤوس الأعمدة
            dataGridViewUsers.ColumnHeadersHeight = 60;

            // تكبير الخط
            dataGridViewUsers.DefaultCellStyle.Font = new Font("Segoe UI", 16F, FontStyle.Regular);
            dataGridViewUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            // مساحة داخل الخلية
            dataGridViewUsers.DefaultCellStyle.Padding = new Padding(5);

            // ملء الشاشة
            dataGridViewUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // تحسين اللمس
            dataGridViewUsers.ScrollBars = ScrollBars.Both;
        }

        private void SetupGrid()
        {
            dataGridViewUsers.RowTemplate.Height = 100;
            dataGridViewUsers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridViewUsers.DefaultCellStyle.Padding = new Padding(10);

            dataGridViewUsers.AutoGenerateColumns = false;
            dataGridViewUsers.AllowUserToAddRows = true;
            dataGridViewUsers.AllowUserToDeleteRows = true;
            dataGridViewUsers.RowHeadersVisible = false;

            // عمود اسم المستخدم
            var colUserName = new DataGridViewTextBoxColumn
            {
                Name = "UserNameColumn",
                DataPropertyName = "UserName",
                HeaderText = "اسم المستخدم",
                Width = 150
            };

            // عمود كلمة المرور
            var colPassword = new DataGridViewTextBoxColumn
            {
                Name = "PasswordColumn",
                DataPropertyName = "Password",
                HeaderText = "كلمة المرور",
                Width = 120
            };

            // عمود الدور (ComboBox)
            var colRole = new DataGridViewComboBoxColumn
            {
                Name = "RoleColumn",
                DataPropertyName = "Role",
                DataSource = Enum.GetValues(typeof(UserRole)),
                HeaderText = "الدور",
                Width = 100
            };

            dataGridViewUsers.Columns.Add(colUserName);
            dataGridViewUsers.Columns.Add(colPassword);
            dataGridViewUsers.Columns.Add(colRole);
        }

        private void LoadUsers()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // جلب قائمة المستخدمين من قاعدة البيانات
                    var usersList = db.Users.ToList();

                    // تحويل البيانات إلى BindingList
                    _bindingUsers = new BindingList<UserAccount>(
                        usersList.Select(u => new UserAccount
                        {
                            UserName = u.UserName,
                            Password = u.Password,
                            Role = u.Role
                        }).ToList()
                    );

                    // ربط القائمة بالـ DataGridView
                    dataGridViewUsers.DataSource = _bindingUsers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ أثناء تحميل المستخدمين: " + ex.Message);
            }
        }

        private void buttonSaveUsers_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewUsers.EndEdit();

                using (var db = new AppDbContext())
                {
                    // جلب جميع المستخدمين الحاليين من قاعدة البيانات
                    var existingUsers = db.Users.ToList();

                    // حذف المستخدمين الذين تم حذفهم من الجدول
                    var currentUserNames = _bindingUsers.Select(u => u.UserName).ToList();
                    var usersToDelete = existingUsers.Where(u => !currentUserNames.Contains(u.UserName)).ToList();

                    foreach (var userToDelete in usersToDelete)
                    {
                        db.Users.Remove(userToDelete);
                    }

                    // إضافة أو تحديث المستخدمين
                    foreach (var user in _bindingUsers)
                    {
                        if (string.IsNullOrWhiteSpace(user.UserName))
                            continue;

                        var existingUser = db.Users.Find(user.UserName);

                        if (existingUser == null)
                        {
                            // مستخدم جديد
                            db.Users.Add(new UserInfo
                            {
                                UserName = user.UserName,
                                Password = user.Password ?? "",
                                Role = user.Role
                            });
                        }
                        else
                        {
                            // تحديث مستخدم موجود
                            existingUser.Password = user.Password ?? "";
                            existingUser.Role = user.Role;
                        }
                    }

                    db.SaveChanges();

                    // تحديث AppDataStore
                    AppDataStore.Current.Users = db.Users.ToList();
                }

                MessageBox.Show("تم حفظ المستخدمين بنجاح في قاعدة البيانات.",
                    "تم", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الحفظ: {ex.Message}",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSaveUsers_Click_1(object sender, EventArgs e)
        {
            buttonSaveUsers_Click(sender, e);
        }

        private void buttonClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            // التحقق من رمز الأدمن قبل الحذف
            if (!VerifyAdminPassword())
                return;

            // جلب الصف المحدد
            var row = dataGridViewUsers.CurrentRow;
            if (row == null || row.IsNewRow)
            {
                MessageBox.Show("الرجاء اختيار مستخدم لحذفه.", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedUser = row.Cells["UserNameColumn"].Value?.ToString();

            if (string.IsNullOrEmpty(selectedUser))
                return;

            // منع حذف الأدمن
            var userInfo = _bindingUsers.FirstOrDefault(u => u.UserName == selectedUser);
            if (userInfo == null) return;

            if (userInfo.Role == UserRole.Admin)
            {
                MessageBox.Show("لا يمكن حذف حساب الأدمن.", "مرفوض",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // منع حذف المستخدم الحالي
            if (selectedUser == Program.LoggedInUserName)
            {
                MessageBox.Show("لا يمكنك حذف حسابك أثناء تسجيل الدخول.", "مرفوض",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // تأكيد الحذف
            var confirm = MessageBox.Show(
                $"هل تريد حذف المستخدم:\n\n {selectedUser} ؟",
                "تأكيد الحذف",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                // حذف من قاعدة البيانات
                using (var db = new AppDbContext())
                {
                    var userToDelete = db.Users.Find(selectedUser);
                    if (userToDelete != null)
                    {
                        db.Users.Remove(userToDelete);
                        db.SaveChanges();
                    }
                }

                // حذف من الجدول
                _bindingUsers.Remove(userInfo);

                // تحديث AppDataStore
                AppDataStore.Current.Users = _bindingUsers.Select(u => new UserInfo
                {
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role
                }).ToList();

                MessageBox.Show("تم حذف المستخدم بنجاح من قاعدة البيانات.", "تم",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطأ في الحذف: {ex.Message}",
                    "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}