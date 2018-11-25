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
        #endregion

        #region Commands

        public ICommand GenerateAllData { get; }
        public ICommand SaveAllData { get; }
        public ICommand EncryptButton { get; }
        public ICommand SaveEncryptedButton { get; }


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
                if (value == null)
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

        #endregion

        #region ctor

        public MainWindowVM(Window owner)
        {
            _owner = owner;
            GenerateAllData = new RelayCommand(GenerateData);
            SaveAllData = new RelayCommand(SaveData);
            EncryptButton = new RelayCommand(Encrypt);
            SaveEncryptedButton = new RelayCommand(SaveEncrypted);
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
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Key File (*.key)|*.key",
                RestoreDirectory = true,
                AddExtension = true,
            };
            if (PrivateKeyText != null)
            {
                sfd.FileName = "PrivateKey";
                sfd.Title = "Choose directory to save the private key:";

                if (sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, (string)Converters.BigNumberListToStringConverter.Instance.Convert(PrivateKeyText, null, null, null));
                }
            }
            else
            {
                MessageBox.Show("Private key is empty, so will NOT be saved", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            if (ModulusText!=0)
            {
                sfd.FileName = "Modulus";
                sfd.Title = "Choose directory to save the modulus:";

                if (sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, (string)Converters.BigNumberToStringConverter.Instance.Convert(ModulusText, null, null, null));
                }
                
            }
            else
            {
                MessageBox.Show("Modulus is empty, so will NOT be saved", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            if (MultiplierText!=0)
            {
                sfd.FileName = "Multiplier";
                sfd.Title = "Choose directory to save the multiplier:";

                if (sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, (string)Converters.BigNumberToStringConverter.Instance.Convert(MultiplierText, null, null, null));
                }
            }
            else
            {
                MessageBox.Show("Multiplier is empty, so will NOT be saved", "Information", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            if (PublicKeyText != null)
            {
                sfd.FileName = "PublicKey";
                sfd.Title = "Choose directory to save the public key:";

                if (sfd.ShowDialog() == true)
                {
                    File.WriteAllText(sfd.FileName, (string)Converters.BigNumberListToStringConverter.Instance.Convert(PublicKeyText, null, null, null));
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
            SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Text File (*.txt)|*.txt",
                RestoreDirectory = true,
                AddExtension = true,
                FileName = "EncryptedMessage"
            };

            if (sfd.ShowDialog() == true)
            {
                File.WriteAllText(sfd.FileName, EncryptedText );
            }
        }
        #endregion



    }
}