namespace ContactsApp.Helpers {
    public interface IEmail {
        bool Send(string email, string subject, string message);
    }
}
