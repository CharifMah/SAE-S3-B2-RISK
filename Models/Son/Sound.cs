using Stockage;
using System;
using System.Windows.Media;

namespace Models.Son
{
    public class Sound
    {
        private MediaPlayer player;
        private bool loop;
        private bool playing;
        private Sound backgroundMusic;

        public Sound(string path)
        {
            loop = false;
            playing = false;
            player = new MediaPlayer();
            player.MediaEnded += new EventHandler(Player_MediaEnded);
            player.MediaOpened += new EventHandler(Player_MediaOpened);
            player.MediaFailed += new EventHandler<ExceptionEventArgs>(Player_MediaFailed);
            player.Open(new Uri(path));
        }

        private void Player_MediaFailed(object sender, ExceptionEventArgs e)
        {
            playing = false;
        }

        private void Player_MediaOpened(object sender, EventArgs e)
        {
        }

        private void Player_MediaEnded(object sender, EventArgs e)
        {
            playing = false;
            if (!loop)
                return;
            player.Position = TimeSpan.Zero;
            player.Play();
        }

        public void Stop()
        {
            player.Stop();
            playing = false;
        }

        public void Play(bool loop = false)
        {
            if (playing)
                return;
            playing = true;
            this.loop = loop;
            player.Position = new TimeSpan(0L);
            player.Play();
        }

        public double Volume
        {
            get => player.Volume;
            set => player.Volume = value;
        }

        public void PlayBackgroundMusic(string file)
        {
            backgroundMusic = Stockage.SoundStore.Get(file);
            backgroundMusic.Play(true);
        }

    }
}
