using Model;
using System.Net.Http;

namespace VNPay_Web.API
{
    public abstract class AuthorizationHandler : BaseHandler
    {
        public EmployeesInfo Employee { get; set; }
        public override object ProcessRequest()
        {
            try
            {
                Employee = ICookiesMaster.GetCookie<EmployeesInfo>(ICookiesMaster.EINFO);
            }
            catch
            {
            }

            if (Employee != null && Employee.EmployeeId != null && Employee.EmployeeId > 0)
                return AuthorizationRequest();
            else
            {
                var response = new HttpResponseMessage();
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }
        }
        public abstract HttpResponseMessage AuthorizationRequest();
    }
}