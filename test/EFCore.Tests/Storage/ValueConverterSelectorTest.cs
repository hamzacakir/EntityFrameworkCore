// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage.Converters;
using Microsoft.EntityFrameworkCore.Storage.Converters.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Storage
{
    public class ValueConverterSelectorTest
    {
        private readonly IValueConverterSelector _selector 
            = new ValueConverterSelector(new ValueConverterSelectorDependencies());

        [Fact]
        public void Can_get_converters_for_int_enums()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Queen)).ToList(),
                (typeof(EnumToNumberConverter<Queen, int>), default),
                (typeof(EnumToNumberConverter<Queen, long>), default),
                (typeof(EnumToNumberConverter<Queen, decimal>), default),
                (typeof(EnumToStringConverter<Queen>), new ConverterMappingHints(size: 512)),
                (typeof(CompositeValueConverter<Queen, int, byte[]>), new ConverterMappingHints(size: 4)),
                (typeof(EnumToNumberConverter<Queen, short>), default),
                (typeof(EnumToNumberConverter<Queen, byte>), default),
                (typeof(EnumToNumberConverter<Queen, ulong>), default),
                (typeof(EnumToNumberConverter<Queen, uint>), default),
                (typeof(EnumToNumberConverter<Queen, ushort>), default),
                (typeof(EnumToNumberConverter<Queen, sbyte>), default),
                (typeof(EnumToNumberConverter<Queen, double>), default),
                (typeof(EnumToNumberConverter<Queen, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_ulong_enums()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Gnr)).ToList(),
                (typeof(EnumToNumberConverter<Gnr, ulong>), default),
                (typeof(EnumToNumberConverter<Gnr, decimal>), new ConverterMappingHints(precision: 20, scale: 0)),
                (typeof(EnumToStringConverter<Gnr>), new ConverterMappingHints(size: 512)),
                (typeof(CompositeValueConverter<Gnr, ulong, byte[]>), new ConverterMappingHints(size: 8)),
                (typeof(EnumToNumberConverter<Gnr, int>), default),
                (typeof(EnumToNumberConverter<Gnr, long>), default),
                (typeof(EnumToNumberConverter<Gnr, short>), default),
                (typeof(EnumToNumberConverter<Gnr, byte>), default),
                (typeof(EnumToNumberConverter<Gnr, uint>), default),
                (typeof(EnumToNumberConverter<Gnr, ushort>), default),
                (typeof(EnumToNumberConverter<Gnr, sbyte>), default),
                (typeof(EnumToNumberConverter<Gnr, double>), default),
                (typeof(EnumToNumberConverter<Gnr, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_long_enums()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Velvets)).ToList(),
                (typeof(EnumToNumberConverter<Velvets, long>), default),
                (typeof(EnumToNumberConverter<Velvets, decimal>), new ConverterMappingHints(precision: 20, scale: 0)),
                (typeof(EnumToStringConverter<Velvets>), new ConverterMappingHints(size: 512)),
                (typeof(CompositeValueConverter<Velvets, long, byte[]>), new ConverterMappingHints(size: 8)),
                (typeof(EnumToNumberConverter<Velvets, int>), default),
                (typeof(EnumToNumberConverter<Velvets, short>), default),
                (typeof(EnumToNumberConverter<Velvets, byte>), default),
                (typeof(EnumToNumberConverter<Velvets, ulong>), default),
                (typeof(EnumToNumberConverter<Velvets, uint>), default),
                (typeof(EnumToNumberConverter<Velvets, ushort>), default),
                (typeof(EnumToNumberConverter<Velvets, sbyte>), default),
                (typeof(EnumToNumberConverter<Velvets, double>), default),
                (typeof(EnumToNumberConverter<Velvets, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_byte_enums()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Nwa)).ToList(),
                (typeof(EnumToNumberConverter<Nwa, byte>), default),
                (typeof(EnumToNumberConverter<Nwa, short>), default),
                (typeof(EnumToNumberConverter<Nwa, ushort>), default),
                (typeof(EnumToNumberConverter<Nwa, int>), default),
                (typeof(EnumToNumberConverter<Nwa, uint>), default),
                (typeof(EnumToNumberConverter<Nwa, long>), default),
                (typeof(EnumToNumberConverter<Nwa, ulong>), default),
                (typeof(EnumToNumberConverter<Nwa, decimal>), default),
                (typeof(EnumToStringConverter<Nwa>), new ConverterMappingHints(size: 512)),
                (typeof(CompositeValueConverter<Nwa, byte, byte[]>), new ConverterMappingHints(size: 1)),
                (typeof(EnumToNumberConverter<Nwa, sbyte>), default),
                (typeof(EnumToNumberConverter<Nwa, double>), default),
                (typeof(EnumToNumberConverter<Nwa, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_enum_to_string()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Queen), typeof(string)).ToList(),
                (typeof(EnumToStringConverter<Queen>), new ConverterMappingHints(size: 512)));
        }

        [Fact]
        public void Can_get_converters_for_enum_to_underlying_enum_type()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Queen), typeof(int)).ToList(),
                (typeof(EnumToNumberConverter<Queen, int>), default));
        }

        [Fact]
        public void Can_get_converters_for_enum_to_other_integer_type()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Queen), typeof(sbyte)).ToList(),
                (typeof(EnumToNumberConverter<Queen, sbyte>), default));
        }

        [Fact]
        public void Can_get_converters_for_enum_to_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Queen), typeof(byte[])).ToList(),
                (typeof(CompositeValueConverter<Queen, int, byte[]>), new ConverterMappingHints(size: 4)));
        }

        [Fact]
        public void Can_get_converters_for_int()
        {
            AssertConverters(
                _selector.ForTypes(typeof(int)).ToList(),
                (typeof(CastingConverter<int, long>), default),
                (typeof(CastingConverter<int, decimal>), default),
                (typeof(NumberToStringConverter<int>), new ConverterMappingHints(size: 64)),
                (typeof(NumberToBytesConverter<int>), new ConverterMappingHints(size: 4)),
                (typeof(CastingConverter<int, short>), default),
                (typeof(CastingConverter<int, byte>), default),
                (typeof(CastingConverter<int, ulong>), default),
                (typeof(CastingConverter<int, uint>), default),
                (typeof(CastingConverter<int, ushort>), default),
                (typeof(CastingConverter<int, sbyte>), default),
                (typeof(CastingConverter<int, double>), default),
                (typeof(CastingConverter<int, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_uint()
        {
            AssertConverters(
                _selector.ForTypes(typeof(uint)).ToList(),
                (typeof(CastingConverter<uint, long>), default),
                (typeof(CastingConverter<uint, ulong>), default),
                (typeof(CastingConverter<uint, decimal>), default),
                (typeof(NumberToStringConverter<uint>), new ConverterMappingHints(size: 64)),
                (typeof(NumberToBytesConverter<uint>), new ConverterMappingHints(size: 4)),
                (typeof(CastingConverter<uint, int>), default),
                (typeof(CastingConverter<uint, short>), default),
                (typeof(CastingConverter<uint, byte>), default),
                (typeof(CastingConverter<uint, ushort>), default),
                (typeof(CastingConverter<uint, sbyte>), default),
                (typeof(CastingConverter<uint, double>), default),
                (typeof(CastingConverter<uint, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_sbyte()
        {
            AssertConverters(
                _selector.ForTypes(typeof(sbyte)).ToList(),
                (typeof(CastingConverter<sbyte, short>), default),
                (typeof(CastingConverter<sbyte, int>), default),
                (typeof(CastingConverter<sbyte, long>), default),
                (typeof(CastingConverter<sbyte, decimal>), default),
                (typeof(NumberToStringConverter<sbyte>), new ConverterMappingHints(size: 64)),
                (typeof(NumberToBytesConverter<sbyte>), new ConverterMappingHints(size: 1)),
                (typeof(CastingConverter<sbyte, byte>), default),
                (typeof(CastingConverter<sbyte, ulong>), default),
                (typeof(CastingConverter<sbyte, uint>), default),
                (typeof(CastingConverter<sbyte, ushort>), default),
                (typeof(CastingConverter<sbyte, double>), default),
                (typeof(CastingConverter<sbyte, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_byte()
        {
            AssertConverters(
                _selector.ForTypes(typeof(byte)).ToList(),
                (typeof(CastingConverter<byte, short>), default),
                (typeof(CastingConverter<byte, ushort>), default),
                (typeof(CastingConverter<byte, int>), default),
                (typeof(CastingConverter<byte, uint>), default),
                (typeof(CastingConverter<byte, long>), default),
                (typeof(CastingConverter<byte, ulong>), default),
                (typeof(CastingConverter<byte, decimal>), default),
                (typeof(NumberToStringConverter<byte>), new ConverterMappingHints(size: 64)),
                (typeof(NumberToBytesConverter<byte>), new ConverterMappingHints(size: 1)),
                (typeof(CastingConverter<byte, sbyte>), default),
                (typeof(CastingConverter<byte, double>), default),
                (typeof(CastingConverter<byte, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_double()
        {
            AssertConverters(
                _selector.ForTypes(typeof(double)).ToList(),
                (typeof(CastingConverter<double, decimal>), default),
                (typeof(NumberToStringConverter<double>), new ConverterMappingHints(size: 64)),
                (typeof(NumberToBytesConverter<double>), new ConverterMappingHints(size: 8)),
                (typeof(CastingConverter<double, int>), default),
                (typeof(CastingConverter<double, long>), default),
                (typeof(CastingConverter<double, short>), default),
                (typeof(CastingConverter<double, byte>), default),
                (typeof(CastingConverter<double, ulong>), default),
                (typeof(CastingConverter<double, uint>), default),
                (typeof(CastingConverter<double, ushort>), default),
                (typeof(CastingConverter<double, sbyte>), default),
                (typeof(CastingConverter<double, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_float()
        {
            AssertConverters(
                _selector.ForTypes(typeof(float)).ToList(),
                (typeof(CastingConverter<float, double>), default),
                (typeof(CastingConverter<float, decimal>), default),
                (typeof(NumberToStringConverter<float>), new ConverterMappingHints(size: 64)),
                (typeof(NumberToBytesConverter<float>), new ConverterMappingHints(size: 4)),
                (typeof(CastingConverter<float, int>), default),
                (typeof(CastingConverter<float, long>), default),
                (typeof(CastingConverter<float, short>), default),
                (typeof(CastingConverter<float, byte>), default),
                (typeof(CastingConverter<float, ulong>), default),
                (typeof(CastingConverter<float, uint>), default),
                (typeof(CastingConverter<float, ushort>), default),
                (typeof(CastingConverter<float, sbyte>), default));
        }

        [Fact]
        public void Can_get_converters_for_decimal()
        {
            AssertConverters(
                _selector.ForTypes(typeof(decimal)).ToList(),
                (typeof(NumberToStringConverter<decimal>), new ConverterMappingHints(size: 64)),
                (typeof(NumberToBytesConverter<decimal>), new ConverterMappingHints(size: 16)),
                (typeof(CastingConverter<decimal, int>), default),
                (typeof(CastingConverter<decimal, long>), default),
                (typeof(CastingConverter<decimal, short>), default),
                (typeof(CastingConverter<decimal, byte>), default),
                (typeof(CastingConverter<decimal, ulong>), default),
                (typeof(CastingConverter<decimal, uint>), default),
                (typeof(CastingConverter<decimal, ushort>), default),
                (typeof(CastingConverter<decimal, sbyte>), default),
                (typeof(CastingConverter<decimal, double>), default),
                (typeof(CastingConverter<decimal, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_double_to_float()
        {
            AssertConverters(
                _selector.ForTypes(typeof(double), typeof(float)).ToList(),
                (typeof(CastingConverter<double, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_float_to_double()
        {
            AssertConverters(
                _selector.ForTypes(typeof(float), typeof(double)).ToList(),
                (typeof(CastingConverter<float, double>), default));
        }

        [Fact]
        public void Can_get_explicit_converters_for_numeric_types()
        {
            var types = new[]
            {
                typeof(int), typeof(short), typeof(long), typeof(sbyte),
                typeof(uint), typeof(ushort), typeof(ulong), typeof(byte),
                typeof(decimal), typeof(double), typeof(float)
            };

            foreach (var fromType in types)
            {
                foreach (var toType in types)
                {
                    var converterInfos = _selector.ForTypes(fromType, toType).ToList();

                    if (fromType == toType)
                    {
                        Assert.Empty(converterInfos);
                    }
                    else
                    {
                        Assert.Equal(
                            typeof(CastingConverter<,>).MakeGenericType(fromType, toType),
                            converterInfos.Single().Create().GetType());
                    }
                }
            }
        }

        [Fact]
        public void Can_get_converters_for_char()
        {
            AssertConverters(
                _selector.ForTypes(typeof(char)).ToList(),
                (typeof(CharToStringConverter), new ConverterMappingHints(size: 1)),
                (typeof(CastingConverter<char, int>), default),
                (typeof(CastingConverter<char, ushort>), default),
                (typeof(CastingConverter<char, uint>), default),
                (typeof(CastingConverter<char, long>), default),
                (typeof(CastingConverter<char, ulong>), default),
                (typeof(CastingConverter<char, decimal>), default),
                (typeof(NumberToBytesConverter<char>), new ConverterMappingHints(size: 2)),
                (typeof(CastingConverter<char, short>), default),
                (typeof(CastingConverter<char, byte>), default),
                (typeof(CastingConverter<char, sbyte>), default),
                (typeof(CastingConverter<char, double>), default),
                (typeof(CastingConverter<char, float>), default));
        }

        [Fact]
        public void Can_get_converters_for_char_to_string()
        {
            AssertConverters(
                _selector.ForTypes(typeof(char), typeof(string)).ToList(),
                (typeof(CharToStringConverter), new ConverterMappingHints(size: 1)));
        }

        [Fact]
        public void Can_get_converters_for_char_to_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(char), typeof(byte[])).ToList(),
                (typeof(NumberToBytesConverter<char>), new ConverterMappingHints(size: 2)));
        }

        [Fact]
        public void Can_get_converters_for_char_to_specific_numeric()
        {
            AssertConverters(
                _selector.ForTypes(typeof(char), typeof(ushort)).ToList(),
                (typeof(CastingConverter<char, ushort>), default));
        }

        [Fact]
        public void Can_get_converters_for_bool()
        {
            AssertConverters(
                _selector.ForTypes(typeof(bool)).ToList(),
                (typeof(BoolToZeroOneConverter<int>), default),
                (typeof(BoolToZeroOneConverter<long>), default),
                (typeof(BoolToZeroOneConverter<short>), default),
                (typeof(BoolToZeroOneConverter<byte>), default),
                (typeof(BoolToZeroOneConverter<ulong>), default),
                (typeof(BoolToZeroOneConverter<uint>), default),
                (typeof(BoolToZeroOneConverter<ushort>), default),
                (typeof(BoolToZeroOneConverter<sbyte>), default),
                (typeof(BoolToZeroOneConverter<decimal>), default),
                (typeof(BoolToZeroOneConverter<double>), default),
                (typeof(BoolToZeroOneConverter<float>), default),
                (typeof(BoolToStringConverter), new ConverterMappingHints(size: 1)),
                (typeof(CompositeValueConverter<bool, byte, byte[]>), new ConverterMappingHints(size: 1)));
        }

        [Fact]
        public void Can_get_converters_for_GUID()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Guid)).ToList(),
                (typeof(GuidToBytesConverter), new ConverterMappingHints(size: 16)),
                (typeof(GuidToStringConverter), new ConverterMappingHints(size: 36)));
        }

        [Fact]
        public void Can_get_converters_for_GUID_to_string()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Guid), typeof(string)).ToList(),
                (typeof(GuidToStringConverter), new ConverterMappingHints(size: 36)));
        }

        [Fact]
        public void Can_get_converters_for_GUID_to_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(Guid), typeof(byte[])).ToList(),
                (typeof(GuidToBytesConverter), new ConverterMappingHints(size: 16)));
        }

        [Fact]
        public void Can_get_converters_for_strings()
        {
            AssertConverters(
                _selector.ForTypes(typeof(string)).ToList(),
                (typeof(StringToBytesConverter), default));
        }

        [Fact]
        public void Can_get_converters_for_string_to_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(string), typeof(byte[])).ToList(),
                (typeof(StringToBytesConverter), default));
        }

        [Fact]
        public void Can_get_converters_for_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(byte[])).ToList(),
                (typeof(BytesToStringConverter), default));
        }

        [Fact]
        public void Can_get_converters_for_bytes_to_strings()
        {
            AssertConverters(
                _selector.ForTypes(typeof(byte[]), typeof(string)).ToList(),
                (typeof(BytesToStringConverter), default));
        }

        [Fact]
        public void Can_get_converters_for_DateTime()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTime)).ToList(),
                (typeof(DateTimeToStringConverter), new ConverterMappingHints(size: 48)),
                (typeof(DateTimeToBinaryConverter), default),
                (typeof(CompositeValueConverter<DateTime, long, byte[]>), new ConverterMappingHints(size: 8)));
        }

        [Fact]
        public void Can_get_converters_for_DateTime_to_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTime), typeof(byte[])).ToList(),
                (typeof(CompositeValueConverter<DateTime, long, byte[]>), new ConverterMappingHints(size: 8)));
        }

        [Fact]
        public void Can_get_converters_for_DateTime_to_string()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTime), typeof(string)).ToList(),
                (typeof(DateTimeToStringConverter), new ConverterMappingHints(size: 48)));
        }

        [Fact]
        public void Can_get_converters_for_DateTime_to_long()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTime), typeof(long)).ToList(),
                (typeof(DateTimeToBinaryConverter), default));
        }

        [Fact]
        public void Can_get_converters_for_DateTimeOffset()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTimeOffset)).ToList(),
                (typeof(DateTimeOffsetToStringConverter), new ConverterMappingHints(size: 48)),
                (typeof(DateTimeOffsetToBinaryConverter), default),
                (typeof(DateTimeOffsetToBytesConverter), new ConverterMappingHints(size: 12)));
        }

        [Fact]
        public void Can_get_converters_for_DateTimeOffset_to_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTimeOffset), typeof(byte[])).ToList(),
                (typeof(DateTimeOffsetToBytesConverter), new ConverterMappingHints(size: 12)));
        }

        [Fact]
        public void Can_get_converters_for_DateTimeOffset_to_string()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTimeOffset), typeof(string)).ToList(),
                (typeof(DateTimeOffsetToStringConverter), new ConverterMappingHints(size: 48)));
        }

        [Fact]
        public void Can_get_converters_for_DateTimeOffset_to_long()
        {
            AssertConverters(
                _selector.ForTypes(typeof(DateTimeOffset), typeof(long)).ToList(),
                (typeof(DateTimeOffsetToBinaryConverter), default));
        }

        [Fact]
        public void Can_get_converters_for_TimeSpan()
        {
            AssertConverters(
                _selector.ForTypes(typeof(TimeSpan)).ToList(),
                (typeof(TimeSpanToStringConverter), new ConverterMappingHints(size: 48)),
                (typeof(TimeSpanToTicksConverter), default),
                (typeof(CompositeValueConverter<TimeSpan, long, byte[]>), new ConverterMappingHints(size: 8)));
        }

        [Fact]
        public void Can_get_converters_for_TimeSpan_to_bytes()
        {
            AssertConverters(
                _selector.ForTypes(typeof(TimeSpan), typeof(byte[])).ToList(),
                (typeof(CompositeValueConverter<TimeSpan, long, byte[]>), new ConverterMappingHints(size: 8)));
        }

        [Fact]
        public void Can_get_converters_for_TimeSpan_to_string()
        {
            AssertConverters(
                _selector.ForTypes(typeof(TimeSpan), typeof(string)).ToList(),
                (typeof(TimeSpanToStringConverter), new ConverterMappingHints(size: 48)));
        }

        [Fact]
        public void Can_get_converters_for_TimeSpan_to_long()
        {
            AssertConverters(
                _selector.ForTypes(typeof(TimeSpan), typeof(long)).ToList(),
                (typeof(TimeSpanToTicksConverter), default));
        }

        private static void AssertConverters(
            IList<ValueConverterInfo> converterInfos,
            params (Type InfoType, ConverterMappingHints Hints)[] converterTypes)
        {
            Assert.Equal(converterTypes.Length, converterInfos.Count);

            for (var i = 0; i < converterTypes.Length; i++)
            {
                var converter = converterInfos[i].Create();
                Assert.Equal(converterTypes[i].InfoType, converter.GetType());
                AssertHints(converterTypes[i].Hints, converterInfos[i].MappingHints);
                AssertHints(converterTypes[i].Hints, converter.MappingHints);
            }
        }

        private static void AssertHints(ConverterMappingHints expected, ConverterMappingHints actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(actual.IsFixedLength, expected.IsFixedLength);
            Assert.Equal(actual.IsUnicode, expected.IsUnicode);
            Assert.Equal(actual.Precision, expected.Precision);
            Assert.Equal(actual.Scale, expected.Scale);
            Assert.Equal(actual.Size, expected.Size);
        }

        private enum Queen
        {
            Freddie,
            Brian,
            Rodger,
            John
        }

        private enum Nwa : byte
        {
            Yella,
            Dre,
            Eazy,
            Cube,
            Ren
        }

        private enum Gnr : ulong
        {
            Axl,
            Duff,
            Slash,
            Izzy,
            Stephen
        }

        private enum Velvets : long
        {
            Lou,
            John,
            Sterling,
            Maureen
        }
    }
}
