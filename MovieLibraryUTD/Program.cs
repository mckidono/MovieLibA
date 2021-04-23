using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieLibraryOO.Context;
using MovieLibraryOO.Data;
using MovieLibraryOO.DataModels;

namespace MovieLibraryUTD
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            Console.WriteLine("1:Create a new movie");
            Console.WriteLine("2:Show Movies");
            Console.WriteLine("3:Update");
            Console.WriteLine("4:Delete");
            int desc;
            desc=Console.Read();
            switch(desc){
                case 1:
            // Crud - CREATE
             System.Console.WriteLine("Enter NEW movie title: ");
             var create = Console.ReadLine();

             using (var db = new MovieContext())
             {
                 var movie = new Movie() {
                     Title = create
                 };
                 db.Movies.Add(movie);
                 db.SaveChanges();

                 var newMovie = db.Movies.FirstOrDefault(x => x.Title == create);
                 System.Console.WriteLine($"({newMovie.Id}) {newMovie.Title}");
             }
             break;

                case 2:
             using (var db = new MovieContext())
            {
                var movie = db.Movies.Include(x=>x.Title)
                    .FirstOrDefault(movie=>movie.Title.Contains("Babe"));
                System.Console.WriteLine($"Movie: {movie.Title} {movie.ReleaseDate.ToString("MM-dd-yyyy")}");

                System.Console.WriteLine("Title:");
                foreach (var title in movie.Title) 
                {
                    System.Console.WriteLine($"\t{movie.Title}");
                }
            }
            
            break;

            case 3:
            // crUd - UPDATE
             System.Console.WriteLine("Enter Movie Title to Update: ");
             var occ3 = Console.ReadLine();

             System.Console.WriteLine("Enter Updated Movie Title: ");
             var occUpdate = Console.ReadLine();

             using (var db = new MovieContext())
             {
                 var updateMovie = db.Movies.FirstOrDefault(x => x.Title == occ3);
                 System.Console.WriteLine($"({updateMovie.Id}) {updateMovie.Title}");

                 updateMovie.Title = occUpdate;

                 db.Movies.Update(updateMovie);
                 db.SaveChanges();

             }
             break;

            case 4:
            // cruD - DELETE
             System.Console.WriteLine("Enter Movie Title to Delete: ");
             var occ4 = Console.ReadLine();

             using (var db = new MovieContext())
             {
                 var deleteMovie = db.Movies.FirstOrDefault(x => x.Title == occ4);
                 System.Console.WriteLine($"({deleteMovie.Id}) {deleteMovie.Title}");

                 // verify exists first
                 db.Movies.Remove(deleteMovie);
                 db.SaveChanges();
             }
             break;

           
            }
           

            Console.WriteLine("\nThanks for using the Movie Library!");
        }
    }
}