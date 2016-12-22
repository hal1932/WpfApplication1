using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.Inspector.UIElements
{
    class Enum : PropertyElement
    {
        public Enum(PropertyInfo prop, object item)
            : base(prop, item)
        {
            Header = new TextBlock()
            {
                Text = GetLabel(),
                Margin = new Thickness(5, 2.5, 2.5, 2.5),
            };

            var valueInput = new ComboBox
            {
                Margin = new Thickness(2.5, 2.5, 5, 2.5),
                SelectedItem = GetEnumFieldLabel(prop.GetValue(item)),
                IsEnabled = prop.SetMethod.IsPublic,
            };
            ValueInput = valueInput;

            foreach (var child in System.Enum.GetValues(prop.PropertyType))
            {
                var label = GetEnumFieldLabel(child);
                valueInput.Items.Add(label);
                _labelDic[child] = label;
            }

            valueInput.SelectionChanged += (_, __) =>
            {
                var selectedItem = _labelDic.First(x => x.Value == (string)valueInput.SelectedItem).Key;
                if (selectedItem != prop.GetValue(item))
                {
                    prop.SetValue(item, selectedItem);
                }
            };
        }

        private Dictionary<object, string> _labelDic = new Dictionary<object, string>();
    }
}
