using System;
using Veronica.Backend.Domain.Models;
using Xunit;

namespace Veronica.Backend.Domain.Tests
{
    public class UnitTests
    {
        [Fact]
        public void ConvertTo_ShouldFailWhenConvertingFromAnItemUnitType()
        {
            var unit1 = Unit.Item;
            var unit2 = Unit.Kilogram;

            Assert.Throws<InvalidOperationException>(() => unit1.ConvertTo(unit2));
        }

        [Fact]
        public void ConvertTo_ShouldFailWhenConvertingToAnItemUnitType()
        {
            var unit1 = Unit.Kilogram;
            var unit2 = Unit.Item;

            Assert.Throws<InvalidOperationException>(() => unit1.ConvertTo(unit2));
        }

        [Fact]
        public void ConvertTo_ShouldConvertBetweenTheSameUnitTypes()
        {
            var unit1 = Unit.Kilogram;
            var unit2 = Unit.Kilogram;

            var result = unit1.ConvertTo(unit2);

            Assert.Equal(1, result);
        }

        [Fact]
        public void ConvertTo_ShouldConvertBetweenTheSameTypesOfUnitType()
        {
            // 5 kilograms should equal 11.02 pounds approximately
            var unit1 = Unit.Kilogram;
            var unit2 = Unit.Pound;

            var result = 5 * unit1.ConvertTo(unit2);

            Assert.Equal(11.02m, Math.Round(result, 2));
        }

        [Fact]
        public void ConvertTo_ShouldFailToConvertBetweenWeightAndVolumeWhenDensityIsNotGiven()
        {
            var unit1 = Unit.Kilogram;
            var unit2 = Unit.Liter;

            Assert.Throws<InvalidOperationException>(() => unit1.ConvertTo(unit2));
        }

        [Fact]
        public void ConvertTo_ShouldConvertFromWeightToVolume()
        {
            // 1 kg milk equals 0.97 liters
            var milkDensity = 1.03m;
            var unit1 = Unit.Kilogram;
            var unit2 = Unit.Liter;

            var result = unit1.ConvertTo(unit2, milkDensity);

            Assert.Equal(1 / milkDensity, result);
        }

        [Fact]
        public void ConvertTo_ShouldConvertFromVolumeToWeight()
        {
            // 1 l milk equals 1030 g
            var milkDensity = 1.03m;
            var unit1 = Unit.Liter;
            var unit2 = Unit.Gram;

            var result = unit1.ConvertTo(unit2, milkDensity);

            Assert.Equal(1030, result);
        }
    }
}
