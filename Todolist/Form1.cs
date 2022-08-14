using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Todolist
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 圆角边框
            //this.BackColor = ColorTranslator.FromHtml("#F7F1F1");
            //this.TransparencyKey = ColorTranslator.FromHtml("#F7F1F1");
        }

        // 对于窗体界面，有 data 数据类
        // 以及 todolist 和 calendar 两个类
        public Data data;
        public Todolist todolist;
        public Calendar calendar;
        public string uid = "18000";

        public Button closeButton;
        //public PictureBox girl33;
        //public PictureBox girl22;

        // 加载表格时，创建数据和两个类
        private void Form1_Load(object sender, EventArgs e)
        {
            data = new Data();
            todolist = new Todolist(this);
            calendar = new Calendar(this);
            this.data.CreateUser(this.uid, "12341234");

            // 加载关闭界面的按钮
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

            /*
            // 感觉这里的图有点违和
            // 加载33娘
            girl33 = new PictureBox();
            girl33.Image = Image.FromFile("33.png");
            girl33.Location = new System.Drawing.Point(40, 10);
            girl33.Size = new System.Drawing.Size(50, 50);
            girl33.TabIndex = 1;
            girl33.TabStop = false;
            girl33.BackColor = System.Drawing.Color.Transparent;
            girl33.Visible = true;
            girl33.SendToBack();
            this.Controls.Add(girl33);

            // 加载22娘
            girl22 = new PictureBox();
            girl22.Image = Image.FromFile("22.png");
            girl22.Location = new System.Drawing.Point(475, 10);
            girl22.Size = new System.Drawing.Size(50, 50);
            girl22.TabIndex = 1;
            girl22.TabStop = false;
            girl22.BackColor = System.Drawing.Color.Transparent;
            girl22.Visible = true;
            girl22.SendToBack();
            this.Controls.Add(girl22);
            */
        }

        // 几个用来控制关闭按钮的函数
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
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
            this.todolist.EditFinished();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            frmMain_MouseDown(this, e);
        }
    }

    // 用于在程序中层结构和底层结构之间传递信息的类
    public class ThingInfo
    {
        // 传递的信息包括：
        // 用户信息
        public string uid, tid;

        // 事件信息
        public int Year, Month, Day;
        public string Content;

        // 以及该项在 Todolist 的第几个
        public int listid; // 该信息对数据库而言没有用 

        public ThingInfo(string uid, string tid, int Year, int Month, int Day,
            string Content, int listid = 0)
        {
            this.uid = uid;
            this.tid = tid;
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
            this.Content = Content;
            this.listid = listid;
        }
    }

    // 左侧的 Todolist 类
    public class Todolist
    {
        // 在这个代码中，所有类都会存一个 form
        // 从而，所有类都可以借助 form 的索引找到任何一个其他的对象（如按钮）
        public Form1 form;
        // 往 Todolist 里添加元素的按钮
        public Button Plusbutton;
        // 用于存放每条信息的列表
        List<Item> ItemList;
        // 左侧的“Todolist”字样
        public Label name;
        public PictureBox girl2233;
        // 创建 Todolist
        public Todolist(Form1 form)
        {
            this.form = form;

            name = new Label();
            name.AutoSize = true;
            name.BackColor = System.Drawing.Color.Transparent;
            name.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            name.Location = new System.Drawing.Point(70, 28);
            name.Size = new System.Drawing.Size(41, 15);
            name.ForeColor = System.Drawing.Color.White;
            name.TabIndex = 0;
            name.Text = "TodoList";
            form.Controls.Add(name);

            // 图片
            girl2233 = new PictureBox();
            girl2233.Image = Image.FromFile("2233_1.png");
            girl2233.Enabled = false;
            girl2233.Location = new System.Drawing.Point(60, 330);
            girl2233.Size = new System.Drawing.Size(150, 150);
            girl2233.TabIndex = 1;
            girl2233.TabStop = false;
            girl2233.BackColor = System.Drawing.Color.Transparent;
            girl2233.Visible = true;
            girl2233.SendToBack();
            form.Controls.Add(girl2233);

            // 创建 TodoList 的时候，刷新界面
            Refresh();
        }
        // Todolist 的每行用一个 Item 类维护
        public class Item
        {
            public Form1 form; // 在这个代码中，所有类都会存一个 form
            public int x, y; // 这行的项开始的位置

            // 左侧的圆圈（叉按钮，按一下就会把这一项从列表中删除）
            public Button Crossbutton;

            // 时间、内容，各有一个Label和一个Textbox
            // 点击 Label 会变成 TextBox，点击外面 TextBox 会变成 Label
            public Label TimeLabel, ContentLabel;
            public TextBox TimeTextbox, ContentTextbox;
            
            // 初始化一个列表项
            // ThingInfo 类里包含了这个列表项需要用到的所有信息
            // 其也被用于和数据库交互
            public Item(Form1 form, int x, int y, ThingInfo thingindex)
            {
                this.x = x;
                this.y = y;
                this.form = form;

                // 以下部分设置了每个按钮的位置、大小、文字内容
                // thingindex.listid 是这个元素在列表的第几项
                // 这是为了方便在 click 事件处理中找到它

                #region label
                TimeLabel = new Label();
                TimeLabel.BackColor = System.Drawing.Color.Transparent;
                TimeLabel.AutoSize = true;
                TimeLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                TimeLabel.Location = new Point(x + 30, y + 2);
                TimeLabel.Text = thingindex.Month + "月" + thingindex.Day + "日";
                TimeLabel.Size = new Size(60, 20);
                TimeLabel.Tag = thingindex;
                TimeLabel.Click += TimeLabel_Click;
                form.Controls.Add(TimeLabel); // 所有东西都要加到 form 上去

                ContentLabel = new Label();
                ContentLabel.BackColor = System.Drawing.Color.Transparent;
                ContentLabel.AutoSize = true;
                ContentLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                ContentLabel.Location = new Point(x + 90, y + 2);
                ContentLabel.Text = thingindex.Content;
                ContentLabel.Size = new Size(60, 20);
                ContentLabel.Tag = thingindex;
                ContentLabel.Click += ContentLabel_Click;
                form.Controls.Add(ContentLabel);

                TimeTextbox = new TextBox();
                TimeTextbox.AutoSize = true;
                TimeTextbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                TimeTextbox.Location = new Point(x + 30, y);
                TimeTextbox.Size = new Size(60, 20);
                TimeTextbox.Text = thingindex.Month + "月" + thingindex.Day + "日";
                TimeTextbox.Tag = thingindex;
                TimeTextbox.Visible = false;
                form.Controls.Add(TimeTextbox);

                ContentTextbox = new TextBox();
                ContentTextbox.AutoSize = true;
                ContentTextbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                ContentTextbox.Location = new Point(x + 90, y);
                ContentTextbox.Size = new Size(60, 20);
                ContentTextbox.Text = thingindex.Content;
                ContentTextbox.Tag = thingindex;
                ContentTextbox.Visible = false;
                form.Controls.Add(ContentTextbox);
                #endregion

                Crossbutton = new Button();
                Crossbutton.BackColor = System.Drawing.Color.Transparent;
                Crossbutton.Image = Image.FromFile("CrossButton.png");
                Crossbutton.FlatAppearance.BorderSize = 0;
                Crossbutton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
                Crossbutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                Crossbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                Crossbutton.ForeColor = System.Drawing.Color.Transparent;
                Crossbutton.Margin = new System.Windows.Forms.Padding(0);
                Crossbutton.Size = new System.Drawing.Size(26, 26);
                Crossbutton.UseVisualStyleBackColor = false;
                Crossbutton.Location = new Point(x, y);
                Crossbutton.Tag = thingindex;
                form.Controls.Add(Crossbutton);
                Crossbutton.Click += Crossbutton_Click;
            }
            // 如果点击了按钮外部的窗体，程序会调用每个 Item 的 Edit 函数
            // Edit 负责把每个按钮的信息更新到数据库里
            public void Edit(int listid)
            {
                Item NowItem = form.todolist.ItemList[listid];
                // 从汉字日期中切分出年月日
                DateTime date;
                try
                {
                    date = Convert.ToDateTime(NowItem.TimeTextbox.Text);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                int Year = date.Year;
                int Month = date.Month;
                int Day = date.Day;

                // 修改数据库数据
                ThingInfo thinginfo = form.todolist.ItemList[listid].TimeLabel.Tag as ThingInfo;
                form.data.Edit(new ThingInfo(
                    thinginfo.uid, thinginfo.tid,
                    Year, Month, Day, NowItem.ContentTextbox.Text, 0));

                // 拷贝 Textbox 的信息给 label
                NowItem.TimeLabel.Text = NowItem.TimeTextbox.Text;
                NowItem.ContentLabel.Text = NowItem.ContentTextbox.Text;
            }

            // 点击 Crossbutton 时，
            // 找到对应项从数据库中删除，并刷新两个界面
            public void Crossbutton_Click(object sender, EventArgs e)
            {
                Button btn = sender as Button;
                ThingInfo thinginfo = (ThingInfo)btn.Tag;
                int x = thinginfo.listid;

                btn.Image = Image.FromFile("CrossButton_mousedown.png");
                form.todolist.ItemList[x].TimeLabel.Font = new Font(TimeLabel.Font, FontStyle.Strikeout);
                form.todolist.ItemList[x].ContentLabel.Font = new Font(ContentLabel.Font, FontStyle.Strikeout);
                form.todolist.girl2233.Image = Image.FromFile("2233_2.png");
                //form.todolist.girl2233.Enabled = true;
                form.Refresh();
                System.Threading.Thread.Sleep(50);
                form.todolist.girl2233.Image = Image.FromFile("2233_1.png");
                form.data.Remove(thinginfo.uid, thinginfo.tid);
                form.todolist.Refresh();
                form.calendar.Refresh();
                form.todolist.girl2233.Enabled = false;
            }

            // 点击时间时，Label 转为 Textbox，可以修改时间和内容
            public void TimeLabel_Click(object sender, EventArgs e)
            {
                Label label = sender as Label;
                ThingInfo thinginfo = (ThingInfo)label.Tag;
                int x = thinginfo.listid;

                // 隐藏 Label，显示 TextBox，并拷贝文字内容
                Item NowItem = form.todolist.ItemList[x];
                NowItem.TimeLabel.Visible = false;
                NowItem.ContentLabel.Visible = false;
                NowItem.TimeTextbox.Visible = true;
                NowItem.ContentTextbox.Visible = true;
                NowItem.TimeTextbox.Text = NowItem.TimeLabel.Text;
                NowItem.ContentTextbox.Text = NowItem.ContentLabel.Text;
            }

            // 同理，点击文字内容时，也可以修改两者
            public void ContentLabel_Click(object sender, EventArgs e)
            {
                Label label = sender as Label;
                ThingInfo thinginfo = (ThingInfo)label.Tag;
                int x = thinginfo.listid;

                // 隐藏 Label，显示 TextBox，并拷贝文字内容
                Item NowItem = form.todolist.ItemList[x];
                NowItem.TimeLabel.Visible = false;
                NowItem.ContentLabel.Visible = false;
                NowItem.TimeTextbox.Visible = true;
                NowItem.ContentTextbox.Visible = true;
                NowItem.TimeTextbox.Text = NowItem.TimeLabel.Text;
                NowItem.ContentTextbox.Text = NowItem.ContentLabel.Text;
            }
        };

        // 如果点击按钮/标签外部的窗体，就会调用这个函数
        // 它则会逐个标签进行排查，修改对应的数据项
        public void EditFinished()
        {
            for (int i = 0; i < ItemList.Count; i++)
                ItemList[i].Edit(i);
            form.todolist.Refresh();
            form.calendar.Refresh();
        }

        // 在刷新前，删除旧的按钮等对象
        // 我们这里的“删除”是假的删除，只是令其不可见而已
        // 这是为了防止在 click 函数调用的过程中把按钮删了
        void RemoveOld()
        {
            if (ItemList == null) return;

            foreach (Item item in ItemList)
            {
                item.TimeLabel.Visible = false;
                item.ContentLabel.Visible = false;
                //item.Checkbutton.Visible = false;
                //item.Editbutton.Visible = false;
                item.Crossbutton.Visible = false;
                item.TimeTextbox.Visible = false;
                item.ContentTextbox.Visible = false;
            }
            if (Plusbutton != null)
                Plusbutton.Visible = false;
        }

        // 刷新 Todolist 界面
        public void Refresh()
        {
            // 首先“删除”旧的按钮
            RemoveOld();
            List<ThingInfo> things = form.data.GetSortedThings(form.uid);

            // 重新确认一遍数据库中有几个元素
            int ItemCount = things.Count;

            // 新建立一个 Item 列表
            ItemList = new List<Item>();
            for (int i = 0; i < ItemCount; i++)
            {
                ThingInfo thing = things[i];
                Item item = new Item(form, 20, 60 + i * 30,
                    new ThingInfo(thing.uid, thing.tid,
                        thing.Year, thing.Month, thing.Day, thing.Content, i)
                    );

                // 往这个列表里添加进 new 出来的一批按钮
                ItemList.Add(item);
            }

            // 在最后，显示一个“+”按钮
            Plusbutton = new Button();
            Plusbutton.BackColor = System.Drawing.Color.Transparent;
            Plusbutton.BackgroundImage = Image.FromFile("Plusbutton.png");
            Plusbutton.FlatAppearance.BorderSize = 0;
            Plusbutton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            Plusbutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            Plusbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            Plusbutton.ForeColor = System.Drawing.Color.Transparent;
            Plusbutton.Margin = new System.Windows.Forms.Padding(0);
            Plusbutton.Size = new System.Drawing.Size(25, 25);
            Plusbutton.UseVisualStyleBackColor = false;
            Plusbutton.Location = new Point(20, 60 + ItemCount * 30);
            Plusbutton.Size = new Size(195, 20);
            Plusbutton.Text = "+";
            Plusbutton.Click += Plusbutton_Click;
            form.Controls.Add(Plusbutton);
        }
        void Plusbutton_Click(object sender, EventArgs e)
        {
            form.data.Add(new ThingInfo(form.uid, form.data.NextTID(form.uid),
                            form.calendar.YearDisplayed,
                            form.calendar.MonthDisplayed, 
                            form.calendar.DaysInMonth, "新任务", 0));
            form.todolist.Refresh();
            form.calendar.Refresh();
        }
    }
    // 右侧的 Calendar 类
    public class Calendar
    {
        Form1 form;
        // 几年几月的 Label
        Label TitleLabel;
        // 星期一到星期日的 Label
        List<Label> DayLabelList;
        // 每个事件的 Label
        List<Item> ItemLabelList;
        // 标题 Calender 字样的 Label
        Label name;
        // “上个月”的 Label 和“下个月”的 Label
        Label LastMonthLabel, NextMonthLabel;

        // 当前展示的是第几个月
        public int YearDisplayed, MonthDisplayed;

        class Item
        {
            public Form1 form;

            // 这里我们只需要用于记录事件和内容的 Label
            public Label TimeLabel, ContentLabel;

            // 有一个“展示更多”的按钮
            public Button ShowMoreButton;

            public Item(Form1 form, int x, int y,
                string time, string content, int count)
            {
                this.form = form;

                // 生成各个 Label
                TimeLabel = new Label();
                TimeLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                TimeLabel.BackColor = System.Drawing.Color.White;
                TimeLabel.Location = new Point(x, y);
                TimeLabel.Size = new Size(50, 20);
                TimeLabel.Text = time;
                form.Controls.Add(TimeLabel);

                ContentLabel = new Label();
                ContentLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                ContentLabel.BackColor = System.Drawing.Color.White;
                ContentLabel.Location = new Point(x, y + 20);
                ContentLabel.Size = new Size(50, 20);
                ContentLabel.Text = content;
                form.Controls.Add(ContentLabel);

                //“...”按钮
                ShowMoreButton = new Button();
                ShowMoreButton.BackColor = System.Drawing.Color.White;
                ShowMoreButton.BackgroundImage = Image.FromFile("...button.png");
                ShowMoreButton.FlatAppearance.BorderSize = 0;
                ShowMoreButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
                ShowMoreButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
                ShowMoreButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                ShowMoreButton.ForeColor = System.Drawing.Color.Transparent;
                ShowMoreButton.Margin = new System.Windows.Forms.Padding(0);
                ShowMoreButton.Size = new System.Drawing.Size(25, 25);
                ShowMoreButton.UseVisualStyleBackColor = false;
                ShowMoreButton.Location = new Point(x, y + 40);
                ShowMoreButton.Size = new Size(50, 20);
                
                // 只有事情数量大于等于2件，才显示“...”按钮
                if (count <= 1) ShowMoreButton.Visible = false;
                form.Controls.Add(ShowMoreButton);
            }

        }

        string[] DigitToChinese = { "一", "二", "三", "四", "五", "六", "日"};
        public Calendar(Form1 form)
        {
            this.form = form;
            YearDisplayed = 2021;
            MonthDisplayed = 1;
            
            name = new Label();
            name.AutoSize = true;
            name.BackColor = System.Drawing.Color.White;
            name.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            name.Location = new System.Drawing.Point(385, 28);
            name.Size = new System.Drawing.Size(41, 15);
            name.ForeColor = Color.FromArgb(100, 150, 255);
            name.TabIndex = 0;
            name.Text = "Calendar";
            form.Controls.Add(name);

            LastMonthLabel = new Label();
            LastMonthLabel.BackColor = System.Drawing.Color.Transparent;
            LastMonthLabel.AutoSize = true;
            LastMonthLabel.Location = new Point(375, 52);
            LastMonthLabel.Text = "←";
            LastMonthLabel.Size = new Size(30, 30);
            LastMonthLabel.Click += LastMonthLabel_Click;
            form.Controls.Add(LastMonthLabel);

            NextMonthLabel = new Label();
            NextMonthLabel.BackColor = System.Drawing.Color.Transparent;
            NextMonthLabel.AutoSize = true;
            NextMonthLabel.Location = new Point(470, 52);
            NextMonthLabel.Text = "→";
            NextMonthLabel.Size = new Size(30, 30);
            NextMonthLabel.Click += NextMonthLabel_Click;
            form.Controls.Add(NextMonthLabel);

            Refresh();
        }

        // “上个月”按钮点击的事件和“下个月”的事件
        public void LastMonthLabel_Click(object sender, EventArgs e)
        {
            DateTime datetime = Convert.ToDateTime(
                YearDisplayed + "-"
                + MonthDisplayed + "-1");
            datetime = datetime.AddMonths(-1);
            YearDisplayed = datetime.Year;
            MonthDisplayed = datetime.Month;
            form.calendar.Refresh();
        }

        public void NextMonthLabel_Click(object sender, EventArgs e)
        {
            DateTime datetime = Convert.ToDateTime(
                YearDisplayed + "-"
                + MonthDisplayed + "-1");
            datetime = datetime.AddMonths(1);
            YearDisplayed = datetime.Year;
            MonthDisplayed = datetime.Month;
            form.calendar.Refresh();
        }

        // 同样也是假的删除。实际上是隐藏
        public void RemoveOld()
        {
            if (TitleLabel != null)
                TitleLabel.Visible = false;
            if (DayLabelList != null)
                foreach (Label label in DayLabelList)
                    label.Visible = false;
            if (ItemLabelList != null)
                foreach (Item item in ItemLabelList)
                {
                    item.TimeLabel.Visible = false;
                    item.ContentLabel.Visible = false;
                    item.ShowMoreButton.Visible = false;
                }

        }

        // CalculatePosition 负责计算出当前显示的（年，月）
        // 的第一天到最后一天分别位于最终日历的第几行第几列
        int[] PosXofDay;
        int[] PosYofDay;
        public int DaysInMonth;
        void CalculatePosition()
        {
            int year = YearDisplayed;
            int month = MonthDisplayed;
            
            // 计算这个月的第一天是星期几，记为 Num
            DateTime day = Convert.ToDateTime(
                year + "-" + month + "-1");
            string DayOfWeek = day.DayOfWeek.ToString();
            
            Dictionary<string, int> Transform = new Dictionary<string, int>();
            Transform["Monday"] = 1;
            Transform["Tuesday"] = 2;
            Transform["Wednesday"] = 3;
            Transform["Thursday"] = 4;
            Transform["Friday"] = 5;
            Transform["Saturday"] = 6;
            Transform["Sunday"] = 7;

            int Num = Transform[DayOfWeek];

            // 从第一天开始，逐天推出每天应该放在第几行，第几列
            // 第 i 天所应该在的（行，列）记为 (PosX[i]，PosY[i])
            DaysInMonth = DateTime.DaysInMonth(year, month);
            PosXofDay = new int[40];
            PosYofDay = new int[40];
            int x = Num, y = 1;
            for (int i = 1; i <= DaysInMonth; i++)
            {
                PosXofDay[i] = x;
                PosYofDay[i] = y;
                x = x + 1;
                if (x == 8)
                {
                    x = 1;
                    y = y + 1;
                }
            }
        }

        // 一个用于从所有事件中筛选出某一天的事件的函数
        public List<ThingInfo> FindthingsByDay(List<ThingInfo> things, int Year, int Month, int Day)
        {
            List<ThingInfo> results = new List<ThingInfo>();
            foreach (ThingInfo thing in things)
            {
                if (thing.Year == Year && thing.Month == Month && thing.Day == Day)
                    results.Add(thing);
            }
            return results;
        }

        // 刷新 Calendar
        public void Refresh()
        {
            // Console.WriteLine("Year: " + YearDisplayed);
            // Console.WriteLine("Month: " + MonthDisplayed);
            RemoveOld();

            // 重设“xx年xx月”的标题
            TitleLabel = new Label();
            TitleLabel.BackColor = System.Drawing.Color.White;
            TitleLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            TitleLabel.Text = YearDisplayed + "年" + MonthDisplayed + "月";
            TitleLabel.Location = new Point(395, 50);
            TitleLabel.Size = new Size(90, 20);
            form.Controls.Add(TitleLabel);
            
            // 重新创建“星期一”到“星期日”的 Label
            DayLabelList = new List<Label>();
            for (int i = 0; i < 7; i++)
            {
                Label label = new Label();
                label.BackColor = System.Drawing.Color.White;
                label.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                label.Text = "星期" + DigitToChinese[i];
                label.Location = new Point(250 + 50 * i, 70);
                label.Size = new Size(50, 20);
                DayLabelList.Add(label);
                form.Controls.Add(label);
            }

            // 计算所有日期应该出现在第几行第几列
            CalculatePosition();

            // 从数据库里，根据 userid 获得这个用户的所有事件（按时间排序）
            ItemLabelList = new List<Item>();
            List<ThingInfo> things = form.data.GetSortedThings(form.uid);

            // 枚举这个月的每一天
            for (int i = 1; i <= DaysInMonth; i++)
            {
                // 筛选这一年这一个月这一天的事件
                List<ThingInfo> Todaythings = FindthingsByDay(things, YearDisplayed, MonthDisplayed, i);
                string content;
                if (Todaythings.Count > 0)
                {
                    content = Todaythings[0].Content;
                }
                else content = ""; // 如果没事件，就空着

                // 展示的时候，如果事件多于2件，显示“...”按钮
                Item item = new Item(form,
                    200 + PosXofDay[i] * 50,
                    40 + PosYofDay[i] * 60,
                    i + "日",
                    //MonthDisplayed + "月" + i + "日",
                    content,
                    Todaythings.Count
                );
                ItemLabelList.Add(item);
            }
        }
    }
}
