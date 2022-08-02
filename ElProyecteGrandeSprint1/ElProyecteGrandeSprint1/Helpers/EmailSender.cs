using System.Net;
using System.Net.Mail;
using System.Text;

namespace ElProyecteGrandeSprint1.Helpers
{
    public class EmailSender
    {
        private string RegistrationMessage(string name)
        {
            StringBuilder messageToBeSent = new StringBuilder();
            messageToBeSent.Append("Dear " + name + "<br><br>");
            messageToBeSent.Append("Welcome to KVM Game News Site!<br>");
            return messageToBeSent.ToString();
        }

        private string ForgotPasswordMessage(string name, Guid guid)
        {
            StringBuilder messageToBeSent = new StringBuilder();
            messageToBeSent.Append("Dear " + name + "<br><br>");
            messageToBeSent.Append("you forgor your password<br>");
            messageToBeSent.Append("Please Click the link below to change your password<br>");
            messageToBeSent.Append($"https://localhost:3000/newPassword/{guid}");
            return messageToBeSent.ToString();
        }

        private string ForgotPasswordMessageSuccess(string name)
        {
            StringBuilder messageToBeSent = new StringBuilder();
            messageToBeSent.Append("Dear " + name + "<br><br>");
            messageToBeSent.Append("your password was successfully changed<br>");
            return messageToBeSent.ToString();
        }

        public virtual void SendConfirmationEmail(string name, string email, string type, Guid guid)
        {
            SmtpClient smtpClient = new SmtpClient("smtp-mail.outlook.com");
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            NetworkCredential credential = new NetworkCredential("kvmgamenews@outlook.hu", "KVMGameNew01");
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = credential;

            MailMessage message = new MailMessage("kvmgamenews@outlook.hu", email);
            message.IsBodyHtml = true;

            if (type == "registration")
            {
                message.Body = RegistrationMessage(name);
                message.Subject = "Registration Confirmation";
            }


            if (type == "forgor")
            {
                message.Body = ForgotPasswordMessage(name, guid);
                message.Subject = "New Password";
            }

            if (type == "success")
            {
                message.Body = ForgotPasswordMessageSuccess(name);
                message.Subject = "Successful password change";
            }


            smtpClient.Send(message);
        }
    }
}
