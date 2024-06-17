using Authorization.Application.Handlers.Command.DeleteUser;
using Authorization.Application.Handlers.Command.UpdatePassword;
using Authorization.Application.Handlers.Users.Command.CreateUser;
using Grpc.Core;
using MediatR;

namespace Authorization.API.gRPC
{
    public class GRPCUsersService : UsersService.UsersServiceBase
    {
        private readonly IMediator _mediator;

        public GRPCUsersService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CreateUserReply> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var query = new CreateUserCommand()
            {
                UserId = request.UserId,
                Login = request.Login,
                PasswordHash = request.PasswordHash
            };

            await _mediator.Send(query, context.CancellationToken);
            return new CreateUserReply { };
        }

        public override async Task<UpdatePasswordReply> UpdatePassword(UpdatePasswordRequest request, 
            ServerCallContext context)
        {
            var query = new UpdatePasswordCommand()
            {
                UserId = request.UserId,
                PasswordHash = request.PasswordHash
            };

            await _mediator.Send(query, context.CancellationToken);
            return new UpdatePasswordReply { };
        }


        public override async Task<DeleteUserReply> DeleteUser(DeleteUserRequest request,
            ServerCallContext context)
        {
            var query = new DeleteUserCommand()
            {
                UserId = request.UserId
            };

            await _mediator.Send(query, context.CancellationToken);
            return new DeleteUserReply { };
        }
    }
}
