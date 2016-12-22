using System;
using System.Windows;
using System.Windows.Interactivity;

namespace WpfApplication1.Inspector
{
    public class UpdatePanelAction : TriggerAction<UpdatePanelAction>
    {
        #region InspectorPanel
        public static readonly DependencyProperty InspectorPanelProperty = DependencyProperty.Register(
            "InspectorPanel",
            typeof(InspectorPanel),
            typeof(UpdatePanelAction),
            new PropertyMetadata());

        public InspectorPanel InspectorPanel
        {
            get { return (InspectorPanel)GetValue(InspectorPanelProperty); }
            set { SetValue(InspectorPanelProperty, value); }
        }
        #endregion

        protected override void Invoke(object _)
        {
            if (InspectorPanel == null)
            {
                throw new ArgumentNullException(nameof(InspectorPanel));
            }

            InspectorPanel.UpdatePanel();
        }
    }
}
