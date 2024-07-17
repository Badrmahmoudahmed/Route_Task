using OrederManagement.Core.Entities;

namespace Route_Task.Helpers
{
    public class EnumValuesToReturn
    {
        public Roles AdminRole { get; set; } = Roles.Admin;
        public Roles CustomerRole { get; set; } = Roles.Customer;

        public Statues PendingPaymentStatus { get; set; } = Statues.Pending;
        public Statues PaymentRecivedPaymentStatus { get; set; } = Statues.PaymentRecived;
        public Statues PaymentFaildPaymentStatus { get; set; } = Statues.PaymentFaild;
    }
}
