using System;

namespace iTunesBackupParser
{
    internal static class BigEndianHelper
    {
        private static byte[] ReverseBytes(byte[] array, int offset, int count)
        {
            if (offset < 0 || count < 0 || offset + count > array.Length)
                throw new InvalidOperationException(
                    "ReverseBytes: Offset and count must not be negative and (offset + count) must be less or equal than the array's length.");
            byte[] array2 = (byte[])array.Clone();

            for (var i = 0; i < count; i++)
            {
                array2[offset + i] = array[offset + count - i];
            }

            return array2;
        }

        internal static short ToInt16(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
                       ? BitConverter.ToInt16(ReverseBytes(value, startIndex, 2), 0)
                       : BitConverter.ToInt16(value, startIndex);
        }

        internal static ushort ToUInt16(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
           ? BitConverter.ToUInt16(ReverseBytes(value, startIndex, 2), 0)
           : BitConverter.ToUInt16(value, startIndex);

        }

        internal static int ToInt32(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
           ? BitConverter.ToInt32(ReverseBytes(value, startIndex, 4), 0)
           : BitConverter.ToInt32(value, startIndex);
        }

        internal static uint ToUInt32(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
           ? BitConverter.ToUInt32(ReverseBytes(value, startIndex, 4), 0)
           : BitConverter.ToUInt32(value, startIndex);
        }

        internal static long ToInt64(byte[] value, int startIndex)
        {
            return BitConverter.IsLittleEndian
           ? BitConverter.ToInt64(ReverseBytes(value, startIndex, 8), 0)
           : BitConverter.ToInt64(value, startIndex);
        }
    }
}
