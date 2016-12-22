using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.Inspector.UIElements
{
    abstract class Number : PropertyElement
    {
        protected abstract Slider CreateSlider();
        protected abstract void SetSliderValue(Slider slider, PropertyInfo prop, object item);
        protected abstract void OnTextBoxValueChanged(TextBox box, Slider slider);
        protected abstract void OnSliderValueChanged(Slider slider, TextBox box, PropertyInfo prop, object item);

        protected Number(PropertyInfo prop, object item)
            : base(prop, item)
        {
            Header = new TextBlock()
            {
                Text = GetLabel(),
                Margin = new Thickness(5, 2.5, 2.5, 2.5),
            };

            var panel = new DockPanel()
            {
                Margin = new Thickness(2.5, 2.5, 5, 2.5),
                IsEnabled = prop.SetMethod.IsPublic,
            };
            ValueInput = panel;

            var valueInputText = new TextBox() { MinWidth = 50 };
            DockPanel.SetDock(valueInputText, Dock.Left);
            panel.Children.Add(valueInputText);

            var valueInputSlider = CreateSlider();
            panel.Children.Add(valueInputSlider);

            valueInputText.TextChanged += (_, __) => OnTextBoxValueChanged(valueInputText, valueInputSlider);
            valueInputSlider.ValueChanged += (_, __) => OnSliderValueChanged(valueInputSlider, valueInputText, prop, item);

            SetSliderValue(valueInputSlider, prop, item);
        }
    }

    class Integer : Number
    {
        public Integer(PropertyInfo prop, object item)
            : base(prop, item)
        { }

        protected override Slider CreateSlider()
        {
            var slider = new Slider()
            {
                SmallChange = 1,
                LargeChange = 1,
            };

            int min, max;
            TryGetRange(out min, out max);

            slider.Minimum = min;
            slider.Maximum = max;

            return slider;
        }

        protected override void SetSliderValue(Slider slider, PropertyInfo prop, object item)
        {
            slider.Value = (int)prop.GetValue(item);
        }

        protected override void OnTextBoxValueChanged(TextBox box, Slider slider)
        {
            int newValue;
            if (int.TryParse(box.Text, out newValue))
            {
                if (slider.Value != newValue)
                {
                    slider.Value = newValue;
                }
            }
        }

        protected override void OnSliderValueChanged(Slider slider, TextBox box, PropertyInfo prop, object item)
        {
            int oldValue;
            if (!int.TryParse(box.Text, out oldValue))
            {
                oldValue = default(int);
            }

            if (slider.Value != oldValue)
            {
                box.Text = ((int)Math.Ceiling(slider.Value)).ToString();
            }

            if ((int)slider.Value != (int)prop.GetValue(item))
            {
                prop.SetValue(item, (int)slider.Value);
            }
        }
    }

    class Double : Number
    {
        public Double(PropertyInfo prop, object item)
            : base(prop, item)
        { }

        protected override Slider CreateSlider()
        {
            var slider = new Slider();

            double min, max;
            TryGetRange(out min, out max, out _fractionalDigits);

            slider.Minimum = min;
            slider.Maximum = max;

            var change = Math.Pow(10, -_fractionalDigits);
            slider.SmallChange = change;
            slider.LargeChange = change;

            return slider;
        }

        protected override void SetSliderValue(Slider slider, PropertyInfo prop, object item)
        {
            slider.Value = (double)prop.GetValue(item);
        }

        protected override void OnTextBoxValueChanged(TextBox box, Slider slider)
        {
            double newValue;
            if (double.TryParse(box.Text, out newValue))
            {
                if (slider.Value != newValue)
                {
                    slider.Value = newValue;
                }
            }
        }

        protected override void OnSliderValueChanged(Slider slider, TextBox box, PropertyInfo prop, object item)
        {
            double oldValue;
            if (!double.TryParse(box.Text, out oldValue))
            {
                oldValue = default(double);
            }

            if (slider.Value != oldValue)
            {
                box.Text = Math.Round(slider.Value, _fractionalDigits).ToString();
            }

            if (slider.Value != (double)prop.GetValue(item))
            {
                prop.SetValue(item, slider.Value);
            }
        }

        private int _fractionalDigits;
    }
}
