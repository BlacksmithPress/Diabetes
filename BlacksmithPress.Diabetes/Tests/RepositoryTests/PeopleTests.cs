using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Persistence.Repositories;
using BlacksmithPress.Diabetes.Types;
using NUnit.Framework;
using ObjectApproval;
using Shouldly;

namespace RepositoryTests
{
    public class PeopleTests : TestBase
    {
        public PeopleTests()
        {
            configuration = new Configuration();
            container = configuration.Container;
            repository = new People(container);
        }

        private IConfiguration configuration;
        private IContainer container;
        private People repository;

        [Test]
        public void PeopleRepository_Create_CreatesNewPerson()
        {
            // arrange isolation

            // arrange test

            // act
            var actual =  repository.Create(new Person {Name = "Ken"});

            // assert
            actual.Name.ShouldBe("Ken");
            actual.Id.ShouldBeGreaterThan(0);

            // clean-up
            repository.Delete(actual.Id);
        }


        [Test]
        public void PeopleRepository_Get_GetsAPerson()
        {
            // arrange isolation

            // arrange test
            var expected = repository.Create(new Person {Name = "Existing Person"});

            // act
            var actual = repository.Get(expected.Id);

            // assert
            actual.ShouldBe(expected);

            // clean-up
            repository.Delete(expected.Id);
        }


        [Test]
        public void PeopleRepository_Update_UpdatesExistingPerson()
        {
            // arrange isolation

            // arrange test
            var existing = repository.Create(new Person {Name = "Existing Person"});
            var expected = new Person {Id = existing.Id, Name = "Existing Person (edited)"};

            // act
            var actual = repository.Update(expected);

            // assert
            actual.ShouldBe(expected);

            // clean-up
            repository.Delete(actual.Id);
        }


        [Test]
        public void PeopleRepository_Delete_DeletesPerson()
        {
            // arrange isolation

            // arrange test
            var existing = repository.Create(new Person { Name = "Existing Person" });

            // act
            repository.Delete(existing.Id);
            var actual = repository.Get(existing.Id);

            // assert
            actual.ShouldBeNull();

            // clean-up
        }


        [Test]
        public void PeopleRepository_GetAll_GetsAllPeople()
        {
            // arrange isolation

            // arrange test

            // act
            var actual = repository.GetAll();

            // assert
            ObjectApprover.VerifyWithJson(actual);

            // clean-up
        }


    }
}
