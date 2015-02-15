using System.Windows;
using Hdc.Windows;

namespace Hdc.Controls
{
    public class HdcBrushes
    {
        internal static ComponentResourceKey GetResourceKey(object keyId)
        {
            return keyId.GetResourceKey<HdcBrushes>();
        }

        private static ResourceKey _borderBrush;

        public static ResourceKey BorderBrushKey
        {
            get
            {
                return _borderBrush ??
                       (_borderBrush =
                        GetResourceKey(Id.BorderBrush));
            }
        }

        private static ResourceKey _lightBorderBrush;

        public static ResourceKey LightBorderBrushKey
        {
            get
            {
                return _lightBorderBrush ??
                       (_lightBorderBrush =
                        GetResourceKey(Id.LightBorderBrush));
            }
        }

        private static ResourceKey _backgroundBrush;

        public static ResourceKey BackgroundBrushKey
        {
            get
            {
                return _backgroundBrush ??
                       (_backgroundBrush =
                        GetResourceKey(Id.BackgroundBrush));
            }
        }

        private static ResourceKey _accentBrush;

        public static ResourceKey AccentBrushKey
        {
            get
            {
                return _accentBrush ??
                       (_accentBrush =
                        GetResourceKey(Id.AccentBrush));
            }
        }

        private static ResourceKey _transparentBrush;

        public static ResourceKey TransparentBrushKey
        {
            get
            {
                return _transparentBrush ??
                       (_transparentBrush =
                        GetResourceKey(Id.TransparentBrush));
            }
        }

        private static ResourceKey _foregroundBrush;

        public static ResourceKey ForegroundBrushKey
        {
            get
            {
                return _foregroundBrush ??
                       (_foregroundBrush =
                        GetResourceKey(Id.ForegroundBrush));
            }
        }

        private static ResourceKey _contrastForegroundBrush;

        public static ResourceKey ContrastForegroundBrushKey
        {
            get
            {
                return _contrastForegroundBrush ??
                       (_contrastForegroundBrush =
                        GetResourceKey(Id.ContrastForegroundBrush));
            }
        }

        private static ResourceKey _contrastBackgroundBrush;

        public static ResourceKey ContrastBackgroundBrushKey
        {
            get
            {
                return _contrastBackgroundBrush ??
                       (_contrastBackgroundBrush =
                        GetResourceKey(Id.ContrastBackgroundBrush));
            }
        }

        private static ResourceKey _pressedBrush;

        public static ResourceKey PressedBrushKey
        {
            get
            {
                return _pressedBrush ??
                       (_pressedBrush =
                        GetResourceKey(Id.PressedBrush));
            }
        }

        private static ResourceKey _pressedBorderBrush;

        public static ResourceKey PressedBorderBrushKey
        {
            get
            {
                return _pressedBorderBrush ??
                       (_pressedBorderBrush =
                        GetResourceKey(Id.PressedBorderBrush));
            }
        }

        private static ResourceKey _chromeBrush;

        public static ResourceKey ChromeBrushKey
        {
            get
            {
                return _chromeBrush ??
                       (_chromeBrush =
                        GetResourceKey(Id.ChromeBrush));
            }
        }

        private static ResourceKey _disabledBrush;

        public static ResourceKey DisabledBrushKey
        {
            get
            {
                return _disabledBrush ??
                       (_disabledBrush =
                        GetResourceKey(Id.DisabledBrush));
            }
        }

        private static ResourceKey _disabledBackgroundBrush;

        public static ResourceKey DisabledBackgroundBrushKey
        {
            get
            {
                return _disabledBackgroundBrush ??
                       (_disabledBackgroundBrush =
                        GetResourceKey(Id.DisabledBackgroundBrush));
            }
        }

        private static ResourceKey _disabledForegroundBrush;

        public static ResourceKey DisabledForegroundBrushKey
        {
            get
            {
                return _disabledForegroundBrush ??
                       (_disabledForegroundBrush =
                        GetResourceKey(Id.DisabledForegroundBrush));
            }
        }

        private static ResourceKey _disabledBorderBrush;

        public static ResourceKey DisabledBorderBrushKey
        {
            get
            {
                return _disabledBorderBrush ??
                       (_disabledBorderBrush =
                        GetResourceKey(Id.DisabledBorderBrush));
            }
        }


        private static ResourceKey _controlBrush;

        public static ResourceKey ControlBrushKey
        {
            get
            {
                return _controlBrush ??
                       (_controlBrush =
                        GetResourceKey(Id.ControlBrush));
            }
        }

        private static ResourceKey _controlBackgroundBrush;

        public static ResourceKey ControlBackgroundBrushKey
        {
            get
            {
                return _controlBackgroundBrush ??
                       (_controlBackgroundBrush =
                        GetResourceKey(Id.ControlBackgroundBrush));
            }
        }

