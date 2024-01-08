using Libs.Memento.Tests.Models;
using Libs.Memento.Tests.MotherObjects;
using NUnit.Framework;

namespace Libs.Memento.Tests;

public class MementoHandlerTests
{
    private const int MaxStepsToRecord = 10;
    private MementoHandler _handler = null!; 

    [SetUp]
    public void SetUp() => _handler = new MementoHandler( MaxStepsToRecord );

#region Save
    [Test]
    public void Save_SaveOneTime_HandlerHasOneSavedState()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        
        // Act
        _handler.Save( MementoTestItem.Initial( item ) );
        
        // Assert
        Assert.That( _handler.SavedStatesCount, Is.EqualTo( 1 ) );
    }

    [Test]
    public void Save_SaveThreeTimes_HandlerHasThreeSavedStates()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        
        // Act
        _handler.Save( MementoTestItem.Initial( item ) );

        for ( int i = 0; i < 2; i++ )
        {
            item.NumberField++;
            _handler.Save( new MementoTestItem( item )
            {
                NumberField = item.NumberField
            } );
        }

        // Arrange
        Assert.That( _handler.SavedStatesCount, Is.EqualTo( 3 ) );
    }

    [Test]
    public void Save_SaveMoreTimesThanAllowedMaxStepsToRecord_FirstStepsAreDeletedAndHandlerHasValidNumberOfSteps()
    {
        // Arrange
        int totalStepsToSave = MaxStepsToRecord + 10;
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );
        
        // Act
        for ( int i = 0; i < totalStepsToSave; i++ )
        {
            item.NumberField = i;
            _handler.Save( new MementoTestItem( item )
            {
                NumberField = item.NumberField
            } );
        }
        
        // Assert
        Assert.That( _handler.SavedStatesCount, Is.EqualTo( MaxStepsToRecord ) );
    }

    [Test]
    public void Save_EntityHasChangesInStringField_StateIsRecordedWithValidChange()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        string valueToSave = "value to save";
        string replaceValueToSave = "replace value to save by this value";
        
        // Act
        item.StringField = valueToSave;
        _handler.Save( new MementoTestItem( item )
        {
            StringField = item.StringField
        } );

        item.StringField = replaceValueToSave;
        
        // Assert
        Assert.That( item.StringField, Is.EqualTo( replaceValueToSave ) );

        var memento = ( ( _handler.GetItemByIndex( 1 ) ?? throw new IndexOutOfRangeException() ) as MementoTestItem )!;
        Assert.That( memento.StringField, Is.EqualTo( valueToSave ) );
    }

    [Test]
    public void Save_EntityHasChangesInNumberField_StateIsRecordedWithValidChange()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        int valueToSave = 10_000;
        int replaceValueToSave = 20_000;
        
        // Act
        item.NumberField = valueToSave;
        _handler.Save( new MementoTestItem( item )
        {
            NumberField = item.NumberField
        } );

        item.NumberField = replaceValueToSave;
        
        // Assert
        Assert.That( item.NumberField, Is.EqualTo( replaceValueToSave ) );

        var memento = ( ( _handler.GetItemByIndex( 1 ) ?? throw new IndexOutOfRangeException() ) as MementoTestItem )!;
        Assert.That( memento.NumberField, Is.EqualTo( valueToSave ) );
    }

    [Test]
    public void Save_EntityHasChangesInStructureField_StateIsRecordedWithValidChange()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        var valueToSave = new TestStructureFieldType( 10_000 );
        var replaceItemValueToSave = new TestStructureFieldType( 20_000 );
        
        // Act
        item.StructureField = valueToSave;
        _handler.Save( new MementoTestItem( item )
        {
            StructureField = item.StructureField
        } );

        item.StructureField = replaceItemValueToSave;
        
        // Assert
        Assert.That( item.StructureField, Is.EqualTo( replaceItemValueToSave ) );

        var memento = ( ( _handler.GetItemByIndex( 1 ) ?? throw new IndexOutOfRangeException() ) as MementoTestItem )!;
        Assert.That( memento.StructureField, Is.EqualTo( valueToSave ) );
    }

    [Test]
    public void Save_EntityHasChangesInClassField_StateIsRecordedWithValidChange()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        var valueToSave = new TestReferenceFieldType( 10_000 );
        var replaceItemValueToSave = new TestReferenceFieldType( 20_000 );
        
        // Act
        item.ClassField = valueToSave;
        _handler.Save( new MementoTestItem( item )
        {
            ClassField = item.ClassField
        } );

        item.ClassField = replaceItemValueToSave;
        
        // Assert
        Assert.That( item.ClassField, Is.EqualTo( replaceItemValueToSave ) );

        var memento = ( ( _handler.GetItemByIndex( 1 ) ?? throw new IndexOutOfRangeException() ) as MementoTestItem )!;
        Assert.That( memento.ClassField, Is.EqualTo( valueToSave ) );
    }

    [Test]
    // TODO: Solve it somehow
    public void Save_ComplicatedTypeFieldHasSomeChangesInSomeOfItsFields_StateIsRecordedIncorrectly()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        int valueForFirstNotInitialState = 10_000;
        int valueForSecondNotInitialState = 20_000;
        
        // Act
        item.ClassField.Value = valueForFirstNotInitialState;
        _handler.Save( new MementoTestItem( item )
        {
            ClassField = item.ClassField
        } );
        
        item.ClassField.Value = valueForSecondNotInitialState;
        _handler.Save( new MementoTestItem( item )
        {
            ClassField = item.ClassField
        } );
        
        // Assert
        var firstNotInitialState = ( ( _handler.GetItemByIndex( 1 ) ?? throw new IndexOutOfRangeException() ) as MementoTestItem )!;
        int savedValueForFirstNotInitialState = firstNotInitialState.ClassField?.Value ?? throw new InvalidOperationException();
        Assert.That( savedValueForFirstNotInitialState, Is.EqualTo( valueForSecondNotInitialState ) );
    }

