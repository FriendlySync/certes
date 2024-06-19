using System;
using System.IO;
using Certes.Jws;

namespace Certes.Crypto
{
    //internal class AsymmetricCipherKey : IKey
    //{
    //    public JsonWebKey JsonWebKey
    //    {
    //        get
    //        {
    //            var ecKey = (ECPublicKeyParameters)KeyPair.Public;
    //            var curve = "P-256";

    //            // https://tools.ietf.org/html/rfc7518#section-6.2.1.2
    //            // get the byte representation of the x & y coords on the Elliptic Curve,
    //            // with padding bytes to the required field length

    //            var xBytes = ecKey.Q.AffineXCoord.GetEncoded();
    //            var yBytes = ecKey.Q.AffineYCoord.GetEncoded();

    //            return new EcJsonWebKey
    //            {
    //                KeyType = "EC",
    //                Curve = curve,
    //                X = JwsConvert.ToBase64String(xBytes),
    //                Y = JwsConvert.ToBase64String(yBytes)
    //            };
    //        }
    //    }

    //    public AsymmetricCipherKeyPair KeyPair { get; }

    //    public KeyAlgorithm Algorithm { get; }

    //    public AsymmetricCipherKey(KeyAlgorithm algorithm, AsymmetricCipherKeyPair keyPair)
    //    {
    //        KeyPair = keyPair ?? throw new ArgumentNullException(nameof(keyPair));
    //        Algorithm = algorithm;
    //    }

    //    public byte[] ToDer()
    //    {
    //        var privateKey = PrivateKeyInfoFactory.CreatePrivateKeyInfo(KeyPair.Private);
    //        return privateKey.GetDerEncoded();
    //    }

    //    public string ToPem()
    //    {
    //        using (var sr = new StringWriter())
    //        {
    //            var pemWriter = new PemWriter(sr);
    //            pemWriter.WriteObject(KeyPair);
    //            return sr.ToString();
    //        }
    //    }
    //}
}
