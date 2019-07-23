using System;

namespace Veronica.Backend.Domain.Models
{
    /// <summary>
    /// Unit in which the quantity of an ingredient is specified in a recipe.
    /// </summary>
    public struct Unit
    {
        public static readonly Unit Gram = new Unit("g", UnitType.Weight);
        public static readonly Unit Kilogram = new Unit("kg", UnitType.Weight, 1000);
        public static readonly Unit Pound = new Unit("lb", UnitType.Weight, 453.59237m);
        public static readonly Unit Ounce = new Unit("oz", UnitType.Weight, Pound.InBaseUnits / 16); // 16 ounces in a pound


        public static readonly Unit Milliliter = new Unit("ml", UnitType.Volume, 1);
        public static readonly Unit Centiliter = new Unit("cl", UnitType.Volume, 10);
        public static readonly Unit Deciliter = new Unit("dl", UnitType.Volume, 100);
        public static readonly Unit Liter = new Unit("l", UnitType.Volume, 1000);
        // Imperial fluid ounces are slightly less, but I'm unsure if we need to define this unit
        // public static readonly Unit ImperialFluidOunces = new Unit("fl oz", UnitType.Volume, 28.4m);
        public static readonly Unit USFluidOunce = new Unit("fl oz", UnitType.Volume, 29.57m);

        public static readonly Unit Cup = new Unit("cup", UnitType.Volume, 240);
        public static readonly Unit Tablespoon = new Unit("tbsp", UnitType.Volume, 15);
        public static readonly Unit Teaspoon = new Unit("tsp", UnitType.Volume, 5);


        public static readonly Unit Item = new Unit("", UnitType.Items);

        private Unit(string value, UnitType type)
        {
            Value = value;
            Type = type;
            InBaseUnits = 1;
        }

        private Unit(string value, UnitType type, decimal inBaseUnits)
        {
            Value = value;
            Type = type;
            InBaseUnits = inBaseUnits;
        }

        public string Value { get; }
        public UnitType Type { get; }
        public decimal InBaseUnits { get; }
        public bool IsBaseUnit => InBaseUnits == 1;

        public decimal ConvertTo(Unit otherUnit)
        {
            return ConvertTo(otherUnit, null); // Prefer method overloads over default parameter values
        }

        public decimal ConvertTo(Unit otherUnit, decimal? density)
        {
            if (Type == UnitType.Items || otherUnit.Type == UnitType.Items)
            {
                // TODO Provide keyed error message for translation purposes
                throw new InvalidOperationException("Converting items from or to weight or volume is not supported."); 
            }

            // Simple conversion
            if (Type == otherUnit.Type)
            {
                return InBaseUnits / otherUnit.InBaseUnits;
            }

            // Converting from weight to volume or volume to weight needs density to work
            if (density == null)
            {
                // TODO Provide keyed error message for translation purposes
                throw new InvalidOperationException("Converting weight from or to volume needs the density of the ingredient.");
            }

            // Convert weight to volume or vice-versa needs to factor in the density of the ingredient
            var conversionFactor = InBaseUnits; // Start by going to the base unit. 1 ml == 1 g
            if (Type == UnitType.Volume)
            {
                conversionFactor *= density.Value; // Factor in the density. I.e. for milk: 1 ml = 1.03 g
            }
            else // Type == UnitType.Weight
            {
                conversionFactor /= density.Value; // Factor in the density. I.e. for milk: 1 g = 0.97 ml (or, more accurately, 0.970873786407767...)
            }
            return conversionFactor / otherUnit.InBaseUnits;
        }
    }

    public enum UnitType
    {
        Weight,
        Volume,
        Items
    }
}