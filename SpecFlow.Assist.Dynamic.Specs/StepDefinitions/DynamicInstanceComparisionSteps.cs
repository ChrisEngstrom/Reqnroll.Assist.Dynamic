using NUnit.Framework;
using Reqnroll.Assist;

namespace Specflow.Assist.Dynamic.Specs.StepDefinitions;

[Binding]
public class DynamicInstanceComparisionSteps
{
    private readonly State state;
    public DynamicInstanceComparisionSteps(State state) => this.state = state;

    private DynamicInstanceComparisonException GetInstanceComparisonException()
    {
        var ex = (DynamicInstanceComparisonException)this.state.CurrentException;
        Assert.That(ex, Is.Not.Null);
        return ex;
    }

    private void CheckForOneDifferenceContainingString(string expectedString)
    {
        var ex = GetInstanceComparisonException();
        var diffs = ((List<string>)ex.Differences);
        var diff = diffs.Find(f => f.Contains(expectedString));
        Assert.That(diff, Is.Not.Null);
    }

    [When("^I compare it to this table$")]
    public void ComparingAgainstDynamicInstance(DataTable table)
    {
        try
        {
            var org = (object)this.state.OriginalInstance;
            table.CompareToDynamicInstance(org);
        }
        catch (DynamicInstanceComparisonException ex)
        {
            this.state.CurrentException = ex;
        }
    }

    [Then("^no instance comparison exception should have been thrown$")]
    public void NoException()
    {
        Assert.That(this.state.CurrentException, Is.Null);
    }

    [Then(@"^an instance comparison exception should be thrown with (\d+) differences$")]
    [Then(@"^an instance comparison exception should be thrown with (\d+) difference$")]
    public void ExceptionShouldHaveBeenThrown(int expectedNumberOfDifferences)
    {
        Assert.That(this.state.CurrentException, Is.Not.Null);
        var ex = GetInstanceComparisonException();
        Assert.That(ex.Differences.Count, Is.EqualTo(expectedNumberOfDifferences), $"Expected {ex.Differences.Count} to be {expectedNumberOfDifferences}");
    }

    [Then(@"^one difference should be on the (.*) column of the table$")]
    public void DifferenceOnTheColumnOfTheTable(string expectedColumnToDiffer)
    {
        CheckForOneDifferenceContainingString(expectedColumnToDiffer);
    }

    [Then(@"^one difference should be on the (.*) field of the instance$")]
    public void DifferenceOnFieldOfInstance(string expectedFieldToDiffer)
    {
        CheckForOneDifferenceContainingString(expectedFieldToDiffer);
    }

    [Then(@"^one message should state that the instance had the value (.*)$")]
    public void ExceptionMessageValueOnInstance(string expectedValueOfInstance)
    {
        CheckForOneDifferenceContainingString(expectedValueOfInstance);
    }

    [Then(@"^one message should state that the table had the value (.*)$")]
    public void ExceptionMessageValueInTable(string expectedValueOfTable)
    {
        CheckForOneDifferenceContainingString(expectedValueOfTable);
    }

    [Then(@"^one difference should be on the (.*) property$")]
    public void ExceptionMessageValueOnProperty(string expectedPropertyName)
    {
        CheckForOneDifferenceContainingString(expectedPropertyName);
    }

    [When(@"^I compare it to this table using no type conversion$")]
    public void WhenICompareItToThisTableUsingNoTypeConversion(DataTable table)
    {
        try
        {
            var org = (object)this.state.OriginalInstance;
            table.CompareToDynamicInstance(org, false);
        }
        catch (DynamicInstanceComparisonException ex)
        {
            this.state.CurrentException = ex;
        }
    }
}
