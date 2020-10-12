using System;
using HDWallet.Core;

namespace HDWallet.Tron
{
    public class TronSignature : Signature
    {
        public byte[] SignatureBytes => Helper.Concat(this.R, this.S, BitConverter.GetBytes(this.RecId));
        public string SignatureHex => Helper.ToHexString(this.SignatureBytes);

        public TronSignature(Signature signature)
        {
            this.R = signature.R;
            this.S = signature.S;
            this.RecId= signature.RecId;
        }
    }
}