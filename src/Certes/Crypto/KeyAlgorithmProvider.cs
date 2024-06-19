using System;
using System.Collections.Generic;
using System.IO;

namespace Certes.Crypto
{
    internal class KeyAlgorithmProvider
    {
        private static readonly Dictionary<KeyAlgorithm, IKeyAlgorithm> keyAlgorithms = new Dictionary<KeyAlgorithm, IKeyAlgorithm>()
        {
            { KeyAlgorithm.ES256, new EllipticCurveAlgorithm("P-256", "SHA-256withECDSA", "SHA256") }
        };

        public IKeyAlgorithm Get(KeyAlgorithm algorithm) =>
            keyAlgorithms.TryGetValue(algorithm, out var signer) ? signer : throw new ArgumentException(nameof(algorithm));

        //public IKey GetKey(string pem)
        //{
        //    using (var reader = new StringReader(pem))
        //    {
        //        var pemReader = new PemReader(reader);
        //        var untyped = pemReader.ReadObject();
        //        switch (untyped)
        //        {
        //            case AsymmetricCipherKeyPair keyPair:
        //                return ReadKey(keyPair.Private);
        //            case AsymmetricKeyParameter keyParam:
        //                return ReadKey(keyParam);
        //            default:
        //                throw new NotSupportedException();
        //        }
        //    }
        //}

        //public IKey GetKey(byte[] der)
        //{
        //    var keyParam = PrivateKeyFactory.CreateKey(der);
        //    return ReadKey(keyParam);
        //}

        //internal (KeyAlgorithm, AsymmetricCipherKeyPair) GetKeyPair(byte[] der)
        //{
        //    var keyParam = Org.BouncyCastle.Security.PrivateKeyFactory.CreateKey(der);
        //    return ParseKey(keyParam);
        //}

        //private static (KeyAlgorithm, AsymmetricCipherKeyPair) ParseKey(AsymmetricKeyParameter keyParam)
        //{
        //    if (keyParam is ECPrivateKeyParameters privateKey)
        //    {
        //        var domain = privateKey.Parameters;
        //        var q = domain.G.Multiply(privateKey.D);

        //        DerObjectIdentifier curveId;
        //        KeyAlgorithm algo;
        //        switch(domain.Curve.FieldSize)
        //        {
        //            case 256:
        //                curveId = SecObjectIdentifiers.SecP256r1;
        //                algo = KeyAlgorithm.ES256;
        //                break;
        //            case 384:
        //                curveId = SecObjectIdentifiers.SecP384r1;
        //                algo = KeyAlgorithm.ES384;
        //                break;
        //            case 521:
        //                curveId = SecObjectIdentifiers.SecP521r1;
        //                algo = KeyAlgorithm.ES512;
        //                break;
        //            default:
        //                throw new NotSupportedException();
        //        }

        //        var publicKey = new ECPublicKeyParameters("EC", q, curveId);
        //        return (algo, new AsymmetricCipherKeyPair(publicKey, keyParam));
        //    }
        //    else
        //    {
        //        throw new NotSupportedException();
        //    }
        //}

        //private static IKey ReadKey(AsymmetricKeyParameter keyParam)
        //{
        //    var (algo, keyPair) = ParseKey(keyParam);
        //    return new AsymmetricCipherKey(algo, keyPair);
        //}
    }
}
