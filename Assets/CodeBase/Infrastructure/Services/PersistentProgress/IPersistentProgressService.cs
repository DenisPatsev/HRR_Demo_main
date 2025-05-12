using CodeBase.Infrastructure.Services;

namespace CodeBase.Data.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}