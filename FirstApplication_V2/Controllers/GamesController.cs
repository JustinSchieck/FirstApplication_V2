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
    [Authorize]
    public class GamesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Games
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games/Details/5
        [AllowAnonymous]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Game game = db.Games.Find(id);
            Game game = db.Games.Include(x=>x.Genres.Select(g=>g.Genre)).SingleOrDefault(y=>y.GameId == id);
            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            Game model = new Game();
            model.Name = String.Format("Game - {0}", DateTime.Now.Ticks);

            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", model.Genres.Select(x => x.GenreId));

            return View(model);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IsMultiplayer, GenreIds")] Game game, string[] GenreIds)
        {
            if (ModelState.IsValid)
            {
                Game checkGame = db.Games.SingleOrDefault(x => x.Name == game.Name && x.IsMultiplayer == game.IsMultiplayer);

                if (checkGame == null)
                {
                    game.GameId = Guid.NewGuid().ToString();
                    game.CreateDate = DateTime.Now;
                    game.EditDate = game.CreateDate;

                    db.Games.Add(game);
                    db.SaveChanges();

                    foreach (string genreId in GenreIds)
                    {
                        GameGenre gameGenre = new Models.GameGenre();

                        gameGenre.GameGenreId = Guid.NewGuid().ToString();
                        gameGenre.CreateDate = DateTime.Now;
                        gameGenre.EditDate = gameGenre.CreateDate;

                        gameGenre.GameId = game.GameId;
                        gameGenre.GenreId = genreId;
                        db.GameGenres.Add(gameGenre);

                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                else
                {
                    ModelState.AddModelError("", "Duplicate Game Detected.");
                }
            }
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", GenreIds);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game model = db.Games.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", model.Genres.Select(x => x.GenreId));

            return View(model);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Name,IsMultiplayer,GenreIds")] Game game, string[] GenreIds)
        {
            if (ModelState.IsValid)
            {
                Game tmpgame = db.Games.Find(game.GameId);
                if (tmpgame != null)
                {
                    Game checkGame = db.Games.SingleOrDefault(x=>x.Name == game.Name && x.IsMultiplayer == game.IsMultiplayer && x.GameId != game.GameId);

                    if (checkGame == null)
                    {
                        tmpgame.Name = game.Name;
                        tmpgame.IsMultiplayer = game.IsMultiplayer;
                        tmpgame.EditDate = DateTime.Now;
                        db.Entry(tmpgame).State = EntityState.Modified;

                        //item to remove
                        var removeItems = tmpgame.Genres.Where(x => !GenreIds.Contains(x.GenreId)).ToList();
                        foreach (var removeItem in removeItems)
                        {
                            db.Entry(removeItem).State = EntityState.Deleted;
                        }

                        //items to add
                        if (GenreIds != null)
                        {
                            var addedItems = GenreIds.Where(x => !tmpgame.Genres.Select(y => y.GenreId).Contains(x)).ToList();

                            foreach (string addedItem in addedItems)
                            {
                                GameGenre gameGenre = new GameGenre();

                                gameGenre.GameGenreId = Guid.NewGuid().ToString();
                                gameGenre.CreateDate = DateTime.Now;
                                gameGenre.EditDate = gameGenre.CreateDate;

                                gameGenre.GameId = tmpgame.GameId;
                                gameGenre.GenreId = addedItem;
                                db.GameGenres.Add(gameGenre);

                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicate Game Detected.");
                    }

                }
            }
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", GenreIds);
            return View(game);
        }



        // GET: Games/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Include(x => x.Genres.Select(g => g.Genre)).SingleOrDefault(y => y.GameId == id);
            //Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }

            return View(game);
        }

        

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
            
        {
            //Game game = db.Games.Find(id);
            Game game = db.Games.Include(x => x.Genres.Select(g => g.Genre)).SingleOrDefault(y => y.GameId == id);
            if(game == null)
            {
                return HttpNotFound();
            }

            //delete foreign key objects
            foreach (var item in game.Genres.ToList())
            {
                db.GameGenres.Remove(item);
            }

            db.Games.Remove(game);
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

