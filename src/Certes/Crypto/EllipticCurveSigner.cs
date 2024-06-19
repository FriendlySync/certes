using System;
using System.Linq;
using System.Security.Cryptography;

namespace Certes.Crypto
{
    internal sealed class EllipticCurveSigner: ISigner
    {
        public EllipticCurveSigner(IKey key)
        {
            KeyPair = key;
        }

        //protected override string SigningAlgorithm { get; }

        //protected override string HashAlgorithm { get; }

        private IKey KeyPair { get; }

        public byte[] ComputeHash(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] SignData(byte[] data)
        {
            return KeyPair.SignData(data);
        }
    }
}
