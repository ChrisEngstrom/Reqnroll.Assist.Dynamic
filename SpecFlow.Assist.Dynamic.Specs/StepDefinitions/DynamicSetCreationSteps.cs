using NUnit.Framework;
using Reqnroll.Assist;

namespace Specflow.Assist.Dynamic.Specs.StepDefinitions;

[Binding]
public class DynamicSetCreationSteps
{
    private readonly State state;

    public DynamicSetCreationSteps(State state) => this.state = state;
    private dynamic GetItem(int itemNumber)
    {
        return this.state.OriginalSet[itemNumber - 1];
    }

    [Given(@"^I create a set of dynamic instances from this table$")]
    [When(@"^I create a set of dynamic instances from this table$")]
    public void WithMethodBinding(DataTable table)
    {
        this.state.OriginalSet = table.CreateDynamicSet().ToList();
    }

    [Given(@"^I create a set of dynamic instances from this table using no type conversion$")]
    public void WithMethodBindingNoTypeConversion(DataTable table)
    {
        this.state.OriginalSet = table.CreateDynamicSet(false).ToList();
    }

    [Then(@"^I should have a list of (\d+) dynamic objects$")]
    public void ShouldContain(int expectedNumberOfItems)
    {
        var actual = this.state.OriginalSet.Count;
        Assert.That(actual, Is.EqualTo(expectedNumberOfItems), $"Expected {actual} to be {expectedNumberOfItems}");
    }

    [Then(@"^the (\d+) item should have BirthDate equal to '(.*)'$")]
    public void ItemInSetShouldHaveExpectedBirthDate(int itemNumber, string expectedBirthDateString)
    {
        var actual = GetItem(itemNumber).BirthDate;
        var expectedBirthDate = DateTime.Parse(expectedBirthDateString).Date;
        Assert.That(actual, Is.EqualTo(expectedBirthDate), $"Expected {actual} to be {expectedBirthDate}");
    }

    [Then(@"^the (\d+) item should have Age equal to '(\d+)'$")]
    public void ItemInSetShouldHaveExpectedAge(int itemNumber, int expectedAge)
    {
        var actual = GetItem(itemNumber).Age;
        Assert.That(actual, Is.EqualTo(expectedAge), $"Expected {actual} to be {expectedAge}");
    }

    [Then(@"^the (.*) item should still Name equal '(.*)'$")]
    public void ThenTheItemShouldStillNameEqual(int itemNumber, string expectedName)
    {
        var actual = GetItem(itemNumber).Name;
        Assert.That(actual, Is.EqualTo(expectedName), $"Expected {actual} to be {expectedName}");
    }

    [Then(@"^the (.*) item should still Age equal '(.*)'$")]
    public void ThenTheItemShouldStillAgeEqual(int itemNumber, string expectedAge)
    {
        var actual = GetItem(itemNumber).Age;
        Assert.That(actual, Is.EqualTo(expectedAge), $"Expected {actual} to be {expectedAge}");
    }

    [Then(@"^the (\d+) item should have Name equal to '(.*)'$")]
    public void ItemInSetShouldHaveExpectedName(int itemNumber, string expectedName)
    {
        var actual = GetItem(itemNumber).Name;
        Assert.That(actual, Is.EqualTo(expectedName), $"Expected {actual} to be {expectedName}");
    }

    [Then(@"^the (\d+) item should have LengthInMeters equal to '(\d+\.\d+)'$")]
    public void ItemInSetShouldHaveExpectedLenghtInMeters(int itemNumber, double expectedLengthInMetersItem)
    {
        var actual = GetItem(itemNumber).LengthInMeters;
        Assert.That(actual, Is.EqualTo(expectedLengthInMetersItem), $"Expected {actual} to be {expectedLengthInMetersItem}");
    }

    [When(@"^I create a set of dynamic instances from this table using no type conversion$")]
    public void WhenICreateASetOfDynamicInstancesFromThisTableUsingNoTypeConversion(DataTable table)
    {
        this.state.OriginalSet = table.CreateDynamicSet(false).ToList();
    }
}
