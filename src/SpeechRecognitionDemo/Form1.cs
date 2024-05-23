using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechRecognitionDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 生成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            string secretId = ConfigurationManager.AppSettings["SecretId"];
            string secretKey = ConfigurationManager.AppSettings["SecretKey"];
            string url = txtUrl.Text;
            // 获取任务id
           string taskId= SpeechRecognitionUtils.CreateTask(secretId,secretKey,url);
            if (!string.IsNullOrWhiteSpace(taskId))
            {
                string result = SpeechRecognitionUtils.SearchResultByTaskId(secretId, secretKey, taskId);
                textBox1.Text = result;
            }
          
        }
    }
}
