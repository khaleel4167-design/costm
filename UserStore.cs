using System.Collections.Generic;

namespace Customer
{
    // نوع المستخدم (أنت أصلاً معرفه في مكان آخر، لو موجود لا تكرره)
    

    // كلاس يمثل حساب مستخدم واحد
    public class UserAccount
    {
        public string UserName { get; set; }   // اسم المستخدم اللي يظهر في الكومبو بوكس
        public string Password { get; set; }   // الرمز/كلمة المرور
        public UserRole Role { get; set; }  // Admin أو Employee
    }

    // مخزن المستخدمين (ثابت ومتاح لكل المشروع)
    public static class UserStore
    {
        //public static List<UserAccount> Users { get; } = new List<UserAccount>
        //{
        //    new UserAccount { UserName = "Admin",     Password = "1234", Role = UserRole.Admin    },
        //    new UserAccount { UserName = "Employee1", Password = "1111", Role = UserRole.Employee },
        //    new UserAccount { UserName = "Employee2", Password = "2222", Role = UserRole.Employee }
        //};
    }
}
