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
            var expected = new Person {Id = 1, Name = "Ken"} as IPerson;

            // act
            var actual =  repository.Create(new Person {Name = "Ken"});

            // assert
            actual.ShouldBe(expected);

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
