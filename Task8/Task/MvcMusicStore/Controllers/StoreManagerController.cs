﻿using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MvcMusicStore.Infrastructure.Logging;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class StoreManagerController : Controller
    {
        private readonly ILogger _logger;
        private readonly MusicStoreEntities _storeContext = new MusicStoreEntities();

        public StoreManagerController(ILogger logger)
        {
            _logger = logger;
        }

        // GET: /StoreManager/
        public async Task<ActionResult> Index()
        {
            return View(await _storeContext.Albums
                .Include(a => a.Genre)
                .Include(a => a.Artist)
                .OrderBy(a => a.Price).ToListAsync());
        }

        // GET: /StoreManager/Details/5
        public async Task<ActionResult> Details(int id = 0)
        {
            var album = await _storeContext.Albums.FindAsync(id);
            
            if (album == null)
            {
                _logger.Warn($"Album with Id = {id} was not found for Details action.");
                return HttpNotFound();
            }

            return View(album);
        }

        // GET: /StoreManager/Create
        public async Task<ActionResult> Create()
        {
            return await BuildView(null);
        }

        // POST: /StoreManager/Create
        [HttpPost]
        public async Task<ActionResult> Create(Album album)
        {
            if (ModelState.IsValid)
            {
                _storeContext.Albums.Add(album);
                
                await _storeContext.SaveChangesAsync();
                
                _logger.Debug($"Album {album.Genre}:{album.Artist} with Id = {album.AlbumId} was created.");
                return RedirectToAction("Index");
            }

            return await BuildView(album);
        }

        // GET: /StoreManager/Edit/5
        public async Task<ActionResult> Edit(int id = 0)
        {
            var album = await _storeContext.Albums.FindAsync(id);
            if (album == null)
            {
                _logger.Warn($"Album with Id = {id} was not found for Edit action.");
                return HttpNotFound();
            }

            return await BuildView(album);
        }

        // POST: /StoreManager/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                _storeContext.Entry(album).State = EntityState.Modified;

                await _storeContext.SaveChangesAsync();
                
                _logger.Debug($"Album with Id = {album.AlbumId} was changed.");
                return RedirectToAction("Index");
            }

            return await BuildView(album);
        }

        // GET: /StoreManager/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            var album = await _storeContext.Albums.FindAsync(id);
            if (album == null)
            {
                _logger.Warn($"Album with Id = {id} was not found for Delete action.");
                return HttpNotFound();
            }

            return View(album);
        }

        // POST: /StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var album = await _storeContext.Albums.FindAsync(id);
            if (album == null)
            {
                _logger.Warn($"Album with Id = {id} was not found for Delete action.");
                return HttpNotFound();
            }

            _storeContext.Albums.Remove(album);

            await _storeContext.SaveChangesAsync();

            _logger.Debug($"Album with Id = {id} was deleted.");
            return RedirectToAction("Index");
        }

        private async Task<ActionResult> BuildView(Album album)
        {
            ViewBag.GenreId = new SelectList(
                await _storeContext.Genres.ToListAsync(),
                "GenreId",
                "Name",
                album == null ? null : (object)album.GenreId);

            ViewBag.ArtistId = new SelectList(
                await _storeContext.Artists.ToListAsync(),
                "ArtistId",
                "Name",
                album == null ? null : (object)album.ArtistId);

            return View(album);
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