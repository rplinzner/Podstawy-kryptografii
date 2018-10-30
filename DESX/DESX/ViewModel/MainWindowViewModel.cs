using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DESX.ViewModel.Base;
using DESX_Model;

namespace DESX.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {

        private Encryptor _encryptor;
        private Decryptor _decryptor;
        private string _textToEncrypt;
        private string _textAfterDecryption;
        private string _encryptedText;
        private string _keyForDes;
        private string _key1ForDesX;
        private string _key2ForDesX;
        public ICommand EncryptCommand { get; }
        public ICommand DecryptCommand { get; }
        public ICommand GenerateKeysCommand { get; }

        public string TextToEncrypt
        {
            get => _textToEncrypt;
            set
            {
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

        public string TextAfterDecryption
        {
            get => _textAfterDecryption;
            set
            {
                _textAfterDecryption = value;
                OnPropertyChanged(nameof(TextAfterDecryption));
            }
        }

        public string KeyForDes
        {
            get => _keyForDes;
            set
            {
                _keyForDes = value;
                OnPropertyChanged(nameof(KeyForDes));
            }
        }

        public string Key1ForDesX
        {
            get => _key1ForDesX;
            set
            {
                _key1ForDesX = value;
                OnPropertyChanged(nameof(Key1ForDesX));
            }
        }

        public string Key2ForDesX
        {
            get => _key2ForDesX;
            set
            {
                _key2ForDesX = value;
                OnPropertyChanged(nameof(Key2ForDesX));
            }
        }

        public MainWindowViewModel()
        {

            EncryptCommand = new RelayCommand(Encrypt);
            DecryptCommand = new RelayCommand(Decrypt);
            GenerateKeysCommand = new RelayCommand(GenerateKeys);
            KeyForDes = String.Empty;
            Key1ForDesX = String.Empty;
            Key2ForDesX = String.Empty;
            TextAfterDecryption = String.Empty;
            EncryptedText = String.Empty;
            TextToEncrypt = String.Empty;

        }

        private void GenerateKeys()
        {
            KeyForDes = Data.Generate64bitKey();
            Key1ForDesX = Data.Generate64bitKey();
            Key2ForDesX = Data.Generate64bitKey();
        }

        private void Decrypt()
        {
            if (KeyForDes.Length != 64 || Key1ForDesX.Length != 64 || Key2ForDesX.Length != 64)
                MessageBox.Show("Wrong number count in keys");
            else
            {
                _decryptor = new Decryptor(KeyForDes, Key2ForDesX, Key1ForDesX);

                TextAfterDecryption = String.Concat(_decryptor.Decrypt(EncryptedText).Where(t => t != '\0'));
            }

        }

        private void Encrypt()
        {
            if (KeyForDes.Length != 64 || Key1ForDesX.Length != 64 || Key2ForDesX.Length != 64)
                MessageBox.Show("Wrong number count in keys");
            else
            {
                _encryptor = new Encryptor(KeyForDes, Key1ForDesX, Key2ForDesX);
                EncryptedText = _encryptor.Encrypt(TextToEncrypt);
            }

        }
    }
}
