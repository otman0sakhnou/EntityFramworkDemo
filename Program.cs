

// Initialisation de la base de données avec des données de test

using EntityFrameWorkTp.Data;

using (var context = new AppDbContext())
{
    try
    {
        DataSeeder.Initialize(context);
        Console.WriteLine("Base de données initialisée avec des données de test.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'initialisation: {ex.Message}");
    }
}