        private static ResourceKey _controlForegroundBrush;

        public static ResourceKey ControlForegroundBrushKey
        {
            get
            {
                return _controlForegroundBrush ??
                       (_controlForegroundBrush =
                        GetResourceKey(Id.ControlForegroundBrush));
            }
        }

        private static ResourceKey _controlBorderBrush;

        public static ResourceKey ControlBorderBrushKey
        {
            get
            {
                return _controlBorderBrush ??
                       (_controlBorderBrush =
                        GetResourceKey(Id.ControlBorderBrush));
            }
        }

        private static ResourceKey _buttonBrush;

        public static ResourceKey ButtonBrushKey
        {
            get
            {
                return _buttonBrush ??
                       (_buttonBrush =
                        GetResourceKey(Id.ButtonBrush));
            }
        }

        private static ResourceKey _buttonBackgroundBrush;

        public static ResourceKey ButtonBackgroundBrushKey
        {
            get
            {
                return _buttonBackgroundBrush ??
                       (_buttonBackgroundBrush =
                        GetResourceKey(Id.ButtonBackgroundBrush));
            }
        }

        private static ResourceKey _buttonForegroundBrush;

        public static ResourceKey ButtonForegroundBrushKey
        {
            get
            {
                return _buttonForegroundBrush ??
                       (_buttonForegroundBrush =
                        GetResourceKey(Id.ButtonForegroundBrush));
            }
        }

        private static ResourceKey _buttonBorderBrush;

        public static ResourceKey ButtonBorderBrushKey
        {
            get
            {
                return _buttonBorderBrush ??
                       (_buttonBorderBrush =
                        GetResourceKey(Id.ButtonBorderBrush));
            }
        }

        private static ResourceKey _dialogBorderBrushKey;

        public static ResourceKey DialogBorderBrushKey
        {
            get
            {
                return _dialogBorderBrushKey ??
                       (_dialogBorderBrushKey =
                        GetResourceKey(Id.DialogBorderBrush));
            }
        }

        private static ResourceKey _dialogBackgroundBrushKey;

        public static ResourceKey DialogBackgroundBrushKey
        {
            get
            {
                return _dialogBackgroundBrushKey ??
                       (_dialogBackgroundBrushKey =
                        GetResourceKey(Id.DialogBackgroundBrush));
            }
        }

        private static ResourceKey _windowBorderBrushKey;

        public static ResourceKey WindowBorderBrushKey
        {
            get
            {
                return _windowBorderBrushKey ??
                       (_windowBorderBrushKey =
                        GetResourceKey(Id.WindowBorderBrush));
            }
        }

        private static ResourceKey _windowBackgroundBrushKey;

        public static ResourceKey WindowBackgroundBrushKey
        {
            get
            {
                return _windowBackgroundBrushKey ??
                       (_windowBackgroundBrushKey =
                        GetResourceKey(Id.WindowBackgroundBrush));
            }
        }

        private static ResourceKey _titlePanelBackgroundBrushKey;

        public static ResourceKey TitlePanelBackgroundBrushKey
        {
            get
            {
                return _titlePanelBackgroundBrushKey ??
                       (_titlePanelBackgroundBrushKey =
                        GetResourceKey(Id.TitlePanelBackgroundBrush));
            }
        }

        private static ResourceKey _panelBackgroundBrushKey;

        public static ResourceKey PanelBackgroundBrushKey
        {
            get
            {
                return _panelBackgroundBrushKey ??
                       (_panelBackgroundBrushKey =
                        GetResourceKey(Id.PanelBackgroundBrush));
            }
        }

        private static ResourceKey _panelBorderBrushKey;

        public static ResourceKey PanelBorderBrushKey
        {
            get
            {
                return _panelBorderBrushKey ??
                       (_panelBorderBrushKey =
                        GetResourceKey(Id.PanelBorderBrush));
            }
        }

        private static ResourceKey _groupBoxHeaderBackgroundBrushKey;

        public static ResourceKey GroupBoxHeaderBackgroundBrushKey
        {
            get
            {
                return _groupBoxHeaderBackgroundBrushKey ??
                       (_groupBoxHeaderBackgroundBrushKey =
                        GetResourceKey(Id.GroupBoxHeaderBackgroundBrush));
            }
        }

        private static ResourceKey _groupBoxHeaderForegroundBrushKey;

        public static ResourceKey GroupBoxHeaderForegroundBrushKey
        {
            get
            {
                return _groupBoxHeaderForegroundBrushKey ??
                       (_groupBoxHeaderForegroundBrushKey =
                        GetResourceKey(Id.GroupBoxHeaderForegroundBrush));
            }
        }


