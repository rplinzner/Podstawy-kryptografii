using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Parago.Windows;
using RSA_Model;
using RSA_View.ViewModel.Base;

namespace RSA_View.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Private fields

        private bool _areKeysGenerated;
        private bool _isMessageBlinded;
        private bool _canSign;
        private string _messageToSign;
        private Keys _keys = new Keys();
        private Signature _signature = new Signature();
        private List<RSABigInteger> _blindedMessage;
        private List<RSABigInteger> _blindedSign;
        private Window _owner;

        #endregion

        #region Commands

        public ICommand SignCommand { get; }
        public ICommand BlindCommand { get; }
        public ICommand CheckValidationCommand { get; }
        public ICommand GenerateKeysCommand { get; }

        #endregion

        #region Properties

        public List<RSABigInteger> BlindedSign
        {
            get => _blindedSign;
            set
            {
                _blindedSign = value;
                OnPropertyChanged(nameof(BlindedSign));
            }
        }
        public bool AreKeysGenerated
        {
            get => _areKeysGenerated;
            set
            {
                _areKeysGenerated = value;
                OnPropertyChanged(nameof(AreKeysGenerated));
            }
        }

        public bool IsMessageBlinded
        {
            get => _isMessageBlinded;
            set
            {
                _isMessageBlinded = value;
                OnPropertyChanged(nameof(IsMessageBlinded));
            }
        }

        public bool CanSign
        {
            get => _canSign;
            set
            {
                _canSign = value;
                OnPropertyChanged(nameof(CanSign));
            }
        }


        public string MessageToSign
        {
            get => _messageToSign;
            set
            {
                _messageToSign = value;
                CanSign = false;
                IsMessageBlinded = false;
                OnPropertyChanged(nameof(MessageToSign));
            }
        }

        #endregion



        public MainWindowViewModel(Window owner)
        {
            _owner = owner;
            SignCommand = new RelayCommand(Sign);
            BlindCommand = new RelayCommand(Blind);
            CheckValidationCommand = new RelayCommand(CheckValidation);
            GenerateKeysCommand = new RelayCommand(GenerateKeys);
        }

        private async void GenerateKeys()
        {
            ProgressDialog dialog = new ProgressDialog(ProgressDialogSettings.WithLabelOnly)
            {
                Owner = _owner,
                Label = "Generating keys..."
            };

            dialog.Show();
            _owner.IsEnabled = false;
            await Task.Run(() => _keys.GenerateKeys());
            dialog.Close();
            _owner.IsEnabled = true;
            AreKeysGenerated = true;
        }

        private void CheckValidation()
        {
            if (_signature.VerifySignature(_blindedSign, _keys.PublicKey))
                MessageBox.Show("Correct!", "Verification Success", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("Incorrect!", "Verification Fail", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Blind()
        {
            _blindedMessage = _signature.BlindMessage(MessageToSign, _keys.PublicKey);
            IsMessageBlinded = true;
            CanSign = true;
        }

        private async void Sign()
        {
            ProgressDialog dialog = new ProgressDialog(ProgressDialogSettings.WithLabelOnly)
            {
                Owner = _owner,
                Label = "Generating blind signature..."
            };

            dialog.Show();
            _owner.IsEnabled = false;
            BlindedSign = await Task.Run(() => _signature.CreateSignature(_blindedMessage, _keys.PrivateKey));
            dialog.Close();
            _owner.IsEnabled = true;

        }
    }
}
