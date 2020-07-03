using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiparisTakip.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SiparisTakip.Controllers
{
    public class AllRequestsController : Controller
    {
        private readonly SiparisTakipDB _siparisTakipDB;

        public AllRequestsController(SiparisTakipDB context)
        {
            _siparisTakipDB = context;
        }

        public bool SessionCont()
        {
            if (HttpContext != null)
            {
                if ((HttpContext.Session.GetString("userId")) != null &&
                    HttpContext.Session.GetString("userPermission") == "Administrator")
                {
                    return true;
                }
            }
            return false;
        }

        public IActionResult Index()
        {
            bool session = SessionCont();
            if (session == false)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(_siparisTakipDB.Requests.Include(r => r.user).ToList());
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var request = await _siparisTakipDB.Requests
                .FirstOrDefaultAsync(m => m.requestId == id);
            if (request == null)
            {
                return RedirectToAction("Index", "Home");
            }

            _siparisTakipDB.Requests.Remove(request);
            await _siparisTakipDB.SaveChangesAsync();
            return RedirectToAction("Index", "AllRequests");
        }
    }
}