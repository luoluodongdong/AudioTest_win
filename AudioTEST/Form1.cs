using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Threading;

namespace AudioTEST
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        public float checkValue = 0.00015F;
        public bool[] result = { false, false, false };
        public string valueStr = "";
        public string testVoice=Application.StartupPath + "\\testVoice1.mp3";

        BackgroundWorker backgroundWorker1;
        private int checkTimerCount = 0;
        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
        float currentValue = 0;
        bool _playAndRecord_Done = false;
        AudioHelper ah = new AudioHelper();
        private string[] channel = { "L", "R", "MIC" };
        private string currentChannel;
        ISampleProvider sampleProvider;
        static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();
        private void Form1_Load(object sender, EventArgs e)
        {

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker1.WorkerReportsProgress = true;//能否报告进度更新。 
            backgroundWorker1.WorkerSupportsCancellation = true;//是否支持异步取消 
                                                                //绑定事件 
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }
        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            result[0] = false;
            result[1] = false;
            result[2] = false;
            valueStr = "";
            Thread.Sleep(500);
            
            for(int i = 0; i < 2; i++)
            {
                currentChannel = channel[i];
                if (i == 0)
                {
                    DoOnUIThread(delegate ()
                    {
                        MIC_resultLabel.Text = "";
                        L_valueLable.Text = "";
                        L_resultLabel.Text = "";
                        L_resultLabel.BackColor = Color.White;
                        R_valueLable.Text = "";
                        R_resultLabel.Text = "";
                        R_resultLabel.BackColor = Color.White;
                    });

                }
                
                checkTimerCount = 0;
                Console.WriteLine("testVoice:" + testVoice);
                DoOnUIThread(delegate () {
                    playChannelAndRecord(testVoice, channel[i]);
                });

                while (!_playAndRecord_Done)
                {
                    Thread.Sleep(100);
                }
                Thread.Sleep(200);
                _playAndRecord_Done = false;

                checkTimerCount = 0;
                string recordFile = Application.StartupPath + string.Format("\\saveVoice{0}.wav", channel[i]);
                DoOnUIThread(delegate () {
                    playWaveOut(recordFile);
                });

                while (!_playAndRecord_Done)
                {
                    Thread.Sleep(100);
                }
                Thread.Sleep(200);
                _playAndRecord_Done = false;
                Console.WriteLine(string.Format("Test channel {0} done.", channel[i]));
                
            }

           
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (result[0] || result[1])
            {
                result[2] = true;
                MIC_resultLabel.Text = "result:PASS";
                MIC_resultLabel.BackColor = Color.Green;
                valueStr += "@MIC:pass";
            }
            else
            {
                result[2] = false;
                MIC_resultLabel.Text = "result:FAIL";
                MIC_resultLabel.BackColor = Color.Red;
                valueStr += "@MIC:fail";
            }
            PlayBtn.Enabled = true;
            Console.WriteLine("audio/mic result:L:{0}R:{1}M:{2}",
                result[0].ToString(), result[1].ToString(), result[2].ToString());
            Console.WriteLine("audio/mic value:" + valueStr);
        }
        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //e.ProgressPercentage 获取异步操作进度的百分比 
            //resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
            string state = (string)e.UserState;//接收ReportProgress方法传递过来的userState 
        }
        private void PlayBtn_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            PlayBtn.Enabled = false;
        }

        private void playWaveOut(string recordWave)
        {
            // we are in a stopped state
            // TODO: only re-initialise if necessary
            if (String.IsNullOrEmpty(recordWave))
            {
                return;
            }

            try
            {
                CreateWaveOut();
            }
            catch (Exception driverCreateException)
            {
                MessageBox.Show(String.Format("{0}", driverCreateException.Message));
                return;
            }

            
            try
            {
                sampleProvider = CreateInputStream(recordWave);
            }
            catch (Exception createException)
            {
                MessageBox.Show(String.Format("{0}", createException.Message), "Error Loading File");
                return;
            }


            //labelTotalTime.Text = String.Format("{0:00}:{1:00}", (int)audioFileReader.TotalTime.TotalMinutes,
            //    audioFileReader.TotalTime.Seconds);

            try
            {
                waveOut.Init(sampleProvider);
            }
            catch (Exception initException)
            {
                MessageBox.Show(String.Format("{0}", initException.Message), "Error Initializing Output");
                return;
            }

            //setVolumeDelegate(volumeSlider1.Volume);
            //groupBoxDriverModel.Enabled = false;
            waveOut.Play();
            timer1.Enabled = true;
        }

        private ISampleProvider CreateInputStream(string fileName)
        {

            audioFileReader = new AudioFileReader(fileName);

            var sampleChannel = new SampleChannel(audioFileReader, true);
            sampleChannel.PreVolumeMeter += OnPreVolumeMeter;
            sampleChannel.Volume = 100;
            //setVolumeDelegate = vol => sampleChannel.Volume = vol;
            var postVolumeMeter = new MeteringSampleProvider(sampleChannel);
            postVolumeMeter.StreamVolume += OnPostVolumeMeter;

            return postVolumeMeter;
        }
        void OnPreVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            // we know it is stereo
            float max1 = e.MaxSampleValues[0];
            float max2 = e.MaxSampleValues[1];
            Console.WriteLine(string.Format("max1:{0} max2:{1}", max1, max2));
            currentValue = max1;
            if(currentChannel == "L")
            {
                L_valueLable.Text = "value:" + max1.ToString();
            }
            else
            {
                R_valueLable.Text = "value:" + max1.ToString();
            }
            
           // waveformPainter1.AddMax(e.MaxSampleValues[0]);
            //waveformPainter2.AddMax(e.MaxSampleValues[1]);
        }

        void OnPostVolumeMeter(object sender, StreamVolumeEventArgs e)
        {
            // we know it is stereo
            //volumeMeter1.Amplitude = e.MaxSampleValues[0];
            //volumeMeter2.Amplitude = e.MaxSampleValues[1];
        }
        private void CloseWaveOut()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }
            if (audioFileReader != null)
            {
                // this one really closes the file and ACM conversion
                audioFileReader.Close();
                audioFileReader.Dispose();
                //setVolumeDelegate = null;
                //audioFileReader = null;
            }
            sampleProvider = null;
        }
        private void CreateWaveOut()
        {
            CloseWaveOut();

            waveOut = new WaveOut();

            waveOut.PlaybackStopped += OnPlaybackStopped;
        }

        void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            //groupBoxDriverModel.Enabled = true;
            if (e.Exception != null)
            {
                MessageBox.Show(e.Exception.Message, "Playback Device Error");
            }
            if (audioFileReader != null)
            {
                audioFileReader.Position = 0;
            }
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
            }
            
        }

        private void playChannelAndRecord(string voiceFile,string channel)
        {
            ah.voiceFile = voiceFile;//Application.StartupPath + "\\1000HZ.WAV";

            axWindowsMediaPlayer1.URL = ah.voiceFile;
            axWindowsMediaPlayer1.settings.volume = 100;
            axWindowsMediaPlayer1.settings.balance = 10000; //右声道
            if (channel.Equals("L"))
            {
                axWindowsMediaPlayer1.settings.balance = -10000; //左声道
            }
            axWindowsMediaPlayer1.Ctlcontrols.play();

            ah.saveFile = Application.StartupPath + string.Format("\\saveVoice{0}.wav",channel);
            if (File.Exists(ah.saveFile))
            {
                File.Delete(ah.saveFile);
            }

            DoOnUIThread(delegate () {
                ah.recordStart();
                checkTimerCount = 0;
                timer2.Enabled = true;
            }); 
        }
        private void DoOnUIThread(MethodInvoker d)
        {
            if (this.InvokeRequired) { this.Invoke(d); } else { d(); }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            checkTimerCount += 1;
            if(checkTimerCount > 10)
            {
                timer1.Enabled = false;
                if (currentChannel.Equals("L"))
                {
                    L_resultLabel.Text = "result:FAIL";
                    L_resultLabel.BackColor = Color.Red;
                    result[0] = false;
                    valueStr = "L:" + currentValue.ToString();
                }else if (currentChannel.Equals("R"))
                {
                    R_resultLabel.Text = "result:FAIL";
                    R_resultLabel.BackColor = Color.Red;
                    result[1] = false;
                    valueStr = "@R:" + currentValue.ToString();
                }

                CloseWaveOut();
                _playAndRecord_Done = true;
                return;
            }
            if(currentValue > checkValue)
            {
                timer1.Enabled = false;
                if (currentChannel.Equals("L"))
                {
                    L_resultLabel.Text = "result:PASS";
                    L_resultLabel.BackColor = Color.Green;
                    result[0] = true;
                    valueStr = "L:" + currentValue.ToString();
                }
                else if (currentChannel.Equals("R"))
                {
                    R_resultLabel.Text = "result:PASS";
                    R_resultLabel.BackColor = Color.Green;
                    result[1] = true;
                    valueStr += "@R:" + currentValue.ToString();
                }
                CloseWaveOut();
                _playAndRecord_Done = true;
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            checkTimerCount += 1;
            Console.WriteLine("timer2:" + checkTimerCount.ToString());
            if(checkTimerCount > 6)
            {
                timer2.Enabled = false;
                axWindowsMediaPlayer1.Ctlcontrols.stop();
                ah.recordStop();
                _playAndRecord_Done = true;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            PlayBtn.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
