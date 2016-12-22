using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.Inspector.UIElements
{
    class UserObject : PropertyElement
    {
        public override bool UseOriginalContainer { get; protected set; } = true;

        public UserObject(PropertyInfo prop, object item)
            : base(prop, item)
        {
            var container = new GroupBox()
            {
                Header = GetLabel(),
                Margin = new Thickness(3),
                IsEnabled = prop.SetMethod.IsPublic,
            };
            Container = container;

            var childItem = prop.GetValue(item);

            var content = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(1),
            };

            foreach (var childProp in prop.PropertyType.GetProperties())
            {
                var child = PropertyElementFactory.CreateElement(childProp, childItem);
                content.Children.Add(child);
            }
            container.Content = content;
        }
    }
}
