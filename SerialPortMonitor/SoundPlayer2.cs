using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
class SoundPlayer
{

    WindowsMediaPlayer wplayer;
    bool isEnglish = false;
    public delegate void OnPlayerStatusChangeDelegate(int newState);
    public int lastPlayed;
    public OnPlayerStatusChangeDelegate OnStatusChange;
    public SoundPlayer()
    {
        wplayer = new WindowsMediaPlayer();
        wplayer.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(OnStatusChanged);

    }
    public void PlaySound(string soundFile)
    {
        bool success = int.TryParse(soundFile, out lastPlayed);
        if (!success)
            lastPlayed = -1;
        wplayer.URL = System.IO.Directory.GetCurrentDirectory() + "/mp3/" + soundFile + (isEnglish?"_ENG":"_POL") + ".mp3";
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
    public void SetEnglish()
    {
        isEnglish = true;
    }
    public void SetPolish()
    {
        isEnglish = false;
    }
    public void OnStatusChanged(int newState)
    {
        OnStatusChange(newState);
    }
}


