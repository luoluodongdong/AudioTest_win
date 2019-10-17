using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using System.Media;

namespace AudioTEST
{
    class AudioHelper
    {
        private WaveIn recorder;
        private WaveFileWriter writer;
        //private BufferedWaveProvider bufferedWaveProvider;
        //private SavingWaveProvider savingWaveProvider;
        private AudioFileReader afr;
        private WaveOut player;
      
        



        public string voiceFile;
        public string saveFile;

        public void startTest()
        {
            
        }
        public void recordStart()
        {
            // set up the recorder
            recorder = new WaveIn();
            recorder.DataAvailable += RecorderOnDataAvailable;

            //recorder.WaveFormat = new WaveFormat(44100, 16, 1);
            writer = new WaveFileWriter(saveFile, recorder.WaveFormat);
            recorder.StartRecording();
            Console.WriteLine("Record start...");
        }
        public void recordStop()
        {
            // stop recording
            recorder.StopRecording();
            writer?.Dispose();
            writer = null;
            Console.WriteLine("Record stop.");
        }
        public void voiceStart()
        {
            afr = new AudioFileReader(voiceFile);
            // set up playback
            player = new WaveOut();
            player.Init(afr);

            // begin playback & record
            player.Volume = 1;

            player.Play();

            Console.WriteLine("Voice start...");
        }
        private void voiceStop()
        {
            // stop playback
            player.Stop();
            // finalise the WAV file
            //savingWaveProvider.Dispose();

            player.Dispose();
            afr.Close();
            Console.WriteLine("Voice stop.");
        }
        public void stopTest()
        {
            
           
        }

        private void RecorderOnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            //Console.WriteLine("la...");
            //bufferedWaveProvider.AddSamples(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
            if (writer != null)
            {
                writer.Write(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);
                writer.Flush();
            }

        }


    }

    class SavingWaveProvider : IWaveProvider, IDisposable
    {
        private readonly IWaveProvider sourceWaveProvider;
        private readonly WaveFileWriter writer;
        private bool isWriterDisposed;

        public SavingWaveProvider(IWaveProvider sourceWaveProvider, string wavFilePath)
        {
            this.sourceWaveProvider = sourceWaveProvider;
            writer = new WaveFileWriter(wavFilePath, sourceWaveProvider.WaveFormat);
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            var read = sourceWaveProvider.Read(buffer, offset, count);
            if (count > 0 && !isWriterDisposed)
            {
                writer.Write(buffer, offset, read);
            }
            if (count == 0)
            {
                Dispose(); // auto-dispose in case users forget
            }
            return read;
        }

        public WaveFormat WaveFormat { get { return sourceWaveProvider.WaveFormat; } }

        public void Dispose()
        {
            if (!isWriterDisposed)
            {
                isWriterDisposed = true;
                writer.Dispose();
            }
        }
    }
}
