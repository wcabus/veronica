namespace Veronica.Backend.Application
{
    public interface ICommand<out TResponse> : MediatR.IRequest<TResponse>, ICommand
    {
        
    }

    public interface ICommand : MediatR.IRequest
    {

    }
}