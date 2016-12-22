using WpfApplication1.Inspector.Attributes;

namespace WpfApplication1
{
    public enum TestEnum
    {
        [Label("アイテム0")] Item0,
        Item1,
        Item2,
        Item3,
    }

    public class TestSubSubItem
    {
        public string SubSubName { get; set; } = "subsubname";
    }

    public class TestSubItem
    {
        public string SubName { get; set; } = "subname";
        public string SubReadOnlyStr { get; private set; } = "sub readonly";
        public int SubInt { get; set; } = 3;
        public int SubReadOnlyInt { get; private set; } = 4;
        public TestEnum SubEnum { get; set; } = TestEnum.Item2;
        public TestEnum SubReadOnlyEnum { get; private set; } = TestEnum.Item3;
        public TestSubSubItem SubSubItem { get; set; } = new TestSubSubItem();
        public double SubDouble { get; set; } = 0.7;
    }

    public class TestConfigItem
    {
        [Label("名前")] public string Name { get; set; } = "name";
        public string ReadOnlyStr { get; private set; } = "readonly";
        [IntRange(Min = 10, Max = 50)] public int Int { get; set; } = 10;
        public int ReadOnlyInt { get; private set; } = 2;

        public TestSubItem SubItem { get; set; } = new TestSubItem();

        public TestEnum Enum { get; set; } = TestEnum.Item0;
        public TestEnum ReadOnlyEnum { get; private set; } = TestEnum.Item1;

        [Label("浮動小数点数")][DoubleRange(0, 20, 2)] public double Double { get; set; } = 0.5;
    }
}
