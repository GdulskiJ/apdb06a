using System.ComponentModel.DataAnnotations;

namespace WebApplication3.DTOs;

public record GetAllAnimalResponse(int IdAnimal, string Name, string Descryption, string Category, string Area);
public record CreateAnimalRequest(int IdAnimal, string Name, string Descryption, string Category, string Area);
public record ControllerCreateAnimalRequest(
    [Required] [MaxLength(200)] int IdAnimal,
    [Required] [MaxLength(200)] string Name,
    [Required] [MaxLength(200)] string Descryption, 
    [Required] [MaxLength(200)] string Category, 
    [Required] [MaxLength(200)] string Area
    );
