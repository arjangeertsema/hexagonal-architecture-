global using Common.CQRS.Abstractions;
global using Common.CQRS.Abstractions.Attributes;

global using Common.DDD.Abstractions;
global using Common.IAM.Abstractions.Commands;

global using Domain.Abstractions;
global using Domain.Abstractions.Events;
global using Domain.Abstractions.Ports;
global using Domain.Abstractions.UseCases;

global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using Zeebe.Client;
global using Zeebe.Client.Api.Responses;
global using Zeebe.Client.Bootstrap.Abstractions;
global using Zeebe.Client.Bootstrap.Extensions;