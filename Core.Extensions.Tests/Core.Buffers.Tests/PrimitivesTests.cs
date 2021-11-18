using System.Buffers;
using System.Buffers.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Buffers.Tests;

[TestClass]
public class PrimitivesTests
{
    [TestMethod]
    public void ReadDoubleValues()
    {
        var buffer = new byte[16];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteDoubleBigEndian(buffer, 1);

        bool result = Primitives.TryReadDoubleBigEndian(sequence, out double value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadDoubleBigEndian(sequence.Slice(0, 8), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadDoubleBigEndian(sequence.Slice(0, 7), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteDoubleLittleEndian(buffer, 1);

        result = Primitives.TryReadDoubleLittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadDoubleLittleEndian(sequence.Slice(0, 8), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadDoubleLittleEndian(sequence.Slice(0, 7), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }

    [TestMethod]
    public void ReadInt16Values()
    {
        var buffer = new byte[4];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteInt16BigEndian(buffer, 1);

        bool result = Primitives.TryReadInt16BigEndian(sequence, out short value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt16BigEndian(sequence.Slice(0, 2), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt16BigEndian(sequence.Slice(0, 1), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteInt16LittleEndian(buffer, 1);

        result = Primitives.TryReadInt16LittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt16LittleEndian(sequence.Slice(0, 2), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt16LittleEndian(sequence.Slice(0, 1), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }

    [TestMethod]
    public void ReadInt32Values()
    {
        var buffer = new byte[8];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteInt32BigEndian(buffer, 1);

        bool result = Primitives.TryReadInt32BigEndian(sequence, out int value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt32BigEndian(sequence.Slice(0, 4), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt32BigEndian(sequence.Slice(0, 3), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteInt32LittleEndian(buffer, 1);

        result = Primitives.TryReadInt32LittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt32LittleEndian(sequence.Slice(0, 4), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt32LittleEndian(sequence.Slice(0, 3), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }

    [TestMethod]
    public void ReadInt64Values()
    {
        var buffer = new byte[16];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteInt64BigEndian(buffer, 1);

        bool result = Primitives.TryReadInt64BigEndian(sequence, out long value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt64BigEndian(sequence.Slice(0, 8), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt64BigEndian(sequence.Slice(0, 7), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteInt64LittleEndian(buffer, 1);

        result = Primitives.TryReadInt64LittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt64LittleEndian(sequence.Slice(0, 8), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadInt64LittleEndian(sequence.Slice(0, 7), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }

    [TestMethod]
    public void ReadSingleValues()
    {
        var buffer = new byte[8];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteSingleBigEndian(buffer, 1);

        bool result = Primitives.TryReadSingleBigEndian(sequence, out float value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadSingleBigEndian(sequence.Slice(0, 4), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadSingleBigEndian(sequence.Slice(0, 3), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteSingleLittleEndian(buffer, 1);

        result = Primitives.TryReadSingleLittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadSingleLittleEndian(sequence.Slice(0, 4), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadSingleLittleEndian(sequence.Slice(0, 3), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }

    [TestMethod]
    public void ReadUInt16Values()
    {
        var buffer = new byte[4];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteUInt16BigEndian(buffer, 1);

        bool result = Primitives.TryReadUInt16BigEndian(sequence, out ushort value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadUInt16BigEndian(sequence.Slice(0, 2), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadUInt16BigEndian(sequence.Slice(0, 1), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteUInt16LittleEndian(buffer, 1);

        result = Primitives.TryReadUInt16LittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadUInt16LittleEndian(sequence.Slice(0, 2), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual(1, value);

        result = Primitives.TryReadUInt16LittleEndian(sequence.Slice(0, 1), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }

    [TestMethod]
    public void ReadUInt32Values()
    {
        var buffer = new byte[8];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteUInt32BigEndian(buffer, 1);

        bool result = Primitives.TryReadUInt32BigEndian(sequence, out uint value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<uint>(1, value);

        result = Primitives.TryReadUInt32BigEndian(sequence.Slice(0, 4), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<uint>(1, value);

        result = Primitives.TryReadUInt32BigEndian(sequence.Slice(0, 3), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteUInt32LittleEndian(buffer, 1);

        result = Primitives.TryReadUInt32LittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<uint>(1, value);

        result = Primitives.TryReadUInt32LittleEndian(sequence.Slice(0, 4), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<uint>(1, value);

        result = Primitives.TryReadUInt32LittleEndian(sequence.Slice(0, 3), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }

    [TestMethod]
    public void ReadUInt64Values()
    {
        var buffer = new byte[16];
        var sequence = new ReadOnlySequence<byte>(buffer);

        BinaryPrimitives.WriteUInt64BigEndian(buffer, 1);

        bool result = Primitives.TryReadUInt64BigEndian(sequence, out ulong value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<ulong>(1, value);

        result = Primitives.TryReadUInt64BigEndian(sequence.Slice(0, 8), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<ulong>(1, value);

        result = Primitives.TryReadUInt64BigEndian(sequence.Slice(0, 7), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);

        BinaryPrimitives.WriteUInt64LittleEndian(buffer, 1);

        result = Primitives.TryReadUInt64LittleEndian(sequence, out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<ulong>(1, value);

        result = Primitives.TryReadUInt64LittleEndian(sequence.Slice(0, 8), out value);
        Assert.AreEqual(true, result);
        Assert.AreEqual<ulong>(1, value);

        result = Primitives.TryReadUInt64LittleEndian(sequence.Slice(0, 7), out value);
        Assert.AreEqual(false, result);
        Assert.AreEqual(default, value);
    }
}