#endregion
    
#region Undo

    [Test]
    public void Undo_HasNoSavedStates_UndoOneAction_NothingHappens()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
            
        // Act
        _handler.Undo();
            
        // Assert
        TestItem sample = TestItemMotherObject.FullyField.Build();
        Assert.That( item.ClassField.Value, Is.EqualTo( sample.ClassField.Value ) );
        Assert.That( item.NumberField, Is.EqualTo( sample.NumberField ) );
        Assert.That( item.StringField, Is.EqualTo( sample.StringField ) );
        Assert.That( item.StructureField.Value, Is.EqualTo( sample.StructureField.Value ) );
    }

    [Test]
    public void Undo_HasOnlyInitialState_UndoOneAction_NothingHappens()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );
        
        // Act
        _handler.Undo();
        
        // Assert
        TestItem sample = TestItemMotherObject.FullyField.Build();
        Assert.That( item.ClassField.Value, Is.EqualTo( sample.ClassField.Value ) );
        Assert.That( item.NumberField, Is.EqualTo( sample.NumberField ) );
        Assert.That( item.StringField, Is.EqualTo( sample.StringField ) );
        Assert.That( item.StructureField.Value, Is.EqualTo( sample.StructureField.Value ) );
    }

    [Test]
    public void Undo_HasTwoSavedStates_UndoOneAction_ItemStateRestoredToInitialValue()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );
        int initialValue = item.NumberField;
        int newValue = 10_000;
        
        // Act
        item.NumberField = newValue;
        _handler.Save( new MementoTestItem( item )
        {
            NumberField = item.NumberField
        } );

        _handler.Undo();

        // Assert
        item.NumberField = initialValue;
    }

    [Test]
    public void Undo_HasThreeSavedStates_UndoMoreActionsThanHasSavedStates_ItemStateRestoredToInitialValue()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );
        int initialValue = item.NumberField;
        
        for ( int i = 0; i < 2; i++ )
        {
            item.NumberField = 10 * i;
            _handler.Save( new MementoTestItem( item )
            {
                NumberField = item.NumberField
            } );
        }
        
        // Act
        for ( int i = 0; i < 10; i++ )
        {
            _handler.Undo();
        }
        
        // Assert
        Assert.That( item.NumberField, Is.EqualTo( initialValue ) );
    }

    [Test]
    public void Undo_HasThreeSavedStates_UndoOneAction_ItemStateRevertedAtOneState()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        for ( int i = 0; i < 2; i++ )
        {
            item.NumberField = i * 10;
            _handler.Save( new MementoTestItem( item )
            {
                NumberField = item.NumberField
            } );
        }
        
        // Act
        _handler.Undo();
        
        // Assert
        Assert.That( item.NumberField, Is.EqualTo( 0 * 10 ) );
    }

    [Test]
    public void Undo_HasFourSavedStates_UndoThreeActionsThenSaveState_ThirdStateIsOverridenAndFourthIsDeleted()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );
        
        for ( int i = 0; i < 3; i++ )
        {
            item.NumberField = 10 * i;
            _handler.Save( new MementoTestItem( item )
            {
                NumberField = item.NumberField
            } );
        }
        
        int valueToOverride = 10_000;
        
        // Act
        for ( int i = 0; i < 3; i++ )
        {
            _handler.Undo();
        }

        item.NumberField = valueToOverride;
        _handler.Save( new MementoTestItem( item )
        {
            NumberField = item.NumberField
        } );

        // Assert
        Assert.That( _handler.SavedStatesCount, Is.EqualTo( 2 ) );
        
        MementoTestItem memento = ( ( _handler.GetItemByIndex( 1 ) ?? throw new IndexOutOfRangeException() ) as MementoTestItem )!;
        Assert.That( memento.NumberField, Is.EqualTo( valueToOverride ) );
    }
    
