using System;
using System.Windows;
using System.Windows.Controls;
using WpfApplication1.Inspector.UIElements;

namespace WpfApplication1.Inspector
{
    /// <summary>
    /// ControlPanel.xaml の相互作用ロジック
    /// </summary>
    public partial class InspectorPanel : UserControl
    {
        #region TargetType
        public static readonly DependencyProperty TargetTypeProperty = DependencyProperty.Register(
            "TargetType",
            typeof(Type),
            typeof(InspectorPanel),
            new PropertyMetadata());
        public Type TargetType
        {
            get { return (Type)GetValue(TargetTypeProperty); }
            set
            {
                UpdatePanel(value, TargetType);
                SetValue(TargetTypeProperty, value);
            }
        }
        #endregion

        #region TargetItem
        public static readonly DependencyProperty TargetItemProperty = DependencyProperty.Register(
            "TargetItem",
            typeof(object),
            typeof(InspectorPanel),
            new PropertyMetadata());
        public object TargetItem
        {
            get { return GetValue(TargetItemProperty); }
            set
            {
                UpdatePanel(TargetType, value);
                SetValue(TargetItemProperty, value);
            }
        }
        #endregion

        public InspectorPanel()
        {
            InitializeComponent();
        }

        public void UpdatePanel()
        {
            UpdatePanel(TargetType, TargetItem);
        }

        private void UpdatePanel(Type type, object item)
        {
            _rootPanel.Children.Clear();

            if (type == null || item == null)
            {
                return;
            }

            foreach (var prop in type.GetProperties())
            {
                var child = PropertyElementFactory.CreateElement(prop, item);
                _rootPanel.Children.Add(child);
            }
        }
    }
}
