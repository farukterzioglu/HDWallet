using NBitcoin.DataEncoders;

namespace HDWallet.Ed25519.NBitcoin.Base
{
    public class PubKey
    {
        byte[] vch = new byte[0];

        /// <summary>
		/// Create a new Public key from string
		/// </summary>
		public PubKey(string hex)
			: this(Encoders.Hex.DecodeData(hex))
		{

		}

        /// <summary>
		/// Create a new Public key from byte array
		/// </summary>
		public PubKey(byte[] bytes)
		{
            this.vch = bytes;
		}
    }
}