namespace BoardBard.Core.Interfaces;

public interface IRepositoryFactory
{
    IRepository<T> CreateRepository<T>() where T : class;
}