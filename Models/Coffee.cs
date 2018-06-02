using System.ComponentModel.DataAnnotations;

namespace vue_experiments.Models
{
    public enum CoffeeType
    {
        Espresso,
        Latte,
        Mocha
    }

    public class Coffee
    {
        public int Id { get; set; }

        [Required, StringLength(80)]
        public string Name { get; set; }

        [Range(0, 2)]
        public CoffeeType CoffeeType { get; set; }

        public string Image { get; set; }
    }
}