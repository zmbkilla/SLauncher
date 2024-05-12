using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SLauncher
{
    internal class CRC
    {

        public static void CalculateFolderCrc(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Console.WriteLine($"Folder not found: {folderPath}");
                return;
            }

            var files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                uint crc = CalculateFileCrc(file);
                Console.WriteLine($"CRC32 for {file}: {crc}");
            }
        }

        private static uint CalculateFileCrc(string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var crc32 = new Crc32();

                return crc32.ComputeHash(fileStream);
            }
        }
        public class Crc32 : HashAlgorithm
        {
            public const uint DefaultPolynomial = 0xedb88320;
            public const uint DefaultSeed = 0xffffffff;

            private uint _hash;
            private readonly uint _seed;
            public readonly uint[] _table;
            private static readonly uint[] _defaultTable = InitializeTable(DefaultPolynomial);

            public Crc32()
            {
                _table = InitializeTable(DefaultPolynomial);
                _seed = DefaultSeed;
                Initialize();
            }

            public override void Initialize()
            {
                _hash = _seed;
            }

            protected override void HashCore(byte[] array, int ibStart, int cbSize)
            {
                _hash = CalculateHash(_table, _hash, array, ibStart, cbSize);
            }

            protected override byte[] HashFinal()
            {
                var hashBuffer = UInt32ToBigEndianBytes(~_hash);
                HashValue = hashBuffer;
                return hashBuffer;
            }

            public override int HashSize => 32;

            public uint ComputeHash(Stream stream)
            {
                Initialize();
                var buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    HashCore(buffer, 0, bytesRead);
                }
                return _hash;
            }

            public uint ComputeHash(byte[] buffer)
            {
                Initialize();
                HashCore(buffer, 0, buffer.Length);
                return _hash;
            }

            public uint ComputeHash(byte[] buffer, int offset, int count)
            {
                Initialize();
                HashCore(buffer, offset, count);
                return _hash;
            }

            private static uint[] InitializeTable(uint polynomial)
            {


                if (polynomial == DefaultPolynomial && _defaultTable != null)
                {
                    return _defaultTable;
                }

                var table = new uint[256];
                for (var i = 0; i < 256; i++)
                {
                    var entry = (uint)i;
                    for (var j = 0; j < 8; j++)
                    {
                        if ((entry & 1) == 1)
                        {
                            entry = (entry >> 1) ^ polynomial;
                        }
                        else
                        {
                            entry = entry >> 1;
                        }
                    }
                    table[i] = entry;
                }
                return table;
            }

            private static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
            {
                if (table == null || buffer == null)
                {
                    throw new ArgumentNullException("table or buffer is null");
                }

                if (start < 0 || start >= buffer.Count || size <= 0 || start + size > buffer.Count)
                {
                    throw new ArgumentOutOfRangeException("Invalid start or size parameters");
                }

                var hash = seed;
                for (var i = start; i < start + size; i++)
                {
                    hash = (hash >> 8) ^ table[buffer[i] ^ hash & 0xff];
                }
                return hash;
            }

            private byte[] UInt32ToBigEndianBytes(uint x)
            {
                return new byte[]
                {
            (byte)((x >> 24) & 0xff),
            (byte)((x >> 16) & 0xff),
            (byte)((x >> 8) & 0xff),
            (byte)(x & 0xff)
                };
            }


        }




    }
}
