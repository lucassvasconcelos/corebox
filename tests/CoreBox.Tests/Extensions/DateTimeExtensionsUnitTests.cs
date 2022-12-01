using System;
using CoreBox.Extensions;
using FluentAssertions;
using Xunit;

namespace CoreBox.Tests.Extensions
{
    public class DateTimeExtensionsUnitTests
    {
        [Theory]
        [InlineData("2021-09-01 00:00:00", 1)]
        [InlineData("2021-09-01 00:00:00", 5)]
        [InlineData("2021-09-01 00:00:00", 10)]
        public void Deve_Validar_SetMonth(DateTime dt, int newMonth)
            => dt.SetMonth(newMonth).Month.Should().Be(newMonth);

        [Theory]
        [InlineData("2021-09-01 00:00:00", 1999)]
        [InlineData("2021-09-01 00:00:00", 2030)]
        [InlineData("2021-09-01 00:00:00", 2021)]
        public void Deve_Validar_SetYear(DateTime dt, int newYear)
            => dt.SetYear(newYear).Year.Should().Be(newYear);

        [Fact]
        public void Deve_Validar_Data()
        {
            var dt = DateTime.Now;
            dt = dt.SetMonth(1);
            dt = dt.SetYear(1999);

            dt.Month.Should().Be(1);
            dt.Year.Should().Be(1999);
        }

        [Theory]
        [InlineData("2021-02-28 00:00:00", "2021-02-01 00:00:00")]
        [InlineData("2022-12-01 00:00:00", "2022-12-01 00:00:00")]
        [InlineData("2022-11-17 00:00:00", "2022-11-01 00:00:00")]
        public void Deve_Obter_O_Primeiro_Dia_Do_Mes(DateTime dt, DateTime result)
            => dt.GetFirstDayOfMonth().Should().Be(result);

        [Theory]
        [InlineData("2016-02-10 00:00:00", "2016-02-29 00:00:00")]
        [InlineData("2021-02-10 00:00:00", "2021-02-28 00:00:00")]
        [InlineData("2024-02-10 00:00:00", "2024-02-29 00:00:00")]
        public void Deve_Obter_O_Ultimo_Dia_Do_Mes(DateTime dt, DateTime result)
            => dt.GetLastDayOfMonth().Should().Be(result);
    }
}