#endregion

#region Redo

    [Test]
    public void Redo_UndoOneTimeThenRedo_ItemStateMustBeRelevant()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        var relevantValue = 2;

        item.NumberField = relevantValue;
        _handler.Save( new MementoTestItem( item )
        {
            NumberField = item.NumberField
        } );
        
        // Act
        _handler.Undo();
        _handler.Redo();
        
        // Assert
        Assert.That( item.NumberField, Is.EqualTo( relevantValue ) );
    }

    [Test]
    public void Redo_UndoTwoTimesThenRedoTwoTimes_ItemStateMustBeRelevant()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        var relevantValue = 1 * 10;

        // Act
        for ( int i = 0; i < 2; i++ )
        {
            item.NumberField = i * 10;
            _handler.Save( new MementoTestItem( item )
            {
                NumberField = item.NumberField
            } );
        }

        for ( int i = 0; i < 2; i++ )
        {
            _handler.Undo();
        }

        for ( int i = 0; i < 2; i++ )
        {
            _handler.Redo();
        }
        
        // Assert
        Assert.That( item.NumberField, Is.EqualTo( relevantValue ) );
    }

    [Test]
    public void Redo_UndoThreeTimesThenRedoTwoTimes_ItemStateMustBeEqualToSecondNotInitialState()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        var relevantValue = 1 * 10;

        // Act
        for ( int i = 0; i < 3; i++ )
        {
            item.NumberField = i * 10;
            _handler.Save( new MementoTestItem( item )
            {
                NumberField = item.NumberField
            } );
        }

        for ( int i = 0; i < 3; i++ )
        {
            _handler.Undo();
        }

        for ( int i = 0; i < 2; i++ )
        {
            _handler.Redo();
        }
        
        // Assert
        Assert.That( item.NumberField, Is.EqualTo( relevantValue ) );
    }

    [Test]
    public void Redo_ItemIsInRelevantState_NothingHappens()
    {
        // Arrange
        TestItem item = TestItemMotherObject.FullyField.Build();
        _handler.Save( MementoTestItem.Initial( item ) );

        var relevantValue = 2;

        item.NumberField = relevantValue;
        _handler.Save( new MementoTestItem( item )
        {
            NumberField = item.NumberField
        } );
        
        // Act
        _handler.Redo();
        
        // Assert
        Assert.That( item.NumberField, Is.EqualTo( relevantValue ) );
    }

    [Test]
    public void Redo_HasNoStates_NothingHappens()
    {
        Assert.DoesNotThrow( () =>
        {
            _handler.Redo();
            _handler.Redo();
        } );
    }

    #endregion
}