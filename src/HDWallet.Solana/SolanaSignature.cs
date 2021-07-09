using HDWallet.Core;

namespace HDWallet.Solana
{
    public class SolanaSignature : Signature
    {
        public byte[] SignatureBytes => Helper.Concat(this.R, this.S);
        public string SignatureHex => Helper.ToHexString(this.SignatureBytes);

        public SolanaSignature(Signature signature)
        {
            this.R = signature.R;
            this.S = signature.S;
            this.RecId= signature.RecId;
        }
    }
}