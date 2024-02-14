using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

/*AddAuthor("Bruce","Alexander");
AddAuthor("Zola", "Zola");
AddAuthor("Poul", "Anderson");
AddAuthor("Zola", "Zola");
AddAuthor("Jean", "Anderson");
AddAuthor("Zola", "Zola");
AddAuthor("Fred", "Anderson");
AddAuthor("Michael", "Alexander");

GetPagedAuthors(10);
*/


/*AddBookForExistingAuthor();*/
/*GetBooksOfAuthor();*/

/*ModifierAuthor("Hugo","Hugho");*/

void AddAuthor(string firstname,string lastname)
{
    var author = new Author { FirstName = firstname, LastName = lastname };
    using var context = new PubContext() ;
    context.Authors.Add(author) ;
    context.SaveChanges() ;
}


void AddAuthorWithBooks()
{
    var author = new Author { FirstName = "Pablo", LastName = "Coelho" };
    author.Books.Add(new Book { Title = "L'alchimiste", BasePrice = 20.5m, PublishDate = new DateOnly(1998, 01, 01) });
    author.Books.Add(new Book { Title = "Onze Minute", BasePrice = 15.4m, PublishDate = new DateOnly(2003, 05, 01) });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}


void AddBookForExistingAuthor()
{
    var book = new Book { Title = "The sligh ledge", BasePrice = 17.5m, PublishDate = new DateOnly(1990, 02, 02),AuthorId = 1 };

    using var context = new PubContext();
    context.Books.Add(book);
    context.SaveChanges();
}


void GetAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.ToList();

    foreach (var item in authors)
    {
        Console.WriteLine($"Auteur : {item.FirstName} {item.LastName}, avec l'ID {item.Id}");
    }

}

void GetSortedAuthors()
{
    using var context = new PubContext();
    var authors = context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();

    foreach (var item in authors)
    {
        Console.WriteLine($"Auteur : {item.LastName} {item.FirstName}, avec l'ID {item.Id}");
    }
}
void GetPagedAuthors(int nbparpage)
{
    using var context = new PubContext();
    int nbofauthors = (context.Authors.Count());
    for (global::System.Int32 i = 0; i < Math.Ceiling(((decimal)nbofauthors / nbparpage)); i++)
    {
        Console.WriteLine($"Page : {i + 1}");
        var authors = context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).Skip(i * nbparpage).Take(nbparpage).ToList();
        foreach (var item in authors)
        {
            Console.WriteLine($"Auteur : {item.FirstName} {item.LastName}, avec l'ID {item.Id}");
        }
    }
}


void GetBooksOfAuthor()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(b => b.Books).ToList();
    foreach (var item in authors)
    {
        Console.WriteLine($"Auteur : {item.FirstName} {item.LastName}, avec l'ID {item.Id}");
        foreach (var item1 in item.Books)
        {
            Console.WriteLine($"\tLivre : {item1.Title}, {item1.BasePrice}");
        }
        
    }
}


void ModifierAuthor(string Nom,string newNom)
{
    using var context = new PubContext();
    var authors = context.Authors.Where(a=>a.LastName == Nom).ToList();
    foreach (var item in authors)
    {
        item.LastName = newNom;
    }
    Console.WriteLine("Avant : " + context.ChangeTracker.DebugView.ShortView);
    context.ChangeTracker.DetectChanges();
    Console.WriteLine("Apres : " + context.ChangeTracker.DebugView.ShortView);
    context.SaveChanges();
}

//___________________________________Save sans tracking_____________Il faut que dans PubContext le stracking sois desactivé
//ModifAuteurNoTracking("Hugho", "Hugo");
void ModifAuteurNoTracking(string Nom,string newNom)
{
    using var context = new PubContext();
    var authors = context.Authors.Where(a => a.LastName == Nom).ToList();//on peut add .AsNoTracking au lieu de modif le pubcontext
    foreach (var item in authors)
    {
        item.LastName=newNom;
        context.Authors.Update(item);
    }
    /*on peut faire une autre option que le updat dans la boucle
    context.UpdateRange(authors);*/
    context.SaveChanges();
}


//___________________________________

/*deleteAuthorByID(7);*/

//__________________Suppression elt
void DeleteAuthorByID(int id)
{
    using var context = new PubContext();
    var author = context.Authors.Find(id);
    if (author != null)
    {
        context.Authors.Remove(author);
        context.SaveChanges();
        Console.WriteLine($"L'auteur {author.LastName} {author.FirstName} avec l'id {author.Id} a été supprimé!.");
    }
    else
    {
        Console.WriteLine($"L'auteur avec l'id {id} n'existe pas");
    }
    
}

/*DeleteAuthorByLastName("Hugo");*/

void DeleteAuthorByLastName(string lastname)
{
    using var context = new PubContext();
    var authors = context.Authors.Where(a => a.LastName == lastname).ToList();
    /*foreach (var item in authors)
    {
        context.Authors.Remove(item);//on peut use RemoveRange pour enlevert la boucle et supprimeùr direct la liste authors c(est plus opti!!!!
        context.SaveChanges();
        Console.WriteLine($"L'auteur {item.LastName} {item.FirstName} avec l'id {item.Id} a été supprimé!.");
    }*/
    context.Authors.RemoveRange(authors);
    context.SaveChanges();

}
