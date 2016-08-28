using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Persistence.Repositories;
using BlacksmithPress.Diabetes.Types;
using NUnit.Framework;
using Shouldly;

namespace RepositoryTests
{
    public class PeopleTests : TestBase
    {

        [Test]
        public async void PeopleRepository_Create_CreatesNewPerson()
        {
            // arrange isolation
            var configuration = new Configuration();
            var container = configuration.Container;

            // arrange test
            var repository = new People(container);
            var expected = new Person {Id = 1, Name = "Ken"} as IPerson;

            // act
            var actual =  await repository.Create(new Person {Name = "Ken"});

            // assert
            actual.ShouldBe(expected);

            // clean-up
        }

    }
}
