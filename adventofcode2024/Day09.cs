namespace adventofcode2024;

class Day09
{
    readonly struct FileBlock(ulong? fileId)
    {
        public readonly ulong? FileId = fileId;

        public readonly bool IsFree
        {
            get
            {
                return FileId == null;
            }
        }

        public override string ToString()
        {
            return IsFree ? $"FileBlock free" : $"FileBlock FileId {FileId}";
        }
    }

    public void Run()
    {
        Console.WriteLine(GetType().Name);

        var fileName = "input/9";
        RunPart1(fileName);
        RunPart2(fileName);
    }

    private void RunPart1(string fileName)
    {
        var diskMap = File.ReadAllText(fileName);
        var fileBlocks = ToBlocks(diskMap);
        // Print(fileBlocks);

        Compress(fileBlocks); // In place compression
        // Print(fileBlocks);

        var checksum = Checksum(fileBlocks);
        Console.WriteLine($"What is the resulting filesystem checksum? {checksum}");
    }

    private List<FileBlock> ToBlocks(string diskMap)
    {
        var fileBlocks = new List<FileBlock>();
        ulong currentId = 0;

        foreach (var c in diskMap)
        {
            if (c == '\n')
            {
                break;
            }

            ulong? fileId = null;

            if (currentId % 2 == 0)
            {
                fileId = currentId / 2;
            }

            for (int i = 0; i < int.Parse($"{c}"); i++)
            {
                var fileBlock = new FileBlock(fileId);
                fileBlocks.Add(fileBlock);
            }

            currentId += 1;
        }

        return fileBlocks;
    }

    private void Print(List<FileBlock> fileBlocks)
    {
        Console.WriteLine("Compact view of FileBlock list (only works well for single digits).");

        foreach (var fileBlock in fileBlocks)
        {
            Console.Write(fileBlock.IsFree ? "." : fileBlock.FileId);
        }

        // Console.WriteLine("\n\nFull view of FileBlock list (verbose).");

        // foreach (var fileBlock in fileBlocks)
        // {
        //     Console.WriteLine(fileBlock);
        // }

        Console.WriteLine();
    }

    private void Compress(List<FileBlock> fileBlocks)
    {
        var freeSpotIndex = GetNextFreeSpot(fileBlocks, 0);
        var blockToMoveIndex = GetNextBlockToMoveIndex(fileBlocks, fileBlocks.Count - 1);

        while (freeSpotIndex < blockToMoveIndex && freeSpotIndex != -1 && blockToMoveIndex != -1)
        {
            (fileBlocks[freeSpotIndex], fileBlocks[blockToMoveIndex]) = (fileBlocks[blockToMoveIndex], fileBlocks[freeSpotIndex]);

            freeSpotIndex = GetNextFreeSpot(fileBlocks, freeSpotIndex);
            blockToMoveIndex = GetNextBlockToMoveIndex(fileBlocks, blockToMoveIndex);
        }
    }

    private void CompressPart2(List<FileBlock> fileBlocks)
    {
        var blockToMoveIndex = GetNextBlockToMoveIndexPart2(fileBlocks, fileBlocks.Count - 1, null);
        var blockToMoveFileId = fileBlocks[blockToMoveIndex].FileId;
        var blockToMoveSize = GetBlockToMoveSize(fileBlocks, blockToMoveIndex, blockToMoveFileId);

        while (true)
        {
            MoveFileBlock(fileBlocks, blockToMoveIndex, blockToMoveSize);
            blockToMoveIndex = GetNextBlockToMoveIndexPart2(fileBlocks, blockToMoveIndex, blockToMoveFileId);

            if (blockToMoveIndex == -1)
            {
                break;
            }

            blockToMoveFileId = fileBlocks[blockToMoveIndex].FileId;
            blockToMoveSize = GetBlockToMoveSize(fileBlocks, blockToMoveIndex, blockToMoveFileId);
        }
    }

    private void MoveFileBlock(List<FileBlock> fileBlocks, int blockToMoveIndex, int blockToMoveSize)
    {
        var freeSpotIndex = GetNextFreeSpot(fileBlocks, 0);
        var freeSpotSize = GetFreeSpotSize(fileBlocks, freeSpotIndex);

        while (freeSpotIndex < blockToMoveIndex && freeSpotIndex != -1 && blockToMoveIndex != -1)
        {
            // If it fits move the entire block.
            if (blockToMoveSize <= freeSpotSize)
            {
                for (int i = 0; i < blockToMoveSize; i++)
                {
                    (fileBlocks[freeSpotIndex + i], fileBlocks[blockToMoveIndex - i]) = (fileBlocks[blockToMoveIndex - i], fileBlocks[freeSpotIndex + i]);
                }

                return;
            }

            freeSpotIndex = GetNextFreeSpot(fileBlocks, freeSpotIndex);
            freeSpotSize = GetFreeSpotSize(fileBlocks, freeSpotIndex);
        }
    }

    private int GetNextFreeSpot(List<FileBlock> fileBlocks, int currentIndex)
    {
        for (int i = currentIndex + 1; i < fileBlocks.Count; i++)
        {
            if (fileBlocks[i].IsFree)
            {
                return i;
            }
        }

        return -1;
    }

    private int GetFreeSpotSize(List<FileBlock> fileBlocks, int freeSpotIndex)
    {
        int size = 1;

        for (int i = freeSpotIndex + 1; i < fileBlocks.Count; i++)
        {
            if (fileBlocks[i].IsFree)
            {
                size += 1;
            }
            else
            {
                return size;
            }
        }

        return size;
    }


    private int GetBlockToMoveSize(List<FileBlock> fileBlocks, int blockToMoveIndex, ulong? fileId)
    {
        int size = 1;

        for (int i = blockToMoveIndex - 1; i > 0; i--)
        {
            if (fileBlocks[i].FileId == fileId)
            {
                size += 1;
            }
            else
            {
                return size;
            }
        }

        return size;
    }

    private int GetNextBlockToMoveIndex(List<FileBlock> fileBlocks, int currentIndex)
    {
        for (int i = currentIndex; i > 0; i--)
        {
            if (!fileBlocks[i].IsFree)
            {
                return i;
            }
        }

        return -1;
    }

    private int GetNextBlockToMoveIndexPart2(List<FileBlock> fileBlocks, int currentIndex, ulong? lastMovedBlockFileId)
    {
        for (int i = currentIndex; i > 0; i--)
        {
            if (!fileBlocks[i].IsFree && fileBlocks[i].FileId != lastMovedBlockFileId)
            {
                return i;
            }
        }

        return -1;
    }

    private ulong Checksum(List<FileBlock> fileBlocks)
    {
        var checksum = 0UL;

        for (int i = 0; i < fileBlocks.Count; i++)
        {
            if (fileBlocks[i].FileId.HasValue)
            {
                checksum += fileBlocks[i].FileId.Value * (ulong)i;
            }
        }

        return checksum;
    }

    private void RunPart2(string fileName)
    {
        var diskMap = File.ReadAllText(fileName);
        var fileBlocks = ToBlocks(diskMap);
        // Print(fileBlocks);

        CompressPart2(fileBlocks); // In place compression
        Print(fileBlocks);

        var checksum = Checksum(fileBlocks);
        Console.WriteLine($"What is the resulting filesystem checksum? {checksum}");
    }
}
