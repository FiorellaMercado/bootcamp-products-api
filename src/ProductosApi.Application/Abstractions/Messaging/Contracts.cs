namespace ProductosApi.Application.Abstractions.Messaging;

/// <summary>Mensaje que produce una respuesta.</summary>
public interface IRequest<out TResponse> { }

/// <summary>Orden: CAMBIA el estado del sistema.</summary>
public interface ICommand<out TResponse> : IRequest<TResponse> { }

/// <summary>Pregunta: solo LEE, no modifica.</summary>
public interface IQuery<out TResponse> : IRequest<TResponse> { }

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken ct);
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>;

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

/// <summary>Paso intermedio del pipeline (validación, logging...). Lo usaremos en la fase 6.5.</summary>
public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct);
}

/// <summary>Punto de entrada: los endpoints solo conocen esto.</summary>
public interface ISender
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken ct = default);
}