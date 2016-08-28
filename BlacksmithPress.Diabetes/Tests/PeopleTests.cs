using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Cloud.Controllers;
using BlacksmithPress.Diabetes.Data;
using MongoDB.Driver;
using NUnit.Framework;
using ObjectApproval;
using Shouldly;

namespace Tests
{
    public class PeopleTests
    {
        [Test]
        public void PeopleController_Post_CreatesPerson()
        {
            var context = new Context("DefaultConnection");
            var controller = new PeopleController(context);

            var name = "TEST: Joshua";

            if (context.People.Any(p => p.Name == name))
                context.People.RemoveRange(context.People.Where(p => p.Name == name));

            var person = new Person {Name = name};
            var result = controller.PostPerson(person);

            person.Id.ShouldBeGreaterThan(0);

            context.People.RemoveRange(context.People.Where(p => p.Name == name));
            context.SaveChanges();
        }


        [Test]
        public void PeopleController_Put_EditsExistingPerson()
        {
            // arrange isolation
            var context = new Context("DefaultConnection");
            var controller = new PeopleController(context);

            // arrange test
            var actual = context.People.FirstOrDefault();
            if (actual == null)
            {
                actual = new Person {Name = "Pre-existing Person"};
                context.People.Add(actual);
                context.SaveChanges();
            }

            // act
            actual.Name += " (edited)";
            var result = controller.PutPerson(actual.Id, actual);

            // assert
            var expected = context.People.FirstOrDefault(p => p.Id == actual.Id);
            actual.ShouldBe(expected);

            // clean-up
            context.People.Remove(actual);
            context.SaveChanges();
        }

    }
}
