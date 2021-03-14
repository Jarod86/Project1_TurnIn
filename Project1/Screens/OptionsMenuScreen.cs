

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Project1.Screens
{
    // The options screen is brought up over the top of the main menu
    // screen, and gives the user a chance to configure the game
    // in various hopefully useful ways.
    public class OptionsMenuScreen : MenuScreen
    {


        private readonly MenuEntry _masterVolume;
        private readonly MenuEntry _backgroundMusic;


        bool temp = true;


        public OptionsMenuScreen() : base("Options")
        {
            _masterVolume = new MenuEntry(string.Empty);
            _backgroundMusic = new MenuEntry(string.Empty);


            SetMenuEntryText();

            var back = new MenuEntry("Back");

            _masterVolume.Selected += MasterVolumeEntrySelected;
            _backgroundMusic.Selected += BackGroundMusicEntrySelected;
  
            back.Selected += OnCancel;

            MenuEntries.Add(_masterVolume);
            MenuEntries.Add(_backgroundMusic);
     
            MenuEntries.Add(back);
        }

        // Fills in the latest values for the options screen menu text.
        private void SetMenuEntryText()
        {
            _masterVolume.Text = $"Master Volume: {SoundEffect.MasterVolume.ToString()}";
            _backgroundMusic.Text = $"Background Music Volume: {MediaPlayer.Volume.ToString()}";
 
        }

        private void MasterVolumeEntrySelected(object sender, PlayerIndexEventArgs e)
        {

            if (SoundEffect.MasterVolume == 0)
            {
                temp = false;
            }
            else if (SoundEffect.MasterVolume == 1)
            {
                temp = true;
            }
            if (SoundEffect.MasterVolume > 0 && temp)
            {
                SoundEffect.MasterVolume -= .25f;
                MediaPlayer.Volume -= .25f;

            }
            else if(SoundEffect.MasterVolume < 1 && !temp)
            { 
                SoundEffect.MasterVolume += .25f;
                MediaPlayer.Volume += .25f;
            }
            
            
            SetMenuEntryText();
        }

        private void BackGroundMusicEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (MediaPlayer.Volume == 0)
            {
                temp = false;
            }
            else if (MediaPlayer.Volume == 1)
            {
                temp = true;
            }
            if (MediaPlayer.Volume > 0 && temp)
            {
               
                MediaPlayer.Volume -= .25f;

            }
            else if (MediaPlayer.Volume < 1 && !temp)
            {
               
                MediaPlayer.Volume += .25f;
            }
            SetMenuEntryText();
        }

     
    }
}