        private static ResourceKey _buttonMouseOverBackgroundBrushKey;

        public static ResourceKey ButtonMouseOverBackgroundBrushKey
        {
            get
            {
                return _buttonMouseOverBackgroundBrushKey ??
                       (_buttonMouseOverBackgroundBrushKey =
                        GetResourceKey(Id.ButtonMouseOverBackgroundBrush));
            }
        }

        private static ResourceKey _buttonMouseOverForegroundBrushKey;

        public static ResourceKey ButtonMouseOverForegroundBrushKey
        {
            get
            {
                return _buttonMouseOverForegroundBrushKey ??
                       (_buttonMouseOverForegroundBrushKey =
                        GetResourceKey(Id.ButtonMouseOverForegroundBrush));
            }
        }

        private static ResourceKey _buttonPressedBackgroundBrushKey;

        public static ResourceKey ButtonPressedBackgroundBrushKey
        {
            get
            {
                return _buttonPressedBackgroundBrushKey ??
                       (_buttonPressedBackgroundBrushKey =
                        GetResourceKey(Id.ButtonPressedBackgroundBrush));
            }
        }

        private static ResourceKey _buttonPressedForegroundBrushKey;

        public static ResourceKey ButtonPressedForegroundBrushKey
        {
            get
            {
                return _buttonPressedForegroundBrushKey ??
                       (_buttonPressedForegroundBrushKey =
                        GetResourceKey(Id.ButtonPressedForegroundBrush));
            }
        }

        private static ResourceKey _buttonDisabledBackgroundBrushKey;

        public static ResourceKey ButtonDisabledBackgroundBrushKey
        {
            get
            {
                return _buttonDisabledBackgroundBrushKey ??
                       (_buttonDisabledBackgroundBrushKey =
                        GetResourceKey(Id.ButtonDisabledBackgroundBrush));
            }
        }

        private static ResourceKey _buttonDisabledForegroundBrushKey;

        public static ResourceKey ButtonDisabledForegroundBrushKey
        {
            get
            {
                return _buttonDisabledForegroundBrushKey ??
                       (_buttonDisabledForegroundBrushKey =
                        GetResourceKey(Id.ButtonDisabledForegroundBrush));
            }
        }

        private static ResourceKey _buttonMouseOverBorderBrushKey;

        public static ResourceKey ButtonMouseOverBorderBrushKey
        {
            get
            {
                return _buttonMouseOverBorderBrushKey ??
                       (_buttonMouseOverBorderBrushKey =
                        GetResourceKey(Id.ButtonMouseOverBorderBrush));
            }
        }

        private static ResourceKey _buttonPressedBorderBrushKey;

        public static ResourceKey ButtonPressedBorderBrushKey
        {
            get
            {
                return _buttonPressedBorderBrushKey ??
                       (_buttonPressedBorderBrushKey =
                        GetResourceKey(Id.ButtonPressedBorderBrush));
            }
        }

        private static ResourceKey _buttonDisabledBorderBrushKey;

        public static ResourceKey ButtonDisabledBorderBrushKey
        {
            get
            {
                return _buttonDisabledBorderBrushKey ??
                       (_buttonDisabledBorderBrushKey =
                        GetResourceKey(Id.ButtonDisabledBorderBrush));
            }
        }

        private static ResourceKey _PrimaryBrush;

        public static ResourceKey PrimaryBrushKey
        {
            get
            {
                return _PrimaryBrush ??
                       (_PrimaryBrush =
                        GetResourceKey(Id.PrimaryBrush));
            }
        }

        private static ResourceKey _PrimaryBackgroundBrush;

        public static ResourceKey PrimaryBackgroundBrushKey
        {
            get
            {
                return _PrimaryBackgroundBrush ??
                       (_PrimaryBackgroundBrush =
                        GetResourceKey(Id.PrimaryBackgroundBrush));
            }
        }

        private static ResourceKey _PrimaryForegroundBrush;

        public static ResourceKey PrimaryForegroundBrushKey
        {
            get
            {
                return _PrimaryForegroundBrush ??
                       (_PrimaryForegroundBrush =
                        GetResourceKey(Id.PrimaryForegroundBrush));
            }
        }

        private static ResourceKey _PrimaryBorderBrush;

        public static ResourceKey PrimaryBorderBrushKey
        {
            get
            {
                return _PrimaryBorderBrush ??
                       (_PrimaryBorderBrush =
                        GetResourceKey(Id.PrimaryBorderBrush));
            }
        }


        private static ResourceKey _SecondaryBrush;

        public static ResourceKey SecondaryBrushKey
        {
            get
            {
                return _SecondaryBrush ??
                       (_SecondaryBrush =
                        GetResourceKey(Id.SecondaryBrush));
            }
        }

