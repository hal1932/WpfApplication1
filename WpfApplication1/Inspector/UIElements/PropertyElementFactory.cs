using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.Inspector.UIElements
{
    public static class PropertyElementFactory
    {
        public static UIElement CreateElement(PropertyInfo prop, object item)
        {
            var element = CreateImpl(prop, item);
            if (element.UseOriginalContainer)
            {
                return element.Container;
            }
            else
            {
                var items = new DockPanel()
                {
                    Margin = new Thickness(1),
                };

                DockPanel.SetDock(element.Header, Dock.Left);
                items.Children.Add(element.Header);

                items.Children.Add(element.ValueInput);

                return items;
            }
        }

        private static PropertyElement CreateImpl(PropertyInfo prop, object item)
        {
            var type = prop.PropertyType;

            if (type == typeof(string)) return new String(prop, item);
            else if (type == typeof(int)) return new Integer(prop, item);
            else if (type == typeof(double)) return new Double(prop, item);
            else if (type.IsEnum) return new Enum(prop, item);

            return new UserObject(prop, item);
        }
    }
}
