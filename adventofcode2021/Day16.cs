using System.Text;

namespace AdventOfCode2021
{
    class Day16
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static void SolvePart1()
        {
            var hex = File.ReadAllText("input/16").Trim();
            var parser = new PacketParser();
            var packet = parser.Parse(hex);

            Console.WriteLine($"Sum of all versions is {packet.VersionSum()}");
        }

        private static void SolvePart2()
        {
            var hex = File.ReadAllText("input/16").Trim();
            var parser = new PacketParser();
            var packet = parser.Parse(hex);

            Console.WriteLine($"Value is {packet.Value}");
        }

        public class PacketParser
        {
            string _bits;
            int _pos;

            public Packet Parse(string hex)
            {
                _bits = HexToBits(hex);
                _pos = 0;

                return ParsePacket();
            }

            private Packet ParsePacket()
            {
                var packet = new Packet();

                packet.Version = Nr(Parse(3));
                var typeId = Nr(Parse(3));

                if (typeId == 4)
                {
                    packet.Value = ParseLiteralValue();
                }
                else
                {
                    packet.Subpackets = ParseSubpackets();
                    switch(typeId)
                    {
                        case 0:
                            packet.Value = packet.Subpackets.Aggregate(0L, (agg, p) => agg + p.Value);
                            break;
                        case 1:
                            packet.Value = packet.Subpackets.Aggregate(1L, (agg, p) => agg * p.Value);
                            break;
                        case 2:
                            packet.Value = packet.Subpackets.Min(p => p.Value);
                            break;
                        case 3:
                            packet.Value = packet.Subpackets.Max(p => p.Value);
                            break;
                        case 5:
                            packet.Value = packet.Subpackets[0].Value > packet.Subpackets[1].Value ? 1 : 0;
                            break;
                        case 6:
                            packet.Value = packet.Subpackets[1].Value < packet.Subpackets[0].Value ? 1 : 0;
                            break;
                        case 7:
                            packet.Value = packet.Subpackets[0].Value == packet.Subpackets[1].Value ? 1 : 0;
                            break;
                    }
                }

                return packet;
            }

            private long ParseLiteralValue()
            {
                bool lastGroup = false;
                var sb = new StringBuilder();

                while (!lastGroup)
                {
                    string lastIndicator = Parse(1);

                    if (lastIndicator == "0")
                    {
                        lastGroup = true;
                    }

                    sb.Append(Parse(4));
                }

                return Nr(sb.ToString());
            }

            private List<Packet> ParseSubpackets()
            {
                var packets = new List<Packet>();
                var lengthTypeId = Parse(1);

                if (lengthTypeId == "0")
                {
                    var totalLengthSubpackets = Nr(Parse(15));
                    int currentPos = _pos;

                    while (_pos - currentPos < totalLengthSubpackets)
                    {
                        packets.Add(ParsePacket());
                    }
                }
                else
                {
                    var nrSubpackets = Nr(Parse(11));

                    for (int i = 0; i < nrSubpackets; i++)
                    {
                        packets.Add(ParsePacket());
                    }
                }

                return packets;
            }

            private static string HexToBits(string hex)
            {
                return hex.Select(s => Convert.ToString(Convert.ToInt64($"{s}", 16), 2).PadLeft(4, '0')).Aggregate("", (agg, s) => $"{agg}{s}");
            }

            private string Parse(int nrBits)
            {
                var res = _bits.Substring(_pos, nrBits);
                _pos += nrBits;
                return res;
            }

            private static long Nr(string bits)
            {
                return Convert.ToInt64(bits, 2);
            }
        }

        public class Packet
        {
            public long Version;
            public long Value;
            public List<Packet> Subpackets = new List<Packet>();

            public long VersionSum()
            {
                var sum = Version;

                if (Subpackets.Count != 0)
                {
                    sum += Subpackets.Aggregate(0L, (agg, p) => agg + p.VersionSum());
                }

                return sum;
            }

            public long ValueSum()
            {
                var sum = Version;

                if (Subpackets.Count != 0)
                {
                    sum += Subpackets.Aggregate(0L, (agg, p) => agg + p.ValueSum());
                }

                return sum;
            }
        }
    }
}
