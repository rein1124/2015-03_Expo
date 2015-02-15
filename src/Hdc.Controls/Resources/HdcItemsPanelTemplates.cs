using System;
using System.Windows;

namespace Hdc.Controls
{
    public class HdcItemsPanelTemplates
    {
        private static ResourceKey _verticalStackItemsPanelTemplateKey;

        public static ResourceKey VerticalStackItemsPanelTemplateKey
        {
            get
            {
                return _verticalStackItemsPanelTemplateKey ??
                       (_verticalStackItemsPanelTemplateKey =
                        new ComponentResourceKey(typeof (HdcItemsPanelTemplates),
                                                 HdcItemsPanelTemplatesResourceKeyId.VerticalStackItemsPanelTemplate));
            }
        }

        private static ResourceKey _horizontalStackItemsPanelTemplateKey;

        public static ResourceKey HorizontalStackItemsPanelTemplateKey
        {
            get
            {
                return _horizontalStackItemsPanelTemplateKey ??
                       (_horizontalStackItemsPanelTemplateKey =
                        new ComponentResourceKey(typeof (HdcItemsPanelTemplates),
                                                 HdcItemsPanelTemplatesResourceKeyId.HorizontalStackItemsPanelTemplate));
            }
        }

        private static ResourceKey _gridItemsPanelTemplateKey;

        public static ResourceKey GridItemsPanelTemplateKey
        {
            get
            {
                return _gridItemsPanelTemplateKey ??
                       (_gridItemsPanelTemplateKey =
                        new ComponentResourceKey(typeof (HdcItemsPanelTemplates),
                                                 HdcItemsPanelTemplatesResourceKeyId.GridItemsPanelTemplate));
            }
        }

        private static ResourceKey _uniformGridRows4ItemsPanelTemplateKey;

        public static ResourceKey UniformGridRows4ItemsPanelTemplateKey
        {
            get
            {
                return _uniformGridRows4ItemsPanelTemplateKey ??
                       (_uniformGridRows4ItemsPanelTemplateKey =
                        new ComponentResourceKey(typeof (HdcItemsPanelTemplates),
                                                 HdcItemsPanelTemplatesResourceKeyId.UniformGridRows4ItemsPanelTemplate));
            }
        }

        private static ResourceKey _uniformGridColumns2ItemsPanelTemplateKey;

        public static ResourceKey UniformGridColumns2ItemsPanelTemplateKey
        {
            get
            {
                return _uniformGridColumns2ItemsPanelTemplateKey ??
                       (_uniformGridColumns2ItemsPanelTemplateKey =
                        new ComponentResourceKey(typeof (HdcItemsPanelTemplates),
                                                 HdcItemsPanelTemplatesResourceKeyId.UniformGridColumns2ItemsPanelTemplate));
            }
        }

        private static ResourceKey _uniformGridColumns4ItemsPanelTemplateKey;

        public static ResourceKey UniformGridColumns4ItemsPanelTemplateKey
        {
            get
            {
                return _uniformGridColumns4ItemsPanelTemplateKey ??
                       (_uniformGridColumns4ItemsPanelTemplateKey =
                        new ComponentResourceKey(typeof (HdcItemsPanelTemplates),
                                                 HdcItemsPanelTemplatesResourceKeyId.UniformGridColumns4ItemsPanelTemplate));
            }
        }

        static HdcItemsPanelTemplates()
        {
            
        }

        private enum HdcItemsPanelTemplatesResourceKeyId
        {
            VerticalStackItemsPanelTemplate,
            HorizontalStackItemsPanelTemplate,
            GridItemsPanelTemplate,
            UniformGridRows1ItemsPanelTemplate,
            UniformGridRows2ItemsPanelTemplate,
            UniformGridRows3ItemsPanelTemplate,
            UniformGridRows4ItemsPanelTemplate,
            UniformGridColumns1ItemsPanelTemplate,
            UniformGridColumns2ItemsPanelTemplate,
            UniformGridColumns3ItemsPanelTemplate,
            UniformGridColumns4ItemsPanelTemplate,
        }
    }
}