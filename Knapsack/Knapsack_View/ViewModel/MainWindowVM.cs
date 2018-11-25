using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Knapsack_Model;
using Knapsack_View.ViewModel.Base;
using Parago.Windows;

namespace Knapsack_View.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        #region fields

        private List<BigNumber> _privateKeyText;
        private BigNumber _modulusText;
        private BigNumber _multiplierText;
        private List<BigNumber> _publicKeyText;
        private Window _owner;
        #endregion

        #region Commands

        public ICommand GenerateAllData { get; }


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

        #endregion

        #region ctor

        public MainWindowVM(Window owner)
        {
            _owner = owner;
            GenerateAllData = new RelayCommand(GenerateData);
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
        

        #endregion



    }
}