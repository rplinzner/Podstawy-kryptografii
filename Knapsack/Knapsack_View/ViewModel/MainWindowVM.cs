using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Knapsack_Model;
using Knapsack_View.ViewModel.Base;
using Microsoft.Win32;
using Parago.Windows;

namespace Knapsack_View.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        #region fields

        private List<BigNumber> _privateKeyText;
        private BigNumber _modulusText = new BigNumber();
        private BigNumber _multiplierText = new BigNumber();
        private List<BigNumber> _publicKeyText;
        private Window _owner;
        private bool _canEncrypt;
        private string _textToEncrypt;
        private string _encryptedText;
        private string _textToDecryption;
        private bool _canDecrypt;
        private string _decryptedText;
        private SaveFileDialog _sfd;
        private OpenFileDialog _ofd;
        

        #endregion

        #region Commands

        public ICommand GenerateAllData { get; }
        public ICommand SaveAllData { get; }
        public ICommand EncryptButton { get; }
        public ICommand SaveEncryptedButton { get; }
        public ICommand LoadAllData { get; }
        public ICommand TransferButton { get; }
        public ICommand LoadFromFileButton { get; }


        #endregion

        #region props

        public List<BigNumber> PrivateKeyText
        {
            get => _privateKeyText;
            set
            {
                
                _privateKeyText = value;
                OnPropertyChanged(nameof(PrivateKeyText));
            }
        }

        public BigNumber ModulusText
        {
            get => _modulusText;
            set
            {
                _modulusText = value;
                OnPropertyChanged(nameof(ModulusText));
            }
        }

        public BigNumber MultiplierText
        {
            get => _multiplierText;
            set
            {
                _multiplierText = value;
                OnPropertyChanged(nameof(MultiplierText));
            }
        }

        public List<BigNumber> PublicKeyText
        {
            get => _publicKeyText;
            set
            {
                _publicKeyText = value;
                OnPropertyChanged(nameof(PublicKeyText));
            }
        }

        public bool CanEncrypt
        {
            get => _canEncrypt;
            set
            {
                _canEncrypt = value;
                OnPropertyChanged(nameof(CanEncrypt));
            }
        }

        public string TextToEncrypt
        {
            get => _textToEncrypt;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    CanEncrypt = false;
                }
                else
                {
                    CanEncrypt = true;
                }
                _textToEncrypt = value;
                OnPropertyChanged(nameof(TextToEncrypt));
            }
        }

        public string EncryptedText
        {
            get => _encryptedText;
            set
            {
                _encryptedText = value;
                OnPropertyChanged(nameof(EncryptedText));
            }
        }

        public string TextToDecryption
        {
            get => _textToDecryption;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    CanDecrypt = false;
                }
                else
                {
                    CanDecrypt = true;
                }
                _textToDecryption = value;
                OnPropertyChanged(nameof(TextToDecryption));
            }
        }

        public bool CanDecrypt
        {
            get => _canDecrypt;
            set
            {
                _canDecrypt = value;
                OnPropertyChanged(nameof(CanDecrypt));
            }
        }

        public bool DecryptedText
        {
            
        }
        #endregion

        #region ctor

        public MainWindowVM(Window owner)
        {
            _owner = owner;
            GenerateAllData = new RelayCommand(GenerateData);
            SaveAllData = new RelayCommand(SaveData);
            EncryptButton = new RelayCommand(Encrypt);
            SaveEncryptedButton = new RelayCommand(SaveEncrypted);
            LoadAllData = new RelayCommand(LoadAllDataFromFiles);
            TransferButton = new RelayCommand(TransferEncrypted);
            LoadFromFileButton = new RelayCommand(LoadFromFileEncrypted);
            _sfd = new SaveFileDialog()
            {
                Filter = "Key File (*.key)|*.key",
                RestoreDirectory = true,
                AddExtension = true,
            };
            _ofd = new OpenFileDialog()
            {
                Filter = "Key File (*.key)|*.key",
                RestoreDirectory = true,
                Multiselect = false,
                
            };
        }
        #endregion

        #region Methods

        private async void GenerateData()
        {
            ProgressDialog dialog = new ProgressDialog(ProgressDialogSettings.WithLabelOnly)
            {
                Owner = _owner,
                Label = "Generating data in progress"
            };
            dialog.Show();
            _owner.IsEnabled = false;

            PrivateKeyText = await Task.Run(() => Generator.PrivateKey());
            ModulusText = await Task.Run(() => Generator.Modulus(PrivateKeyText));
            MultiplierText = await Task.Run(() => Generator.Multiplier(ModulusText));
            PublicKeyGenerator gen = new PublicKeyGenerator(PrivateKeyText,ModulusText,MultiplierText);
            PublicKeyText = await Task.Run(()=>gen.GetPublicKey());

            dialog.Close();
            _owner.IsEnabled = true;
        }

        private void SaveData()
        {
            
            if (PrivateKeyText != null)
            {
                _sfd.FileName = "PrivateKey";
                _sfd.Title = "Choose directory to save the private key:";

                if (_sfd.ShowDialog() == true)
                {
                    File.WriteAllText(_sfd.FileName, (string)Converters.BigNumberListToStringConverter.Instance.Convert(PrivateKeyText, null, null, null));
                }
            }
            else
            {
                MessageBox.Show("Private key is empty, so will NOT be saved", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            if (ModulusText!=0)
            {
                _sfd.FileName = "Modulus";
                _sfd.Title = "Choose directory to save the modulus:";

                if (_sfd.ShowDialog() == true)
                {
                    File.WriteAllText(_sfd.FileName, (string)Converters.BigNumberToStringConverter.Instance.Convert(ModulusText, null, null, null));
                }
                
            }
            else
            {
                MessageBox.Show("Modulus is empty, so will NOT be saved", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            if (MultiplierText!=0)
            {
                _sfd.FileName = "Multiplier";
                _sfd.Title = "Choose directory to save the multiplier:";

                if (_sfd.ShowDialog() == true)
                {
                    File.WriteAllText(_sfd.FileName, (string)Converters.BigNumberToStringConverter.Instance.Convert(MultiplierText, null, null, null));
                }
            }
            else
            {
                MessageBox.Show("Multiplier is empty, so will NOT be saved", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            if (PublicKeyText != null)
            {
                _sfd.FileName = "PublicKey";
                _sfd.Title = "Choose directory to save the public key:";

                if (_sfd.ShowDialog() == true)
                {
                    File.WriteAllText(_sfd.FileName, (string)Converters.BigNumberListToStringConverter.Instance.Convert(PublicKeyText, null, null, null));
                }
            }
            else
            {
                MessageBox.Show("Public Key is empty, so will NOT be saved", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void Encrypt()
        {
            if (TextToEncrypt == null)
            {
                MessageBox.Show("NO text to encrypt", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            Encryption enc = new Encryption();
            enc.PublicKey = PublicKeyText;
            try
            {
                EncryptedText = enc.Encrypt(TextToEncrypt);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Information", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            

        }

        private void SaveEncrypted()
        {
           
            _sfd.Filter = "Text File (*.txt)|*.txt";
            _sfd.FileName = "EncryptedMessage";

            if (_sfd.ShowDialog() == true)
            {
                File.WriteAllText(_sfd.FileName, EncryptedText );
            }
        }

        private void LoadAllDataFromFiles()
        {
            _ofd.FileName = "PrivateKey";
            string temp;
            if (_ofd.ShowDialog() == true)
            {
                temp = File.ReadAllText(_ofd.FileName);
                PrivateKeyText = (List<BigNumber>)Converters.BigNumberListToStringConverter.Instance.ConvertBack(temp, null, null, null);
            }
            _ofd.FileName = "Modulus";
            if (_ofd.ShowDialog() == true)
            {
                temp = File.ReadAllText(_ofd.FileName);
                ModulusText = (BigNumber)Converters.BigNumberToStringConverter.Instance.ConvertBack(temp, null, null, null);
            }
            _ofd.FileName = "Multiplier";
            if (_ofd.ShowDialog() == true)
            {
                temp = File.ReadAllText(_ofd.FileName);
                MultiplierText = (BigNumber)Converters.BigNumberToStringConverter.Instance.ConvertBack(temp, null, null, null);
            }

            _ofd.FileName = "PublicKey";
            if (_ofd.ShowDialog() == true)
            {
                temp = File.ReadAllText(_ofd.FileName);
                PublicKeyText = (List<BigNumber>)Converters.BigNumberListToStringConverter.Instance.ConvertBack(temp, null, null, null);
            }
        }

        private void TransferEncrypted()
        {
            TextToDecryption = EncryptedText;
        }

        private void LoadFromFileEncrypted()
        {
            _ofd.Filter = "Text File (*.txt)|*.txt";
            _ofd.FileName = "EncryptedMessage";
            
            if (_ofd.ShowDialog() == true)
            {
                TextToDecryption = File.ReadAllText(_ofd.FileName);
            }
        }
        #endregion



    }
}