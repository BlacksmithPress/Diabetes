using System.ComponentModel.DataAnnotations.Schema;

namespace BlacksmithPress.Diabetes.Persistence.Database
{
    public class Person 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
    }
}