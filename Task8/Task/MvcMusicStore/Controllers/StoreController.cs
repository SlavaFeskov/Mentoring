using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Infrastructure.Logging;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger _logger;
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();

        public StoreController(ILogger logger)
        {
            _logger = logger;
        }

        // GET: /Store/
        public async Task<ActionResult> Index()
        {
            return View(await _storeContext.Genres.ToListAsync());
        }

        // GET: /Store/Browse?genre=Disco
        public async Task<ActionResult> Browse(string genre)
        {
            return View(await _storeContext.Genres.Include("Albums").SingleAsync(g => g.Name == genre));
        }

        public async Task<ActionResult> Details(int id)
        {
            var album = await _storeContext.Albums.FindAsync(id);

            if (album != null)
            {
                return View(album);
            }

            _logger.Warn($"Album with Id = {id} was not found.");
            return HttpNotFound();
        }

        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            return PartialView(
                _storeContext.Genres.OrderByDescending(
                    g => g.Albums.Sum(a => a.OrderDetails.Sum(od => od.Quantity))).Take(9).ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _storeContext.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}