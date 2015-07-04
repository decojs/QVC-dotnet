using System.Linq;
using NUnit.Framework;
using Qvc.Validation;
using Shouldly;
using Tests.Executables;

namespace Tests.Validation
{
    [TestFixture]
    public class ValidatorTests
    {
        [Test]
        public void TestConforms()
        {
            Should.NotThrow(() => 
                Validator.Validate(new QueryA{Name = "Hello"}));
        }

        [Test]
        public void TestViolates()
        {
            var result = Should.Throw<ValidationException>(() => 
                Validator.Validate(new QueryA()));

            result.Violations.ShouldNotBeEmpty();
            result.Violations.Single().FieldName.ShouldBe("Name");
            result.Violations.Single().Message.ShouldBe("The Name field is required.");
        }
    }
}