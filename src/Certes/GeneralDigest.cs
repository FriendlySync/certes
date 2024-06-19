using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.Acme
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GeneralDigest
    {
        private const int BYTE_LENGTH = 64;

        private byte[] xBuf;
        private int xBufOff;

        private long byteCount;

        internal GeneralDigest()
        {
            xBuf = new byte[4];
        }

        internal GeneralDigest(GeneralDigest t)
        {
            xBuf = new byte[t.xBuf.Length];
            CopyIn(t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        protected void CopyIn(GeneralDigest t)
        {
            Array.Copy(t.xBuf, 0, xBuf, 0, t.xBuf.Length);

            xBufOff = t.xBufOff;
            byteCount = t.byteCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        public void Update(byte input)
        {
            xBuf[xBufOff++] = input;

            if (xBufOff == xBuf.Length)
            {
                ProcessWord(xBuf, 0);
                xBufOff = 0;
            }

            byteCount++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="inOff"></param>
        /// <param name="length"></param>
        public void BlockUpdate(
            byte[] input,
            int inOff,
            int length)
        {
            length = System.Math.Max(0, length);

            //
            // fill the current word
            //
            int i = 0;
            if (xBufOff != 0)
            {
                while (i < length)
                {
                    xBuf[xBufOff++] = input[inOff + i++];
                    if (xBufOff == 4)
                    {
                        ProcessWord(xBuf, 0);
                        xBufOff = 0;
                        break;
                    }
                }
            }

            //
            // process whole words.
            //
            int limit = length - 3;
            for (; i < limit; i += 4)
            {
                ProcessWord(input, inOff + i);
            }

            //
            // load in the remainder.
            //
            while (i < length)
            {
                xBuf[xBufOff++] = input[inOff + i++];
            }

            byteCount += length;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Finish()
        {
            long bitLength = (byteCount << 3);

            //
            // add the pad bytes.
            //
            Update((byte)128);

            while (xBufOff != 0) Update((byte)0);
            ProcessLength(bitLength);
            ProcessBlock();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Reset()
        {
            byteCount = 0;
            xBufOff = 0;
            Array.Clear(xBuf, 0, xBuf.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetByteLength()
        {
            return BYTE_LENGTH;
        }

        internal abstract void ProcessWord(byte[] input, int inOff);
        internal abstract void ProcessLength(long bitLength);
        internal abstract void ProcessBlock();
        /// <summary>
        /// 
        /// </summary>
        public abstract string AlgorithmName { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract int GetDigestSize();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="outOff"></param>
        /// <returns></returns>
        public abstract int DoFinal(byte[] output, int outOff);
        //public abstract IMemoable Copy();
        //public abstract void Reset(IMemoable t);
    }
}
