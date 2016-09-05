using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BlacksmithPress.Diabetes.Entities;
using BlacksmithPress.Diabetes.Persistence.Repositories;
using BlacksmithPress.Diabetes.Types;
using Microsoft.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace Console
{
    class Program
    {
        private static CommandLineApplication _application = new CommandLineApplication(false);

        static int Main(string[] args)
        {
            _application.Command("people", people =>
            {
                people.Command("add", add =>
                {
                    add.Argument("json", "A JSON representation of the Person to add.");
                    add.Invoke = People_Add;
                }, false);
                people.Command("list", list =>
                {
                    list.Invoke = People_List;
                }, false);
            }, false);
            _application.Option("--api|-a <API>", "Base URL for the REST API.", CommandOptionType.SingleValue);

            return _application.Execute(args);
        }

        private static int People_List()
        {
            return 1;
        }

        private static int People_Add()
        {
            var command = _application.Commands.Find(c => c.Name == "people").Commands.Find(c => c.Name == "add");
            var json = command.Arguments.Find(a => a.Name == "json").Value;
            var person = JsonConvert.DeserializeObject<Person>(json);
            var repository = new People(Configuration.Instance.Container, new NetworkCredential());
            var result = repository.Create(person);
            System.Console.Out.WriteLine($"Created a Person (Id: {result.Id}, Name: \"{result.Name}\").");
            return 0;
        }
    }
}
