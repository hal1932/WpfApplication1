using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.Inspector.UIElements
{
    class String : PropertyElement
    {
        public String(PropertyInfo prop, object item)
            : base(prop, item)
        {
            Header = new TextBlock()
            {
                Text = GetLabel(),
                Margin = new Thickness(5, 2.5, 2.5, 2.5),
            };

            var valueInput = new TextBox()
            {
                Text = prop.GetValue(item) as string,
                Margin = new Thickness(2.5, 2.5, 5, 2.5),
                IsEnabled = prop.SetMethod.IsPublic,
            };
            ValueInput = valueInput;

            valueInput.TextChanged += (_, __) =>
            {
                if (valueInput.Text != prop.GetValue(item) as string)
                {
                    prop.SetValue(item, valueInput.Text);
                }
            };
        }
    }
}
