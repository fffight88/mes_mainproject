using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pyCall
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("IronPython : 안되면 요청해주세요!");
        }

        private void button2_Click(object sender, EventArgs e)          // cmd에서 파이썬을 실행시킨 후 문자열을 입력, 실행하여 리턴한 값을 텍스트박스에 표현하는 기능
        {
            string baseDir = Application.StartupPath;                   // 본 응용프로그램(.exe)이 설치되어 있는 경로를 baseDir로 받음
            string pyPath = Path.Combine(baseDir, "A.I", "lotto.py");   // baseDir , A.I, lotto.py 세 문자열을 연결하여 하나의 경로로 만듦(baseDir\A.I\lotto.py)

            ProcessStartInfo cmd = new ProcessStartInfo();              // 프로세스 시작의 설정값을 cmd에 저장
            Process process = new Process();                            // 프로세스를 process로 객체화(여기서는 프로세스가 cmd)
            cmd.WindowStyle = ProcessWindowStyle.Hidden;                
            cmd.CreateNoWindow = true;                                                                  
            cmd.UseShellExecute = false;                                
            cmd.RedirectStandardOutput = true;                          // cmd창에서 데이터를 가져오기
            cmd.RedirectStandardInput = true;                           // cmd창으로 데이터 보내기
            cmd.RedirectStandardError = true;                           // cmd창에서 오류 내용 가져오기
            cmd.FileName = @"cmd";                                      // 실행시킬 파일 이름
            process.StartInfo = cmd;                                    // cmd에 저장된 프로세스 설정값을 process의 설정값으로 저장
            process.Start();                                            // 프로세스 시작 ( = cmd 실행)

            string pyPathCmd = "python.exe " + pyPath;                  // python.exe + 한칸띄어쓰기 + pyPath 문자열을 pyPathCmd에 저장
            process.StandardInput.WriteLine(pyPathCmd);                 // StandardInput(키보드)으로 cmd창에 pyPathCmd 문자열을 쓰고 실행한다.
            process.StandardInput.Close();                              // StandardInput을 종료한다.
            string result = process.StandardOutput.ReadToEnd();         // StandardInput의 리턴값을 처음부터 끝까지 읽어서 result에 문자열로 담는다

            if(!result.Contains("로또"))
            {
                MessageBox.Show("python이 실행되지 않았습니다. python 설치 또는 python code 오류를 확인해주세요.");
                return;
            }
            
            textBox1.Text = result;                                     // 리턴받은 값으로 테이블작성 차트작성 등 처리할 것이 있으면 여기서 한다.
            textBox1.Select(textBox1.Text.Length, 0);
            textBox1.ScrollToCaret();

            process.StandardOutput.Close();                             // StandardOutput을 종료한다.
;
            process.WaitForExit();
            process.Close();
        }
    }
}
