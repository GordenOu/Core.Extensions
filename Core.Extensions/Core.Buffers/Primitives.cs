using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Core.Buffers;

/// <summary>
/// Reads bytes as primitives with specific endianness.
/// </summary>
public unsafe static class Primitives
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryRead16BitBigEndian<T>(ReadOnlySequence<byte> source, out T value)
        where T : unmanaged
    {
        Debug.Assert(sizeof(T) == 2);

        var reader = new SequenceReader<byte>(source);
        if (reader.TryReadBigEndian(out short shortValue))
        {
            T* pValue = (T*)&shortValue;
            value = *pValue;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryRead16BitLittleEndian<T>(ReadOnlySequence<byte> source, out T value)
        where T : unmanaged
    {
        Debug.Assert(sizeof(T) == 2);

        var reader = new SequenceReader<byte>(source);
        if (reader.TryReadLittleEndian(out short shortValue))
        {
            T* pValue = (T*)&shortValue;
            value = *pValue;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryRead32BitBigEndian<T>(ReadOnlySequence<byte> source, out T value)
        where T : unmanaged
    {
        Debug.Assert(sizeof(T) == 4);

        var reader = new SequenceReader<byte>(source);
        if (reader.TryReadBigEndian(out int intValue))
        {
            T* pValue = (T*)&intValue;
            value = *pValue;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryRead32BitLittleEndian<T>(ReadOnlySequence<byte> source, out T value)
        where T : unmanaged
    {
        Debug.Assert(sizeof(T) == 4);

        var reader = new SequenceReader<byte>(source);
        if (reader.TryReadLittleEndian(out int intValue))
        {
            T* pValue = (T*)&intValue;
            value = *pValue;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryRead64BitBigEndian<T>(ReadOnlySequence<byte> source, out T value)
        where T : unmanaged
    {
        Debug.Assert(sizeof(T) == 8);

        var reader = new SequenceReader<byte>(source);
        if (reader.TryReadBigEndian(out long longValue))
        {
            T* pValue = (T*)&longValue;
            value = *pValue;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool TryRead64BitLittleEndian<T>(ReadOnlySequence<byte> source, out T value)
        where T : unmanaged
    {
        Debug.Assert(sizeof(T) == 8);

        var reader = new SequenceReader<byte>(source);
        if (reader.TryReadLittleEndian(out long longValue))
        {
            T* pValue = (T*)&longValue;
            value = *pValue;
            return true;
        }
        else
        {
            value = default;
            return false;
        }
    }

    /// <summary>
    /// Reads a System.Double from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Double; otherwise, false.</returns>
    public static bool TryReadDoubleBigEndian(ReadOnlySequence<byte> source, out double value)
    {
        return TryRead64BitBigEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.Double from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Double; otherwise, false.</returns>
    public static bool TryReadDoubleLittleEndian(ReadOnlySequence<byte> source, out double value)
    {
        return TryRead64BitLittleEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.Int16 from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Int16; otherwise, false.</returns>
    public static bool TryReadInt16BigEndian(ReadOnlySequence<byte> source, out short value)
    {
        var reader = new SequenceReader<byte>(source);
        return reader.TryReadBigEndian(out value);
    }

    /// <summary>
    /// Reads a System.Int16 from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Int16; otherwise, false.</returns>
    public static bool TryReadInt16LittleEndian(ReadOnlySequence<byte> source, out short value)
    {
        var reader = new SequenceReader<byte>(source);
        return reader.TryReadLittleEndian(out value);
    }

    /// <summary>
    /// Reads a System.Int32 from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Int32; otherwise, false.</returns>
    public static bool TryReadInt32BigEndian(ReadOnlySequence<byte> source, out int value)
    {
        var reader = new SequenceReader<byte>(source);
        return reader.TryReadBigEndian(out value);
    }

    /// <summary>
    /// Reads a System.Int32 from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Int32; otherwise, false.</returns>
    public static bool TryReadInt32LittleEndian(ReadOnlySequence<byte> source, out int value)
    {
        var reader = new SequenceReader<byte>(source);
        return reader.TryReadLittleEndian(out value);
    }

    /// <summary>
    /// Reads a System.Int64 from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Int64; otherwise, false.</returns>
    public static bool TryReadInt64BigEndian(ReadOnlySequence<byte> source, out long value)
    {
        var reader = new SequenceReader<byte>(source);
        return reader.TryReadBigEndian(out value);
    }

    /// <summary>
    /// Reads a System.Int64 from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Int64; otherwise, false.</returns>
    public static bool TryReadInt64LittleEndian(ReadOnlySequence<byte> source, out long value)
    {
        var reader = new SequenceReader<byte>(source);
        return reader.TryReadLittleEndian(out value);
    }

    /// <summary>
    /// Reads a System.Single from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Single; otherwise, false.</returns>
    public static bool TryReadSingleBigEndian(ReadOnlySequence<byte> source, out float value)
    {
        return TryRead32BitBigEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.Single from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.Single; otherwise, false.</returns>
    public static bool TryReadSingleLittleEndian(ReadOnlySequence<byte> source, out float value)
    {
        return TryRead32BitLittleEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.UInt16 from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.UInt16; otherwise, false.</returns>
    public static bool TryReadUInt16BigEndian(ReadOnlySequence<byte> source, out ushort value)
    {
        return TryRead16BitBigEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.UInt16 from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.UInt16; otherwise, false.</returns>
    public static bool TryReadUInt16LittleEndian(ReadOnlySequence<byte> source, out ushort value)
    {
        return TryRead16BitLittleEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.UInt32 from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.UInt32; otherwise, false.</returns>
    public static bool TryReadUInt32BigEndian(ReadOnlySequence<byte> source, out uint value)
    {
        return TryRead32BitBigEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.UInt32 from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.UInt32; otherwise, false.</returns>
    public static bool TryReadUInt32LittleEndian(ReadOnlySequence<byte> source, out uint value)
    {
        return TryRead32BitLittleEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.UInt64 from the beginning of a read-only sequence of bytes, as big endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as big endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.UInt64; otherwise, false.</returns>
    public static bool TryReadUInt64BigEndian(ReadOnlySequence<byte> source, out ulong value)
    {
        return TryRead64BitBigEndian(source, out value);
    }

    /// <summary>
    /// Reads a System.UInt64 from the beginning of a read-only sequence of bytes, as little endian.
    /// </summary>
    /// <param name="source">The read-only sequence of bytes to read.</param>
    /// <param name="value">
    /// When this method returns, contains the value read out of the read-only sequence of bytes, as little endian.
    /// </param>
    /// <returns>true if the span is large enough to contain a System.UInt64; otherwise, false.</returns>
    public static bool TryReadUInt64LittleEndian(ReadOnlySequence<byte> source, out ulong value)
    {
        return TryRead64BitLittleEndian(source, out value);
    }
}
