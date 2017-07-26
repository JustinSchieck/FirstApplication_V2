using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstApplication_V2.Models;

namespace FirstApplication_V2.Controllers
{
    public class GenresController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Genres
        public ActionResult Index()
        {
            return View(db.Genres.ToList());
        }

        // GET: Genres/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Genre genre = db.Genres.Find(id);
            Genre genre = db.Genres.Include(x => x.Games.Select(g => g.Game)).SingleOrDefault(y => y.GenreId == id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            return View(genre);
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            Genre model = new Genre();
            model.Name = String.Format("Genre - {0}", DateTime.Now.Ticks);
            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GameId", "Name", model.Games);
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, GameIds")] Genre genre, string[] GameIds)
        {
            if (ModelState.IsValid)
            {
                Game checkGenre = db.Games.SingleOrDefault(x => x.Name == genre.Name);

                if (checkGenre == null)
                {
                genre.GenreId = Guid.NewGuid().ToString();
                genre.CreateDate = DateTime.Now;
                genre.EditDate = genre.CreateDate;

                db.Genres.Add(genre);
                db.SaveChanges();
                    foreach (string gameId in GameIds)
                    {
                        GameGenre gameGenre = new Models.GameGenre();

                        gameGenre.GameGenreId = Guid.NewGuid().ToString();
                        gameGenre.CreateDate = DateTime.Now;
                        gameGenre.EditDate = genre.CreateDate;

                        gameGenre.GenreId = genre.GenreId;
                        gameGenre.GameId = gameId;
                        db.GameGenres.Add(gameGenre);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Duplicate Genre Detected.");
            }
            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GameId", "Name", GameIds);
            return View(genre);
        }

        // GET: Genres/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GameId", "Name", genre.Games);
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenreId,Name,CreateDate,GameIds")] Genre genre, string[] GameIds)
        {
            if (ModelState.IsValid)
            {
                Game checkGenre = db.Games.SingleOrDefault(x => x.Name == genre.Name);

                if (checkGenre == null)
                {
                    genre.EditDate = DateTime.Now;
                    db.Entry(genre).State = EntityState.Modified;
                    //item to remove
                    var removeItems = genre.Games.Where(x => !GameIds.Contains(x.GameId)).ToList();
                    foreach (var removeItem in removeItems)
                    {
                        db.Entry(removeItem).State = EntityState.Deleted;
                    }

                    if (GameIds != null)
                    {
                        var addedItems = GameIds.Where(x => !genre.Games.Select(y => y.GameId).Contains(x)).ToList();

                        foreach (string addedItem in addedItems)
                        {
                            GameGenre gameGenre = new GameGenre();

                            gameGenre.GameGenreId = Guid.NewGuid().ToString();
                            gameGenre.CreateDate = DateTime.Now;
                            gameGenre.EditDate = gameGenre.CreateDate;

                            gameGenre.GameId = addedItem;
                            gameGenre.GenreId = genre.GenreId;
                            db.GameGenres.Add(gameGenre);
                        }
                    }

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate Genre Detected.");
                }
            }
            ViewBag.Games = new MultiSelectList(db.Games.ToList(), "GameId", "Name", genre.Games);
            return View(genre);
        }

        // GET: Genres/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = db.Genres.Include(x => x.Games.Select(g => g.Game)).SingleOrDefault(y => y.GenreId == id);
            //Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Genre genre = db.Genres.Include(x => x.Games.Select(g => g.Game)).SingleOrDefault(y => y.GenreId == id);
            if (genre == null)
            {
                return HttpNotFound();
            }

            foreach (var item in genre.Games.ToList())
            {
                db.GameGenres.Remove(item);
            }

            db.Genres.Remove(genre);
            var deleted = db.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
