using Models.Son;
using Stockage;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace Models
{
    [DataContract]
    public class Settings
    {
        #region Attribute
        
        private Page actualPageName;
        private ICharge _loadSettings;
        private ISauve _saveSettings;

        [DataMember]
        private bool _pleinEcran;
        [DataMember]
        private string _culturename;
        [DataMember]
        private List<string> _availableCulture;
        [DataMember]
        private bool _musique;
 
        private Sound _backgroundMusic;

        [DataMember]
        private double _musicVolume;
        #endregion

        #region Property
        /// <summary>
        /// True if full screen else False
        /// </summary>
        public bool PleinEcran
        {
            get { return _pleinEcran; }
            set { _pleinEcran = value; }
        }

        /// <summary>
        /// Allow to chose the language
        /// </summary>
        public string Culturename
        {
            get { return _culturename; }
            set
            {
                _culturename = value;
            }
        }

        public List<String> AvailableCutlures
        {
            get
            {
                return _availableCulture;
            }
            set
            {
                _availableCulture = value;
            }
        }

        public Page ActualPage { get => actualPageName; set => actualPageName = value; }
        
        public bool MusiqueOnOff 
        { 
            get { return _musique; }
            set { _musique = value; }
        }

        public double Volume
        {
            get
            {
                return _musicVolume;
            }
            set
            {
               _musicVolume= value;
            }
        }
       

        public Sound Backgroundmusic
        {
            get { return _backgroundMusic; }
            set
            {
                _backgroundMusic = value;
            }
        }

        #endregion

        #region Singleton

        private static Settings _instance;

        /// <summary>
        /// Get or create an instance
        /// </summary>
        /// <returns>instance settings</returns>
        /// <Author>Charif</Author>
        public static Settings Get()
        {
            if (_instance == null)
                _instance = new Settings();
            return _instance;
        }

        /// <summary>
        /// Save settings in a Json file
        /// </summary>
        /// <Author>Charif</Author>
        private Settings()
        {
            _availableCulture = new List<string>() { "fr-FR", "en-US" };
            _pleinEcran = false;
            _culturename = Thread.CurrentThread.CurrentCulture.Name;
            LoadSettings();       
        }

        #endregion

        public void SaveSettings()
        {
            _saveSettings = new SauveCollection(Environment.CurrentDirectory);
            _saveSettings.Sauver(this,"Settings");
        }

        private void LoadSettings()
        {
            _loadSettings = new ChargerCollection(Environment.CurrentDirectory);
            Settings instance = _loadSettings.Charger<Settings>("Settings");
            if (instance != null)
            {
                this._culturename = instance._culturename;
                this._pleinEcran = instance._pleinEcran;
                       
                this._musique = instance._musique;
                this._backgroundMusic = instance._backgroundMusic;
                this._musicVolume = instance._musicVolume;
            }

            
        }
    }
}
