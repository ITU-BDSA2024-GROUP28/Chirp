namespace DomainModel;

public interface ICheepRepository
{
    public Cheep ReadCheep(CheepDTO cheepDTO);
}