namespace Libs.Memento.Tests.Models;

public class MementoTestItem : IMemento
{
    private readonly TestItem _originalItem;
    
    public string? StringField = null;
    public int? NumberField = null;
    public TestReferenceFieldType? ClassField = null;
    public TestStructureFieldType? StructureField = null;

    public MementoTestItem( TestItem originalItem )
    {
        _originalItem = originalItem;
    }

    public static MementoTestItem Initial( TestItem originalItem ) => new( originalItem )
    {
        StringField = originalItem.StringField,
        ClassField = originalItem.ClassField,
        NumberField = originalItem.NumberField,
        StructureField = originalItem.StructureField
    };

    public void Restore()
    {
        if ( StringField is not null )
        {
            _originalItem.StringField = StringField;
        }

        if ( NumberField is not null )
        {
            _originalItem.NumberField = NumberField.Value;
        }

        if ( ClassField is not null )
        {
            _originalItem.ClassField = ClassField;
        }

        if ( StructureField is not null )
        {
            _originalItem.StructureField = StructureField.Value;
        }
    }
}