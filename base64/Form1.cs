using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using base64;
using System.Collections;
namespace base64
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        string  Passwd= "12345678";//加密密钥
        string Url = "";
        string source = "";//原文件内容
        bool Encryption = false;//加密判断
        string tmp = "";//临时变量
        bool SaveEncrypt = false;//保存自动加密
        bool OpenDecrypt = false;//打开自动解密
        private void Form1_Load(object sender, EventArgs e)
        {
            GetSetup();//加载配置文件
            string command = Environment.CommandLine;//获取进程命令行参数
            string[] para = command.Split('\"');
            if (para.Length > 3)
            {
                if(OpenDecrypt)
                {
                    bool tmpEncryption=false;
                    string pathC = para[3];//获取打开的文件的路径
                    StreamReader sTmp = new StreamReader(pathC, System.Text.Encoding.UTF8);
                    string sr = sTmp.ReadToEnd();
                    try
                    {
                        tmp = base64.Decrypt(sr, Passwd);
                        tmpEncryption = true;
                        Encryption = true;
                    }
                    catch
                    {
                        tmpEncryption = false;
                        Encryption = false;
                    }
                    if(tmpEncryption)
                    {
                        textBox1.Text = sr;
                        source = sr;
                        sTmp.Close();           //关闭StreamWriter对象
                        toolStripStatusURL.Text = pathC;
                        Url = pathC;
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                        toolStripStatusLabel1.Text = "编码:UTF-8";
                    }
                  else
                    {
                        textBox1.Text = tmp;
                        source = sr;
                        sTmp.Close();           //关闭StreamWriter对象
                        toolStripStatusURL.Text = pathC;
                        Url = pathC;
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                        toolStripStatusLabel1.Text = "编码:UTF-8";
                    }
                    
                }
                else
                {
                    string pathC = para[3];//获取打开的文件的路径
                    StreamReader sTmp = new StreamReader(pathC, System.Text.Encoding.UTF8);
                    string sr = sTmp.ReadToEnd();
                    textBox1.Text = sr;
                    source = textBox1.Text;
                    sTmp.Close();           //关闭StreamWriter对象
                    toolStripStatusURL.Text = pathC;
                    Url = pathC;
                    this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                    toolStripStatusLabel1.Text = "编码:UTF-8";
                    Judgment();//判断是否加密
                }
            }
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void GetSetup()
        {
            try
            {
                ArrayList al = new ArrayList();
                StreamReader sr = new StreamReader(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini", System.Text.Encoding.UTF8);
                String line;
                al.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    al.Add(line);
                }
                sr.Close();
                for (int i = 0; i < al.Count; i++)
                {
                    if (al[0].ToString() == "SaveEncrypt=true")//保存自动加密
                    {
                        SaveEncrypt = true;
                        保存自动加密ToolStripMenuItem.Text = "√保存自动加密";
                    }
                    else
                    {
                        SaveEncrypt = false;
                        保存自动加密ToolStripMenuItem.Text = "保存自动加密";
                    }
                    if (al[1].ToString() == "OpenDecrypt=true")//打开自动解密
                    {
                        OpenDecrypt = true;
                        打开自动解密ToolStripMenuItem.Text = "√打开自动解密";
                    }
                    else
                    {
                        OpenDecrypt = false;
                        打开自动解密ToolStripMenuItem.Text = "打开自动解密";
                    }
                }
                sr.Close();//关闭StreamWriter对象
            }
          catch
            {
                MessageBox.Show("获取配置文件失败！");
            }

        }
        private void 打开ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Open(openFileDialog1);
            Judgment();//判断是否加密

        }

        private void Open(OpenFileDialog openFileDialog1)
        {
            if(OpenDecrypt)
            {
                
                string resultFile = "";
                openFileDialog1.InitialDirectory = "C:\\";
                openFileDialog1.Filter = "All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    resultFile = openFileDialog1.FileName;
                if (resultFile != "")
                {
                    bool tmpEncryption = false;
                    StreamReader sTmp = new StreamReader(resultFile, System.Text.Encoding.UTF8);
                    string sr = sTmp.ReadToEnd();
                    try
                    {
                        tmp = base64.Decrypt(sr, Passwd);
                        tmpEncryption = true;
                        Encryption = true;
                    }
                    catch
                    {
                        tmpEncryption = false;
                        Encryption = false;
                    }
                    if(tmpEncryption)
                    {
                        textBox1.Text = tmp;
                        source = sr;
                        sTmp.Close();           //关闭StreamWriter对象
                        toolStripStatusURL.Text = resultFile;
                        Url = resultFile;
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                        toolStripStatusLabel1.Text = "编码:UTF-8";
                    }
                    else
                    {
                        textBox1.Text =sr;
                        source = sr;
                        sTmp.Close();           //关闭StreamWriter对象
                        toolStripStatusURL.Text = resultFile;
                        Url = resultFile;
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                        toolStripStatusLabel1.Text = "编码:UTF-8";
                    }
                    //   MessageBox.Show("导入成功");
                }
            }
           else
            {
                string resultFile = "";
                openFileDialog1.InitialDirectory = "C:\\";
                openFileDialog1.Filter = "All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    resultFile = openFileDialog1.FileName;
                if (resultFile != "")
                {
                    StreamReader sTmp = new StreamReader(resultFile, System.Text.Encoding.UTF8);
                    textBox1.Text = sTmp.ReadToEnd();
                    source = textBox1.Text;
                    sTmp.Close();           //关闭StreamWriter对象
                    toolStripStatusURL.Text = resultFile;
                    Url = resultFile;
                    this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                    toolStripStatusLabel1.Text = "编码:UTF-8";
                    //   MessageBox.Show("导入成功");
                }
            }
        }

        private void Save()
        {
            if(SaveEncrypt)
            {
                if (Url != "")
                {
                    StreamWriter sTmp = new StreamWriter(Url);
                    sTmp.Write(base64.Encrypt(textBox1.Text, Passwd));          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    MessageBox.Show("保存成功！");
                }
                else
                {
                    saveFileDialog1.Title = "保存加密文件";
                    saveFileDialog1.Filter = "xiz文件|*.xiz|记事本文件|*.txt|所有文件|*.*";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter sTmp = new StreamWriter(saveFileDialog1.FileName);
                        sTmp.Write(base64.Encrypt(textBox1.Text, Passwd));
                        sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                        sTmp.Close();           //关闭StreamWriter对象
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);//去除未保存标识
                        toolStripStatusURL.Text = saveFileDialog1.FileName;
                        toolStripStatusLabel1.Text = "编码:UTF-8";
                        MessageBox.Show("保存成功！");
                    }
                }
            }
            else
            {
                if (Url != "")
                {
                    StreamWriter sTmp = new StreamWriter(Url);
                    sTmp.Write(textBox1.Text);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    MessageBox.Show("保存成功！");
                }
                else
                {
                    saveFileDialog1.Title = "保存文件";
                    saveFileDialog1.Filter = "xiz文件|*.xiz|记事本文件|*.txt|所有文件|*.*";
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        StreamWriter sTmp = new StreamWriter(saveFileDialog1.FileName);
                        sTmp.Write(textBox1.Text);
                        sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                        sTmp.Close();           //关闭StreamWriter对象
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);//去除未保存标识
                        toolStripStatusURL.Text = saveFileDialog1.FileName;
                        toolStripStatusLabel1.Text = "编码:UTF-8";
                        MessageBox.Show("保存成功！");
                    }
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void 加密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encryption = true;
            textBox1.Text = base64.Encrypt(textBox1.Text, Passwd);
        }

        private void 解密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Encryption = false;
            textBox1.Text = base64.Decrypt(textBox1.Text, Passwd);
        }




        /// <summary>
        /// GB2312转换成UTF8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string gb2312_utf8(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            byte[] gb;
            gb = gb2312.GetBytes(text);
            gb = System.Text.Encoding.Convert(gb2312, utf8, gb);
            //返回转换后的字符   
            return utf8.GetString(gb);
        }

        /// <summary>
        /// UTF8转换成GB2312
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string utf8_gb2312(string text)
        {
            //声明字符集   
            System.Text.Encoding utf8, gb2312;
            //utf8   
            utf8 = System.Text.Encoding.GetEncoding("utf-8");
            //gb2312   
            gb2312 = System.Text.Encoding.GetEncoding("gb2312");
            byte[] utf;
            utf = utf8.GetBytes(text);
            utf = System.Text.Encoding.Convert(utf8, gb2312, utf);
            //返回转换后的字符   
            return gb2312.GetString(utf);
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = gb2312_utf8(textBox1.Text);
            toolStripStatusLabel1.Text = "编码:UTF-8";
        }

        private void gBKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = utf8_gb2312(textBox1.Text);
            toolStripStatusLabel1.Text = "编码:GBK";
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            //快捷键保存
                if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.S)
                {
                    if(SaveEncrypt)
                    {
                        if(Url!="")
                        {
                            StreamWriter sTmp = new StreamWriter(Url);
                            sTmp.Write(base64.Encrypt(textBox1.Text, Passwd));
                            sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                            sTmp.Close();           //关闭StreamWriter对象
                            source= (base64.Encrypt(textBox1.Text, Passwd));
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);//去除未保存标识
                        }
                        else
                        {
                            saveFileDialog1.Title = "保存加密文件";
                            saveFileDialog1.Filter = "xiz文件|*.xiz|记事本文件|*.txt|所有文件|*.*";
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                StreamWriter sTmp = new StreamWriter(saveFileDialog1.FileName);
                                sTmp.Write(base64.Encrypt(textBox1.Text, Passwd));
                                source = (base64.Encrypt(textBox1.Text, Passwd));
                                sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                                sTmp.Close();           //关闭StreamWriter对象
                                this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);//去除未保存标识
                                toolStripStatusURL.Text = saveFileDialog1.FileName;
                                toolStripStatusLabel1.Text = "编码:UTF-8";
                                MessageBox.Show("保存成功！");
                            }
                        }
                   
                    }
                    else
                    {
                        if (Url != "")
                        {
                            StreamWriter sTmp = new StreamWriter(Url);
                            sTmp.Write(textBox1.Text);
                            sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                            sTmp.Close();           //关闭StreamWriter对象
                           source =textBox1.Text;
                        this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);//去除未保存标识
                        }
                        else
                        {
                            saveFileDialog1.Title = "保存文件";
                            saveFileDialog1.Filter = "xiz文件|*.xiz|记事本文件|*.txt|所有文件|*.*";
                            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                            {
                                StreamWriter sTmp = new StreamWriter(saveFileDialog1.FileName);
                                sTmp.Write(textBox1.Text);
                            source = textBox1.Text;
                            sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                                sTmp.Close();           //关闭StreamWriter对象
                                this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);//去除未保存标识
                                toolStripStatusURL.Text = saveFileDialog1.FileName;
                                toolStripStatusLabel1.Text = "编码:UTF-8";
                                MessageBox.Show("保存成功！");
                            }
                        }
                    }
                   
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //发生修改

                if (source != "")
                {
                    if(Encryption)//加密了的话
                    {
                        if (textBox1.Text!= base64.Decrypt(source, Passwd))//进行解密
                        {
                            this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                            this.Text = "*" + this.Text;

                        }
                        else
                        {
                            this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                        }
                    }
                    else //没加密的话
                    {
                            if (source != textBox1.Text)
                            {
                                this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                                this.Text = "*" + this.Text;
                            }
                            else
                            {
                                this.Text = this.Text.Substring(this.Text.LastIndexOf("*") + 1);
                            }
                      }
                }


        }

        /// <summary>
        /// 判断是否加密了
        /// </summary>
        private void Judgment()
        {
            try
            {
                tmp = base64.Decrypt(source,Passwd);
                Encryption = true;
            }
            catch
            {
                Encryption = false;
            }
        }

        private void 保存自动加密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveEncrypt)
            {
                SaveEncrypt = true;//开启自动加密
                string setup = "";
                ArrayList al = new ArrayList();
                FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                String line;
                al.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    al.Add(line);
                }
                if (al.Count == 0)//如果配置文件空的话重新生成下
                {
                    setup += "SaveEncrypt=true" + "\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    保存自动加密ToolStripMenuItem.Text = "√保存自动加密";
                }
                else
                {
                    setup = "SaveEncrypt=true" + "\r\n"+al[1].ToString()+"\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    保存自动加密ToolStripMenuItem.Text = "√保存自动加密";
                }
            }
            else
            {
                SaveEncrypt = false;
                string setup = "";
                ArrayList al = new ArrayList();
                FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                String line;
                al.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    al.Add(line);
                }
                if (al.Count == 0)//如果配置文件空的话重新生成下
                {
                    setup += "SaveEncrypt=false" + "\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    保存自动加密ToolStripMenuItem.Text = "保存自动加密";
                }
                else
                {
                    setup = "SaveEncrypt=false" + "\r\n" + al[1].ToString() + "\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    保存自动加密ToolStripMenuItem.Text = "保存自动加密";
                }
            }
        }

        private void 打开自动解密ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!OpenDecrypt)
            {
                OpenDecrypt = true;
                string setup = "";
                ArrayList al = new ArrayList();
                FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                String line;
                al.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    al.Add(line);
                }
                if (al.Count == 0)//如果配置文件空的话重新生成下
                {
                   setup = "SaveEncrypt=true" + "\r\n";
                   setup =setup+ "OpenDecrypt=true" + "\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    打开自动解密ToolStripMenuItem.Text = "√打开自动解密";
                }
                else
                {
                    setup = al[0].ToString() + "\r\n" + "OpenDecrypt=true" + "\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    打开自动解密ToolStripMenuItem.Text = "√打开自动解密";
                }
            }
           else
            {
                OpenDecrypt = false;
                string setup = "";
                ArrayList al = new ArrayList();
                FileStream fs = new FileStream(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                String line;
                al.Clear();
                while ((line = sr.ReadLine()) != null)
                {
                    al.Add(line);
                }
                if (al.Count == 0)//如果配置文件空的话重新生成下
                {
                    setup = "SaveEncrypt=false" + "\r\n";
                    setup =setup+ "OpenDecrypt=false" + "\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    打开自动解密ToolStripMenuItem.Text = "√打开自动解密";
                }
                else
                {
                    setup = al[0].ToString() + "\r\n" + "OpenDecrypt=false" + "\r\n";
                    StreamWriter sTmp = new StreamWriter(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "setup.ini");
                    sTmp.Write(setup);          //将字符串写入流
                    sTmp.Flush();           //将缓冲区数据写入流，并清理所有缓冲区
                    sTmp.Close();           //关闭StreamWriter对象
                    打开自动解密ToolStripMenuItem.Text = "打开自动解密";
                }
            }

        }
    }
}
