using System.Threading.Tasks;

using Qvc.Executables;

namespace Qvc.Handlers
{
    public interface IHandleCommand<in TCommand> : IHandleExecutable
        where TCommand : ICommand
    {
        void Handle(TCommand command);
    }

    public interface IHandleCommandAsync<in TCommand> : IHandleExecutable
        where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}