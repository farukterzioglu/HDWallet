using System;
using dotnetstandard_bip32;

namespace NEd25519
{
    public class ExtKey : NBitcoin.ExtKeyBase
    {
        /// <summary>
		/// Constructor. Creates a new extended key from the specified seed bytes.
		/// </summary>
		public ExtKey(byte[] seed): base(seed)
		{
		}

        /// <summary>
		/// Constructor. Creates a new extended key from the specified seed bytes.
		/// </summary>
		public ExtKey(string seed): base(seed.HexToByteArray())
		{
		}

        protected override (byte[] Key, byte[] ChainCode) SetMaster()
        {
            dotnetstandard_bip32.BIP32 bip32 = new dotnetstandard_bip32.BIP32();
            return bip32.GetMasterKeyFromSeed(base.seed.ToStringHex());
        }
    }
}