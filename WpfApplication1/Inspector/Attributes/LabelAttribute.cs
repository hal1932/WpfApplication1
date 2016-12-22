namespace WpfApplication1.Inspector.Attributes
{
    public class LabelAttribute : InspectorAttribute
    {
        public string Name { get; private set; }

        public LabelAttribute(string name)
        {
            Name = name;
        }
    }
}
