using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Web.Controllers
{
    public class BaseController : Controller
    {
        public enum AlertType { success, danger, warning, info }

        public void Alert(string message, AlertType type = AlertType.info)
        {
            TempData["Alert.Message"] = message;
            TempData["Alert.Type"] = type.ToString();
        }
    }
}
