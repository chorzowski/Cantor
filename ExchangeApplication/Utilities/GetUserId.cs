using ExchangeApplication.Models;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;


namespace ExchangeApplication.Utilities
{
    public class GetUserId : IGetUserId
    {
        private GenericUnitOfWork uow = null;
        public GetUserId()
        {
            uow = new GenericUnitOfWork();
        }

        public GetUserId(GenericUnitOfWork _uow)
        {
            this.uow = _uow;
        }

        public ApplicationUser GetUserID()
        {
            var idLoggedUser = HttpContext.Current.User.Identity.GetUserId();
            var loggedUser = uow.Repository<ApplicationUser>().GetDetail(p => p.Id == idLoggedUser);
            return loggedUser;
        }
    }
}