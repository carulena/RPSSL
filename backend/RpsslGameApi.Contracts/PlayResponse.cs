using RpsslGameApi.Domain.Entities;
namespace RpsslGameApi.Contracts;

public class PlayResponse
{
    public required string Result { get; set; }
    public required int Player {get; set;}
    public required int Computer {get; set;}
   
}