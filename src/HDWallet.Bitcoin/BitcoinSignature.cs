using HDWallet.Core;

namespace HDWallet.Bitcoin
{
    public class BitcoinSignature : Signature
    {
        public byte[] SignatureBytes => Helper.Concat(this.R, this.S);
        public string SignatureHex => Helper.ToHexString(this.SignatureBytes);

        public BitcoinSignature(Signature signature)
        {
            this.R = signature.R;
            this.S = signature.S;
            this.RecId= signature.RecId;
        }
    }
}