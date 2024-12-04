# Redis clone

Includes a very basic implementation of redis server with a few commands in go and a client in C#.

Both are written from scratch without any dependencies, besides standard libraries.

Utilizes the RESP protocol for communication

## Server

- Only a few commands implemented
- Supports multiple clients
- Lacks many features of redis such as snapshotting, expiry, replication, etc.
- Only runs on port `6379`, not configurable.

You can use any redis compatible client to connect to the server, or even netcat.

### Implemented Commands

- PING
- SET
- GET
- DEL

## Client

Client is a .NET Core v6.0 console application without any dependencies.

- `EXIT`, to exit the client

## Running for development

### Running the server

The server requires go to be installed

```bash
cd redis-server-go
go run main.go
```

### Running the client

client requires .NET Core 6.0 to be installed

```bash
cd redis-cli-client/redis-client
dotnet run -- -h localhost -p 6379
```

Defaults to localhost:6379
