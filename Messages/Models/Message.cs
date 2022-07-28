using System.ComponentModel.DataAnnotations;

namespace Messages.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public string Sender { get; set; }

        public string Receiver { get; set; }
    }
}
