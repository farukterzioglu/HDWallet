namespace HDWallet.Core
{
    public class Signature
    {
        public string SignatureHex { get; set; }
        public byte[] SignatureBytes => SignatureHex.FromHexToByteArray(); 
        public byte[] R;
        public byte[] S;
        public int RecId;
    }
}