﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using java.io;
using Newtonsoft.Json;

namespace TCPClient
{    
    public partial class Form1 : Form
    {
        Client client;
        private bool run = false;

        class PhoneBookClient
        {
            public Int64 code { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
            public string email { get; set; }
            public byte[] avatar { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        //IPEndPoint IP;
        //Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        //Client sv = new Client(IPAddress.Parse(textBox1.Text), Int32.Parse(textBox2.Text));
        public void Connect(object sender, EventArgs e)
        {

            //Client client = new Client(IPAddress.Parse(textBox1.Text), Int32.Parse(textBox2.Text));
            const Int32 sizePictureMax = 1000000000;
            client = new Client(IPAddress.Parse(textBox1.Text), Int32.Parse(textBox2.Text), sizePictureMax);
            client.Connect();
            button1.Enabled = false;
            button2.Enabled = true;
            run = true;

        }
        
     

    private void Disconnect(object sender, EventArgs e)
        {
            if (run == true)
            {
                button1.Enabled = true;
                button2.Enabled = false;
                MessageBox.Show("Đóng kết nối thành công");
                this.Close();
            }
            else
                MessageBox.Show("Chưa kết nối server");
        }
    

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.ForeColor = Color.Black;
                textBox2.ForeColor = Color.Black;
                textBox1.Text = "127.0.0.1";
                textBox2.Text = "8080";

            }
        }

        private void textBox2_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text != "Input Port") return;
            textBox2.Text = " ";
            textBox2.ForeColor = Color.Black;
        }

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != "Input IP") return;
            textBox1.Text = " ";
            textBox1.ForeColor = Color.Black;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text != "") return;
            textBox1.ForeColor = Color.Gray;
            textBox1.Text = "Input IP";
        }

        private void textBox2_Leave_1(object sender, EventArgs e)
        {
            if (textBox2.Text != "") return;
            textBox2.ForeColor = Color.Gray;
            textBox2.Text = "Input Port";
        }

        public void Display(object sender, EventArgs e)
        {
            client.Send("Display");

            List<PhoneBookClient> phoneBookClients = new List<PhoneBookClient>();

            string convert =Encoding.UTF8.GetString(client.Recieve());

            phoneBookClients = JsonConvert.DeserializeObject<List<PhoneBookClient>>(convert);

            showObject(phoneBookClients[2]);
        }  
        //gui ma so can tìm kiem
        private void Search(object sender, EventArgs e)
        {            
            client.Send(SearchTextBox.Text);

            //Nhan lai thong tin tu server
            string data = Encoding.UTF8.GetString(client.Recieve());

            PhoneBookClient phoneBookClient = new PhoneBookClient();
            phoneBookClient = JsonConvert.DeserializeObject<PhoneBookClient>(data);

            showObject(phoneBookClient);
        }

        private void showObject(PhoneBookClient phoneBookClient)
        {
            CodeView.Text = phoneBookClient.code.ToString();
            NameView.Text = phoneBookClient.name;
            PhoneView.Text = phoneBookClient.phone;
            EmailView.Text = phoneBookClient.email;
            pictureBox.Image = new Bitmap(Image.FromStream(new MemoryStream(phoneBookClient.avatar)), new Size(100, 100));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadAvata(object sender, EventArgs e)
        {
            
        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    } 
}