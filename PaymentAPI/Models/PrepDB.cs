using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace PaymentAPI.Models
{
    public class PrepDB
    {
        public static void PrepoPulation(IApplicationBuilder app)
        {
            using (var services = app.ApplicationServices.CreateScope())
            {
                SendData(services.ServiceProvider.GetService<PaymentDetailContext>());
            }
        }
        private static void SendData(PaymentDetailContext context)
        {
            Console.WriteLine("Appling Migration...");
            context.Database.Migrate();
            if (context.paymentDetails.Any())
            {
                context.paymentDetails.AddRange(new PaymentDetails() {
                    CardNumber = "1234567890123456",
                    CardOwnerName = "Geeta",
                    ExpirationDate = "02/25",
                    SecurityCode = "123"
                });
                context.SaveChanges();
            }
            else
                Console.WriteLine("Already Have Data -- No seeding");
        }
    }

   
}
