using NUnit.Framework;
using Reqnroll.Assist;

namespace Specflow.Assist.Dynamic.Specs.StepDefinitions;

[Binding]
public class DynamicSetComparisonSteps
{
    private readonly State state;

    public DynamicSetComparisonSteps(State state) => this.state = state;

    private DynamicSetComparisonException GetSetComparisonException()
    {
        return (DynamicSetComparisonException)state.CurrentException;
    }

    private void CheckForOneDifferenceContainingString(string expectedString)
    {
        var ex = GetSetComparisonException();
        var diffs = ((List<string>)ex.Differences);
        var diff = diffs.Find(f => f.Contains(expectedString));
        Assert.That(diff, Is.Not.Null);
    }

    [When(@"^I compare the set to this table$")]
    public void CompareSetToInstance(DataTable table)
    {
        try
        {
            table.CompareToDynamicSet(this.state.OriginalSet);
        }
        catch (DynamicSetComparisonException ex)
        {
            this.state.CurrentException = ex;
        }
    }

    [When(@"^I compare the set to this table using no type conversion$")]
    public void CompareSetToInstanceNoConversion(DataTable table)
    {
        try
        {
            table.CompareToDynamicSet(this.state.OriginalSet, false);
        }
        catch (DynamicSetComparisonException ex)
        {
            this.state.CurrentException = ex;
        }
    }

    [Then(@"^no set comparison exception should have been thrown$")]
    public void NoSetExceptionThrown()
    {
        Assert.That(this.state.CurrentException, Is.Null);
    }

    [Then(@"^an set comparison exception should be thrown$")]
    public void SetComparisonExceptionThrown()
    {
        Assert.That(GetSetComparisonException(), Is.Not.Null);
    }

    [Then(@"^an set comparision exception should be thrown with (\d+) differences$")]
    [Then(@"^an set comparision exception should be thrown with (\d+) difference$")]
    public void SetComparisionExceptionWithNumberOfDifferences(int expectedNumberOfDifference)
    {
        SetComparisonExceptionThrown();
        var actual = GetSetComparisonException().Differences.Count;
        Assert.That(actual, Is.EqualTo(expectedNumberOfDifference), $"Expected {actual} to be {expectedNumberOfDifference}");
    }

    [Then(@"^the error message for different rows should expect (.*) for table and (.*) for instance$")]
    public void ShouldDifferInRowCount(string tableRowCountString, string instanceRowCountString)
    {
        var message = GetSetComparisonException().Message;
        Assert.That(message.Contains(tableRowCountString), Is.True);
        Assert.That(message.Contains(instanceRowCountString), Is.True);
    }

    [Then(@"^one set difference should be on the (.*) column of the table$")]
    public void DifferenceOnTheColumnOfTheTable(string expectedColumnToDiffer)
    {
        CheckForOneDifferenceContainingString(expectedColumnToDiffer);
    }

    [Then(@"^one set difference should be on the (.*) field of the instance$")]
    public void DifferenceOnFieldOfInstance(string expectedFieldToDiffer)
    {
        CheckForOneDifferenceContainingString(expectedFieldToDiffer);
    }

    [Then(@"^(\d+) difference should be on row (\d+) on property '(.*)' for the values '(.*)' and '(.*)'$")]
    public void DifferenceOnValue(int differenceNumber, int rowNumber, string expectedProperty, string instanceValue, string tableRowValue)
    {
        var exception = GetSetComparisonException();
        var difference = exception.Differences[differenceNumber - 1];
        CheckForOneDifferenceContainingString("'" + rowNumber + "'");
        CheckForOneDifferenceContainingString("'" + expectedProperty + "'");
    }
}
