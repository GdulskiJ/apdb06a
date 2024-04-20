using System.Data.SqlClient;
using FluentValidation;
using WebApplication3.DTOs;

namespace WebApplication3;

public static class AnimalsEndpoints
{
    public static void RegisterAnimalsEndpoints(this WebApplication app)
    {
        app.MapPost("/api/animals", (IConfiguration configuration, CreateAnimalRequest request, IValidator<CreateAnimalRequest> validator) =>
{
    var validation = validator.Validate(request);
    if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());
            
    using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
    {
        var sqlCommand = new SqlCommand("INSERT INTO Animal (Name, Descryption, Category, Area) VALUES (@name, @description, @category, @area)", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@name", request.Name);
        sqlCommand.Parameters.AddWithValue("@description", request.Descryption);
        sqlCommand.Parameters.AddWithValue("@category", request.Category); 
        sqlCommand.Parameters.AddWithValue("@area", request.Area);
      
        sqlCommand.Connection.Open();
                
        sqlCommand.ExecuteNonQuery();

        return Results.Created("", null);
    }
});

app.MapPut("/api/animals/{idAnimal}", (IConfiguration configuration, int idAnimal,CreateAnimalRequest request, IValidator<CreateAnimalRequest> validator) =>
{
    var validation = validator.Validate(request);
    if (!validation.IsValid) return Results.ValidationProblem(validation.ToDictionary());
            
    using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
    {
        var sqlCommand = new SqlCommand("UPDATE Animal SET Name = @name, Descryption = @description, Category = @category, Area = @area WHERE IdAnimal = @idAnimal", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@name", request.Name);
        sqlCommand.Parameters.AddWithValue("@description", request.Descryption);
        sqlCommand.Parameters.AddWithValue("@category", request.Category);
        sqlCommand.Parameters.AddWithValue("@area", request.Area);
        sqlCommand.Parameters.AddWithValue("@idAnimal", idAnimal);
      
        sqlCommand.Connection.Open();
                
        var rowsAffected = sqlCommand.ExecuteNonQuery();

        if (rowsAffected == 0)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
});




app.MapGet("api/animals", (IConfiguration configuration, string? orderBy) =>
{
    if (string.IsNullOrEmpty(orderBy))
    {
        orderBy = "name";
    }
    else
    {
        var allowedColumns = new List<string> { "name", "description", "category", "area" };
        if (!allowedColumns.Contains(orderBy.ToLower()))
        {
            return Results.BadRequest("Invalid orderBy parameter. Allowed values: name, description, category, area.");
        }
    }
    
    var students = new List<GetAllAnimalResponse>();
    using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
    {
        // Poprawione zapytanie SQL, aby uwzględnić dynamiczne sortowanie
        var sqlCommand = new SqlCommand("SELECT * FROM Animal ORDER BY " + @orderBy + " ASC", sqlConnection);
        sqlCommand.Parameters.AddWithValue(@orderBy,orderBy);
        sqlCommand.Connection.Open();
        var reader = sqlCommand.ExecuteReader();

        while (reader.Read())
        {
            students.Add(new GetAllAnimalResponse(
                reader.GetInt32(0), 
                reader.GetString(1), 
                reader.GetString(2), 
                reader.GetString(3), 
                reader.GetString(4))
            );
        }
    }
            
    return Results.Ok(students);
});
app.MapDelete("/api/animals/{idAnimal}", (IConfiguration configuration, int idAnimal) =>
{
    using (var sqlConnection = new SqlConnection(configuration.GetConnectionString("Default")))
    {
        var sqlCommand = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @idAnimal", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@idAnimal", idAnimal);
      
        sqlCommand.Connection.Open();
                
        var rowsAffected = sqlCommand.ExecuteNonQuery();

        if (rowsAffected == 0)
        {
            return Results.NotFound();
        }

        return Results.NoContent();
    }
});
    }
}