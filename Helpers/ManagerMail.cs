using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Drim.Interfaces.MVC.Helpers
{


    public enum RemitenteEmail
    {
        drimrecursoshumanos = 0,
        controlhorari = 1,
        seleccioempleados = 2,
        contractacio = 3,
        informes = 4

    }


    public static class ManagerMail
    {


        public static string GetPasswordRemitente(this RemitenteEmail remitenteEmail)
        {

            switch (remitenteEmail)
            {

                case RemitenteEmail.controlhorari:
                    return EntornoHelper.EmailAccountPassword(EntornoHelper.RemoteEmailAccount.ControlHorario);
                case RemitenteEmail.drimrecursoshumanos:
                    return EntornoHelper.EmailAccountPassword(EntornoHelper.RemoteEmailAccount.RecursosHumanos);
                case RemitenteEmail.seleccioempleados:
                    return EntornoHelper.EmailAccountPassword(EntornoHelper.RemoteEmailAccount.Seleccion);
                case RemitenteEmail.contractacio:
                    return EntornoHelper.EmailAccountPassword(EntornoHelper.RemoteEmailAccount.Contratacion);
                case RemitenteEmail.informes:
                    return EntornoHelper.EmailAccountPassword(EntornoHelper.RemoteEmailAccount.Informes);
                default:
                    return "";
            }


        }

        public static string GetEmailRemitente(this RemitenteEmail remitenteEmail)
        {

            switch (remitenteEmail)
            {

                case RemitenteEmail.controlhorari:
                    return EntornoHelper.EmailAccountAddress(EntornoHelper.RemoteEmailAccount.ControlHorario);
                case RemitenteEmail.drimrecursoshumanos:
                    return EntornoHelper.EmailAccountAddress(EntornoHelper.RemoteEmailAccount.RecursosHumanos);
                case RemitenteEmail.seleccioempleados:
                    return EntornoHelper.EmailAccountAddress(EntornoHelper.RemoteEmailAccount.Seleccion);
                case RemitenteEmail.informes:
                    return EntornoHelper.EmailAccountAddress(EntornoHelper.RemoteEmailAccount.Informes);
                default:
                    return EntornoHelper.EmailAccountAddress(EntornoHelper.RemoteEmailAccount.RecursosHumanos);
            }


        }





        public static string EnviarMail(RemitenteEmail remitenteEmail, string Subject, string Body, List<string> lTo, List<string> filesname = null, List<Attachment> attachments = null)
        {

            string result = string.Empty;

            if (lTo != null && lTo.Count() >= 5)
            {
                return "Existen mas de 5 Destinatarios";
            }


            try
            {

                string Remitente = remitenteEmail.GetEmailRemitente();
                string RemitentePswd = remitenteEmail.GetPasswordRemitente();


                string SMTP = EntornoHelper.SmtpHost;
                int SMTP_Puerto = EntornoHelper.SmtpPort;

                using (MailMessage email = new MailMessage())
                {

                    foreach (string To in lTo)
                    {
                        if (!string.IsNullOrEmpty(To))
                            email.To.Add(new MailAddress(To));
                    }

                    if (filesname != null)
                    {
                        foreach (var filename in filesname)
                        {
                            Attachment data = new Attachment(filename, MediaTypeNames.Application.Octet);
                            email.Attachments.Add(data);
                            //data.Dispose();
                        }
                    }

                    if (attachments != null && attachments.Any())
                    {
                        foreach (var att in attachments)
                        {
                            email.Attachments.Add(att);
                        }
                    }

                    email.From = new MailAddress(Remitente);
                    email.Subject = Subject;
                    email.Body = Body;
                    email.IsBodyHtml = true;
                    email.Priority = MailPriority.High;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = SMTP;
                    smtp.Port = SMTP_Puerto;
                    smtp.EnableSsl = EntornoHelper.SmtpEnableSsl;
                    smtp.Credentials = new NetworkCredential(Remitente, RemitentePswd);
                    smtp.Send(email);
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;

                result += " ." + (ex.InnerException != null ? ex.InnerException.Message : "");
            }

            return result;
        }

    }
}
