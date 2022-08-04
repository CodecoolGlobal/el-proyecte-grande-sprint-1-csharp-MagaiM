using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElProyecteGrandeSprint1.Helpers;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestBackend
{
    public class EmailSenderTest
    {

        private EmailSender _emailSender = new EmailSender();

        //[Test]
        public void SendConfirmationEmailTest()
        { 
            _emailSender.SendConfirmationEmail("admin", "admin@admin.com", "registration", new Guid());
            _emailSender.SendConfirmationEmail("admin", "admin@admin.com", "forgor", new Guid());
            _emailSender.SendConfirmationEmail("admin", "admin@admin.com", "success", new Guid());
            Assert.Pass();
        }
    }
}
