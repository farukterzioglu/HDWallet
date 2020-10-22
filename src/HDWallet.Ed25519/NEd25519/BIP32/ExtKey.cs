using System;
using dotnetstandard_bip32;
using NBitcoin.Base;

namespace NEd25519
{
    public class ExtKey : ExtKeyBase<Ed25519Key>
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

        protected override (Ed25519Key Key, byte[] ChainCode) SetMaster()
        {
            dotnetstandard_bip32.BIP32 bip32 = new dotnetstandard_bip32.BIP32();
            var (Key, ChainCode) = bip32.GetMasterKeyFromSeed(base.seed.ToStringHex());

            return (new Ed25519Key(Key), ChainCode);
        }

        //
        // Summary:
        //     Derives a new extended key in the hierarchy as the given child number.
        internal ExtKey Derive(uint addressIndex)
        {
            throw new NotImplementedException();
        }
    }
}