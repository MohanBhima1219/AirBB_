namespace AirBB.Models
{
    public static class Check
    {
        public static string EmailExists(AirBBDbcontext ctx, string email)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(email))
            {
                var customer = ctx.Client.FirstOrDefault(
                    c => c.Email.ToLower() == email.ToLower());
                if (customer != null)
                    msg = $"Email address {email} already in use.";
            }
            return msg;
        }

        public static string OwnerExists(AirBBDbcontext ctx, int ownerId)
        {
            string msg = string.Empty;

            if (ownerId > 0)
            {
                var owner = ctx.Client.FirstOrDefault(u => u.ClientId == ownerId && u.UserType == "Owner");
                if (owner == null)
                    msg = $"Owner with ID {ownerId} is not an Owner.";
            }
            else
            {
                msg = "Owner ID is required.";
            }

            return msg;
        }
    }
}