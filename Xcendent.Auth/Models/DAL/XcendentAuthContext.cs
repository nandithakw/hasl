namespace Xcendant.Auth.Models.DAL
{
    public class XcendentAuthContext :  AbstractXcendentAuthContext
    {

        public XcendentAuthContext()
            : base("XcendentAuthContext", throwIfV1Schema: true)
        {
        }

        public static XcendentAuthContext Create()
        {
            return new XcendentAuthContext();
        }

    }
}