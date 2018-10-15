// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: protos/CRUD.proto
// </auto-generated>
// Original file comments:
// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace RestLib {
  /// <summary>
  /// The CRUDs service definition.
  /// </summary>
  public static partial class CRUDs
  {
    static readonly string __ServiceName = "RestLib.CRUDs";

    static readonly grpc::Marshaller<global::RestLib.QryProxy> __Marshaller_RestLib_QryProxy = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::RestLib.QryProxy.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::RestLib.CCProxy> __Marshaller_RestLib_CCProxy = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::RestLib.CCProxy.Parser.ParseFrom);

    static readonly grpc::Method<global::RestLib.QryProxy, global::RestLib.CCProxy> __Method_CCFill = new grpc::Method<global::RestLib.QryProxy, global::RestLib.CCProxy>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "CCFill",
        __Marshaller_RestLib_QryProxy,
        __Marshaller_RestLib_CCProxy);

    static readonly grpc::Method<global::RestLib.CCProxy, global::RestLib.CCProxy> __Method_CCUpdate = new grpc::Method<global::RestLib.CCProxy, global::RestLib.CCProxy>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CCUpdate",
        __Marshaller_RestLib_CCProxy,
        __Marshaller_RestLib_CCProxy);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::RestLib.CRUDReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of CRUDs</summary>
    public abstract partial class CRUDsBase
    {
      public virtual global::System.Threading.Tasks.Task CCFill(global::RestLib.QryProxy request, grpc::IServerStreamWriter<global::RestLib.CCProxy> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task<global::RestLib.CCProxy> CCUpdate(global::RestLib.CCProxy request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for CRUDs</summary>
    public partial class CRUDsClient : grpc::ClientBase<CRUDsClient>
    {
      /// <summary>Creates a new client for CRUDs</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public CRUDsClient(grpc::Channel channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for CRUDs that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public CRUDsClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected CRUDsClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected CRUDsClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual grpc::AsyncServerStreamingCall<global::RestLib.CCProxy> CCFill(global::RestLib.QryProxy request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CCFill(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::RestLib.CCProxy> CCFill(global::RestLib.QryProxy request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_CCFill, null, options, request);
      }
      public virtual global::RestLib.CCProxy CCUpdate(global::RestLib.CCProxy request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CCUpdate(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::RestLib.CCProxy CCUpdate(global::RestLib.CCProxy request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CCUpdate, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::RestLib.CCProxy> CCUpdateAsync(global::RestLib.CCProxy request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CCUpdateAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::RestLib.CCProxy> CCUpdateAsync(global::RestLib.CCProxy request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CCUpdate, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override CRUDsClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new CRUDsClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(CRUDsBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CCFill, serviceImpl.CCFill)
          .AddMethod(__Method_CCUpdate, serviceImpl.CCUpdate).Build();
    }

  }
}
#endregion
