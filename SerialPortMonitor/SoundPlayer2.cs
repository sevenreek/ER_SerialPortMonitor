using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
class SoundPlayer
{

    WindowsMediaPlayer wplayer;
    public SoundPlayer()
    {
        wplayer = new WindowsMediaPlayer();

    }
    public void PlaySound(string soundFile)
    {
        wplayer.URL = soundFile;
        wplayer.controls.play();
    }
    public void StopSound()
    {
        wplayer.controls.stop();
    }
    public void PauseSound()
    {
        wplayer.controls.pause();
    }
}


