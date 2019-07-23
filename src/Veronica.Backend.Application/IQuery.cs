namespace Veronica.Backend.Application
{
    public interface IQuery<out TResponse> : MediatR.IRequest<TResponse>
    {
        
    }
}