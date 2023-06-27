using Microsoft.AspNetCore.Mvc;
using MovieStore.Models;
using MovieStore.Models.Repository;
using MovieStore.ViewModels;
using NToastNotify;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace MovieStore.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genresRepository;
        private readonly IToastNotification _toastNotification;

        public MoviesController(IMovieRepository movieRepository,IGenreRepository genreRepository,IToastNotification toastNotification)
        {
            _movieRepository = movieRepository;
            _genresRepository = genreRepository;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _movieRepository.ListAsync();
            return View(movies);
        }


        public async Task<IActionResult> Create()
        {

            var viewModel = new MovieViewModel
            {
                Genres = await _genresRepository.ListAsync()
            };

            _toastNotification.AddSuccessToastMessage("The movie is added succssufully");
            return View(viewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieViewModel model)
        {
            var files = Request.Form.Files;
            
          
            if(!files.Any())
            {
                model.Genres = await _genresRepository.ListAsync();
                ModelState.AddModelError("Poster","the poster is rrequired");
                
                return View(model);

            }

            var Poster = files.FirstOrDefault();

            if (!ModelState.IsValid)
            {
                model.Genres = await _genresRepository.ListAsync();
                return View(model);
            }


            using var dataStream = new MemoryStream();
            
            await Poster.CopyToAsync(dataStream);

            var allowedExtenstions = new List<string> { ".jpg", ".png" };
            if (!allowedExtenstions.Contains(Path.GetExtension(Poster.FileName.ToLower())))
            {
                model.Genres = await _genresRepository.ListAsync();
                ModelState.AddModelError("Poster", "Only .png, .jpg photo allowed");
                return View(model);
            }


            if (Poster.Length > 1000000)
            {
                model.Genres = await _genresRepository.ListAsync();
                ModelState.AddModelError("Poster", "Poster canot be more than 1 megabyte");
                return View(model);
            }

            var movie = new Movie
            {
                Title = model.Title,
                GenreId = model.GenreId,
                Year = model.Year,
                Rate = model.Rate,
                Storeline = model.Storeline,
                Poster = dataStream.ToArray()
            };

           await _movieRepository.Add(movie);

             return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var movie = await _movieRepository.Get(id);
            if(movie == null) return NotFound();


            var viewModel = new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Rate = movie.Rate,
                Storeline = movie.Storeline,
                Poster = movie.Poster,
                GenreId = movie.GenreId,
                Genres =await _genresRepository.ListAsync()

            };

            return View(nameof(Create), viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MovieViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Genres = await _genresRepository.ListAsync();
                return View("Create", model);     
            }

            var movie = await _movieRepository.Get(model.Id);
            if (movie == null)
                return NotFound();

            var files = Request.Form.Files;
            if(files.Any())
            {
                var Poster = files.FirstOrDefault();

                using var dataStream = new MemoryStream();
                await Poster.CopyToAsync(dataStream);

                var allowedExtenstions = new List<string> { ".jpg", ".png" };

                model.Poster = dataStream.ToArray();
                if (!allowedExtenstions.Contains(Path.GetExtension(Poster.FileName.ToLower())))
                {
                    model.Genres = await _genresRepository.ListAsync();
                    ModelState.AddModelError("Poster", "Only .png, .jpg photo allowed");
                    return View(model);
                }


                if (Poster.Length > 1000000)
                {
                    model.Genres = await _genresRepository.ListAsync();
                    ModelState.AddModelError("Poster", "Poster canot be more than 1 megabyte");
                    return View(model);
                }

                movie.Poster = dataStream.ToArray();

            }


            movie.Title = model.Title;
            movie.Rate = model.Rate;
            movie.GenreId = model.GenreId;
            movie.Storeline = model.Storeline;
            movie.Year = model.Year;
           

            await _movieRepository.Update();

            return RedirectToAction(nameof(Index));

        }
        

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null) return BadRequest();
            var movie = await _movieRepository.Get(Id);
            if (movie == null) return NotFound();

            return View(movie);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();
            var movie = await _movieRepository.Get(id);
            if (movie == null) return NotFound();
            return Ok();
        }




    }
}
