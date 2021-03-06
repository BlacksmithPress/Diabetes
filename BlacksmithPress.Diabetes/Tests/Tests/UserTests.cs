﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Cloud.Controllers;
using BlacksmithPress.Diabetes.Persistence.Database;
using NUnit.Framework;
using ObjectApproval;
using Shouldly;

namespace Tests
{
    public class UserTests
    {
        [Test]
        public void UsersController_Post_CreatesPerson()
        {
            var context = new Context("DefaultConnection");
            var controller = new UsersController(context);

            var name = "New User";
            var username = "Username";
            var password = "Password";

            if (context.Users.Any(p => p.Username == username))
                context.Users.RemoveRange(context.Users.Where(p => p.Username == username));

            var user = new User {Name = name, Username = username, Password = password};
            var result = controller.PostUser(user);

            user.Id.ShouldBeGreaterThan(0);

            context.Users.RemoveRange(context.Users.Where(p => p.Username == username));
            context.SaveChanges();
        }


        [Test]
        public void User_ToCredentials_CreatesNetworkCredentialForUser()
        {
            // arrange isolation

            // arrange test
            var user = new User {Username = "username", Password = "password"};
            var expected = new NetworkCredential(user.Username, user.Password);

            // act
            var actual = user.ToCredentials();

            // assert
            actual.UserName.ShouldBe(expected.UserName);
            actual.Password.ShouldBe(expected.Password);
            actual.Domain.ShouldBe(expected.Domain);

            // clean-up
        }


        [Test]
        public void User_Authenticate_VerifiesCredentials()
        {
            // arrange isolation

            // arrange test
            var context = new Context("DefaultConnection");
            var user = new User
            {
                Name = "User for Authentication Test",
                Username = "username",
                Password = "password",
            };
            context.Users.Add(user);
            context.SaveChanges();

            // act
            var actual = context.Authenticate(user.ToCredentials());

            // assert
            actual.ShouldNotBeNull();
            actual.Identity.IsAuthenticated.ShouldBeTrue();

            // clean-up
            context.Users.Remove(user);
            context.SaveChanges();
        }


        [Test]
        public void UsersController_NoCredentials_RequiresAuthentication()
        {
            // arrange isolation

            // arrange test
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://diabetes.blacksmithpress.com.local/"),
            };

            // act
            var actual = client.GetAsync("api/users/").Result;

            // assert
            actual.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            // clean-up
        }

        [Test]
        public void UsersController_WithCredentials_RequiresAuthentication()
        {
            // arrange isolation

            // arrange test
            var client = new HttpClient()
            {
                BaseAddress = new Uri("http://diabetes.blacksmithpress.com.local/"),
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "a2VuOlBhc3N3b3Jk");

            // act
            var actual = client.GetAsync("api/users/").Result;

            // assert
            actual.StatusCode.ShouldBe(HttpStatusCode.OK);

            // clean-up
        }


        [Test]
        public void User_ToBasicAuthentication_ReturnsAuthenticationHeaderValue()
        {
            // arrange isolation

            // arrange test
            var expected = "a2VuOlBhc3N3b3Jk";
            var user = new User {Username = "ken", Password = "Password"};

            // act
            var actual = user.ToBasicAuthentication();

            // assert
            actual.ShouldBe(expected);

            // clean-up
        }


    }
}
