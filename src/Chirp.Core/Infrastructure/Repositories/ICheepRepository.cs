namespace DomainModel;

public interface ICheepRepository
{
    public Cheep CreateCheep(CheepDTO cheepDTO);
}