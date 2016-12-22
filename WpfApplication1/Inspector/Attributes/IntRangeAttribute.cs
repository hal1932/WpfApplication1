namespace WpfApplication1.Inspector.Attributes
{
    public class IntRangeAttribute : InspectorAttribute
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public IntRangeAttribute(int min = 0, int max = 10)
        {
            Min = min;
            Max = max;
        }
    }
}
