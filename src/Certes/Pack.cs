using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Acme
{
    internal static class Pack
    {
        internal static void UInt32_To_BE(uint n, byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt32BigEndian(bs, n);
#else
            bs[0] = (byte)(n >> 24);
            bs[1] = (byte)(n >> 16);
            bs[2] = (byte)(n >> 8);
            bs[3] = (byte)n;
#endif
        }

        internal static void UInt32_To_BE(uint n, byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            BinaryPrimitives.WriteUInt32BigEndian(bs.AsSpan(off), n);
#else
            bs[off] = (byte)(n >> 24);
            bs[off + 1] = (byte)(n >> 16);
            bs[off + 2] = (byte)(n >> 8);
            bs[off + 3] = (byte)n;
#endif
        }

        internal static void UInt32_To_BE_High(uint n, byte[] bs, int off, int len)
        {
            Debug.Assert(1 <= len && len <= 4);

            int pos = 24;
            bs[off] = (byte)(n >> pos);
            for (int i = 1; i < len; ++i)
            {
                pos -= 8;
                bs[off + i] = (byte)(n >> pos);
            }
        }

        internal static void UInt32_To_BE_Low(uint n, byte[] bs, int off, int len)
        {
            UInt32_To_BE_High(n << ((4 - len) << 3), bs, off, len);
        }

        internal static void UInt32_To_BE(uint[] ns, byte[] bs, int off)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                UInt32_To_BE(ns[i], bs, off);
                off += 4;
            }
        }

        internal static void UInt32_To_BE(uint[] ns, int nsOff, int nsLen, byte[] bs, int bsOff)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                UInt32_To_BE(ns[nsOff + i], bs, bsOff);
                bsOff += 4;
            }
        }

        internal static byte[] UInt32_To_BE(uint n)
        {
            byte[] bs = new byte[4];
            UInt32_To_BE(n, bs);
            return bs;
        }

        internal static byte[] UInt32_To_BE(uint[] ns)
        {
            byte[] bs = new byte[4 * ns.Length];
            UInt32_To_BE(ns, bs, 0);
            return bs;
        }

        internal static uint BE_To_UInt32(byte[] bs)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt32BigEndian(bs);
#else
            return (uint)bs[0] << 24
                | (uint)bs[1] << 16
                | (uint)bs[2] << 8
                | bs[3];
#endif
        }

        internal static uint BE_To_UInt32(byte[] bs, int off)
        {
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return BinaryPrimitives.ReadUInt32BigEndian(bs.AsSpan(off));
#else
            return (uint)bs[off] << 24
                | (uint)bs[off + 1] << 16
                | (uint)bs[off + 2] << 8
                | bs[off + 3];
#endif
        }

        internal static uint BE_To_UInt32_High(byte[] bs, int off, int len)
        {
            return BE_To_UInt32_Low(bs, off, len) << ((4 - len) << 3);
        }

        internal static uint BE_To_UInt32_Low(byte[] bs, int off, int len)
        {
            Debug.Assert(1 <= len && len <= 4);

            uint result = bs[off];
            for (int i = 1; i < len; ++i)
            {
                result <<= 8;
                result |= bs[off + i];
            }
            return result;
        }

        internal static void BE_To_UInt32(byte[] bs, int off, uint[] ns)
        {
            for (int i = 0; i < ns.Length; ++i)
            {
                ns[i] = BE_To_UInt32(bs, off);
                off += 4;
            }
        }

        internal static void BE_To_UInt32(byte[] bs, int bsOff, uint[] ns, int nsOff, int nsLen)
        {
            for (int i = 0; i < nsLen; ++i)
            {
                ns[nsOff + i] = BE_To_UInt32(bs, bsOff);
                bsOff += 4;
            }
        }
    }
}
