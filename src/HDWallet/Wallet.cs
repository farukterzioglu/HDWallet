using NBitcoin;

namespace HDWallet
{
    public class Wallet
    {
        protected ExtKey _masterKey;
        
        public Wallet(Mnemonic mneumonic)
        {
            byte[] seed = mneumonic.DeriveSeed();
            _masterKey = new ExtKey(seed);
        }

        public Wallet(Mnemonic mneumonic, string passphrase)
        {
            byte[] seed = mneumonic.DeriveSeed(passphrase);
            _masterKey = new ExtKey(seed);
        }
    }
}