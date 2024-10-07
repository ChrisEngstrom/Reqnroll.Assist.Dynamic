# Reqnroll.Assist.Dynamic

Reqnroll.Assist.Dynamic is a couple of simple extension methods for the Reqnroll DataTable object that helps you to write less code. 

What would you rather write? 
This:
```c#
[Binding]
public class StepsUsingStaticType
{
    private Person _person;

    [Given(@"I create an instance from this table")]
    public void GivenICreateAnInstanceFromThisTable(DataTable table)
    {
        _person = table.CreateInstance<Person>();
    }

    [Then(@"the Name property on Person should equal '(.*)'")]
    public void PersonNameShouldBe(string expectedValue)
    {
        Assert.That(_person.Name, Is.EqualTo(expectedValue));
    }
}

// And then make sure to not forget defining a separate Person class for testing, 
// since you don't want to reuse the one your system under test is using - that's bad practice

// Should probably be in another file too...
// might need unit tests if the logic is complicated
public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime BirthDate { get; set; }
    public double LengthInMeters { get; set; }
}
```
    
Or this:  
```c#
[Binding]
public class StepsUsingDynamic
{
    private dynamic _instance;

    [Given(@"I create an instance from this table")]
    public void c(dynamic instance) { _instance = instance; }

    [Then(@"the Name property should equal '(.*)'")]
    public void NameShouldBe(string expectedValue)
    {
        Assert.That(_instance.Name, Is.EqualTo(expectedValue));
    }
}
```
The later version uses Reqnroll.Assist.Dynamic. Shorter, sweeter and more fun!

> well, this is may be one of the best usecases for dynamic i have ever seen
>> A happy Reqnroll.Assists.Dynamic user

Full [documentation on the wiki](https://github.com/marcusoftnet/SpecFlow.Assist.Dynamic/wiki/Documentation)
