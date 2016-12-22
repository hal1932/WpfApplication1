namespace WpfApplication1.Inspector.Attributes
{
    public class DoubleRangeAttribute : InspectorAttribute
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public int FractionalDigits { get; set; }

        public DoubleRangeAttribute(double min = 0, double max = 10, int fractionalDigits = 1)
        {
            Min = min;
            Max = max;
            FractionalDigits = fractionalDigits;
        }
    }
}
