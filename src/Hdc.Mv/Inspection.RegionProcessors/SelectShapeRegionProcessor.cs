using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Markup;
using HalconDotNet;

namespace Hdc.Mv.Inspection
{
    [Serializable]
    [ContentProperty("Items")]
    public class SelectShapeRegionProcessor : Collection<SelectShapeEntry>, IRegionProcessor
    {
        public HRegion Process(HRegion region)
        {
            HTuple features = new HTuple();
            HTuple min = new HTuple();
            HTuple max = new HTuple();

            string operation = Operation.ToHalconString();

            HRegion selectedRegion = region;

            foreach (var entry in Items)
            {
                var feature = entry.Feature.ToHalconString();
                features.Append(feature);

                min.Append(entry.Min);
                max.Append(entry.Max);
            }

            region = selectedRegion.SelectShape(features, operation, min, max);
            return region;
        }

        public LogicOperation Operation { get; set; }
    }
}