using System.IO;
using System.Net.Http;
using Kavosh.Services;
using Kavosh.Services.DTOs;
using MyCom.Class;
using Newtonsoft.Json;
//using Microsoft.AspNetCore.SignalR.Client;

namespace Kavosh.UI.Forms
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        private readonly LoginUserService _loginUserService;

        private ClsFont _clsFontBold = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, false);
        public Guid _UserLogin = default;
        public bool exit = false;
        public FrmLogin(LoginUserService loginUserService)
        {
            _loginUserService = loginUserService;
            InitializeComponent();
        }

        public void SetFonts()
        {
            _clsFontBold.ChangeFont(btnEnter, 14);
            _clsFontBold.ChangeFont(btnExit, 14);
            _clsFontBold.ChangeFont(label1, 17);
            _clsFontBold.ChangeFont(label2, 17);
            //btnExit.LookAndFeel.UseDefaultLookAndFeel = false;
            btnExit.LookAndFeel.SkinName = "WXI";
            btnEnter.LookAndFeel.SkinName = "WXI";
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //connection = new HubConnectionBuilder()
            //    .WithUrl(ClsURI.GetURI + "chathub")
            //    .Build();
            //connection.StartAsync();
            if (File.Exists("SaveUser.com2"))
            {
                StreamReader sr = new StreamReader("SaveUser.com2");
                txtUser.Text = sr.ReadLine();
                txtPass.Select();
                txtPass.Focus();
                sr.Close();
            }


            //TransparencyKey = Color.FromArgb(30, 231, 15);
            TransparencyKey = Color.DarkOliveGreen;
            SetFonts();
        }
        //HubConnection connection;

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            //connection.InvokeAsync("SendMessage", "User1", "Test SignalR");
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Wait(() =>
            //{
            //    var getDate = DateTime.Now.DateTimePersian().ShortDateTime.Replace("/", "-").Replace(":", "-");
            //    SrvBackupRestore.Backup(getDate);
            //});
            // Application.ExitThread();
            //Application.Exit();
           Application.Exit();
            //Environment.Exit(0);
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            FunLogin();
        }

        private async void FunLogin()
        {
            var getUsername = txtUser.Text;
            var getPass = txtPass.Text;

            Dictionary<bool, string> getUsers;
            getUsers = await _loginUserService.Enter(new LoginUserDto { Username = getUsername, Password = getPass });
            if (getUsers.TryGetValue(true, out var userInfo))
            {
                StreamWriter sr = new StreamWriter("SaveUser.com2");
                await sr.WriteAsync(txtUser.Text);

                sr.Close();
                this.Hide();
            }
            else if (getUsers.TryGetValue(false, out var errorMessage))
            {
                ClassMessageBox.ShowMSG("نام کاربری و رمز را بررسی کنید", Class_Text.Msg_Name, ClassMessageBox.enumIcon.بستن_مربع);
            }
            ////var getUsers = SrvPerson.GetAll().Where(w => w.GroupId == Guid.Parse("c4d26da5-1c33-46ab-aea9-83336196c354"));
            //if (getUsers.Any(a => a.Username.ToLower() == getUsername.ToLower() && a.Password == getPass))
            //{
            //    Person person = getUsers.First(a => a.Username.ToLower() == getUsername.ToLower() && a.Password == getPass);
            //    if (!person.Active)
            //    {
            //        new Class_Text().Frm_Msg("حساب شما غیر فعال می باشد.", Class_Text.Msg_Name, 4);
            //        return;
            //    }

            //    var getToken = SrvLogin.Login(new Services.Login.LoginModel(getUsername.ToLower(), getPass)).Result;


            //    Properties.Settings.Default.CurrentUsername = txtUser.Text;
            //    Properties.Settings.Default.CurrentToken = getToken;
            //    Properties.Settings.Default.Save();

            //    //SrvLogin.SetUser(person.Id.ToString());
            //    this.Hide();
            //    _UserLogin = person.Id;
            //}
            //else
            //{
            //    new Class_Text().Frm_Msg("نام کاربری و رمز را صحیح وارد نمایید", Class_Text.Msg_Name, 4);

            //    txtPass.Focus();
            //    txtPass.SelectAll();
            //    return;
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter || e.KeyData == Keys.Return) && txtUser.Text != "")
            {
                txtPass.Select();
                txtPass.Focus();
            }
        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyData == Keys.Enter || e.KeyData == Keys.Return))
            {
                if (txtUser.Text == "")
                {
                    txtUser.Select();
                    txtUser.Focus();
                }
                else if (txtPass.Text != "")
                {
                    btnEnter.Focus();
                    FunLogin();
                }

            }
        }

        #region New Login












        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }

        #endregion

        //    var loginModel = new LoginModel { Username = "admin", Password = "admin" };
        //    var token = await AuthenticateUser(loginModel);
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        MessageBox.Show("Login successful!");
        //        // ذخیره توکن یا انجام عملیات دیگر
        //    }
        //    else
        //    {
        //        MessageBox.Show("Login failed. Please check your credentials.");
        //    }
        //}

        private void btnGetUserId_Click(object sender, EventArgs e)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri($"{ClsURI.GetURI}auth/");
            //    var response = client.GetAsync("GetUserData").Result;
            //    var json = response.Content.ReadAsStringAsync().Result;
            //    var getJson = JsonConvert.DeserializeObject<IEnumerable<ListGame>>(json);

            //}



        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}