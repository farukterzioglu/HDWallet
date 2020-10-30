using System;
using dotnetstandard_bip32;
using NBitcoin.Base;

namespace NEd25519
{
    /// <summary>
	/// A private Hierarchical Deterministic key
	/// </summary>
    public class ExtKey : ExtKeyBase<Ed25519Key, ExtKey>
    {
        public ExtKey()
        {

        }

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

        protected override (Ed25519Key Key, byte[] ChainCode) SetMaster()
        {
            dotnetstandard_bip32.BIP32 bip32 = new dotnetstandard_bip32.BIP32();
            var (Key, ChainCode) = bip32.GetMasterKeyFromSeed(base.seed.ToStringHex());

            return (new Ed25519Key(Key), ChainCode);
        }
    }
}