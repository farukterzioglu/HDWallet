### Avalanche HD Wallet

An HD wallet for for Avalanche. 
Avalanche HD wallets can be generated from mnemonic (w/ or w/o passphrase) or from extended private key (xprv) and non-hd wallets can be generated from private key.  

Avalanche HD wallets can derive sub accounts, and from that accounts external (deposit) wallets or internal (change) wallets can be derived. Using generated wallets, addresses can be retrived.

#### Samples  
Sample for generating HD Wallet for Avalanche (purpose: BIP44, coin type: 9000) from mnemonic and getting the first account's first deposit wallet;  
```csharp
IHDWallet<AvalancheWallet> wallet = new AvalancheHDWallet("conduct stadium ask orange vast impose depend assume income sail chunk tomorrow life grape dutch", "");
var account0 = wallet.GetAccount(0);
var wallet0 = account0.GetExternalWallet(0);

Assert.AreEqual("X-avax1wn9s0qlpeur87pk2ccxajlj68d5wt3tw3tts8z",wallet0.Address);
```  

Sample for generating a wallet (non-hd) from private key;  
```csharp
var wallet = new AvalancheWallet("c878c962bdebe816addda5dd12aff7f54f5bf1173c32e91dcb4441980ecd3123");
Assert.AreEqual("X-avax1wn9s0qlpeur87pk2ccxajlj68d5wt3tw3tts8z",wallet.Address );
```