        private static ResourceKey _SecondaryBackgroundBrush;

        public static ResourceKey SecondaryBackgroundBrushKey
        {
            get
            {
                return _SecondaryBackgroundBrush ??
                       (_SecondaryBackgroundBrush =
                        GetResourceKey(Id.SecondaryBackgroundBrush));
            }
        }

        private static ResourceKey _SecondaryForegroundBrush;

        public static ResourceKey SecondaryForegroundBrushKey
        {
            get
            {
                return _SecondaryForegroundBrush ??
                       (_SecondaryForegroundBrush =
                        GetResourceKey(Id.SecondaryForegroundBrush));
            }
        }

        private static ResourceKey _SecondaryBorderBrush;

        public static ResourceKey SecondaryBorderBrushKey
        {
            get
            {
                return _SecondaryBorderBrush ??
                       (_SecondaryBorderBrush =
                        GetResourceKey(Id.SecondaryBorderBrush));
            }
        }

        private enum Id
        {
            // Border
            BorderBrush,
            NormalBorderBrush,
            LightBorderBrush,
            DarkBorderBrush,

            // Special
            TransparentBrush,
            ChromeBrush,

            //
            BackgroundBrush,
            ForegroundBrush,

            // Contrast
            ContrastForegroundBrush,
            ContrastBackgroundBrush,

            // Disabled
            DisabledBrush,
            DisabledBackgroundBrush,
            DisabledForegroundBrush,
            DisabledBorderBrush,

            // MouseOver
            MouseOverBackgroundBrush,
            MouseOverForegroundBrush,

            // Pressed
            PressedBrush,
            PressedBackgroundBrush,
            PressedForegroundBrush,
            PressedBorderBrush,

            // Opacity
            OpacityBackgroundBrush,
            OpacityForegroundBrush,
            OpacityContrastBackgroundBrush,
            OpacityContrastForegroundBrush,

            // Control
            ControlBrush,
            ControlForegroundBrush,
            ControlBackgroundBrush,
            ControlDisabledBrush,
            ControlSubBrush,
            ControlBorderBrush,

            // TextBlock
            TextBlockBrush,

            // Checkbox
            CheckBoxFillNormalBrush,
            CheckBoxDisabledBrush,
            CheckBoxStrokeBrush,

            // RadioButton
            RadioButtonPressedBrush,

            // Button
            ButtonBrush,
            ButtonBorderBrush,
            ButtonBackgroundBrush,
            ButtonForegroundBrush,
            ButtonMouseOverBackgroundBrush,
            ButtonMouseOverForegroundBrush,
            ButtonMouseOverBorderBrush,
            ButtonPressedBackgroundBrush,
            ButtonPressedForegroundBrush,
            ButtonPressedBorderBrush,
            ButtonDisabledBackgroundBrush,
            ButtonDisabledForegroundBrush,
            ButtonDisabledBorderBrush,

            // Accent,
            AccentBrush,
            AccentMagentaBrush,
            AccentPurpleBrush,
            AccentTealBrush,
            AccentLimeBrush,
            AccentBrownBrush,
            AccentPinkBrush,
            AccentOrangeBrush,
            AccentBlueBrush,
            AccentRedBrush,
            AccentGreenBrush,

            // Theme
            ThemeBrush,

            // Window
            WindowBorderBrush,
            WindowBackgroundBrush,

            // Dialog
            DialogBorderBrush,
            DialogBackgroundBrush,

            // TextBox
            TextBoxBackgroundBrush,
            TextBoxForegroundBrush,
            TextBoxBorderBrush,
            TextBoxEditBackgroundBrush,
            TextBoxEditForegroundBrush,
            TextBoxReadOnlyBackgroundBrush,
            TextBoxReadOnlyForegroundBrush,
            TextBoxDisabledBackgroundBrush,
            TextBoxDisabledForegroundBrush,

            // Strip
            StripControlBackgroundBrush,
            StripControlTickStrokeBrush,
            StripControlLockedFillBrush,
            StripControlObjectiveFillBrush,
            ControlObjectiveBrush,

            // Container
            ContainerBackgroundBrush,
            MiddleContainerBackgroundBrush,

            // Panel
            TitlePanelBackgroundBrush,
            PanelBackgroundBrush,
            PanelBorderBrush,

            // Primary
            PrimaryBrush,
            PrimaryBackgroundBrush,
            PrimaryForegroundBrush,
            PrimaryBorderBrush,

            // Secondary
            SecondaryBrush,
            SecondaryBackgroundBrush,
            SecondaryForegroundBrush,
            SecondaryBorderBrush,

            //GroupBox
            GroupBoxHeaderBackgroundBrush,
            GroupBoxHeaderForegroundBrush,
        }
    }
}