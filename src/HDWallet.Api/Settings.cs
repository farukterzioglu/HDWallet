using System;

namespace HDWallet.Api
{
    public class Settings
    {
        public string Mnemonic { get; set; }
        
        public string Passphrase { get; set; }
        
        public string AccountHDKey { get; set; }
        
        public uint AccountNumber { get; set; }

        public string[] SelectedCoinEndpoints { get; set; }

        public void Validate() 
        {
            if(string.IsNullOrWhiteSpace(this.Mnemonic) && string.IsNullOrWhiteSpace(this.AccountHDKey))
            {
                throw new Exception($"Both {nameof(Mnemonic)} and {nameof(AccountHDKey)} not defined!");
            }
        }
    }
}