using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Todolist
{
    public partial class Form2 : Form
    {
        public Button closeButton;
        public Label uid;
        public Label pwd;
        public TextBox uidText;
        public TextBox pwdText;
        public PictureBox uidTextBg;
        public PictureBox pwdTextBg;
        public Button login;
        public Button sign;
        public int wrongCnt = 0;
        public Form2()
        {
            InitializeComponent();
            closeButton = new Button();
            closeButton.BackColor = System.Drawing.Color.Transparent;
            closeButton.BackgroundImage = Image.FromFile("closeButton.png");
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            closeButton.ForeColor = System.Drawing.Color.Transparent;
            closeButton.Margin = new System.Windows.Forms.Padding(0);
            closeButton.Size = new System.Drawing.Size(25, 25);
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Location = new Point(622, 5);
            closeButton.Click += closeButton_Click;
            closeButton.MouseHover += closeButton_MouseHover;
            closeButton.MouseLeave += closeButton_MouseLeave;
            this.Controls.Add(closeButton);

            uid = new Label();
            uid.AutoSize = true;
            uid.BackColor = System.Drawing.Color.Transparent;
            uid.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            uid.Location = new System.Drawing.Point(70, 220);
            uid.ForeColor = System.Drawing.Color.White;
            uid.TabIndex = 0;
            uid.Text = "账号：";
            this.Controls.Add(uid);

            pwd = new Label();
            pwd.AutoSize = true;
            pwd.BackColor = System.Drawing.Color.Transparent;
            pwd.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            pwd.Location = new System.Drawing.Point(70, 280);
            pwd.ForeColor = System.Drawing.Color.White;
            pwd.TabIndex = 0;
            pwd.Text = "密码：";
            this.Controls.Add(pwd);

            uidText = new TextBox();
            uidText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            uidText.AutoSize = true;
            uidText.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            uidText.Location = new Point(150, 220);
            uidText.Size = new Size(80, 20);
            uidText.Text = "";
            this.Controls.Add(uidText);

            pwdText = new TextBox();
            pwdText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            pwdText.AutoSize = true;
            pwdText.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            pwdText.Location = new Point(150, 280);
            pwdText.Size = new Size(80, 20);
            pwdText.Text = "";
            pwdText.PasswordChar = '*';
            this.Controls.Add(pwdText);

            login = new Button();
            login.BackColor = System.Drawing.Color.Transparent;
            login.BackgroundImage = Image.FromFile("login.png");
            login.FlatAppearance.BorderSize = 0;
            login.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            login.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            login.ForeColor = System.Drawing.Color.Transparent;
            login.Margin = new System.Windows.Forms.Padding(0);
            login.Size = new System.Drawing.Size(70, 30);
            login.UseVisualStyleBackColor = false;
            login.Location = new Point(75, 340);
            login.Click += login_Click;
            this.Controls.Add(login);

            sign = new Button();
            sign.BackColor = System.Drawing.Color.Transparent;
            sign.BackgroundImage = Image.FromFile("sign.png");
            sign.FlatAppearance.BorderSize = 0;
            sign.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            sign.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            sign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            sign.ForeColor = System.Drawing.Color.Transparent;
            sign.Margin = new System.Windows.Forms.Padding(0);
            sign.Size = new System.Drawing.Size(70, 30);
            sign.UseVisualStyleBackColor = false;
            sign.Location = new Point(175, 340);
            sign.Click += sign_Click;
            this.Controls.Add(sign);

            uidTextBg = new PictureBox();
            uidTextBg.Image = Image.FromFile("textbox.png");
            uidTextBg.Enabled = false;
            uidTextBg.Location = new System.Drawing.Point(145, 218);
            uidTextBg.Size = new System.Drawing.Size(100, 30);
            uidTextBg.TabIndex = 1;
            uidTextBg.TabStop = false;
            uidTextBg.BackColor = System.Drawing.Color.Transparent;
            uidTextBg.Visible = true;
            uidTextBg.SendToBack();
            this.Controls.Add(uidTextBg);

            pwdTextBg = new PictureBox();
            pwdTextBg.Image = Image.FromFile("textbox.png");
            pwdTextBg.Enabled = false;
            pwdTextBg.Location = new System.Drawing.Point(145, 278);
            pwdTextBg.Size = new System.Drawing.Size(100, 30);
            pwdTextBg.TabIndex = 1;
            pwdTextBg.TabStop = false;
            pwdTextBg.BackColor = System.Drawing.Color.Transparent;
            pwdTextBg.Visible = true;
            pwdTextBg.SendToBack();
            this.Controls.Add(pwdTextBg);
        }
        public void login_Click(object sender, EventArgs e)
        {
            string uid = uidText.Text;
            string pwd = pwdText.Text;
            if(uid == "")
            {
                MessageBox.Show("账号或密码错误！");
            }
            else
            {
                account a = new account(uid);
                if (a.password != "" && a.password == pwd)
                {
                    wrongCnt = 0;
                    // 这里是直接新建了一个和uid没关系的form，因为form里的函数没写完
                    Form1 form1 = new Form1();
                    Hide();
                    form1.ShowDialog();
                    Application.ExitThread();
                }
                else
                {
                    MessageBox.Show("账号或密码错误！");
                    wrongCnt++;
                    if(wrongCnt >= 3)
                    {
                        Application.ExitThread();
                    }
                }
            }
        }
        public void sign_Click(object sender, EventArgs e)
        {
            string uid = uidText.Text;
            string pwd = pwdText.Text;
            account a = new account(uid);
            if(a.password == "")
            {
                a.set_password(pwd);
                MessageBox.Show("注册成功！");
            }
            else
            {
                MessageBox.Show("用户名已使用！");
            }
        }
        public void closeButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void closeButton_MouseHover(object sender, EventArgs e)
        {
            closeButton.BackgroundImage = Image.FromFile("close_mousedown.png");
        }
        public void closeButton_MouseLeave(object sender, EventArgs e)
        {
            closeButton.BackgroundImage = Image.FromFile("closeButton.png");
        }

        // 用来使得整个窗体都可以被拖动的代码
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x0112, 0xF012, 0);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            frmMain_MouseDown(this, e);
        }
    }
}
