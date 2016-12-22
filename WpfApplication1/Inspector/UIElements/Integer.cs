using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.Inspector.UIElements
{
    class Integer : PropertyElement
    {
        public Integer(PropertyInfo prop, object item)
            :base(prop, item)
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

            int min, max;
            TryGetRange(out min, out max);

            var valueInputSlider = new Slider()
            {
                TickFrequency = 1,

                Minimum = min,
                Maximum = max,

                SmallChange = 1,
                LargeChange = 1,
            };
            panel.Children.Add(valueInputSlider);

            SetValueSync(valueInputText, valueInputSlider);

            valueInputSlider.ValueChanged += (_, __) =>
            {
                if ((int)valueInputSlider.Value != (int)prop.GetValue(item))
                {
                    prop.SetValue(item, (int)valueInputSlider.Value);
                }
            };

            var value = (int)prop.GetValue(item);

            var modifiedValue = value;
            if (value < min) modifiedValue = min;
            if (value > max) modifiedValue = max;

            valueInputSlider.Value = modifiedValue;
        }

        private void SetValueSync(TextBox box, Slider slider)
        {
            box.TextChanged += (_, __) =>
            {
                int newValue;
                if (int.TryParse(box.Text, out newValue))
                {
                    if (slider.Value != newValue)
                    {
                        slider.Value = newValue;
                    }
                }
            };

            slider.ValueChanged += (_, __) =>
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
            };
        }
    }
}
