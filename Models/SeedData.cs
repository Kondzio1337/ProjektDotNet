using Microsoft.EntityFrameworkCore;
using Projekt.Data;

namespace Projekt.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ProjektContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ProjektContext>>()))
            {
                if (context == null || context.Produkt == null||context.kategorie==null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.

                 
                
 
                context.kategorie.AddRange(
                    new Kategoria
                    {

                        CategoryName = "Ogrod",
                        Description = "Jestem dla ogrodu",
                        
                    },
                    new Kategoria
                    {

                        CategoryName = "Dom",
                        Description = "Jestem dla domu"
                    }

                    );
                //context.SaveChanges();


                if (context.Produkt.Any())
                {
                    return;   // DB has been seeded
                }

                context.Produkt.AddRange(
                    new Produkt
                    {
                        Name = "Doniczka",
                        Price = 7.99M,
                        
                    },

                    new Produkt
                    {
                        Name = "Podstawka",
                        Price = 8.99M,
                       
                    },

                    new Produkt
                    {
                        Name = "Talerz",
                        Price = 9.99M,
                        
                    },

                    new Produkt
                    {
                        Name = "Stol",
                        Price = 3.99M,
                        
                    }
                ) ;
                context.SaveChanges();
                context.connectors.AddRange(
                    new Connector
                    {
                        KategoriaId = context.kategorie.Single(x=>x.CategoryName == "Ogrod").CategoryID,
                        ProduktId = context.Produkt.Single(p=>p.Name == "Doniczka").Id
                    },
                    new Connector
                    {
                        KategoriaId = context.kategorie.Single(x => x.CategoryName == "Ogrod").CategoryID,
                        ProduktId = context.Produkt.Single(p => p.Name == "Podstawka").Id
                    },
                    new Connector
                    {
                        KategoriaId = context.kategorie.Single(x => x.CategoryName == "Dom").CategoryID,
                        ProduktId = context.Produkt.Single(p => p.Name == "Talerz").Id
                    },
                    new Connector
                    {
                        KategoriaId = context.kategorie.Single(x => x.CategoryName == "Dom").CategoryID,
                        ProduktId = context.Produkt.Single(p => p.Name == "Stol").Id
                    }, 
                    new Connector
                    {
                        KategoriaId = context.kategorie.Single(x => x.CategoryName == "Dom").CategoryID,
                        ProduktId = context.Produkt.Single(p => p.Name == "Doniczka").Id
                    },
                    new Connector
                    {
                        KategoriaId = context.kategorie.Single(x => x.CategoryName == "Dom").CategoryID,
                        ProduktId = context.Produkt.Single(p => p.Name == "Podstawka").Id
                    }


                    );


                context.SaveChanges();
            }
        }
    }
}
