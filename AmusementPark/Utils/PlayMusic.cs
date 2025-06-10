using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmusementPark.Utils
{
    public class PlayMusic
    {

        public static void PlaysMusic()
        {
            while (true)
            {
                var musicPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Duck_Music.mp3");
                using var audioFile = new AudioFileReader(musicPath);
                using var outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }

    }
}
