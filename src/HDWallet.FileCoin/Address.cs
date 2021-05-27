using System;
using HDWallet.Core;

namespace HDWallet.FileCoin
{
    public class Config
    {
        public ushort Size;
    }
    
    public enum Network
    {
        Mainnet = 0,
        Testnet
    }

    public enum Protocol : ushort
    {
        ID = 0,
        SECP256K1 
    }

    public class Address 
    {
        public string Str { get; set; }

        public Protocol Protocol { get; set; }

        public byte[] Payload { get; set; }

        const string MainnetPrefix = "f";
        const string TestnetPrefix = "t";

        const ushort ChecksumHashLength = 4;
        const ushort PayloadHashLength = 20;
        
        static Config ChecksumHashConfig = new Config() { Size = 4 };
        static Config PayloadHashConfig = new Config() { Size = 20 };

        public Network CurrentNetwork { get; }

        const string encodeStd = "abcdefghijklmnopqrstuvwxyz234567";

        public Address (Network network) 
        {
            this.CurrentNetwork = network;
        }

        public static Address WithNetwork(Network network) 
        {
            return new Address(network);
        }

        public Address NewSecp256k1Address(byte[] pubKeyBytes)
        {
            return this.NewAddress(Protocol.SECP256K1, addressHash(pubKeyBytes));
        }

        byte[] addressHash(byte[] ingest) 
        {
            return hash(ingest, PayloadHashConfig);
        }

        byte[] hash (byte[] ingest, Config config)
        {
            var blake2bHashed = BlakeHashExtension.Blake2(ingest, size: config.Size * 8);
            return blake2bHashed;
        }

        Address NewAddress(Protocol protocol, byte[] payload)
        {
            switch (protocol)
            {
                case Protocol.SECP256K1:
                    if(payload.Length != PayloadHashLength)
                    {
                        throw new Exception("ErrInvalidPayload");
                    }
                    break;
                default:
                    throw new Exception("ErrUnknownProtocol");
            }

            this.Protocol = protocol;
            this.Payload = payload;

            return this;
        }

        string encode(Network network, Address addr)
        {
            if(addr == null) 
            {
                throw new Exception("UndefAddressString");
            }

            string ntwk = MainnetPrefix;
            switch (network)
            {
                case Network.Mainnet:
                    ntwk = MainnetPrefix;
                    break;
                case Network.Testnet:
                    ntwk = TestnetPrefix;
                    break;
                default:
                    throw new Exception("ErrUnknownNetwork");
            }

            string strAddr = null;
            switch (addr.Protocol)
            {
                case Protocol.SECP256K1:
                    var cksm = Checksum( Helper.Concat(
                        new byte[] { (byte)addr.Protocol }, 
                        addr.Payload)
                    );

                    var encodedPubKey = EncodeToString(Helper.Concat(addr.Payload, cksm));
                    strAddr = $"{ntwk}{(ushort)addr.Protocol}{encodedPubKey}";
                    break;
                default:
                    throw new Exception("ErrUnknownProtocol");
            }

            return strAddr;
        }

        string EncodeToString(byte[] ingest)
        {
            var addrCh = SimpleBase.Base32.FileCoin.Encode(ingest, padding: false);
            return addrCh;
        }

        byte[] Checksum(byte[] ingest)
        {
            return hash(ingest, ChecksumHashConfig);
        }

        public override string ToString()
        {
            return encode(CurrentNetwork, this);
        }

        // var (
        //     // ErrUnknownNetwork is returned when encountering an unknown network in an address.
        //     ErrUnknownNetwork = errors.New("unknown address network")

        //     // ErrUnknownProtocol is returned when encountering an unknown protocol in an address.
        //     ErrUnknownProtocol = errors.New("unknown address protocol")
        //     // ErrInvalidPayload is returned when encountering an invalid address payload.
        //     ErrInvalidPayload = errors.New("invalid address payload")
        //     // ErrInvalidLength is returned when encountering an address of invalid length.
        //     ErrInvalidLength = errors.New("invalid address length")
        //     // ErrInvalidChecksum is returned when encountering an invalid address checksum.
        //     ErrInvalidChecksum = errors.New("invalid address checksum")
        // )
    }
}