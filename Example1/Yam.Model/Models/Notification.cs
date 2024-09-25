namespace Yam.Model.Models
{
    public class Notification
    {
        public Notification(Guid id, string text)
        {
            Id = id;
            Text = text;
        }

        public Notification(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
        }


        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
