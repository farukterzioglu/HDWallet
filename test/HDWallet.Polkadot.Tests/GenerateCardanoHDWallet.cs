using System;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using HDWallet.Polkadot;
using NUnit.Framework;

namespace HDWallet.Cardano.Tests
{
    /// <summary>
    /// Copy of HDWallet.Ed25519.Tests/GenerateCardanoHDWallet.cs
    /// </summary>
    public class GenerateCardanoHDWallet
    {
        string passPhrase = "Substrate";
        
        [TestCase(
            "scissors invite lock maple supreme raw rapid void congress muscle digital elegant little brisk hair mango congress clump", 
            "TODO")]
        public void ShouldGenerateFromMnemonic(string mnemonic, string expectedSeed)
        {
            PolkadotHDWallet hdWallet = new PolkadotHDWallet(mnemonic, passPhrase);
            Assert.AreEqual(expectedSeed, hdWallet.BIP39Seed);
        }
    }
}