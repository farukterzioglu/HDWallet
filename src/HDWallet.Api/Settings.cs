using System;

namespace HDWallet.Api
{
    public class Settings
    {
        public string Mnemonic { get; set; }
        
        public string Passphrase { get; set; }
        
        public string AccountHDKey { get; set; }
        
        public uint? AccountNumber { get; set; }
        
        public void Validate() 
        {
            if(string.IsNullOrWhiteSpace(this.Mnemonic) && string.IsNullOrWhiteSpace(this.AccountHDKey))
            {
                throw new Exception($"Both {nameof(Mnemonic)} and {AccountHDKey} not defined!");
            }

            if(string.IsNullOrWhiteSpace(this.AccountHDKey) == false && AccountNumber == null)
            {
                throw new Exception($"{nameof(AccountNumber)} need to be defined with {nameof(AccountHDKey)}");
            }
        }
    }
}