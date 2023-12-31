using MailKit.Net.Smtp;
using MimeKit;

namespace BankConsole;

public static class EmailService
{
    public static void SendEmail()
    {
        //Mensaje que queremos enviar
        var message = new MimeMessage(); //Instancia
        //Enviar el mensaje
        message.From.Add(new MailboxAddress ("Ariadne Romero", "ariadneselena12@gmail.com"));
        //Correo del destinatario
        message.To.Add(new MailboxAddress ("Ariadne Romero", "ariadne.selena@hotmail.com"));
        message.Subject = "BankConsole: Usuarios nuevos";

        message.Body = new TextPart("plain") {
            Text = GetEmailText()
        };

        //Enviar el correo
        using (var client = new SmtpClient ()) {
            client.Connect("smtp.gmail.com", 587, false); //Servidor del correo
            client.Authenticate("ariadneselena12@gmail.com", "vvbakaqamqcvwfzj");
            client.Send(message);
            client.Disconnect(true);
        }
    }

    private static string GetEmailText()
    {
        List<User> newUsers = Storage.GetNewUsers(); 

        if(newUsers.Count == 0)
            return "No hay usuarios nuevos. ";
        
        string emailText = "Usuarios agregados hoy:\n";

        foreach (User user in newUsers)
            emailText += "\t+ " + user.ShowData() + "\n";
        
        return emailText;
    }
}