using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;

namespace Todolist
{
    public class account
    {

        public string[] info_table = { "0", "123456", "Jack", "boy", "2000", "1", "1", "yes", "yes", "12", "1", "root" };
        //basic info
        public string ID = "0";
        public string password = "";
        public string nickname = "jack";
        public string sex = "boy";
        public string birth_year = "2000";
        public string birth_month = "1";
        public string birth_day = "1";
        public string alarm = "yes";
        public string delete_delayed = "yes";
        public string _12_or_24_ = "24";
        public string style = "1";
        public string father = "root";
        public account(string uID)
        {
            info_table[0] = uID;
            this.ID = uID;
            string filePath = "database/" + "userinfo.csv";
            string strline = "";
            bool if_exit = false;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] == uID)
                {
                    if_exit = true;
                    info_table = strline.Split(',');
                    this.ID = info_table[0];
                    this.password = info_table[1];
                    this.nickname = info_table[2];
                    this.sex = info_table[3];
                    this.birth_year = info_table[4];
                    this.birth_month = info_table[5];
                    this.birth_day = info_table[6];
                    this.alarm = info_table[7];
                    this.delete_delayed = info_table[8];
                    this._12_or_24_ = info_table[9];
                    this.style = info_table[10];
                    this.father = temp[11];
                    break;
                }
            }
            sr.Close();
            fs.Close();
            //若该用户不存在，则新建该用户项和对应的数据文件
            if (!if_exit)
            {

                fs = new FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(this.ID + "," + this.password + "," + this.nickname + "," + this.sex + "," + this.birth_year + "," + this.birth_month + "," + this.birth_day + "," + this.alarm + "," + this.delete_delayed + "," + this._12_or_24_ + "," + this.style + "," + this.father);
                sw.Close();
                fs.Close();
                filePath = "database/" + uID + "_data.csv";
                FileInfo fi = new FileInfo(filePath);

                fi.Directory.Create();

                fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine("things_ID,thing_to_do,level,12_or_24,deadline_year,deadline_month,deadline_day,deadline_hour,deadline_minute,alarm,alarm_num,finished,delayed,emergency,son_num");
                sw.Close();
                fs.Close();
            }
        }


        public string get_birth()
        {
            return this.birth_year + "_" + this.birth_month + "_" + this.birth_day;
        }

        public void change_info()
        {
            string filePath = "database/" + "userinfo.csv";
            string[] strline = null;
            string[] temp = null;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            strline = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < strline.Length; i++)
            {
                temp = strline[i].Split(',');
                if (temp[0] == this.ID)
                {
                    strline[i] = this.ID + "," + this.password + "," + this.nickname + "," + this.sex + "," + this.birth_year + "," + this.birth_month + "," + this.birth_day + "," + this.alarm + "," + this.delete_delayed + "," + this._12_or_24_ + "," + this.style + "," + this.father;
                }
            }
            sr.Close();
            fs.Close();
            fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            for (int k = 0; k < strline.Length; k++)
            {
                if (strline[k] != "")
                {
                    sw.WriteLine(strline[k]);
                }
            }
            sw.Close();
            fs.Close();
        }

        public void set_password(string s)
        {
            this.password = s;
            this.change_info();
        }
        public void set_nickname(string s)
        {
            this.nickname = s;
            this.change_info();
        }
        public void set_sex(string s)
        {
            this.sex = s;
            this.change_info();
        }
        public void set_birth_year(string s)
        {
            this.birth_year = s;
            this.change_info();
        }
        public void set_birth_month(string s)
        {
            this.birth_month = s;
            this.change_info();
        }
        public void set_birth_day(string s)
        {
            this.birth_day = s;
            this.change_info();
        }
        public void set_alarm(string s)
        {
            this.alarm = s;
            this.change_info();
        }
        public void set_delete_delayed(string s)
        {
            this.delete_delayed = s;
            this.change_info();
        }
        public void set_12_or_24(string s)
        {
            this._12_or_24_ = s;
            this.change_info();
        }
        public void set_style(string s)
        {
            this.style = s;
            this.change_info();
        }

        //返回ID为tID的事件
        public thing get_things(string tID)
        {
            string filePath = "database/" + this.ID + "_data.csv";
            string strline = "";
            bool if_exit = false;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] == tID)
                {
                    if_exit = true;
                    break;
                }
            }
            sr.Close();
            fs.Close();
            if (if_exit) return new thing(this.ID, tID);
            else return null;
        }

        public thing[] get_things()
        {
            string filePath = "database/" + this.ID + "_data.csv";
            string strline = "";
            List<thing> thinglist = new List<thing>();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] != "")
                {
                    thinglist.Add(new thing(this.ID, temp[0]));
                }
            }
            sr.Close();
            fs.Close();
            thing[] things = thinglist.ToArray();
            return things;
        }

    }



    //构造:thing(string uID, int tID),处理用户uID的第tID个事件
    public class thing
    {
        public string user_ID = "";
        public string things_ID = "";
        public string thing_to_do = "homework";
        public string level = "1";
        public string _12_or_24 = "24";
        public string deadline_year = "2035";
        public string deadline_month = "1";
        public string deadline_day = "1";
        public string deadline_hour = "23";
        public string deadline_minute = "59";
        public string alarm = "yes";
        public string alarm_num = "0";
        public string finished = "no";
        public string delayed = "no";
        public string emergency = "no";
        public string son_num = "0";
        public thing(string uID, string tID)
        {
            this.user_ID = uID;
            this.things_ID = tID;
            string filePath = "database/" + uID + "_data.csv";
            string strline = "";
            bool if_exit = false;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] == tID)
                {
                    if_exit = true;
                    this.thing_to_do = temp[1];
                    this.level = temp[2];
                    this._12_or_24 = temp[3];
                    this.deadline_year = temp[4];
                    this.deadline_month = temp[5];
                    this.deadline_day = temp[6];
                    this.deadline_hour = temp[7];
                    this.deadline_minute = temp[8];
                    this.alarm = temp[9];
                    this.alarm_num = temp[10];
                    this.finished = temp[11];
                    this.delayed = temp[12];
                    this.emergency = temp[13];
                    this.son_num = temp[14];
                    break;
                }
            }
            sr.Close();
            fs.Close();
            if (!if_exit)
            {

                fs = new FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(this.things_ID + "," + this.thing_to_do + "," + this.level + "," + this._12_or_24 + "," + this.deadline_year + "," + this.deadline_month + "," + this.deadline_day + "," + this.deadline_hour + "," + this.deadline_minute + "," + this.alarm + "," + this.alarm_num + "," + this.finished + "," + this.delayed + "," + this.emergency + "," + this.son_num);
                sw.Close();
                fs.Close();


                filePath = "database/" + uID + "_" + tID + "_alarm.csv";
                FileInfo fi = new FileInfo(filePath);

                fi.Directory.Create();

                fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine("alarm_ID,alarm_year,alarm_month,alarm_day,alarm_hour,alarm_minute,finished");
                sw.Close();
                fs.Close();


                filePath = "database/" + uID + "_" + tID + "_son.csv";
                fi = new FileInfo(filePath);

                fi.Directory.Create();

                fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine("son_ID,things_to_do,finished");
                sw.Close();
                fs.Close();
            }
        }

        public string get_deadline()
        {
            return this.deadline_year + "_" + this.deadline_month + "_" + this.deadline_day + "_" + this.deadline_hour + "_" + this.deadline_minute;
        }

        public void change_info()
        {
            string filePath = "database/" + this.user_ID + "_data.csv";
            string[] strline = null;
            string[] temp = null;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            strline = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < strline.Length; i++)
            {
                temp = strline[i].Split(',');
                if (temp[0] == this.things_ID)
                {
                    strline[i] = this.things_ID + "," + this.thing_to_do + "," + this.level + "," + this._12_or_24 + "," + this.deadline_year + "," + this.deadline_month + "," + this.deadline_day + "," + this.deadline_hour + "," + this.deadline_minute + "," + this.alarm + "," + this.alarm_num + "," + this.finished + "," + this.delayed + "," + this.emergency + "," + this.son_num;
                }
            }
            sr.Close();
            fs.Close();
            fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            for (int k = 0; k < strline.Length; k++)
            {
                if (strline[k] != "")
                {
                    sw.WriteLine(strline[k]);
                }
            }
            sw.Close();
            fs.Close();
        }

        public void set_thing_to_do(string s)
        {
            this.thing_to_do = s;
            this.change_info();
        }
        public void set_level(string s)
        {
            this.level = s;
            this.change_info();
        }
        public void set_12_or_24(string s)
        {
            this._12_or_24 = s;
            this.change_info();
        }
        public void set_deadline_year(string s)
        {
            this.deadline_year = s;
            this.change_info();
        }
        public void set_deadline_month(string s)
        {
            this.deadline_month = s;
            this.change_info();
        }
        public void set_deadline_day(string s)
        {
            this.deadline_day = s;
            this.change_info();
        }
        public void set_deadline_hour(string s)
        {
            this.deadline_hour = s;
            this.change_info();
        }
        public void set_deadline_minute(string s)
        {
            this.deadline_minute = s;
            this.change_info();
        }
        public void set_alarm(string s)
        {
            this.alarm = s;
            this.change_info();
        }
        public void set_alarm_num(string s)
        {
            this.alarm_num = s;
            this.change_info();
        }
        public void set_finished(string s)
        {
            this.finished = s;
            this.change_info();
        }
        public void set_delayed(string s)
        {
            this.delayed = s;
            this.change_info();
        }
        public void set_emergency(string s)
        {
            this.emergency = s;
            this.change_info();
        }
        public void set_son_num(string s)
        {
            this.son_num = s;
            this.change_info();
        }

        public void remove_thing()
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_alarm.csv";
            File.Delete(filePath);

            //FileInfo fi = new FileInfo(filePath);
            //fi.Directory.Delete();

            filePath = "database/" + this.user_ID + "_" + this.things_ID + "_son.csv";
            File.Delete(filePath);
            //fi = new FileInfo(filePath);
            //fi.Directory.Delete();

            filePath = "database/" + this.user_ID + "_data.csv";
            string[] strline = null;
            string[] temp = null;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            strline = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < strline.Length; i++)
            {
                temp = strline[i].Split(',');
                if (temp[0] == this.things_ID)
                {
                    strline[i] = "";
                }
            }
            sr.Close();
            fs.Close();
            fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            for (int k = 0; k < strline.Length; k++)
            {
                if (strline[k] != "")
                {
                    sw.WriteLine(strline[k]);
                }
            }
            sw.Close();
            fs.Close();
        }

        //返回alarm
        public alarm get_alarms(string aID)
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_alarm.csv";
            string strline = "";
            bool if_exit = false;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] == aID)
                {
                    if_exit = true;
                    break;
                }
            }
            sr.Close();
            fs.Close();
            if (if_exit) return new alarm(this.user_ID, this.things_ID, aID);
            else return null;
        }

        public alarm[] get_alarms()
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_alarm.csv";
            string strline = "";
            List<alarm> alarmlist = new List<alarm>();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (strline != "")
                {
                    alarmlist.Add(new alarm(this.user_ID, this.things_ID, temp[0]));
                }
            }
            sr.Close();
            fs.Close();
            alarm[] alarms = alarmlist.ToArray();
            return alarms;
        }



        //返回son_thing
        public son_thing get_sons(string sID)
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_son.csv";
            string strline = "";
            bool if_exit = false;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] == sID)
                {
                    if_exit = true;
                    break;
                }
            }
            sr.Close();
            fs.Close();
            if (if_exit) return new son_thing(this.user_ID, this.things_ID, sID);
            else return null;
        }

        public son_thing[] get_sons()
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_son.csv";
            string strline = "";
            List<son_thing> sonlist = new List<son_thing>();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (strline != "")
                {
                    sonlist.Add(new son_thing(this.user_ID, this.things_ID, temp[0]));
                }
            }
            sr.Close();
            fs.Close();
            son_thing[] sons = sonlist.ToArray();
            return sons;
        }
    }

    public class alarm
    {
        public string user_ID = "";
        public string things_ID = "";
        public string alarm_ID = "";
        public string alarm_year = "2035";
        public string alarm_month = "1";
        public string alarm_day = "1";
        public string alarm_hour = "23";
        public string alarm_minute = "59";
        public string finished = "no";
        public alarm(string uID, string tID, string aID)
        {
            this.user_ID = uID;
            this.things_ID = tID;
            this.alarm_ID = aID;
            string filePath = "database/" + uID + "_" + tID + "_alarm.csv";
            string strline = "";
            bool if_exit = false;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] == tID)
                {
                    if_exit = true;
                    this.alarm_year = temp[1];
                    this.alarm_month = temp[2];
                    this.alarm_day = temp[3];
                    this.alarm_hour = temp[4];
                    this.alarm_minute = temp[5];
                    this.finished = temp[6];
                    break;
                }
            }
            sr.Close();
            fs.Close();
            if (!if_exit)
            {
                fs = new FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(this.alarm_ID + "," + this.alarm_year + "," + this.alarm_month + "," + this.alarm_day + "," + this.alarm_hour + "," + this.alarm_minute + "," + this.finished);
                sw.Close();
                fs.Close();
            }
        }

        public string get_alarm()
        {
            return this.alarm_year + "_" + this.alarm_month + "_" + this.alarm_day + "_" + this.alarm_hour + "_" + this.alarm_minute;
        }

        public void change_info()
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_alarm.csv";
            string[] strline = null;
            string[] temp = null;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            strline = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < strline.Length; i++)
            {
                temp = strline[i].Split(',');
                if (temp[0] == this.alarm_ID)
                {
                    strline[i] = this.alarm_ID + "," + this.alarm_year + "," + this.alarm_month + "," + this.alarm_day + "," + this.alarm_hour + "," + this.alarm_minute + "," + this.finished;
                }
            }
            sr.Close();
            fs.Close();
            fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            for (int k = 0; k < strline.Length; k++)
            {
                if (strline[k] != "")
                {
                    sw.WriteLine(strline[k]);
                }
            }
            sw.Close();
            fs.Close();
        }

        public void set_alarm_year(string s)
        {
            this.alarm_year = s;
            this.change_info();
        }
        public void set_alarm_month(string s)
        {
            this.alarm_month = s;
            this.change_info();
        }
        public void set_alarm_day(string s)
        {
            this.alarm_day = s;
            this.change_info();
        }
        public void set_alarm_hour(string s)
        {
            this.alarm_hour = s;
            this.change_info();
        }
        public void set_alarm_minute(string s)
        {
            this.alarm_minute = s;
            this.change_info();
        }

        public void remove_alarm()
        {

            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_alarm.csv";
            string[] strline = null;
            string[] temp = null;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            strline = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < strline.Length; i++)
            {
                temp = strline[i].Split(',');
                if (temp[0] == this.alarm_ID)
                {
                    strline[i] = "";
                }
            }
            sr.Close();
            fs.Close();
            fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            for (int k = 0; k < strline.Length; k++)
            {
                if (strline[k] != "")
                {
                    sw.WriteLine(strline[k]);
                }
            }
            sw.Close();
            fs.Close();
        }
    }

    public class son_thing
    {
        public string user_ID = "";
        public string things_ID = "";
        public string son_ID = "";
        public string things_to_do = "homework";
        public string finished = "no";
        public son_thing(string uID, string tID, string sID)
        {
            this.user_ID = uID;
            this.things_ID = tID;
            this.son_ID = sID;
            string filePath = "database/" + uID + "_" + tID + "_son.csv";
            string strline = "";
            bool if_exit = false;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            strline = sr.ReadLine();
            string[] temp = null;
            while ((strline = sr.ReadLine()) != null)
            {
                temp = strline.Split(',');
                if (temp[0] == sID)
                {
                    if_exit = true;
                    this.things_to_do = temp[1];
                    this.finished = temp[2];
                    break;
                }
            }
            sr.Close();
            fs.Close();
            if (!if_exit)
            {
                fs = new FileStream(filePath, System.IO.FileMode.Append, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                sw.WriteLine(this.son_ID + "," + this.things_to_do + "," + this.finished);
                sw.Close();
                fs.Close();
            }
        }

        public void change_info()
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_son.csv";
            string[] strline = null;
            string[] temp = null;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            strline = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < strline.Length; i++)
            {
                temp = strline[i].Split(',');
                if (temp[0] == this.son_ID)
                {
                    strline[i] = this.son_ID + "," + this.things_to_do + "," + this.finished;
                }
            }
            sr.Close();
            fs.Close();
            fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            for (int k = 0; k < strline.Length; k++)
            {
                if (strline[k] != "")
                {
                    sw.WriteLine(strline[k]);
                }
            }
            sw.Close();
            fs.Close();
        }

        public void set_things_to_do(string s)
        {
            this.things_to_do = s;
            this.change_info();
        }
        public void set_finished(string s)
        {
            this.finished = s;
            this.change_info();
        }

        public void remove_son()
        {
            string filePath = "database/" + this.user_ID + "_" + this.things_ID + "_son.csv";
            string[] strline = null;
            string[] temp = null;
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.UTF8);

            strline = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < strline.Length; i++)
            {
                temp = strline[i].Split(',');
                if (temp[0] == this.son_ID)
                {
                    strline[i] = "";
                }
            }
            sr.Close();
            fs.Close();
            fs = new FileStream(filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            for (int k = 0; k < strline.Length; k++)
            {
                if (strline[k] != "")
                {
                    sw.WriteLine(strline[k]);
                }
            }
            sw.Close();
            fs.Close();
        }
    }



    // 数据类
    public class Data
    {
        public Data() { }
        public void CreateUser(string uid, string passwd)
        {
            account a = new account(uid);
            a.set_password(passwd);
        }
        public bool HasUser(string uid, string passwd)
        {
            account a = new account(uid);
            if (a.password == passwd) return true;
            else return false;
        }
        public bool IsParent(string uid1, string uid2)
        {
            account a1 = new account(uid1);
            while (true)
            {

                string temp = a1.father;
                if (temp == uid2)
                {
                    return true;
                }
                else if (temp == "root")
                {
                    break;
                }
                else a1 = new account(temp);
            }
            return false;
        }
        // 修改 Todolist 的 uid 的用户的 tid 事件为 Thinginfo 里的事件和内容
        public void Edit(ThingInfo info)
        {
            thing t = new thing(info.uid, info.tid);
            t.set_deadline_year(info.Year.ToString());
            t.set_deadline_month(info.Month.ToString());
            t.set_deadline_day(info.Day.ToString());
            t.set_thing_to_do(info.Content);
        }
        // 返回如果要往用户 uid 添加一个新的事件的话，tid 应该用什么 
        public string NextTID(string uid)
        {
            account a = new account(uid);
            thing[] things = a.get_things();
            int temp = 0;
            for (int i = 0; i < things.Length; i++)
            {
                if (int.Parse(things[i].things_ID) > temp) temp = int.Parse(things[i].things_ID);
            }
            temp += 1;
            return temp.ToString();
        }
        // 往用户 uid 的 Todolist 末尾添加一个元素 ThingInfo
        public void Add(ThingInfo info)
        {
            thing t = new thing(info.uid, info.tid);
            t.set_deadline_year(info.Year.ToString());
            t.set_deadline_month(info.Month.ToString());
            t.set_deadline_day(info.Day.ToString());
            t.set_thing_to_do(info.Content);
        }
        // 删除 Todolist 的 uid 用户的编号为 tid 的元素
        public void Remove(string uid, string tid)
        {
            thing t = new thing(uid, tid);
            t.remove_thing();
        }
        bool Greater(thing t1, thing t2)
        {
            if (int.Parse(t1.deadline_year) != int.Parse(t2.deadline_year))
                return int.Parse(t1.deadline_year) > int.Parse(t2.deadline_year);
            if (int.Parse(t1.deadline_month) != int.Parse(t2.deadline_month))
                return int.Parse(t1.deadline_month) > int.Parse(t2.deadline_month);
            if (int.Parse(t1.deadline_day) != int.Parse(t2.deadline_day))
                return int.Parse(t1.deadline_day) > int.Parse(t2.deadline_day);
            return false;
        }
        // 返回 Todolist 的 uid 用户所有事件，按时间排序
        public List<ThingInfo> GetSortedThings(string uid)
        {
            List<ThingInfo> ThingInfoList = new List<ThingInfo>();
            account a = new account(uid);
            thing[] things = a.get_things();
            thing temp = null;
            int l = things.Length;
            for (int i = 0; i < l - 1; i++)
            {
                for (int j = 0; j < l - 1; j++)
                {
                    if (Greater(things[j], things[j + 1]))
                    {
                        temp = things[j];
                        things[j] = things[j + 1];
                        things[j + 1] = temp;
                    }
                }
            }

            /************** DEBUG ***************
            Console.WriteLine("233333333333333333");
            Console.WriteLine("l: " + l);
            for (int i = 0; i < l; i++)
                Console.WriteLine(i
                    + ": " + int.Parse(things[i].deadline_year)
                    + ", " + int.Parse(things[i].deadline_month)
                    + ", " + int.Parse(things[i].deadline_day));
            Console.WriteLine(Greater(things[2], things[3]).ToString());
            Console.WriteLine(Greater(things[3], things[4]).ToString());
            ************* DEBUG ****************/

            for (int i = 0; i < l; i++)
            {
                ThingInfoList.Add(new ThingInfo(
                    things[i].user_ID, things[i].things_ID,
                    int.Parse(things[i].deadline_year),
                    int.Parse(things[i].deadline_month),
                    int.Parse(things[i].deadline_day),
                    things[i].thing_to_do, 0));
            }
            return ThingInfoList;
        }
    }

}
