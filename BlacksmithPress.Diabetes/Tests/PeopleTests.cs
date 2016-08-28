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
            var person = new Person
            {
                Name = "Ken",
            };
            using (var context = new Context("DefaultConnection"))
            {
                context.People.Add(person);
                context.SaveChanges();
            }
            ObjectApprover.VerifyWithJson(person);
        }
    }
}
