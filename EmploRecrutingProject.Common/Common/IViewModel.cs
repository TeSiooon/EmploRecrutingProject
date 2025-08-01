using System.Linq.Expressions;

namespace EmploRecrutingProject.Common.Common;

public interface IViewModel<TViewModel, TEntity>
{
    static abstract Expression<Func<TEntity, TViewModel>> GetMapping();
    static abstract TViewModel From(TEntity source);
    static abstract TViewModel? FromNullable(TEntity? source);
}