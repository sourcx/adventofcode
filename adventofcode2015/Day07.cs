namespace AdventOfCode2015
{
    class Day07
    {
        public static void Solve()
        {
            SolvePart1();
            SolvePart2();
        }

        private static ushort SolvePart1()
        {
            var circuit = ReadCircuit(File.ReadAllLines("input/7"));
            var signal = circuit.GetWireSignal("a");
            Console.WriteLine($"Signal for 'a' is {signal}");
            return signal;
        }

        private static void SolvePart2()
        {
            var newB = SolvePart1();
            var lines2 = File.ReadAllText("input/7").Replace("1674 -> b", $"{newB} -> b");
            var circuit = ReadCircuit(lines2.Trim().Split("\r\n"));
            var signal = circuit.GetWireSignal("a");
            Console.WriteLine($"Signal for 'a' is {signal}");
        }

        private static Circuit ReadCircuit(string[] lines)
        {
            var circuit = new Circuit();

            foreach (var line in lines)
            {
                Console.WriteLine(line);
                var operation = line.Split(" -> ")[0];
                var id = line.Split(" -> ")[1];

                if (operation.Contains("AND"))
                {
                    var leftVal = operation.Split(" AND ")[0];
                    var rightVal = operation.Split(" AND ")[1];

                    if (ushort.TryParse(leftVal, out var leftValNr))
                    {
                        Wire lhs = new Wire { Val = leftValNr };
                        circuit.Add(leftVal, lhs);
                    }

                    circuit.Add(id, new Wire { Op = "AND", Lhs = leftVal, Rhs = rightVal } );
                }
                else if (operation.Contains("OR"))
                {
                    var leftVal = operation.Split(" OR ")[0];
                    var rightVal = operation.Split(" OR ")[1];

                    circuit.Add(id, new Wire { Op = "OR", Lhs = leftVal, Rhs = rightVal } );
                }
                else if (operation.Contains("NOT"))
                {
                    var lhs = operation.Split("NOT ")[1];
                    circuit.Add(id, new Wire { Op = "NOT", Lhs = lhs } );
                }
                else if (operation.Contains("LSHIFT"))
                {
                    var lhs = operation.Split(" LSHIFT ")[0];
                    var shiftAmount = ushort.Parse(operation.Split(" LSHIFT ")[1]);
                    circuit.Add(id, new Wire { Op = "LSHIFT", Lhs = lhs, ShiftAmount = shiftAmount } );
                }
                else if (operation.Contains("RSHIFT"))
                {
                    var lhs = operation.Split(" RSHIFT ")[0];
                    var shiftAmount = ushort.Parse(operation.Split(" RSHIFT ")[1]);
                    circuit.Add(id, new Wire { Op = "RSHIFT", Lhs = lhs, ShiftAmount = shiftAmount } );
                }
                else // direct value or direct wire
                {
                    if (ushort.TryParse(operation, out var val))
                    {
                        circuit.Add(id, new Wire { Val = val } );
                    }
                    else
                    {
                        circuit.Add(id, new Wire { Lhs = operation } );
                    }
                }
            }

            return circuit;
        }

        class Circuit
        {
            Dictionary<string, Wire> _wires = new Dictionary<string, Wire>();
            Dictionary<string, ushort> _cache = new Dictionary<string, ushort>();

            public void Add(string id, Wire wire)
            {
                if (!_wires.ContainsKey(id))
                {
                    _wires.Add(id, wire);
                }
            }

            public ushort GetWireSignal(string id)
            {
                if (_cache.TryGetValue(id, out ushort res))
                {
                    return res;
                }

                var wire = _wires[id];
                ushort signal = 0;

                if (wire.Op == "AND")
                {
                    signal = (ushort) (GetWireSignal(wire.Lhs) & GetWireSignal(wire.Rhs));
                }
                else if (wire.Op == "OR")
                {
                    signal = (ushort) (GetWireSignal(wire.Lhs) | GetWireSignal(wire.Rhs));
                }
                else if (wire.Op == "NOT")
                {
                    signal = (ushort) (~ GetWireSignal(wire.Lhs));
                }
                else if (wire.Op == "LSHIFT")
                {
                    signal = (ushort) (GetWireSignal(wire.Lhs) << wire.ShiftAmount.Value);
                }
                else if (wire.Op == "RSHIFT")
                {
                    signal = (ushort) (GetWireSignal(wire.Lhs) >> wire.ShiftAmount.Value);
                }
                else
                {
                    signal = (wire.Val ??= GetWireSignal(wire.Lhs));
                }

                _cache.Add(id, signal);
                return signal;
            }
        }

        class Wire
        {
            public ushort? Val;
            public string Op;
            public string Lhs;
            public string Rhs;
            public ushort? ShiftAmount;
        }
    }
}
