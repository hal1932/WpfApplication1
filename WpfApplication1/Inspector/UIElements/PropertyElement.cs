using System.Reflection;
using System.Windows;
using WpfApplication1.Inspector.Attributes;

namespace WpfApplication1.Inspector.UIElements
{
    abstract class PropertyElement
    {
        public virtual bool UseOriginalContainer { get; protected set; } = false;

        public UIElement Header { get; protected set; }
        public UIElement ValueInput { get; protected set; }

        public UIElement Container { get; protected set; }

        public PropertyElement(PropertyInfo prop, object item)
        {
            _prop = prop;
            _item = item;
        }

        protected string GetLabel()
        {
            var label = _prop.GetCustomAttribute<LabelAttribute>();
            return (label != null) ? label.Name : _prop.Name;
        }

        protected string GetEnumFieldLabel(object enumItem)
        {
            var itemName = enumItem.ToString();
            var label = _prop.PropertyType.GetField(itemName)
                ?.GetCustomAttribute<LabelAttribute>();
            return (label != null) ? label.Name : itemName;
        }

        protected bool TryGetRange(out int min, out int max)
        {
            var range = _prop.GetCustomAttribute<IntRangeAttribute>();
            if (range != null)
            {
                min = range.Min;
                max = range.Max;
                return true;
            }

            min = 0;
            max = 100;
            return false;
        }

        private PropertyInfo _prop;
        private object _item;
    }
}
