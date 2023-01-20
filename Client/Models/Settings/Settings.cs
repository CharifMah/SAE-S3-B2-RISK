using Models.Son;
using Newtonsoft.Json;
using Stockage;
using System.Runtime.Serialization;
using System.Windows.Controls;

namespace Models.Settings
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

        public List<string> AvailableCutlures
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
                _musicVolume = value;
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
        }

        #endregion

        /// <summary>
        /// Save Settings in a json file
        /// </summary>
        public void SaveSettings()
        {
            _saveSettings = new SauveCollection(Environment.CurrentDirectory);
            _saveSettings.Sauver(this, "Settings");
        }

        /// <summary>
        /// Load Settigns from json file
        /// </summary>
        public void LoadSettings()
        {
            _loadSettings = new ChargerCollection(Environment.CurrentDirectory);
            _instance = _loadSettings.Charger<Settings>("Settings");
            if (_instance != null)
            {
                _culturename = _instance._culturename;
                _pleinEcran = _instance._pleinEcran;

                _musique = _instance._musique;
                _backgroundMusic = _instance._backgroundMusic;
                _musicVolume = _instance._musicVolume;
            }


        }
    }
}
