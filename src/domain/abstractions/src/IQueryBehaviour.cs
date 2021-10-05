using System.Threading;
using System.Threading.Tasks;

namespace Reference.Domain.Abstractions
{
    public delegate Task<TResponse> QueryBehaviourDelegate<TResponse>();
    
    public interface IQueryBehaviour<in TQuery, TResponse>
        where TQuery : notnull, IQuery<TResponse>
    {
        Task<TResponse> Handle(TQuery query, IAttributeCollection attributeCollection, CancellationToken cancellationToken, QueryBehaviourDelegate<TResponse> next);
    }
}