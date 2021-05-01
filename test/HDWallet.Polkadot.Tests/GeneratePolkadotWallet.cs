using System;
using HDWallet.Core;
using HDWallet.Ed25519.Sample;
using HDWallet.Polkadot;
using NUnit.Framework;

namespace HDWallet.Cardano.Tests
{
    public class GeneratePolkadotWallet
    {
        [Test]
        public void ShouldGenerateFromPrivateKey()
        {
            var privateKey = "f5e5767cf153319517630f226876b86c8160cc583bc013744c6bf255f5cc0ee5";
            PolkadotWallet wallet = new PolkadotWallet(privateKey);

            Assert.AreEqual(
                "278117fc144c72340f67d0f2316e8386ceffbf2b2428c9c51fef7c597f1d426e", 
                wallet.PublicKey.ToHexString());
        }

        [Test]
        public void ShouldSignFromPrivateKey()
        {
            var privateKey = "f5e5767cf153319517630f226876b86c8160cc583bc013744c6bf255f5cc0ee5";
            PolkadotWallet wallet = new PolkadotWallet(privateKey);

            var message  = "A81056D713AF1FF17B599E60D287952E89301B5208324A0529B62DC7369C745D".FromHexToByteArray();
            var signature = wallet.Sign(message);

            Assert.AreEqual(
                "10b6aacb0beca6ca60b712fb5db54e957cec304489366544d96f3e59ac2d4328be7b6602ec98e622c0f16ab427eb497d6ef053e00ddfdb3d3f3b6496b0b17a0c", 
                $"{signature.R.ToHexString()}{signature.S.ToHexString()}");
        }
    }
}