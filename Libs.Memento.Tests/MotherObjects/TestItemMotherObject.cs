using Libs.Memento.Tests.Models;

namespace Libs.Memento.Tests.MotherObjects;

internal static class TestItemMotherObject
{
    public class TestItemMotherObjectData
    {
        public string StringField;
        public int NumberField;
        public TestReferenceFieldType ClassField;
        public TestStructureFieldType StructureField;
    }

    public static TestItemMotherObjectData FullyField => new()
    {
        NumberField = 1,
        StringField = "1",
        ClassField = new TestReferenceFieldType( 1 ),
        StructureField = new TestStructureFieldType( 1 ),
    };

    public static TestItem Build( this TestItemMotherObjectData data ) => new()
    {
        ClassField = data.ClassField,
        NumberField = data.NumberField,
        StringField = data.StringField,
        StructureField = data.StructureField
    };
}