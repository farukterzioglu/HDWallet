namespace HDWallet.Core
{
    public class Signature
    {
        public string SignatureHex { get; set; }
        public byte[] R;
        public byte[] S;
        public int RecId;
    }
}